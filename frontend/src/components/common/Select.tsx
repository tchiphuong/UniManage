import React, { forwardRef } from "react";

/**
 * Select Component - FlyonUI select dropdown
 *
 * @example
 * <Select
 *   label="Vai trò"
 *   value={role}
 *   onChange={(e) => setRole(e.target.value)}
 *   options={[
 *     { value: 'admin', label: 'Quản trị viên' },
 *     { value: 'user', label: 'Người dùng' }
 *   ]}
 * />
 */

export interface SelectOption {
    value: string | number;
    label: string;
    disabled?: boolean;
}

export interface SelectProps extends Omit<React.SelectHTMLAttributes<HTMLSelectElement>, "size"> {
    label?: string;
    options: SelectOption[];
    error?: boolean;
    helperText?: string;
    size?: "xs" | "sm" | "md" | "lg";
    fullWidth?: boolean;
}

export const Select = forwardRef<HTMLSelectElement, SelectProps>(
    (
        {
            label,
            options,
            error = false,
            helperText,
            size = "md",
            fullWidth = true,
            className = "",
            ...props
        },
        ref
    ) => {
        const baseClasses =
            "select select-bordered transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent";

        const sizeClasses = {
            xs: "select-xs",
            sm: "select-sm",
            md: "select-md",
            lg: "select-lg",
        };

        const errorClasses = error ? "select-error border-red-500 focus:ring-red-500" : "";
        const widthClasses = fullWidth ? "w-full" : "";

        const selectClasses = `
          ${baseClasses}
          ${sizeClasses[size]}
          ${errorClasses}
          ${widthClasses}
          ${className}
        `
            .trim()
            .replace(/\s+/g, " ");

        return (
            <div className={fullWidth ? "w-full" : ""}>
                {label && (
                    <label className={`label ${error ? "text-red-500" : ""}`}>
                        <span className="label-text font-medium">{label}</span>
                    </label>
                )}

                <select ref={ref} className={selectClasses} {...props}>
                    <option value="">Chọn...</option>
                    {options.map((option) => (
                        <option key={option.value} value={option.value} disabled={option.disabled}>
                            {option.label}
                        </option>
                    ))}
                </select>

                {helperText && (
                    <div className={`label ${error ? "text-red-500" : "text-gray-500"}`}>
                        <span className="label-text-alt">{helperText}</span>
                    </div>
                )}
            </div>
        );
    }
);

Select.displayName = "Select";
