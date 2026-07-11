"use client";

import { useLocale, useTranslations } from "next-intl";
import { usePathname, useSearchParams } from "next/navigation";
import type { Locale } from "@/i18n/config";
import Cookies from "js-cookie";
import { Select, ListBox } from "@heroui/react";
import type { Key } from "react";
import { Icon } from "@iconify/react";

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

export function LanguageSwitcher({
    className = "",
}: Readonly<LanguageSwitcherProps>) {
    const t = useTranslations("common.global.lbl");
    const locale = useLocale() as Locale;

    const pathname = usePathname();
    const searchParams = useSearchParams();

    const handleSelectionChange = (key: Key | Key[] | null) => {
        const newLocale = key as Locale;
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
            <Select
                aria-label={t("languageSelect")}
                value={locale}
                onChange={handleSelectionChange}
                className="w-48"
            >
                <Select.Trigger>
                    <Select.Value>
                        {({ defaultChildren, isPlaceholder, state }) => {
                            // Cập nhật để khi đóng Select vẫn hiển thị cờ bên ngoài
                            const selectedItem = state.selectedItems[0];
                            if (isPlaceholder || !selectedItem)
                                return defaultChildren;
                            return (
                                <div className="flex items-center gap-2">
                                    <Icon
                                        icon={`circle-flags:${selectedItem.key === "vi" ? "vn" : "us"}`}
                                        className="text-xl"
                                    />
                                    <span>{selectedItem.textValue}</span>
                                </div>
                            );
                        }}
                    </Select.Value>
                    <Select.Indicator />
                </Select.Trigger>
                <Select.Popover>
                    <ListBox>
                        <ListBox.Item
                            key="vi"
                            id="vi"
                            textValue={t("languageVietnamese")}
                        >
                            <div className="flex items-center gap-2">
                                <Icon
                                    icon="circle-flags:vn"
                                    className="text-xl"
                                />
                                <span>{t("languageVietnamese")}</span>
                            </div>
                            <ListBox.ItemIndicator />
                        </ListBox.Item>
                        <ListBox.Item
                            key="en"
                            id="en"
                            textValue={t("languageEnglish")}
                        >
                            <div className="flex items-center gap-2">
                                <Icon
                                    icon="circle-flags:us"
                                    className="text-xl"
                                />
                                <span>{t("languageEnglish")}</span>
                            </div>
                            <ListBox.ItemIndicator />
                        </ListBox.Item>
                    </ListBox>
                </Select.Popover>
            </Select>
        </div>
    );
}
