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
        <div className="min-h-screen flex items-center justify-center p-4 bg-gradient-to-br from-gray-900 via-purple-900 to-gray-900">
            {/* Background effects */}
            <div className="absolute inset-0 overflow-hidden">
                <div className="absolute -top-40 -right-40 w-80 h-80 bg-purple-500 rounded-full mix-blend-multiply filter blur-xl opacity-20 animate-blob"></div>
                <div className="absolute -bottom-40 -left-40 w-80 h-80 bg-blue-500 rounded-full mix-blend-multiply filter blur-xl opacity-20 animate-blob animation-delay-2000"></div>
                <div className="absolute top-40 left-40 w-80 h-80 bg-pink-500 rounded-full mix-blend-multiply filter blur-xl opacity-20 animate-blob animation-delay-4000"></div>
            </div>

            {/* Form card */}
            <div className="relative w-full max-w-md">
                <div className="backdrop-blur-xl bg-white/10 rounded-2xl p-8 shadow-2xl border border-white/20">
                    {/* Logo and title */}
                    <div className="text-center mb-8">
                        <div className="flex justify-center mb-4">
                            <div className="w-16 h-16 bg-gradient-to-br from-blue-500 to-purple-600 rounded-xl flex items-center justify-center">
                                <Image
                                    src="/logo.svg"
                                    alt="UniManage Logo"
                                    width={40}
                                    height={40}
                                    className="invert"
                                />
                            </div>
                        </div>

                        <h1 className="text-3xl font-bold text-white mb-2">{title}</h1>
                        {description && (
                            <p className="text-gray-300 text-sm">{description}</p>
                        )}
                    </div>

                    {/* Form content */}
                    <div className="space-y-6">{children}</div>

                    {/* Back to login */}
                    {backToLogin && (
                        <div className="mt-6 text-center">
                            <Link
                                href="/auth/login"
                                className="text-sm text-blue-400 hover:text-blue-300 transition-colors"
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

