# UniManage - Master Task Backlog & Roadmap (Toàn Bộ 16 Phân Hệ ERP)

Tài liệu quản lý toàn bộ danh mục công việc (Task Backlog) của hệ thống UniManage được phân chia chi tiết theo 16 phân hệ ERP chuẩn từ [function-list.md](file:///d:/Coding/dot_NET/UniManage/.agents/skills/unimanage-backend/resources/function-list.md).

---

## 📊 Ma Trận Task Backlog 16 Phân Hệ ERP

| STT | Phân Hệ ERP | File Quản Lý Tasks | Số Lượng Tasks | Trạng Thái |
| :--- | :--- | :--- | :---: | :---: |
| **01** | **Dashboard** | [01.dashboard-tasks.md](01.dashboard-tasks.md) | 8 Tasks | 🟡 In Progress |
| **02** | **System (Hệ thống)** | [02.system-tasks.md](02.system-tasks.md) | 12 Tasks | 🟡 In Progress |
| **03** | **Master Data** | [03.master-data-tasks.md](03.master-data-tasks.md) | 18 Tasks | ⚪ Planned |
| **04** | **Sales (Bán hàng)** | [04.sales-tasks.md](04.sales-tasks.md) | 14 Tasks | ⚪ Planned |
| **05** | **Purchasing (Mua hàng)** | [05.purchasing-tasks.md](05.purchasing-tasks.md) | 14 Tasks | ⚪ Planned |
| **06** | **Inventory (Kho)** | [06.inventory-tasks.md](06.inventory-tasks.md) | 15 Tasks | ⚪ Planned |
| **07** | **Production (Sản xuất)** | [07.production-tasks.md](07.production-tasks.md) | 16 Tasks | ⚪ Planned |
| **08** | **Quality Management** | [08.quality-management-tasks.md](08.quality-management-tasks.md) | 10 Tasks | ⚪ Planned |
| **09** | **Finance & Accounting** | [09.finance-accounting-tasks.md](09.finance-accounting-tasks.md) | 20 Tasks | ⚪ Planned |
| **10** | **Human Resources** | [10.human-resources-tasks.md](10.human-resources-tasks.md) | 18 Tasks | ⚪ Planned |
| **11** | **CRM** | [11.crm-tasks.md](11.crm-tasks.md) | 10 Tasks | ⚪ Planned |
| **12** | **Project Management** | [12.project-management-tasks.md](12.project-management-tasks.md) | 12 Tasks | ⚪ Planned |
| **13** | **Asset Management** | [13.asset-management-tasks.md](13.asset-management-tasks.md) | 10 Tasks | ⚪ Planned |
| **14** | **Workflow** | [14.workflow-tasks.md](14.workflow-tasks.md) | 8 Tasks | ⚪ Planned |
| **15** | **Document Management** | [15.document-management-tasks.md](15.document-management-tasks.md) | 8 Tasks | ⚪ Planned |
| **16** | **Reports & Analytics** | [16.reports-tasks.md](16.reports-tasks.md) | 12 Tasks | ⚪ Planned |

---

## ⚙️ Quy Định Gán Nhãn (Task Labels & Priority)

- **`priority: critical`**: Các task nền tảng CSDL, Authentication và Core API.
- **`priority: high`**: Màn hình nghiệp vụ chính (Bán hàng, Mua hàng, Kho, Nhân sự).
- **`priority: medium`**: Báo cáo, Dashboard và các tiện ích nâng cao.

---

## ⏰ Master Sprint Roadmap & Timeline (10 Sprints)

> **Thời gian hiện tại**: Tháng 07/2026  
> **Chu kỳ Sprint**: 2 Tuần / Sprint  
> **Mốc Go-Live Production**: 18/12/2026

| Sprint | Chủ Đề Sprint (Sprint Focus) | Ngày Bắt Đầu | Ngày Kết Thúc | DEADLINE | Milestone |
| :--- | :--- | :---: | :---: | :---: | :---: |
| **Sprint 01** | Core System & Security (Users, Roles, Audit) | 27/07/2026 | 07/08/2026 | **07/08/2026** | 🚀 Alpha Release |
| **Sprint 02** | Master Data Foundations (Org, Partners, Items) | 10/08/2026 | 21/08/2026 | **21/08/2026** | |
| **Sprint 03** | Sales & Quotations (Quotation, SO, Delivery) | 24/08/2026 | 04/09/2026 | **04/09/2026** | |
| **Sprint 04** | Purchasing & Procurement (PR, RFQ, PO, GRN) | 07/09/2026 | 18/09/2026 | **18/09/2026** | 🚀 Beta v0.5 |
| **Sprint 05** | Inventory & Stock Count (Transfers, Counts) | 21/09/2026 | 02/10/2026 | **02/10/2026** | |
| **Sprint 06** | Production Planning & Shopfloor (BOM, MRP, WO) | 05/10/2026 | 16/10/2026 | **16/10/2026** | |
| **Sprint 07** | Quality Management & Workflows (QM, Approval) | 19/10/2026 | 30/10/2026 | **30/10/2026** | 🚀 Beta v0.8 |
| **Sprint 08** | Finance & Accounting (GL, AR, AP, Cash) | 02/11/2026 | 13/11/2026 | **13/11/2026** | |
| **Sprint 09** | Human Resources & Payroll (HR, Attendance, Pay) | 16/11/2026 | 27/11/2026 | **27/11/2026** | 🚀 Release Candidate |
| **Sprint 10** | CRM, Assets, Projects & Analytics | 30/11/2026 | 11/12/2026 | **11/12/2026** | 🏁 Code Freeze |
| **FINAL** | **Nghiệm Thu Toàn Trình & Release Production** | 14/12/2026 | 18/12/2026 | **18/12/2026** | 🎉 **GO-LIVE v1.0** |

### 📌 Chi Tiết Task Board Cho Từng Sprint

- 📌 **Sprint 01**: [sprint-01/](sprint-01/) (Deadline: `07/08/2026`)

> **Lưu ý**: Sprint 02~10 sẽ được tạo folder chi tiết (`sprint-02/`, `sprint-03/`...) khi sprint bắt đầu.
