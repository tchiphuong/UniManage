# Quy Trình Phát Triển Dự Án & Chuẩn Tải Lượng Kiểm Thử (SDLC Workflow & Test Cases)

Tài liệu này quy định chi tiết 7 giai đoạn phát triển phần mềm UniManage. **Mỗi giai đoạn BẮT BỘC phải có đầy đủ Tài liệu (Documentation) và Kịch bản kiểm thử (Test Cases)** tương ứng trước khi chuyển giao sang giai đoạn tiếp theo.

---

## 1. Chi Tiết 7 Giai Đoạn SDLC (Kèm Tài Liệu & Test Cases)

```
[1. Phân Tích Yêu Cầu] ➔ [2. Thiết Kế Hệ Thống] ➔ [3. Setup & Quy Chuẩn] ➔ [4. Phát Triển Tính Năng]
                                                                                      │
[7. Bảo Trì & Vận Hành] ◄── [6. Triển Khai Release] ◄── [5. QA & Kiểm Thử Toàn Diện] ◄┘
```

---

### Giai Đoạn 1: Phân Tích Yêu Cầu (Requirement Analysis)

- **Công việc**: Khảo sát nghiệp vụ cùng Product Owner (PO), xác định phạm vi hệ thống.
- **📄 Tài liệu bắt buộc**:
  - `BRD / SRS`: Tài liệu tả chi tiết yêu cầu phần mềm và luồng nghiệp vụ.
  - `User Stories & AC`: Danh sách tính năng kèm điều kiện nghiệm thu (Acceptance Criteria).
- **🧪 Test Cases bắt buộc**:
  - `Acceptance Test Cases`: Kịch bản kiểm thử nghiệm thu yêu cầu (Dựa trên Acceptance Criteria của từng User Story).

---

### Giai Đoạn 2: Thiết Kế Hệ Thống & UI/UX (System Design)

- **Công việc**: Thiết kế kiến trúc tổng thể, mô hình CSDL và giao diện người dùng.
- **📄 Tài liệu bắt buộc**:
  - `Database ERD Diagram`: Sơ đồ quan hệ thực thể các bảng SQL Server.
  - `API Specification`: Tài liệu định dạng RESTful API (Swagger OpenAPI spec draft).
  - `UI/UX Figma Design`: Bản vẽ mockup chi tiết từng màn hình.
- **🧪 Test Cases bắt buộc**:
  - `API Contract Test Cases`: Kịch bản kiểm tra cấu trúc dữ liệu Request/Response của API.
  - `Database Integrity Test Cases`: Test case kiểm tra ràng buộc toàn vẹn CSDL (Unique, Foreign Keys, Nullability).

---

### Giai Đoạn 3: Khởi Tạo Dự Án & Quy Chuẩn (Project Setup & Governance)

- **Công việc**: Dựng Base Code Monorepo, cấu hình Linter, CI/CD pipeline và thiết lập môi trường.
- **📄 Tài liệu bắt buộc**:
  - `Setup Guide`: Hướng dẫn cài đặt môi trường phát triển ([setup-guide.md](setup-guide.md)).
  - `Coding Standards`: Quy chuẩn đặt tên, cấu trúc dự án ([coding-standards.md](coding-standards.md)).
- **🧪 Test Cases bắt buộc**:
  - `Build & Lint Test Cases`: Kiểm tra tự động mã nguồn compile không lỗi và tuân thủ ESLint/Stylelint.
  - `Environment Health Check`: Kịch bản test kết nối CSDL, IdentityServer và Redis/Hangfire.

---

### Giai Đoạn 4: Phát Triển Tính Năng (Implementation / Coding)

- **Công việc**: Lập trình Backend (CQRS .NET 9) và Frontend (Next.js 15 / HeroUI v3) theo từng Sprint.
- **📄 Tài liệu bắt buộc**:
  - `Code Doc Comments`: Doc comments chuẩn tiếng Anh (`///` XML doc / JSDoc) cho các class/interface/function.
  - `Pull Request Description`: Mô tả chi tiết thay đổi và danh sách file cập nhật trong PR.
- **🧪 Test Cases bắt buộc**:
  - `Unit Test Cases`: Test case kiểm thử logic nghiệp vụ độc lập của từng Command/Query Handler (.NET xUnit/NUnit).
  - `Component Test Cases`: Test case kiểm tra khả năng render và tương tác của UI Components.

---

### Giai Đoạn 5: Kiểm Thử & Đảm Bảo Chất Lượng (Testing & QA)

- **Công việc**: Thực hiện kiểm thử toàn diện hệ thống trên môi trường Staging/Testing.
- **📄 Tài liệu bắt buộc**:
  - `Test Plan & Test Matrix`: Kế hoạch và ma trận bao phủ kiểm thử (Test Coverage).
  - `Bug Reports`: Báo cáo danh sách lỗi phát sinh kèm mức độ nghiêm trọng (Severity).
  - `Test Summary Report`: Báo cáo nghiệm thu chất lượng trước khi release.
- **🧪 Test Cases bắt buộc**:
  - `Manual Test Suites`: Bộ test case thủ công cho toàn bộ luồng nghiệp vụ trên giao diện.
  - `Automated E2E Test Suites`: Kịch bản kiểm thử tự động toàn trình end-to-end với Playwright (`e2e-tests/`).
  - `Security & Performance Test Cases`: Test case kiểm tra phân quyền JWT Token, SQL Injection và Load test.

---

### Giai Đoạn 6: Triển Khai & Đóng Gói (Deployment & CI/CD)

- **Công việc**: Đóng gói Docker Container, chạy SQL Migration và phát hành bản Release.
- **📄 Tài liệu bắt buộc**:
  - `Deployment Checklist`: Danh mục các bước chuẩn bị và triển khai Production.
  - `Release Notes`: Nhật ký ghi nhận các tính năng mới và bugfix của phiên bản v1.x.
- **🧪 Test Cases bắt buộc**:
  - `Sanity / Deployment Test Cases`: Bộ kịch bản test nhanh (Smoke Test) ngay sau khi deploy lên Server để đảm bảo hệ thống sẵn sàng.

---

### Giai Đoạn 7: Bảo Trì & Vận Hành (Maintenance & Documentation)

- **Công việc**: Hỗ trợ vận hành, sửa lỗi và cập nhật bộ tài liệu dự án.
- **📄 Tài liệu bắt buộc**:
  - `User Manuals`: Bộ sổ tay hướng dẫn sử dụng chi tiết theo từng màn hình trong `docs/manuals/`.
  - `API Overview & Troubleshooting`: Tài liệu khắc phục sự cố hệ thống (`docs/api/` & `docs/architecture/`).
- **🧪 Test Cases bắt buộc**:
  - `Regression Test Suites`: Bộ test case kiểm thử hồi biến (Regression Testing), đảm bảo khi cập nhật tính năng mới không làm hỏng các tính năng cũ.

---

## 2. Ma Trận Quản Lý Tài Liệu & Test Cases

| Giai đoạn SDLC | Tài liệu bắt buộc (Documentation) | Test Cases bắt buộc |
| :--- | :--- | :--- |
| **1. Phân Tích Yêu Cầu** | BRD, SRS, User Stories & AC | Acceptance Test Cases |
| **2. Thiết Kế Hệ Thống** | ERD Diagram, API Spec, Figma Mockup | API Contract & DB Integrity Test Cases |
| **3. Setup Dự Án** | Setup Guide, Coding Standards | Build, Lint & Health Check Test Cases |
| **4. Phát Triển** | XML/JSDoc Comments, PR Notes | Unit Test Cases & Component Tests |
| **5. Kiểm Thử (QA)** | Test Plan, Bug Reports, Test Summary | Manual Suites, Playwright E2E, Load Tests |
| **6. Triển Khai** | Deployment Checklist, Release Notes | Sanity & Smoke Test Cases |
| **7. Bảo Trì & Vận Hành**| User Manuals (`docs/manuals/`), Troubleshooting | Regression Test Suites |
