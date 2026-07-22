import { Card } from "@heroui/react";

export interface StatItem {
    title: string;
    value: number | string;
    prefix?: string;
    suffix?: string;
    icon: string; // Icon name or image URL directly from Icons8
    color: string;
    change?: string;
    changeType?: "up" | "down" | "neutral";
}

interface StatsCardProps {
    stat: StatItem;
}

export function StatsCard({ stat }: Readonly<StatsCardProps>) {
    const { title, value, prefix, suffix, icon, change, changeType } = stat;

    let changeColor = "text-zinc-500";
    let changeIcon = "";
    if (changeType === "up") {
        changeColor = "text-success-500";
        changeIcon = "↑";
    } else if (changeType === "down") {
        changeColor = "text-danger-500";
        changeIcon = "↓";
    }

    // Use the direct URL if it starts with http, otherwise fallback to the slug guesser
    const iconUrl = icon.startsWith("http")
        ? icon
        : `https://img.icons8.com/color-glass/96/${icon}.png`;

    return (
        <Card className="border border-white/20 bg-white/40 shadow-sm backdrop-blur-md dark:border-white/10 dark:bg-zinc-900/40">
            <div className="p-6">
                <div className="flex items-center justify-between">
                    <div>
                        <p className="text-sm font-medium text-zinc-500 dark:text-zinc-400">
                            {title}
                        </p>
                        <div className="mt-2 flex items-baseline gap-2">
                            <h3 className="text-3xl font-bold tracking-tight text-zinc-800 dark:text-white">
                                {prefix}
                                {value}
                                {suffix}
                            </h3>
                            {change && (
                                <span
                                    className={`text-xs font-semibold ${changeColor}`}
                                >
                                    {changeIcon} {change}
                                </span>
                            )}
                        </div>
                    </div>

                    <div className="flex h-16 w-16 items-center justify-center drop-shadow-md">
                        {/* eslint-disable-next-line @next/next/no-img-element */}
                        <img
                            src={iconUrl}
                            alt={title}
                            className="h-14 w-14 object-contain"
                        />
                    </div>
                </div>
            </div>
        </Card>
    );
}
