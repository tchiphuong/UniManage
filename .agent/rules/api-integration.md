# API Integration

**Activation**: Model Decision - Apply when creating frontend services that call backend APIs

This rule ensures consistent API integration patterns between frontend and backend.

## API Client Configuration

### Base HTTP Client Setup

Location: `frontend/uni-manage/lib/http-client.ts`

```tsx
import axios from "axios";

const httpClient = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_URL || "http://localhost:5297/api/v1",
    timeout: 30000,
    headers: {
        "Content-Type": "application/json",
    },
});

// Request interceptor
httpClient.interceptors.request.use(
    (config) => {
        // Add auth token
        const token = localStorage.getItem("access_token");
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

// Response interceptor
httpClient.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            // Redirect to login
            window.location.href = "/auth/login";
        }
        return Promise.reject(error);
    }
);

export { httpClient };
```

## Backend API Response Format

All backend APIs return this format:

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Success message",
    "data": {}
}
```

Paginated responses:

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Success",
    "data": {
        "items": [],
        "paging": {
            "pageIndex": 1,
            "pageSize": 20,
            "totalItems": 100,
            "totalPages": 5
        }
    }
}
```

## TypeScript Types for API Responses

Location: `frontend/uni-manage/types/api.types.ts`

```tsx
export interface ApiResponse<T> {
    returnCode: number;
    message: string;
    data: T;
    errors: string[];
}

export interface PagingInfo {
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}

export interface PagedResult<T> {
    items: T[];
    paging: PagingInfo;
}

export type PagedResponse<T> = ApiResponse<PagedResult<T>>;
```

## Service Layer Pattern

Each resource should have a service class with CRUD methods.

Location: `frontend/uni-manage/services/{ResourceName}Service.ts`

```tsx
import { httpClient } from "@/lib/http-client";
import type { ApiResponse, PagedResponse } from "@/types/api.types";

// Request/Response types
export interface CreateUserRequest {
    username: string;
    displayName: string;
    email: string;
    password: string;
}

export interface UpdateUserRequest {
    displayName: string;
    email: string;
}

export interface UserListItem {
    id: number;
    username: string;
    displayName: string;
    email: string;
    status: string;
    createdAt: string;
}

export interface UserDetail extends UserListItem {
    phoneNumber?: string;
    departmentCode?: string;
    positionCode?: string;
}

export interface UserListParams {
    keyword?: string;
    status?: string;
    pageIndex?: number;
    pageSize?: number;
    sortBy?: string;
    sortDirection?: "ASC" | "DESC";
}

export class UserService {
    private static readonly BASE_PATH = "/users";

    /**
     * Get paginated list of users
     */
    static async getList(params?: UserListParams): Promise<PagedResponse<UserListItem>> {
        const response = await httpClient.get<PagedResponse<UserListItem>>(this.BASE_PATH, {
            params,
        });
        return response.data;
    }

    /**
     * Get single user by ID
     */
    static async getById(id: number): Promise<ApiResponse<UserDetail>> {
        const response = await httpClient.get<ApiResponse<UserDetail>>(`${this.BASE_PATH}/${id}`);
        return response.data;
    }

    /**
     * Create new user
     */
    static async create(data: CreateUserRequest): Promise<ApiResponse<{ id: number }>> {
        const response =
            (await httpClient.post) < ApiResponse < { id: number } >>> (this.BASE_PATH, data);
        return response.data;
    }

    /**
     * Update existing user
     */
    static async update(id: number, data: UpdateUserRequest): Promise<ApiResponse<boolean>> {
        const response = await httpClient.put<ApiResponse<boolean>>(
            `${this.BASE_PATH}/${id}`,
            data
        );
        return response.data;
    }

    /**
     * Delete user
     */
    static async delete(id: number): Promise<ApiResponse<boolean>> {
        const response = await httpClient.delete<ApiResponse<boolean>>(`${this.BASE_PATH}/${id}`);
        return response.data;
    }

    /**
     * Get users for combobox/select
     */
    static async getCombobox(): Promise<ApiResponse<Array<{ value: string; label: string }>>> {
        const response = await httpClient.get<ApiResponse<Array<{ value: string; label: string }>>>(
            `${this.BASE_PATH}/combobox`
        );
        return response.data;
    }
}
```

## TanStack Query Integration

### Query Keys Convention

Location: `frontend/uni-manage/lib/query-keys.ts`

```tsx
export const queryKeys = {
    users: {
        all: ["users"] as const,
        lists: () => [...queryKeys.users.all, "list"] as const,
        list: (filters: string) => [...queryKeys.users.lists(), filters] as const,
        details: () => [...queryKeys.users.all, "detail"] as const,
        detail: (id: number) => [...queryKeys.users.details(), id] as const,
        combobox: () => [...queryKeys.users.all, "combobox"] as const,
    },
    departments: {
        all: ["departments"] as const,
        lists: () => [...queryKeys.departments.all, "list"] as const,
        list: (filters: string) => [...queryKeys.departments.lists(), filters] as const,
        details: () => [...queryKeys.departments.all, "detail"] as const,
        detail: (id: number) => [...queryKeys.departments.details(), id] as const,
    },
    // Add more resources...
};
```

### Using TanStack Query with Services

```tsx
"use client";

import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { UserService } from "@/services/UserService";
import { queryKeys } from "@/lib/query-keys";
import { toast } from "sonner"; // or your toast library

export function useUserList(params?: UserListParams) {
    return useQuery({
        queryKey: queryKeys.users.list(JSON.stringify(params)),
        queryFn: () => UserService.getList(params),
        staleTime: 5 * 60 * 1000, // 5 minutes
    });
}

export function useUserDetail(id: number) {
    return useQuery({
        queryKey: queryKeys.users.detail(id),
        queryFn: () => UserService.getById(id),
        enabled: !!id, // Only fetch if id is provided
    });
}

export function useCreateUser() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: UserService.create,
        onSuccess: (response) => {
            // Invalidate and refetch user lists
            queryClient.invalidateQueries({ queryKey: queryKeys.users.lists() });

            toast.success(response.message || "User created successfully");
        },
        onError: (error: any) => {
            const message = error.response?.data?.message || "Failed to create user";
            toast.error(message);
        },
    });
}

export function useUpdateUser() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, data }: { id: number; data: UpdateUserRequest }) =>
            UserService.update(id, data),
        onSuccess: (response, { id }) => {
            // Invalidate specific user detail and all lists
            queryClient.invalidateQueries({ queryKey: queryKeys.users.detail(id) });
            queryClient.invalidateQueries({ queryKey: queryKeys.users.lists() });

            toast.success(response.message || "User updated successfully");
        },
        onError: (error: any) => {
            const message = error.response?.data?.message || "Failed to update user";
            toast.error(message);
        },
    });
}

export function useDeleteUser() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: UserService.delete,
        onSuccess: (response) => {
            queryClient.invalidateQueries({ queryKey: queryKeys.users.lists() });

            toast.success(response.message || "User deleted successfully");
        },
        onError: (error: any) => {
            const message = error.response?.data?.message || "Failed to delete user";
            toast.error(message);
        },
    });
}
```

## Error Handling

### Frontend Error Display

```tsx
import { ApiResponse } from "@/types/api.types";

export function getErrorMessage(error: any): string {
    // API error response
    if (error.response?.data?.message) {
        return error.response.data.message;
    }

    // API validation errors
    if (error.response?.data?.errors?.length > 0) {
        return error.response.data.errors.join(", ");
    }

    // Network error
    if (error.message === "Network Error") {
        return "Cannot connect to server. Please check your connection.";
    }

    // Timeout error
    if (error.code === "ECONNABORTED") {
        return "Request timeout. Please try again.";
    }

    return "An unexpected error occurred";
}
```

### Backend Error Response

Backend should return errors in this format:

```csharp
// Validation errors
return ResponseHelper.Error<T>(
    "Validation failed",
    new List<string> { "Email is required", "Username must be unique" }
);

// Single error
return ResponseHelper.Error<T>("User not found");

// Exception
return ResponseHelper.Error<T>("An error occurred while processing your request");
```

## API Endpoints Convention

Frontend should match backend RESTful patterns:

```tsx
// ✅ CORRECT - Matches backend
GET    /api/v1/users          → UserService.getList()
GET    /api/v1/users/{id}     → UserService.getById(id)
POST   /api/v1/users          → UserService.create(data)
PUT    /api/v1/users/{id}     → UserService.update(id, data)
DELETE /api/v1/users/{id}     → UserService.delete(id)
GET    /api/v1/users/combobox → UserService.getCombobox()

// ❌ WRONG
GET    /api/v1/users/getList
POST   /api/v1/users/create
```

## Loading & Error States

### Standard Pattern

```tsx
export function UserList() {
    const { data, isLoading, error } = useUserList();

    if (isLoading) {
        return (
            <div className="flex justify-center items-center h-64">
                <Spinner size="lg" color="primary" />
            </div>
        );
    }

    if (error) {
        return (
            <div className="p-4 text-center">
                <p className="text-danger">{getErrorMessage(error)}</p>
                <Button onPress={() => window.location.reload()}>Retry</Button>
            </div>
        );
    }

    if (!data?.data?.items?.length) {
        return <div className="p-4 text-center text-gray-500">No users found</div>;
    }

    return (
        <div>
            {data.data.items.map((user) => (
                <UserCard key={user.id} user={user} />
            ))}
        </div>
    );
}
```

## Optimistic Updates

For better UX, use optimistic updates:

```tsx
export function useUpdateUser() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, data }: { id: number; data: UpdateUserRequest }) =>
            UserService.update(id, data),

        // Optimistic update
        onMutate: async ({ id, data }) => {
            // Cancel outgoing refetches
            await queryClient.cancelQueries({ queryKey: queryKeys.users.detail(id) });

            // Snapshot previous value
            const previousUser = queryClient.getQueryData(queryKeys.users.detail(id));

            // Optimistically update
            queryClient.setQueryData(queryKeys.users.detail(id), (old: any) => ({
                ...old,
                data: { ...old.data, ...data },
            }));

            return { previousUser };
        },

        // Rollback on error
        onError: (err, variables, context) => {
            if (context?.previousUser) {
                queryClient.setQueryData(
                    queryKeys.users.detail(variables.id),
                    context.previousUser
                );
            }
        },

        // Refetch after success or error
        onSettled: (data, error, { id }) => {
            queryClient.invalidateQueries({ queryKey: queryKeys.users.detail(id) });
        },
    });
}
```

## Pagination Integration

### Backend Pagination Parameters

Backend expects:

-   `pageIndex`: number (1-based)
-   `pageSize`: number (default 20)
-   `keyword`: string (optional)
-   `sortBy`: string (optional)
-   `sortDirection`: "ASC" | "DESC" (optional)

### Frontend Pagination Component

```tsx
import { Pagination } from "@heroui/react";

export function UserTable() {
    const [page, setPage] = useState(1);
    const [pageSize] = useState(20);

    const { data, isLoading } = useUserList({
        pageIndex: page,
        pageSize,
    });

    return (
        <>
            <Table>{/* Table content */}</Table>

            <div className="flex justify-center mt-4">
                <Pagination
                    total={data?.data?.paging?.totalPages || 1}
                    page={page}
                    onChange={setPage}
                    showControls
                />
            </div>
        </>
    );
}
```

## Standards Checklist

✅ Use httpClient from `lib/http-client.ts`
✅ Create service class per resource in `services/`
✅ Define TypeScript types for requests/responses
✅ Use TanStack Query for data fetching
✅ Define query keys in `lib/query-keys.ts`
✅ Handle loading/error states consistently
✅ Show toast notifications for mutations
✅ Invalidate queries after mutations
✅ Match backend RESTful URL patterns
✅ Use proper HTTP methods (GET, POST, PUT, DELETE)
✅ Handle API errors gracefully
✅ Use optimistic updates when appropriate
✅ Implement pagination correctly (1-based pageIndex)
