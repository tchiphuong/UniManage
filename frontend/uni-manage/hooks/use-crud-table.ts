import { PagedResponse, PagingParams } from "@/types";

import { useConfirmDelete } from "./use-confirm-delete";
import { useDataModal } from "./use-data-modal";
import { useDataTable } from "./use-data-table";

interface UseCrudTableProps<T, TParams extends PagingParams> {
    queryKey: string;
    listService: (params: TParams) => Promise<PagedResponse<T>>;
    deleteService: (ids: number[]) => Promise<unknown>;
    defaultPageSize?: number;
}

export function useCrudTable<
    T extends { id: number },
    TParams extends PagingParams = PagingParams,
>({
    queryKey,
    listService,
    deleteService,
    defaultPageSize = 20,
}: UseCrudTableProps<T, TParams>) {
    // 1. Data Table (Fetching, Pagination, Search)
    const table = useDataTable<T, TParams>({
        queryKey,
        listService,
        defaultPageSize,
    });

    // 2. Modal (Create/Edit)
    const modal = useDataModal<T>();

    // 3. Delete (Confirmation & Mutation)
    const confirmDelete = useConfirmDelete({
        queryKey,
        deleteService,
    });

    return {
        ...table,
        ...modal,
        ...confirmDelete,
    };
}
