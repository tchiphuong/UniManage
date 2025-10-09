import { apiClient } from "./apiClient";
import type { LoginRequest, LoginResponse, User } from "@/types/api";

export const authService = {
    async login(request: LoginRequest) {
        return apiClient.post<LoginResponse>("/auth/login", request);
    },

    async logout() {
        return apiClient.post("/auth/logout");
    },

    async getCurrentUser() {
        return apiClient.get<User>("/auth/me");
    },

    async refreshToken(refreshToken: string) {
        return apiClient.post<LoginResponse>("/auth/refresh", { refreshToken });
    },
};
