"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useState } from "react";
import { ThemeProvider as NextThemesProvider } from "next-themes";
import { SidebarProvider } from "@/contexts/sidebar-context";
import { ToastProvider } from "@heroui/toast";

/**
 * Root Providers cho UniManage
 * Include ThemeProvider and SidebarProvider
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
                <SidebarProvider>
                    {children}
                    <ToastProvider placement="top-right" />
                </SidebarProvider>
            </NextThemesProvider>
        </QueryClientProvider>
    );
}
