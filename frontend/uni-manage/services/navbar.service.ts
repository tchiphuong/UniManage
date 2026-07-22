import useSWR from "swr";

import { apiClient } from "@/lib/api-client";
import { SYSTEM_ENDPOINTS } from "@/lib/api-endpoints";
import { NavbarItem } from "@/types";

/**
 * Backend navbar response structure from GET /api/v1/system/navbars
 * 3-level hierarchy: Module → Function Group → Function
 */
interface FunctionNode {
    code: string;
    resourceKey: string;
    icon: string | null;
    url: string | null;
    sortOrder: number;
}

interface GroupNode {
    code: string;
    resourceKey: string;
    icon: string | null;
    sort: number;
    children: FunctionNode[];
}

interface ModuleNode {
    code: string;
    resourceKey: string;
    icon: string | null;
    sort: number;
    children: GroupNode[];
}

/**
 * Convert dot-path resourceKey to i18n key path.
 * E.g. "module.system" → "common.module.system"
 *      "func.group.sy.security" → "common.func.group.sy.security"
 *      "func.sy.user" → "common.func.sy.user"
 */
function toTranslationKey(resourceKey: string): string {
    return `common.${resourceKey}`;
}

/**
 * Map API 3-level tree to flat NavbarItem[] structure cho navbar component.
 * Level 1 (Module) → Section header (no link, has children)
 *   Level 2 (Group) → Expandable group (no link, has children)
 *     Level 3 (Function) → Leaf item (has link, no children)
 */
function mapModuleToNavbarItems(modules: ModuleNode[]): NavbarItem[] {
    return modules.map((mod) => {
        const moduleItem: NavbarItem = {
            id: mod.code,
            title: toTranslationKey(mod.resourceKey),
            icon: mod.icon || undefined,
            // Module là section header, không có link
            children: mod.children.map((grp) => {
                const groupItem: NavbarItem = {
                    id: grp.code,
                    title: toTranslationKey(grp.resourceKey),
                    icon: grp.icon || undefined,
                    // Function cũng không có link, chỉ chứa functions
                    children: grp.children.map((func) => {
                        return {
                            id: func.code,
                            title: toTranslationKey(func.resourceKey),
                            icon: func.icon || undefined,
                            link: func.url || undefined,
                        };
                    }),
                };

                return groupItem;
            }),
        };

        return moduleItem;
    });
}

/**
 * SWR fetcher – calls real backend API
 */
const fetcher = async (): Promise<NavbarItem[]> => {
    const response = await apiClient.get<ModuleNode[]>(SYSTEM_ENDPOINTS.NAVBAR);

    if (response.returnCode !== 0 || !response.data) {
        throw new Error(response.message || "Failed to fetch menu");
    }

    return mapModuleToNavbarItems(response.data);
};

/**
 * Hook to load menu data from API
 * Uses SWR for caching, revalidation, and error handling
 */
export function useNavbar() {
    const { data, error, isLoading } = useSWR<NavbarItem[]>(
        SYSTEM_ENDPOINTS.NAVBAR,
        fetcher,
        {
            revalidateOnFocus: false,
            revalidateOnReconnect: true,
            dedupingInterval: 60000, // Cache 60s
            errorRetryCount: 3, // Giới hạn retry tối đa 3 lần nếu API lỗi
        },
    );

    return {
        menuData: data || [],
        isLoading,
        isError: error,
    };
}
