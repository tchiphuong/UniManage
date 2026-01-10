"use client";

import React from "react";
import { Controller, useFormContext } from "react-hook-form";
import { Input as HeroInput } from "@heroui/react";

interface FormInputProps extends React.ComponentProps<typeof HeroInput> {
    name: string;
    label?: string;
}

export function FormInput({ name, label, className, ...props }: FormInputProps) {
    const { control } = useFormContext();

    return (
        <Controller
            name={name}
            control={control}
            render={({ field, fieldState: { error } }) => (
                <HeroInput
                    {...field}
                    {...props}
                    label={label}
                    isInvalid={!!error}
                    errorMessage={error?.message}
                    className={className}
                    value={field.value || ""}
                    onChange={(e) => field.onChange(e.target.value)}
                />
            )}
        />
    );
}
