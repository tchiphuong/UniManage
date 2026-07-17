import {
    Input as HeroInput,
    InputProps as HeroInputProps,
} from "@heroui/react";
import { forwardRef } from "react";

export type InputProps = HeroInputProps;

export const Input = forwardRef<HTMLInputElement, InputProps>((props, ref) => {
    return <HeroInput ref={ref} {...props} />;
});

Input.displayName = "Input";
