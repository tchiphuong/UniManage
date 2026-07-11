"use client";

import React, { useState } from "react";
import { Spinner, Button, Input } from "@heroui/react";

export interface Column<T = any> {
    uid: string;
    name: string;
    sortable?: boolean;
    filterable?: boolean;
    width?: string;
    minWidth?: string;
    render?: (item: T) => React.ReactNode;
}

interface SortDescriptor {
    column: string;
    direction: "ascending" | "descending";
}

interface DataTableProps<T> {
    columns: Column[];
    data: T[];
    isLoading?: boolean;
    page?: number;
    totalPages?: number;
    onPageChange?: (page: number) => void;
    sortDescriptor?: SortDescriptor;
    onSortChange?: (descriptor: SortDescriptor) => void;
    renderCell?: (item: T, columnKey: React.Key) => React.ReactNode;
    emptyContent?: string;
    striped?: boolean;
}

const SortIcon = ({
    direction,
}: {
    direction?: "ascending" | "descending";
}) => {
    if (!direction) {
        return (
            <svg
                className="h-4 w-4 opacity-30"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
            >
                <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M7 16V4m0 0L3 8m4-4l4 4m6 0v12m0 0l4-4m-4 4l-4-4"
                />
            </svg>
        );
    }
    return (
        <svg
            className="h-4 w-4"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
        >
            {direction === "ascending" ? (
                <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M5 15l7-7 7 7"
                />
            ) : (
                <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M19 9l-7 7-7-7"
                />
            )}
        </svg>
    );
};

/**
 * DataTable Component
 * Feature-rich table with sorting, filtering, column resizing, and striped rows
 */
export function DataTable<T extends { id?: string | number }>({
    columns,
    data,
    isLoading = false,
    page = 1,
    totalPages = 1,
    onPageChange,
    sortDescriptor,
    onSortChange,
    renderCell,
    emptyContent = "No data available",
    striped = true,
}: DataTableProps<T>) {
    const [columnFilters, setColumnFilters] = useState<Record<string, string>>(
        {},
    );
    const [columnWidths, setColumnWidths] = useState<Record<string, string>>(
        {},
    );

    const getKeyValue = (item: any, key: React.Key) => {
        return item[key as keyof typeof item];
    };

    const defaultRenderCell = (item: T, columnKey: React.Key) => {
        return getKeyValue(item, columnKey);
    };

    const actualRenderCell = renderCell || defaultRenderCell;

    const handleSort = (columnUid: string) => {
        if (!onSortChange) return;

        const newDirection =
            sortDescriptor?.column === columnUid &&
            sortDescriptor.direction === "ascending"
                ? "descending"
                : "ascending";

        onSortChange({ column: columnUid, direction: newDirection });
    };

    const handleFilterChange = (columnUid: string, value: string) => {
        setColumnFilters((prev) => ({
            ...prev,
            [columnUid]: value,
        }));
    };

    // Filter data locally if filters are set
    const filteredData = React.useMemo(() => {
        if (Object.keys(columnFilters).length === 0) return data;

        return data.filter((item) => {
            return Object.entries(columnFilters).every(
                ([columnUid, filterValue]) => {
                    if (!filterValue) return true;
                    const cellValue = getKeyValue(item, columnUid);
                    return String(cellValue)
                        .toLowerCase()
                        .includes(filterValue.toLowerCase());
                },
            );
        });
    }, [data, columnFilters]);

    const getColumnWidth = (column: Column) => {
        return columnWidths[column.uid] || column.width || "auto";
    };

    return (
        <div className="flex flex-col gap-4">
            {/* Table */}
            <div className="border-default-200 relative overflow-x-auto rounded-lg border">
                <table className="w-full table-fixed text-left text-sm">
                    <thead className="bg-default-100 dark:bg-default-50 text-xs uppercase">
                        <tr>
                            {columns.map((column) => (
                                <th
                                    key={column.uid}
                                    scope="col"
                                    className="text-default-700 relative px-6 py-3 font-semibold"
                                    style={{
                                        width: getColumnWidth(column),
                                        minWidth: column.minWidth,
                                    }}
                                >
                                    <div className="flex items-center justify-between gap-2">
                                        <span className="truncate">
                                            {column.name}
                                        </span>
                                        {column.sortable && (
                                            <button
                                                onClick={() =>
                                                    handleSort(column.uid)
                                                }
                                                className="hover:bg-default-200 rounded p-1 transition-colors"
                                                aria-label={`Sort by ${column.name}`}
                                            >
                                                <SortIcon
                                                    direction={
                                                        sortDescriptor?.column ===
                                                        column.uid
                                                            ? sortDescriptor.direction
                                                            : undefined
                                                    }
                                                />
                                            </button>
                                        )}
                                    </div>
                                </th>
                            ))}
                        </tr>
                        {/* Filter Row */}
                        {columns.some((col) => col.filterable) && (
                            <tr className="bg-default-50">
                                {columns.map((column) => (
                                    <th
                                        key={`filter-${column.uid}`}
                                        className="px-6 py-2"
                                    >
                                        {column.filterable && (
                                            <Input
                                                size="sm"
                                                placeholder={`Filter ${column.name}...`}
                                                value={
                                                    columnFilters[column.uid] ||
                                                    ""
                                                }
                                                onChange={(e) =>
                                                    handleFilterChange(
                                                        column.uid,
                                                        e.target.value,
                                                    )
                                                }
                                                className="w-full"
                                                classNames={{
                                                    input: "text-xs",
                                                    inputWrapper: "h-8 min-h-8",
                                                }}
                                            />
                                        )}
                                    </th>
                                ))}
                            </tr>
                        )}
                    </thead>
                    <tbody>
                        {isLoading ? (
                            <tr>
                                <td
                                    colSpan={columns.length}
                                    className="px-6 py-12 text-center"
                                >
                                    <div className="flex items-center justify-center">
                                        <Spinner size="lg" />
                                    </div>
                                </td>
                            </tr>
                        ) : filteredData && filteredData.length > 0 ? (
                            filteredData.map((item, index) => (
                                <tr
                                    key={item.id ?? index}
                                    className={`border-default-200 hover:bg-default-100 border-b transition-colors dark:hover:bg-gray-700 ${
                                        striped && index % 2 === 1
                                            ? "bg-default-50 dark:bg-gray-800"
                                            : "bg-white dark:bg-gray-900"
                                    }`}
                                >
                                    {columns.map((column) => (
                                        <td
                                            key={column.uid}
                                            className="text-default-700 truncate px-6 py-4"
                                            title={String(
                                                actualRenderCell(
                                                    item,
                                                    column.uid,
                                                ),
                                            )}
                                        >
                                            {actualRenderCell(item, column.uid)}
                                        </td>
                                    ))}
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td
                                    colSpan={columns.length}
                                    className="text-default-500 px-6 py-8 text-center"
                                >
                                    {emptyContent}
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>

            {/* Pagination */}
            {totalPages > 1 && (
                <div className="flex w-full items-center justify-between">
                    <span className="text-default-600 text-sm">
                        Showing {filteredData.length} of {data.length} entries
                    </span>
                    <div className="flex items-center gap-2">
                        <Button
                            size="sm"
                            variant="ghost"
                            onPress={() =>
                                onPageChange?.(Math.max(1, page - 1))
                            }
                            isDisabled={page <= 1}
                        >
                            Previous
                        </Button>
                        <span className="text-default-600 text-sm">
                            Page {page} of {totalPages}
                        </span>
                        <Button
                            size="sm"
                            variant="ghost"
                            onPress={() =>
                                onPageChange?.(Math.min(totalPages, page + 1))
                            }
                            isDisabled={page >= totalPages}
                        >
                            Next
                        </Button>
                    </div>
                </div>
            )}
        </div>
    );
}
