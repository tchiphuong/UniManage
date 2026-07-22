import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { useState } from "react";

import { PagedResponse, PagingParams } from "@/types";

import { useDebounce } from "./use-debounce";

interface UseDataTableProps<T, TParams extends PagingParams> {
    queryKey: string;
    listService: (params: TParams) => Promise<PagedResponse<T>>;
    defaultPageSize?: number;
}

export function useDataTable<T, TParams extends PagingParams = PagingParams>({
    queryKey,
    listService,
    defaultPageSize = 20,
}: UseDataTableProps<T, TParams>) {
    // State
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(defaultPageSize);
    const [keyword, setKeyword] = useState("");
    const debouncedKeyword = useDebounce(keyword, 500);

    // Query params
    const queryParams: TParams = {
        pageIndex,
        pageSize,
        keyword: debouncedKeyword,
    } as unknown as TParams;

    // Data fetching
    const { data, isLoading, isFetching, refetch } = useQuery({
        queryKey: [queryKey, pageIndex, pageSize, debouncedKeyword],
        queryFn: () => listService(queryParams),
        placeholderData: keepPreviousData,
    });

    return {
        items: data?.data?.items || [],
        paging: data?.data?.paging || {
            pageIndex: 1,
            pageSize,
            totalItems: 0,
            totalPages: 0,
        },
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
