import { useState, useCallback } from "react";

export function useDataModal<T>() {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedItem, setSelectedItem] = useState<T | null>(null);

    const onOpen = useCallback(() => setIsOpen(true), []);
    
    const onClose = useCallback(() => {
        setIsOpen(false);
        setSelectedItem(null);
    }, []);

    const handleCreate = useCallback(() => {
        setSelectedItem(null);
        onOpen();
    }, [onOpen]);

    const handleEdit = useCallback((item: T) => {
        setSelectedItem(item);
        onOpen();
    }, [onOpen]);

    return {
        isOpen,
        onClose,
        selectedItem,
        handleCreate,
        handleEdit,
    };
}
