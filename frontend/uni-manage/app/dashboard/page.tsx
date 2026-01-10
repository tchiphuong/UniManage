"use client";

import { useTranslations } from "next-intl";
import { DashboardLayout } from "@/components/dashboard-layout";

export default function DashboardPage() {
    const t = useTranslations("dashboard");
    const tCommon = useTranslations("common");

    return (
        <DashboardLayout>
            {/* Breadcrumb */}
            <nav aria-label="Breadcrumb" className="mb-6">
                <ol className="inline-flex list-none p-0 gap-2">
                    <li className="flex items-center">
                        <svg
                            className="mr-2 h-4 w-4 text-gray-500 dark:text-gray-400"
                            fill="currentColor"
                            viewBox="0 0 20 20"
                        >
                            <path d="M10.707 2.293a1 1 0 00-1.414 0l-7 7a1 1 0 001.414 1.414L4 10.414V17a1 1 0 001 1h2a1 1 0 001-1v-2a1 1 0 011-1h2a1 1 0 011 1v2a1 1 0 001 1h2a1 1 0 001-1v-6.586l.293.293a1 1 0 001.414-1.414l-7-7z" />
                        </svg>
                        <span className="text-gray-600 dark:text-gray-300">Home</span>
                    </li>
                    <li className="flex items-center">
                        <svg
                            className="mx-2 h-4 w-4 text-gray-500 dark:text-gray-400"
                            fill="currentColor"
                            viewBox="0 0 20 20"
                        >
                            <path
                                fillRule="evenodd"
                                d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                                clipRule="evenodd"
                            />
                        </svg>
                        <span className="text-gray-800 dark:text-gray-200 font-semibold">Dashboard Overview</span>
                    </li>
                </ol>
            </nav>

            {/* Page Header */}
            <div className="flex items-center justify-between mb-6">
                <h1 className="text-3xl font-semibold text-gray-800 dark:text-gray-200">Dashboard Overview</h1>
                <button className="flex items-center px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-200 bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
                    <svg
                        className="mr-2 h-4 w-4"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15"
                        />
                    </svg>
                    Refresh
                </button>
            </div>

            {/* Stats Cards */}
            <div className="mb-6 grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-4">
                {/* Total Users */}
                <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                    <div className="flex items-center justify-between mb-4">
                        <div className="rounded-full bg-blue-100 dark:bg-blue-900/30 p-3">
                            <svg className="h-6 w-6 text-blue-600 dark:text-blue-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
                            </svg>
                        </div>
                    </div>
                    <h3 className="text-sm font-medium text-gray-600 dark:text-gray-400 mb-2">Total Users</h3>
                    <div className="flex items-end justify-between">
                        <p className="text-3xl font-bold text-gray-900 dark:text-white">1,234</p>
                        <span className="flex items-center text-sm text-green-600 dark:text-green-400">
                            <svg className="h-4 w-4 mr-1" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M5.293 9.707a1 1 0 010-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 01-1.414 1.414L11 7.414V15a1 1 0 11-2 0V7.414L6.707 9.707a1 1 0 01-1.414 0z" clipRule="evenodd" />
                            </svg>
                            23%
                        </span>
                    </div>
                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-2">from last month</p>
                </div>

                {/* Revenue */}
                <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                    <div className="flex items-center justify-between mb-4">
                        <div className="rounded-full bg-green-100 dark:bg-green-900/30 p-3">
                            <svg className="h-6 w-6 text-green-600 dark:text-green-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                        </div>
                    </div>
                    <h3 className="text-sm font-medium text-gray-600 dark:text-gray-400 mb-2">Revenue</h3>
                    <div className="flex items-end justify-between">
                        <p className="text-3xl font-bold text-gray-900 dark:text-white">$54,239</p>
                        <span className="flex items-center text-sm text-green-600 dark:text-green-400">
                            <svg className="h-4 w-4 mr-1" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M5.293 9.707a1 1 0 010-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 01-1.414 1.414L11 7.414V15a1 1 0 11-2 0V7.414L6.707 9.707a1 1 0 01-1.414 0z" clipRule="evenodd" />
                            </svg>
                            17%
                        </span>
                    </div>
                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-2">from last month</p>
                </div>

                {/* Orders */}
                <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                    <div className="flex items-center justify-between mb-4">
                        <div className="rounded-full bg-purple-100 dark:bg-purple-900/30 p-3">
                            <svg className="h-6 w-6 text-purple-600 dark:text-purple-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z" />
                            </svg>
                        </div>
                    </div>
                    <h3 className="text-sm font-medium text-gray-600 dark:text-gray-400 mb-2">Orders</h3>
                    <div className="flex items-end justify-between">
                        <p className="text-3xl font-bold text-gray-900 dark:text-white">573</p>
                        <span className="flex items-center text-sm text-green-600 dark:text-green-400">
                            <svg className="h-4 w-4 mr-1" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M5.293 9.707a1 1 0 010-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 01-1.414 1.414L11 7.414V15a1 1 0 11-2 0V7.414L6.707 9.707a1 1 0 01-1.414 0z" clipRule="evenodd" />
                            </svg>
                            4%
                        </span>
                    </div>
                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-2">from last month</p>
                </div>

                {/* Products */}
                <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                    <div className="flex items-center justify-between mb-4">
                        <div className="rounded-full bg-yellow-100 dark:bg-yellow-900/30 p-3">
                            <svg className="h-6 w-6 text-yellow-600 dark:text-yellow-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
                            </svg>
                        </div>
                    </div>
                    <h3 className="text-sm font-medium text-gray-600 dark:text-gray-400 mb-2">Products</h3>
                    <div className="flex items-end justify-between">
                        <p className="text-3xl font-bold text-gray-900 dark:text-white">245</p>
                        <span className="flex items-center text-sm text-red-600 dark:text-red-400">
                            <svg className="h-4 w-4 mr-1" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M14.707 10.293a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 111.414-1.414L9 12.586V5a1 1 0 012 0v7.586l2.293-2.293a1 1 0 011.414 0z" clipRule="evenodd" />
                            </svg>
                            5%
                        </span>
                    </div>
                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-2">from last month</p>
                </div>
            </div>

            {/* Main Grid - 2 columns */}
            <div className="grid grid-cols-1 xl:grid-cols-3 gap-6 mb-6">
                {/* Left Column - Notifications + Quick Actions */}
                <div className="xl:col-span-1 space-y-6">
                    {/* Recent Notifications */}
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h3 className="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-4 flex items-center">
                            <svg className="mr-2 h-5 w-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" />
                            </svg>
                            Recent Notifications
                        </h3>
                        <div className="space-y-4">
                            <div className="flex items-start space-x-3 pb-4 border-b border-gray-100 dark:border-gray-700">
                                <div className="rounded-full p-2 bg-blue-100 dark:bg-blue-900/30">
                                    <div className="h-2 w-2 rounded-full bg-blue-600"></div>
                                </div>
                                <div className="flex-1">
                                    <p className="text-sm font-medium text-gray-900 dark:text-white">New order received</p>
                                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-1">2 minutes ago</p>
                                </div>
                            </div>
                            <div className="flex items-start space-x-3 pb-4 border-b border-gray-100 dark:border-gray-700">
                                <div className="rounded-full p-2 bg-red-100 dark:bg-red-900/30">
                                    <div className="h-2 w-2 rounded-full bg-red-600"></div>
                                </div>
                                <div className="flex-1">
                                    <p className="text-sm font-medium text-gray-900 dark:text-white">Low stock alert</p>
                                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-1">15 minutes ago</p>
                                </div>
                            </div>
                            <div className="flex items-start space-x-3 pb-4 border-b border-gray-100 dark:border-gray-700">
                                <div className="rounded-full p-2 bg-green-100 dark:bg-green-900/30">
                                    <div className="h-2 w-2 rounded-full bg-green-600"></div>
                                </div>
                                <div className="flex-1">
                                    <p className="text-sm font-medium text-gray-900 dark:text-white">System update completed</p>
                                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-1">1 hour ago</p>
                                </div>
                            </div>
                            <div className="flex items-start space-x-3">
                                <div className="rounded-full p-2 bg-purple-100 dark:bg-purple-900/30">
                                    <div className="h-2 w-2 rounded-full bg-purple-600"></div>
                                </div>
                                <div className="flex-1">
                                    <p className="text-sm font-medium text-gray-900 dark:text-white">Payment processed</p>
                                    <p className="text-xs text-gray-500 dark:text-gray-400 mt-1">2 hours ago</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Quick Actions */}
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h3 className="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-4 flex items-center">
                            <svg className="mr-2 h-5 w-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 10V3L4 14h7v7l9-11h-7z" />
                            </svg>
                            Quick Actions
                        </h3>
                        <div className="grid grid-cols-2 gap-3">
                            <button className="flex flex-col items-center justify-center p-4 rounded-lg bg-blue-50 dark:bg-blue-900/20 hover:bg-blue-100 dark:hover:bg-blue-900/30 transition-colors">
                                <svg className="h-6 w-6 text-blue-600 dark:text-blue-400 mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
                                </svg>
                                <span className="text-xs font-medium text-gray-700 dark:text-gray-300 text-center">+New Order</span>
                            </button>
                            <button className="flex flex-col items-center justify-center p-4 rounded-lg bg-green-50 dark:bg-green-900/20 hover:bg-green-100 dark:hover:bg-green-900/30 transition-colors">
                                <svg className="h-6 w-6 text-green-600 dark:text-green-400 mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
                                </svg>
                                <span className="text-xs font-medium text-gray-700 dark:text-gray-300 text-center">Add User</span>
                            </button>
                            <button className="flex flex-col items-center justify-center p-4 rounded-lg bg-purple-50 dark:bg-purple-900/20 hover:bg-purple-100 dark:hover:bg-purple-900/30 transition-colors">
                                <svg className="h-6 w-6 text-purple-600 dark:text-purple-400 mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
                                </svg>
                                <span className="text-xs font-medium text-gray-700 dark:text-gray-300 text-center">New Product</span>
                            </button>
                            <button className="flex flex-col items-center justify-center p-4 rounded-lg bg-yellow-50 dark:bg-yellow-900/20 hover:bg-yellow-100 dark:hover:bg-yellow-900/30 transition-colors">
                                <svg className="h-6 w-6 text-yellow-600 dark:text-yellow-400 mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                                </svg>
                                <span className="text-xs font-medium text-gray-700 dark:text-gray-300 text-center">Reports</span>
                            </button>
                        </div>
                    </div>
                </div>

                {/* Right Column - Charts + Performance */}
                <div className="xl:col-span-2 space-y-6">
                    {/* Revenue Trend Chart Placeholder */}
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h3 className="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-4 flex items-center">Revenue Trend</h3>
                        <div className="h-48 bg-gray-50 dark:bg-gray-900 rounded-lg" />
                    </div>

                    {/* Performance Metrics */}
                    <div className="rounded-xl bg-white dark:bg-gray-800 p-6 shadow-lg border border-gray-200 dark:border-gray-700">
                        <h3 className="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-4">Performance Metrics</h3>
                        <div className="grid grid-cols-2 gap-4">
                            <div className="p-4 bg-gray-50 dark:bg-gray-900 rounded">Metric A</div>
                            <div className="p-4 bg-gray-50 dark:bg-gray-900 rounded">Metric B</div>
                        </div>
                    </div>
                </div>
            </div>
        </DashboardLayout>
    );
}
