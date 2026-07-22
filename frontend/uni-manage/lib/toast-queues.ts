import { ToastQueue } from "@heroui/react";

/**
 * Shared queue for all toasts displayed at the same placement (e.g. top end).
 * Prevents UI overlapping by allowing HeroUI to stack toasts instead of rendering them on top of each other.
 */
export const appToastQueue = new ToastQueue({ maxVisibleToasts: 5 });
