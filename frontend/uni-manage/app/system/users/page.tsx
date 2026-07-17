"use client";

import { useEffect, useState, useCallback, useMemo } from "react";
import { 
    Table, 
    TableHeader, 
    TableColumn, 
    TableBody, 
    TableRow, 
    TableCell, 
    Pagination, 
    Input, 
    Button, 
    Chip, 
    Spinner,
    Tooltip
} from "@heroui/react";
import { SearchIcon, EditIcon, DeleteIcon, PlusIcon } from "lucide-react";
import { userService } from "@/services/system/user.service";
import { UserModel, UserListParams } from "@/types/system";
import { toast } from "sonner";
import { useDebounce } from "@/hooks/use-debounce"; 
// Assuming use-debounce exists, otherwise we will create it or use simple timeout

import { UserFormModal } from "@/components/system/users/user-form-modal";
import { UserDeleteModal } from "@/components/system/users/user-delete-modal";

export default function UsersPage() {
    const [users, setUsers] = useState<UserModel[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [totalItems, setTotalItems] = useState(0);
    
    // Pagination & Filter state
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [searchQuery, setSearchQuery] = useState("");
    
    // Debounce search query to prevent spamming API
    const debouncedSearch = useDebounce(searchQuery, 500);

    // Modal state
    const [isFormModalOpen, setIsFormModalOpen] = useState(false);
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<UserModel | null>(null);
    const [userDetail, setUserDetail] = useState<UserDetailModel | null>(null);
    const [isModalLoading, setIsModalLoading] = useState(false);

    const fetchUsers = useCallback(async () => {
        setIsLoading(true);
        try {
            const params: UserListParams = {
                pageIndex: page,
                pageSize: rowsPerPage,
                keyword: debouncedSearch
            };
            const response = await userService.getUsers(params);
            
            if (response.returnCode === 200 && response.data) {
                setUsers(response.data.items);
                setTotalItems(response.data.paging.totalItems);
            } else {
                toast.error(response.message || "Failed to fetch users");
            }
        } catch (error) {
            toast.error("An error occurred while fetching users");
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    }, [page, rowsPerPage, debouncedSearch]);

    useEffect(() => {
        fetchUsers();
    }, [fetchUsers]);

    const pages = Math.ceil(totalItems / rowsPerPage) || 1;

    const onSearchChange = useCallback((value?: string) => {
        if (value) {
            setSearchQuery(value);
            setPage(1);
        } else {
            setSearchQuery("");
        }
    }, []);

    const onClear = useCallback(() => {
        setSearchQuery("");
        setPage(1);
    }, []);

    const handleAdd = () => {
        setUserDetail(null);
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
                toast.error(response.message || "Failed to load user details");
                setIsFormModalOpen(false);
            }
        } catch (error) {
            toast.error("Failed to fetch user details");
            setIsFormModalOpen(false);
        } finally {
            setIsModalLoading(false);
        }
    };

    const handleDelete = (user: UserModel) => {
        setSelectedUser(user);
        setIsDeleteModalOpen(true);
    };

    const onFormSubmit = async (data: any) => {
        setIsModalLoading(true);
        try {
            if (userDetail) {
                // Update
                const response = await userService.updateUser(userDetail.uuid, data);
                if (response.returnCode === 200) {
                    toast.success("User updated successfully");
                    setIsFormModalOpen(false);
                    fetchUsers();
                } else {
                    toast.error(response.message || "Failed to update user");
                }
            } else {
                // Create
                const response = await userService.createUser(data);
                if (response.returnCode === 200) {
                    toast.success("User created successfully");
                    setIsFormModalOpen(false);
                    fetchUsers();
                } else {
                    toast.error(response.message || "Failed to create user");
                }
            }
        } catch (error) {
            toast.error("An error occurred");
        } finally {
            setIsModalLoading(false);
        }
    };

    const onDeleteConfirm = async () => {
        if (!selectedUser) return;
        setIsModalLoading(true);
        try {
            const response = await userService.deleteUsers([selectedUser.uuid]);
            if (response.returnCode === 200) {
                toast.success("User deleted successfully");
                setIsDeleteModalOpen(false);
                fetchUsers();
            } else {
                toast.error(response.message || "Failed to delete user");
            }
        } catch (error) {
            toast.error("An error occurred");
        } finally {
            setIsModalLoading(false);
        }
    };

    const renderCell = useCallback((user: UserModel, columnKey: React.Key) => {
        const cellValue = user[columnKey as keyof UserModel];

        switch (columnKey) {
            case "username":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-small capitalize">{user.username}</p>
                    </div>
                );
            case "employeeCode":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-small capitalize">{user.employeeCode || "N/A"}</p>
                    </div>
                );
            case "roleCode":
                return (
                    <Chip className="capitalize" size="sm" variant="flat">
                        {user.roleCode || "No Role"}
                    </Chip>
                );
            case "status":
                return (
                    <Chip className="capitalize" color={user.status === "active" ? "success" : "danger"} size="sm" variant="flat">
                        {user.status}
                    </Chip>
                );
            case "actions":
                return (
                    <div className="relative flex items-center gap-2">
                        <Tooltip content="Edit user">
                            <span 
                                className="text-lg text-default-400 cursor-pointer active:opacity-50"
                                onClick={() => handleEdit(user)}
                            >
                                <EditIcon size={20} />
                            </span>
                        </Tooltip>
                        <Tooltip color="danger" content="Delete user">
                            <span 
                                className="text-lg text-danger cursor-pointer active:opacity-50"
                                onClick={() => handleDelete(user)}
                            >
                                <DeleteIcon size={20} />
                            </span>
                        </Tooltip>
                    </div>
                );
            default:
                return cellValue;
        }
    }, [handleEdit, handleDelete]);

    const topContent = useMemo(() => {
        return (
            <div className="flex flex-col gap-4">
                <div className="flex justify-between gap-3 items-end">
                    <Input
                        isClearable
                        className="w-full sm:max-w-[44%]"
                        placeholder="Search by username, email or code..."
                        startContent={<SearchIcon />}
                        value={searchQuery}
                        onClear={() => onClear()}
                        onValueChange={onSearchChange}
                    />
                    <div className="flex gap-3">
                        <Button color="primary" endContent={<PlusIcon />} onPress={handleAdd}>
                            Add New
                        </Button>
                    </div>
                </div>
                <div className="flex justify-between items-center">
                    <span className="text-default-400 text-small">Total {totalItems} users</span>
                    <label className="flex items-center text-default-400 text-small">
                        Rows per page:
                        <select
                            className="bg-transparent outline-none text-default-400 text-small ml-2"
                            onChange={(e) => {
                                setRowsPerPage(Number(e.target.value));
                                setPage(1);
                            }}
                            value={rowsPerPage}
                        >
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                        </select>
                    </label>
                </div>
            </div>
        );
    }, [searchQuery, totalItems, rowsPerPage, onSearchChange, onClear]);

    const bottomContent = useMemo(() => {
        return (
            <div className="py-2 px-2 flex justify-between items-center">
                <Pagination
                    isCompact
                    showControls
                    showShadow
                    color="primary"
                    page={page}
                    total={pages}
                    onChange={setPage}
                />
            </div>
        );
    }, [page, pages]);

    return (
        <div className="p-6 max-w-[1200px] mx-auto w-full">
            <h1 className="text-2xl font-bold mb-6">User Management</h1>
            <Table
                aria-label="User management table with pagination"
                isHeaderSticky
                bottomContent={bottomContent}
                bottomContentPlacement="outside"
                classNames={{
                    wrapper: "max-h-[600px]",
                }}
                topContent={topContent}
                topContentPlacement="outside"
            >
                <TableHeader>
                    <TableColumn key="username">USERNAME</TableColumn>
                    <TableColumn key="employeeCode">EMPLOYEE CODE</TableColumn>
                    <TableColumn key="roleCode">ROLE</TableColumn>
                    <TableColumn key="status">STATUS</TableColumn>
                    <TableColumn key="actions" align="center">ACTIONS</TableColumn>
                </TableHeader>
                <TableBody
                    emptyContent={isLoading ? " " : "No users found"}
                    items={users}
                    isLoading={isLoading}
                    loadingContent={<Spinner label="Loading..." />}
                >
                    {(item) => (
                        <TableRow key={item.uuid}>
                            {(columnKey) => <TableCell>{renderCell(item, columnKey)}</TableCell>}
                        </TableRow>
                    )}
                </TableBody>
            </Table>

            <UserFormModal 
                isOpen={isFormModalOpen}
                onClose={() => setIsFormModalOpen(false)}
                onSubmit={onFormSubmit}
                initialData={userDetail}
                isLoading={isModalLoading}
            />

            <UserDeleteModal
                isOpen={isDeleteModalOpen}
                onClose={() => setIsDeleteModalOpen(false)}
                onConfirm={onDeleteConfirm}
                user={selectedUser}
                isLoading={isModalLoading}
            />
        </div>
    );
}
