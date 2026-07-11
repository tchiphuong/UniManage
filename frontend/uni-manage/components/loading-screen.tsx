"use client";

/**
 * Component loading screen toàn trang
 */
export function LoadingScreen() {
    return (
        <div className="flex min-h-screen flex-col items-center justify-center">
            <div className="h-12 w-12 animate-spin rounded-full border-b-2 border-blue-600"></div>
            <p className="mt-4 text-gray-600">Đang tải...</p>
        </div>
    );
}
