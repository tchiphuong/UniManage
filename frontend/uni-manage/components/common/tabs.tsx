"use client";

import { Tabs as HeroTabs, Tab, TabList, TabPanel } from "@heroui/react";
import { ReactNode } from "react";

interface TabItem {
    id: string;
    label: string;
    content: ReactNode;
}

interface TabsProps {
    items: TabItem[];
    defaultTab?: string;
    onChange?: (key: string) => void;
    className?: string;
}

/**
 * Reusable Tabs Component
 * Wrapper around HeroUI Tabs with simplified API
 */
export function Tabs({ items, defaultTab, onChange, className = "" }: TabsProps) {
    return (
        <HeroTabs
            defaultSelectedKey={defaultTab || items[0]?.id}
            onSelectionChange={(key) => onChange?.(key as string)}
            className={className}
        >
            <TabList className="flex gap-2 border-b border-default-200 mb-6">
                {items.map((item) => (
                    <Tab
                        key={item.id}
                        id={item.id}
                        className="px-4 py-2 text-sm font-medium data-[selected=true]:border-b-2 data-[selected=true]:border-primary data-[selected=true]:text-primary transition-colors"
                    >
                        {item.label}
                    </Tab>
                ))}
            </TabList>

            {items.map((item) => (
                <TabPanel key={item.id} id={item.id} className="py-4">
                    {item.content}
                </TabPanel>
            ))}
        </HeroTabs>
    );
}
