---
name: unimanage-senior-engineer
description: Senior Software Engineer & Technical Architect skill for UniManage. Focuses on advanced backend (.NET 9 CQRS, Dapper/EF Core hybrid performance), frontend optimization (Next.js 15, HeroUI v3), security hardening, database query tuning, fault tolerance, and refactoring best practices. Use when designing complex features, optimizing performance, refactoring legacy code, or hardening security.
---

# UniManage Senior Software Engineer Skill

## Overview

Skill này quy định chuẩn mực kỹ thuật cao cấp, tư duy tối ưu hóa hệ thống, bảo mật và refactoring mã nguồn dành cho vị trí **Senior Software Engineer / Technical Architect** trong dự án UniManage.

---

## 1. Tư Duy Tối Ưu Hóa Hiệu Năng (Performance Optimization)

### 1.1. Backend (.NET 9 & Database)
- **Tối Ưu Hybrid ORM**:
  - Thao tác Ghi (Write/Command): Dùng EF Core 9 với `DbContext` có Transaction Scope.
  - Thao tác Đọc (Read/Query): **Ưu tiên Dapper** với câu lệnh SQL thuần được tối ưu Index (Clustered / Non-Clustered Indexes).
  - Tránh triệt để lỗi **N+1 Query** trong EF Core (luôn dùng `Include` / `AsNoTracking` cho read-only entities).
- **Xử Lý Bất Đồng Bộ (Async/Await Best Practices)**:
  - Bắt buộc truyền `CancellationToken` cho tất cả các thao tác bất đồng bộ (DbContext, Dapper, HTTP Client).
  - Tuyệt đối không dùng `.Result` hoặc `.Wait()` để tránh gây thread starvation / deadlock.
- **Caching Strategy**:
  - Áp dụng `IMemoryCache` cho dữ liệu danh mục ít biến động (`SyConfigs`, `SyLanguages`).
  - Sử dụng Redis / Distributed Cache cho Session Token và Permission Cache.

### 1.2. Frontend (Next.js 15 & HeroUI v3)
- **Server vs Client Components**: Maximize Server Components (RSC) để giảm Kích thước Bundle gửi về Client. Chỉ dùng `'use client'` khi có tương tác state hoặc event handlers.
- **Memoization**: Dùng `useCallback` và `useMemo` hợp lý để tránh re-render không cần thiết trên các bảng dữ liệu lớn.
- **Debounce Input**: Tất cả các ô tìm kiếm dữ liệu (Search Inputs) bắt buộc qua `useDebounce` (300-500ms) trước khi trigger API call.

---

## 2. Bảo Mật & An Toàn Dữ Liệu (Security Hardening)

1. **Phòng Chống OWASP Top 10**:
   - **SQL Injection**: 100% câu lệnh Dapper phải dùng Parameterized Queries (`new { Id = id }`), cấm nối chuỗi SQL thủ công.
   - **XSS & Injection**: Tự động sanitize input và mã hóa HTML output.
   - **CSRF & CORS**: Cấu hình CORS Policy chặt chẽ trong `UniManage.WebApi` chỉ cho phép các domain được cấp phép.
2. **Mã Hóa & Bảo Vệ Credentials**:
   - Chuỗi kết nối Database trong production bắt buộc phải qua mã hóa bằng công cụ mã hóa `UniManage.Core`.
   - Mật khẩu người dùng được hash bằng chuẩn an toàn (BCrypt / Argon2 / Identity PasswordHasher).

---

## 3. Quy Chuẩn Refactoring & Clean Code

- **Tái Cấu Trúc An Toàn (Safe Refactoring)**:
  - Không thay đổi hành vi API công khai (Breaking Changes) trừ khi có sự đồng ý từ PL/BA.
  - Viết/chạy E2E & Unit Test trước khi refactoring để đảm bảo không gãy tính năng cũ.
- **Documentation Standards**:
  - Tất cả JSDoc / TSDoc / XML Doc comments (`/// <summary>`) **bắt buộc viết bằng tiếng Anh**.
  - Tự động phát hiện và loại bỏ Code Smell, Magic Numbers, Duplicate Code.

---

## 4. Checklist Đánh Giá Mã Nguồn Cấp Senior (Senior Code Review Checklist)

- [ ] **Data Access Efficiency**: Đã dùng Dapper cho Query phân trang lớn chưa? Có index CSDL phù hợp chưa?
- [ ] **Async Flow**: Tất cả các async method có nhận `CancellationToken` không?
- [ ] **Error Handling**: Lỗi API được bắt và chuyển hóa qua `Result<T>` chuẩn với `returnCode != 0` chưa?
- [ ] **Memory & Resource Leak**: Các kết nối `IDbConnection`, `Stream`, `HttpClient` đã được giải phóng (IDisposable / async using) đúng cách chưa?
- [ ] **Logging & Auditing**: Command/Query mới đã triển khai `ILoggableCommand` chưa?
