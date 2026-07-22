"use client";

import { PlusIcon } from "@heroicons/react/24/outline";
import { useCallback, useEffect, useState } from "react";

import {
    Button,
    Chip,
    ConfirmModal,
    Table,
    TableColumn,
    TableAction,
} from "@/components/common";
import { useApiHandler } from "@/hooks";
import { useDebounce } from "@/hooks/use-debounce";
import { userService } from "@/services/system/user.service";
import { UserDetailModel, UserListParams, UserModel } from "@/types/system";

import { UserFormModal, UserFormValues } from "./components/user-form-modal";

export default function UsersPage() {
    const { handleResponse, handleError } = useApiHandler();

    const [users, setUsers] = useState<UserModel[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [totalItems, setTotalItems] = useState(0);
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [searchQuery, setSearchQuery] = useState("");
    const debouncedSearch = useDebounce(searchQuery, 500);

    const [isFormModalOpen, setIsFormModalOpen] = useState(false);
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<UserModel | null>(null);
    const [userDetail, setUserDetail] = useState<UserDetailModel | undefined>();
    const [isModalLoading, setIsModalLoading] = useState(false);

    const loadUsers = useCallback(async () => {
        setIsLoading(true);
        try {
            const params: UserListParams = {
                pageIndex: page,
                pageSize: rowsPerPage,
                search: debouncedSearch,
            };

            const response = await userService.getUsers(params);
            if (response.returnCode === 200 && response.data) {
                setUsers(response.data.items);
                setTotalItems(response.data.paging.totalItems);
            } else {
                handleResponse(response, () => {});
            }
        } catch (error) {
            handleError(error);
        } finally {
            setIsLoading(false);
        }
    }, [page, rowsPerPage, debouncedSearch, handleResponse, handleError]);

    useEffect(() => {
        loadUsers();
    }, [loadUsers]);

    const handleAdd = () => {
        setUserDetail(undefined);
        setIsFormModalOpen(true);
    };

    const handleEdit = async (user: UserModel) => {
        setIsModalLoading(true);
        setIsFormModalOpen(true);
        try {
            const response = await userService.getUserById(user.uuid);
            if (response.returnCode === 200 && response.data) {
                setUserDetail(response.data);
            } else {
                handleResponse(response, () => {});
                setIsFormModalOpen(false);
            }
        } catch (error) {
            handleError(error);
            setIsFormModalOpen(false);
        } finally {
            setIsModalLoading(false);
        }
    };

    const handleDelete = (user: UserModel) => {
        setSelectedUser(user);
        setIsDeleteModalOpen(true);
    };

    const onFormSubmit = async (data: UserFormValues) => {
        setIsModalLoading(true);
        try {
            let response;
            if (userDetail?.uuid) {
                response = await userService.updateUser(userDetail.uuid, {
                    ...data,
                    rowVersion: userDetail.rowVersion || "",
                });
            } else {
                response = await userService.createUser(data);
            }

            handleResponse(
                response,
                () => {
                    setIsFormModalOpen(false);
                    loadUsers();
                },
                userDetail?.uuid
                    ? "User updated successfully"
                    : "User created successfully",
            );
        } catch (error) {
            handleError(error);
        } finally {
            setIsModalLoading(false);
        }
    };

    const onDeleteConfirm = async () => {
        if (!selectedUser) return;

        setIsModalLoading(true);
        try {
            const response = await userService.deleteUsers([selectedUser.uuid]);
            handleResponse(
                response,
                () => {
                    setIsDeleteModalOpen(false);
                    if (users.length === 1 && page > 1) {
                        setPage(page - 1);
                    } else {
                        loadUsers();
                    }
                },
                "User deleted successfully",
            );
        } catch (error) {
            handleError(error);
        } finally {
            setIsModalLoading(false);
        }
    };

    const columns: TableColumn<UserModel>[] = [
        { key: "username", label: "USERNAME", sortable: true },
        { key: "employeeCode", label: "EMPLOYEE CODE", sortable: true },
        { key: "roleCode", label: "ROLE", sortable: true },
        {
            key: "status",
            label: "STATUS",
            sortable: true,
            render: (user: UserModel) => (
                <Chip
                    color={user.status === "ACTIVE" ? "success" : "danger"}
                    size="sm"
                    variant="soft"
                >
                    {user.status}
                </Chip>
            ),
        },
    ];

    const actions: TableAction<UserModel>[] = [
        {
            key: "edit",
            label: "Edit",
            onClick: handleEdit,
        },
        {
            key: "delete",
            label: "Delete",
            onClick: handleDelete,
        },
    ];

    return (
        <div className="mx-auto w-full max-w-300 p-6">
            <h1 className="mb-6 text-2xl font-bold">User Management</h1>
            <Table<UserModel>
                columns={columns}
                items={users}
                getRowKey={(item) => item.uuid}
                isLoading={isLoading}
                showSearch={true}
                searchPlaceholder="Search by username, email or code..."
                searchValue={searchQuery}
                onSearchChange={setSearchQuery}
                toolbarContent={
                    <Button
                        className="bg-primary text-primary-foreground"
                        onPress={handleAdd}
                    >
                        <div className="flex items-center gap-2">
                            <PlusIcon className="size-4" />
                            <span>Add New</span>
                        </div>
                    </Button>
                }
                pagination={{
                    page,
                    pageSize: rowsPerPage,
                    total: totalItems,
                    onPageChange: setPage,
                    onPageSizeChange: (size) => {
                        setRowsPerPage(size);
                        setPage(1);
                    },
                    pageSizeOptions: [10, 20, 50, 100],
                }}
                actions={actions}
            />

            <UserFormModal
                isOpen={isFormModalOpen}
                onClose={() => setIsFormModalOpen(false)}
                onSubmit={onFormSubmit}
                initialData={userDetail || null}
                isLoading={isModalLoading}
            />

            <ConfirmModal
                isOpen={isDeleteModalOpen}
                onClose={() => setIsDeleteModalOpen(false)}
                onConfirm={onDeleteConfirm}
                title="Confirm Deletion"
                message={
                    <>
                        <p>
                            Are you sure you want to delete the user{" "}
                            <strong>{selectedUser?.username}</strong>?
                        </p>
                        <p className="text-small text-default-500 mt-2">
                            This action cannot be undone. All data associated
                            with this user will be removed.
                        </p>
                    </>
                }
                confirmText="Delete User"
                isLoading={isModalLoading}
            />
        </div>
    );
}
