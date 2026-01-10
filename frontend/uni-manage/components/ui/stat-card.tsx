interface StatCardProps {
    title: string;
    value: number;
    trend: number;
    color: "blue" | "green" | "purple" | "orange" | "yellow" | "red";
    icon: string;
    prefix?: string;
    suffix?: string;
}

export function StatCard({
    title,
    value,
    trend,
    color,
    icon,
    prefix = "",
    suffix = "",
}: StatCardProps) {
    const getColorClasses = (color: string) => {
        const colorMap: Record<string, { bg: string; text: string }> = {
            blue: {
                bg: "bg-blue-100 dark:bg-blue-900/30",
                text: "text-blue-600 dark:text-blue-400",
            },
            green: {
                bg: "bg-green-100 dark:bg-green-900/30",
                text: "text-green-600 dark:text-green-400",
            },
            purple: {
                bg: "bg-purple-100 dark:bg-purple-900/30",
                text: "text-purple-600 dark:text-purple-400",
            },
            orange: {
                bg: "bg-orange-100 dark:bg-orange-900/30",
                text: "text-orange-600 dark:text-orange-400",
            },
            yellow: {
                bg: "bg-yellow-100 dark:bg-yellow-900/30",
                text: "text-yellow-600 dark:text-yellow-400",
            },
            red: {
                bg: "bg-red-100 dark:bg-red-900/30",
                text: "text-red-600 dark:text-red-400",
            },
        };
        return colorMap[color] || { bg: "bg-gray-100", text: "text-gray-600" };
    };

    const colors = getColorClasses(color);

    return (
        <div className="relative overflow-hidden rounded-xl border border-gray-200 bg-gradient-to-br from-white to-gray-50 p-6 shadow-lg transition-all duration-300 hover:shadow-xl dark:border-gray-600 dark:from-gray-800 dark:to-gray-700">
            <div className="mb-4 flex items-center justify-between">
                <h3 className="text-lg font-semibold text-gray-700 dark:text-gray-200">{title}</h3>
                <div
                    className={`flex h-12 w-12 items-center justify-center rounded-full ${colors.bg}`}
                >
                    <svg
                        className={`h-6 w-6 ${colors.text}`}
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d={icon}
                        />
                    </svg>
                </div>
            </div>
            <div className="space-y-2">
                <p className="text-3xl font-bold text-gray-900 dark:text-white">
                    {prefix}
                    {value.toLocaleString()}
                    {suffix}
                </p>
                <p className="text-sm text-gray-600 dark:text-gray-400">
                    <span
                        className={`${
                            trend >= 0
                                ? "text-green-600 dark:text-green-400"
                                : "text-red-600 dark:text-red-400"
                        }`}
                    >
                        {trend >= 0 ? "↑" : "↓"} {Math.abs(trend)}%
                    </span>{" "}
                    from last month
                </p>
            </div>
        </div>
    );
}
