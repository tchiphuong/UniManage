import { 
    Modal, 
    ModalContent, 
    ModalHeader, 
    ModalBody, 
    ModalFooter,
    Select,
    SelectItem,
    Button,
    Input 
} from "@/components/common";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { CreateUserPayload, UpdateUserPayload, UserModel, UserDetailModel } from "@/types/system";

const userSchema = z.object({
    username: z.string().min(3, "Username must be at least 3 characters").max(50, "Username too long"),
    email: z.string().email("Invalid email address"),
    employeeCode: z.string().optional(),
    roleCode: z.string().optional(),
    status: z.enum(["active", "inactive"]),
});

type UserFormValues = z.infer<typeof userSchema>;

interface UserFormModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (data: UserFormValues) => Promise<void>;
    initialData: UserDetailModel | null;
    isLoading: boolean;
}

export function UserFormModal({ isOpen, onClose, onSubmit, initialData, isLoading }: UserFormModalProps) {
    const isEditMode = !!initialData;

    const { control, handleSubmit, reset, formState: { errors } } = useForm<UserFormValues>({
        resolver: zodResolver(userSchema),
        defaultValues: {
            username: "",
            email: "",
            employeeCode: "",
            roleCode: "",
            status: "active",
        }
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

    const handleFormSubmit = async (data: UserFormValues) => {
        await onSubmit(data);
    };

    return (
        <Modal isOpen={isOpen} onOpenChange={(open) => !open && onClose()} placement="top-center">
            <ModalContent>
                {(onClose) => (
                    <form onSubmit={handleSubmit(handleFormSubmit)}>
                        <ModalHeader className="flex flex-col gap-1">
                            {isEditMode ? "Edit User" : "Create New User"}
                        </ModalHeader>
                        <ModalBody>
                            <Controller
                                name="username"
                                control={control}
                                render={({ field }) => (
                                    <Input
                                        {...field}
                                        autoFocus
                                        label="Username"
                                        placeholder="Enter username"
                                        isInvalid={!!errors.username}
                                        errorMessage={errors.username?.message}
                                        isDisabled={isEditMode} // Usually username cannot be changed
                                    />
                                )}
                            />
                            <Controller
                                name="email"
                                control={control}
                                render={({ field }) => (
                                    <Input
                                        {...field}
                                        label="Email"
                                        placeholder="Enter email address"
                                        type="email"
                                        isInvalid={!!errors.email}
                                        errorMessage={errors.email?.message}
                                    />
                                )}
                            />
                            <Controller
                                name="employeeCode"
                                control={control}
                                render={({ field }) => (
                                    <Input
                                        {...field}
                                        label="Employee Code"
                                        placeholder="Enter employee code"
                                        isInvalid={!!errors.employeeCode}
                                        errorMessage={errors.employeeCode?.message}
                                    />
                                )}
                            />
                            <div className="flex gap-4">
                                <Controller
                                    name="roleCode"
                                    control={control}
                                    render={({ field }) => (
                                        <Input
                                            {...field}
                                            className="w-full"
                                            label="Role Code"
                                            placeholder="Enter role code"
                                            isInvalid={!!errors.roleCode}
                                            errorMessage={errors.roleCode?.message}
                                        />
                                    )}
                                />
                                <Controller
                                    name="status"
                                    control={control}
                                    render={({ field }) => (
                                        <Select
                                            {...field}
                                            label="Status"
                                            variant="bordered"
                                            className="w-full"
                                            isInvalid={!!errors.status}
                                            errorMessage={errors.status?.message}
                                            selectedKeys={new Set([field.value])}
                                            onSelectionChange={(keys) => {
                                                const selectedValue = Array.from(keys)[0] as string;
                                                field.onChange(selectedValue);
                                            }}
                                        >
                                            <SelectItem key="active" value="active">Active</SelectItem>
                                            <SelectItem key="inactive" value="inactive">Inactive</SelectItem>
                                        </Select>
                                    )}
                                />
                            </div>
                        </ModalBody>
                        <ModalFooter>
                            <Button color="danger" variant="flat" onPress={onClose} isDisabled={isLoading}>
                                Cancel
                            </Button>
                            <Button color="primary" type="submit" isLoading={isLoading}>
                                {isEditMode ? "Save Changes" : "Create"}
                            </Button>
                        </ModalFooter>
                    </form>
                )}
            </ModalContent>
        </Modal>
    );
}
