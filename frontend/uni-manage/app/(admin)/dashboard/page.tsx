"use client";

import { Card } from "@heroui/react";
import { useTranslations } from "next-intl";

import { StatsCard } from "./components";
import { RecentOrders } from "./components/recent-orders";

export default function Dashboard() {
    const t = useTranslations();

    return (
        <div className="relative space-y-8">
            {/* Ambient Background Blobs for Liquid Glass Effect */}
            <div className="pointer-events-none absolute inset-0 -z-10 overflow-hidden">
                <div className="bg-surface-secondary absolute -top-[10%] -left-[10%] h-[40%] w-[40%] rounded-full blur-[120px]" />
                <div className="bg-surface-tertiary absolute top-[20%] -right-[10%] h-[30%] w-[30%] rounded-full blur-[100px]" />
            </div>

            <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 xl:grid-cols-4">
                <StatsCard
                    stat={{
                        title: t("dashboard.overview.lbl.totalUsers"),
                        value: 10245,
                        icon: "/icons/group.png",
                        color: "blue",
                        change: "12%",
                        changeType: "up",
                    }}
                />
                <StatsCard
                    stat={{
                        title: t("dashboard.overview.lbl.revenue"),
                        value: 84320,
                        prefix: "$",
                        icon: "/icons/money.png",
                        color: "green",
                        change: "8%",
                        changeType: "up",
                    }}
                />
                <StatsCard
                    stat={{
                        title: t("dashboard.overview.lbl.orders"),
                        value: 1325,
                        icon: "/icons/shopping-cart.png",
                        color: "purple",
                        change: "3%",
                        changeType: "down",
                    }}
                />
                <StatsCard
                    stat={{
                        title: t("dashboard.overview.lbl.conversionRate"),
                        value: 3.8,
                        suffix: "%",
                        icon: "/icons/visible.png",
                        color: "yellow",
                        change: "1.2%",
                        changeType: "up",
                    }}
                />
            </div>

            {/* Charts Placeholders */}
            <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
                <Card className="border-surface-tertiary bg-surface-secondary/40 border shadow-sm backdrop-blur-md">
                    <Card.Content className="p-6">
                        <h3 className="text-foreground mb-6 text-lg font-semibold tracking-tight">
                            {t("dashboard.overview.lbl.salesOverview")}
                        </h3>
                        <div className="border-border-secondary bg-surface-tertiary/50 flex h-[300px] items-center justify-center rounded-xl border border-dashed">
                            <span className="text-muted text-sm">
                                {t(
                                    "dashboard.overview.msg.areaChartPlaceholder",
                                )}
                            </span>
                        </div>
                    </Card.Content>
                </Card>

                <Card className="border-surface-tertiary bg-surface-secondary/40 border shadow-sm backdrop-blur-md">
                    <Card.Content className="p-6">
                        <h3 className="text-foreground mb-6 text-lg font-semibold tracking-tight">
                            {t("dashboard.overview.lbl.userGrowth")}
                        </h3>
                        <div className="border-border-secondary bg-surface-tertiary/50 flex h-[300px] items-center justify-center rounded-xl border border-dashed">
                            <span className="text-muted text-sm">
                                {t(
                                    "dashboard.overview.msg.barChartPlaceholder",
                                )}
                            </span>
                        </div>
                    </Card.Content>
                </Card>
            </div>

            {/* Recent Orders Table inside Glass Card */}
            <Card className="border-surface-tertiary bg-surface-secondary/40 border shadow-sm backdrop-blur-md">
                <Card.Content className="p-0 sm:p-2">
                    <RecentOrders />
                </Card.Content>
            </Card>
        </div>
    );
}
