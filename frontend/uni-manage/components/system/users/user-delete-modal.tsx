import { 
    Modal, 
    ModalContent, 
    ModalHeader, 
    ModalBody, 
    ModalFooter, 
    Button 
} from "@heroui/react";
import { UserModel } from "@/types/system";

interface UserDeleteModalProps {
    isOpen: boolean;
    onClose: () => void;
    onConfirm: () => Promise<void>;
    user: UserModel | null;
    isLoading: boolean;
}

export function UserDeleteModal({ isOpen, onClose, onConfirm, user, isLoading }: UserDeleteModalProps) {
    if (!user) return null;

    return (
        <Modal isOpen={isOpen} onOpenChange={(open) => !open && onClose()} placement="top-center">
            <ModalContent>
                {(onClose) => (
                    <>
                        <ModalHeader className="flex flex-col gap-1 text-danger">
                            Confirm Deletion
                        </ModalHeader>
                        <ModalBody>
                            <p>
                                Are you sure you want to delete the user <strong>{user.username}</strong>?
                            </p>
                            <p className="text-small text-default-500">
                                This action cannot be undone. All data associated with this user will be removed.
                            </p>
                        </ModalBody>
                        <ModalFooter>
                            <Button variant="flat" onPress={onClose} isDisabled={isLoading}>
                                Cancel
                            </Button>
                            <Button color="danger" onPress={onConfirm} isLoading={isLoading}>
                                Delete User
                            </Button>
                        </ModalFooter>
                    </>
                )}
            </ModalContent>
        </Modal>
    );
}
