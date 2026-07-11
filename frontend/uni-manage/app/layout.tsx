import type { Metadata } from "next";
import { NextIntlClientProvider } from "next-intl";
import { getLocale, getMessages } from "next-intl/server";
import { Inter } from "next/font/google";
import "./globals.css";
import { Providers } from "./providers";

const inter = Inter({
    subsets: ["latin"],
    display: "swap",
    variable: "--font-inter",
});

export const metadata: Metadata = {
    title: "UniManage",
    description: "University Management System",
};

export default async function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    const locale = await getLocale();
    const messages = await getMessages();

    return (
        <html lang={locale} suppressHydrationWarning className={inter.variable} data-scrollbar="thin">
            <body className="bg-background text-foreground antialiased">
                <NextIntlClientProvider messages={messages}>
                    <Providers>{children}</Providers>
                </NextIntlClientProvider>
            </body>
        </html>
    );
}
