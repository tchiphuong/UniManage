import React, { forwardRef, useEffect, useRef } from "react";
import flatpickr from "flatpickr";
import "flatpickr/dist/flatpickr.css";

/**
 * DatePicker - FlyonUI date picker with flatpickr integration
 * @example
 * <DatePicker
 *   label="Ngày sinh"
 *   value={birthDate}
 *   onChange={(date) => setBirthDate(date)}
 *   mode="date"
 * />
 */
export interface DatePickerProps {
    label?: string;
    value?: string;
    onChange?: (date: string) => void;
    error?: boolean;
    helperText?: string;
    fullWidth?: boolean;
    size?: "xs" | "sm" | "md" | "lg";
    mode?: "date" | "time" | "datetime" | "range";
    placeholder?: string;
    disabled?: boolean;
    required?: boolean;
    className?: string;
}

export const DatePicker = forwardRef<HTMLInputElement, DatePickerProps>(
    (
        {
            label,
            value = "",
            onChange,
            error = false,
            helperText,
            fullWidth = true,
            size = "md",
            mode = "date",
            placeholder,
            disabled = false,
            required = false,
            className = "",
        },
        ref
    ) => {
        const inputRef = useRef<HTMLInputElement>(null);
        const flatpickrRef = useRef<flatpickr.Instance | null>(null);

        // Combine refs
        React.useImperativeHandle(ref, () => inputRef.current!);

        useEffect(() => {
            if (!inputRef.current) return;

            // Flatpickr configuration based on mode
            const config: flatpickr.Options.Options = {
                monthSelectorType: "static",
                onChange: (_, dateStr) => {
                    if (onChange) {
                        onChange(dateStr);
                    }
                },
            };

            // Configure based on mode
            switch (mode) {
                case "time":
                    config.enableTime = true;
                    config.noCalendar = true;
                    config.dateFormat = "H:i";
                    config.time_24hr = true;
                    break;
                case "datetime":
                    config.enableTime = true;
                    config.dateFormat = "Y-m-d H:i";
                    config.time_24hr = true;
                    break;
                case "range":
                    config.mode = "range";
                    config.dateFormat = "Y-m-d";
                    break;
                default: // date
                    config.dateFormat = "Y-m-d";
                    break;
            }

            // Initialize flatpickr
            flatpickrRef.current = flatpickr(inputRef.current, config);

            // Set initial value if provided
            if (value) {
                flatpickrRef.current.setDate(value);
            }

            // Cleanup function
            return () => {
                if (flatpickrRef.current) {
                    flatpickrRef.current.destroy();
                    flatpickrRef.current = null;
                }
            };
        }, [mode]);

        // Update value when prop changes
        useEffect(() => {
            if (flatpickrRef.current && value !== undefined) {
                flatpickrRef.current.setDate(value);
            }
        }, [value]);

        const baseClasses =
            "input input-bordered transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent";

        const sizeClasses = {
            xs: "input-xs",
            sm: "input-sm",
            md: "input-md",
            lg: "input-lg",
        };

        const errorClasses = error ? "input-error border-red-500 focus:ring-red-500" : "";
        const widthClasses = fullWidth ? "w-full" : "max-w-sm";
        const disabledClasses = disabled ? "input-disabled opacity-50" : "";

        const inputClasses = `
            ${baseClasses}
            ${sizeClasses[size]}
            ${errorClasses}
            ${widthClasses}
            ${disabledClasses}
            ${className}
        `
            .trim()
            .replace(/\s+/g, " ");

        // Generate placeholder based on mode
        const getPlaceholder = () => {
            if (placeholder) return placeholder;
            switch (mode) {
                case "time":
                    return "HH:MM";
                case "datetime":
                    return "YYYY-MM-DD HH:MM";
                case "range":
                    return "YYYY-MM-DD to YYYY-MM-DD";
                default:
                    return "YYYY-MM-DD";
            }
        };

        return (
            <div className={fullWidth ? "w-full" : ""}>
                {label && (
                    <label className={`label ${error ? "text-red-500" : ""}`}>
                        <span className="label-text font-medium">
                            {label}
                            {required && <span className="text-red-500 ml-1">*</span>}
                        </span>
                    </label>
                )}

                <input
                    ref={inputRef}
                    type="text"
                    className={inputClasses}
                    placeholder={getPlaceholder()}
                    disabled={disabled}
                    required={required}
                    readOnly
                />

                {helperText && (
                    <div className={`label ${error ? "text-red-500" : "text-gray-500"}`}>
                        <span className="label-text-alt">{helperText}</span>
                    </div>
                )}
            </div>
        );
    }
);

DatePicker.displayName = "DatePicker";
