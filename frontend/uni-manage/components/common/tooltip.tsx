"use client";

import { Tooltip as HeroTooltip, TooltipProps } from "@heroui/react";
import { forwardRef } from "react";

export type { TooltipProps };

export const Tooltip = forwardRef<HTMLDivElement, TooltipProps>((props, ref) => {
    return <HeroTooltip ref={ref} {...props} />;
});
Tooltip.displayName = "Tooltip";
