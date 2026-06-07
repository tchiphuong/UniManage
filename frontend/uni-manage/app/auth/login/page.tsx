"use client";

import { useTranslations } from "next-intl";
import { useState } from "react";
import { useRouter } from "@/i18n/navigation";
import { LanguageSwitcher } from "@/components";
import { useAuth } from "@/hooks/use-auth";
import { setAccessToken } from "@/lib";
import {
    UserIcon,
    LockClosedIcon,
    EyeIcon,
    EyeSlashIcon,
    ArrowRightIcon,
} from "@heroicons/react/24/outline";
import { Checkbox, Form } from "@heroui/react";
import { Button, Input } from "@/components/ui";

export default function LoginPage() {
    const t = useTranslations("auth");
    const router = useRouter();
    const { login, isLoading: authLoading } = useAuth();
    
    const [isVisible, setIsVisible] = useState(false);
    const [errors, setErrors] = useState<{ [key: string]: string }>({});

    const toggleVisibility = () => setIsVisible(!isVisible);

    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const data = Object.fromEntries(new FormData(e.currentTarget));

        // Custom validation
        const newErrors: { [key: string]: string } = {};
        if (!data.username) newErrors.username = t("usernameRequired") || "Username is required";
        if (!data.password) newErrors.password = t("passwordRequired") || "Password is required";

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        setErrors({});

        const result = await login({
            username: data.username as string,
            password: data.password as string,
            rememberMe: data.remember === "true",
        });

        if (result.success) {
            router.push("/dashboard");
        } else {
            setErrors({ _global: result.error || t("loginFailed") });
        }
    };

    const handleDemoLogin = () => {
        setAccessToken("dummy-demo-token", true);
        router.push("/dashboard");
    };

    const handleSocialLogin = (provider: string) => {
        console.log(`Login with ${provider}`);
        // Implement social login logic here
    };

    return (
        <div className="relative flex min-h-screen items-center justify-center overflow-hidden bg-gray-50 transition-colors duration-300 dark:bg-zinc-900">
            {/* Language Switcher Positioned absolute top right */}
            <div className="absolute top-6 right-6 z-50">
                <LanguageSwitcher />
            </div>

            {/* Background decorations */}
            <div className="pointer-events-none absolute top-0 left-0 h-full w-full overflow-hidden">
                <div className="animate-blob absolute -top-[30%] -left-[10%] h-[70%] w-[70%] rounded-full bg-blue-400/20 blur-3xl dark:bg-blue-600/10"></div>
                <div className="animate-blob animation-delay-2000 absolute top-[20%] -right-[10%] h-[60%] w-[60%] rounded-full bg-purple-400/20 blur-3xl dark:bg-purple-600/10"></div>
                <div className="animate-blob animation-delay-4000 absolute -bottom-[20%] left-[20%] h-[50%] w-[50%] rounded-full bg-pink-400/20 blur-3xl dark:bg-pink-600/10"></div>
            </div>

            <div className="z-10 mx-4 w-full max-w-md">
                <div className="bg-opacity-90 dark:bg-opacity-90 overflow-hidden rounded-3xl border border-zinc-100 bg-white shadow-2xl backdrop-blur-sm dark:border-zinc-700 dark:bg-zinc-800">
                    <div className="p-8 sm:p-10">
                        <div className="mb-10 text-center">
                            <div className="mb-6 inline-flex h-16 w-16 rotate-3 transform items-center justify-center rounded-2xl bg-blue-600 bg-linear-to-br from-blue-500 to-indigo-600 text-white shadow-lg transition-transform duration-300 hover:rotate-6">
                                <UserIcon className="h-8 w-8" />
                            </div>
                            <h2 className="mb-2 text-3xl font-bold tracking-tight text-gray-900 dark:text-white">
                                {t("loginTitle")}
                            </h2>
                            <p className="text-gray-500 dark:text-gray-400">{t("loginSubtitle")}</p>
                        </div>

                        <Form
                            className="w-full space-y-4"
                            validationBehavior="native"
                            onSubmit={onSubmit}
                            validationErrors={errors}
                        >
                            <Input
                                isRequired
                                errorMessage={({ validationDetails }) => {
                                    if (validationDetails.valueMissing) {
                                        return t("usernameRequired") || "Please enter your username";
                                    }
                                    return errors.username;
                                }}
                                label={t("username")}
                                name="username"
                                placeholder={t("usernamePlaceholder")}
                                type="text"
                                color={errors.username ? "danger" : "default"}
                                startContent={
                                    <UserIcon className="pointer-events-none h-5 w-5 shrink-0 text-gray-400" />
                                }
                            />

                            <Input
                                isRequired
                                errorMessage={({ validationDetails }) => {
                                    if (validationDetails.valueMissing) {
                                        return t("passwordRequired") || "Please enter your password";
                                    }
                                    return errors.password;
                                }}
                                label={t("password")}
                                name="password"
                                placeholder={t("passwordPlaceholder")}
                                type={isVisible ? "text" : "password"}
                                color={errors.password ? "danger" : "default"}
                                startContent={
                                    <LockClosedIcon className="pointer-events-none h-5 w-5 shrink-0 text-gray-400" />
                                }
                                endContent={
                                    <button
                                        className="focus:outline-none"
                                        type="button"
                                        onClick={toggleVisibility}
                                        aria-label="toggle password visibility"
                                    >
                                        {isVisible ? (
                                            <EyeSlashIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                        ) : (
                                            <EyeIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                        )}
                                    </button>
                                }
                            />

                            <div className="flex w-full items-center justify-between px-1 py-2">
                                <Checkbox
                                    name="remember"
                                    size="sm"
                                    value="true"
                                    classNames={{
                                        label: "text-small text-gray-700 dark:text-gray-300",
                                    }}
                                >
                                    {t("rememberMe")}
                                </Checkbox>
                                <a
                                    className="text-small text-primary cursor-pointer font-medium hover:underline"
                                    href="/auth/forgot-password"
                                >
                                    {t("forgotPassword")}
                                </a>
                            </div>

                            {errors._global && (
                                <div className="p-3 rounded-lg bg-danger-50 border border-danger-200 text-danger-600 text-sm font-medium text-center">
                                    {errors._global}
                                </div>
                            )}

                            <Button
                                className="h-12 w-full bg-linear-to-r from-blue-600 to-indigo-600 text-base font-semibold text-white shadow-lg shadow-blue-500/30"
                                color="primary"
                                type="submit"
                                variant="solid"
                                isLoading={authLoading}
                            >
                                {authLoading ? (
                                    "Logging in..."
                                ) : (
                                    <span className="flex items-center justify-center">
                                        {t("login")}
                                        <ArrowRightIcon className="ml-2 h-5 w-5" />
                                    </span>
                                )}
                            </Button>
                        </Form>

                        <div className="relative my-8">
                            <div className="absolute inset-0 flex items-center">
                                <div className="w-full border-t border-zinc-200 dark:border-zinc-700"></div>
                            </div>
                            <div className="relative flex justify-center text-sm">
                                <span className="bg-white px-4 font-medium tracking-wide text-gray-500 dark:bg-zinc-800 dark:text-gray-400 uppercase">
                                    {t("orContinueWith")}
                                </span>
                            </div>
                        </div>

                        <div className="grid grid-cols-2 gap-4">
                            <Button
                                type="button"
                                variant="bordered"
                                onClick={() => handleSocialLogin("google")}
                                className="group h-12"
                            >
                                <img
                                    src="https://www.svgrepo.com/show/475656/google-color.svg"
                                    className="h-5 w-5 transition-transform duration-200 group-hover:scale-110"
                                    alt="Google"
                                />
                                <span className="ml-3 text-sm font-medium text-gray-700 dark:text-gray-200">
                                    Google
                                </span>
                            </Button>
                            <Button
                                type="button"
                                variant="bordered"
                                onClick={handleDemoLogin}
                                className="group h-12"
                            >
                                <UserIcon className="h-5 w-5 text-gray-900 transition-transform duration-200 group-hover:scale-110 dark:text-white" />
                                <span className="ml-3 text-sm font-medium text-gray-700 dark:text-gray-200">
                                    Demo User
                                </span>
                            </Button>
                        </div>
                    </div>
                    
                    <div className="border-t border-zinc-100 bg-gray-50 px-8 py-6 text-center dark:border-zinc-700 dark:bg-zinc-700/30">
                        <p className="text-sm text-gray-600 dark:text-gray-400">
                            {t("noAccount")}{" "}
                            <a
                                href="/auth/register"
                                className="font-semibold text-blue-600 transition-colors hover:text-blue-500 dark:text-blue-400"
                            >
                                {t("createAccount")}
                            </a>
                        </p>
                    </div>
                </div>
            </div>
            {/* Custom Animation Styles */}
            <style>{`
                @keyframes blob {
                    0% { transform: translate(0px, 0px) scale(1); }
                    33% { transform: translate(30px, -50px) scale(1.1); }
                    66% { transform: translate(-20px, 20px) scale(0.9); }
                    100% { transform: translate(0px, 0px) scale(1); }
                }
                .animate-blob {
                    animation: blob 7s infinite;
                }
                .animation-delay-2000 {
                    animation-delay: 2s;
                }
                .animation-delay-4000 {
                    animation-delay: 4s;
                }
            `}</style>
        </div>
    );
}
