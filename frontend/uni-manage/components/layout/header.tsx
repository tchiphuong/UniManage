"use client";

import { useState, useEffect } from "react";
import { useRouter } from "@/i18n/navigation";
import { useTranslations } from "next-intl";
import useSWR from "swr";
import { useTheme } from "next-themes";
import { useSidebar } from "@/contexts/sidebar-context";
import { Notification, User } from "@/types";
import { Dropdown, Avatar, Button } from "@heroui/react";
import {
    SunIcon,
    MoonIcon,
    BellIcon,
    ChevronDownIcon,
    UserIcon,
    Cog6ToothIcon,
    ArrowRightStartOnRectangleIcon,
} from "@heroicons/react/24/outline";

export function Header() {
    const t = useTranslations();
    const { theme, setTheme, resolvedTheme } = useTheme();
    const darkMode = resolvedTheme === "dark";
    const toggleDarkMode = () => setTheme(darkMode ? "light" : "dark");
    const { sidebarOpen, toggleSidebar } = useSidebar();
    const router = useRouter();

    const [mounted, setMounted] = useState(false);
    useEffect(() => setMounted(true), []);

    // Fetch notifications with SWR
    const { data: notificationsData, mutate: mutateNotifications } = useSWR<Notification[]>(
        "https://jsonplaceholder.typicode.com/comments?_limit=5",
        (url) => fetch(url).then((res) => res.json()),
        { revalidateOnFocus: false, dedupingInterval: 600000 }
    );
    const notifications = notificationsData || [];

    // Fetch user with SWR
    const { data: userData } = useSWR(
        "https://randomuser.me/api/",
        (url) => fetch(url).then((res) => res.json()),
        { revalidateOnFocus: false, dedupingInterval: 600000 }
    );
    const user: User | null = userData?.results?.[0] || null;

    const markAllAsRead = () => {
        mutateNotifications([], false);
    };

    // User menu handlers
    const handleProfile = () => {
        router.push("/dashboard/profile");
    };

    const handleSettings = () => {
        router.push("/dashboard/settings");
    };

    const handleSignOut = () => {
        router.push("/auth/login");
    };

    const renderThemeIcon = () => {
        if (!mounted) return <div className="h-6 w-6" />;
        if (darkMode) {
            return <SunIcon className="h-6 w-6 text-yellow-500" />;
        }
        return <MoonIcon className="h-6 w-6 text-blue-500" />;
    };

    return (
        <header className="z-50 h-16 bg-surface text-surface-foreground shadow-md transition-colors duration-300">
            <div className="mx-auto h-full px-4">
                <div className="flex h-full items-center justify-between">
                    <div className="flex items-center space-x-4">
                        {/* Hamburger/X button */}
                        <button
                            onClick={toggleSidebar}
                            className="relative h-8 w-8 cursor-pointer text-muted transition-colors hover:text-foreground focus:outline-none"
                        >
                            <span
                                className={`absolute block h-0.5 w-6 transform rounded-full bg-current transition duration-300 ease-in-out ${sidebarOpen ? "top-3.5 rotate-45" : "top-2"
                                    }`}
                            />
                            <span
                                className={`absolute block h-0.5 w-6 rounded-full bg-current transition-all duration-300 ease-in-out ${sidebarOpen ? "opacity-0" : "top-4"
                                    }`}
                            />
                            <span
                                className={`absolute block h-0.5 w-6 transform rounded-full bg-current transition duration-300 ease-in-out ${sidebarOpen ? "top-3.5 -rotate-45" : "top-6"
                                    }`}
                            />
                        </button>

                        <img
                            src="/imgs/logo-horizontal.png"
                            alt="UniManage Logo"
                            className="h-8 max-w-32 object-contain"
                        />
                    </div>

                    <div className="flex items-center gap-2">
                        {/* Dark mode toggle */}
                        <Button
                            isIconOnly
                            variant="ghost"
                            onClick={toggleDarkMode}
                            className="rounded-full text-muted hover:text-foreground"
                        >
                            {renderThemeIcon()}
                        </Button>

                        {/* Notification Dropdown */}
                        <Dropdown>
                            <Dropdown.Trigger>
                                <div className="relative flex h-10 w-10 cursor-pointer items-center justify-center overflow-visible rounded-full text-muted hover:text-foreground focus:outline-none">
                                    <BellIcon className="h-6 w-6 text-current" />
                                    {notifications.length > 0 && (
                                        <span className="bg-danger-500 absolute -top-1 -right-1 flex h-4 min-w-4 items-center justify-center rounded-full px-1 text-[10px] font-bold text-white shadow-sm">
                                            {notifications.length > 99
                                                ? "99+"
                                                : notifications.length}
                                        </span>
                                    )}
                                </div>
                            </Dropdown.Trigger>
                            <Dropdown.Popover placement="bottom end">
                                <Dropdown.Menu
                                    aria-label="Notifications"
                                    className="w-80 gap-4"
                                >
                                    <Dropdown.Item
                                        key="header"
                                        className="h-10 gap-2 opacity-100"
                                    >
                                        <div className="flex w-full items-center justify-between">
                                            <span className="text-sm font-semibold text-foreground">
                                                {t(
                                                    "common.global.lbl.notifications",
                                                )}
                                            </span>
                                            <span className="text-xs text-muted">
                                                {t(
                                                    "common.global.msg.unreadNotifications",
                                                    {
                                                        count: notifications.length,
                                                    },
                                                )}
                                            </span>
                                        </div>
                                    </Dropdown.Item>

                                    {notifications.length === 0 ? (
                                        <Dropdown.Item
                                            key="empty"
                                            className="py-4 text-center text-muted"
                                        >
                                            No new notifications
                                        </Dropdown.Item>
                                    ) : (
                                        notifications
                                            .slice(0, 5)
                                            .map((notification) => (
                                                <Dropdown.Item
                                                    key={notification.id}
                                                    textValue={
                                                        notification.name
                                                    }
                                                >
                                                    <div className="flex flex-col gap-1">
                                                        <p className="line-clamp-1 text-sm font-medium">
                                                            {notification.name}
                                                        </p>
                                                        <p className="line-clamp-1 text-xs italic text-muted">
                                                            {notification.email}
                                                        </p>
                                                        <p className="line-clamp-2 text-xs text-muted">
                                                            {notification.body}
                                                        </p>
                                                    </div>
                                                </Dropdown.Item>
                                            ))
                                    )}

                                    <Dropdown.Item
                                        key="mark-all"
                                        className="text-center font-medium text-accent"
                                        onPress={markAllAsRead}
                                    >
                                        {t("common.global.btn.markAllAsRead")}
                                    </Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown.Popover>
                        </Dropdown>

                        {/* User Profile Dropdown */}
                        <Dropdown>
                            <Dropdown.Trigger>
                                <div className="flex h-auto cursor-pointer items-center gap-2 rounded-lg p-1 transition-colors hover:bg-default focus:outline-none">
                                    <Avatar
                                        className="ring-accent ring-2"
                                        color="accent"
                                        size="sm"
                                    >
                                        <Avatar.Image
                                            src={user?.picture?.medium || ""}
                                            alt={user?.name?.first || "User"}
                                        />
                                        <Avatar.Fallback>
                                            {user?.name?.first?.[0] || "U"}
                                        </Avatar.Fallback>
                                    </Avatar>
                                    <div className="hidden flex-col items-start sm:flex">
                                        <span className="line-clamp-1 text-left text-sm font-medium text-foreground">
                                            {user
                                                ? `${user.name.first} ${user.name.last}`
                                                : t("common.global.lbl.loading")}
                                        </span>
                                        <span className="line-clamp-1 text-left text-xs text-muted">
                                            {user?.email ||
                                                t("common.global.lbl.loading")}
                                        </span>
                                    </div>
                                </div>
                            </Dropdown.Trigger>
                            <Dropdown.Popover placement="bottom end">
                                <Dropdown.Menu aria-label="Profile Actions">
                                    <Dropdown.Item
                                        key="profile_info"
                                        className="h-14 gap-2 opacity-100 sm:hidden"
                                    >
                                        <p className="font-semibold">
                                            {user
                                                ? `${user.name.first} ${user.name.last}`
                                                : t(
                                                    "common.global.lbl.loading",
                                                )}
                                        </p>
                                        <p className="text-xs text-muted">
                                            {user?.email ||
                                                t("common.global.lbl.loading")}
                                        </p>
                                    </Dropdown.Item>
                                    <Dropdown.Item
                                        key="profile"
                                        textValue={t("common.global.lbl.profile")}
                                        onPress={handleProfile}
                                    >
                                        <div className="flex items-center gap-2">
                                            <UserIcon className="h-5 w-5 text-current" />
                                            <span>{t("common.global.lbl.profile")}</span>
                                        </div>
                                    </Dropdown.Item>
                                    <Dropdown.Item
                                        key="settings"
                                        textValue={t("common.menu.lbl.settings")}
                                        onPress={handleSettings}
                                    >
                                        <div className="flex items-center gap-2">
                                            <Cog6ToothIcon className="h-5 w-5 text-current" />
                                            <span>{t("common.menu.lbl.settings")}</span>
                                        </div>
                                    </Dropdown.Item>
                                    <Dropdown.Item
                                        key="logout"
                                        variant="danger"
                                        textValue={t("common.global.btn.signOut")}
                                        onPress={handleSignOut}
                                    >
                                        <div className="flex items-center gap-2">
                                            <ArrowRightStartOnRectangleIcon className="h-5 w-5 text-current" />
                                            <span>{t("common.global.btn.signOut")}</span>
                                        </div>
                                    </Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown.Popover>
                        </Dropdown>
                    </div>
                </div>
            </div>
        </header>
    );
}
