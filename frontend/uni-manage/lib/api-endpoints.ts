/**
 * API Endpoints Constants
 * Centralized management of all API endpoints following RESTful conventions
 */

const API_VERSION = '/v1';

/**
 * Authentication & Authorization Endpoints
 */
export const AUTH_ENDPOINTS = {
    LOGIN: `${API_VERSION}/auth/login`,
    LOGOUT: `${API_VERSION}/auth/logout`,
    REFRESH_TOKEN: `${API_VERSION}/auth/refresh`,
    ME: `${API_VERSION}/auth/me`,
    CHANGE_PASSWORD: `${API_VERSION}/auth/change-password`,
    FORGOT_PASSWORD: `${API_VERSION}/auth/forgot-password`,
    RESET_PASSWORD: `${API_VERSION}/auth/reset-password`,
} as const;

/**
 * User Management Endpoints
 */
export const USER_ENDPOINTS = {
    LIST: `${API_VERSION}/users`,
    DETAIL: (id: number | string) => `${API_VERSION}/users/${id}`,
    CREATE: `${API_VERSION}/users`,
    UPDATE: (id: number | string) => `${API_VERSION}/users/${id}`,
    DELETE: `${API_VERSION}/users`,
    COMBOBOX: `${API_VERSION}/users/combobox`,
    EXPORT: `${API_VERSION}/users/export`,
    IMPORT: `${API_VERSION}/users/import`,
} as const;

/**
 * Role Management Endpoints
 */
export const ROLE_ENDPOINTS = {
    LIST: `${API_VERSION}/roles`,
    DETAIL: (id: number | string) => `${API_VERSION}/roles/${id}`,
    CREATE: `${API_VERSION}/roles`,
    UPDATE: (id: number | string) => `${API_VERSION}/roles/${id}`,
    DELETE: `${API_VERSION}/roles`,
    COMBOBOX: `${API_VERSION}/roles/combobox`,
} as const;

/**
 * Menu Management Endpoints
 */
export const MENU_ENDPOINTS = {
    LIST: `${API_VERSION}/menus`,
    DETAIL: (id: number | string) => `${API_VERSION}/menus/${id}`,
    CREATE: `${API_VERSION}/menus`,
    UPDATE: (id: number | string) => `${API_VERSION}/menus/${id}`,
    DELETE: `${API_VERSION}/menus`,
} as const;

/**
 * Department Management Endpoints
 */
export const DEPARTMENT_ENDPOINTS = {
    LIST: `${API_VERSION}/departments`,
    DETAIL: (id: number | string) => `${API_VERSION}/departments/${id}`,
    CREATE: `${API_VERSION}/departments`,
    UPDATE: (id: number | string) => `${API_VERSION}/departments/${id}`,
    DELETE: `${API_VERSION}/departments`,
    COMBOBOX: `${API_VERSION}/departments/combobox`,
} as const;

/**
 * System Endpoints
 */
export const SYSTEM_ENDPOINTS = {
    HEALTH: `${API_VERSION}/health`,
    VERSION: `${API_VERSION}/version`,
} as const;

/**
 * All API Endpoints
 */
export const API_ENDPOINTS = {
    AUTH: AUTH_ENDPOINTS,
    USER: USER_ENDPOINTS,
    ROLE: ROLE_ENDPOINTS,
    MENU: MENU_ENDPOINTS,
    DEPARTMENT: DEPARTMENT_ENDPOINTS,
    SYSTEM: SYSTEM_ENDPOINTS,
} as const;
