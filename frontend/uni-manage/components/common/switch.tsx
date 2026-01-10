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
                <HeroSwitch.Control className="w-11 h-6 bg-default-200 rounded-full relative transition-colors data-[state=checked]:bg-primary">
                    <HeroSwitch.Thumb className="block w-5 h-5 bg-white rounded-full shadow-sm transition-transform data-[state=checked]:translate-x-5 translate-x-0.5" />
                </HeroSwitch.Control>
            </HeroSwitch>
            {(label || description) && (
                <div className="flex flex-col">
                    {label && <Label className="text-sm font-medium text-default-700">{label}</Label>}
                    {description && <p className="text-xs text-default-500 mt-0.5">{description}</p>}
                </div>
            )}
        </div>
    );
}
