import { useState, FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { Input } from "@/components/common";
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
        <div className="relative min-h-screen overflow-hidden bg-gradient-to-br from-slate-900 via-purple-900 to-slate-900">
            {/* Animated Background Elements */}
            <div className="absolute inset-0 overflow-hidden">
                {/* Large Gradient Orbs */}
                <div className="absolute -top-40 -right-40 w-80 h-80 bg-purple-500 rounded-full mix-blend-multiply filter blur-3xl opacity-30 animate-blob"></div>
                <div className="absolute -bottom-40 -left-40 w-80 h-80 bg-blue-500 rounded-full mix-blend-multiply filter blur-3xl opacity-30 animate-blob delay-1000"></div>
                <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-80 h-80 bg-indigo-500 rounded-full mix-blend-multiply filter blur-3xl opacity-30 animate-blob delay-500"></div>
                
                {/* Floating Grid Pattern */}
                <div className="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWxsPSJub25lIiBmaWxsLXJ1bGU9ImV2ZW5vZGQiPjxnIGZpbGw9IiNmZmYiIGZpbGwtb3BhY2l0eT0iMC4wNSI+PHBhdGggZD0iTTM2IDE2djhoOHYtOGgtOHpNMjAgMjB2NGg0di00aC00ek00IDR2NGg0VjRINHoiLz48L2c+PC9nPjwvc3ZnPg==')] opacity-20"></div>
            </div>

            {/* Main Content */}
            <div className="relative z-10 flex items-center justify-center min-h-screen p-4">
                {/* Login Container */}
                <div className="w-full max-w-md">
                    {/* Logo/Brand Section with Animation */}
                    <div className="text-center mb-8 animate-slide-down">
                        <div className="inline-flex items-center justify-center w-20 h-20 mb-4 bg-gradient-to-br from-blue-500 to-purple-600 rounded-2xl shadow-2xl animate-float">
                            <svg className="w-10 h-10 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
                            </svg>
                        </div>
                        <h1 className="text-4xl font-bold text-white mb-2 text-gradient-blue">
                            Welcome Back
                        </h1>
                        <p className="text-gray-300 text-lg">
                            UniManage System
                        </p>
                    </div>

                    {/* Glass Card with Animation */}
                    <div className="glass-dark rounded-3xl shadow-2xl p-8 backdrop-blur-xl animate-scale-in delay-200 border border-white/10">
                        <form onSubmit={handleSubmit} className="space-y-6">
                            {/* Error Message with Animation */}
                            {errorMessage && (
                                <div className="p-4 bg-red-500/10 border border-red-500/30 rounded-xl backdrop-blur-sm animate-slide-up">
                                    <div className="flex items-center gap-3">
                                        <div className="flex-shrink-0">
                                            <svg className="w-5 h-5 text-red-400" fill="currentColor" viewBox="0 0 20 20">
                                                <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
                                            </svg>
                                        </div>
                                        <p className="text-sm text-red-300">{errorMessage}</p>
                                    </div>
                                </div>
                            )}

                            {/* Username Input */}
                            <div className="animate-slide-up delay-300">
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
                            </div>

                            {/* Password Input */}
                            <div className="animate-slide-up delay-400">
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
                            </div>

                            {/* Remember Me & Forgot Password */}
                            <div className="flex items-center justify-between animate-slide-up delay-500">
                                <label className="flex items-center group cursor-pointer">
                                    <input
                                        type="checkbox"
                                        className="w-4 h-4 text-blue-500 bg-white/10 border-white/20 rounded focus:ring-blue-500 focus:ring-offset-0 transition-all"
                                    />
                                    <span className="ml-2 text-sm text-gray-300 group-hover:text-white transition-colors">
                                        Remember me
                                    </span>
                                </label>
                                <a
                                    href="#"
                                    className="text-sm text-blue-400 hover:text-blue-300 transition-colors hover:underline"
                                >
                                    Forgot password?
                                </a>
                            </div>

                            {/* Submit Button with Gradient */}
                            <div className="animate-slide-up delay-700">
                                <button
                                    type="submit"
                                    disabled={isLoading}
                                    className="relative w-full group overflow-hidden rounded-xl bg-gradient-to-r from-blue-600 to-purple-600 p-[2px] transition-all hover:scale-[1.02] active:scale-[0.98] disabled:opacity-50 disabled:cursor-not-allowed"
                                >
                                    <div className="relative bg-gradient-to-r from-blue-600 to-purple-600 rounded-xl px-6 py-3.5 transition-all group-hover:from-blue-500 group-hover:to-purple-500">
                                        {isLoading ? (
                                            <div className="flex items-center justify-center gap-2">
                                                <svg className="animate-spin h-5 w-5 text-white" fill="none" viewBox="0 0 24 24">
                                                    <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                                                    <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                                                </svg>
                                                <span className="text-white font-semibold">Signing in...</span>
                                            </div>
                                        ) : (
                                            <div className="flex items-center justify-center gap-2">
                                                <span className="text-white font-semibold">Sign In</span>
                                                <svg className="w-5 h-5 text-white transition-transform group-hover:translate-x-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 7l5 5m0 0l-5 5m5-5H6" />
                                                </svg>
                                            </div>
                                        )}
                                    </div>
                                </button>
                            </div>

                            {/* Divider */}
                            <div className="relative animate-fade-in delay-1000">
                                <div className="absolute inset-0 flex items-center">
                                    <div className="w-full border-t border-white/10"></div>
                                </div>
                                <div className="relative flex justify-center text-sm">
                                    <span className="px-4 text-gray-400 bg-slate-900/50">Demo credentials</span>
                                </div>
                            </div>

                            {/* Demo Credentials */}
                            <div className="text-center animate-fade-in delay-1000">
                                <div className="inline-flex items-center gap-2 px-4 py-2 bg-white/5 border border-white/10 rounded-lg backdrop-blur-sm">
                                    <svg className="w-4 h-4 text-blue-400" fill="currentColor" viewBox="0 0 20 20">
                                        <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clipRule="evenodd" />
                                    </svg>
                                    <code className="text-xs text-gray-300 font-mono">
                                        admin / 123456
                                    </code>
                                </div>
                            </div>
                        </form>
                    </div>

                    {/* Footer */}
                    <div className="mt-8 text-center animate-fade-in delay-1000">
                        <p className="text-sm text-gray-400">
                            Powered by{" "}
                            <span className="text-transparent bg-clip-text bg-gradient-to-r from-blue-400 to-purple-400 font-semibold">
                                UniManage
                            </span>
                        </p>
                    </div>
                </div>
            </div>

            {/* Subtle Glow Effects */}
            <div className="absolute bottom-0 left-0 right-0 h-px bg-gradient-to-r from-transparent via-blue-500 to-transparent opacity-50"></div>
        </div>
    );
};
