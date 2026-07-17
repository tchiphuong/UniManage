import {
    Button as HeroButton,
    ButtonProps as HeroButtonProps,
} from "@heroui/react";
import { forwardRef } from "react";

export type ButtonProps = HeroButtonProps;

export const Button = forwardRef<HTMLButtonElement, ButtonProps>(
    (props, ref) => {
        return <HeroButton ref={ref} {...props} />;
    },
);

Button.displayName = "Button";
