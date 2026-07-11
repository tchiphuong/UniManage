"use client";

interface PageHeaderProps {
    title: string;
    description?: string;
    action?: {
        label: string;
        onClick: () => void;
    };
}

/**
 * Component header chuẩn cho các trang
 */
export function PageHeader({ title, description, action }: PageHeaderProps) {
    return (
        <div className="mb-6 flex items-center justify-between">
            <div>
                <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
                    {title}
                </h1>
                {description && (
                    <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
                        {description}
                    </p>
                )}
            </div>
            {action && (
                <button
                    onClick={action.onClick}
                    className="rounded-lg bg-blue-600 px-4 py-2 text-white transition-colors hover:bg-blue-700"
                >
                    {action.label}
                </button>
            )}
        </div>
    );
}
