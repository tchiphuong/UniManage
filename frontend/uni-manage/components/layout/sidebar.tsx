"use client";

import { useState } from "react";
import { Link } from "@/i18n/navigation";
import { usePathname } from "next/navigation";

interface SidebarProps {
    sidebarOpen: boolean;
}

export function Sidebar({ sidebarOpen }: SidebarProps) {
    const pathname = usePathname();
    const [dashboardOpen, setDashboardOpen] = useState(false);
    const [reportsOpen, setReportsOpen] = useState(false);

    const isActive = (path: string) => pathname === path;

    return (
        <aside
            className={`fixed left-0 top-18 z-40 h-[calc(100vh-4.5rem)] w-64 transform bg-white dark:bg-gray-800 shadow-md transition-transform duration-300 ease-in-out lg:static lg:translate-x-0 ${sidebarOpen ? "translate-x-0" : "-translate-x-full lg:w-0 lg:overflow-hidden"
                }`}
        >
            <nav className="h-full overflow-y-auto p-4">
                {/* Dashboard Group */}
                <div className="mb-2">
                    <button
                        onClick={() => setDashboardOpen(!dashboardOpen)}
                        className={`flex w-full items-center justify-between rounded-lg px-4 py-2 text-gray-700 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-700 ${dashboardOpen ? "bg-gray-100 dark:bg-gray-700" : ""
                            }`}
                    >
                        <span className="flex items-center">
                            <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
                            </svg>
                            Dashboard
                        </span>
                        <svg
                            className={`h-4 w-4 transition-transform duration-200 ${dashboardOpen ? "rotate-180" : ""}`}
                            fill="none"
                            stroke="currentColor"
                            viewBox="0 0 24 24"
                        >
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
                        </svg>
                    </button>
                    {dashboardOpen && (
                        <div className="ml-4 mt-1 space-y-1 pl-4 border-l-2 border-gray-200 dark:border-gray-600">
                            <Link
                                href="/"
                                className={`block rounded-lg px-4 py-2 text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-white hover:bg-gray-50 dark:hover:bg-gray-700 ${isActive("/") ? "bg-gray-50 dark:bg-gray-700 font-medium text-blue-600 dark:text-blue-400" : ""
                                    }`}
                            >
                                Overview
                            </Link>
                            <Link
                                href="#"
                                className="block rounded-lg px-4 py-2 text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-white hover:bg-gray-50 dark:hover:bg-gray-700"
                            >
                                Analytics
                            </Link>
                        </div>
                    )}
                </div>

                {/* Reports Group */}
                <div className="mb-2">
                    <button
                        onClick={() => setReportsOpen(!reportsOpen)}
                        className={`flex w-full items-center justify-between rounded-lg px-4 py-2 text-gray-700 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-700 ${reportsOpen ? "bg-gray-100 dark:bg-gray-700" : ""
                            }`}
                    >
                        <span className="flex items-center">
                            <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                            </svg>
                            Reports
                        </span>
                        <svg
                            className={`h-4 w-4 transition-transform duration-200 ${reportsOpen ? "rotate-180" : ""}`}
                            fill="none"
                            stroke="currentColor"
                            viewBox="0 0 24 24"
                        >
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
                        </svg>
                    </button>
                    {reportsOpen && (
                        <div className="ml-4 mt-1 space-y-1 pl-4 border-l-2 border-gray-200 dark:border-gray-600">
                            {["Daily", "Weekly", "Monthly", "Quarterly", "Yearly"].map((item) => (
                                <Link
                                    key={item}
                                    href="#"
                                    className="block rounded-lg px-4 py-2 text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-white hover:bg-gray-50 dark:hover:bg-gray-700"
                                >
                                    {item}
                                </Link>
                            ))}
                        </div>
                    )}
                </div>

                {/* Single Links */}
                {[
                    { name: "Users", icon: "M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z", href: "/system/users" },
                    { name: "Products", icon: "M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10l-8 4" },
                    { name: "Orders", icon: "M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-3 7h3m-3 4h3m-6-4h.01M9 16h.01" },
                    { name: "Inventory", icon: "M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4" },
                    { name: "Customers", icon: "M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" },
                    { name: "Marketing", icon: "M11 5.882V19.24a1.76 1.76 0 01-3.417.592l-2.147-6.15M18 13a3 3 0 100-6M5.436 13.683A4.001 4.001 0 017 6h1.832c4.1 0 7.625-1.234 9.168-3v14c-1.543-1.766-5.067-3-9.168-3H7a3.988 3.988 0 01-1.564-.317z" },
                    { name: "Analytics", icon: "M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2M9 19l6-6-6-6" },
                    { name: "Integrations", icon: "M13 10V3L4 14h7v7l9-11h-7z" },
                    { name: "Settings", icon: "M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z", icon2: "M15 12a3 3 0 11-6 0 3 3 0 016 0z" },
                    { name: "Help & Support", icon: "M18.364 5.636l-3.536 3.536m0 5.656l3.536 3.536M9.172 9.172L5.636 5.636m3.536 9.192l-3.536 3.536M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-5 0a4 4 0 11-8 0 4 4 0 018 0z" },
                    { name: "Profile", icon: "M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" },
                ].map((item) => (
                    <Link
                        key={item.name}
                        href={item.href || "#"}
                        className="flex items-center rounded-lg px-4 py-2 text-gray-700 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-700"
                    >
                        <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d={item.icon} />
                            {item.icon2 && <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d={item.icon2} />}
                        </svg>
                        {item.name}
                    </Link>
                ))}
            </nav>
        </aside>
    );
}
