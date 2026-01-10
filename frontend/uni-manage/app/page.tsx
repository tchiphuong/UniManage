"use client";

import { DashboardLayout } from "@/components";
import { StatsCard } from "@/components/dashboard/stats-card";
import { RecentOrders } from "@/components/dashboard/recent-orders";

export default function Home() {
    return (
        <DashboardLayout>
            {/* Dashboard Title */}
            <h1 className="mb-6 text-3xl font-semibold text-gray-800 dark:text-gray-100">
                Dashboard Overview
            </h1>

            {/* Stats Cards */}
            <div className="mb-8 grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-4">
                <StatsCard 
                    title="Total Users" 
                    value="10,245" 
                    change="12% from last month" 
                    changeType="positive" 
                    colorClass="text-blue-600"
                />
                <StatsCard 
                    title="Revenue" 
                    value="$84,320" 
                    change="8% from last month" 
                    changeType="positive" 
                    colorClass="text-green-600"
                />
                 <StatsCard 
                    title="Orders" 
                    value="1,325" 
                    change="3% from last month" 
                    changeType="negative" 
                    colorClass="text-purple-600"
                />
                 <StatsCard 
                    title="Conversion Rate" 
                    value="3.8%" 
                    change="1.2% from last month" 
                    changeType="positive" 
                    colorClass="text-yellow-600"
                />
            </div>

            {/* Charts Placeholders */}
            <div className="mb-8 grid grid-cols-1 gap-8 lg:grid-cols-2">
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Sales Overview</h3>
                    <div className="flex h-[300px] items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">User Growth</h3>
                   <div className="flex h-[300px] items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Revenue Distribution</h3>
                    <div className="flex h-[300px] items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                 <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Product Performance</h3>
                   <div className="flex h-[300px] items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
            </div>

            {/* Recent Orders Table */}
            <RecentOrders />
        </DashboardLayout>
    );
}
