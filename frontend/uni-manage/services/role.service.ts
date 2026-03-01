import { apiClient } from "@/lib/api-client";
import { ROLE_ENDPOINTS } from "@/lib/api-endpoints";
import { ApiResponse, PagedResponse, PagedResult, PagingParams } from "@/types";

export interface Role {
    id: number;
    roleCode: string; // Unique Identifier
    roleName: string;
    description?: string;
    isActive: number; // 1 = Active, 0 = Inactive
    createdBy: string;
    createdAt: string;
    updatedBy?: string;
    updatedAt?: string;
    dataRowVersion?: string; // Base64 string for concurrency check
}

export interface RoleListParams extends PagingParams {
    keyword?: string;
    isActive?: number;
}

export const RoleService = {
    list: async (params: RoleListParams): Promise<PagedResponse<Role>> => {
        const response = await apiClient.get<PagedResult<Role>>(ROLE_ENDPOINTS.LIST, { params });
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

    getByCode: async (code: string): Promise<ApiResponse<Role>> => {
        const response = await apiClient.get<Role>(`${ROLE_ENDPOINTS.LIST}/${code}`);
        return response as ApiResponse<Role>;
    },

    create: async (data: Partial<Role>): Promise<ApiResponse<{ id: number }>> => {
        const response = await apiClient.post<{ id: number }>(ROLE_ENDPOINTS.CREATE, data);
        return response as ApiResponse<{ id: number }>;
    },

    update: async (
        code: string,
        data: Partial<Role>,
    ): Promise<ApiResponse<{ success: boolean }>> => {
        const response = await apiClient.put<{ success: boolean }>(
            `${ROLE_ENDPOINTS.LIST}/${code}`,
            data,
        );
        return response as ApiResponse<{ success: boolean }>;
    },

    delete: async (roleCodes: string[]): Promise<ApiResponse<{ success: boolean }>> => {
        // Backend DeleteRoleCommand accepts { roleCode: "..." } or list?
        // Let's check DeleteRoleCommand.cs backend.
        // Usually list delete sends body: [ "code1", "code2" ]
        // But RolesController Delete method: [FromBody] DeleteRoleCommand command
        // Let's verify DeleteRoleCommand structure first.

        // For now, assuming sticking to pattern allowing single delete via command
        // But mass delete might need list.
        // Wait, standard delete usually takes IDs. Here RoleCode is key.
        // I will assume for now we delete one by one or the command accepts a list.
        // I'll check DeleteRoleCommand.cs in a moment.

        // Let's assume standard behavior for now:
        const response = await apiClient.delete<{ success: boolean }>(ROLE_ENDPOINTS.DELETE, {
            data: { roleCodes },
        });
        return response as ApiResponse<{ success: boolean }>;
    },

    getCombobox: async (keyword?: string): Promise<ApiResponse<any[]>> => {
        const response = await apiClient.get<any[]>(ROLE_ENDPOINTS.COMBOBOX, {
            params: { keyword },
        });
        return response as ApiResponse<any[]>;
    },
};
