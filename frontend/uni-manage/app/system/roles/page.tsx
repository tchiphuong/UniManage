"use client";

import { useTranslations } from "next-intl";
import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Column, DataTable } from "@/components/common/data-table";
import { RoleService, Role } from "@/services/role.service";
import { PageHeader } from "@/components/page-header-i18n";
import { useDebounce } from "@/hooks/use-debounce";
import { Input, Chip } from '@heroui/react';
import { Icon } from "@iconify/react";
import { RoleModal } from "./components/role-modal";
import { ConfirmModal } from "@/components/common/confirm-modal";
import { addToast } from "@heroui/toast";

// Icon components
const EditIcon = (props: any) => <Icon icon="solar:pen-bold" {...props} />;
const DeleteIcon = (props: any) => <Icon icon="solar:trash-bin-trash-bold" {...props} />;
const PlusIcon = (props: any) => <Icon icon="solar:add-circle-bold" {...props} />;

export default function RolesPage() {
    const t = useTranslations("common");
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(20);
    const [keyword, setKeyword] = useState("");
    const debouncedKeyword = useDebounce(keyword, 500);

    // Modal State
    const [isOpen, setIsOpen] = useState(false);
    const onClose = () => setIsOpen(false);
    const onOpen = () => setIsOpen(true);

    const [selectedRole, setSelectedRole] = useState<Role | null>(null);

    // Confirm Delete State
    const [deleteCode, setDeleteCode] = useState<string | null>(null);
    const [isDeleteOpen, setIsDeleteOpen] = useState(false);

    const queryClient = useQueryClient();

    const { data, isLoading } = useQuery({
        queryKey: ["roles", pageIndex, pageSize, debouncedKeyword],
        queryFn: () =>
            RoleService.list({
                pageIndex,
                pageSize,
                keyword: debouncedKeyword,
            }),
    });

    const deleteMutation = useMutation({
        mutationFn: (roleCodes: string[]) => RoleService.delete(roleCodes),
        onSuccess: (data: any) => {
            queryClient.invalidateQueries({ queryKey: ["roles"] });
            addToast({
                title: "Success",
                description: data?.message || "Role deleted successfully",
                color: "success",
            });
            setDeleteCode(null);
            setIsDeleteOpen(false);
        },
        onError: (error) => {
            console.error("Delete failed", error);
            // @ts-ignore
            addToast({
                title: "Error",
                description: (error as any)?.message || "Failed to delete role",
                color: "danger",
            });
        }
    });

    const handleCreate = () => {
        setSelectedRole(null);
        onOpen();
    };

    const handleEdit = (role: Role) => {
        setSelectedRole(role);
        onOpen();
    };

    const handleDeleteClick = (code: string) => {
        setDeleteCode(code);
        setIsDeleteOpen(true);
    };

    const confirmDelete = () => {
        if (deleteCode) {
            deleteMutation.mutate([deleteCode]);
        }
    };

    const columns: Column<Role>[] = [
        {
            uid: "roleCode",
            name: "Role Code",
            sortable: true
        },
        {
            uid: "roleName",
            name: "Role Name",
            sortable: true
        },
        {
            uid: "description",
            name: "Description",
        },
        {
            uid: "isActive",
            name: "Status",
            render: (role: Role) => (
                <Chip
                    className="capitalize"
                    color={role.isActive === 1 ? "success" : "danger"}
                    size="sm"
                    variant="soft"
                >
                    {role.isActive === 1 ? "Active" : "Inactive"}
                </Chip>
            ),
        },
        {
            uid: "createdAt",
            name: "Created At",
            sortable: true,
            render: (role: Role) => new Date(role.createdAt).toLocaleDateString("vi-VN"),
        },
        {
            uid: "actions",
            name: "Actions",
            render: (role: Role) => (
                <div className="relative flex items-center gap-2">
                    <span className="text-lg text-default-400 cursor-pointer active:opacity-50" onClick={() => handleEdit(role)} title="Edit role">
                        <EditIcon />
                    </span>
                    <span className="text-lg text-danger cursor-pointer active:opacity-50" onClick={() => handleDeleteClick(role.roleCode)} title="Delete role">
                        <DeleteIcon />
                    </span>
                </div>
            )
        }
    ];

    return (
        <div className="space-y-6">
            <PageHeader
                title="Roles Management"
                description="Manage system roles and permissions"
                action={{
                    label: "Add Role",
                    onClick: handleCreate,
                    icon: <PlusIcon />
                }}
            />

            <div className="bg-white dark:bg-gray-800 rounded-lg shadow p-4">
                <div className="mb-4 flex flex-col sm:flex-row justify-between gap-4">
                    <Input
                        className="w-full sm:max-w-[44%]"
                        placeholder="Search roles..."
                        value={keyword}
                        onChange={(e) => setKeyword(e.target.value)}
                        startContent={<Icon icon="solar:magnifer-linear" className="text-default-400" />}
                    />
                </div>

                <DataTable
                    columns={columns}
                    data={data?.data?.items || []}
                    totalPages={data?.data?.paging?.totalPages || 1}
                    page={pageIndex}
                    onPageChange={setPageIndex}
                    isLoading={isLoading}
                />
            </div>

            <RoleModal
                isOpen={isOpen}
                onClose={onClose}
                role={selectedRole}
            />

            <ConfirmModal
                isOpen={isDeleteOpen}
                onClose={() => setIsDeleteOpen(false)}
                onConfirm={confirmDelete}
                title="Confirm Delete"
                message={`Are you sure you want to delete role "${deleteCode}"?`}
                confirmLabel="Delete"
                cancelLabel="Cancel"
                isLoading={deleteMutation.isPending}
                variant="danger"
            />
        </div>
    );
}
