"use client";

import { Modal as HeroModal } from "@heroui/react";
import { ReactNode } from "react";

export interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    title: ReactNode;
    children: ReactNode;
    footer?: ReactNode;
    size?: "xs" | "sm" | "md" | "lg" | "cover" | "full";
    scroll?: "inside" | "outside";
    backdrop?: "opaque" | "blur" | "transparent";
    className?: string;
}

/**
 * Modal wrapper chuẩn hóa với default props
 * Default: size="md", scroll="inside", backdrop="blur"
 */
export function Modal({
    isOpen,
    onClose,
    title,
    children,
    footer,
    size = "md",
    scroll = "inside",
    backdrop = "blur",
    className,
}: Readonly<ModalProps>) {
    return (
        <HeroModal>
            <HeroModal.Backdrop
                variant={backdrop}
                isOpen={isOpen}
                onOpenChange={(open) => {
                    if (!open) onClose();
                }}
            />
            <HeroModal.Container size={size} scroll={scroll}>
                <HeroModal.Dialog className={className}>
                    <HeroModal.Header>{title}</HeroModal.Header>
                    <HeroModal.Body>{children}</HeroModal.Body>
                    {footer && <HeroModal.Footer>{footer}</HeroModal.Footer>}
                    <HeroModal.CloseTrigger />
                </HeroModal.Dialog>
            </HeroModal.Container>
        </HeroModal>
    );
}

// Re-export sub-components for direct use if needed
export { HeroModal as HeroModalRoot };
