import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";
import { locales, defaultLocale } from "./i18n/config";

export function middleware(request: NextRequest) {
    // Check if there is any supported locale in the pathname
    const { pathname } = request.nextUrl;
    
    // Ignore internal paths
    if (
        pathname.startsWith("/_next") ||
        pathname.startsWith("/api") ||
        pathname.includes(".")
    ) {
        return;
    }

    // Check query or cookie for locale
    const localeCookie = request.cookies.get("locale")?.value;
    const locale = localeCookie && locales.includes(localeCookie as any) 
        ? localeCookie 
        : defaultLocale;

    // We do NOT rewrite the path because we are not using [locale] folder structure.
    // The locale is determined by the cookie/header in i18n/request.ts.
    
    const response = NextResponse.next();
    
    // Ensure cookie is set if not present
    if (!localeCookie) {
        response.cookies.set("locale", locale);
    }

    return response;
}

export const config = {
    matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"],
};
