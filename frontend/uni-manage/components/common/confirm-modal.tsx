"use client";

import { AlertDialog, Button } from "@heroui/react";
import { ReactNode } from "react";

interface ConfirmModalProps {
    isOpen: boolean;
    onClose: () => void;
    onConfirm: () => void;
    title?: string;
    message?: string | ReactNode;
    confirmLabel?: string;
    cancelLabel?: string;
    variant?: "default" | "danger";
    isLoading?: boolean;
}

/**
 * ConfirmModal Component - Wrapper around HeroUI AlertDialog
 * Simple confirmation dialog for user actions
 *
 * Usage:
 * <ConfirmModal
 *   isOpen={isOpen}
 *   onClose={() => setIsOpen(false)}
 *   onConfirm={handleDelete}
 *   title="Delete Item"
 *   message="Are you sure you want to delete this item?"
 *   variant="danger"
 * />
 */
export function ConfirmModal({
    isOpen,
    onClose,
    onConfirm,
    title = "Confirm Action",
    message = "Are you sure you want to proceed?",
    confirmLabel = "Confirm",
    cancelLabel = "Cancel",
    variant = "default",
    isLoading = false,
}: ConfirmModalProps) {
    const handleConfirm = () => {
        onConfirm();
        if (!isLoading) {
            onClose();
        }
    };

    return (
        <AlertDialog open={isOpen} onOpenChange={(open) => !open && onClose()}>
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
                            variant="light"
                            onPress={onClose}
                            isDisabled={isLoading}
                        >
                            {cancelLabel}
                        </Button>
                        <Button
                            color={variant === "danger" ? "danger" : "primary"}
                            onPress={handleConfirm}
                            isLoading={isLoading}
                        >
                            {confirmLabel}
                        </Button>
                    </AlertDialog.Footer>
                </AlertDialog.Dialog>
            </AlertDialog.Container>
        </AlertDialog>
    );
}
