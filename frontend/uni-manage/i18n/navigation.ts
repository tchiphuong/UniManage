import { createSharedPathnamesNavigation } from "next-intl/navigation";
import { locales } from "./config";

/**
 * Navigation helpers với i18n support
 * Sử dụng: import { Link, redirect, usePathname, useRouter } from '@/i18n/navigation'
 */
export const { Link, redirect, usePathname, useRouter } = createSharedPathnamesNavigation({
    locales,
});
