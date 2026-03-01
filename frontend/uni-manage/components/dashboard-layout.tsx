/* eslint-disable */
"use client";

import { useState, useEffect } from "react";
import { Header } from "./layout/header";
import { Sidebar } from "./layout/sidebar";

interface DashboardLayoutProps {
    children: React.ReactNode;
}

export function DashboardLayout({ children }: DashboardLayoutProps) {
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
    }, []);

    if (!mounted) {
        return null;
    }

    return (
        <div className="flex h-screen flex-col overflow-hidden bg-gray-100 dark:bg-zinc-900 transition-colors duration-300">
            <Header />
            <div className="flex flex-1 overflow-hidden">
                <Sidebar />
                <main className="flex-1 overflow-y-auto overflow-x-hidden bg-gray-100 dark:bg-gray-900">
                    <div className="container mx-auto gap-3 p-6">{children}</div>
                </main>
            </div>
        </div>
    );
}
