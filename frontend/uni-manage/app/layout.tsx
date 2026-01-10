import type { Metadata } from "next";
import { NextIntlClientProvider } from "next-intl";
import { getLocale, getMessages } from "next-intl/server";
import { Inter } from "next/font/google";
import "./globals.css";
import { Providers } from "./providers";
import { DashboardLayout } from "@/components/dashboard-layout";

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
}: {
  children: React.ReactNode;
}) {
  const locale = await getLocale();
  const messages = await getMessages();

  return (
    <html lang={locale} suppressHydrationWarning className={inter.variable}>
      <body className="antialiased">
        <NextIntlClientProvider messages={messages}>
          <Providers>
            <DashboardLayout>{children}</DashboardLayout>
          </Providers>
        </NextIntlClientProvider>
      </body>
    </html>
  );
}
