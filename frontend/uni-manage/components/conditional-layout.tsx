"use client";

import { usePathname } from "next/navigation";
import { DashboardLayout } from "./dashboard-layout";

interface ConditionalLayoutProps {
    children: React.ReactNode;
}

export function ConditionalLayout({ children }: ConditionalLayoutProps) {
    const pathname = usePathname();

    // Skip dashboard layout for auth routes
    const isAuthRoute = pathname?.startsWith("/auth") || pathname === "/";

    if (isAuthRoute) {
        return <>{children}</>;
    }

    return <DashboardLayout>{children}</DashboardLayout>;
}

