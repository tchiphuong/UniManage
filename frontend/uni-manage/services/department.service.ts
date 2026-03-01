import { apiClient } from "@/lib/api-client";
import { DEPARTMENT_ENDPOINTS } from "@/lib/api-endpoints";
import { ApiResponse, PagedResponse, PagedResult, PagingParams } from "@/types";

export interface Department {
    id: number;
    departmentCode: string;
    departmentName: string;
    description?: string;
    managerName?: string;
    status: number;
    createdAt: string;
}

export interface DepartmentListParams extends PagingParams {
    keyword?: string;
    status?: number;
}

export const DepartmentService = {
    list: async (params: DepartmentListParams): Promise<PagedResponse<Department>> => {
        const response = await apiClient.get<PagedResult<Department>>(DEPARTMENT_ENDPOINTS.LIST, {
            params,
        });
        return (
            response || {
                returnCode: -1,
                message: "Error",
                errors: [],
                data: {
                    items: [],
                    paging: { pageIndex: 1, pageSize: 20, totalItems: 0, totalPages: 0 },
                },
            }
        );
    },

    get: async (id: number): Promise<ApiResponse<Department>> => {
        const response = await apiClient.get<Department>(DEPARTMENT_ENDPOINTS.DETAIL(id));
        return response as ApiResponse<Department>;
    },

    create: async (data: Partial<Department>): Promise<ApiResponse<number>> => {
        const response = await apiClient.post<number>(DEPARTMENT_ENDPOINTS.CREATE, data);
        return response as ApiResponse<number>;
    },

    update: async (id: number, data: Partial<Department>): Promise<ApiResponse<void>> => {
        const response = await apiClient.put<void>(DEPARTMENT_ENDPOINTS.UPDATE(id), data);
        return response as ApiResponse<void>;
    },

    delete: async (ids: number[]): Promise<ApiResponse<void>> => {
        const response = await apiClient.delete<void>(DEPARTMENT_ENDPOINTS.DELETE, { data: ids });
        return response as ApiResponse<void>;
    },
};
