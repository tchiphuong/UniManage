import {
    Button as HeroButton,
    ButtonProps as HeroButtonProps,
} from "@heroui/react";
import React from "react";

export interface ButtonProps extends Omit<HeroButtonProps, "children"> {
    isIconOnlyMobile?: boolean;
    startContent?: React.ReactNode;
    children?: React.ReactNode;
}

export const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
    (
        { children, className = "", isIconOnlyMobile, startContent, ...props },
        ref,
    ) => {
        if (isIconOnlyMobile) {
            return (
                <HeroButton
                    ref={ref}
                    className={`sm:px-4 ${className}`}
                    {...props}
                >
                    <div className="flex items-center gap-2">
                        {/* Show icon always, but size might need adjustment if passed manually */}
                        {startContent ? (
                            <span className="flex items-center justify-center">
                                {startContent}
                            </span>
                        ) : null}
                        {/* Hide text on mobile */}
                        <span className="hidden sm:inline">{children}</span>
                    </div>
                </HeroButton>
            );
        }

        return (
            <HeroButton ref={ref} className={className} {...props}>
                {startContent ? (
                    <span className="flex items-center gap-2">
                        {startContent}
                        {children}
                    </span>
                ) : (
                    children
                )}
            </HeroButton>
        );
    },
);

Button.displayName = "Button";
