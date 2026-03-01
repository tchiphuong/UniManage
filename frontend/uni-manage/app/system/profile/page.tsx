"use client";

import { useTranslations } from "next-intl";

export default function ProfilePage() {
    const t = useTranslations("common");

    return (
        <div className="rounded-lg bg-white p-6 shadow dark:bg-zinc-800">
            <h1 className="mb-4 text-2xl font-bold text-gray-900 dark:text-white">Profile</h1>
            <p className="text-gray-600 dark:text-gray-400">User profile settings will appear here.</p>
        </div>
    );
}
