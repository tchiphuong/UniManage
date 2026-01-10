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
export function SettingCard({ title, description, children, className = "" }: SettingCardProps) {
    return (
        <div className={`bg-white dark:bg-gray-800 rounded-lg p-6 shadow-sm ${className}`}>
            <div className="mb-6">
                <h2 className="text-xl font-semibold text-foreground">{title}</h2>
                {description && <p className="text-sm text-default-500 mt-1">{description}</p>}
            </div>
            {children}
        </div>
    );
}
