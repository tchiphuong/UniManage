# 🚀 UniManage - Quick Start Guide

Hướng dẫn nhanh để chạy UniManage (Backend + Frontend) trong môi trường development.

## 📋 Prerequisites

Đảm bảo bạn đã cài đặt:

-   ✅ .NET 9 SDK
-   ✅ SQL Server 2019+
-   ✅ Node.js 18+ LTS
-   ✅ npm 9+

## 🎯 Quick Start (3 bước)

### 1️⃣ Setup Database

```sql
-- Tạo database
CREATE DATABASE UniManage;

-- Chạy migration scripts (trong backend/docs/ hoặc backend/scripts/)
-- Tạo tables: sy_languages, sy_resources, Users, etc.
```

### 2️⃣ Run Backend

```bash
# Terminal 1
cd backend/src
dotnet restore
dotnet run --project UniManage.Api
```

✅ Backend API: **http://localhost:5000/api**

### 3️⃣ Run Frontend

```bash
# Terminal 2
cd frontend

# First time only
npm install
cp .env.example .env

# Start dev server
npm run dev
```

✅ Frontend: **http://localhost:3000**

## 🔧 Configuration

### Backend (`backend/src/UniManage.Api/appsettings.Development.json`)

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=UniManage;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
    },
    "Database": {
        "Server": "localhost",
        "Database": "UniManage",
        "Username": "sa",
        "Password": "encrypted_password_here",
        "UseEncryption": true
    }
}
```

### Frontend (`frontend/.env`)

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

## 📂 Project Structure

```
UniManage/
├── backend/
│   ├── src/
│   │   ├── UniManage.Api/              # REST API
│   │   ├── UniManage.Application/      # CQRS Commands/Queries
│   │   ├── UniManage.Core/             # Domain Models
│   │   ├── UniManage.Resource/         # Localization
│   │   └── UniManage.IdentityServer/   # Auth
│   ├── docs/                           # Documentation
│   └── scripts/                        # PowerShell scripts
│
├── frontend/
│   ├── src/
│   │   ├── components/                 # React components
│   │   ├── pages/                      # Pages
│   │   ├── services/                   # API clients
│   │   ├── store/                      # Zustand stores
│   │   └── types/                      # TypeScript types
│   ├── public/                         # Static files
│   └── package.json
│
└── README.md                           # Main documentation
```

## 🧪 Testing

### Backend

```bash
cd backend/src
dotnet test
```

### Frontend

```bash
cd frontend
npm run lint
npm run build  # Test build
```

## 🛠️ Common Tasks

### Generate Resources from Database

```powershell
cd backend/src/UniManage.Resource
powershell -ExecutionPolicy Bypass -File GenerateCoreResource.ps1
```

### Encrypt Database Password

```powershell
cd backend/scripts
.\EncryptPassword.ps1 -Password "YourPassword"
```

### Build for Production

**Backend:**

```bash
cd backend/src
dotnet publish -c Release -o ../../publish/backend
```

**Frontend:**

```bash
cd frontend
npm run build
# Output: frontend/dist/
```

## 📚 Documentation

-   **Main README**: `README.md` - Tổng quan dự án
-   **Backend Guide**: `backend/docs/` - Chi tiết backend architecture
-   **Frontend Guide**: `frontend/README.md` - Chi tiết frontend setup
-   **Encryption Guide**: `README.md` (section Database Encryption)

## 🐛 Troubleshooting

### Backend không kết nối được database

1. Kiểm tra SQL Server đang chạy
2. Kiểm tra connection string trong `appsettings.Development.json`
3. Kiểm tra password đã được encrypt đúng chưa

### Frontend không gọi được API

1. Kiểm tra backend đang chạy tại `http://localhost:5000`
2. Kiểm tra `.env` có `VITE_API_BASE_URL` đúng chưa
3. Kiểm tra CORS settings trong backend

### Build errors

**Backend:**

```bash
dotnet clean
dotnet restore
dotnet build
```

**Frontend:**

```bash
rm -rf node_modules package-lock.json
npm install
```

## 🎨 Tech Stack Summary

| Layer                  | Technology              |
| ---------------------- | ----------------------- |
| **Backend Framework**  | ASP.NET Core 9          |
| **Architecture**       | CQRS + MediatR          |
| **Database**           | SQL Server              |
| **ORM**                | Dapper                  |
| **Logging**            | log4net (by date & API) |
| **Auth**               | Duende IdentityServer   |
| **Frontend Framework** | React 18 + TypeScript   |
| **Build Tool**         | Vite 6                  |
| **Styling**            | Tailwind CSS 3.4        |
| **State Management**   | Zustand                 |
| **HTTP Client**        | Axios                   |

## 📞 Support

Nếu gặp vấn đề, kiểm tra:

1. Main README.md
2. backend/docs/ và frontend/README.md
3. GitHub Issues

---

**Happy Coding!** 🎉
