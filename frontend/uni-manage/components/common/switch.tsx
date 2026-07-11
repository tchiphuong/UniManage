"use client";

import { Switch as HeroSwitch, Label } from "@heroui/react";

interface SwitchProps {
    checked: boolean;
    onChange: (checked: boolean) => void;
    label?: string;
    description?: string;
    disabled?: boolean;
    className?: string;
}

/**
 * Reusable Switch Component
 * Wrapper around HeroUI Switch with label support
 */
export function Switch({
    checked,
    onChange,
    label,
    description,
    disabled = false,
    className = "",
}: SwitchProps) {
    return (
        <div className={`flex items-start gap-3 ${className}`}>
            <HeroSwitch
                checked={checked}
                onChange={onChange}
                isDisabled={disabled}
                className="flex items-center"
            >
                <HeroSwitch.Control className="bg-default-200 data-[state=checked]:bg-primary relative h-6 w-11 rounded-full transition-colors">
                    <HeroSwitch.Thumb className="block h-5 w-5 translate-x-0.5 rounded-full bg-white shadow-sm transition-transform data-[state=checked]:translate-x-5" />
                </HeroSwitch.Control>
            </HeroSwitch>
            {(label || description) && (
                <div className="flex flex-col">
                    {label && (
                        <Label className="text-default-700 text-sm font-medium">
                            {label}
                        </Label>
                    )}
                    {description && (
                        <p className="text-default-500 mt-0.5 text-xs">
                            {description}
                        </p>
                    )}
                </div>
            )}
        </div>
    );
}
