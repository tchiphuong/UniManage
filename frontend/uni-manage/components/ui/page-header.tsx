import { ReactNode } from "react";

interface PageHeaderProps {
    title: string;
    action?: ReactNode;
}

export function PageHeader({ title, action }: PageHeaderProps) {
    return (
        <div className="mb-6 flex items-center justify-between">
            <h1 className="text-3xl font-semibold text-gray-800 dark:text-gray-200">{title}</h1>
            {action}
        </div>
    );
}
