import { useState, useEffect } from "react";

export * from "./use-auth";

/**
 * Hook để detect mounted state
 */
export function useMounted() {
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
    }, []);

    return mounted;
}
