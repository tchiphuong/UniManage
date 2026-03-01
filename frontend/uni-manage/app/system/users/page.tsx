"use client";

import { useTranslations } from "next-intl";
import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Column, DataTable } from "@/components/common/data-table";
import { UserService, User } from "@/services/user.service";
import { PageHeader } from "@/components/page-header-i18n";
import { useDebounce } from "@/hooks/use-debounce";
import { Input, Chip } from '@heroui/react';
import { Icon } from "@iconify/react";
import { UserModal } from "./components/user-modal";
import { ConfirmModal } from "@/components/common/confirm-modal";
import { addToast } from "@heroui/toast";

// Icon components
const EditIcon = (props: any) => <Icon icon="solar:pen-bold" {...props} />;
const DeleteIcon = (props: any) => <Icon icon="solar:trash-bin-trash-bold" {...props} />;
const EyeIcon = (props: any) => <Icon icon="solar:eye-bold" {...props} />;
const PlusIcon = (props: any) => <Icon icon="solar:add-circle-bold" {...props} />;
const SearchIcon = (props: any) => <Icon icon="solar:magnifer-linear" {...props} />;

export default function UsersPage() {
    const t = useTranslations("common");
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(20);
    const [keyword, setKeyword] = useState("");
    const debouncedKeyword = useDebounce(keyword, 500);

    // Modal State
    const [isOpen, setIsOpen] = useState(false);
    const onClose = () => setIsOpen(false);
    const onOpen = () => setIsOpen(true);

    const [selectedUser, setSelectedUser] = useState<User | null>(null);

    // Confirm Delete State
    const [deleteId, setDeleteId] = useState<number | null>(null);
    const [isDeleteOpen, setIsDeleteOpen] = useState(false);

    const queryClient = useQueryClient();

    const { data, isLoading } = useQuery({
        queryKey: ["users", pageIndex, pageSize, debouncedKeyword],
        queryFn: () =>
            UserService.list({
                pageIndex,
                pageSize,
                keyword: debouncedKeyword,
            }),
    });

    const deleteMutation = useMutation({
        mutationFn: UserService.delete,
        onSuccess: (data) => {
            queryClient.invalidateQueries({ queryKey: ["users"] });
            addToast({
                title: "Success",
                description: data?.message,
                color: "success",
            });
            setDeleteId(null);
            setIsDeleteOpen(false);
        },
        onError: (error) => {
            console.error("Delete failed", error);
            addToast({
                title: "Error",
                description: "Failed to delete user",
                color: "danger",
            });
        }
    });

    const handleCreate = () => {
        setSelectedUser(null);
        onOpen();
    };

    const handleEdit = (user: User) => {
        setSelectedUser(user);
        onOpen();
    };

    const handleDeleteClick = (id: number) => {
        setDeleteId(id);
        setIsDeleteOpen(true);
    };

    const confirmDelete = () => {
        if (deleteId) {
            deleteMutation.mutate([deleteId]);
        }
    };

    const columns: Column<User>[] = [
        {
            uid: "username",
            name: "Username",
        },
        {
            uid: "displayName",
            name: "Display Name",
        },
        {
            uid: "displayName",
            name: "Display Name",
        },
        // Email column removed
        {
            uid: "status",
            name: "Status",
            render: (user: User) => (
                <Chip
                    className="capitalize"
                    color={user.status === "Active" ? "success" : "danger"}
                    size="sm"
                    variant="soft"
                >
                    {user.status === "Active" ? "Active" : "Inactive"}
                </Chip>
            ),
        },
        {
            uid: "createdAt",
            name: "Created At",
            render: (user: User) => new Date(user.createdAt).toLocaleDateString("vi-VN"),
        },
        {
            uid: "actions",
            name: "Actions",
            render: (user: User) => (
                <div className="relative flex items-center gap-2">
                    <span className="text-lg text-default-400 cursor-pointer active:opacity-50" onClick={() => handleEdit(user)} title="Edit user">
                        <EditIcon />
                    </span>
                    <span className="text-lg text-danger cursor-pointer active:opacity-50" onClick={() => handleDeleteClick(user.id)} title="Delete user">
                        <DeleteIcon />
                    </span>
                </div>
            )
        }
    ];

    return (
        <div className="space-y-6">
            <PageHeader
                title="Users Management"
                description="Manage users in the system"
                action={{
                    label: "Add User",
                    onClick: handleCreate,
                }}
            />

            <div className="bg-white dark:bg-gray-800 rounded-lg shadow p-4">
                <div className="mb-4 flex flex-col sm:flex-row justify-between gap-4">
                    <Input
                        className="w-full sm:max-w-[44%]"
                        placeholder="Search users..."
                        value={keyword}
                        onChange={(e) => setKeyword(e.target.value)}
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

            <UserModal
                isOpen={isOpen}
                onClose={onClose}
                user={selectedUser}
            />

            <ConfirmModal
                isOpen={isDeleteOpen}
                onClose={() => setIsDeleteOpen(false)}
                onConfirm={confirmDelete}
                title="Confirm Delete"
                message="Are you sure you want to delete this user?"
                confirmLabel="Delete"
                cancelLabel="Cancel"
                isLoading={deleteMutation.isPending}
                variant="danger"
            />
        </div>
    );
}
