import { zodResolver } from "@hookform/resolvers/zod";
import { CheckIcon, XMarkIcon } from "@heroicons/react/24/outline";
import {
    FieldError,
    Input,
    Label,
    ListBox,
    Modal,
    Select,
    TextField,
} from "@heroui/react";
import { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import { z } from "zod";

import { Button } from "@/components/common";

import { UserDetailModel } from "@/types/system";

const userSchema = z.object({
    username: z
        .string()
        .min(3, "Username must be at least 3 characters")
        .max(50, "Username too long"),
    email: z.string().email("Invalid email address"),
    employeeCode: z.string().optional(),
    roleCode: z.string().optional(),
    status: z.enum(["active", "inactive"]),
    password: z.string().optional(),
});

export type UserFormValues = z.infer<typeof userSchema>;

interface UserFormModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (data: UserFormValues) => Promise<void>;
    initialData: UserDetailModel | null;
    isLoading: boolean;
}

export function UserFormModal({
    isOpen,
    onClose,
    onSubmit,
    initialData,
    isLoading,
}: Readonly<UserFormModalProps>) {
    const isEditMode = !!initialData;

    const {
        control,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<UserFormValues>({
        resolver: zodResolver(userSchema),
        defaultValues: {
            username: "",
            email: "",
            employeeCode: "",
            roleCode: "",
            status: "active",
        },
    });

    useEffect(() => {
        if (isOpen) {
            if (initialData) {
                reset({
                    username: initialData.username,
                    email: initialData.email,
                    employeeCode: initialData.employeeCode,
                    roleCode: initialData.roleCode,
                    status: initialData.status as "active" | "inactive",
                });
            } else {
                reset({
                    username: "",
                    email: "",
                    employeeCode: "",
                    roleCode: "",
                    status: "active",
                });
            }
        }
    }, [isOpen, initialData, reset]);

    return (
        <Modal>
            <Modal.Backdrop
                variant="blur"
                isOpen={isOpen}
                onOpenChange={(open) => !open && onClose()}
            />
            <Modal.Container size="md" scroll="inside">
                <Modal.Dialog>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <Modal.Header>
                            {isEditMode ? "Edit User" : "Create New User"}
                        </Modal.Header>
                        <Modal.Body>
                            <div className="flex flex-col gap-4">
                                {/* Username */}
                                <Controller
                                    name="username"
                                    control={control}
                                    render={({ field }) => (
                                        <TextField
                                            {...field}
                                            autoFocus
                                            isInvalid={!!errors.username}
                                            isDisabled={isEditMode}
                                        >
                                            <Label>Username</Label>
                                            <Input placeholder="Enter username" />
                                            {errors.username && (
                                                <FieldError>
                                                    {errors.username.message}
                                                </FieldError>
                                            )}
                                        </TextField>
                                    )}
                                />

                                {/* Email */}
                                <Controller
                                    name="email"
                                    control={control}
                                    render={({ field }) => (
                                        <TextField
                                            {...field}
                                            type="email"
                                            isInvalid={!!errors.email}
                                        >
                                            <Label>Email</Label>
                                            <Input placeholder="Enter email address" />
                                            {errors.email && (
                                                <FieldError>
                                                    {errors.email.message}
                                                </FieldError>
                                            )}
                                        </TextField>
                                    )}
                                />

                                {/* Employee Code */}
                                <Controller
                                    name="employeeCode"
                                    control={control}
                                    render={({ field }) => (
                                        <TextField
                                            {...field}
                                            isInvalid={!!errors.employeeCode}
                                        >
                                            <Label>Employee Code</Label>
                                            <Input placeholder="Enter employee code" />
                                            {errors.employeeCode && (
                                                <FieldError>
                                                    {
                                                        errors.employeeCode
                                                            .message
                                                    }
                                                </FieldError>
                                            )}
                                        </TextField>
                                    )}
                                />

                                {/* Password (chỉ hiển thị khi tạo mới) */}
                                {!isEditMode && (
                                    <Controller
                                        name="password"
                                        control={control}
                                        rules={{
                                            required: "Password is required",
                                        }}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                type="password"
                                                isInvalid={!!errors.password}
                                            >
                                                <Label>Password</Label>
                                                <Input placeholder="Enter password" />
                                                {errors.password && (
                                                    <FieldError>
                                                        {
                                                            errors.password
                                                                .message
                                                        }
                                                    </FieldError>
                                                )}
                                            </TextField>
                                        )}
                                    />
                                )}

                                <div className="flex gap-4">
                                    {/* Role Code */}
                                    <Controller
                                        name="roleCode"
                                        control={control}
                                        render={({ field }) => (
                                            <TextField
                                                {...field}
                                                isInvalid={!!errors.roleCode}
                                                isDisabled={isEditMode}
                                                className="w-full"
                                            >
                                                <Label>Role Code</Label>
                                                <Input placeholder="Enter role code" />
                                                {errors.roleCode && (
                                                    <FieldError>
                                                        {
                                                            errors.roleCode
                                                                .message
                                                        }
                                                    </FieldError>
                                                )}
                                            </TextField>
                                        )}
                                    />

                                    {/* Status */}
                                    <Controller
                                        name="status"
                                        control={control}
                                        render={({ field }) => (
                                            <Select
                                                className="w-full"
                                                value={field.value}
                                                onChange={(key) => {
                                                    if (key) {
                                                        field.onChange(
                                                            String(key),
                                                        );
                                                    }
                                                }}
                                            >
                                                <Label>Status</Label>
                                                <Select.Trigger>
                                                    <Select.Value />
                                                    <Select.Indicator />
                                                </Select.Trigger>
                                                <Select.Popover>
                                                    <ListBox>
                                                        <ListBox.Item
                                                            id="active"
                                                            textValue="Active"
                                                        >
                                                            Active
                                                            <ListBox.ItemIndicator />
                                                        </ListBox.Item>
                                                        <ListBox.Item
                                                            id="inactive"
                                                            textValue="Inactive"
                                                        >
                                                            Inactive
                                                            <ListBox.ItemIndicator />
                                                        </ListBox.Item>
                                                    </ListBox>
                                                </Select.Popover>
                                            </Select>
                                        )}
                                    />
                                </div>
                            </div>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button
                                variant="danger"
                                onPress={onClose}
                                isDisabled={isLoading}
                            >
                                <XMarkIcon className="h-5 w-5" />
                                Cancel
                            </Button>
                            <Button
                                type="submit"
                                variant="primary"
                                isPending={isLoading}
                            >
                                {!isLoading && (
                                    <CheckIcon className="h-5 w-5" />
                                )}
                                {isEditMode ? "Save Changes" : "Create"}
                            </Button>
                        </Modal.Footer>
                    </form>
                </Modal.Dialog>
            </Modal.Container>
        </Modal>
    );
}
