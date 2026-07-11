import { ReactNode } from "react";

interface SettingCardProps {
    title: string;
    description?: string;
    children: ReactNode;
    className?: string;
}

/**
 * Reusable Setting Card Component
 * Provides consistent styling for settings sections
 */
export function SettingCard({
    title,
    description,
    children,
    className = "",
}: SettingCardProps) {
    return (
        <div
            className={`rounded-lg bg-white p-6 shadow-sm dark:bg-gray-800 ${className}`}
        >
            <div className="mb-6">
                <h2 className="text-foreground text-xl font-semibold">
                    {title}
                </h2>
                {description && (
                    <p className="text-default-500 mt-1 text-sm">
                        {description}
                    </p>
                )}
            </div>
            {children}
        </div>
    );
}
