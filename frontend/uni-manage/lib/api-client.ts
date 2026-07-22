/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosError, AxiosInstance, AxiosRequestConfig } from "axios";

import type { ApiResponse } from "@/types";

import { getAccessToken, removeAccessToken } from "./cookies";

/**
 * API Client cho UniManage
 * Tích hợp với backend ASP.NET Core
 */
class ApiClient {
    private readonly instance: AxiosInstance;

    constructor() {
        const envUrl = process.env.NEXT_PUBLIC_API_BASE_URL;
        // Auto-fix for local development port mismatch (IIS Express vs Kestrel)
        const baseURL = envUrl;

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
                // Add token if exists
                const token = getAccessToken();
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }

                // Add correlation ID
                config.headers["X-Correlation-Id"] =
                    this.generateCorrelationId();

                // Add Accept-Language from current locale
                // Backend CultureMiddleware requires this header
                if (typeof window !== "undefined") {
                    const locale =
                        window.location.pathname.split("/")[1] || "vi";
                    const langMap: Record<string, string> = {
                        vi: "vi-VN",
                        en: "en-US",
                    };
                    config.headers["Accept-Language"] =
                        langMap[locale] || "vi-VN";
                }

                return config;
            },
            (error) => Promise.reject(error),
        );

        // Response interceptor
        this.instance.interceptors.response.use(
            (response) => response,
            async (error: AxiosError<ApiResponse>) => {
                if (error.response?.status === 401) {
                    // Token expired, redirect to login
                    if (typeof window !== "undefined") {
                        removeAccessToken(); // Clear cookie to prevent middleware loop
                        window.location.href = "/auth/login";
                    }
                }
                throw error;
            },
        );
    }

    private generateCorrelationId(): string {
        if (
            typeof crypto !== "undefined" &&
            typeof crypto.randomUUID === "function"
        ) {
            return crypto.randomUUID();
        }
        // Fallback for older environments
        return `${Date.now()}-fallback-id`;
    }

    async get<T>(
        url: string,
        config?: AxiosRequestConfig,
    ): Promise<ApiResponse<T>> {
        const response = await this.instance.get<ApiResponse<T>>(url, config);
        return response.data;
    }

    async post<T>(
        url: string,
        data?: any,
        config?: AxiosRequestConfig,
    ): Promise<ApiResponse<T>> {
        const response = await this.instance.post<ApiResponse<T>>(
            url,
            data,
            config,
        );
        return response.data;
    }

    async put<T>(
        url: string,
        data?: any,
        config?: AxiosRequestConfig,
    ): Promise<ApiResponse<T>> {
        const response = await this.instance.put<ApiResponse<T>>(
            url,
            data,
            config,
        );
        return response.data;
    }

    async delete<T>(
        url: string,
        config?: AxiosRequestConfig,
    ): Promise<ApiResponse<T>> {
        const response = await this.instance.delete<ApiResponse<T>>(
            url,
            config,
        );
        return response.data;
    }

    async patch<T>(
        url: string,
        data?: any,
        config?: AxiosRequestConfig,
    ): Promise<ApiResponse<T>> {
        const response = await this.instance.patch<ApiResponse<T>>(
            url,
            data,
            config,
        );
        return response.data;
    }
}

export const apiClient = new ApiClient();
