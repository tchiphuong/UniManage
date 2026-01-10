"use client";

import { Button } from "@heroui/react";

export { Button };
export default Button;

/**
 * Button Component - Re-export from HeroUI
 * 
 * Available variants:
 * - primary (default)
 * - secondary
 * - tertiary
 * - ghost
 * - danger
 * - danger-soft
 * 
 * Available sizes:
 * - sm
 * - md (default)
 * - lg
 * 
 * Features:
 * - isIconOnly: For icon-only buttons
 * - isPending: Loading state with render props
 * - isDisabled: Disabled state
 * - fullWidth: Full width button
 * 
 * @example Basic
 * <Button>Click me</Button>
 * 
 * @example With variant
 * <Button variant="danger">Delete</Button>
 * 
 * @example With icon
 * <Button>
 *   <Icon />
 *   Click me
 * </Button>
 * 
 * @example Icon only
 * <Button isIconOnly>
 *   <Icon />
 * </Button>
 * 
 * @example Loading state
 * <Button isPending={isLoading}>
 *   {({ isPending }) => (
 *     <>
 *       {isPending ? <Spinner size="sm" /> : <Icon />}
 *       {isPending ? "Loading..." : "Submit"}
 *     </>
 *   )}
 * </Button>
 */
