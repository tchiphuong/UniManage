"use client";

import { useEffect, useState } from "react";

import { Header } from "./components/header";
import { Navbar } from "./components/navbar";

interface AdminLayoutProps {
    readonly children: React.ReactNode;
}

export default function AdminLayout({ children }: AdminLayoutProps) {
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
    }, []);

    if (!mounted) {
        return null;
    }

    return (
        <div className="bg-background flex h-screen flex-col overflow-hidden">
            <Header />
            <div className="flex flex-1 overflow-hidden">
                <Navbar />
                <main className="flex-1 overflow-x-hidden overflow-y-auto">
                    <div className="container mx-auto gap-3 p-6">
                        {children}
                    </div>
                </main>
            </div>
        </div>
    );
}
