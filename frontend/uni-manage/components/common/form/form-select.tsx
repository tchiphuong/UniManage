"use client";

import React from "react";
import { Controller, useFormContext } from "react-hook-form";
import { Select, SelectItem } from "@heroui/react";

interface Option {
    label: string;
    value: string | number;
}

interface FormSelectProps extends Omit<React.ComponentProps<typeof Select>, "children"> {
    name: string;
    label?: string;
    options: Option[];
}

export function FormSelect({ name, label, options, className, ...props }: FormSelectProps) {
    const { control } = useFormContext();

    return (
        <Controller
            name={name}
            control={control}
            render={({ field, fieldState: { error } }) => (
                <Select
                    {...field}
                    {...props}
                    label={label}
                    selectedKeys={field.value ? [String(field.value)] : []}
                    onChange={(e) => field.onChange(e.target.value)}
                    isInvalid={!!error}
                    errorMessage={error?.message}
                    className={className}
                >
                    {options.map((option) => (
                        <SelectItem key={option.value} value={option.value}>
                            {option.label}
                        </SelectItem>
                    ))}
                </Select>
            )}
        />
    );
}
