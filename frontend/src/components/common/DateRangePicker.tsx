import React, { forwardRef, useEffect, useRef } from "react";
import flatpickr from "flatpickr";
import "flatpickr/dist/flatpickr.css";

export interface DateRangePickerProps {
    label?: string;
    value?: string;
    onChange?: (range: string) => void;
    error?: boolean;
    helperText?: string;
    fullWidth?: boolean;
    size?: "xs" | "sm" | "md" | "lg";
    placeholder?: string;
    disabled?: boolean;
    required?: boolean;
    className?: string;
    separator?: string;
}

export const DateRangePicker = forwardRef<HTMLInputElement, DateRangePickerProps>(
    (props, ref) => {
        const {
            label,
            value = "",
            onChange,
            error = false,
            helperText,
            fullWidth = true,
            size = "md",
            placeholder = "YYYY-MM-DD to YYYY-MM-DD",
            disabled = false,
            required = false,
            className = "",
            separator = " to ",
        } = props;

        const inputRef = useRef<HTMLInputElement>(null);
        const flatpickrRef = useRef<flatpickr.Instance | null>(null);

        React.useImperativeHandle(ref, () => inputRef.current!);

        useEffect(() => {
            if (!inputRef.current) return;

            const config: flatpickr.Options.Options = {
                mode: "range",
                dateFormat: "Y-m-d",
                monthSelectorType: "static",
                conjunction: separator,
                onChange: (_, dateStr) => {
                    if (onChange) {
                        onChange(dateStr);
                    }
                },
            };

            flatpickrRef.current = flatpickr(inputRef.current, config);

            if (value) {
                flatpickrRef.current.setDate(value);
            }

            return () => {
                if (flatpickrRef.current) {
                    flatpickrRef.current.destroy();
                    flatpickrRef.current = null;
                }
            };
        }, [separator, onChange]);

        useEffect(() => {
            if (flatpickrRef.current && value !== undefined) {
                flatpickrRef.current.setDate(value);
            }
        }, [value]);

        const baseClasses = "input input-bordered";
        const sizeClasses = {
            xs: "input-xs",
            sm: "input-sm", 
            md: "input-md",
            lg: "input-lg",
        };

        const errorClasses = error ? "input-error" : "";
        const widthClasses = fullWidth ? "w-full" : "max-w-sm";
        const disabledClasses = disabled ? "input-disabled" : "";

        const inputClasses = `${baseClasses} ${sizeClasses[size]} ${errorClasses} ${widthClasses} ${disabledClasses} ${className}`.trim();

        return (
            <div className={fullWidth ? "w-full" : ""}>
                {label && (
                    <label className="label">
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
                    placeholder={placeholder}
                    disabled={disabled}
                    required={required}
                    readOnly
                />

                {helperText && (
                    <div className="label">
                        <span className="label-text-alt">{helperText}</span>
                    </div>
                )}
            </div>
        );
    }
);

DateRangePicker.displayName = "DateRangePicker";