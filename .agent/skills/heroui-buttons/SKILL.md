---
name: heroui-buttons
description: Hero UI Button components documentation (Button, ButtonGroup, CloseButton). Use when working with button interactions, button groups, or close buttons in UniManage project with Hero UI v3.
---

# Hero UI Button Components

Complete guide for Hero UI Button components based on official documentation at https://v3.heroui.com/docs/react/components.

## Button Component

**Documentation**: https://v3.heroui.com/docs/components/button  
**Import**: `import { Button } from '@heroui/react';`

### Variants

-   `primary` (default) - Primary actions
-   `secondary` - Secondary actions
-   `tertiary` - Tertiary actions
-   `ghost` - Minimal styling
-   `danger` - Destructive actions

### Sizes

-   `sm` - Small
-   `md` - Medium (default)
-   `lg` - Large

### Props

| Prop         | Type                                                            | Default     | Description                  |
| ------------ | --------------------------------------------------------------- | ----------- | ---------------------------- |
| `variant`    | `'primary' \| 'secondary' \| 'tertiary' \| 'ghost' \| 'danger'` | `'primary'` | Visual style variant         |
| `size`       | `'sm' \| 'md' \| 'lg'`                                          | `'md'`      | Button size                  |
| `fullWidth`  | `boolean`                                                       | `false`     | Take full width of container |
| `isDisabled` | `boolean`                                                       | `false`     | Disable button interaction   |
| `isPending`  | `boolean`                                                       | `false`     | Loading state                |
| `isIconOnly` | `boolean`                                                       | `false`     | Contains only icon           |
| `onPress`    | `(e: PressEvent) => void`                                       | -           | Press handler                |
| `children`   | `ReactNode \| (values) => ReactNode`                            | -           | Content or render prop       |

### Usage Examples

```tsx
import { Button } from '@heroui/react';

// Basic button
<Button onPress={() => console.log('Clicked')}>
  Click me
</Button>

// Variants
<Button variant="primary">Primary</Button>
<Button variant="secondary">Secondary</Button>
<Button variant="tertiary">Tertiary</Button>
<Button variant="ghost">Ghost</Button>
<Button variant="danger">Danger</Button>

// Sizes
<Button size="sm">Small</Button>
<Button size="md">Medium</Button>
<Button size="lg">Large</Button>

// With icons
import { PlusIcon } from '@heroicons/react/24/outline';
<Button>
  <PlusIcon className="w-4 h-4" />
  Add Item
</Button>

// Icon only
<Button isIconOnly aria-label="Delete">
  <TrashIcon className="w-5 h-5" />
</Button>

// Loading state
<Button isPending>
  Uploading...
</Button>

// Disabled
<Button isDisabled>
  Cannot Click
</Button>

// Full width
<Button fullWidth>
  Full Width Button
</Button>

// Form submit
<Button type="submit">
  Submit Form
</Button>
```

### Interactive States

-   **Hover**: `:hover` or `[data-hovered="true"]`
-   **Active/Pressed**: `:active` or `[data-pressed="true"]`
-   **Focus**: `:focus-visible` or `[data-focus-visible="true"]`
-   **Disabled**: `:disabled` or `[aria-disabled="true"]`
-   **Pending**: `[data-pending]`

### CSS Classes

-   `.button` - Base button styles
-   `.button--sm`, `.button--md`, `.button--lg` - Size variants
-   `.button--primary`, `.button--secondary`, etc. - Style variants
-   `.button--icon-only` - Icon only modifier

### Render Props Pattern

```tsx
<Button>
    {({ isPending, isPressed, isHovered }) => (
        <>
            {isPending ? <Spinner /> : <PlusIcon />}
            {isPressed ? "Pressed" : isHovered ? "Hover" : "Add"}
        </>
    )}
</Button>
```

---

## ButtonGroup Component

**Documentation**: https://v3.heroui.com/docs/components/button-group  
**Import**: `import { ButtonGroup } from '@heroui/react';`

Group related buttons together with consistent styling and spacing.

### Usage

```tsx
import { Button, ButtonGroup } from '@heroui/react';

<ButtonGroup>
  <Button>Save</Button>
  <Button variant="secondary">Cancel</Button>
</ButtonGroup>

// Vertical layout
<ButtonGroup orientation="vertical">
  <Button>Option 1</Button>
  <Button>Option 2</Button>
  <Button>Option 3</Button>
</ButtonGroup>

// Full width
<ButtonGroup fullWidth>
  <Button>Left</Button>
  <Button>Center</Button>
  <Button>Right</Button>
</ButtonGroup>
```

---

## CloseButton Component

**Documentation**: https://v3.heroui.com/docs/components/close-button  
**Import**: `import { CloseButton } from '@heroui/react';`

Specialized close button for dialogs, modals, and dismissible content.

### Usage

```tsx
import { CloseButton } from '@heroui/react';

// Basic close button
<CloseButton onPress={() => setOpen(false)} />

// Custom size
<CloseButton size="lg" />

// With custom aria-label
<CloseButton aria-label="Close notification" />

// In modal/dialog
<Modal>
  <Modal.Dialog>
    <CloseButton className="absolute right-4 top-4" />
    {/* Modal content */}
  </Modal.Dialog>
</Modal>
```

---

## Best Practices

### Button Usage

1. **Use semantic variants**: Match button variant to action intent
2. **Loading states**: Always use `isPending` for async operations
3. **Accessibility**: Provide `aria-label` for icon-only buttons
4. **Icon placement**: Icons before text for actions, after for navigation
5. **Form buttons**: Always specify `type="submit"` for form submission

### ButtonGroup Usage

1. **Related actions**: Group buttons that perform related actions
2. **Consistent variants**: Use same variant for grouped buttons
3. **Limited count**: Max 3-4 buttons per group for UX

### CloseButton Usage

1. **Consistent placement**: Usually top-right corner
2. **Always visible**: Don't hide behind other content
3. **Keyboard accessible**: Ensure focusable with Tab key
4. **Appropriate size**: Match container size (small for alerts, large for modals)

---

## Common Patterns

### Action Buttons

```tsx
<div className="flex gap-2 justify-end">
    <Button variant="secondary" onPress={handleCancel}>
        Cancel
    </Button>
    <Button variant="primary" isPending={saving} onPress={handleSave}>
        Save Changes
    </Button>
</div>
```

### Danger Action with Confirmation

```tsx
import { useConfirm } from "@/components/providers/confirm-provider";

function DeleteButton() {
    const confirm = useConfirm();

    const handleDelete = async () => {
        const confirmed = await confirm({
            title: "Delete Item",
            message: "This action cannot be undone",
            variant: "danger",
        });

        if (confirmed) {
            await deleteItem();
        }
    };

    return (
        <Button variant="danger" onPress={handleDelete}>
            Delete
        </Button>
    );
}
```

### Button with Icon and Badge

```tsx
<Button className="relative">
    <BellIcon className="w-5 h-5" />
    Notifications
    {hasUnread && (
        <span className="absolute -top-1 -right-1 h-5 w-5 rounded-full bg-red-500 text-xs text-white flex items-center justify-center">
            3
        </span>
    )}
</Button>
```

### Toolbar Buttons

```tsx
<ButtonGroup>
    <Button isIconOnly aria-label="Bold">
        <BoldIcon />
    </Button>
    <Button isIconOnly aria-label="Italic">
        <ItalicIcon />
    </Button>
    <Button isIconOnly aria-label="Underline">
        <UnderlineIcon />
    </Button>
</ButtonGroup>
```

---

## When to Use This Skill

Use this skill when:

-   Implementing button interactions
-   Creating forms with submit/cancel buttons
-   Building action toolbars
-   Adding close buttons to modals/dialogs
-   Grouping related buttons
-   Implementing icon-only actions
-   Handling loading/pending states
-   Working with dangerous/destructive actions
