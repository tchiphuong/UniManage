"use client";


import { LanguageSwitcher } from "./language-switcher";

import Button from "@/components/common/button";

interface PageHeaderProps {
    title: string;
    description?: string;
    action?: {
        label: string;
        onClick: () => void;
    };
    showLanguageSwitcher?: boolean;
}

/**
 * Component header chuẩn cho các trang với i18n support
 */
export function PageHeaderI18n({
    title,
    description,
    action,
    showLanguageSwitcher = false,
}: PageHeaderProps) {
    return (
        <div className="flex items-center justify-between mb-6">
            <div className="flex-1">
                <h1 className="text-2xl font-bold text-gray-900 dark:text-white">{title}</h1>
                {description && (
                    <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">{description}</p>
                )}
            </div>
            <div className="flex items-center gap-4">
                {showLanguageSwitcher && <LanguageSwitcher />}
                {action && (
                    <Button
                        color="primary"
                        onPress={action.onClick}
                        // HeroUI Button uses onPress, but we can pass onClick to standard button props
                        onClick={action.onClick} 
                    >
                        {action.label}
                    </Button>
                )}
            </div>
        </div>
    );
}

export { PageHeaderI18n as PageHeader };
