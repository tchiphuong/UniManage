import { apiClient } from "@/lib/api-client";
import { POSITION_ENDPOINTS } from "@/lib/api-endpoints";
import { ApiResponse, PagedResponse, PagedResult, PagingParams } from "@/types";

export interface Position {
    id: number;
    positionCode: string;
    positionName: string;
    description?: string;
    level?: number;
    status: number;
    createdAt: string;
}

export interface PositionListParams extends PagingParams {
    keyword?: string;
    status?: number;
}

export const PositionService = {
    list: async (params: PositionListParams): Promise<PagedResponse<Position>> => {
        const response = await apiClient.get<PagedResult<Position>>(POSITION_ENDPOINTS.LIST, {
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

    get: async (id: number): Promise<ApiResponse<Position>> => {
        const response = await apiClient.get<Position>(POSITION_ENDPOINTS.DETAIL(id));
        return response as ApiResponse<Position>;
    },

    create: async (data: Partial<Position>): Promise<ApiResponse<number>> => {
        const response = await apiClient.post<number>(POSITION_ENDPOINTS.CREATE, data);
        return response as ApiResponse<number>;
    },

    update: async (id: number, data: Partial<Position>): Promise<ApiResponse<void>> => {
        const response = await apiClient.put<void>(POSITION_ENDPOINTS.UPDATE(id), data);
        return response as ApiResponse<void>;
    },

    delete: async (ids: number[]): Promise<ApiResponse<void>> => {
        const response = await apiClient.delete<void>(POSITION_ENDPOINTS.DELETE, { data: ids });
        return response as ApiResponse<void>;
    },
};
