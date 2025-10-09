export const API_ENDPOINTS = {
    AUTH: {
        LOGIN: "/auth/login",
        LOGOUT: "/auth/logout",
        REFRESH: "/auth/refresh",
        ME: "/auth/me",
    },
    USERS: {
        LIST: "/users",
        GET: (id: number) => `/users/${id}`,
        CREATE: "/users",
        UPDATE: (id: number) => `/users/${id}`,
        DELETE: (id: number) => `/users/${id}`,
    },
} as const;

export const ROUTES = {
    HOME: "/",
    LOGIN: "/login",
    DASHBOARD: "/dashboard",
    USERS: "/users",
    PROFILE: "/profile",
} as const;
