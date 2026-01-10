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
export function ColorPicker({ label, value, onChange, className = "" }: ColorPickerProps) {
    return (
        <div className={className}>
            <Label className="text-sm font-medium mb-2 block">{label}</Label>
            <div className="flex gap-2 items-center">
                <input
                    type="color"
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className="w-12 h-12 rounded-lg cursor-pointer border-2 border-default-200"
                />
                <Input
                    value={value}
                    onChange={(e) => onChange(e.target.value)}
                    className="flex-1 px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all"
                    placeholder="#000000"
                />
            </div>
        </div>
    );
}
