"use client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useState } from "react";
import { ThemeProvider } from "@/contexts/ThemeContext";
import { SidebarProvider } from "@/contexts/SidebarContext";

import { HeroUIProvider } from "@heroui/system";
import { useRouter } from "next/navigation";
import { ToastProvider } from "@heroui/toast";

/**
 * Root Providers cho UniManage
 * Include ThemeProvider and SidebarProvider
 */
export function Providers({ children }: { children: React.ReactNode }) {
    const router = useRouter();
    const [queryClient] = useState(() => new QueryClient({
        defaultOptions: {
            queries: {
                // With SSR, we usually want to set some default staleTime
                // above 0 to avoid refetching immediately on the client
                staleTime: 60 * 1000,
            },
        },
    }));

    return (
        <HeroUIProvider navigate={router.push}>
            <QueryClientProvider client={queryClient}>
                <ThemeProvider>
                    <SidebarProvider>
                        {children}
                        <ToastProvider placement="top-right" />
                    </SidebarProvider>
                </ThemeProvider>
            </QueryClientProvider>
        </HeroUIProvider>
    );
}
