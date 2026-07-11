import {
    Input as HeroInput,
    InputProps as HeroInputProps,
} from "@heroui/react";
import { forwardRef } from "react";

export interface InputProps extends HeroInputProps {}

export const Input = forwardRef<HTMLInputElement, InputProps>((props, ref) => {
    return (
        <HeroInput
            ref={ref}
            variant={props.variant || "bordered"}
            radius={props.radius || "sm"}
            {...props}
        />
    );
});

Input.displayName = "Input";
