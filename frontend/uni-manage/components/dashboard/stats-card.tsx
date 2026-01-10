interface StatsCardProps {
    title: string;
    value: string | number;
    change?: string;
    changeType?: "positive" | "negative";
    colorClass?: string;
}

export function StatsCard({ title, value, change, changeType, colorClass = "text-blue-600" }: StatsCardProps) {
    return (
        <div className="rounded-lg bg-white p-4 shadow-md dark:bg-gray-800">
            <div className="overflow-hidden">
                <h3 className="mb-2 text-lg font-semibold text-gray-700 dark:text-gray-300">{title}</h3>
                <p className={`text-3xl font-bold ${colorClass}`}>{value}</p>
                {change && (
                    <p className={`mt-2 text-sm ${changeType === "positive" ? "text-green-500" : "text-red-500"}`}>
                        {changeType === "positive" ? "↑" : "↓"} {change}
                    </p>
                )}
            </div>
        </div>
    );
}
