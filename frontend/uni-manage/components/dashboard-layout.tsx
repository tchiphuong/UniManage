/* eslint-disable */
"use client";

import { useState, useEffect } from "react";
import { Header } from "./layout/header";
import { Sidebar } from "./layout/sidebar";

interface DashboardLayoutProps {
    children: React.ReactNode;
}

export function DashboardLayout({ children }: DashboardLayoutProps) {
    // Initialize as true (default for server) or closed for mobile?
    // To match server, we should pick a static default.
    // Let's default to closed for mobile-first or true for desktop-first?
    // Ideally, default to true to match the typical desktop user if that's the primary target,
    // but verifying text content mismatch requires identical initial state.
    // Let's use `false` initially if we want to be safe, or `true`.
    // Better: use a simple default and update in effect.
    const [sidebarOpen, setSidebarOpen] = useState(true);
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
        // Sync with localStorage or window width only on client
        if (typeof window !== "undefined") {
            const savedState = localStorage.getItem("sidebarOpen");
            if (savedState !== null) {
                setSidebarOpen(savedState === "true");
            } else {
                setSidebarOpen(window.innerWidth >= 768);
            }
        }
    }, []);

    useEffect(() => {
        if (mounted) {
            localStorage.setItem("sidebarOpen", sidebarOpen.toString());
        }
    }, [sidebarOpen, mounted]);

    useEffect(() => {
        const handleResize = () => {
            if (window.innerWidth < 768) {
                setSidebarOpen(false);
            }
        };
        window.addEventListener("resize", handleResize);
        return () => window.removeEventListener("resize", handleResize);
    }, []);

    if (!mounted) {
        return null; // Or a loading spinner
    }

    return (
        <div className="flex h-screen flex-col overflow-hidden bg-gray-100 dark:bg-zinc-900 transition-colors duration-300">
            <Header sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
            <div className="flex flex-1 overflow-hidden">
                <Sidebar sidebarOpen={sidebarOpen} />
                <main className="flex-1 overflow-y-auto overflow-x-hidden bg-gray-100 dark:bg-gray-900">
                    <div className="container mx-auto gap-3 p-6">{children}</div>
                </main>
            </div>
        </div>
    );
}
