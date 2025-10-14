import { useState, FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Input, Card } from "@/components/common";
import { authService } from "@/services/authService";
import { useAuthStore } from "@/store/authStore";

export const LoginPage = () => {
    const navigate = useNavigate();
    const setAuth = useAuthStore((state) => state.setAuth);

    const [formData, setFormData] = useState({
        username: "",
        password: "",
    });
    const [errors, setErrors] = useState<{ username?: string; password?: string }>({});
    const [isLoading, setIsLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    const validate = () => {
        const newErrors: typeof errors = {};

        if (!formData.username.trim()) {
            newErrors.username = "Username is required";
        }

        if (!formData.password.trim()) {
            newErrors.password = "Password is required";
        } else if (formData.password.length < 6) {
            newErrors.password = "Password must be at least 6 characters";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setErrorMessage("");

        if (!validate()) {
            return;
        }

        setIsLoading(true);

        try {
            const response = await authService.login({
                username: formData.username,
                password: formData.password,
            });

            if (response.returnCode === 0 && response.data) {
                // Save auth data to store
                setAuth(response.data.user, response.data.accessToken, response.data.refreshToken);

                // Save to localStorage
                localStorage.setItem("accessToken", response.data.accessToken);
                localStorage.setItem("refreshToken", response.data.refreshToken);

                // Redirect to dashboard
                navigate("/dashboard");
            } else {
                setErrorMessage(response.message || "Login failed");
            }
        } catch (error: unknown) {
            console.error("Login error:", error);
            const errorMessage = error && typeof error === "object" && "response" in error
                ? (error as { response?: { data?: { message?: string } } }).response?.data?.message
                : undefined;
            setErrorMessage(errorMessage || "An error occurred during login");
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-gray-900 dark:to-gray-800 flex items-center justify-center p-4">
            <Card className="w-full max-w-md">
                <div className="text-center mb-8">
                    <h1 className="text-3xl font-bold text-gray-800 dark:text-white mb-2">
                        Welcome to UniManage
                    </h1>
                    <p className="text-gray-600 dark:text-gray-400">
                        Sign in to your account to continue
                    </p>
                </div>

                <form onSubmit={handleSubmit} className="space-y-6">
                    {errorMessage && (
                        <div className="p-4 bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 rounded-lg">
                            <p className="text-sm text-red-600 dark:text-red-400">{errorMessage}</p>
                        </div>
                    )}

                    <Input
                        label="Username"
                        type="text"
                        placeholder="Enter your username"
                        value={formData.username}
                        onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                        error={errors.username}
                        required
                        disabled={isLoading}
                    />

                    <Input
                        label="Password"
                        type="password"
                        placeholder="Enter your password"
                        value={formData.password}
                        onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                        error={errors.password}
                        required
                        disabled={isLoading}
                    />

                    <div className="flex items-center justify-between">
                        <label className="flex items-center">
                            <input
                                type="checkbox"
                                className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                            />
                            <span className="ml-2 text-sm text-gray-600 dark:text-gray-400">
                                Remember me
                            </span>
                        </label>
                        <a
                            href="#"
                            className="text-sm text-blue-600 hover:text-blue-700 dark:text-blue-400"
                        >
                            Forgot password?
                        </a>
                    </div>

                    <Button
                        type="submit"
                        variant="primary"
                        size="lg"
                        fullWidth
                        isLoading={isLoading}
                    >
                        Sign In
                    </Button>
                </form>

                <div className="mt-6 text-center">
                    <p className="text-sm text-gray-600 dark:text-gray-400">
                        Demo credentials: <br />
                        <code className="bg-gray-100 dark:bg-gray-700 px-2 py-1 rounded text-xs">
                            username: admin / password: 123456
                        </code>
                    </p>
                </div>
            </Card>
        </div>
    );
};
