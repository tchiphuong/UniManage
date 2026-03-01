import { useState } from "react";
import { useRouter } from "@/i18n/navigation";
import {
    apiClient,
    setAccessToken,
    setRefreshToken,
    clearAuthCookies,
    isAuthenticated as checkAuth,
    getAccessToken,
} from "@/lib";
import { API_ENDPOINTS } from "@/lib/api-endpoints";
import type { LoginRequest, LoginResponse, UserProfile, FieldErrorModel } from "@/types";

/**
 * Hook để xử lý authentication
 */
export function useAuth() {
    const router = useRouter();
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    /**
     * Login
     */
    const login = async (credentials: LoginRequest) => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await apiClient.post<LoginResponse>(
                API_ENDPOINTS.AUTH.LOGIN,
                credentials,
            );

            if (response.returnCode === 0 && response.data) {
                // Save tokens to cookies
                setAccessToken(response.data.accessToken, credentials.rememberMe);
                if (response.data.refreshToken) {
                    setRefreshToken(response.data.refreshToken, credentials.rememberMe);
                }

                return { success: true, data: response.data };
            } else {
                const firstError = response.errors[0];
                let errorMessage = "Login failed";

                if (typeof firstError === "string") {
                    errorMessage = firstError;
                } else if (
                    firstError &&
                    typeof firstError === "object" &&
                    "messages" in firstError
                ) {
                    const model = firstError as FieldErrorModel;
                    errorMessage = model.messages[0] || "Login failed";
                }

                setError(errorMessage);
                return { success: false, error: errorMessage };
            }
        } catch (err: any) {
            const errorMsg = err.response?.data?.message || "An error occurred";
            setError(errorMsg);
            return { success: false, error: errorMsg };
        } finally {
            setIsLoading(false);
        }
    };

    /**
     * Logout
     */
    const logout = () => {
        clearAuthCookies();
        router.push("/login");
    };

    /**
     * Get current user profile
     */
    const getCurrentUser = async () => {
        try {
            const response = await apiClient.get<UserProfile>(API_ENDPOINTS.AUTH.ME);
            if (response.returnCode === 0 && response.data) {
                return response.data;
            }
            return null;
        } catch {
            return null;
        }
    };

    /**
     * Check if user is authenticated
     */
    const isAuthenticated = () => {
        return !!getAccessToken();
    };
    return {
        login,
        logout,
        getCurrentUser,
        isAuthenticated: isAuthenticated(),
        getToken: getAccessToken,
        isLoading,
        error,
    };
}

/**
 * Hook để kiểm tra xem user có đang login hay không
 */
export function useAuthCheck() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const router = useRouter();

    const checkAuth = async () => {
        const token = getAccessToken();

        if (!token) {
            setIsAuthenticated(false);
            setIsLoading(false);
            return false;
        }

        try {
            const response = await apiClient.get<UserProfile>(API_ENDPOINTS.AUTH.ME);
            if (response.returnCode === 0) {
                setIsAuthenticated(true);
                setIsLoading(false);
                return true;
            } else {
                clearAuthCookies();
                setIsAuthenticated(false);
                setIsLoading(false);
                return false;
            }
        } catch {
            clearAuthCookies();
            setIsAuthenticated(false);
            setIsLoading(false);
            return false;
        }
    };

    return { isAuthenticated, isLoading, checkAuth };
}
