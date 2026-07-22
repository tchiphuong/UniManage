export const appConfig = {
    // 1. App Info
    appName: "UniManage",
    companyName: "Phuong Tran",
    version: "1.0.0",
    copyright: `© 2024 - ${new Date().getFullYear()} Phuong Tran`,
    logoUrl: "/logo.png",

    // 2. Localization
    defaultLocale: "vi",
    locales: ["vi", "en"],

    // 3. UI / UX Defaults
    ui: {
        defaultTheme: "light", // 'light' | 'dark'
        pagination: {
            defaultPageSize: 10,
            pageSizeOptions: [10, 20, 50, 100],
        },
        table: {
            defaultSortOrder: "desc",
        },
        toast: {
            duration: 3000,
            placement: "top-right" as const,
        },
    },

    // 4. Formatting
    format: {
        date: "DD/MM/YYYY",
        dateTime: "DD/MM/YYYY HH:mm",
        currency: "VND",
        currencySymbol: "₫",
    },

    // 5. Network / API
    api: {
        // Lấy từ biến môi trường, fallback về localhost
        baseUrl:
            process.env.NEXT_PUBLIC_API_URL || "https://localhost:5001/api",
        timeout: 30000, // 30 seconds
        retryAttempts: 2,
    },

    // 6. Security / Auth
    auth: {
        tokenStorageKey: "unimanage_token",
        refreshTokenStorageKey: "unimanage_refresh_token",
        sessionTimeoutMinutes: 60,
    },
};
