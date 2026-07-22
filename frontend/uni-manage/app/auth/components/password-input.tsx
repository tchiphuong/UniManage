"use client";

import { FieldError, Input, Label, TextField } from "@heroui/react";
import { Icon } from "@iconify/react";
import { useMemo, useState } from "react";

interface PasswordInputProps {
    label?: string;
    placeholder?: string;
    value: string;
    onChange: (value: string) => void;
    error?: string;
    showStrength?: boolean;
    required?: boolean;
    disabled?: boolean;
    name?: string;
    autoComplete?: string;
}

interface PasswordStrength {
    score: number; // 0-4
    label: string;
    color: string;
    percentage: number;
}

export function PasswordInput({
    label = "Mật khẩu",
    placeholder = "Nhập mật khẩu",
    value,
    onChange,
    error,
    showStrength = false,
    required = false,
    disabled = false,
    name = "password",
    autoComplete = "current-password",
}: PasswordInputProps) {
    const [isVisible, setIsVisible] = useState(false);

    const toggleVisibility = () => setIsVisible(!isVisible);

    // Calculate password strength
    const strength = useMemo((): PasswordStrength => {
        if (!value) {
            return {
                score: 0,
                label: "",
                color: "bg-danger-500",
                percentage: 0,
            };
        }

        let score = 0;
        const checks = {
            length: value.length >= 8,
            lowercase: /[a-z]/.test(value),
            uppercase: /[A-Z]/.test(value),
            number: /[0-9]/.test(value),
            special: /[^A-Za-z0-9]/.test(value),
        };

        // Length check (2 points)
        if (checks.length) score += 2;
        if (value.length >= 12) score += 1;

        // Character variety (1 point each)
        if (checks.lowercase) score += 1;
        if (checks.uppercase) score += 1;
        if (checks.number) score += 1;
        if (checks.special) score += 1;

        // Normalize to 0-4 scale
        const normalizedScore = Math.min(4, Math.floor(score / 2));

        const strengthMap: Record<
            number,
            { label: string; color: string; percentage: number }
        > = {
            0: { label: "Rất yếu", color: "bg-danger-500", percentage: 20 },
            1: { label: "Yếu", color: "bg-danger-500", percentage: 40 },
            2: { label: "Trung bình", color: "bg-warning-500", percentage: 60 },
            3: { label: "Mạnh", color: "bg-success-500", percentage: 80 },
            4: { label: "Rất mạnh", color: "bg-success-500", percentage: 100 },
        };

        return { score: normalizedScore, ...strengthMap[normalizedScore] };
    }, [value]);

    const getStrengthTextColor = () => {
        if (strength.score <= 1) return "text-danger-500";
        if (strength.score === 2) return "text-warning-500";
        return "text-success-500";
    };

    return (
        <div className="w-full space-y-2">
            <TextField
                className="flex flex-col gap-1.5"
                name={name}
                type={isVisible ? "text" : "password"}
                isRequired={required}
                isDisabled={disabled}
                validationBehavior="aria"
                value={value}
                onChange={onChange}
            >
                <Label className="text-default-700 text-sm font-medium">
                    {label}
                </Label>
                <div className="relative">
                    <Input
                        className="bg-default-100 hover:bg-default-200 focus:ring-primary-500 data-[invalid=true]:ring-danger-500 data-[invalid=true]:bg-danger-50 flex w-full rounded-lg border-none px-3 py-2 pr-10 transition-all outline-none focus:ring-2"
                        placeholder={placeholder}
                        autoComplete={autoComplete}
                    />
                    <button
                        className="absolute top-1/2 right-3 -translate-y-1/2 focus:outline-none"
                        type="button"
                        onClick={toggleVisibility}
                        aria-label={isVisible ? "Ẩn mật khẩu" : "Hiện mật khẩu"}
                    >
                        {isVisible ? (
                            <Icon
                                icon="solar:eye-closed-bold"
                                className="text-default-400 text-xl"
                                width={20}
                            />
                        ) : (
                            <Icon
                                icon="solar:eye-bold"
                                className="text-default-400 text-xl"
                                width={20}
                            />
                        )}
                    </button>
                </div>
                {error && (
                    <FieldError className="text-tiny text-danger-500">
                        {error}
                    </FieldError>
                )}
            </TextField>

            {showStrength && value && (
                <div className="space-y-1.5">
                    {/* Strength bar */}
                    <div className="bg-default-200 h-1.5 w-full overflow-hidden rounded-full">
                        <div
                            className={`h-full transition-all duration-300 ${strength.color}`}
                            style={{ width: `${strength.percentage}%` }}
                        />
                    </div>

                    {/* Strength label */}
                    <p className={`text-xs ${getStrengthTextColor()}`}>
                        Độ mạnh: {strength.label}
                    </p>
                </div>
            )}
        </div>
    );
}
