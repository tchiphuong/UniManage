"use client";

import { appToastQueue } from "@/lib/toast-queues";
import { useTranslations } from "next-intl";
import { useMemo } from "react";

// Optional extra configuration you can pass to the toast
type ToastOptions = {
    title?: string;
    [key: string]: any;
};

export const useAppToast = () => {
    const t = useTranslations("common.error.msg");
    const tSuccess = useTranslations("common.success.msg");

    return useMemo(
        () => ({
            success: (message?: string, options?: ToastOptions) =>
                appToastQueue.add({
                    description: message || tSuccess("saved"),
                    variant: "success",
                    ...options,
                }),

            error: (message?: string, options?: ToastOptions) =>
                appToastQueue.add({
                    description: message || t("general"),
                    variant: "danger",
                    ...options,
                }),

            warning: (message?: string, options?: ToastOptions) =>
                appToastQueue.add({
                    description: message || t("general"),
                    variant: "warning",
                    ...options,
                }),

            info: (message?: string, options?: ToastOptions) =>
                appToastQueue.add({
                    description: message || t("general"),
                    variant: "default",
                    ...options,
                }),

            default: (message: string, options?: ToastOptions) =>
                appToastQueue.add({
                    description: message,
                    ...options,
                }),

            clear: () => {
                appToastQueue.clear();
            },
        }),
        [t, tSuccess],
    );
};
