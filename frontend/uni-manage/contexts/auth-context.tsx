"use client";

import { createContext, useContext, useState, useEffect, ReactNode } from "react";
import { useRouter } from "next/navigation";
import { authService, type CurrentUserResponse } from "@/lib/services/auth.service";
import { getAccessToken, getRefreshToken, setAccessToken, setRefreshToken, clearAuthCookies } from "@/lib/cookies";

interface AuthContextType {
    user: CurrentUserResponse | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    login: (accessToken: string, refreshToken: string, user: CurrentUserResponse) => void;
    logout: () => Promise<void>;
    refreshUser: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<CurrentUserResponse | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const router = useRouter();

    // Initialize auth state from cookies
    useEffect(() => {
        const initAuth = async () => {
            const token = getAccessToken();
            if (token) {
                await refreshUser();
            } else {
                setIsLoading(false);
            }
        };

        initAuth();
    }, []);

    const login = (accessToken: string, refreshToken: string, userData: CurrentUserResponse) => {
        setAccessToken(accessToken);
        setRefreshToken(refreshToken);
        setUser(userData);
    };

    const logout = async () => {
        try {
            const refreshTokenValue = getRefreshToken();
            if (refreshTokenValue) {
                await authService.logout(refreshTokenValue);
            }
        } catch (error) {
            console.error("Logout error:", error);
        } finally {
            clearAuthCookies();
            setUser(null);
            router.push("/auth/login");
        }
    };

    const refreshUser = async () => {
        try {
            setIsLoading(true);
            const token = getAccessToken();
            if (!token) {
                setUser(null);
                return;
            }

            // Decode JWT to get username (simple base64 decode of payload)
            const payload = JSON.parse(atob(token.split(".")[1]));
            const username = payload.sub || payload.username;

            if (!username) {
                throw new Error("Username not found in token");
            }

            const response = await authService.getCurrentUser(username);
            if (response.returnCode === 0 && response.data) {
                setUser(response.data);
            } else {
                clearAuthCookies();
                setUser(null);
            }
        } catch (error) {
            console.error("Refresh user error:", error);
            clearAuthCookies();
            setUser(null);
        } finally {
            setIsLoading(false);
        }
    };

    const value: AuthContextType = {
        user,
        isAuthenticated: !!user,
        isLoading,
        login,
        logout,
        refreshUser,
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
}

