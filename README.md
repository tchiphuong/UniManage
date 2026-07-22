<div align="center">

# 🏢 UniManage — Enterprise Resource Planning

**Hệ thống Quản lý Doanh nghiệp Toàn diện**

[![GitHub Issues](https://img.shields.io/github/issues/tchiphuong/UniManage?style=for-the-badge&logo=github&label=Issues)](https://github.com/tchiphuong/UniManage/issues)
[![GitHub Issues Open](https://img.shields.io/github/issues-raw/tchiphuong/UniManage?style=for-the-badge&color=orange&label=Open)](https://github.com/tchiphuong/UniManage/issues?q=is%3Aopen)
[![GitHub Issues Closed](https://img.shields.io/github/issues-closed/tchiphuong/UniManage?style=for-the-badge&color=green&label=Closed)](https://github.com/tchiphuong/UniManage/issues?q=is%3Aclosed)
[![GitHub Milestones](https://img.shields.io/github/milestones/open/tchiphuong/UniManage?style=for-the-badge&color=blue&label=Sprints)](https://github.com/tchiphuong/UniManage/milestones)

[![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Next.js](https://img.shields.io/badge/Next.js_15-000000?style=flat-square&logo=nextdotjs&logoColor=white)](https://nextjs.org/)
[![React](https://img.shields.io/badge/React_19-61DAFB?style=flat-square&logo=react&logoColor=black)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=flat-square&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_v4-06B6D4?style=flat-square&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)

</div>

---

## 📊 Tiến Độ Dự Án (Realtime)

| Sprint | Chủ Đề | Deadline | Tiến Độ |
| :---: | :--- | :---: | :--- |
| **01** | Core System & Security | `07/08/2026` | [![Sprint 01](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/1?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/1) |
| **02** | Master Data Foundations | `21/08/2026` | [![Sprint 02](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/2?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/2) |
| **03** | Sales & Quotations | `04/09/2026` | [![Sprint 03](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/3?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/3) |
| **04** | Purchasing & Procurement | `18/09/2026` | [![Sprint 04](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/4?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/4) |
| **05** | Inventory & Stock Count | `02/10/2026` | [![Sprint 05](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/5?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/5) |
| **06** | Production Planning | `16/10/2026` | [![Sprint 06](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/6?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/6) |
| **07** | Quality & Workflows | `30/10/2026` | [![Sprint 07](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/7?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/7) |
| **08** | Finance & Accounting | `13/11/2026` | [![Sprint 08](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/8?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/8) |
| **09** | Human Resources & Payroll | `27/11/2026` | [![Sprint 09](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/9?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/9) |
| **10** | CRM, Assets & Analytics | `11/12/2026` | [![Sprint 10](https://img.shields.io/github/milestones/progress-percent/tchiphuong/UniManage/10?style=flat-square&label=)](https://github.com/tchiphuong/UniManage/milestone/10) |

> 🎉 **Go-Live Production v1.0**: 18/12/2026

---

## 🏗️ Kiến Trúc Hệ Thống

```
UniManage/                          # Monorepo root
├── backend/                       # ASP.NET Core .NET 9
│   ├── src/
│   │   ├── 01.Shared/            # Domain, Application, Infrastructure, Resource
│   │   ├── 02.Modules/           # Modular Monolith (System, HR, Master...)
│   │   ├── 03.Apps/              # WebApi (5297), IdentityServer (5000), Worker
│   │   └── 04.Tools/             # CodeGen & Utilities
│   └── scripts/                  # SQL Migration Scripts
│
├── frontend/uni-manage/           # Next.js 15 Dashboard
│   ├── app/                      # App Router pages
│   ├── components/               # Common UI wrappers (HeroUI v3)
│   ├── hooks/                    # Custom React hooks
│   ├── services/                 # API service layer
│   └── messages/                 # i18n (vi.json, en.json)
│
├── auto_test/                     # Playwright E2E Test Suite
├── docs/                          # System Documentation
├── tasks/                         # Task Backlog & Sprint Plans
└── .agents/skills/               # AI Agent Skills (BA, QA, PL, Senior)
```

---

## ⚙️ Tech Stack

### Backend
| Công nghệ | Mục đích |
| :--- | :--- |
| **ASP.NET Core .NET 9** | Web API Framework |
| **Clean Architecture + CQRS** | Tách biệt Read/Write, MediatR Pipeline |
| **EF Core 9 + Dapper** | Hybrid ORM: EF Core (Write) + Dapper (Read) |
| **Duende IdentityServer** | OAuth2 / JWT Authentication |
| **FluentValidation** | Request validation |
| **log4net SiftingAppender** | Audit logging per API per day |
| **Hangfire** | Background job scheduling |

### Frontend
| Công nghệ | Mục đích |
| :--- | :--- |
| **Next.js 15 (App Router)** | React framework, SSR/SSG |
| **React 19 + TypeScript** | UI library |
| **Tailwind CSS v4** | Utility-first CSS |
| **HeroUI v3** | Component library (wrapped in `@/components/common`) |
| **TanStack Query v5** | Server state management |
| **Zustand** | Client state management |
| **next-intl** | Internationalization (VI/EN) |

---

## 📦 16 Phân Hệ ERP

| # | Module | Trạng thái | Issues |
| :---: | :--- | :---: | :--- |
| 01 | **Dashboard & Workspace** | 🟡 Planned | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:dashboard?style=flat-square&label=&color=1d76db)](https://github.com/tchiphuong/UniManage/labels/module%3Adashboard) |
| 02 | **System (Quản trị)** | 🟢 Sprint 01 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:system?style=flat-square&label=&color=5319e7)](https://github.com/tchiphuong/UniManage/labels/module%3Asystem) |
| 03 | **Master Data (Danh mục)** | 🟡 Sprint 02 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:master-data?style=flat-square&label=&color=0075ca)](https://github.com/tchiphuong/UniManage/labels/module%3Amaster-data) |
| 04 | **Sales (Bán hàng)** | ⚪ Sprint 03 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:sales?style=flat-square&label=&color=e4e669)](https://github.com/tchiphuong/UniManage/labels/module%3Asales) |
| 05 | **Purchasing (Mua hàng)** | ⚪ Sprint 04 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:purchasing?style=flat-square&label=&color=d876e3)](https://github.com/tchiphuong/UniManage/labels/module%3Apurchasing) |
| 06 | **Inventory (Kho)** | ⚪ Sprint 05 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:inventory?style=flat-square&label=&color=006b75)](https://github.com/tchiphuong/UniManage/labels/module%3Ainventory) |
| 07 | **Production (Sản xuất)** | ⚪ Sprint 06 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:production?style=flat-square&label=&color=b60205)](https://github.com/tchiphuong/UniManage/labels/module%3Aproduction) |
| 08 | **Quality Management** | ⚪ Sprint 07 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:quality?style=flat-square&label=&color=fbca04)](https://github.com/tchiphuong/UniManage/labels/module%3Aquality) |
| 09 | **Finance & Accounting** | ⚪ Sprint 08 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:finance?style=flat-square&label=&color=0e8a16)](https://github.com/tchiphuong/UniManage/labels/module%3Afinance) |
| 10 | **Human Resources** | ⚪ Sprint 09 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:hr?style=flat-square&label=&color=c5def5)](https://github.com/tchiphuong/UniManage/labels/module%3Ahr) |
| 11 | **CRM** | ⚪ Sprint 10 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:crm?style=flat-square&label=&color=f9d0c4)](https://github.com/tchiphuong/UniManage/labels/module%3Acrm) |
| 12 | **Project Management** | ⚪ Sprint 10 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:project?style=flat-square&label=&color=bfdadc)](https://github.com/tchiphuong/UniManage/labels/module%3Aproject) |
| 13 | **Asset Management** | ⚪ Sprint 10 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:asset?style=flat-square&label=&color=d4c5f9)](https://github.com/tchiphuong/UniManage/labels/module%3Aasset) |
| 14 | **Workflow & Approval** | ⚪ Sprint 07 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:workflow?style=flat-square&label=&color=c2e0c6)](https://github.com/tchiphuong/UniManage/labels/module%3Aworkflow) |
| 15 | **Document Management** | ⚪ Sprint 10 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:document?style=flat-square&label=&color=fef2c0)](https://github.com/tchiphuong/UniManage/labels/module%3Adocument) |
| 16 | **Reports & Analytics** | ⚪ Sprint 10 | [![](https://img.shields.io/github/issues/tchiphuong/UniManage/module:reports?style=flat-square&label=&color=ededed)](https://github.com/tchiphuong/UniManage/labels/module%3Areports) |

---

## 🚀 Quick Start

### Yêu cầu
- .NET 9 SDK
- Node.js 20+ LTS
- SQL Server 2019+

### 1. Backend

```bash
# Tạo database UniManage & UniManageLog, chạy migration scripts
cd backend/scripts && # Xem README.md trong thư mục này

# Chạy IdentityServer (port 5000)
cd backend/src/03.Apps/UniManage.IdentityServer && dotnet run

# Chạy WebApi (port 5297)
cd backend/src/03.Apps/UniManage.WebApi && dotnet run
```

### 2. Frontend

```bash
cd frontend/uni-manage
npm install
cp .env.example .env.local    # Cấu hình NEXT_PUBLIC_API_URL
npm run dev                    # http://localhost:3000
```

### 3. Chạy tất cả (Windows)

```bash
./run-all.bat     # Khởi chạy IdentityServer + WebApi + Frontend
./stop-all.bat    # Dừng tất cả
```

---

## 📚 Tài Liệu

| Tài liệu | Mô tả |
| :--- | :--- |
| [📐 Kiến Trúc Hệ Thống](docs/architecture/overview.md) | Clean Architecture, CQRS, Hybrid ORM |
| [🎨 Thiết Kế & ERD](docs/architecture/system-design.md) | Component Diagram, Database Schema, JWT Flow |
| [🔄 Luồng Nghiệp Vụ](docs/business/) | Sales O2C, Procurement P2P, Inventory, Production |
| [💻 Hướng Dẫn Cài Đặt](docs/development/setup-guide.md) | Backend, Frontend, SQL Server setup |
| [📏 Quy Chuẩn Lập Trình](docs/development/coding-standards.md) | Naming, CQRS rules, HeroUI Wrapper, i18n |
| [🔁 Quy Trình SDLC](docs/development/project-workflow.md) | 7 giai đoạn SDLC + 5-step Feature Workflow |
| [🌐 REST API Specs](docs/api/overview.md) | Result\<T\>, PagedResult\<T\>, JWT Auth |
| [📖 User Manuals](docs/manuals/) | Hướng dẫn sử dụng 16 phân hệ |

---

## 🔗 Links

- 📋 [**Issues Board**](https://github.com/tchiphuong/UniManage/issues) — Danh sách công việc
- 🏁 [**Milestones**](https://github.com/tchiphuong/UniManage/milestones) — Sprint schedule & progress
- 🏷️ [**Labels**](https://github.com/tchiphuong/UniManage/labels) — Module, Priority, Type labels
- 📡 **Swagger**: `http://localhost:5297/swagger`

---

## 📄 License

Private / Proprietary
