interface ActivityItemProps {
    type: string;
    description: string;
    time: string;
    color: "blue" | "green" | "purple" | "yellow" | "red" | "orange";
    icon: string;
}

export function ActivityItem({ type, description, time, color, icon }: ActivityItemProps) {
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
        <div className="flex items-start space-x-4 rounded-lg p-4 transition-colors hover:bg-gray-50 dark:hover:bg-gray-700">
            <div
                className={`flex h-10 w-10 flex-shrink-0 items-center justify-center rounded-full ${colors.bg}`}
            >
                <svg
                    className={`h-5 w-5 ${colors.text}`}
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                >
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d={icon} />
                </svg>
            </div>
            <div className="flex-1">
                <div className="flex items-start justify-between">
                    <div>
                        <p className="text-sm font-semibold text-gray-900 dark:text-white">
                            {type}
                        </p>
                        <p className="mt-1 text-sm text-gray-600 dark:text-gray-300">
                            {description}
                        </p>
                    </div>
                    <span className="ml-4 whitespace-nowrap text-xs text-gray-500 dark:text-gray-400">
                        {time}
                    </span>
                </div>
            </div>
        </div>
    );
}
