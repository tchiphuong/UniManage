"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/dashboard-layout";
import {
    Breadcrumb,
    PageHeader,
    StatCard,
    ActivityItem,
    PerformanceMetric,
    type BreadcrumbItem,
} from "@/components/ui";

interface StatCard {
    title: string;
    value: number;
    trend: number;
    color: "blue" | "green" | "purple" | "orange" | "yellow" | "red";
    icon: string;
    prefix?: string;
    suffix?: string;
}

interface RecentOrder {
    id: string;
    customer: string;
    amount: number;
    status: "completed" | "pending" | "processing" | "cancelled";
    date: string;
}

interface Activity {
    id: number;
    type: string;
    description: string;
    time: string;
    color: "blue" | "green" | "purple" | "yellow" | "red" | "orange";
    icon: string;
}

interface PerformanceMetric {
    label: string;
    value: number;
    color: "blue" | "green" | "purple" | "yellow";
}

function getColorClasses(color: string, type: "bg" | "text"): string {
    const colorMap = {
        blue: { bg: "bg-blue-500", text: "text-blue-500" },
        green: { bg: "bg-green-500", text: "text-green-500" },
        purple: { bg: "bg-purple-500", text: "text-purple-500" },
        orange: { bg: "bg-orange-500", text: "text-orange-500" },
        yellow: { bg: "bg-yellow-500", text: "text-yellow-500" },
        red: { bg: "bg-red-500", text: "text-red-500" },
    };
    return colorMap[color as keyof typeof colorMap]?.[type] || "";
}

export default function DashboardOverviewPage() {
    const [stats] = useState<StatCard[]>([
        {
            title: "Total Users",
            value: 1234,
            trend: 23,
            color: "blue",
            icon: "M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z",
        },
        {
            title: "Revenue",
            value: 54239,
            trend: 17,
            color: "green",
            icon: "M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z",
            prefix: "$",
        },
        {
            title: "Orders",
            value: 573,
            trend: 4,
            color: "purple",
            icon: "M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z",
        },
        {
            title: "Products",
            value: 245,
            trend: 5,
            color: "orange",
            icon: "M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4",
        },
    ]);

    const [recentOrders] = useState<RecentOrder[]>([
        {
            id: "ORD-001",
            customer: "John Doe",
            amount: 1250.0,
            status: "completed",
            date: "2025-12-23T10:30:00",
        },
        {
            id: "ORD-002",
            customer: "Jane Smith",
            amount: 890.0,
            status: "processing",
            date: "2025-12-23T09:15:00",
        },
        {
            id: "ORD-003",
            customer: "Mike Johnson",
            amount: 2100.0,
            status: "pending",
            date: "2025-12-23T08:45:00",
        },
        {
            id: "ORD-004",
            customer: "Sarah Wilson",
            amount: 750.0,
            status: "completed",
            date: "2025-12-22T16:20:00",
        },
    ]);

    const [recentActivity] = useState<Activity[]>([
        {
            id: 1,
            type: "New order",
            description: "Order #ORD-001 was placed by John Doe",
            time: "2 minutes ago",
            color: "blue",
            icon: "M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z",
        },
        {
            id: 2,
            type: "Product update",
            description: "Wireless Headphones price updated to $99.99",
            time: "15 minutes ago",
            color: "green",
            icon: "M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4",
        },
        {
            id: 3,
            type: "User registered",
            description: "New user Sarah Wilson joined the platform",
            time: "1 hour ago",
            color: "purple",
            icon: "M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z",
        },
        {
            id: 4,
            type: "Payment received",
            description: "Payment of $1,250.00 received for order #ORD-001",
            time: "2 hours ago",
            color: "yellow",
            icon: "M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z",
        },
        {
            id: 5,
            type: "Inventory alert",
            description: "Low stock alert for Smart Watch (5 items remaining)",
            time: "3 hours ago",
            color: "red",
            icon: "M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z",
        },
    ]);

    const [performanceMetrics] = useState<PerformanceMetric[]>([
        { label: "Customer Satisfaction", value: 94, color: "blue" },
        { label: "Order Completion Rate", value: 87, color: "green" },
        { label: "Revenue Growth", value: 76, color: "purple" },
        { label: "Inventory Turnover", value: 82, color: "yellow" },
    ]);

    const getStatusColor = (status: string) => {
        const statusMap: Record<string, string> = {
            completed: "bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-300",
            pending: "bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-300",
            processing: "bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-300",
            cancelled: "bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-300",
        };
        return statusMap[status] || "bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300";
    };

    const formatCurrency = (amount: number) => {
        return new Intl.NumberFormat("en-US", {
            style: "currency",
            currency: "USD",
        }).format(amount);
    };

    const formatDate = (dateString: string) => {
        return new Date(dateString).toLocaleDateString("en-US", {
            month: "short",
            day: "numeric",
            hour: "2-digit",
            minute: "2-digit",
        });
    };

    const breadcrumbItems: BreadcrumbItem[] = [
        { label: "Home", href: "/" },
        { label: "Dashboard", href: "/dashboard" },
        { label: "Overview", href: "/dashboard/overview" },
    ];

    return (
        <DashboardLayout>
            {/* Breadcrumb */}
            <div className="mb-6">
                <Breadcrumb items={breadcrumbItems} />
            </div>

            <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
                <div className="md:col-span-2">
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h2 className="text-2xl font-semibold mb-4">Overview</h2>
                        <div className="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-4">
                            {stats.map((s) => (
                                <div key={s.title} className="p-4 bg-white dark:bg-gray-900 rounded shadow-sm">
                                    <h3 className="text-sm text-gray-500">{s.title}</h3>
                                    <p className="text-2xl font-bold">{s.prefix || ""}{s.value}</p>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>

                <div>
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h3 className="text-lg font-semibold">Recent Activity</h3>
                        <div className="mt-4 space-y-3">
                            {recentActivity.map((a) => (
                                <ActivityItem key={a.id} type={a.type} description={a.description} time={a.time} color={a.color} icon={a.icon} />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </DashboardLayout>
    );
}
