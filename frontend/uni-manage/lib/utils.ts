/* eslint-disable @typescript-eslint/no-explicit-any */
import type { ClassValue } from "clsx";
import clsx from "clsx";

/**
 * Utility để merge Tailwind CSS classes
 */
export function cn(...inputs: ClassValue[]) {
    return clsx(inputs);
}

/**
 * Format số tiền Việt Nam
 */
export function formatCurrency(amount: number): string {
    return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
    }).format(amount);
}

/**
 * Format ngày tháng
 */
export function formatDate(date: string | Date, format: "short" | "long" = "short"): string {
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
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

/**
 * Validate phone number Việt Nam
 */
export function isValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^(0|\+84)[3|5|7|8|9][0-9]{8}$/;
    return phoneRegex.test(phone.replace(/\s/g, ""));
}

/**
 * Debounce function
 */
export function debounce<T extends (...args: any[]) => any>(
    func: T,
    wait: number
): (...args: Parameters<T>) => void {
    let timeout: NodeJS.Timeout;
    return (...args: Parameters<T>) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => func(...args), wait);
    };
}
