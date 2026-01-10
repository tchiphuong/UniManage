interface PerformanceMetricProps {
    label: string;
    value: number;
    color: "blue" | "green" | "purple" | "yellow";
}

export function PerformanceMetric({ label, value, color }: PerformanceMetricProps) {
    const getColorClass = (color: string) => {
        const colorMap: Record<string, string> = {
            blue: "bg-blue-600 dark:bg-blue-400",
            green: "bg-green-600 dark:bg-green-400",
            purple: "bg-purple-600 dark:bg-purple-400",
            yellow: "bg-yellow-600 dark:bg-yellow-400",
        };
        return colorMap[color] || "bg-gray-600";
    };

    return (
        <div>
            <div className="mb-2 flex items-center justify-between">
                <span className="text-sm font-medium text-gray-700 dark:text-gray-300">
                    {label}
                </span>
                <span className="text-sm font-bold text-gray-900 dark:text-white">{value}%</span>
            </div>
            <div className="h-2 w-full overflow-hidden rounded-full bg-gray-200 dark:bg-gray-700">
                <div
                    className={`h-full rounded-full ${getColorClass(color)}`}
                    style={{ width: `${value}%` }}
                />
            </div>
        </div>
    );
}
