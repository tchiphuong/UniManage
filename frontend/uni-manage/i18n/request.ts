import { getRequestConfig } from "next-intl/server";
import { notFound } from "next/navigation";
import { cookies } from "next/headers";
import { locales, defaultLocale, Locale } from "./config";

/**
 * Config cho next-intl
 */
export default getRequestConfig(async ({ requestLocale }) => {
    // Try requestLocale (Accept-Language or framework detection)
    let locale = await requestLocale;

    // Fall back to cookie `locale` if present (LanguageSwitcher sets it)
    try {
        const c = (await cookies()).get("locale");
        if (!locale && c?.value) {
            locale = c.value;
        }
    } catch (e) {
        // ignore (safe fallback)
    }

    if (!locale) {
        locale = defaultLocale;
    }

    // Validate locale
    if (!locales.includes(locale as Locale)) {
        notFound();
    }

    return {
        messages: (await import(`../messages/${locale}.json`)).default,
        timeZone: "Asia/Ho_Chi_Minh",
        now: new Date(),
    };
});
