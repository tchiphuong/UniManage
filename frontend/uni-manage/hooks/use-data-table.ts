import { useState } from "react";
import { useQuery, keepPreviousData } from "@tanstack/react-query";
import { useDebounce } from "./use-debounce";
import { PagedResponse, PagingParams } from "@/types";

interface UseDataTableProps<T, TParams extends PagingParams> {
    queryKey: string;
    listService: (params: TParams) => Promise<PagedResponse<T>>;
    defaultPageSize?: number;
}

export function useDataTable<T, TParams extends PagingParams = any>({
    queryKey,
    listService,
    defaultPageSize = 20
}: UseDataTableProps<T, TParams>) {
    // State
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(defaultPageSize);
    const [keyword, setKeyword] = useState("");
    const debouncedKeyword = useDebounce(keyword, 500);

    // Query params
    const queryParams: any = {
        pageIndex,
        pageSize,
        keyword: debouncedKeyword,
    };

    // Data fetching
    const { data, isLoading, isFetching, refetch } = useQuery({
        queryKey: [queryKey, pageIndex, pageSize, debouncedKeyword],
        queryFn: () => listService(queryParams),
        placeholderData: keepPreviousData,
    });

    return {
        items: data?.data?.items || [],
        paging: data?.data?.paging || { pageIndex: 1, pageSize, totalItems: 0, totalPages: 0 },
        isLoading,
        isFetching,
        refetch,
        keyword,
        setKeyword,
        pageIndex,
        setPageIndex,
        pageSize,
        setPageSize,
    };
}
