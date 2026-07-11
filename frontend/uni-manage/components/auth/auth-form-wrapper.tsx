import { ReactNode } from "react";
import Link from "next/link";
import Image from "next/image";

interface AuthFormWrapperProps {
    children: ReactNode;
    title: string;
    description?: string;
    backToLogin?: boolean;
}

export function AuthFormWrapper({
    children,
    title,
    description,
    backToLogin = true,
}: AuthFormWrapperProps) {
    return (
        <div className="flex min-h-screen items-center justify-center bg-gradient-to-br from-gray-900 via-purple-900 to-gray-900 p-4">
            {/* Background effects */}
            <div className="absolute inset-0 overflow-hidden">
                <div className="animate-blob absolute -top-40 -right-40 h-80 w-80 rounded-full bg-purple-500 opacity-20 mix-blend-multiply blur-xl filter"></div>
                <div className="animate-blob animation-delay-2000 absolute -bottom-40 -left-40 h-80 w-80 rounded-full bg-blue-500 opacity-20 mix-blend-multiply blur-xl filter"></div>
                <div className="animate-blob animation-delay-4000 absolute top-40 left-40 h-80 w-80 rounded-full bg-pink-500 opacity-20 mix-blend-multiply blur-xl filter"></div>
            </div>

            {/* Form card */}
            <div className="relative w-full max-w-md">
                <div className="rounded-2xl border border-white/20 bg-white/10 p-8 shadow-2xl backdrop-blur-xl">
                    {/* Logo and title */}
                    <div className="mb-8 text-center">
                        <div className="mb-4 flex justify-center">
                            <div className="flex h-16 w-16 items-center justify-center rounded-xl bg-gradient-to-br from-blue-500 to-purple-600">
                                <Image
                                    src="/logo.svg"
                                    alt="UniManage Logo"
                                    width={40}
                                    height={40}
                                    className="invert"
                                />
                            </div>
                        </div>

                        <h1 className="mb-2 text-3xl font-bold text-white">
                            {title}
                        </h1>
                        {description && (
                            <p className="text-sm text-gray-300">
                                {description}
                            </p>
                        )}
                    </div>

                    {/* Form content */}
                    <div className="space-y-6">{children}</div>

                    {/* Back to login */}
                    {backToLogin && (
                        <div className="mt-6 text-center">
                            <Link
                                href="/auth/login"
                                className="text-sm text-blue-400 transition-colors hover:text-blue-300"
                            >
                                ← Quay lại đăng nhập
                            </Link>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}
