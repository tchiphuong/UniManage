"use client";

import { useTranslations } from "next-intl";
import { useState } from "react";
import { useRouter } from "@/i18n/navigation";
import { LanguageSwitcher } from "@/components";
import { useAuth } from "@/hooks/use-auth";
import { setAccessToken } from "@/lib";
import {
    Button,
    Checkbox,
    Link,
    Form,
    Input,
    Label,
    TextField,
    FieldError,
    ErrorMessage,
    Spinner,
} from "@heroui/react";
import { Icon } from "@iconify/react";

export default function LoginPage() {
    const t = useTranslations("auth"); // Correct namespace
    const router = useRouter();
    const { login, isLoading: authLoading } = useAuth();

    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);

        const formData = new FormData(e.currentTarget);
        const username = formData.get("username") as string;
        const password = formData.get("password") as string;

        const result = await login({
            username,
            password,
            rememberMe: true,
        });

        if (result.success) {
            router.push("/dashboard");
        } else {
            setError(result.error || t("loginFailed"));
        }
    };

    const handleDemoLogin = () => {
        setAccessToken("dummy-demo-token", true);
        router.push("/dashboard");
    };

    return (
        <div className="flex h-screen w-full overflow-hidden bg-gray-100">
            {/* Left Side - Image/Brand */}
            <div className="hidden lg:flex flex-col relative w-1/2 h-full bg-black">
                <div className="absolute inset-0 bg-linear-to-b from-black/60 to-black/30 z-10" />
                <img
                    src="https://images.unsplash.com/photo-1541339907198-e08756dedf3f?q=80&w=2070&auto=format&fit=crop"
                    alt="University Campus"
                    className="absolute inset-0 w-full h-full object-cover opacity-90"
                />
                <div className="relative z-20 flex flex-col justify-between h-full p-12 text-white">
                    <div className="font-bold text-2xl tracking-wide">UniManage</div>
                    <div>
                        <h2 className="text-4xl font-bold mb-4">
                            Manage your university with ease.
                        </h2>
                        <p className="text-lg text-gray-200 w-2/3">
                            Streamline administration, improved student tracking, and better
                            academic outcomes.
                        </p>
                    </div>
                </div>
            </div>

            {/* Right Side - Login Form */}
            <div className="flex flex-col w-full lg:w-1/2 h-full items-center justify-center p-8 lg:p-12 relative">
                {/* Language Switcher Positioned absolute top right */}
                <div className="absolute top-6 right-6">
                    <LanguageSwitcher />
                </div>

                <div className="w-full max-w-100 space-y-8">
                    <div className="text-center space-y-2">
                        <h1 className="text-3xl font-bold tracking-tight text-foreground">
                            {t("loginTitle")}
                        </h1>
                        <p className="text-default-500">{t("loginSubtitle")}</p>
                    </div>

                    <Form className="flex flex-col gap-6" onSubmit={handleSubmit} validationBehavior="aria">
                        <div className="space-y-4">
                            <TextField
                                className="flex flex-col gap-1.5"
                                name="username"
                                type="text"
                                isRequired
                                validate={(value) => {
                                    if (value.length < 3) {
                                        return "Username must be at least 3 characters";
                                    }
                                    return null;
                                }}
                            >
                                <Label className="text-sm font-medium text-default-700">
                                    {t("username")}
                                </Label>
                                <Input
                                    className="flex w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all border-none data-[invalid=true]:ring-danger-500 data-[invalid=true]:bg-danger-50"
                                    placeholder={t("usernamePlaceholder")}
                                />
                                <FieldError className="text-tiny text-danger-500">
                                    {(validationErrors) => {
                                        if (typeof validationErrors === "string") return validationErrors;
                                        if (Array.isArray(validationErrors)) return validationErrors.join(", ");
                                        if (validationErrors && typeof validationErrors === "object") {
                                            if ("validationErrors" in (validationErrors as any)) {
                                                return ((validationErrors as any).validationErrors as string[])?.join(", ");
                                            }
                                            if ("messages" in (validationErrors as any)) {
                                                return ((validationErrors as any).messages as string[])?.join(", ");
                                            }
                                        }
                                        return String(validationErrors || "");
                                    }}
                                </FieldError>
                            </TextField>

                            <div className="space-y-1">
                                <TextField
                                    className="flex flex-col gap-1.5"
                                    name="password"
                                    type="password"
                                    isRequired
                                    validate={(value) => {
                                        if (value.length < 6) {
                                            return "Password must be at least 6 characters";
                                        }
                                        return null;
                                    }}
                                >
                                    <Label className="text-sm font-medium text-default-700">
                                        {t("password")}
                                    </Label>
                                    <Input
                                        className="flex w-full px-3 py-2 bg-default-100 hover:bg-default-200 rounded-lg outline-none focus:ring-2 focus:ring-primary-500 transition-all border-none data-[invalid=true]:ring-danger-500 data-[invalid=true]:bg-danger-50"
                                        placeholder={t("passwordPlaceholder")}
                                    />
                                    <FieldError className="text-tiny text-danger-500">
                                        {(validationErrors) => {
                                            if (typeof validationErrors === "string") return validationErrors;
                                            if (Array.isArray(validationErrors)) return validationErrors.join(", ");
                                            if (validationErrors && typeof validationErrors === "object") {
                                                if ("validationErrors" in (validationErrors as any)) {
                                                    return ((validationErrors as any).validationErrors as string[])?.join(", ");
                                                }
                                                // Fallback for FieldErrorModel or other object types
                                                if ("messages" in (validationErrors as any)) {
                                                    return ((validationErrors as any).messages as string[])?.join(", ");
                                                }
                                            }
                                            return String(validationErrors || "");
                                        }}
                                    </FieldError>
                                </TextField>

                                <div className="flex items-center gap-2 pt-1">
                                    <Checkbox id="remember-me" value="true">
                                        <Checkbox.Control>
                                            <Checkbox.Indicator />
                                        </Checkbox.Control>
                                    </Checkbox>
                                    <Label
                                        htmlFor="remember-me"
                                        className="text-sm text-default-500 cursor-pointer select-none"
                                    >
                                        {t("rememberMe")}
                                    </Label>
                                    <div className="flex-1" />
                                    <Link href="/auth/forgot-password" className="text-sm">
                                        {t("forgotPassword")}
                                    </Link>
                                </div>
                            </div>
                        </div>

                        {error && (
                            <div className="p-3 rounded-lg bg-danger-50 text-danger-600 text-sm">
                                {typeof error === "string" ? error : JSON.stringify(error)}
                            </div>
                        )}

                        <Button
                            type="submit"
                            isPending={authLoading}
                            fullWidth
                            className="w-full font-medium bg-primary text-primary-foreground hover:bg-primary/90"
                        >
                            {({ isPending }) => (
                                <>
                                    {isPending ? <Spinner color="current" size="sm" /> : null}
                                    {isPending ? "Logging in..." : t("login")}
                                </>
                            )}
                        </Button>
                    </Form>

                    <div className="flex items-center gap-4 w-full">
                        <div className="h-px bg-default-200 flex-1 dark:bg-default-100/20" />
                        <span className="text-tiny text-default-400 uppercase">
                            {t("orContinueWith")}
                        </span>
                        <div className="h-px bg-default-200 flex-1 dark:bg-default-100/20" />
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <Button className="w-full" variant="tertiary">
                            <Icon icon="devicon:google" />
                            Sign in with Google
                        </Button>
                        <Button
                            className="bg-default-100 w-full hover:bg-default-200 font-medium py-2 dark:bg-default-50/10"
                            onPress={handleDemoLogin}
                        >
                            Demo User
                        </Button>
                    </div>

                    <div className="text-center text-sm text-default-500">
                        {t("noAccount")}{" "}
                        <Link
                            href="/auth/register"
                            className="font-medium text-primary cursor-pointer hover:underline text-small ml-1"
                        >
                            {t("createAccount")}
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
}
