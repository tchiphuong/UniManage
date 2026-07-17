"use client";

import { Chip as HeroChip, ChipProps } from "@heroui/react";
import { forwardRef } from "react";

export type { ChipProps };

export const Chip = forwardRef<HTMLDivElement, ChipProps>((props, ref) => {
    return <HeroChip ref={ref} {...props} />;
});
Chip.displayName = "Chip";
