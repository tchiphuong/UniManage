import useSWR from "swr";
import { MenuItem } from "@/types";
import { apiClient } from "@/lib/api-client";
import { MENU_ENDPOINTS } from "@/lib/api-endpoints";

/**
 * Backend menu response structure from GET /api/v1/menus
 */
interface MenuApiItem {
    id: number;
    code: string;
    functionCode: string;
    parentCode: string | null;
    resourceKey: string;
    url: string | null;
    icon: string | null;
    children: MenuApiItem[];
}

/**
 * Transform backend menu response to frontend MenuItem format
 */
function mapApiToMenuItem(apiItem: MenuApiItem): MenuItem {
    const menuItem: MenuItem = {
        id: apiItem.code,
        title: apiItem.resourceKey,
        icon: apiItem.icon || undefined,
        link: apiItem.url || undefined,
    };

    if (apiItem.children && apiItem.children.length > 0) {
        menuItem.children = apiItem.children.map(mapApiToMenuItem);
    }

    return menuItem;
}

/**
 * SWR fetcher – calls real backend API
 */
const fetcher = async (): Promise<MenuItem[]> => {
    const response = await apiClient.get<MenuApiItem[]>(MENU_ENDPOINTS.LIST);

    if (response.returnCode !== 0 || !response.data) {
        throw new Error(response.message || "Failed to fetch menu");
    }

    return response.data.map(mapApiToMenuItem);
};

/**
 * Hook to load menu data from API
 * Uses SWR for caching, revalidation, and error handling
 */
export function useMenu() {
    const { data, error, isLoading } = useSWR<MenuItem[]>("/api/v1/menus", fetcher, {
        revalidateOnFocus: false,
        revalidateOnReconnect: true,
        dedupingInterval: 60000, // Cache 60s
    });

    return {
        menuData: data || [],
        isLoading,
        isError: error,
    };
}
