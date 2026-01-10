"use client";

import { useLocale, useTranslations } from "next-intl";
import { usePathname, useSearchParams } from "next/navigation";
import { type Locale } from "@/i18n/config";
import Cookies from "js-cookie";

interface LanguageSwitcherProps {
    className?: string;
}

const languages = [
    {
        code: "vi" as Locale,
        nameKey: "languageVietnamese",
        flag: "🇻🇳",
    },
    {
        code: "en" as Locale,
        nameKey: "languageEnglish",
        flag: "🇺🇸",
    },
];

export function LanguageSwitcher({ className = "" }: LanguageSwitcherProps) {
    const t = useTranslations("common");
    const locale = useLocale() as Locale;

    const pathname = usePathname();
    const searchParams = useSearchParams();

    const handleSelectionChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const newLocale = e.target.value as Locale;
        if (!newLocale || newLocale === locale) return;

        // Set cookie so server-side i18n can pick the selected locale on next request
        Cookies.set("locale", newLocale, { expires: 365 });

        // Reload the page so server picks up new locale from cookie
        // Use location.assign to perform a full reload
        if (typeof window !== "undefined") {
            const params = searchParams.toString();
            const fullPath = params ? `${pathname}?${params}` : pathname;
            window.location.assign(fullPath);
        }
    };

    return (
        <div className={`relative ${className}`}>
            <select
                value={locale}
                onChange={handleSelectionChange}
                className="appearance-none w-full bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 text-gray-700 dark:text-gray-200 py-2 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                aria-label={t("languageSelect")}
            >
                {languages.map((lang) => (
                    <option key={lang.code} value={lang.code}>
                        {lang.flag} {t(lang.nameKey)}
                    </option>
                ))}
            </select>
            <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700 dark:text-gray-200">
                <svg className="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                    <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                </svg>
            </div>
        </div>
    );
}
