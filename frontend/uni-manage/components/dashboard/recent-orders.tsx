"use client";

import { Table, Chip } from "@heroui/react";
import { useTranslations } from "next-intl";

const rows = [
    {
        id: "1",
        customer: "John Doe",
        total: "$234.50",
        status: "Completed",
    },
    {
        id: "2",
        customer: "Jane Smith",
        total: "$125.99",
        status: "Processing",
    },
    {
        id: "3",
        customer: "Bob Johnson",
        total: "$456.75",
        status: "Shipped",
    },
    {
        id: "4",
        customer: "Alice Brown",
        total: "$789.20",
        status: "Completed",
    },
    {
        id: "5",
        customer: "Charlie Wilson",
        total: "$55.50",
        status: "Processing",
    },
];

const statusColorMap: Record<string, "success" | "warning" | "accent"> = {
    Completed: "success",
    Processing: "warning",
    Shipped: "accent",
};

export function RecentOrders() {
    const t = useTranslations();

    return (
        <div className="p-4">
            <h3 className="text-foreground mb-4 text-lg font-semibold tracking-tight">
                {t("dashboard.overview.lbl.recentOrders")}
            </h3>
            <Table className="bg-transparent shadow-none p-0">
                <Table.ScrollContainer>
                    <Table.Content
                        aria-label={t("dashboard.overview.lbl.recentOrders")}
                    >
                        <Table.Header>
                            <Table.Column isRowHeader>
                                {t("dashboard.overview.lbl.orderId")}
                            </Table.Column>
                            <Table.Column>
                                {t("dashboard.overview.lbl.customer")}
                            </Table.Column>
                            <Table.Column>
                                {t("dashboard.overview.lbl.total")}
                            </Table.Column>
                            <Table.Column>
                                {t("dashboard.overview.lbl.status")}
                            </Table.Column>
                        </Table.Header>
                        <Table.Body items={rows}>
                            {(item) => (
                                <Table.Row key={item.id}>
                                    <Table.Cell className="text-foreground font-medium">
                                        #{item.id}
                                    </Table.Cell>
                                    <Table.Cell>{item.customer}</Table.Cell>
                                    <Table.Cell className="font-mono">
                                        {item.total}
                                    </Table.Cell>
                                    <Table.Cell>
                                        <Chip
                                            size="sm"
                                            variant="soft"
                                            color={
                                                statusColorMap[item.status] ||
                                                "default"
                                            }
                                        >
                                            {item.status}
                                        </Chip>
                                    </Table.Cell>
                                </Table.Row>
                            )}
                        </Table.Body>
                    </Table.Content>
                </Table.ScrollContainer>
            </Table>
        </div>
    );
}
