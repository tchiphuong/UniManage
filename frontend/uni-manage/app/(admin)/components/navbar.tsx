"use client";

import {
    ChevronDownIcon,
    MagnifyingGlassIcon,
    StarIcon,
    XMarkIcon,
} from "@heroicons/react/24/outline";
import { StarIcon as StarIconSolid } from "@heroicons/react/24/solid";
import { Dropdown } from "@heroui/react";
import Cookies from "js-cookie";
import { useLocale, useTranslations } from "next-intl";
import React, {
    useCallback,
    useDeferredValue,
    useEffect,
    useRef,
    useState,
    useMemo,
} from "react";

import { useNavbarContext } from "@/contexts/navbar-context";
import { Link, usePathname, useRouter } from "@/i18n/navigation";
import { useNavbar } from "@/services/navbar.service";
import { NavbarItem } from "@/types";
import { GroupIcon } from "@/components/common";

// Menu data with translation keys - organized by groups
// Menu data is now fetched from API via useMenu hook

// Badge counts (mock data - in real app, fetch from API)
const badgeCounts: Record<string, number> = {
    todos: 5,
    comments: 12,
    projects: 3,
};

interface NavbarItemProps {
    item: NavbarItem;
    onNavigate: (link: string) => void;
    currentPath: string;
    isFavorite: boolean;
    onToggleFavorite: (id: string) => void;
    badgeCount?: number;
}

const FavoriteButton = React.memo(
    ({
        id,
        isFavorite,
        isActive,
        onToggleFavorite,
    }: {
        id: string;
        isFavorite: boolean;
        isActive: boolean;
        onToggleFavorite: (id: string) => void;
    }) => (
        <button
            type="button"
            onClick={(e) => {
                e.preventDefault();
                e.stopPropagation();
                onToggleFavorite(id);
            }}
            className="rounded-full p-1 opacity-0 transition-opacity group-hover:opacity-100 hover:bg-white/20 focus:outline-none"
        >
            {isFavorite ? (
                <StarIconSolid className="h-4 w-4 text-yellow-500" />
            ) : (
                <StarIcon
                    className={`h-4 w-4 ${isActive ? "text-white/70 hover:text-yellow-300" : "text-gray-400 hover:text-yellow-500"}`}
                />
            )}
        </button>
    ),
);

const NavbarItemComponent = React.memo(function NavbarItemComponent({
    item,
    currentPath,
    isFavorite,
    onToggleFavorite,
    onNavigate,
    badgeCount,
}: NavbarItemProps) {
    const t = useTranslations();
    const [isOpen, setIsOpen] = useState(false);
    const hasChildren = item.children && item.children.length > 0;

    const isActive =
        item.link === currentPath ||
        (hasChildren &&
            item.children?.some((child) => child.link === currentPath));

    useEffect(() => {
        if (isActive && hasChildren) {
            // eslint-disable-next-line react-hooks/set-state-in-effect
            setIsOpen(true);
        }
    }, [currentPath, hasChildren, isActive]);

    const handleExpandToggle = (e: React.MouseEvent) => {
        if (hasChildren) {
            e.preventDefault();
            setIsOpen(!isOpen);
        }
    };

    const commonClasses = `mx-2 flex flex-1 cursor-pointer items-center justify-between rounded-full p-1.5 transition-all duration-200 ${
        isActive
            ? "bg-blue-500 text-white shadow-md shadow-blue-500/25"
            : "text-gray-700 hover:bg-gray-100 dark:text-zinc-300 dark:hover:bg-zinc-700/50"
    }`;
    const content = (
        <>
            <span className="flex flex-1 items-center gap-3 overflow-hidden">
                {item.icon && (
                    <span
                        className={`flex shrink-0 items-center justify-center rounded-full transition-colors ${
                            isActive
                                ? "bg-white/20 p-1"
                                : "bg-transparent p-1 group-hover:bg-blue-100 dark:group-hover:bg-blue-900/30"
                        }`}
                    >
                        <GroupIcon
                            resourceKey={item.title.replace("common.", "")}
                            className={`h-6 w-6 transition-all duration-200 ${
                                isActive
                                    ? "text-white drop-shadow-md"
                                    : "text-gray-500 group-hover:text-blue-600 dark:text-gray-400 dark:group-hover:text-blue-400"
                            }`}
                        />
                    </span>
                )}
                <span className="flex-1 truncate text-left text-sm font-medium">
                    {t(item.title)}
                </span>
            </span>

            <span className="flex shrink-0 items-center gap-1">
                {/* Badge */}
                {badgeCount !== undefined && badgeCount > 0 && (
                    <span className="rounded-full bg-red-500 px-1.5 py-0.5 text-xs font-bold text-white">
                        {badgeCount > 99 ? "99+" : badgeCount}
                    </span>
                )}

                {/* Keyboard Shortcut */}
                {item.shortcut && !hasChildren && (
                    <kbd
                        className={`hidden rounded-md border px-1.5 py-0.5 text-[10px] font-semibold sm:inline ${
                            isActive
                                ? "border-white/30 bg-white/20 text-white"
                                : "border-zinc-300 bg-gray-100 text-gray-500 dark:border-zinc-500 dark:bg-zinc-600 dark:text-zinc-300"
                        }`}
                    >
                        âŒ˜ + {item.shortcut}
                    </kbd>
                )}

                {hasChildren && (
                    <ChevronDownIcon
                        className={`mx-1 h-4 w-4 transition-transform duration-200 ${isOpen ? "rotate-180" : ""}`}
                    />
                )}
            </span>
        </>
    );

    return (
        <li className="relative">
            <div className="group relative flex items-center">
                {hasChildren ? (
                    <button
                        type="button"
                        onClick={handleExpandToggle}
                        className={`${commonClasses} flex-1 text-left`}
                    >
                        {content}
                    </button>
                ) : (
                    <Link
                        href={item.link || "#"}
                        className={`${commonClasses} flex-1`}
                        onClick={() => onNavigate(item.link || "#")}
                    >
                        {content}
                    </Link>
                )}

                {/* Favorite Star - Sibling to avoid DOM nesting errors */}
                {item.id && !hasChildren && (
                    <div className="absolute right-4 z-10 flex items-center">
                        <FavoriteButton
                            id={item.id}
                            isFavorite={isFavorite}
                            isActive={!!isActive}
                            onToggleFavorite={onToggleFavorite}
                        />
                    </div>
                )}
            </div>

            {hasChildren && isOpen && (
                <ul className="mt-2 ml-6 space-y-0.5 border-l-2 border-zinc-200 dark:border-zinc-700">
                    {item.children?.map((subItem) => (
                        <li key={subItem.id || subItem.title}>
                            <Link
                                href={subItem.link || "#"}
                                onClick={() => onNavigate(subItem.link || "#")}
                                className={`mx-2 flex items-center rounded-full py-2 pr-3 pl-4 text-left text-sm transition-all duration-200 ${
                                    subItem.link === currentPath
                                        ? "bg-blue-500 text-white shadow-md shadow-blue-500/25"
                                        : "text-gray-500 hover:bg-gray-100 hover:text-gray-900 dark:text-zinc-400 dark:hover:bg-zinc-700/50 dark:hover:text-white"
                                }`}
                            >
                                <span className="truncate font-medium">
                                    {t(subItem.title)}
                                </span>
                            </Link>
                        </li>
                    ))}
                </ul>
            )}
        </li>
    );
});

import { CN, JP, KR, TH, US, VN } from "country-flag-icons/react/3x2";

const languages = [
    { code: "en", name: "English", Flag: US },
    { code: "vi", name: "Tiáº¿ng Viá»‡t", Flag: VN },
    { code: "ja", name: "æ—¥æœ¬èªž", Flag: JP },
    { code: "zh", name: "ä¸­æ–‡", Flag: CN },
    { code: "ko", name: "í•œêµ­ì–´", Flag: KR },
    { code: "th", name: "à¹„à¸—à¸¢", Flag: TH },
];

const FAVORITES_KEY = "sidebar-favorites";

// Main Navbar Component
export function Navbar() {
    const t = useTranslations();
    const { navbarOpen, closeNavbar } = useNavbarContext();
    const { menuData, isLoading } = useNavbar();
    const router = useRouter();
    const pathname = usePathname();
    const locale = useLocale();

    const [langOpen, setLangOpen] = useState(false);
    const [searchQuery, setSearchQuery] = useState("");
    const deferredSearchQuery = useDeferredValue(searchQuery);
    const [favorites, setFavorites] = useState<string[]>([]);

    // Generate stable keys for skeleton loading
    const skeletonKeys = useMemo(
        () => Array.from({ length: 10 }).map(() => crypto.randomUUID()),
        [],
    );

    const handleNavigate = useCallback(
        (_path: string) => {
            // Navigation is handled by Link, but we might want to close sidebar on mobile
            if (window.innerWidth < 1024) {
                closeNavbar();
            }
        },
        [closeNavbar],
    );

    // Load favorites from localStorage
    useEffect(() => {
        const saved = localStorage.getItem(FAVORITES_KEY);
        if (saved) {
            // eslint-disable-next-line react-hooks/set-state-in-effect
            setFavorites(JSON.parse(saved));
        }
    }, []);

    // Save favorites to localStorage
    const toggleFavorite = useCallback((id: string) => {
        setFavorites((prev) => {
            const newFavorites = prev.includes(id)
                ? prev.filter((f) => f !== id)
                : [...prev, id];
            localStorage.setItem(FAVORITES_KEY, JSON.stringify(newFavorites));
            return newFavorites;
        });
    }, []);

    // Language dropdown ref and click-outside handler
    const langRef = useRef<HTMLDivElement>(null);
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (
                langRef.current &&
                !langRef.current.contains(event.target as Node)
            ) {
                setLangOpen(false);
            }
        };
        if (langOpen) {
            document.addEventListener("mousedown", handleClickOutside);
        }
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [langOpen]);

    const currentLang =
        languages.find((l) => l.code === locale) || languages[0];

    // Filter menu items based on search using deferred query
    const filteredMenu = menuData.filter((item) => {
        if (!deferredSearchQuery) return true;
        const title = t(item.title).toLowerCase();
        const query = deferredSearchQuery.toLowerCase();
        return (
            title.includes(query) ||
            item.children?.some((child) =>
                t(child.title).toLowerCase().includes(query),
            )
        );
    });

    // Separate favorites and regular items
    const favoriteItems = filteredMenu.filter(
        (item) => item.id && favorites.includes(item.id),
    );
    const regularItems = filteredMenu.filter(
        (item) => !item.id || !favorites.includes(item.id),
    );

    // Keyboard shortcuts handler
    useEffect(() => {
        const handleKeyDown = (e: KeyboardEvent) => {
            // Check for Ctrl/Cmd + number
            if ((e.ctrlKey || e.metaKey) && /^\d$/.test(e.key)) {
                e.preventDefault();
                const item = menuData.find((m) => m.shortcut === e.key);
                if (item) {
                    if (item.link) {
                        handleNavigate(item.link);
                    } else if (item.children?.[0]?.link) {
                        handleNavigate(item.children[0].link);
                    }
                }
            }
        };

        window.addEventListener("keydown", handleKeyDown);
        return () => window.removeEventListener("keydown", handleKeyDown);
    }, [handleNavigate, menuData]);

    return (
        <aside
            className={`sidebar-transition fixed top-16 left-0 z-40 flex h-[calc(100vh-4rem)] flex-col overflow-hidden bg-white shadow-lg transition-all duration-300 ease-in-out lg:relative lg:top-0 lg:m-3 lg:h-[calc(100vh-5.5rem)] lg:rounded-xl lg:border lg:border-zinc-200 dark:bg-zinc-800 dark:lg:border-zinc-600 ${
                navbarOpen
                    ? "w-full translate-x-0 opacity-100 lg:w-64"
                    : "-translate-x-full opacity-0 lg:w-0 lg:translate-x-0"
            }`}
        >
            {/* Search Box */}
            {/* Search Box */}
            <div className="dark:bg-content2 border-b border-zinc-200 p-3 dark:border-zinc-700">
                <div className="relative">
                    <MagnifyingGlassIcon className="absolute top-1/2 left-3 h-4 w-4 -translate-y-1/2 text-gray-400" />
                    <input
                        type="text"
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                        placeholder={t("common.global.lbl.search") + "..."}
                        className="w-full rounded-full border border-zinc-200 bg-gray-50 py-2 pr-8 pl-9 text-sm transition-colors focus:border-blue-500 focus:bg-white focus:outline-none dark:border-zinc-600 dark:bg-zinc-700 dark:text-white dark:focus:border-blue-500"
                    />
                    {searchQuery && (
                        <button
                            onClick={() => setSearchQuery("")}
                            className="absolute top-1/2 right-2 -translate-y-1/2 rounded-full p-1 hover:bg-gray-200 dark:hover:bg-zinc-600"
                        >
                            <XMarkIcon className="h-4 w-4 text-gray-400" />
                        </button>
                    )}
                </div>
            </div>

            <nav className="flex-1 overflow-x-hidden overflow-y-auto py-4">
                {isLoading ? (
                    <div className="space-y-2 p-4">
                        {skeletonKeys.map((key) => (
                            <div
                                key={key}
                                className="h-10 w-full animate-pulse rounded-lg bg-gray-200 dark:bg-zinc-700"
                            ></div>
                        ))}
                    </div>
                ) : (
                    <>
                        {/* Favorites Section */}
                        {favoriteItems.length > 0 && (
                            <div className="mb-4">
                                <div className="mb-2 flex items-center gap-2 px-4">
                                    <StarIconSolid className="h-4 w-4 text-yellow-500" />
                                    <span className="text-xs font-semibold tracking-wider text-gray-400 uppercase dark:text-gray-500">
                                        {t("common.global.lbl.favorites")}
                                    </span>
                                </div>
                                <ul className="space-y-1">
                                    {favoriteItems.map((item) => (
                                        <NavbarItemComponent
                                            key={`fav-${item.id || item.title}`}
                                            item={item}
                                            onNavigate={() =>
                                                handleNavigate(item.link || "#")
                                            }
                                            currentPath={pathname}
                                            isFavorite={true}
                                            onToggleFavorite={toggleFavorite}
                                            badgeCount={
                                                item.id
                                                    ? badgeCounts[item.id]
                                                    : undefined
                                            }
                                        />
                                    ))}
                                </ul>
                                <div className="mx-4 my-3 border-t border-zinc-200 dark:border-zinc-700"></div>
                            </div>
                        )}

                        {/* Menu Items - Rendered from API tree */}
                        {regularItems.map((item) => {
                            // Root items with children â†’ section with header
                            if (item.children && item.children.length > 0) {
                                return (
                                    <div
                                        key={item.id || item.title}
                                        className="mb-2"
                                    >
                                        {/* Section Header */}
                                        <div className="sticky -top-4 z-10 mb-1 border-b border-zinc-100 bg-white px-4 pb-1 dark:border-zinc-700 dark:bg-zinc-800">
                                            <span className="text-[10px] font-bold tracking-widest text-gray-400 uppercase dark:text-gray-500">
                                                {t(item.title)}
                                            </span>
                                        </div>
                                        {/* Section Items */}
                                        <ul className="space-y-0.5">
                                            {item.children.map((child) => (
                                                <NavbarItemComponent
                                                    key={`${item.id}-${child.id || child.title}`}
                                                    item={child}
                                                    onNavigate={() =>
                                                        handleNavigate(
                                                            child.link || "#",
                                                        )
                                                    }
                                                    currentPath={pathname}
                                                    isFavorite={
                                                        child.id
                                                            ? favorites.includes(
                                                                  child.id,
                                                              )
                                                            : false
                                                    }
                                                    onToggleFavorite={
                                                        toggleFavorite
                                                    }
                                                    badgeCount={
                                                        child.id
                                                            ? badgeCounts[
                                                                  child.id
                                                              ]
                                                            : undefined
                                                    }
                                                />
                                            ))}
                                        </ul>
                                    </div>
                                );
                            }

                            // Root items without children â†’ standalone menu item
                            return (
                                <NavbarItemComponent
                                    key={item.id || item.title}
                                    item={item}
                                    onNavigate={() =>
                                        handleNavigate(item.link || "#")
                                    }
                                    currentPath={pathname}
                                    isFavorite={
                                        item.id
                                            ? favorites.includes(item.id)
                                            : false
                                    }
                                    onToggleFavorite={toggleFavorite}
                                    badgeCount={
                                        item.id
                                            ? badgeCounts[item.id]
                                            : undefined
                                    }
                                />
                            );
                        })}
                    </>
                )}

                {/* No Results */}
                {filteredMenu.length === 0 && (
                    <div className="px-4 py-8 text-center text-sm text-gray-400 dark:text-gray-500">
                        {t("common.global.lbl.noResults")}
                    </div>
                )}
            </nav>

            {/* Compact Footer: Language + Version + Copyright */}
            <div className="border-t border-zinc-200 px-3 py-2 dark:border-zinc-700">
                <div className="relative flex items-center justify-between">
                    {/* Language Selector - Compact */}
                    <Dropdown>
                        <Dropdown.Trigger>
                            <div className="flex cursor-pointer items-center gap-1.5 rounded-lg px-2 py-1 text-xs transition-all duration-200 hover:bg-gray-100 focus:outline-none dark:hover:bg-gray-700/50">
                                <currentLang.Flag
                                    title={currentLang.name}
                                    className="h-4 w-5 rounded-sm"
                                />
                                <ChevronDownIcon className="h-3 w-3 text-gray-400 transition-transform" />
                            </div>
                        </Dropdown.Trigger>
                        <Dropdown.Popover placement="top start">
                            <Dropdown.Menu
                                aria-label="Language selection"
                                className="min-w-32"
                                onAction={(key) => {
                                    Cookies.set("locale", key as string);
                                    router.refresh();
                                }}
                            >
                                {languages
                                    .toSorted((a, b) =>
                                        a.name.localeCompare(b.name),
                                    )
                                    .map((lang) => (
                                        <Dropdown.Item
                                            key={lang.code}
                                            textValue={lang.name}
                                        >
                                            <div className="flex w-full items-center gap-2.5">
                                                <lang.Flag
                                                    title={lang.name}
                                                    className="h-4 w-5 rounded-sm"
                                                />
                                                <span className="text-foreground">
                                                    {lang.name}
                                                </span>
                                            </div>
                                        </Dropdown.Item>
                                    ))}
                            </Dropdown.Menu>
                        </Dropdown.Popover>
                    </Dropdown>

                    {/* Version & Copyright */}
                    <div className="text-right text-[10px] text-gray-400 dark:text-gray-500">
                        <span>v1.0.0</span>
                        <span className="mx-1">â€¢</span>
                        <span>Â© 2024 Phuong Tran</span>
                    </div>
                </div>
            </div>
        </aside>
    );
}
