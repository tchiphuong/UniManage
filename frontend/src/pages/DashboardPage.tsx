import { useNavigate } from "react-router-dom";
import { useAuthStore } from "@/store/authStore";
import { Button, Card } from "@/components/common";

export const DashboardPage = () => {
    const navigate = useNavigate();
    const { user, clearAuth } = useAuthStore();

    const handleLogout = () => {
        clearAuth();
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        navigate("/login");
    };

    return (
        <div className="min-h-screen bg-gray-100 dark:bg-gray-900">
            {/* Header */}
            <header className="bg-white dark:bg-gray-800 shadow">
                <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4">
                    <div className="flex justify-between items-center">
                        <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
                            UniManage Dashboard
                        </h1>
                        <div className="flex items-center gap-4">
                            <div className="text-right">
                                <p className="text-sm font-medium text-gray-900 dark:text-white">
                                    {user?.displayName}
                                </p>
                                <p className="text-xs text-gray-500 dark:text-gray-400">
                                    {user?.roleCode || "User"}
                                </p>
                            </div>
                            <Button variant="danger" size="sm" onClick={handleLogout}>
                                Logout
                            </Button>
                        </div>
                    </div>
                </div>
            </header>

            {/* Main Content */}
            <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
                    {/* Stats Cards */}
                    <Card>
                        <div className="flex items-center">
                            <div className="flex-shrink-0 bg-blue-500 rounded-md p-3">
                                <svg
                                    className="h-6 w-6 text-white"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    stroke="currentColor"
                                >
                                    <path
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth={2}
                                        d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z"
                                    />
                                </svg>
                            </div>
                            <div className="ml-5">
                                <p className="text-sm font-medium text-gray-500 dark:text-gray-400">
                                    Total Users
                                </p>
                                <p className="text-2xl font-semibold text-gray-900 dark:text-white">
                                    1,234
                                </p>
                            </div>
                        </div>
                    </Card>

                    <Card>
                        <div className="flex items-center">
                            <div className="flex-shrink-0 bg-green-500 rounded-md p-3">
                                <svg
                                    className="h-6 w-6 text-white"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    stroke="currentColor"
                                >
                                    <path
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth={2}
                                        d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
                                    />
                                </svg>
                            </div>
                            <div className="ml-5">
                                <p className="text-sm font-medium text-gray-500 dark:text-gray-400">
                                    Active
                                </p>
                                <p className="text-2xl font-semibold text-gray-900 dark:text-white">
                                    867
                                </p>
                            </div>
                        </div>
                    </Card>

                    <Card>
                        <div className="flex items-center">
                            <div className="flex-shrink-0 bg-yellow-500 rounded-md p-3">
                                <svg
                                    className="h-6 w-6 text-white"
                                    fill="none"
                                    viewBox="0 0 24 24"
                                    stroke="currentColor"
                                >
                                    <path
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth={2}
                                        d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z"
                                    />
                                </svg>
                            </div>
                            <div className="ml-5">
                                <p className="text-sm font-medium text-gray-500 dark:text-gray-400">
                                    Pending
                                </p>
                                <p className="text-2xl font-semibold text-gray-900 dark:text-white">
                                    42
                                </p>
                            </div>
                        </div>
                    </Card>
                </div>

                {/* Welcome Card */}
                <Card title="Welcome">
                    <div className="space-y-4">
                        <p className="text-gray-600 dark:text-gray-300">
                            Welcome back, <span className="font-semibold">{user?.displayName}</span>
                            !
                        </p>
                        <div className="bg-blue-50 dark:bg-blue-900/20 p-4 rounded-lg">
                            <h3 className="text-sm font-medium text-blue-900 dark:text-blue-300 mb-2">
                                Quick Info
                            </h3>
                            <dl className="grid grid-cols-2 gap-4 text-sm">
                                <div>
                                    <dt className="text-gray-500 dark:text-gray-400">User ID:</dt>
                                    <dd className="font-medium text-gray-900 dark:text-white">
                                        {user?.id}
                                    </dd>
                                </div>
                                <div>
                                    <dt className="text-gray-500 dark:text-gray-400">Username:</dt>
                                    <dd className="font-medium text-gray-900 dark:text-white">
                                        {user?.userCode}
                                    </dd>
                                </div>
                                <div>
                                    <dt className="text-gray-500 dark:text-gray-400">Email:</dt>
                                    <dd className="font-medium text-gray-900 dark:text-white">
                                        {user?.email || "N/A"}
                                    </dd>
                                </div>
                                <div>
                                    <dt className="text-gray-500 dark:text-gray-400">Role:</dt>
                                    <dd className="font-medium text-gray-900 dark:text-white">
                                        {user?.roleCode || "User"}
                                    </dd>
                                </div>
                            </dl>
                        </div>
                    </div>
                </Card>
            </main>
        </div>
    );
};
