"use client";

import { useTranslations } from "next-intl";
import { Link, useRouter } from "@/i18n/navigation";
import { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import {
    Form,
    Input,
    Button,
    Checkbox,
    TextField,
    Label,
    FieldError,
} from "@heroui/react";
import { EyeIcon, EyeSlashIcon } from "@heroicons/react/24/outline";
import { LanguageSwitcher } from "@/components";

import { apiClient } from "@/lib";
import { API_ENDPOINTS } from "@/lib/api-endpoints";

export default function RegisterPage() {
    const t = useTranslations("auth.register");
    const router = useRouter();
    const [isVisible, setIsVisible] = useState(false);
    const [isVisibleConfirm, setIsVisibleConfirm] = useState(false);
    const [apiError, setApiError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    const toggleVisibility = () => setIsVisible(!isVisible);
    const toggleVisibilityConfirm = () =>
        setIsVisibleConfirm(!isVisibleConfirm);

    const registerSchema = z
        .object({
            fullName: z
                .string()
                .min(1, t("err.fullNameRequired") || "Full name is required"),
            username: z
                .string()
                .min(
                    3,
                    t("err.usernameMinLength") ||
                        "Username must be at least 3 characters",
                ),
            password: z
                .string()
                .min(
                    6,
                    t("err.passwordMinLength") ||
                        "Password must be at least 6 characters",
                ),
            confirmPassword: z
                .string()
                .min(
                    1,
                    t("err.confirmPasswordRequired") ||
                        "Please confirm your password",
                ),
            terms: z.boolean().refine((val) => val === true, {
                message:
                    t("msg.termsRequired") ||
                    "You must accept the terms and conditions",
            }),
        })
        .refine((data) => data.password === data.confirmPassword, {
            message: t("err.passwordsDoNotMatch") || "Passwords do not match",
            path: ["confirmPassword"],
        });

    type RegisterFormValues = z.infer<typeof registerSchema>;

    const {
        control,
        handleSubmit,
        formState: { errors },
    } = useForm<RegisterFormValues>({
        resolver: zodResolver(registerSchema),
        defaultValues: {
            fullName: "",
            username: "",
            password: "",
            confirmPassword: "",
            terms: false,
        },
    });

    const onSubmit = async (data: RegisterFormValues) => {
        setApiError(null);
        setIsLoading(true);
        try {
            const response = await apiClient.post<any>(
                API_ENDPOINTS.AUTH.REGISTER,
                {
                    fullName: data.fullName,
                    username: data.username,
                    password: data.password,
                },
            );

            if (response.returnCode === 0) {
                router.push("/auth/login?registered=true");
            } else {
                setApiError(
                    response.errors?.[0]?.toString() ||
                        t("err.failed") ||
                        "Registration failed. Please try again.",
                );
            }
        } catch (error: any) {
            setApiError(
                error.response?.data?.message ||
                    t("err.failed") ||
                    "Registration failed. Please try again.",
            );
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="relative flex min-h-screen items-center justify-center overflow-hidden bg-gray-50 transition-colors duration-300 dark:bg-zinc-900">
            {/* Language Switcher Positioned absolute top right */}
            <div className="absolute top-6 right-6 z-50">
                <LanguageSwitcher />
            </div>

            {/* Ambient Background Blobs */}
            <div className="pointer-events-none absolute top-0 left-0 h-full w-full overflow-hidden">
                <div className="animate-blob absolute -top-[30%] -left-[10%] h-[70%] w-[70%] rounded-full bg-blue-400/20 blur-3xl dark:bg-blue-600/10"></div>
                <div className="animate-blob animation-delay-2000 absolute top-[20%] -right-[10%] h-[60%] w-[60%] rounded-full bg-purple-400/20 blur-3xl dark:bg-purple-600/10"></div>
                <div className="animate-blob animation-delay-4000 absolute -bottom-[20%] left-[20%] h-[50%] w-[50%] rounded-full bg-pink-400/20 blur-3xl dark:bg-pink-600/10"></div>
            </div>

            <div className="z-10 mx-4 my-8 w-full max-w-md">
                <div className="overflow-hidden rounded-3xl border border-white/20 bg-white/40 shadow-2xl backdrop-blur-md dark:border-white/10 dark:bg-zinc-900/40">
                    <div className="p-8 sm:p-10">
                        <div className="mb-8 text-center">
                            <h2 className="mb-2 text-3xl font-bold tracking-tight text-gray-900 dark:text-white">
                                {t("btn.register")}
                            </h2>
                            <p className="text-gray-500 dark:text-gray-400">
                                {t("lbl.subtitle") || "Join us today"}
                            </p>
                        </div>

                        <Form
                            className="w-full space-y-4"
                            onSubmit={handleSubmit(onSubmit)}
                        >
                            <Controller
                                name="fullName"
                                control={control}
                                render={({ field }) => (
                                    <TextField
                                        className="w-full"
                                        isInvalid={!!errors.fullName}
                                        name={field.name}
                                        value={field.value}
                                        onChange={field.onChange}
                                    >
                                        <Label className="text-sm font-medium">
                                            {t("lbl.fullName") || "Full Name"}
                                        </Label>
                                        <Input
                                            placeholder={
                                                t("ph.fullName") ||
                                                "Enter your full name"
                                            }
                                            type="text"
                                            className="w-full rounded-lg border-zinc-200/50 bg-white/50 px-3 py-2 backdrop-blur-sm dark:border-zinc-700/50 dark:bg-zinc-800/50"
                                        />
                                        <FieldError className="text-danger text-xs">
                                            {errors.fullName?.message as string}
                                        </FieldError>
                                    </TextField>
                                )}
                            />

                            <Controller
                                name="username"
                                control={control}
                                render={({ field }) => (
                                    <TextField
                                        className="w-full"
                                        isInvalid={!!errors.username}
                                        name={field.name}
                                        value={field.value}
                                        onChange={field.onChange}
                                    >
                                        <Label className="text-sm font-medium">
                                            {t("lbl.username") || "Username"}
                                        </Label>
                                        <Input
                                            placeholder={
                                                t("ph.username") ||
                                                "Choose a username"
                                            }
                                            type="text"
                                            className="w-full rounded-lg border-zinc-200/50 bg-white/50 px-3 py-2 backdrop-blur-sm dark:border-zinc-700/50 dark:bg-zinc-800/50"
                                        />
                                        <FieldError className="text-danger text-xs">
                                            {errors.username?.message as string}
                                        </FieldError>
                                    </TextField>
                                )}
                            />

                            <Controller
                                name="password"
                                control={control}
                                render={({ field }) => (
                                    <TextField
                                        className="w-full"
                                        isInvalid={!!errors.password}
                                        name={field.name}
                                        value={field.value}
                                        onChange={field.onChange}
                                    >
                                        <Label className="text-sm font-medium">
                                            {t("lbl.password") || "Password"}
                                        </Label>
                                        <div className="relative w-full">
                                            <Input
                                                placeholder={
                                                    t("ph.password") ||
                                                    "Create a password"
                                                }
                                                type={
                                                    isVisible
                                                        ? "text"
                                                        : "password"
                                                }
                                                className="w-full rounded-lg border-zinc-200/50 bg-white/50 px-3 py-2 backdrop-blur-sm dark:border-zinc-700/50 dark:bg-zinc-800/50"
                                            />
                                            <button
                                                className="absolute inset-y-0 right-3 flex items-center focus:outline-none"
                                                type="button"
                                                onClick={toggleVisibility}
                                            >
                                                {isVisible ? (
                                                    <EyeSlashIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                                ) : (
                                                    <EyeIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                                )}
                                            </button>
                                        </div>
                                        <FieldError className="text-danger text-xs">
                                            {errors.password?.message as string}
                                        </FieldError>
                                    </TextField>
                                )}
                            />

                            <Controller
                                name="confirmPassword"
                                control={control}
                                render={({ field }) => (
                                    <TextField
                                        className="w-full"
                                        isInvalid={!!errors.confirmPassword}
                                        name={field.name}
                                        value={field.value}
                                        onChange={field.onChange}
                                    >
                                        <Label className="text-sm font-medium">
                                            {t("lbl.confirmPassword") ||
                                                "Confirm Password"}
                                        </Label>
                                        <div className="relative w-full">
                                            <Input
                                                placeholder={
                                                    t("ph.confirmPassword") ||
                                                    "Confirm your password"
                                                }
                                                type={
                                                    isVisibleConfirm
                                                        ? "text"
                                                        : "password"
                                                }
                                                className="w-full rounded-lg border-zinc-200/50 bg-white/50 px-3 py-2 backdrop-blur-sm dark:border-zinc-700/50 dark:bg-zinc-800/50"
                                            />
                                            <button
                                                className="absolute inset-y-0 right-3 flex items-center focus:outline-none"
                                                type="button"
                                                onClick={
                                                    toggleVisibilityConfirm
                                                }
                                            >
                                                {isVisibleConfirm ? (
                                                    <EyeSlashIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                                ) : (
                                                    <EyeIcon className="h-5 w-5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300" />
                                                )}
                                            </button>
                                        </div>
                                        <FieldError className="text-danger text-xs">
                                            {
                                                errors.confirmPassword
                                                    ?.message as string
                                            }
                                        </FieldError>
                                    </TextField>
                                )}
                            />

                            <div className="flex w-full items-start px-1 py-2">
                                <Controller
                                    name="terms"
                                    control={control}
                                    render={({ field }) => (
                                        <Checkbox
                                            isSelected={field.value}
                                            onChange={field.onChange}
                                            isInvalid={!!errors.terms}
                                            name={field.name}
                                        >
                                            <Checkbox.Content>
                                                <Checkbox.Control>
                                                    <Checkbox.Indicator />
                                                </Checkbox.Control>
                                                <span className="text-small text-gray-700 dark:text-gray-300">
                                                    {t("lbl.agreeToTerms") ||
                                                        "I agree to the Terms and Conditions"}
                                                </span>
                                            </Checkbox.Content>
                                        </Checkbox>
                                    )}
                                />
                            </div>

                            {errors.terms && (
                                <p className="text-danger px-1 text-[12px]">
                                    {errors.terms.message}
                                </p>
                            )}

                            {apiError && (
                                <div className="bg-danger-50 border-danger-200 text-danger-600 rounded-lg border p-3 text-center text-sm font-medium">
                                    {apiError}
                                </div>
                            )}

                            <Button
                                variant="primary"
                                type="submit"
                                isPending={isLoading}
                            >
                                {isLoading ? (
                                    "Creating account..."
                                ) : (
                                    <span className="flex items-center justify-center">
                                        {t("btn.register") || "Sign Up"}
                                    </span>
                                )}
                            </Button>
                        </Form>
                    </div>

                    <div className="border-t border-zinc-200/50 bg-white/30 px-8 py-6 text-center backdrop-blur-md dark:border-zinc-700/50 dark:bg-zinc-800/30">
                        <p className="text-sm text-gray-600 dark:text-gray-400">
                            {t("lbl.alreadyHaveAccount") ||
                                "Already have an account?"}{" "}
                            <Link
                                href="/auth/login"
                                className="font-semibold text-blue-600 transition-colors hover:text-blue-500 dark:text-blue-400"
                            >
                                {t("btn.login")}
                            </Link>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
}
