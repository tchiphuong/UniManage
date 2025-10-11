# 📋 API Roadmap - UniManage System

## ✅ APIs Đã Có (Existing)

### 🔐 Authentication & Authorization

-   ✅ `POST /api/auth/login` - Đăng nhập (COMPLETED)
-   ⚠️ `POST /api/auth/logout` - Đăng xuất (TODO: implement logic)

### 👥 User Management (Commands/Queries exist)

-   Commands: CreateUser, UpdateUser, DeleteUser
-   Queries: GetUserById, GetUserList

### 👨‍💼 Employee Management

-   Commands: CreateEmployee, UpdateEmployee, DeleteEmployee
-   Queries: GetEmployeeById, GetEmployeeList

### 🏢 Unit/Department Management

-   Commands: CreateUnit, UpdateUnit, DeleteUnit
-   Queries: GetUnitById, GetUnitList

### 🌐 System

-   Query: GetLanguageList

---

## 🚀 APIs Cần Bổ Sung (Recommended)

### 1. 🔐 Authentication & Security (Priority: HIGH)

#### Refresh Token

```
POST /api/auth/refresh
Request: { refreshToken: string }
Response: { accessToken, refreshToken, expiresIn }
```

**Lý do**: Token hết hạn sau 1h, cần refresh không logout

#### Get Current User

```
GET /api/auth/me
Headers: Authorization: Bearer {token}
Response: { user: UserInfo }
```

**Lý do**: Load thông tin user khi reload page

#### Change Password

```
POST /api/auth/change-password
Request: { oldPassword, newPassword }
Response: { success: boolean }
```

**Lý do**: User cần đổi password

#### Forgot Password

```
POST /api/auth/forgot-password
Request: { email: string }
Response: { success, message }
```

**Lý do**: User quên password

#### Reset Password

```
POST /api/auth/reset-password
Request: { token, newPassword }
Response: { success: boolean }
```

**Lý do**: Reset password qua email token

---

### 2. 👥 User Management APIs (Priority: HIGH)

#### List Users (Paged)

```
GET /api/users?pageIndex=1&pageSize=20&keyword=search
Response: PagedResponse<UserDto>
```

**Status**: ⚠️ Query exists, need Controller endpoint

#### Get User by ID

```
GET /api/users/{id}
Response: ApiResponse<UserDto>
```

**Status**: ⚠️ Query exists, need Controller endpoint

#### Create User

```
POST /api/users
Request: { userCode, displayName, email, password, roleCode }
Response: ApiResponse<{ id }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

#### Update User

```
PUT /api/users/{id}
Request: { displayName, email, roleCode, status }
Response: ApiResponse<{ success }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

#### Delete User

```
DELETE /api/users/{id}
Response: ApiResponse<{ success }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

#### Activate/Deactivate User

```
PATCH /api/users/{id}/status
Request: { status: 0|1 }
Response: ApiResponse<{ success }>
```

**Lý do**: Enable/disable user account

---

### 3. 🏢 Organization/Department APIs (Priority: MEDIUM)

#### List Departments

```
GET /api/departments?pageIndex=1&pageSize=20
Response: PagedResponse<DepartmentDto>
```

#### Get Department Tree

```
GET /api/departments/tree
Response: ApiResponse<DepartmentNode[]>
```

**Lý do**: Hiển thị cây phòng ban

#### Create Department

```
POST /api/departments
Request: { code, name, parentId, managerId }
Response: ApiResponse<{ id }>
```

#### Update Department

```
PUT /api/departments/{id}
Request: { name, parentId, managerId, status }
Response: ApiResponse<{ success }>
```

#### Delete Department

```
DELETE /api/departments/{id}
Response: ApiResponse<{ success }>
```

---

### 4. 👨‍💼 Employee Management APIs (Priority: MEDIUM)

#### List Employees (Paged)

```
GET /api/employees?pageIndex=1&pageSize=20&departmentId=1
Response: PagedResponse<EmployeeDto>
```

**Status**: ⚠️ Query exists, need Controller endpoint

#### Get Employee by ID

```
GET /api/employees/{id}
Response: ApiResponse<EmployeeDto>
```

**Status**: ⚠️ Query exists, need Controller endpoint

#### Create Employee

```
POST /api/employees
Request: { code, name, email, phone, departmentId, positionId }
Response: ApiResponse<{ id }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

#### Update Employee

```
PUT /api/employees/{id}
Request: { name, email, phone, departmentId, status }
Response: ApiResponse<{ success }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

#### Delete Employee

```
DELETE /api/employees/{id}
Response: ApiResponse<{ success }>
```

**Status**: ⚠️ Command exists, need Controller endpoint

---

### 5. 🎯 Role & Permission Management (Priority: MEDIUM)

#### List Roles

```
GET /api/roles
Response: ApiResponse<RoleDto[]>
```

#### Get Role by ID

```
GET /api/roles/{id}
Response: ApiResponse<RoleDto>
```

#### Create Role

```
POST /api/roles
Request: { code, name, description, permissions }
Response: ApiResponse<{ id }>
```

#### Update Role

```
PUT /api/roles/{id}
Request: { name, description, permissions }
Response: ApiResponse<{ success }>
```

#### Delete Role

```
DELETE /api/roles/{id}
Response: ApiResponse<{ success }>
```

#### Get Role Permissions

```
GET /api/roles/{id}/permissions
Response: ApiResponse<PermissionDto[]>
```

#### Update Role Permissions

```
PUT /api/roles/{id}/permissions
Request: { permissionIds: number[] }
Response: ApiResponse<{ success }>
```

---

### 6. 📊 Dashboard & Statistics (Priority: LOW)

#### Dashboard Summary

```
GET /api/dashboard/summary
Response: {
  totalUsers, activeUsers, totalEmployees,
  totalDepartments, recentActivities
}
```

#### User Statistics

```
GET /api/dashboard/users/stats
Response: {
  byRole: [], byStatus: [], byDepartment: []
}
```

#### Activity Logs

```
GET /api/dashboard/activities?pageIndex=1&pageSize=20
Response: PagedResponse<ActivityDto>
```

---

### 7. 📝 Audit Log (Priority: LOW)

#### List Audit Logs

```
GET /api/audit-logs?pageIndex=1&pageSize=20&userId=1
Response: PagedResponse<AuditLogDto>
```

#### Get Audit Log by ID

```
GET /api/audit-logs/{id}
Response: ApiResponse<AuditLogDto>
```

---

### 8. 🌐 Localization/Resources (Priority: LOW)

#### Get Languages

```
GET /api/languages
Response: ApiResponse<LanguageDto[]>
```

**Status**: ⚠️ Query exists, need Controller endpoint

#### Get Resources by Language

```
GET /api/resources/{languageCode}
Response: ApiResponse<{ [key]: value }>
```

#### Update Resource

```
PUT /api/resources/{id}
Request: { value, languageCode }
Response: ApiResponse<{ success }>
```

---

### 9. 📁 File Upload/Download (Priority: MEDIUM)

#### Upload File

```
POST /api/files/upload
Request: FormData with file
Response: ApiResponse<{ fileId, url, name }>
```

#### Download File

```
GET /api/files/{id}/download
Response: File stream
```

#### Delete File

```
DELETE /api/files/{id}
Response: ApiResponse<{ success }>
```

---

### 10. 🔔 Notifications (Priority: LOW)

#### List Notifications

```
GET /api/notifications?pageIndex=1&pageSize=20
Response: PagedResponse<NotificationDto>
```

#### Mark as Read

```
PATCH /api/notifications/{id}/read
Response: ApiResponse<{ success }>
```

#### Mark All as Read

```
PATCH /api/notifications/read-all
Response: ApiResponse<{ success }>
```

---

## 📊 Priority Summary

### 🔴 **HIGH Priority** (Cần làm ngay)

1. ✅ Login API (DONE)
2. 🔄 Refresh Token API
3. 👤 Get Current User API
4. 👥 User CRUD APIs (connect Controller to existing Commands/Queries)
5. 🔑 Change Password API

### 🟡 **MEDIUM Priority** (Làm tiếp theo)

1. 👨‍💼 Employee CRUD APIs (connect Controller)
2. 🏢 Department/Unit CRUD APIs
3. 🎯 Role Management APIs
4. 📁 File Upload/Download

### 🟢 **LOW Priority** (Làm sau)

1. 📊 Dashboard Statistics
2. 📝 Audit Logs
3. 🌐 Localization Management
4. 🔔 Notifications

---

## 🎯 Recommended Next Steps

### Phase 1: Complete Authentication (Week 1)

-   [ ] Implement Refresh Token API
-   [ ] Implement Get Current User API
-   [ ] Implement Change Password API
-   [ ] Add JWT to existing API responses

### Phase 2: Core CRUD APIs (Week 2)

-   [ ] Connect UserController to existing Commands/Queries
-   [ ] Create User List/Detail pages in frontend
-   [ ] Create User Create/Edit forms
-   [ ] Test full CRUD flow

### Phase 3: Employee & Department (Week 3)

-   [ ] Connect EmployeeController
-   [ ] Connect DepartmentController
-   [ ] Create Employee management UI
-   [ ] Create Department tree UI

### Phase 4: Advanced Features (Week 4+)

-   [ ] Role & Permission management
-   [ ] File upload/download
-   [ ] Dashboard statistics
-   [ ] Audit logs

---

## 💡 Code Structure for New APIs

### Example: RefreshTokenCommand

```csharp
// Command
public sealed class RefreshTokenCommand : IRequest<ApiResponse<RefreshTokenCommand.Response>>
{
    public string RefreshToken { get; init; } = default!;

    public sealed class Response
    {
        public string AccessToken { get; init; } = default!;
        public string RefreshToken { get; init; } = default!;
        public int ExpiresIn { get; init; }
    }
}

// Validator
public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required");
    }
}

// Handler
public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenCommand.Response>>
{
    // Implementation
}
```

### Example: Controller Endpoint

```csharp
[HttpPost("refresh")]
public async Task<ActionResult<ApiResponse<RefreshTokenCommand.Response>>> RefreshToken(
    [FromBody] RefreshTokenCommand request,
    CancellationToken ct)
{
    var response = await _mediator.Send(request, ct);
    return response.ReturnCode == 0 ? Ok(response) : BadRequest(response);
}
```

---

**Bạn muốn tôi implement API nào trước?**
