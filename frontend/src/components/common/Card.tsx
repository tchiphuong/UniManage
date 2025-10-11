import { FC, ReactNode } from "react";

interface CardProps {
    children: ReactNode;
    title?: string;
    className?: string;
}

export const Card: FC<CardProps> = ({ children, title, className = "" }) => {
    return (
        <div className={`bg-white dark:bg-gray-800 rounded-lg shadow-lg ${className}`}>
            {title && (
                <div className="px-6 py-4 border-b border-gray-200 dark:border-gray-700">
                    <h2 className="text-xl font-semibold text-gray-800 dark:text-white">{title}</h2>
                </div>
            )}
            <div className="p-6">{children}</div>
        </div>
    );
};
