// API Response types
export interface ApiResponse<T = any> {
    returnCode: number;
    errors: string[];
    message: string;
    data: T;
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

// Auth types
export interface LoginRequest {
    username: string;
    password: string;
}

export interface LoginResponse {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}

export interface User {
    id: number;
    userCode: string;
    displayName: string;
    email: string;
    status: number;
}
