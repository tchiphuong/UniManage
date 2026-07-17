import React from "react";
import { Skeleton, Card, Separator } from "@heroui/react";

export const CardSkeleton = () => {
    return (
        <Card className="w-full space-y-5 p-4 rounded-lg">
            <Skeleton className="rounded-lg">
                <div className="h-32 rounded-lg bg-default-300" />
            </Skeleton>
            <div className="space-y-3">
                <Skeleton className="w-3/5 rounded-lg">
                    <div className="h-3 w-3/5 rounded-lg bg-default-200" />
                </Skeleton>
                <Skeleton className="w-4/5 rounded-lg">
                    <div className="h-3 w-4/5 rounded-lg bg-default-200" />
                </Skeleton>
                <Skeleton className="w-2/5 rounded-lg">
                    <div className="h-3 w-2/5 rounded-lg bg-default-300" />
                </Skeleton>
            </div>
        </Card>
    );
};

export const TableSkeleton = ({ rows = 5 }: { rows?: number }) => {
    return (
        <div className="w-full">
            {/* Toolbar/Header placeholder */}
            <div className="mb-4 flex items-center justify-between gap-3">
                <Skeleton className="h-10 w-1/3 rounded-lg" />
                <div className="flex gap-2">
                    <Skeleton className="h-10 w-24 rounded-lg" />
                    <Skeleton className="h-10 w-24 rounded-lg" />
                </div>
            </div>
            
            {/* Table wrapper placeholder */}
            <Card className="w-full p-0 shadow-sm">
                <div className="flex flex-col w-full">
                    {/* Header Row */}
                    <div className="bg-default-100 flex items-center h-12 px-4 gap-4">
                        <Skeleton className="h-4 w-1/4 rounded-md" />
                        <Skeleton className="h-4 w-1/4 rounded-md" />
                        <Skeleton className="h-4 w-1/4 rounded-md" />
                        <Skeleton className="h-4 w-1/4 rounded-md" />
                    </div>
                    <Separator />
                    
                    {/* Body Rows */}
                    {Array.from({ length: rows }).map((_, i) => (
                        // eslint-disable-next-line react/no-array-index-key
                        <React.Fragment key={i}>
                            <div className="flex items-center h-14 px-4 gap-4 hover:bg-default-50">
                                <Skeleton className="h-4 w-1/4 rounded-md" />
                                <Skeleton className="h-4 w-1/4 rounded-md" />
                                <Skeleton className="h-4 w-1/4 rounded-md" />
                                <Skeleton className="h-8 w-1/4 rounded-md" />
                            </div>
                            {i < rows - 1 && <Separator />}
                        </React.Fragment>
                    ))}
                </div>
                
                {/* Pagination placeholder */}
                <Separator />
                <div className="flex items-center justify-between p-4 bg-default-50">
                    <Skeleton className="h-4 w-32 rounded-md" />
                    <Skeleton className="h-8 w-64 rounded-md" />
                </div>
            </Card>
        </div>
    );
};

export const FormSkeleton = ({ fields = 4 }: { fields?: number }) => {
    return (
        <Card className="w-full space-y-6 p-6 rounded-lg">
            {/* Form Header */}
            <div className="space-y-2">
                <Skeleton className="w-1/4 rounded-lg">
                    <div className="h-6 w-1/4 rounded-lg bg-default-300" />
                </Skeleton>
                <Skeleton className="w-2/4 rounded-lg">
                    <div className="h-4 w-2/4 rounded-lg bg-default-200" />
                </Skeleton>
            </div>
            <Separator />
            {/* Form Fields */}
            <div className="space-y-4">
                {Array.from({ length: fields }).map((_, i) => (
                    // eslint-disable-next-line react/no-array-index-key
                    <div key={i} className="space-y-2">
                        <Skeleton className="w-1/5 rounded-md">
                            <div className="h-4 w-1/5 rounded-md bg-default-200" />
                        </Skeleton>
                        <Skeleton className="w-full rounded-lg">
                            <div className="h-10 w-full rounded-lg bg-default-100" />
                        </Skeleton>
                    </div>
                ))}
            </div>
            {/* Actions */}
            <div className="flex justify-end gap-3 pt-4">
                <Skeleton className="h-10 w-24 rounded-lg" />
                <Skeleton className="h-10 w-24 rounded-lg" />
            </div>
        </Card>
    );
};
