/**
 * API Response Models
 * Theo chuẩn UniManage backend
 */

export interface ApiResponse<T = any> {
    returnCode: number;
    message: string;
    data?: T;
    errors: (string | FieldErrorModel)[];
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

export interface PagedResponse<T> extends ApiResponse<PagedResult<T>> {}

export interface FieldErrorModel {
    field: string;
    messages: string[];
}

export interface PagingParams {
    pageIndex: number;
    pageSize: number;
    [key: string]: any;
}
