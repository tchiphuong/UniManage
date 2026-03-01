# Communication Standards

Always On rule for communication and task completion standards.

## Activation

**Mode:** Always On

**Applies to:** All interactions with user

## Language Requirement

**🇻🇳 LUÔN PHẢN HỒI BẰNG TIẾNG VIỆT**

-   Tất cả responses phải bằng tiếng Việt
-   Code comments có thể tiếng Anh (chuẩn quốc tế)
-   Documentation có thể song ngữ (Việt - Anh)
-   Error messages trong code giữ nguyên tiếng Anh (để debug)

**Ngoại lệ:**

-   Technical terms giữ nguyên (API, CQRS, SQL, React, etc.)
-   Code và SQL queries bằng tiếng Anh
-   File names và folder names bằng tiếng Anh
-   Git commit messages bằng tiếng Anh

**Ví dụ responses đúng:**

✅ "Đã tạo xong CreateUserCommand với validator và handler. Bây giờ cần test API endpoint."

✅ "File này có 3 lỗi: validator thiếu SQL query, handler chưa commit transaction, và thiếu response object."

✅ "Sử dụng `PasswordHelper.HashPassword()` để hash password thay vì MD5."

❌ "Created CreateUserCommand with validator and handler. Now need to test API endpoint."

❌ "This file has 3 errors: validator missing SQL query..."

## Task Completion Standard

**🧪 CODE CHƯA TEST = CHƯA DONE**

### Definition of Done

Một task chỉ được coi là hoàn thành khi:

1. ✅ **Code đã viết xong** - Implementation complete
2. ✅ **Code compiles** - Không có compilation errors
3. ✅ **Code đã test** - Đã verify hoạt động đúng
4. ✅ **Test passed** - Tất cả tests đều pass

### Testing Requirements

**Backend (Command/Query):**

```bash
# Test API endpoint với curl hoặc Postman
curl -X POST http://localhost:5000/api/v1/users \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d '{"username":"testuser","email":"test@example.com",...}'

# Hoặc dùng Postman collection
```

**Verification checklist:**

-   [ ] API returns correct status code (200, 201, 400, 404, etc.)
-   [ ] Response format matches ApiResponse/PagedResponse
-   [ ] Validation errors được return đúng format
-   [ ] Data được insert/update/delete đúng trong database
-   [ ] Logs được ghi đúng vào file (check logs/YYYY-MM-DD/)

**Frontend (Component):**

```bash
# Start dev server và test UI
cd frontend/uni-manage
npm run dev

# Hoặc chạy tests
npm test
```

**Verification checklist:**

-   [ ] Component renders without errors
-   [ ] Form validation works
-   [ ] API calls work correctly
-   [ ] Loading states display properly
-   [ ] Error handling works
-   [ ] Success messages show

**Database (Migration):**

```bash
# Run migration script
sqlcmd -S localhost -d UniManage -i migrations/xxx.sql

# Verify changes
SELECT * FROM sy_users; -- Check table structure/data
```

**Verification checklist:**

-   [ ] Script executes without errors
-   [ ] Schema changes applied correctly
-   [ ] Data integrity maintained
-   [ ] Foreign keys work
-   [ ] Indexes created

### Workflow Integration

**Khi tạo mới feature:**

1. Viết code (Command/Query/Component)
2. ✋ **PAUSE** - Chưa gọi là xong!
3. Test functionality
4. Verify results
5. ✅ Confirm completion

**Khi fix bug:**

1. Identify root cause
2. Implement fix
3. ✋ **PAUSE** - Chưa gọi là xong!
4. Reproduce bug và verify fix works
5. Test edge cases
6. ✅ Confirm bug is fixed

**Khi refactor:**

1. Make changes
2. ✋ **PAUSE** - Chưa gọi là xong!
3. Verify all existing functionality still works
4. Run regression tests
5. ✅ Confirm no regressions

### Communication Pattern

**❌ SAI - Báo done quá sớm:**

```
User: "Tạo CreateUserCommand"
Agent: "Đã tạo xong CreateUserCommand! ✅"
User: "Test chưa?"
Agent: "Ơ, chưa test..."
```

**✅ ĐÚNG - Test trước khi báo done:**

```
User: "Tạo CreateUserCommand"
Agent: "Đã tạo xong CreateUserCommand. Bây giờ test API endpoint:
        POST /api/v1/users
        Cần token để test, bạn có sẵn token chưa?"
User: "Có rồi, test đi"
Agent: [Run test]
       "✅ Test thành công! API trả về:
        - Status: 200
        - User created với ID: 123
        - Log đã ghi vào logs/2026-01-17/Users-Create.log
        Task hoàn thành!"
```

### Testing Tools Reference

**Backend:**

-   `curl` - Command line HTTP client
-   Postman - API testing tool (có collection trong `postman/`)
-   `sqlcmd` - SQL Server command line
-   `dotnet test` - Run unit/integration tests

**Frontend:**

-   Browser DevTools - Console, Network tab
-   `npm test` - Run Jest/Vitest tests
-   React DevTools - Component inspection

**Database:**

-   SQL Server Management Studio (SSMS)
-   Azure Data Studio
-   `sqlcmd` command line

### Progress Updates

**Trong quá trình làm task dài:**

```
User: "Tạo user management module"
Agent:
"📋 Todo list:
1. ✅ CreateUserCommand - Done & tested
2. ✅ UpdateUserCommand - Done & tested
3. 🔄 DeleteUserCommand - Đang viết code...
4. ⏳ GetUserQuery - Chưa bắt đầu
5. ⏳ ListUsersQuery - Chưa bắt đầu

Hiện tại đang làm DeleteUserCommand, sẽ test xong mới chuyển sang GetUserQuery."
```

### When Testing Fails

**Nếu test fail:**

1. ❌ Không đánh dấu task là done
2. 📝 Report test failure với details
3. 🔍 Debug và fix issue
4. 🔄 Test lại
5. ✅ Chỉ mark done khi test pass

**Example:**

```
Agent: "Test API endpoint:
       ❌ Test failed!
       Error: 'Column Username does not exist'

       Root cause: SQL query có typo - 'UserName' thay vì 'Username'

       Fixing now..."

[Fix code]

Agent: "Đã fix SQL query. Test lại:
       ✅ Test passed!
       - Status: 200
       - User created: ID 123

       Task hoàn thành!"
```

## Exceptions

**Khi nào KHÔNG cần test ngay:**

1. **Documentation only** - Chỉ viết docs, không code
2. **Config changes** - Chỉ đổi config files
3. **Refactor với existing tests** - Đã có tests coverage tốt
4. **Research tasks** - Chỉ tìm hiểu, không implement

**Nhưng vẫn phải test trước khi deploy!**

## Summary

**Communication:**

-   🇻🇳 Luôn response tiếng Việt
-   Technical terms giữ nguyên tiếng Anh
-   Code và SQL bằng tiếng Anh

**Task Completion:**

-   🧪 Code chưa test = chưa done
-   Luôn test và verify trước khi báo hoàn thành
-   Report test results rõ ràng
-   Nếu test fail → fix → test lại
-   Progress updates trong quá trình làm

**Remember:**

> "Untested code is broken code" - Anonymous
> "Code chưa test là code hỏng" - Nguyên lý cơ bản
