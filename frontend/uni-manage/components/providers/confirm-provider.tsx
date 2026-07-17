"use client";

import React, {
    createContext,
    useContext,
    useState,
    useCallback,
    ReactNode,
} from "react";
import { AlertDialog } from "@heroui/react";
import { Button } from "@/components/common";
// import { Icon } from "@iconify/react"; // Not needed if using AlertDialog.Icon
import { useTranslations } from "next-intl";

// 1. Definition of Confirm Options
export interface ConfirmOptions {
    title?: string;
    message: string;
    confirmLabel?: string;
    cancelLabel?: string;
    variant?: "danger" | "warning" | "info" | "success";
    icon?: string;
}

// 2. Context Definition
type ConfirmContextType = {
    confirm: (options: ConfirmOptions) => Promise<boolean>;
};

const ConfirmContext = createContext<ConfirmContextType | undefined>(undefined);

// 3. Provider Component
export function ConfirmProvider({ children }: Readonly<{ children: ReactNode }>) {
    const t = useTranslations("common.confirmDialog");
    const [isOpen, setIsOpen] = useState(false);
    const [options, setOptions] = useState<ConfirmOptions>({
        message: "",
        title: "",
        confirmLabel: "",
        cancelLabel: "",
        variant: "danger",
    });

    // Store the resolve function of the promise
    const [resolver, setResolver] = useState<((value: boolean) => void) | null>(
        null,
    );

    const confirm = useCallback((opts: ConfirmOptions) => {
        setOptions({
            title: opts.title,
            message: opts.message,
            confirmLabel: opts.confirmLabel,
            cancelLabel: opts.cancelLabel,
            variant: opts.variant || "danger",
            icon: opts.icon,
        });
        setIsOpen(true);

        return new Promise<boolean>((resolve) => {
            setResolver(() => resolve);
        });
    }, []);

    const handleConfirm = useCallback(() => {
        if (resolver) resolver(true);
        setIsOpen(false);
    }, [resolver]);

    const handleCancel = useCallback(() => {
        if (resolver) resolver(false);
        setIsOpen(false);
    }, [resolver]);

    const handleOpenChange = useCallback(
        (open: boolean) => {
            if (!open) handleCancel();
        },
        [handleCancel],
    );

    // Map variant to AlertDialog status
    const getStatus = () => {
        switch (options.variant) {
            case "danger":
                return "danger";
            case "warning":
                return "warning";
            case "success":
                return "success";
            case "info":
                return "accent";
            default:
                return "danger";
        }
    };

    const getButtonVariant = () => {
        if (options.variant === "danger") return "danger";
        return "primary";
    };

    return (
        <ConfirmContext.Provider value={{ confirm }}>
            {children}
            <AlertDialog isOpen={isOpen} onOpenChange={handleOpenChange}>
                <AlertDialog.Backdrop />
                <AlertDialog.Container>
                    <AlertDialog.Dialog>
                        <AlertDialog.Header>
                            <AlertDialog.Icon status={getStatus()} />
                            <AlertDialog.Heading>
                                {options.title || t("lbl.title")}
                            </AlertDialog.Heading>
                        </AlertDialog.Header>
                        <AlertDialog.Body>
                            <p className="text-default-500">
                                {options.message || t("msg.message")}
                            </p>
                        </AlertDialog.Body>
                        <AlertDialog.Footer>
                            <Button variant="tertiary" onPress={handleCancel}>
                                {options.cancelLabel || t("btn.cancel")}
                            </Button>
                            <Button variant={getButtonVariant()} onPress={handleConfirm}>
                                {options.confirmLabel || t("btn.confirm")}
                            </Button>
                        </AlertDialog.Footer>
                    </AlertDialog.Dialog>
                </AlertDialog.Container>
            </AlertDialog>
        </ConfirmContext.Provider>
    );
}

// 4. Custom Hook
export function useConfirm() {
    const context = useContext(ConfirmContext);
    if (!context) {
        throw new Error("useConfirm must be used within a ConfirmProvider");
    }
    return context.confirm;
}
