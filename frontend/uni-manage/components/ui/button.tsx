import {
    Button as HeroButton,
    ButtonProps as HeroButtonProps,
} from "@heroui/react";
import { forwardRef } from "react";

export interface ButtonProps extends HeroButtonProps {}

export const Button = forwardRef<HTMLButtonElement, ButtonProps>(
    (props, ref) => {
        return (
            <HeroButton ref={ref} radius={props.radius || "sm"} {...props} />
        );
    },
);

Button.displayName = "Button";
