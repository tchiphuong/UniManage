"use client";

import { Input, Label } from "@heroui/react";

interface ColorPickerProps {
    label: string;
    value: string;
    onChange: (value: string) => void;
    className?: string;
}

/**
 * Reusable Color Picker Component
 * Combines native color input with text input for hex values
 */
export function ColorPicker({
    label,
    value,
    onChange,
    className = "",
}: ColorPickerProps) {
    return (
        <div className={className}>
            <Label className="mb-2 block text-sm font-medium">{label}</Label>
            <div className="flex items-center gap-2">
                <input
                    type="color"
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className="border-default-200 h-12 w-12 cursor-pointer rounded-lg border-2"
                />
                <Input
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className="bg-default-100 hover:bg-default-200 focus:ring-primary-500 flex-1 rounded-lg px-3 py-2 transition-all outline-none focus:ring-2"
                    placeholder="#000000"
                />
            </div>
        </div>
    );
}
