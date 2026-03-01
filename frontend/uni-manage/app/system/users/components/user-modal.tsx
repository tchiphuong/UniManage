"use client";

import { useEffect, useState } from "react";
import { Button, Form, Input, Label, Modal, Switch } from "@heroui/react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { User, UserService } from "@/services/user.service";
import { Icon } from "@iconify/react";
import { addToast } from "@heroui/toast";

// Define Base Schema (without refinements yet)
const baseUserSchema = z.object({
    username: z.string().min(3, "Username must be at least 3 characters"),
    displayName: z.string().min(1, "Display name is required"),
    // email: z.string().email("Invalid email address"), // Removed
    password: z.string().optional(),
    status: z.string(),
});

// Create Schema: Password mandatory
const createUserSchema = baseUserSchema.extend({
    password: z.string().min(6, "Password must be at least 6 characters"),
});

// Edit Schema: Password optional (no change if empty)
const editUserSchema = baseUserSchema;

interface UserModalProps {
    isOpen: boolean;
    onClose: () => void;
    user?: User | null;
}

export function UserModal({ isOpen, onClose, user }: UserModalProps) {
    const isEdit = !!user;
    const queryClient = useQueryClient();
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const { control, handleSubmit, reset, formState: { errors } } = useForm({
        resolver: zodResolver(isEdit ? editUserSchema : createUserSchema),
        defaultValues: {
            username: "",
            displayName: "",
            // email: "", // Removed
            password: "",
            status: "",
        },
    });

    // Reset form when user changes or modal opens
    useEffect(() => {
        setErrorMessage(null); // Clear error on open
        if (isOpen) {
            if (user) {
                reset({
                    username: user.username,
                    displayName: user.displayName,
                    // email: user.email, // Removed
                    password: "",
                    status: user.status,
                });
            } else {
                reset({
                    username: "",
                    displayName: "",
                    // email: "", // Removed
                    password: "",
                    status: "",
                });
            }
        }
    }, [isOpen, user, reset]);

    const mutation = useMutation({
        mutationFn: async (data: any) => {
            const payload = {
                ...data,
                status: data.status ? 1 : 0,
            };

            if (isEdit && user) {
                // Remove password if empty to avoid updating it
                if (!payload.password) delete payload.password;
                return UserService.update(user.id, payload);
            } else {
                return UserService.create(payload);
            }
        },
        onSuccess: (data) => {
            queryClient.invalidateQueries({ queryKey: ["users"] });
            addToast({
                title: "Success",
                description: data.message,
                color: "success",
            });
            onClose();
        },
        onError: (error: any) => {
            console.error("Error saving user:", error);
            const msg = error?.message;
            setErrorMessage(msg);
            addToast({
                title: "Error",
                description: msg,
                color: "danger",
            });
        }
    });

    const onSubmit = (data: any) => {
        mutation.mutate(data);
    };

    const handleOpenChange = (open: boolean) => {
        if (!open) onClose();
    };

    return (
        <Modal isOpen={isOpen} onOpenChange={handleOpenChange}>
            <Modal.Backdrop>
                <Modal.Container placement="auto">
                    <Modal.Dialog className="sm:max-w-xl">
                        <Modal.CloseTrigger />
                        <Modal.Header>
                            <Modal.Icon className="bg-accent-soft text-accent-soft-foreground">
                                <Icon icon="solar:user-bold" className="size-5" />
                            </Modal.Icon>
                            <Modal.Heading>
                                {isEdit ? "Edit User" : "Create New User"}
                            </Modal.Heading>
                            <p className="mt-1.5 text-sm leading-5 text-muted-foreground">
                                {isEdit ? "Update user information below." : "Fill in the details to create a new user."}
                            </p>
                        </Modal.Header>
                        <Modal.Body className="p-6">
                            <Form className="flex flex-col gap-4" onSubmit={handleSubmit(onSubmit)}>
                                <Controller
                                    name="username"
                                    control={control}
                                    render={({ field }) => (
                                        <Input
                                            {...field}
                                            label="Username"
                                            placeholder="Enter username"
                                            errorMessage={errors.username?.message?.toString()}
                                            isInvalid={!!errors.username}
                                            variant="bordered"
                                            labelPlacement="outside"
                                        />
                                    )}
                                />

                                <Controller
                                    name="displayName"
                                    control={control}
                                    render={({ field }) => (
                                        <Input
                                            {...field}
                                            label="Display Name"
                                            placeholder="Enter display name"
                                            errorMessage={errors.displayName?.message?.toString()}
                                            isInvalid={!!errors.displayName}
                                            variant="bordered"
                                            labelPlacement="outside"
                                        />
                                    )}
                                />

                                {/* Email field removed */}

                                <Controller
                                    name="password"
                                    control={control}
                                    render={({ field }) => (
                                        <Input
                                            {...field}
                                            type="password"
                                            label={isEdit ? "Password (leave blank to keep current)" : "Password"}
                                            placeholder="Enter password"
                                            errorMessage={errors.password?.message?.toString()}
                                            isInvalid={!!errors.password}
                                            variant="bordered"
                                            labelPlacement="outside"
                                        />
                                    )}
                                />

                                <Controller
                                    name="status"
                                    control={control}
                                    render={({ field: { value, onChange, ...field } }) => (
                                        <Switch
                                            {...field}
                                            isSelected={value}
                                            onValueChange={onChange}
                                        >
                                            Active Status
                                        </Switch>
                                    )}
                                />
                            </Form>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onPress={onClose} isDisabled={mutation.isPending}>
                                Cancel
                            </Button>
                            <Button onPress={() => handleSubmit(onSubmit)()} isPending={mutation.isPending}>
                                {isEdit ? "Update" : "Create"}
                            </Button>
                        </Modal.Footer>
                    </Modal.Dialog>
                </Modal.Container>
            </Modal.Backdrop>
        </Modal>
    );
}
