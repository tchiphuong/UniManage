using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Domains.Command.Master.Unit;
using UniManage.Api.Domains.Query.Master.Unit;
using UniManage.Application.Queries.Master.Unit;
using UniManage.Core.Models;

namespace UniManage.Api.Controllers
{
    /// <summary>
    /// API endpoint for unit management operations
    /// </summary>
    [ApiController]
    [Route("api/v1/units")]
    public class UnitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new unit
        /// </summary>
        /// <param name="request">Unit creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created unit information</returns>
        [HttpPost]
        public async Task<ActionResult<CoreResponse>> Create(
            [FromBody] CreateUnitCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get unit by ID
        /// </summary>
        /// <param name="id">Unit ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Unit details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetUnitByIdQuery.Response>>> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var request = new GetUnitByIdQuery { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get list of units with pagination
        /// </summary>
        /// <param name="request">Query parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paginated list of units</returns>
        [HttpGet]
        public async Task<ActionResult<CoreResponse>> GetList(
            [FromQuery] GetUnitListQuery request,
            CancellationToken cancellationToken)
        {
            request ??= new();
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing unit
        /// </summary>
        /// <param name="id">Unit ID</param>
        /// <param name="request">Unit update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<CoreResponse>> Update(
            [FromRoute] int id,
            [FromBody] UpdateUnitCommand request,
            CancellationToken cancellationToken)
        {
            request ??= new();
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete units by IDs
        /// </summary>
        /// <param name="ids">List of unit IDs to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Delete result</returns>
        [HttpDelete]
        public async Task<ActionResult<CoreResponse>> Delete(
            [FromBody] List<int> ids,
            CancellationToken cancellationToken)
        {
            var request = new DeleteUnitCommand { Ids = ids };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
