import { StatsCard } from "@/components/dashboard";
import { RecentOrders } from "@/components/dashboard/recent-orders";

export default function Dashboard() {
    return (
        <div className="space-y-6">
            {/* Stats Cards */}
            <StatsCard
                stat={{
                    title: "Total Users",
                    value: 10245,
                    icon: "fa-users",
                    color: "blue",
                    change: "12%",
                    changeType: "up"
                }}
            />
            <StatsCard
                stat={{
                    title: "Revenue",
                    value: 84320,
                    prefix: "$",
                    icon: "fa-dollar-sign",
                    color: "green",
                    change: "8%",
                    changeType: "up"
                }}
            />
            <StatsCard
                stat={{
                    title: "Orders",
                    value: 1325,
                    icon: "fa-shopping-cart",
                    color: "purple",
                    change: "3%",
                    changeType: "down"
                }}
            />
            <StatsCard
                stat={{
                    title: "Conversion Rate",
                    value: 3.8,
                    suffix: "%",
                    icon: "fa-eye",
                    color: "yellow",
                    change: "1.2%",
                    changeType: "up"
                }}
            />

            {/* Charts Placeholders */}
            <div className="mb-8 grid grid-cols-1 gap-8 lg:grid-cols-2">
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Sales Overview</h3>
                    <div className="flex h-75 items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">User Growth</h3>
                    <div className="flex h-75 items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Revenue Distribution</h3>
                    <div className="flex h-75 items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
                <div className="rounded-lg bg-white p-6 shadow-md dark:bg-gray-800">
                    <h3 className="mb-4 text-lg font-semibold text-gray-700 dark:text-gray-300">Product Performance</h3>
                    <div className="flex h-75 items-center justify-center rounded bg-gray-50 dark:bg-gray-700">
                        <span className="text-gray-400">Chart Placeholder</span>
                    </div>
                </div>
            </div>

            {/* Recent Orders Table */}
            <RecentOrders />
        </div>
    );
}
