import { ApiResponse, PagedResult } from "@/types/api";

const DELAY_MS = 800;

function delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
}

// Dummy Models
export interface DummyUser {
    id: number;
    username: string;
    email: string;
    role: "Admin" | "User" | "Manager";
    status: "Active" | "Inactive" | "Banned";
    createdAt: string;
}

export interface DummyOrder {
    id: string;
    customerName: string;
    totalAmount: number;
    status: "Pending" | "Processing" | "Completed" | "Cancelled";
    date: string;
}

// Mock Data
const users: DummyUser[] = Array.from({ length: 50 }).map((_, i) => ({
    id: i + 1,
    username: `user_${i + 1}`,
    email: `user${i + 1}@example.com`,
    role: i % 3 === 0 ? "Admin" : i % 2 === 0 ? "Manager" : "User",
    status: i % 5 === 0 ? "Banned" : i % 3 === 0 ? "Inactive" : "Active",
    createdAt: new Date(Date.now() - Math.random() * 10000000000).toISOString(),
}));

const orders: DummyOrder[] = Array.from({ length: 50 }).map((_, i) => ({
    id: `ORD-${1000 + i}`,
    customerName: `Customer ${i + 1}`,
    totalAmount: Math.floor(Math.random() * 500) + 50,
    status: ["Pending", "Processing", "Completed", "Cancelled"][i % 4] as DummyOrder["status"],
    date: new Date(Date.now() - Math.random() * 5000000000).toISOString(),
}));

// Generic Mock Response Builder
function mockPagedResponse<T>(items: T[], page: number, pageSize: number): ApiResponse<PagedResult<T>> {
    const totalItems = items.length;
    const totalPages = Math.ceil(totalItems / pageSize);
    const start = (page - 1) * pageSize;
    const pagedItems = items.slice(start, start + pageSize);

    return {
        returnCode: 0,
        message: "Success",
        errors: [],
        data: {
            items: pagedItems,
            paging: {
                pageIndex: page,
                pageSize,
                totalItems,
                totalPages,
            },
        },
    };
}

export const DummyApi = {
    users: {
        list: async (page = 1, pageSize = 10, search = ""): Promise<ApiResponse<PagedResult<DummyUser>>> => {
            await delay(DELAY_MS);
            let filtered = [...users];
            if (search) {
                const lowerSearch = search.toLowerCase();
                filtered = filtered.filter(
                    (u) => u.username.toLowerCase().includes(lowerSearch) || u.email.toLowerCase().includes(lowerSearch)
                );
            }
            return mockPagedResponse(filtered, page, pageSize);
        },
        create: async (data: Omit<DummyUser, "id" | "createdAt">): Promise<ApiResponse<DummyUser>> => {
            await delay(DELAY_MS);
            const newUser: DummyUser = {
                ...data,
                id: users.length + 1,
                createdAt: new Date().toISOString(),
            };
            users.unshift(newUser);
            return { returnCode: 0, message: "User created successfully", errors: [], data: newUser };
        },
        delete: async (id: number): Promise<ApiResponse<null>> => {
            await delay(DELAY_MS);
            const index = users.findIndex((u) => u.id === id);
            if (index > -1) {
                users.splice(index, 1);
            }
            return { returnCode: 0, message: "User deleted successfully", errors: [] };
        }
    },
    orders: {
        list: async (page = 1, pageSize = 10): Promise<ApiResponse<PagedResult<DummyOrder>>> => {
            await delay(DELAY_MS);
            return mockPagedResponse(orders, page, pageSize);
        },
    },
};
