import {
    ArrowPathIcon,
    ChevronDownIcon,
    EyeIcon,
    MagnifyingGlassIcon,
    PencilIcon,
    TrashIcon,
} from "@heroicons/react/24/outline";
import {
    Button,
    Dropdown,
    Input,
    Label,
    Pagination,
    Skeleton,
    SortDescriptor,
    Spinner,
    Table as BaseTable,
    Tooltip,
} from "@heroui/react";
import { ReactNode, useCallback, useMemo } from "react";

export type { SortDescriptor } from "@heroui/react";

// ==================== TYPES ====================

// Column definition
export interface TableColumn<T> {
    key: string;
    label: string;
    width?: number;
    align?: "start" | "center" | "end";
    sortable?: boolean;
    filterable?: boolean;
    filterType?: "text" | "select" | "date" | "number";
    filterOptions?: { key: string; label: string }[];
    render?: (item: T, index: number) => ReactNode;
}

// Action definition
export interface TableAction<T> {
    key: string;
    label: string;
    icon?: ReactNode;
    color?:
        "default" | "primary" | "secondary" | "success" | "warning" | "danger";
    onClick: (item: T) => void;
    isVisible?: (item: T) => boolean;
    isDisabled?: (item: T) => boolean;
}

// Pagination info
export interface TablePagination {
    page: number;
    pageSize: number;
    total: number;
    onPageChange: (page: number) => void;
    pageSizeOptions?: number[];
    onPageSizeChange?: (pageSize: number) => void;
}

// Sort info
export interface TableSort {
    column: string;
    direction: "ascending" | "descending";
}

// Filter value
export interface TableFilter {
    [key: string]: string | number | undefined;
}

export interface CommonTableProps<T extends object> {
    items: T[];
    columns: TableColumn<T>[];
    getRowKey: (item: T) => string | number;

    // Optional features
    isLoading?: boolean;
    enableSkeleton?: boolean;
    skeletonRows?: number;
    emptyContent?: ReactNode;
    loadingContent?: ReactNode;

    // Pagination
    pagination?: TablePagination;
    showPaginationInfo?: boolean;

    // Sorting
    sortDescriptor?: SortDescriptor;
    onSortChange?: (descriptor: SortDescriptor) => void;

    // Filtering
    showSearch?: boolean;
    searchPlaceholder?: string;
    searchValue?: string;
    onSearchChange?: (value: string) => void;
    showFilters?: boolean;
    filters?: TableFilter;
    onFilterChange?: (filters: TableFilter) => void;

    // Column visibility
    visibleColumns?: Set<string> | "all";
    onVisibleColumnsChange?: (columns: Set<string>) => void;
    showColumnToggle?: boolean;

    // Actions column
    actions?: TableAction<T>[];
    onRowAction?: (key: string | number) => void; // Allow row click
    actionsLabel?: string;
    actionsWidth?: number;

    // Toolbar
    toolbarContent?: ReactNode;
    showRefresh?: boolean;
    onRefresh?: () => void;

    // Styling
    isStriped?: boolean;
    isCompact?: boolean;
    selectionMode?: "none" | "single" | "multiple";
    selectedKeys?: Iterable<string | number> | "all";
    onSelectionChange?: (keys: "all" | Set<string | number>) => void;

    // Custom top/bottom content
    topContent?: ReactNode;
    bottomContent?: ReactNode;

    // Layout & Scroll
    isHeaderSticky?: boolean;
    maxHeight?: string; // e.g. "400px" or "calc(100vh - 200px)"

    // Aria
    "aria-label"?: string;
}

// ==================== DEFAULT VALUES ====================

const defaultIcons: Record<string, ReactNode> = {
    view: <EyeIcon className="h-4 w-4 text-gray-500" />,
    edit: <PencilIcon className="h-4 w-4 text-blue-500" />,
    delete: <TrashIcon className="h-4 w-4 text-red-500" />,
};

const defaultColors: Record<
    string,
    "primary" | "secondary" | "success" | "warning" | "danger"
> = {
    view: "secondary",
    edit: "primary",
    delete: "danger",
};

// ==================== COMPONENT ====================

export function Table<T extends object>({
    items = [],
    columns,
    actions = [],
    onRowAction,
    getRowKey,
    isLoading = false,
    enableSkeleton = true,
    skeletonRows = 10,
    emptyContent = "No data found",
    loadingContent,
    pagination,
    showPaginationInfo = true,
    sortDescriptor,
    onSortChange,
    showSearch = false,
    searchPlaceholder = "Search...",
    searchValue = "",
    onSearchChange,
    showFilters = false,
    filters = {},
    onFilterChange,
    visibleColumns = "all",
    onVisibleColumnsChange,
    showColumnToggle = false,
    actionsLabel = "Actions",
    actionsWidth = 120,
    toolbarContent,
    showRefresh = false,
    onRefresh,
    isStriped = true,
    isCompact = false,
    selectionMode = "none",
    selectedKeys,
    onSelectionChange,
    topContent,
    bottomContent,
    isHeaderSticky = true,
    maxHeight,
    "aria-label": ariaLabel = "Data table",
}: Readonly<CommonTableProps<T>>) {
    // Calculate pagination info
    const totalPages = pagination
        ? Math.ceil(pagination.total / pagination.pageSize)
        : 0;
    const startItem = pagination
        ? (pagination.page - 1) * pagination.pageSize + 1
        : 0;
    const endItem = pagination
        ? Math.min(pagination.page * pagination.pageSize, pagination.total)
        : items.length;

    // Determine if we should show skeleton
    const showSkeleton = isLoading && enableSkeleton && items.length === 0;

    // Prepare items for rendering
    // If showing skeleton, use dummy array. Cast to any[] since T is unknown.
    // The items itself will be undefined but we just need the length.
    const displayItems = showSkeleton
        ? (Array.from({ length: skeletonRows }).map((_, i) => ({
              _isSkeleton: true,
              id: `skeleton-${i}`,
          })) as unknown as T[])
        : items;

    // Override isLoading if showing skeleton (to prevent spinner)
    const tableIsLoading = isLoading && !showSkeleton;

    // Get filterable columns
    const filterableColumns = useMemo(
        () => columns.filter((col) => col.filterable),
        [columns],
    );

    // Handle sort change
    const handleSortChange = useCallback(
        (descriptor: SortDescriptor) => {
            onSortChange?.(descriptor);
        },
        [onSortChange],
    );

    // Handle filter change
    const handleFilterChange = useCallback(
        (key: string, value: string) => {
            onFilterChange?.({
                ...filters,
                [key]: value || undefined,
            });
        },
        [filters, onFilterChange],
    );

    const renderCell = (item: T, columnKey: string, index: number) => {
        // Check for skeleton
        if ((item as any)._isSkeleton) {
            return (
                <Skeleton className="w-full rounded-lg">
                    <div className="bg-default-200 h-3 w-4/5 rounded-lg"></div>
                </Skeleton>
            );
        }

        // Actions column
        if (columnKey === "_actions" && actions) {
            return (
                <div className="flex items-center gap-1">
                    {actions.map((action) => {
                        if (action.isVisible && !action.isVisible(item)) {
                            return null;
                        }

                        const isDisabled = action.isDisabled
                            ? action.isDisabled(item)
                            : false;
                        const icon = action.icon || defaultIcons[action.key];

                        let textColorClass = "text-primary";
                        if (action.color === "danger")
                            textColorClass = "text-danger";
                        else if (action.color === "success")
                            textColorClass = "text-success";

                        return (
                            <Tooltip key={action.key}>
                                <Tooltip.Trigger>
                                    <Button
                                        className={`hover:bg-default-100 min-h-8 min-w-8 bg-transparent p-1 ${textColorClass}`}
                                        aria-label={action.label}
                                        isDisabled={isDisabled}
                                        onPress={() => action.onClick(item)}
                                    >
                                        {icon}
                                    </Button>
                                </Tooltip.Trigger>
                                <Tooltip.Content>
                                    {action.label}
                                </Tooltip.Content>
                            </Tooltip>
                        );
                    })}
                </div>
            );
        }

        // Custom render function
        const column = columns.find((col) => col.key === columnKey);
        if (column?.render) {
            return column.render(item, index);
        }

        // Default: access property by key
        const value = (item as Record<string, unknown>)[columnKey];
        if (value === null || value === undefined) {
            return <span className="text-gray-400">-</span>;
        }
        if (typeof value === "object") {
            return JSON.stringify(value);
        }
        // eslint-disable-next-line @typescript-eslint/no-base-to-string
        return String(value);
    };

    // ==================== TOOLBAR ====================

    // Get visible columns for rendering
    const displayColumns = useMemo(() => {
        if (visibleColumns === "all") return columns;
        return columns.filter((col) => visibleColumns.has(col.key));
    }, [columns, visibleColumns]);

    // Build visible columns Set for rendering (includes actions)
    const tableColumnsFiltered = useMemo(
        () => [
            ...displayColumns,
            ...(actions && actions.length > 0
                ? [
                      {
                          key: "_actions",
                          label: actionsLabel,
                          width: actionsWidth,
                      },
                  ]
                : []),
        ],
        [displayColumns, actions, actionsLabel, actionsWidth],
    );

    const defaultTopContent = (
        <div className="flex flex-col gap-4">
            {/* Main toolbar row */}
            <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
                {/* Left side: Search + Filter dropdowns */}
                <div className="flex flex-1 flex-wrap items-center gap-3">
                    {showSearch && (
                        <div className="relative w-full sm:max-w-50">
                            <MagnifyingGlassIcon className="absolute top-1/2 left-3 h-4 w-4 -translate-y-1/2 text-gray-400" />
                            <Input
                                className="w-full pl-10"
                                placeholder={searchPlaceholder}
                                value={searchValue}
                                onChange={(e) =>
                                    onSearchChange?.(e.target.value)
                                }
                            />
                        </div>
                    )}

                    {/* Inline filter dropdowns */}
                    {showFilters &&
                        filterableColumns.map((column) => {
                            if (
                                column.filterType === "select" &&
                                column.filterOptions
                            ) {
                                return (
                                    <Dropdown key={column.key}>
                                        <Dropdown.Trigger>
                                            <Button
                                                variant="ghost"
                                                size="sm"
                                                className="hidden sm:flex"
                                            >
                                                {column.label}
                                                {filters[column.key] && (
                                                    <span className="text-primary ml-1">
                                                        ({filters[column.key]})
                                                    </span>
                                                )}
                                                <ChevronDownIcon className="ml-1 h-4 w-4" />
                                            </Button>
                                        </Dropdown.Trigger>
                                        <Dropdown.Popover>
                                            <Dropdown.Menu
                                                disallowEmptySelection={false}
                                                aria-label={`Filter by ${column.label}`}
                                                selectedKeys={
                                                    filters[column.key]
                                                        ? new Set([
                                                              String(
                                                                  filters[
                                                                      column.key
                                                                  ],
                                                              ),
                                                          ])
                                                        : new Set()
                                                }
                                                selectionMode="single"
                                                onSelectionChange={(keys) => {
                                                    const selected =
                                                        Array.from(keys)[0];
                                                    handleFilterChange(
                                                        column.key,
                                                        selected
                                                            ? String(selected)
                                                            : "",
                                                    );
                                                }}
                                            >
                                                {column.filterOptions.map(
                                                    (opt) => (
                                                        <Dropdown.Item
                                                            id={opt.key}
                                                            key={opt.key}
                                                            textValue={
                                                                opt.label
                                                            }
                                                        >
                                                            <Label>
                                                                {opt.label}
                                                            </Label>
                                                        </Dropdown.Item>
                                                    ),
                                                )}
                                            </Dropdown.Menu>
                                        </Dropdown.Popover>
                                    </Dropdown>
                                );
                            }
                            return null;
                        })}
                </div>

                {/* Right side: Custom content, rows per page & Refresh */}
                <div className="flex items-center gap-3">
                    {pagination && (
                        <span className="text-small text-default-400">
                            Total: <strong>{pagination.total}</strong> row(s)
                        </span>
                    )}

                    {toolbarContent}

                    {pagination?.onPageSizeChange && (
                        <div className="flex items-center gap-1">
                            <Dropdown>
                                <Dropdown.Trigger>
                                    <Button variant="ghost" size="sm">
                                        {pagination.pageSize} / page
                                        <ChevronDownIcon className="h-4 w-4" />
                                    </Button>
                                </Dropdown.Trigger>
                                <Dropdown.Popover>
                                    <Dropdown.Menu
                                        aria-label="Rows per page"
                                        selectionMode="single"
                                        selectedKeys={
                                            new Set([
                                                String(pagination.pageSize),
                                            ])
                                        }
                                        onSelectionChange={(keys) => {
                                            const selected =
                                                Array.from(keys)[0];
                                            if (selected) {
                                                pagination.onPageSizeChange?.(
                                                    Number(selected),
                                                );
                                            }
                                        }}
                                    >
                                        {(
                                            pagination.pageSizeOptions || [
                                                5, 10, 20, 50,
                                            ]
                                        ).map((size) => (
                                            <Dropdown.Item
                                                id={String(size)}
                                                key={String(size)}
                                                textValue={String(size)}
                                            >
                                                <Label>{String(size)}</Label>
                                            </Dropdown.Item>
                                        ))}
                                    </Dropdown.Menu>
                                </Dropdown.Popover>
                            </Dropdown>
                        </div>
                    )}

                    {showRefresh && (
                        <Tooltip>
                            <Tooltip.Trigger>
                                <Button
                                    isIconOnly
                                    variant="ghost"
                                    size="sm"
                                    isPending={isLoading}
                                    onPress={onRefresh}
                                >
                                    <ArrowPathIcon
                                        className={`h-4 w-4 ${isLoading ? "animate-spin" : ""}`}
                                    />
                                </Button>
                            </Tooltip.Trigger>
                            <Tooltip.Content>Refresh</Tooltip.Content>
                        </Tooltip>
                    )}
                </div>
            </div>
        </div>
    );

    // ==================== PAGINATION ====================
    const defaultBottomContent = pagination && totalPages > 0 && (
        <div className="flex items-center justify-between px-2 py-2">
            <span className="text-small text-default-400 hidden w-[30%] sm:block">
                {selectionMode !== "none" &&
                selectedKeys &&
                selectedKeys !== "all" ? (
                    <>
                        {selectedKeys instanceof Set ? selectedKeys.size : 0} of{" "}
                        {pagination.total} selected
                    </>
                ) : (
                    <>
                        Showing {startItem}-{endItem} of {pagination.total}
                    </>
                )}
            </span>

            <Pagination
                size="sm"
                className="flex w-full justify-center sm:w-auto"
            >
                <Pagination.Content>
                    <Pagination.Item>
                        <Pagination.Previous
                            isDisabled={pagination.page <= 1}
                            onPress={() =>
                                pagination.onPageChange(pagination.page - 1)
                            }
                        >
                            Previous
                        </Pagination.Previous>
                    </Pagination.Item>

                    {totalPages > 0 && (
                        <>
                            <Pagination.Item>
                                <Pagination.Link
                                    isActive={pagination.page === 1}
                                    onPress={() => pagination.onPageChange(1)}
                                >
                                    1
                                </Pagination.Link>
                            </Pagination.Item>

                            {pagination.page > 3 && totalPages > 3 && (
                                <Pagination.Item>
                                    <Pagination.Ellipsis />
                                </Pagination.Item>
                            )}

                            {[
                                pagination.page - 1,
                                pagination.page,
                                pagination.page + 1,
                            ]
                                .filter((p) => p > 1 && p < totalPages)
                                .map((p) => (
                                    <Pagination.Item key={p}>
                                        <Pagination.Link
                                            isActive={pagination.page === p}
                                            onPress={() =>
                                                pagination.onPageChange(p)
                                            }
                                        >
                                            {p}
                                        </Pagination.Link>
                                    </Pagination.Item>
                                ))}

                            {pagination.page < totalPages - 2 &&
                                totalPages > 3 && (
                                    <Pagination.Item>
                                        <Pagination.Ellipsis />
                                    </Pagination.Item>
                                )}

                            {totalPages > 1 && (
                                <Pagination.Item>
                                    <Pagination.Link
                                        isActive={
                                            pagination.page === totalPages
                                        }
                                        onPress={() =>
                                            pagination.onPageChange(totalPages)
                                        }
                                    >
                                        {totalPages}
                                    </Pagination.Link>
                                </Pagination.Item>
                            )}
                        </>
                    )}

                    <Pagination.Item>
                        <Pagination.Next
                            isDisabled={pagination.page >= totalPages}
                            onPress={() =>
                                pagination.onPageChange(pagination.page + 1)
                            }
                        >
                            Next
                        </Pagination.Next>
                    </Pagination.Item>
                </Pagination.Content>
            </Pagination>

            <div className="hidden w-[30%] sm:block"></div>
        </div>
    );

    // ==================== RENDER ====================

    return (
        <div className="flex flex-col gap-4">
            {topContent !== undefined ? topContent : defaultTopContent}

            <BaseTable className="w-full">
                <BaseTable.ScrollContainer>
                    <BaseTable.Content
                        aria-label={ariaLabel}
                        selectionMode={selectionMode as any}
                        selectedKeys={selectedKeys as any}
                        onSelectionChange={onSelectionChange as any}
                        sortDescriptor={sortDescriptor}
                        onSortChange={handleSortChange}
                        className={maxHeight ? `max-h-[${maxHeight}]` : ""}
                    >
                        <BaseTable.Header>
                            {tableColumnsFiltered.map((col) => (
                                <BaseTable.Column
                                    key={col.key}
                                    id={col.key}
                                    allowsSorting={col.sortable}
                                >
                                    {col.sortable
                                        ? ({ sortDirection }) => (
                                              <BaseTable.SortableColumnHeader
                                                  sortDirection={sortDirection}
                                              >
                                                  {col.label}
                                              </BaseTable.SortableColumnHeader>
                                          )
                                        : col.label}
                                </BaseTable.Column>
                            ))}
                        </BaseTable.Header>

                        <BaseTable.Body
                            renderEmptyState={() => (
                                <div className="text-default-500 flex w-full flex-col items-center justify-center py-10">
                                    {tableIsLoading
                                        ? loadingContent || (
                                              <Spinner size="md" />
                                          )
                                        : emptyContent}
                                </div>
                            )}
                        >
                            {displayItems.map((item, index) => {
                                const key = (item as any)._isSkeleton
                                    ? (item as any).id
                                    : getRowKey(item);

                                return (
                                    <BaseTable.Row
                                        key={key}
                                        id={key}
                                        className={
                                            onRowAction &&
                                            !(item as any)._isSkeleton
                                                ? "hover:bg-default-100 cursor-pointer"
                                                : ""
                                        }
                                    >
                                        {tableColumnsFiltered.map((col) => (
                                            <BaseTable.Cell key={col.key}>
                                                {renderCell(
                                                    item,
                                                    col.key,
                                                    index,
                                                )}
                                            </BaseTable.Cell>
                                        ))}
                                    </BaseTable.Row>
                                );
                            })}
                        </BaseTable.Body>
                    </BaseTable.Content>
                </BaseTable.ScrollContainer>
            </BaseTable>

            {bottomContent !== undefined ? bottomContent : defaultBottomContent}
        </div>
    );
}
