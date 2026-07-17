"use client";

import { Pagination as HeroPagination, PaginationProps } from "@heroui/react";
import { forwardRef } from "react";

export type { PaginationProps };

export const Pagination = forwardRef<HTMLElement, PaginationProps>((props, ref) => {
    return <HeroPagination ref={ref} {...props} />;
});
Pagination.displayName = "Pagination";
