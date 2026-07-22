"use client";

import { CheckIcon, XMarkIcon } from "@heroicons/react/24/outline";
import { AlertDialog } from "@heroui/react";
import { ReactNode } from "react";

import { Button } from "./button";

export interface ConfirmModalProps {
    isOpen: boolean;
    onClose: () => void;
    onConfirm: () => void;
    title?: string;
    message?: string | ReactNode;
    confirmLabel?: string;
    confirmText?: string;
    cancelLabel?: string;
    variant?: "default" | "danger";
    isLoading?: boolean;
}

/**
 * ConfirmModal Component - Wrapper around HeroUI AlertDialog
 * Confirmation dialog for user actions
 */
export function ConfirmModal({
    isOpen,
    onClose,
    onConfirm,
    title = "Confirm Action",
    message = "Are you sure you want to proceed?",
    confirmLabel,
    confirmText,
    cancelLabel = "Cancel",
    variant = "default",
    isLoading = false,
}: Readonly<ConfirmModalProps>) {
    // confirmText is an alias for confirmLabel (backward compatibility)
    const finalConfirmLabel = confirmLabel || confirmText || "Confirm";

    const handleConfirm = () => {
        onConfirm();
        if (!isLoading) {
            onClose();
        }
    };

    return (
        <AlertDialog
            isOpen={isOpen}
            onOpenChange={(open) => !open && onClose()}
        >
            <AlertDialog.Backdrop className="fixed inset-0 z-50 bg-black/50" />
            <AlertDialog.Container className="fixed inset-0 z-50 flex items-center justify-center p-4">
                <AlertDialog.Dialog className="w-full max-w-md rounded-lg bg-white shadow-xl dark:bg-gray-800">
                    <AlertDialog.Header className="p-6 pb-4">
                        <AlertDialog.Heading className="text-foreground text-xl font-semibold">
                            {title}
                        </AlertDialog.Heading>
                        <AlertDialog.CloseTrigger
                            className="text-default-400 hover:text-default-600 absolute top-4 right-4"
                            onClick={onClose}
                        />
                    </AlertDialog.Header>

                    <AlertDialog.Body className="px-6 pb-4">
                        <p className="text-default-600">{message}</p>
                    </AlertDialog.Body>

                    <AlertDialog.Footer className="flex justify-end gap-3 p-6 pt-4">
                        <Button
                            variant="ghost"
                            className="text-danger-500"
                            onPress={onClose}
                            isDisabled={isLoading}
                        >
                            <XMarkIcon className="h-5 w-5" />
                            {cancelLabel}
                        </Button>
                        <Button
                            variant={
                                variant === "danger" ? "danger" : "primary"
                            }
                            onPress={handleConfirm}
                            isPending={isLoading}
                        >
                            {!isLoading && <CheckIcon className="h-5 w-5" />}
                            {finalConfirmLabel}
                        </Button>
                    </AlertDialog.Footer>
                </AlertDialog.Dialog>
            </AlertDialog.Container>
        </AlertDialog>
    );
}
