---
name: heroui-overlays
description: Hero UI Overlay components (Modal, AlertDialog, Popover, Tooltip, Drawer). Use for dialogs, confirmations, contextual information in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Overlay Components

Complete guide for Hero UI Overlay components. For detailed examples, visit the official documentation links below.

## Modal Component

**Docs**: https://v3.heroui.com/docs/components/modal  
**Import**: `import { Modal, useOverlayState } from '@heroui/react';`

Full-featured dialog component with backdrop, animations, and accessibility.

### Anatomy

```tsx
<Modal>
    <Modal.Backdrop />
    <Modal.Container>
        <Modal.Dialog>
            <Modal.Header>Title</Modal.Header>
            <Modal.Body>Content</Modal.Body>
            <Modal.Footer>Actions</Modal.Footer>
        </Modal.Dialog>
    </Modal.Container>
</Modal>
```

### Key Props (Modal.Dialog)

-   `placement`: `'auto' | 'top' | 'center' | 'bottom'` (default: `'center'`)
-   `size`: `'xs' | 'sm' | 'md' | 'lg' | 'cover' | 'full'` (default: `'md'`)
-   `scrollBehavior`: `'inside' | 'outside'` (default: `'inside'`)
-   `isDismissable`: `boolean` (default: `true`) - Click backdrop to close
-   `isKeyboardDismissDisabled`: `boolean` - Disable ESC key

### Key Props (Modal.Backdrop)

-   `variant`: `'opaque' | 'blur' | 'transparent'` (default: `'opaque'`)

### Basic Usage with State

```tsx
import { Modal, Button, useOverlayState } from "@heroui/react";

function ModalExample() {
    const state = useOverlayState();

    return (
        <>
            <Button onPress={state.open}>Open Modal</Button>

            <Modal state={state}>
                <Modal.Backdrop />
                <Modal.Container>
                    <Modal.Dialog>
                        <Modal.Header>
                            <h2>Modal Title</h2>
                            <Modal.CloseButton />
                        </Modal.Header>
                        <Modal.Body>
                            <p>Modal content goes here</p>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="ghost" onPress={state.close}>
                                Cancel
                            </Button>
                            <Button variant="primary" onPress={handleConfirm}>
                                Confirm
                            </Button>
                        </Modal.Footer>
                    </Modal.Dialog>
                </Modal.Container>
            </Modal>
        </>
    );
}
```

### Size Variants

```tsx
// Small modal
<Modal.Dialog size="sm">...</Modal.Dialog>

// Large modal
<Modal.Dialog size="lg">...</Modal.Dialog>

// Full screen
<Modal.Dialog size="full">...</Modal.Dialog>

// Cover content area
<Modal.Dialog size="cover">...</Modal.Dialog>
```

### Placement

```tsx
// Top of viewport
<Modal.Dialog placement="top">...</Modal.Dialog>

// Bottom of viewport (drawer-like)
<Modal.Dialog placement="bottom">...</Modal.Dialog>

// Center (default)
<Modal.Dialog placement="center">...</Modal.Dialog>
```

### Scroll Behavior

```tsx
// Scroll inside modal body
<Modal.Dialog scrollBehavior="inside">...</Modal.Dialog>

// Scroll entire modal
<Modal.Dialog scrollBehavior="outside">...</Modal.Dialog>
```

### Backdrop Variants

```tsx
// Blurred backdrop
<Modal.Backdrop variant="blur" />

// Transparent backdrop
<Modal.Backdrop variant="transparent" />

// Opaque backdrop (default)
<Modal.Backdrop variant="opaque" />
```

---

## AlertDialog Component

**Docs**: https://v3.heroui.com/docs/components/alert-dialog  
**Import**: `import { AlertDialog, useOverlayState } from '@heroui/react';`

Modal dialog for critical confirmations (delete, destructive actions).

### Anatomy

```tsx
<AlertDialog>
    <AlertDialog.Backdrop />
    <AlertDialog.Container>
        <AlertDialog.Dialog>
            <AlertDialog.Icon status="danger" />
            <AlertDialog.Heading>Confirm Action</AlertDialog.Heading>
            <AlertDialog.Body>Are you sure?</AlertDialog.Body>
            <AlertDialog.Footer>Actions</AlertDialog.Footer>
        </AlertDialog.Dialog>
    </AlertDialog.Container>
</AlertDialog>
```

### Key Props (AlertDialog.Icon)

-   `status`: `'default' | 'accent' | 'success' | 'warning' | 'danger'`

### Key Props (AlertDialog.Dialog)

-   `isDismissable`: `boolean` (default: `false`) - Prevent accidental dismissal
-   `isKeyboardDismissDisabled`: `boolean` (default: `true`) - Prevent ESC key
-   Same size/placement props as Modal

### Usage

```tsx
import { AlertDialog, Button, useOverlayState } from "@heroui/react";

function DeleteConfirmation() {
    const state = useOverlayState();

    const handleDelete = () => {
        // Delete logic
        state.close();
    };

    return (
        <>
            <Button variant="danger" onPress={state.open}>
                Delete
            </Button>

            <AlertDialog state={state}>
                <AlertDialog.Backdrop />
                <AlertDialog.Container>
                    <AlertDialog.Dialog>
                        <AlertDialog.Icon status="danger" />
                        <AlertDialog.Heading>Confirm Deletion</AlertDialog.Heading>
                        <AlertDialog.Body>
                            This action cannot be undone. Are you sure you want to delete this item?
                        </AlertDialog.Body>
                        <AlertDialog.Footer>
                            <Button variant="ghost" onPress={state.close}>
                                Cancel
                            </Button>
                            <Button variant="danger" onPress={handleDelete}>
                                Delete
                            </Button>
                        </AlertDialog.Footer>
                    </AlertDialog.Dialog>
                </AlertDialog.Container>
            </AlertDialog>
        </>
    );
}
```

### Status Variants

```tsx
// Danger (delete, destructive actions)
<AlertDialog.Icon status="danger" />

// Warning (caution required)
<AlertDialog.Icon status="warning" />

// Success (confirmation)
<AlertDialog.Icon status="success" />

// Default
<AlertDialog.Icon status="default" />
```

---

## Popover Component

**Docs**: https://v3.heroui.com/docs/components/popover  
**Import**: `import { Popover, useOverlayState } from '@heroui/react';`

Contextual popup positioned relative to trigger element.

### Anatomy

```tsx
<Popover>
    <Popover.Trigger>
        <Button>Trigger</Button>
    </Popover.Trigger>
    <Popover.Content>
        <Popover.Header>Title</Popover.Header>
        <Popover.Body>Content</Popover.Body>
    </Popover.Content>
</Popover>
```

### Key Props (Popover.Content)

-   `placement`: `'top' | 'bottom' | 'left' | 'right' | 'top-start' | 'top-end' | 'bottom-start' | 'bottom-end'`
-   `offset`: `number` - Distance from trigger (default: `8`)
-   `isDismissable`: `boolean` - Click outside to close
-   `isArrow`: `boolean` - Show arrow pointer

### Usage

```tsx
import { Popover, Button } from "@heroui/react";

function PopoverExample() {
    return (
        <Popover>
            <Popover.Trigger>
                <Button>Open Popover</Button>
            </Popover.Trigger>
            <Popover.Content placement="bottom-start">
                <Popover.Header>
                    <h3>Popover Title</h3>
                </Popover.Header>
                <Popover.Body>
                    <p>This is popover content</p>
                </Popover.Body>
            </Popover.Content>
        </Popover>
    );
}
```

### Placement Examples

```tsx
// Bottom right
<Popover.Content placement="bottom-end" />

// Top left
<Popover.Content placement="top-start" />

// Right side
<Popover.Content placement="right" />
```

### With Arrow

```tsx
<Popover.Content isArrow>
    <Popover.Body>Content with arrow pointer</Popover.Body>
</Popover.Content>
```

---

## Tooltip Component

**Docs**: https://v3.heroui.com/docs/components/tooltip  
**Import**: `import { Tooltip } from '@heroui/react';`

Simple text popup on hover for additional information.

### Usage

```tsx
import { Tooltip, Button } from "@heroui/react";

<Tooltip content="This is a helpful tooltip">
    <Button>Hover me</Button>
</Tooltip>;
```

### Key Props

-   `content`: `ReactNode` - Tooltip text/content
-   `placement`: `'top' | 'bottom' | 'left' | 'right'` (default: `'top'`)
-   `offset`: `number` - Distance from target
-   `delay`: `number` - Show delay in ms (default: `0`)
-   `closeDelay`: `number` - Hide delay in ms (default: `0`)
-   `isDisabled`: `boolean` - Disable tooltip

### Placement Examples

```tsx
// Below element
<Tooltip content="Bottom tooltip" placement="bottom">
  <Button>Bottom</Button>
</Tooltip>

// Right side
<Tooltip content="Right tooltip" placement="right">
  <Button>Right</Button>
</Tooltip>
```

### With Delay

```tsx
<Tooltip content="Delayed tooltip" delay={500} closeDelay={200}>
    <Button>Hover me</Button>
</Tooltip>
```

---

## Drawer Component

**Docs**: https://v3.heroui.com/docs/components/drawer  
**Import**: `import { Drawer, useOverlayState } from '@heroui/react';`

Side panel overlay (similar to Modal but slides from edge).

### Usage

```tsx
import { Drawer, Button, useOverlayState } from "@heroui/react";

function DrawerExample() {
    const state = useOverlayState();

    return (
        <>
            <Button onPress={state.open}>Open Drawer</Button>

            <Drawer state={state} placement="right">
                <Drawer.Backdrop />
                <Drawer.Container>
                    <Drawer.Dialog>
                        <Drawer.Header>
                            <h2>Drawer Title</h2>
                            <Drawer.CloseButton />
                        </Drawer.Header>
                        <Drawer.Body>
                            <p>Drawer content</p>
                        </Drawer.Body>
                    </Drawer.Dialog>
                </Drawer.Container>
            </Drawer>
        </>
    );
}
```

### Placement

```tsx
// From right (default)
<Drawer placement="right">...</Drawer>

// From left
<Drawer placement="left">...</Drawer>

// From top
<Drawer placement="top">...</Drawer>

// From bottom
<Drawer placement="bottom">...</Drawer>
```

---

## Menu Component

**Docs**: https://v3.heroui.com/docs/components/menu  
**Import**: `import { Menu } from '@heroui/react';`

Dropdown menu with actions.

### Usage

```tsx
import { Menu, Button } from "@heroui/react";

<Menu>
    <Menu.Trigger>
        <Button>Actions</Button>
    </Menu.Trigger>
    <Menu.Content>
        <Menu.Item key="edit" onAction={handleEdit}>
            Edit
        </Menu.Item>
        <Menu.Item key="duplicate">Duplicate</Menu.Item>
        <Menu.Item key="delete" variant="danger">
            Delete
        </Menu.Item>
    </Menu.Content>
</Menu>;
```

### With Sections

```tsx
<Menu.Content>
    <Menu.Section title="Actions">
        <Menu.Item key="edit">Edit</Menu.Item>
        <Menu.Item key="duplicate">Duplicate</Menu.Item>
    </Menu.Section>
    <Menu.Section title="Danger Zone">
        <Menu.Item key="delete" variant="danger">
            Delete
        </Menu.Item>
    </Menu.Section>
</Menu.Content>
```

---

## DropdownMenu Component

**Docs**: https://v3.heroui.com/docs/components/dropdown-menu  
**Import**: `import { DropdownMenu } from '@heroui/react';`

Enhanced menu with checkboxes and radio options.

### Usage

```tsx
<DropdownMenu>
    <DropdownMenu.Trigger>
        <Button>Options</Button>
    </DropdownMenu.Trigger>
    <DropdownMenu.Content>
        <DropdownMenu.CheckboxItem checked={option1}>Option 1</DropdownMenu.CheckboxItem>
        <DropdownMenu.CheckboxItem checked={option2}>Option 2</DropdownMenu.CheckboxItem>
    </DropdownMenu.Content>
</DropdownMenu>
```

---

## Common Patterns

### Confirmation Dialog

```tsx
function useConfirmation() {
    const state = useOverlayState();

    const confirm = () =>
        new Promise((resolve) => {
            const handleConfirm = () => {
                resolve(true);
                state.close();
            };

            const handleCancel = () => {
                resolve(false);
                state.close();
            };

            state.open();
            // Return JSX to render
        });

    return { confirm, Dialog };
}
```

### Form Modal

```tsx
function FormModal({ state }) {
    const [formData, setFormData] = useState({});

    return (
        <Modal state={state}>
            <Modal.Backdrop />
            <Modal.Container>
                <Modal.Dialog size="lg">
                    <Modal.Header>
                        <h2>Edit User</h2>
                    </Modal.Header>
                    <Modal.Body>
                        <TextField
                            label="Name"
                            value={formData.name}
                            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                        />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="ghost" onPress={state.close}>
                            Cancel
                        </Button>
                        <Button variant="primary" onPress={handleSave}>
                            Save
                        </Button>
                    </Modal.Footer>
                </Modal.Dialog>
            </Modal.Container>
        </Modal>
    );
}
```

### Context Menu (Right-click)

```tsx
function ContextMenuExample() {
    const [position, setPosition] = useState({ x: 0, y: 0 });
    const state = useOverlayState();

    const handleContextMenu = (e) => {
        e.preventDefault();
        setPosition({ x: e.clientX, y: e.clientY });
        state.open();
    };

    return (
        <div onContextMenu={handleContextMenu}>
            Right click me
            <Menu state={state} position={position}>
                <Menu.Item key="copy">Copy</Menu.Item>
                <Menu.Item key="paste">Paste</Menu.Item>
            </Menu>
        </div>
    );
}
```

---

## Best Practices

1. **Use AlertDialog for destructive actions** (delete, permanent changes)
2. **Use Modal for forms and complex content**
3. **Use Popover for contextual information**
4. **Use Tooltip for short hints** (keep text brief)
5. **Use Drawer for side navigation or filters**
6. **Always provide close button** in Modal/Drawer header
7. **Focus management**: Focus first input on Modal open
8. **Keyboard support**: ESC to close (unless `isKeyboardDismissDisabled`)
9. **Prevent backdrop dismiss** for critical dialogs (`isDismissable={false}`)
10. **Test on mobile**: Ensure modals are scrollable and responsive

---

## Accessibility Notes

-   Focus trap: User can't Tab outside modal
-   Focus restoration: Returns focus to trigger on close
-   ESC key closes overlay (unless disabled)
-   Screen reader announcements for dialog roles
-   Backdrop click to dismiss (unless `isDismissable={false}`)
-   Arrow keys navigate menu items
-   Enter/Space to select menu items

---

## When to Use This Skill

Use this skill when:

-   Creating dialogs or modals
-   Implementing confirmation prompts
-   Building delete confirmations
-   Adding contextual help (tooltips)
-   Creating dropdown menus
-   Implementing side panels (drawers)
-   Building context menus
-   Showing forms in overlays
-   Implementing user actions menus
