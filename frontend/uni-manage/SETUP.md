# Setup Instructions

## 1. Cài đặt dependencies

```bash
cd frontend/uni-manage
npm install
```

## 2. Environment Variables

File `.env.local` đã tạo sẵn với config mặc định:

-   API: http://localhost:5000/api
-   IdentityServer: http://localhost:5001

## 3. Chạy development server

```bash
npm run dev
```

## 4. Cấu trúc đã setup

✅ **Đã hoàn thành:**

-   ✅ Tailwind CSS v4 configured
-   ✅ TypeScript strict mode
-   ✅ API Client với axios + interceptors
-   ✅ Type definitions (ApiResponse, PagedResponse, PagingInfo)
-   ✅ Utilities (formatCurrency, formatDate, validation helpers)
-   ✅ Custom hooks (useMounted, useDebounce)
-   ✅ Reusable components (PageHeader, LoadingScreen)
-   ✅ Environment configuration
-   ✅ Folder structure chuẩn

**Folders:**

```
app/              # Pages (Next.js App Router)
components/       # UI components
lib/              # API client & utilities
hooks/            # Custom React hooks
types/            # TypeScript interfaces
```

## 5. Next Steps

-   [ ] Cài thêm UI library nếu cần (shadcn/ui, MUI, Ant Design)
-   [ ] Setup authentication flow
-   [ ] Tạo layout với sidebar/navbar
-   [ ] Implement CRUD pages
-   [ ] Add form validation với zod + react-hook-form

## 6. API Integration Example

```typescript
import { apiClient } from "@/lib";

// GET request
const response = await apiClient.get<UserProfile>("/users/me");
if (response.returnCode === 0) {
    console.log(response.data);
}

// POST request with pagination
const listResponse = await apiClient.post<PagedResult<User>>("/users/list", {
    keyword: "test",
    pageIndex: 1,
    pageSize: 20,
});
```

## Notes

-   HeroUI beta version có issues, đã remove tạm thời
-   Dùng native HTML elements với Tailwind styling
-   API client tự động thêm Bearer token từ localStorage
-   Tất cả responses theo format ApiResponse<T>
