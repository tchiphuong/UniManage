/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosError, AxiosInstance, AxiosRequestConfig } from "axios";
import type { ApiResponse } from "@/types";
import { getAccessToken } from "./cookies";

/**
 * API Client cho UniManage
 * Tích hợp với backend ASP.NET Core
 */
class ApiClient {
    private instance: AxiosInstance;

    constructor() {
        const envUrl = process.env.NEXT_PUBLIC_API_BASE_URL;
        // Auto-fix for local development port mismatch (IIS Express vs Kestrel)
        const baseURL = envUrl?.includes("44325")
            ? "https://localhost:44325/api"
            : envUrl || "https://localhost:44325/api";

        this.instance = axios.create({
            baseURL,
            timeout: 30000,
            headers: {
                "Content-Type": "application/json",
                Accept: "text/plain",
            },
        });

        this.setupInterceptors();
    }

    private setupInterceptors() {
        // Request interceptor
        this.instance.interceptors.request.use(
            (config) => {
                // Thêm token nếu có
                const token = getAccessToken();
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }

                // Thêm correlation ID
                config.headers["X-Correlation-Id"] = this.generateCorrelationId();

                // Thêm Accept-Language từ locale hiện tại
                // Backend CultureMiddleware cần header này
                if (typeof window !== "undefined") {
                    const locale = window.location.pathname.split("/")[1] || "vi";
                    const langMap: Record<string, string> = {
                        vi: "vi-VN",
                        en: "en-US",
                    };
                    config.headers["Accept-Language"] = langMap[locale] || "vi-VN";
                }

                return config;
            },
            (error) => Promise.reject(error)
        );

        // Response interceptor
        this.instance.interceptors.response.use(
            (response) => response,
            async (error: AxiosError<ApiResponse>) => {
                if (error.response?.status === 401) {
                    // Token hết hạn, redirect to login
                    if (typeof window !== "undefined") {
                        // Clear cookies sẽ được handle ở logout
                        window.location.href = "/login";
                    }
                }
                return Promise.reject(error);
            }
        );
    }

    private generateCorrelationId(): string {
        return `${Date.now()}-${Math.random().toString(36).substr(2, 9)}`;
    }

    async get<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        const response = await this.instance.get<ApiResponse<T>>(url, config);
        return response.data;
    }

    async post<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        const response = await this.instance.post<ApiResponse<T>>(url, data, config);
        return response.data;
    }

    async put<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        const response = await this.instance.put<ApiResponse<T>>(url, data, config);
        return response.data;
    }

    async delete<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        const response = await this.instance.delete<ApiResponse<T>>(url, config);
        return response.data;
    }

    async patch<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        const response = await this.instance.patch<ApiResponse<T>>(url, data, config);
        return response.data;
    }
}

export const apiClient = new ApiClient();
