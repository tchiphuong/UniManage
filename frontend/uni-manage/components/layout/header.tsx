/* eslint-disable @next/next/no-img-element */
"use client";

import { useState } from "react";
import { useTheme } from "next-themes";
import { LanguageSwitcher } from "@/components";

interface HeaderProps {
    sidebarOpen: boolean;
    setSidebarOpen: (open: boolean) => void;
}

export function Header({ sidebarOpen, setSidebarOpen }: HeaderProps) {
    const { theme, setTheme } = useTheme();
    const [notificationsOpen, setNotificationsOpen] = useState(false);
    const [userDropdownOpen, setUserDropdownOpen] = useState(false);

    return (
        <header className="sticky top-0 z-50 flex h-18 w-full items-center justify-between bg-white px-6 shadow-md dark:bg-gray-800 transition-colors duration-300">
            <div className="flex items-center gap-x-4">
                <button
                    onClick={() => setSidebarOpen(!sidebarOpen)}
                    className="text-gray-600 hover:text-gray-800 dark:text-gray-200 dark:hover:text-white focus:outline-none lg:text-gray-600"
                >
                    <span className="sr-only">Toggle sidebar</span>
                    <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth="2"
                            d={sidebarOpen ? "M6 18L18 6M6 6l12 12" : "M4 6h16M4 12h16M4 18h16"}
                        />
                    </svg>
                </button>
                <div className="flex items-center space-x-2">
                    <img src="https://pub-e63b17b4d990438a83af58c15949f8a2.r2.dev/type/circle.png" alt="Company Logo" className="h-8 w-auto object-contain" />
                </div>
            </div>

            <div className="flex items-center space-x-4">
                {/* Theme Toggle */}
                <button
                    onClick={() => setTheme(theme === "dark" ? "light" : "dark")}
                    className="p-1 text-gray-400 hover:text-gray-500 dark:hover:text-gray-300 focus:outline-none"
                >
                    {theme === "dark" ? (
                        <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z"></path>
                        </svg>
                    ) : (
                         <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M20.354 15.354A9 9 0 018.646 3.646 9.003 9.003 0 0012 21a9.003 9.003 0 008.354-5.646z"></path>
                        </svg>
                    )}
                </button>

                {/* Notifications */}
                <div className="relative">
                    <button
                        onClick={() => setNotificationsOpen(!notificationsOpen)}
                        className="p-1 text-gray-400 hover:text-gray-500 dark:hover:text-gray-300 focus:outline-none"
                    >
                        <span className="sr-only">View notifications</span>
                        <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                             <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" />
                        </svg>
                        <span className="absolute top-0 right-0 block h-2 w-2 rounded-full bg-red-600 ring-2 ring-white"></span>
                    </button>
                    {notificationsOpen && (
                        <>
                            <div className="fixed inset-0 z-10 w-full h-full" onClick={() => setNotificationsOpen(false)}></div>
                            <div className="absolute right-0 mt-2 w-80 rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 z-20 overflow-hidden dark:bg-gray-800">
                                <div className="px-4 py-2 text-xs font-semibold text-gray-500 uppercase tracking-wider dark:text-gray-400">
                                    Notifications
                                </div>
                                <div className="max-h-60 overflow-y-auto">
                                    <a href="#" className="flex items-center px-4 py-3 text-gray-600 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700 -mx-2">
                                        <figure className="w-8 h-8 rounded-full bg-blue-500"></figure>
                                        <p className="mx-2 text-sm">
                                            <span className="font-bold">New user registered</span>
                                            <span className="block text-xs text-gray-500 dark:text-gray-400">2 min ago</span>
                                        </p>
                                    </a>
                                    <a href="#" className="flex items-center px-4 py-3 text-gray-600 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700 -mx-2">
                                        <figure className="w-8 h-8 rounded-full bg-green-500"></figure>
                                        <p className="mx-2 text-sm">
                                            <span className="font-bold">New order received</span>
                                            <span className="block text-xs text-gray-500 dark:text-gray-400">1 hour ago</span>
                                        </p>
                                    </a>
                                </div>
                            </div>
                        </>
                    )}
                </div>

                {/* User Dropdown */}
                <div className="relative">
                    <button
                        onClick={() => setUserDropdownOpen(!userDropdownOpen)}
                        className="flex items-center focus:outline-none space-x-2"
                    >
                         <img className="h-8 w-8 rounded-full border-2 border-gray-300" src="https://via.placeholder.com/150" alt="User avatar" />
                         <span className="hidden lg:block text-sm font-medium text-gray-700 dark:text-gray-200">Admin User</span>
                         <svg className="hidden lg:block h-4 w-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7"></path>
                         </svg>
                    </button>
                    {userDropdownOpen && (
                         <>
                            <div className="fixed inset-0 z-10 w-full h-full" onClick={() => setUserDropdownOpen(false)}></div>
                            <div className="absolute right-0 mt-2 w-48 rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 z-20 py-1 dark:bg-gray-800">
                                <a href="#" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700">Your Profile</a>
                                <a href="#" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700">Settings</a>
                                <a href="#" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700">Sign out</a>
                            </div>
                        </>
                    )}
                </div>
                 
                {/* Language Switcher */}
                <div className="items-center">
                    <LanguageSwitcher />
                </div>
            </div>
        </header>
    );
}
