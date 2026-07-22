# UniManage - Tài Liệu Hệ Thống (System Documentation)

Chào mừng bạn đến với trung tâm tài liệu kỹ thuật, quy trình phát triển và hướng dẫn vận hành hệ thống **UniManage**.

---

## 📚 Cấu Trúc Tài Liệu Chính

### 1. 🏗️ Thiết Kế Kiến Trúc Hệ Thống (`/architecture`)
- **[Tổng Quan Kiến Trúc](architecture/overview.md)**: Kiến trúc Clean Architecture, CQRS, Modular Monolith & IdentityServer.
- **[Thiết Kế Chi Tiết & ERD Schema](architecture/system-design.md)**: Sơ đồ thành phần, ERD CSDL, Sequence Diagram xác thực JWT Token và Hybrid ORM.

### 2. 🔄 Tài Liệu Mô Tả Luồng Nghiệp Vụ (`/business`)
- **[Quy Trình Quản Trị & Phân Quyền](business/01.system-flow.md)**: Luồng khởi tạo người dùng, vai trò & RBAC.
- **[Quy Trình Bán Hàng (O2C)](business/02.sales-flow.md)**: Luồng Báo giá ➔ Đơn hàng ➔ Giao hàng ➔ Hóa đơn ➔ Thu tiền.
- **[Quy Trình Mua Hàng (P2P)](business/03.procurement-flow.md)**: Luồng Yêu cầu mua PR ➔ PO ➔ Nhập kho GRN ➔ Thanh toán.
- **[Quy Trình Quản Lý Kho](business/04.inventory-flow.md)**: Luồng Nhập - Xuất - Chuyển kho & Kiểm kê điều chỉnh.
- **[Quy Trình Sản Xuất](business/05.production-flow.md)**: Luồng BOM ➔ MRP ➔ Lệnh sản xuất ➔ Nhập kho thành phẩm.

### 3. 💻 Hướng Dẫn & Quy Trình Phát Triển (`/development`)
- **[Quy Trình Phát Triển Dự Án & Test Cases](development/project-workflow.md)**: Chuẩn 7 giai đoạn SDLC (kèm yêu cầu Tài liệu & Test Cases cho từng bước) và Quy trình 5 bước phát triển tính năng (Feature Workflow).
- **[Hướng Dẫn Cài Đặt](development/setup-guide.md)**: Các bước setup Backend, Frontend, CSDL SQL Server & IdentityServer.
- **[Quy Chuẩn Lập Trình](development/coding-standards.md)**: Quy tắc đặt tên (Naming Conventions), chuẩn CQRS, HeroUI Wrapper Pattern và i18n.

### 4. 🌐 API Documentation (`/api`)
- **[Tổng Quan REST API](api/overview.md)**: Chuẩn cấu trúc phản hồi `Result<T>` (`returnCode === 0`), JWT Authentication & Endpoints.

---

## 📖 Hướng Dẫn Sử Dụng Chi Tiết Cho 16 Phân Hệ (`/manuals`)

| STT | Phân hệ (Module) | Đường dẫn tài liệu hướng dẫn |
| :--- | :--- | :--- |
| **01** | **Dashboard** | [Executive Dashboard](manuals/01.dashboard/01.executive-dashboard.md) \| [My Workspace](manuals/01.dashboard/02.my-workspace.md) |
| **02** | **System (Hệ thống)** | [Users & Roles](manuals/02.system/01.users-roles.md) \| [Configs](manuals/02.system/02.configurations.md) \| [Numbering Rules](manuals/02.system/03.numbering-rules.md) \| [Audit Logs](manuals/02.system/04.audit-logs.md) |
| **03** | **Master Data** | [Organization](manuals/03.master-data/01.organization.md) \| [Partners](manuals/03.master-data/02.business-partners.md) \| [Items & Warehouses](manuals/03.master-data/03.items-warehouses.md) \| [BOM & Routings](manuals/03.master-data/04.bom-routings.md) |
| **04** | **Sales (Bán hàng)** | [Quotations & Sales Orders](manuals/04.sales/01.quotations.md) |
| **05** | **Purchasing (Mua hàng)** | [Purchase Requests & Orders](manuals/05.purchasing/01.purchase-requests.md) |
| **06** | **Inventory (Kho)** | [Stock Receipts, Issues & Count](manuals/06.inventory/01.receipts-issues.md) |
| **07** | **Production (Sản xuất)** | [Production Planning & Work Orders](manuals/07.production/01.production-planning.md) |
| **08** | **Quality Management** | [Quality Inspections (IQC/PQC/OQC)](manuals/08.quality-management/01.quality-inspections.md) |
| **09** | **Finance & Accounting** | [General Ledger & AR/AP](manuals/09.finance-accounting/01.general-ledger.md) |
| **10** | **Human Resources** | [Employee Profiles, Attendance & Payroll](manuals/10.human-resources/01.employee-profiles.md) |
| **11** | **CRM** | [Leads & Sales Opportunities](manuals/11.crm/01.leads-opportunities.md) |
| **12** | **Project Management** | [Projects, Tasks & Budget](manuals/12.project-management/01.projects-tasks.md) |
| **13** | **Asset Management** | [Assets & Maintenance](manuals/13.asset-management/01.assets-maintenance.md) |
| **14** | **Workflow** | [Approval Workflows & Delegations](manuals/14.workflow/01.approvals.md) |
| **15** | **Document Management** | [Document Library & Version Control](manuals/15.document-management/01.documents.md) |
| **16** | **Reports & Analytics** | [Operational & Financial Reports](manuals/16.reports/01.operational-reports.md) |

---

## 🛠️ Công Nghệ Nền Tảng

- **Backend**: ASP.NET Core .NET 9, MediatR, FluentValidation, EF Core 9, Dapper, Duende IdentityServer, Hangfire, log4net.
- **Frontend**: Next.js 15 (App Router), React 19, TypeScript, Tailwind CSS v4, HeroUI v3, TanStack Query v5, Zustand.
- **Database**: Microsoft SQL Server.
