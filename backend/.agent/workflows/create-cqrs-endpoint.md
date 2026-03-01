# Create CQRS Endpoint

Create a new API endpoint following CQRS pattern with MediatR for the UniManage backend.

## Steps

### 1. Clarify Requirements

Ask the user for these details:

-   What is the resource name? (e.g., User, Department, Product)
-   What operation? (Create, Update, Delete, Get single, List with pagination)
-   What are the input fields?
-   What are the output fields?
-   Any special validation rules?
-   Which database table(s) will be affected?

### 2. Determine Command or Query

Based on the operation:

-   **Create/Update/Delete** → Command (with transaction)
-   **Get single/List** → Query (no transaction)

### 3. Create Command/Query File

Location: `UniManage.Application/Commands/{Module}/{UseCase}/` or `Queries/`

Create file with 3 regions:

1. Command/Query definition (inherit BaseCommand or BaseQuery)
2. Validator (FluentValidation)
3. Handler

**For Command (Create/Update/Delete):**

```csharp
#region Command

public sealed class {Verb}{Noun}Command : BaseCommand, IRequest<ApiResponse<{Verb}{Noun}Command.Response>>
{
    // Input properties
    public string Field1 { get; init; } = default!;
    public string Field2 { get; init; } = default!;

    public sealed class Response
    {
        public int Id { get; init; }
        // Simple result only, not full entity
    }
}

#endregion

#region Validator

public sealed class {Verb}{Noun}Validator : AbstractValidator<{Verb}{Noun}Command>
{
    public {Verb}{Noun}Validator()
    {
        RuleFor(x => x.Field1)
            .NotEmpty().WithMessage("Field1 is required")
            .Must(ValidationHelper.IsValidXXX).WithMessage("Invalid format");
    }
}

#endregion

#region Handler

public sealed class {Verb}{Noun}CommandHandler : IRequestHandler<{Verb}{Noun}Command, ApiResponse<{Verb}{Noun}Command.Response>>
{
    public async Task<ApiResponse<{Verb}{Noun}Command.Response>> Handle({Verb}{Noun}Command request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Field1), request.Field1)
            }
        };

        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Execute SQL with Dapper
                var id = await dbContext.ExecuteScalarAsync<int>(
                    "INSERT INTO table (...) VALUES (...); SELECT SCOPE_IDENTITY();",
                    new { /* params */ },
                    ct);

                await dbContext.CommitAsync(ct);

                var response = ResponseHelper.Success(
                    new {Verb}{Noun}Command.Response { Id = id },
                    "Operation successful");

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(ct);

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                return ResponseHelper.Error<{Verb}{Noun}Command.Response>("Error occurred");
            }
        }
    }
}

#endregion
```

**For Query (Get/List):**

```csharp
#region Query

public sealed class {Get/List}{Noun}Query : BaseQuery, IRequest<ApiResponse<PagedResult<{Get/List}{Noun}Query.Result>>>
{
    // Additional filters (Keyword, PageIndex, PageSize from BaseQuery)
    public string? Status { get; set; }

    public class Result
    {
        // DTO properties for UI
        public long Id { get; set; }
        public string Name { get; set; } = default!;
    }
}

#endregion

#region Validator

public sealed class {Get/List}{Noun}QueryValidator : AbstractValidator<{Get/List}{Noun}Query>
{
    public {Get/List}{Noun}QueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

public sealed class {Get/List}{Noun}QueryHandler : IRequestHandler<{Get/List}{Noun}Query, ApiResponse<PagedResult<{Get/List}{Noun}Query.Result>>>
{
    public async Task<ApiResponse<PagedResult<{Get/List}{Noun}Query.Result>>> Handle({Get/List}{Noun}Query request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword)
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                // Build filters with DatabaseHelper
                var filters = new Dictionary<string, object?>();
                if (!string.IsNullOrEmpty(request.Keyword))
                    filters.Add("Name", QueryHelper.SanitizeSearchTerm(request.Keyword));

                var (whereClause, parameters) = DatabaseHelper.BuildWhereClause(filters);

                // Build ORDER BY
                var columnMappings = new Dictionary<string, string>
                {
                    { "default", "CreatedAt" },
                    { "name", "Name" }
                };
                var (orderBy, _) = QueryHelper.BuildOrderByClause(
                    request.SortBy,
                    request.SortDirection ?? "DESC",
                    columnMappings);

                var baseQuery = "SELECT Id, Name FROM table";

                // Execute with pagination
                var result = await DatabaseHelper.QueryPagingAsync<{Get/List}{Noun}Query.Result>(
                    dbContext,
                    baseQuery,
                    whereClause,
                    orderBy,
                    parameters,
                    request.PageIndex,
                    request.PageSize);

                var response = ResponseHelper.Success(result, "Retrieved successfully");

                log.Result = result;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                var response = ResponseHelper.Error<PagedResult<{Get/List}{Noun}Query.Result>>("Error occurred");

                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
        }
    }
}

#endregion
```

### 4. Create Controller

Location: `UniManage.Api/Controllers/{ModuleName}Controller.cs`

```csharp
[ApiController]
[Route("api/v1/{resource-plural}")]
public class {Resource}Controller : BaseController
{
    #region Properties

    private readonly IMediator _mediator;

    public {Resource}Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region POST: /api/v1/{resource-plural}

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateCommand.Response>>> Create([FromBody] CreateCommand command, CancellationToken ct)
    {
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/{resource-plural}

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ListQuery.Result>>>> GetList([FromQuery] ListQuery query, CancellationToken ct)
    {
        query ??= new ListQuery();
        query.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region GET: /api/v1/{resource-plural}/{id}

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetByIdQuery.Result>>> GetById(long id, CancellationToken ct)
    {
        var query = new GetByIdQuery { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    #endregion

    #region PUT: /api/v1/{resource-plural}/{id}

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UpdateCommand.Response>>> Update([FromRoute] long id, [FromBody] UpdateCommand command, CancellationToken ct)
    {
        command ??= new UpdateCommand();
        command.Id = id;
        command.HeaderInfo = HeaderInfo;
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion

    #region DELETE: /api/v1/{resource-plural}/{id}

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(long id, CancellationToken ct)
    {
        var command = new DeleteCommand { Id = id, HeaderInfo = HeaderInfo };
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    #endregion
}
```

### 5. Check Utilities Before Writing Logic

Review `UniManage.Core/Utilities/` for existing helpers:

-   `PasswordHelper` - Password hashing/validation
-   `ValidationHelper` - Email, phone, code validation
-   `DatabaseHelper` - Query pagination, WHERE clause building
-   `ResponseHelper` - API response creation
-   `StringHelper` - String manipulation
-   `QueryHelper` - SQL query building

**Use utilities instead of reimplementing logic!**

### 6. Write SQL Queries

Use Dapper with parameterized queries:

**Insert:**

```csharp
var id = await dbContext.ExecuteScalarAsync<int>(
    @"INSERT INTO table (Column1, Column2, CreatedBy, CreatedAt)
      VALUES (@Column1, @Column2, @CreatedBy, GETDATE());
      SELECT SCOPE_IDENTITY();",
    new { Column1 = value1, Column2 = value2, CreatedBy = request.HeaderInfo!.Username },
    ct);
```

**Update with Concurrency:**

```csharp
var rows = await dbContext.ExecuteAsync(
    @"UPDATE table
      SET Column1 = @Column1, UpdatedBy = @UpdatedBy, UpdatedAt = GETDATE()
      WHERE Id = @Id AND DataRowVersion = @RowVersion",
    new { Id = id, Column1 = value1, UpdatedBy = user, RowVersion = rowVersion },
    ct);

if (rows == 0)
    throw new DbConcurrencyException("Record was modified by another user");
```

**Delete:**

```csharp
await dbContext.ExecuteAsync(
    "DELETE FROM table WHERE Id = @Id",
    new { Id = id },
    ct);
```

**Select:**

```csharp
var item = await dbContext.QuerySingleOrDefaultAsync<Result>(
    "SELECT Id, Name FROM table WHERE Id = @Id",
    new { Id = id },
    ct);
```

### 7. Test the Endpoint

1. Run the backend: `dotnet run` in `UniManage.Api`
2. Test with Postman or curl:

```bash
# POST (Create)
curl -X POST http://localhost:5297/api/v1/resource \
  -H "Content-Type: application/json" \
  -d '{"field1": "value1", "field2": "value2"}'

# GET (List)
curl http://localhost:5297/api/v1/resource?pageIndex=1&pageSize=20

# GET (Single)
curl http://localhost:5297/api/v1/resource/1

# PUT (Update)
curl -X PUT http://localhost:5297/api/v1/resource/1 \
  -H "Content-Type: application/json" \
  -d '{"field1": "updated"}'

# DELETE
curl -X DELETE http://localhost:5297/api/v1/resource/1
```

### 8. Verify Checklist

✅ Command/Query inherits BaseCommand/BaseQuery
✅ HeaderInfo assigned in Controller
✅ ResponseHelper used for all responses
✅ Utilities used (PasswordHelper, ValidationHelper, etc.)
✅ Validator created with FluentValidation
✅ Logging with CoreLogModel + UniLogManager
✅ Using statements for DbContext
✅ Transaction for Commands, none for Queries
✅ Regions properly organized
✅ No SELECT \*
✅ RESTful URLs (nouns, not verbs)
✅ Parameterized SQL queries

## Summary

This workflow creates a complete CQRS endpoint with:

-   Command/Query with proper inheritance
-   FluentValidation validator
-   Handler with logging and error handling
-   Controller with RESTful routes
-   Dapper SQL queries
-   Proper transaction management
-   ResponseHelper usage
