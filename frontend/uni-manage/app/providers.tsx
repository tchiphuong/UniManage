"use client";

import { Toast } from "@heroui/react";
import { appToastQueue } from "@/lib/toast-queues";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider as NextThemesProvider } from "next-themes";
import { useState } from "react";

import { NavbarProvider } from "@/contexts/navbar-context";
import { AuthProvider } from "@/contexts/auth-context";

/**
 * Root Providers cho UniManage
 * Include ThemeProvider and NavbarProvider
 */
export function Providers({
    children,
}: Readonly<{ children: React.ReactNode }>) {
    const [queryClient] = useState(
        () =>
            new QueryClient({
                defaultOptions: {
                    queries: {
                        // With SSR, we usually want to set some default staleTime
                        // above 0 to avoid refetching immediately on the client
                        staleTime: 60 * 1000,
                    },
                },
            }),
    );

    return (
        <QueryClientProvider client={queryClient}>
            <NextThemesProvider
                attribute="class"
                defaultTheme="system"
                enableSystem
                disableTransitionOnChange
            >
                <AuthProvider>
                    <NavbarProvider>
                        {children}
                        {/* Global Toast Queue */}
                        <Toast.Provider
                            placement="top end"
                            queue={appToastQueue}
                        />
                    </NavbarProvider>
                </AuthProvider>
            </NextThemesProvider>
        </QueryClientProvider>
    );
}
