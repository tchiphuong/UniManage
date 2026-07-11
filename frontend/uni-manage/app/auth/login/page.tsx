"use client";

import { useTranslations } from "next-intl";
import { useState } from "react";
import { useRouter } from "@/i18n/navigation";
import { LanguageSwitcher } from "@/components";
import { useAuth } from "@/hooks/use-auth";
import {
    UserIcon,
    EyeSlashIcon,
    ArrowRightIcon,
    EyeIcon,
} from "@heroicons/react/24/outline";
import {
    Checkbox,
    Form,
    Input,
    Button,
    TextField,
    Label,
    FieldError,
} from "@heroui/react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { Icon } from "@iconify/react";
import { setAccessToken } from "@/lib";

export default function LoginPage() {
    const t = useTranslations("auth.login");
    const router = useRouter();
    const { login, isLoading: authLoading } = useAuth();
    const [isVisible, setIsVisible] = useState(false);
    const [apiError, setApiError] = useState<string | null>(null);

    const toggleVisibility = () => setIsVisible(!isVisible);

    const handleDemoLogin = () => {
        setAccessToken("dummy-demo-token", true);
        router.push("/dashboard");
    };

    const loginSchema = z.object({
        username: z
            .string()
            .min(1, t("err.usernameRequired") || "Username is required"),
        password: z
            .string()
            .min(1, t("err.passwordRequired") || "Password is required"),
        rememberMe: z.boolean(),
    });

    type LoginFormValues = z.infer<typeof loginSchema>;

    const {
        control,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginFormValues>({
        resolver: zodResolver(loginSchema),
        defaultValues: {
            username: "",
            password: "",
            rememberMe: false,
        },
    });

    const onSubmit = async (data: LoginFormValues) => {
        setApiError(null);
        const result = await login({
            username: data.username,
            password: data.password,
            rememberMe: data.rememberMe,
        });

        if (result.success) {
            router.push("/dashboard");
        } else {
            setApiError(result.error || t("err.failed"));
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

            <div className="z-10 mx-4 w-full max-w-md">
                <div className="overflow-hidden rounded-3xl border border-white/20 bg-white/40 shadow-2xl backdrop-blur-md dark:border-white/10 dark:bg-zinc-900/40">
                    <div className="p-8 sm:p-10">
                        <div className="mb-10 text-center">
                            <div className="mb-6 inline-flex h-16 w-16 rotate-3 transform items-center justify-center rounded-2xl bg-blue-600 bg-linear-to-br from-blue-500 to-indigo-600 text-white shadow-lg transition-transform duration-300 hover:rotate-6">
                                <UserIcon className="h-8 w-8" />
                            </div>
                            <h2 className="mb-2 text-3xl font-bold tracking-tight text-gray-900 dark:text-white">
                                {t("lbl.title")}
                            </h2>
                            <p className="text-gray-500 dark:text-gray-400">
                                {t("lbl.subtitle")}
                            </p>
                        </div>

                        <Form
                            className="w-full space-y-4"
                            onSubmit={handleSubmit(onSubmit)}
                        >
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
                                            {t("lbl.username")}
                                        </Label>
                                        <Input
                                            placeholder={t("ph.username")}
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
                                            {t("lbl.password")}
                                        </Label>
                                        <div className="relative w-full">
                                            <Input
                                                placeholder={t("ph.password")}
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
                                                aria-label="toggle password visibility"
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

                            <div className="flex w-full items-center justify-between px-1 py-2">
                                <Controller
                                    name="rememberMe"
                                    control={control}
                                    render={({ field }) => (
                                        <Checkbox name="basic-terms">
                                            <Checkbox.Content>
                                                <Checkbox.Control>
                                                    <Checkbox.Indicator />
                                                </Checkbox.Control>
                                                {t("lbl.rememberMe")}
                                            </Checkbox.Content>
                                        </Checkbox>
                                    )}
                                />
                                <a
                                    className="text-small text-primary cursor-pointer font-medium hover:underline"
                                    href="/auth/forgot-password"
                                >
                                    {t("btn.forgotPassword")}
                                </a>
                            </div>

                            {apiError && (
                                <div className="bg-danger-50 border-danger-200 text-danger-600 rounded-lg border p-3 text-center text-sm font-medium">
                                    {apiError}
                                </div>
                            )}

                            <Button
                                variant="primary"
                                type="submit"
                                fullWidth
                                isPending={authLoading}
                            >
                                {authLoading ? (
                                    t("msg.loggingIn")
                                ) : (
                                    <span className="flex items-center justify-center">
                                        {t("btn.login")}
                                        <ArrowRightIcon className="ml-2 h-5 w-5" />
                                    </span>
                                )}
                            </Button>
                        </Form>

                        <div className="relative my-8">
                            <div className="absolute inset-0 flex items-center">
                                <div className="w-full border-t border-zinc-200/50 dark:border-zinc-700/50"></div>
                            </div>
                            <div className="relative flex justify-center text-sm">
                                <span className="bg-transparent px-4 font-medium tracking-wide text-gray-500 uppercase backdrop-blur-md dark:text-gray-400">
                                    {t("lbl.orContinueWith")}
                                </span>
                            </div>
                        </div>

                        <div className="flex w-full flex-col gap-3">
                            <Button className="w-full" variant="secondary">
                                <Icon icon="devicon:google" />
                                {t("btn.signInWithGoogle")}
                            </Button>
                            <Button className="w-full" variant="secondary">
                                <Icon icon="devicon:facebook" />
                                Sign in with Facebook
                            </Button>
                            {process.env.NODE_ENV === "development" && (
                                <Button
                                    className="border-warning text-warning w-full border-2 border-dashed"
                                    variant="ghost"
                                    onPress={handleDemoLogin}
                                >
                                    <Icon
                                        icon="lucide:bug"
                                        className="text-lg"
                                    />
                                    Dev Login (Skip Auth)
                                </Button>
                            )}
                        </div>
                    </div>

                    <div className="border-t border-zinc-200/50 bg-white/30 px-8 py-6 text-center backdrop-blur-md dark:border-zinc-700/50 dark:bg-zinc-800/30">
                        <p className="text-sm text-gray-600 dark:text-gray-400">
                            {t("lbl.noAccount")}{" "}
                            <a
                                href="/auth/register"
                                className="font-semibold text-blue-600 transition-colors hover:text-blue-500 dark:text-blue-400"
                            >
                                {t("btn.createAccount")}
                            </a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
}
