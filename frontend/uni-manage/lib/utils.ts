/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ClassValue } from "clsx";
import clsx from "clsx";

/**
 * Utility to merge Tailwind CSS classes
 */
export function cn(...inputs: ClassValue[]) {
    return clsx(inputs);
}

/**
 * Format Vietnamese currency
 */
export function formatCurrency(amount: number): string {
    return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
    }).format(amount);
}

/**
 * Format date
 */
export function formatDate(
    date: string | Date,
    format: "short" | "long" = "short",
): string {
    const d = typeof date === "string" ? new Date(date) : date;

    if (format === "short") {
        return d.toLocaleDateString("vi-VN");
    }

    return d.toLocaleDateString("vi-VN", {
        year: "numeric",
        month: "long",
        day: "numeric",
    });
}

/**
 * Validate email
 */
export function isValidEmail(email: string): boolean {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
}

/**
 * Validate Vietnamese phone number
 */
export function isValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^(0|\+84)[35789]\d{8}$/;
    return phoneRegex.test(phone.replace(/\s/g, ""));
}

/**
 * Debounce function
 */
export function debounce<T extends (...args: any[]) => any>(
    func: T,
    wait: number,
): (...args: Parameters<T>) => void {
    let timeout: NodeJS.Timeout;
    return (...args: Parameters<T>) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => func(...args), wait);
    };
}

/**
 * Common utility to extract meaningful error messages from the backend API responses
 */
export function handleApiError(
    err: any,
    defaultMessage = "An error occurred",
): string {
    // Handle both Axios errors (err.response.data) and direct API responses (err)
    const responseData = err?.response?.data || err;

    if (responseData) {
        if (responseData.errors && responseData.errors.length > 0) {
            const firstError = responseData.errors[0];
            if (typeof firstError === "string") {
                return firstError;
            } else if (
                firstError &&
                typeof firstError === "object" &&
                "messages" in firstError &&
                firstError.messages.length > 0
            ) {
                return firstError.messages[0];
            }
        }
        if (responseData.message) {
            return responseData.message;
        }
    }

    return err?.message || defaultMessage;
}

/**
 * Decode JWT token and extract username
 * Shared utility for hooks that need user info from token
 */
export function getTokenUsername(
    token: string | null | undefined,
): string | null {
    try {
        if (!token) return null;
        const payload = JSON.parse(atob(token.split(".")[1]));
        return payload.sub || payload.username || null;
    } catch {
        return null;
    }
}
