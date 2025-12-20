import React, { forwardRef } from "react";

/**
 * Checkbox Component - FlyonUI checkbox component
 *
 * @example
 * <Checkbox
 *   label="Ghi nhớ đăng nhập"
 *   checked={rememberMe}
 *   onChange={(e) => setRememberMe(e.target.checked)}
 * />
 */

export interface CheckboxProps extends Omit<React.InputHTMLAttributes<HTMLInputElement>, "size"> {
    label?: string;
    size?: "xs" | "sm" | "md" | "lg";
    color?: "primary" | "secondary" | "accent" | "success" | "warning" | "info" | "error";
    error?: boolean;
}

export const Checkbox = forwardRef<HTMLInputElement, CheckboxProps>(
    ({ label, size = "md", color = "primary", error = false, className = "", ...props }, ref) => {
        const baseClasses = "checkbox transition-all duration-200";

        const sizeClasses = {
            xs: "checkbox-xs",
            sm: "checkbox-sm",
            md: "checkbox-md",
            lg: "checkbox-lg",
        };

        const colorClasses = {
            primary: "checkbox-primary",
            secondary: "checkbox-secondary",
            accent: "checkbox-accent",
            success: "checkbox-success",
            warning: "checkbox-warning",
            info: "checkbox-info",
            error: "checkbox-error",
        };

        const errorClasses = error ? "checkbox-error" : "";

        const checkboxClasses = `
            ${baseClasses}
            ${sizeClasses[size]}
            ${colorClasses[color]}
            ${errorClasses}
            ${className}
        `
            .trim()
            .replace(/\s+/g, " ");

        if (label) {
            return (
                <label className="flex items-center space-x-2 cursor-pointer">
                    <input ref={ref} type="checkbox" className={checkboxClasses} {...props} />
                    <span className={`label-text ${error ? "text-red-500" : "text-gray-700"}`}>
                        {label}
                    </span>
                </label>
            );
        }

        return <input ref={ref} type="checkbox" className={checkboxClasses} {...props} />;
    }
);

Checkbox.displayName = "Checkbox";
