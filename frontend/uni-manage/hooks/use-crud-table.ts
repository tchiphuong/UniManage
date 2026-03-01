import { PagedResponse, PagingParams } from "@/types";
import { useDataTable } from "./use-data-table";
import { useDataModal } from "./use-data-modal";
import { useConfirmDelete } from "./use-confirm-delete";

interface UseCrudTableProps<T, TParams extends PagingParams> {
    queryKey: string;
    listService: (params: TParams) => Promise<PagedResponse<T>>;
    deleteService: (ids: number[]) => Promise<any>;
    defaultPageSize?: number;
}

export function useCrudTable<T extends { id: number }, TParams extends PagingParams = any>({
    queryKey,
    listService,
    deleteService,
    defaultPageSize = 20
}: UseCrudTableProps<T, TParams>) {
    
    // 1. Data Table (Fetching, Pagination, Search)
    const table = useDataTable<T, TParams>({
        queryKey,
        listService,
        defaultPageSize
    });

    // 2. Modal (Create/Edit)
    const modal = useDataModal<T>();

    // 3. Delete (Confirmation & Mutation)
    const confirmDelete = useConfirmDelete({
        queryKey,
        deleteService
    });

    return {
        ...table,
        ...modal,
        ...confirmDelete,
    };
}

