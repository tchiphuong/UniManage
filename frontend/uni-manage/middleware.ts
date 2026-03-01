import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";
import { locales, defaultLocale } from "./i18n/config";

/**
 * Routes that don't require authentication
 */
const publicPaths = ["/auth/login", "/auth/register", "/auth/forgot-password"];

/**
 * Check if the path is a public route (no auth required)
 */
function isPublicPath(pathname: string): boolean {
    return publicPaths.some((path) => pathname === path || pathname.startsWith(path + "/"));
}

export function middleware(request: NextRequest) {
    const { pathname } = request.nextUrl;

    // Ignore internal paths and static files
    if (pathname.startsWith("/_next") || pathname.startsWith("/api") || pathname.includes(".")) {
        return;
    }

    // --- Auth Guard ---
    const token = request.cookies.get("access_token")?.value;

    // If accessing protected route without token → redirect to login
    if (!isPublicPath(pathname) && !token) {
        const loginUrl = new URL("/auth/login", request.url);
        loginUrl.searchParams.set("callbackUrl", pathname);
        return NextResponse.redirect(loginUrl);
    }

    // If accessing login page with valid token → redirect to dashboard
    if (pathname === "/auth/login" && token) {
        return NextResponse.redirect(new URL("/dashboard", request.url));
    }

    // --- Locale Handling ---
    const localeCookie = request.cookies.get("locale")?.value;
    const locale =
        localeCookie && locales.includes(localeCookie as any) ? localeCookie : defaultLocale;

    const response = NextResponse.next();

    // Ensure locale cookie is set if not present
    if (!localeCookie) {
        response.cookies.set("locale", locale);
    }

    return response;
}

export const config = {
    matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"],
};
