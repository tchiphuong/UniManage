import { apiClient } from "@/lib/api-client";
import { ApiResponse, PagedResponse, PagedResult, PagingParams } from "@/types";

export interface User {
    id: number;
    username: string;
    displayName: string;
    email: string;
    avatar?: string;
    status: number;
    createdAt: string;
}

export interface UserListParams extends PagingParams {
    keyword?: string;
    status?: number;
}

export const UserService = {
    list: async (params: UserListParams): Promise<PagedResponse<User>> => {
        const response = await apiClient.get<PagedResult<User>>("/users", { params });
        // Ensure strictly typed return or handle undefined if apiClient can be undefined (though interceptor throws)
        return response || {
            returnCode: -1,
            message: "Error",
            errors: [],
            data: { items: [], paging: { pageIndex: 1, pageSize: 20, totalItems: 0, totalPages: 0 } }
        };
    },

    get: async (id: number): Promise<ApiResponse<User>> => {
        const response = await apiClient.get<User>(`/users/${id}`);
        return response as ApiResponse<User>;
    },

    create: async (data: Partial<User>): Promise<ApiResponse<number>> => {
        const response = await apiClient.post<number>("/users", data);
        return response as ApiResponse<number>;
    },

    update: async (id: number, data: Partial<User>): Promise<ApiResponse<void>> => {
        const response = await apiClient.put<void>(`/users/${id}`, data);
        return response as ApiResponse<void>;
    },

    delete: async (id: number): Promise<ApiResponse<void>> => {
        const response = await apiClient.delete<void>(`/users/${id}`);
        return response as ApiResponse<void>;
    }
};
