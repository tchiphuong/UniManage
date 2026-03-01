---
name: unimanage-frontend
description: Comprehensive guide for working with UniManage frontend components. Use this skill when developing, documenting, or modifying React components in the UniManage Next.js project using Hero UI v3, TypeScript, and Tailwind CSS.
---

# UniManage Frontend Development Skill

This skill provides guidelines and conventions for developing frontend components in the UniManage project.

## Project Stack

-   **Framework**: Next.js 14+ with App Router
-   **Language**: TypeScript (strict mode)
-   **UI Library**: Hero UI v3 ([docs](https://v3.heroui.com/docs/react/components))
-   **Styling**: Tailwind CSS
-   **Internationalization**: next-intl
-   **State Management**: React Context API
-   **Icons**: Heroicons

## Component Architecture

### Directory Structure

```
components/
├── common/          # Reusable generic components
│   ├── button.tsx
│   ├── text-field.tsx
│   ├── switch.tsx
│   ├── tabs.tsx
│   ├── data-table.tsx
│   ├── confirm-modal.tsx
│   ├── setting-card.tsx
│   └── form/        # Form-specific components
├── ui/              # Project-specific UI components
│   ├── stat-card.tsx
│   ├── page-header.tsx
│   ├── breadcrumb.tsx
│   ├── activity-item.tsx
│   └── performance-metric.tsx
├── layout/          # Layout components
│   ├── header.tsx
│   └── sidebar.tsx
├── dashboard/       # Dashboard-specific components
│   ├── stats-card.tsx
│   └── recent-orders.tsx
├── providers/       # Context providers
│   └── confirm-provider.tsx
└── index.ts         # Central export
```

### Component Categories

1. **Common Components** (`components/common/`)

    - Generic, reusable across entire project
    - Often wrap Hero UI components
    - No business logic
    - Examples: Button, TextField, Switch, Tabs

2. **UI Components** (`components/ui/`)

    - Project-specific but reusable
    - Business logic allowed
    - Examples: StatCard, PageHeader, Breadcrumb

3. **Layout Components** (`components/layout/`)

    - Application structure
    - Navigation, headers, sidebars
    - Examples: Header, Sidebar

4. **Feature Components** (`components/[feature]/`)

    - Feature-specific (dashboard, auth, etc.)
    - Can contain business logic
    - Examples: StatsCard, RecentOrders

5. **Provider Components** (`components/providers/`)
    - Context providers
    - Global state management
    - Examples: ConfirmProvider

## Coding Conventions

### TypeScript Standards

```typescript
// ✅ CORRECT: Interface for props
interface ComponentProps {
    title: string;
    value: number;
    onChange?: (val: number) => void;
    className?: string;
}

// ✅ CORRECT: Named export with type safety
export function Component({ title, value, onChange, className = "" }: ComponentProps) {
    // Implementation
}

// ❌ AVOID: Any types
function Component({ data }: { data: any }) {}

// ❌ AVOID: Default export for components (use named exports)
export default function Component() {}
```

### Component Patterns

#### 1. Client Components

```typescript
"use client"; // Always at the top for interactive components

import { useState } from "react";
import { Button } from "@heroui/react";

export function InteractiveComponent() {
    const [count, setCount] = useState(0);

    return <Button onPress={() => setCount(count + 1)}>{count}</Button>;
}
```

#### 2. Hero UI Wrappers

```typescript
"use client";

import { Switch as HeroSwitch, Label } from "@heroui/react";

interface SwitchProps {
    checked: boolean;
    onChange: (checked: boolean) => void;
    label?: string;
    disabled?: boolean;
}

export function Switch({ checked, onChange, label, disabled = false }: SwitchProps) {
    return (
        <div className="flex items-center gap-3">
            <HeroSwitch checked={checked} onChange={onChange} isDisabled={disabled}>
                <HeroSwitch.Control className="w-11 h-6">
                    <HeroSwitch.Thumb className="w-5 h-5" />
                </HeroSwitch.Control>
            </HeroSwitch>
            {label && <Label>{label}</Label>}
        </div>
    );
}
```

#### 3. Context Providers

```typescript
"use client";

import { createContext, useContext, useState, ReactNode } from "react";

type ContextType = {
    // Context shape
};

const Context = createContext<ContextType | undefined>(undefined);

export function Provider({ children }: { children: ReactNode }) {
    // Provider implementation
    return <Context.Provider value={value}>{children}</Context.Provider>;
}

export function useCustomHook() {
    const context = useContext(Context);
    if (!context) throw new Error("Hook must be used within Provider");
    return context;
}
```

### Styling Conventions

#### Tailwind Utilities

```typescript
// ✅ CORRECT: Semantic class ordering
<div className="flex items-center justify-between gap-4 px-6 py-4 bg-white dark:bg-gray-800 rounded-lg shadow-sm">

// Class order:
// 1. Layout: flex, grid, block
// 2. Positioning: relative, absolute, sticky
// 3. Sizing: w-*, h-*, min-*, max-*
// 4. Spacing: p-*, m-*, gap-*
// 5. Typography: text-*, font-*
// 6. Colors: bg-*, text-*, border-*
// 7. Effects: shadow-*, opacity-*
// 8. Transitions: transition-*
// 9. Dark mode: dark:*
```

#### Color System

```typescript
// Hero UI semantic colors
<Button color="primary">    // Blue
<Button color="secondary">  // Purple
<Button color="success">    // Green
<Button color="warning">    // Orange
<Button color="danger">     // Red
<Button color="default">    // Gray

// Tailwind colors for custom styling
- Primary: blue-600, blue-500
- Success: green-600, green-500
- Warning: orange-600, orange-500
- Danger: red-600, red-500
- Gray scale: gray-50 to gray-900
```

#### Dark Mode

```typescript
// Always provide dark mode variants
<div className="bg-white dark:bg-gray-800 text-gray-900 dark:text-white">
    <h1 className="text-gray-700 dark:text-gray-200">Title</h1>
</div>
```

### Internationalization

```typescript
import { useTranslations } from "next-intl";

export function Component() {
    const t = useTranslations("namespace");

    return (
        <div>
            <h1>{t("title")}</h1>
            <p>{t("description", { count: 5 })}</p>
        </div>
    );
}
```

## Hero UI Component Usage

### Common Components

#### Buttons

```typescript
import { Button } from "@heroui/react";

<Button color="primary" variant="solid">Primary</Button>
<Button color="danger" variant="bordered">Delete</Button>
<Button isLoading={loading}>Submit</Button>
<Button startContent={<Icon />}>Add Item</Button>
```

#### Form Inputs

```typescript
import { Input, TextField, TextArea } from "@heroui/react";

<TextField label="Email" type="email" required />
<Input placeholder="Search..." />
<TextArea label="Description" rows={4} />
```

#### Overlays

```typescript
import { Modal, AlertDialog, Tooltip, Popover } from "@heroui/react";

// Modal
<Modal isOpen={open} onOpenChange={setOpen}>
    <Modal.Container>
        <Modal.Dialog>
            <Modal.Header><Modal.Heading>Title</Modal.Heading></Modal.Header>
            <Modal.Body>Content</Modal.Body>
            <Modal.Footer><Button>OK</Button></Modal.Footer>
        </Modal.Dialog>
    </Modal.Container>
</Modal>

// AlertDialog (confirmation)
<AlertDialog isOpen={open}>
    <AlertDialog.Header>
        <AlertDialog.Icon status="danger" />
        <AlertDialog.Heading>Confirm</AlertDialog.Heading>
    </AlertDialog.Header>
</AlertDialog>
```

#### Navigation

```typescript
import { Tabs, Tab, TabList, TabPanel } from "@heroui/react";

<Tabs>
    <TabList>
        <Tab id="tab1">Tab 1</Tab>
        <Tab id="tab2">Tab 2</Tab>
    </TabList>
    <TabPanel id="tab1">Content 1</TabPanel>
    <TabPanel id="tab2">Content 2</TabPanel>
</Tabs>;
```

## Component Documentation

### Documentation Format

Each component should have a markdown file with:

```markdown
# Component Name

**Location**: `components/[category]/component-name.tsx`
**Hero UI Base**: [Component](https://v3.heroui.com/docs/components/component)

Brief description of what the component does.

## Import

\`\`\`typescript
import { Component } from '@/components/[category]';
\`\`\`

## Props

| Name    | Type     | Required | Default | Description |
| ------- | -------- | -------- | ------- | ----------- |
| `prop1` | `string` | ✅       | -       | Description |
| `prop2` | `number` | ❌       | `0`     | Description |

## Usage Examples

### Basic Usage

\`\`\`tsx
<Component prop1="value" />
\`\`\`

### Advanced Usage

\`\`\`tsx
<Component prop1="value" prop2={100} />
\`\`\`

## Accessibility

-   List accessibility features

## Best Practices

1. Practice 1
2. Practice 2
```

### Documentation Location

```
components/
├── COMPONENTS.md           # Main index
├── common/
│   ├── button.md
│   ├── text-field.md
│   └── ...
└── ui/
    ├── stat-card.md
    └── ...
```

## Common Patterns

### Data Fetching

```typescript
"use client";

import { useState, useEffect } from "react";

export function DataComponent() {
    const [data, setData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch("/api/data");
                const result = await response.json();
                setData(result);
            } catch (err) {
                setError(err);
            } finally {
                setIsLoading(false);
            }
        }
        fetchData();
    }, []);

    if (isLoading) return <Spinner />;
    if (error) return <Alert>Error: {error.message}</Alert>;
    return <div>{/* Render data */}</div>;
}
```

### Form Handling

```typescript
"use client";

import { useState } from "react";
import { TextField, Button } from "@heroui/react";

export function FormComponent() {
    const [formData, setFormData] = useState({ email: "", password: "" });
    const [errors, setErrors] = useState({});

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        // Validation
        // API call
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                label="Email"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                errorMessage={errors.email}
            />
            <Button type="submit">Submit</Button>
        </form>
    );
}
```

### Confirmation Dialogs

```typescript
import { useConfirm } from "@/components/providers/confirm-provider";

export function DeleteButton() {
    const confirm = useConfirm();

    const handleDelete = async () => {
        const confirmed = await confirm({
            title: "Delete Item",
            message: "Are you sure you want to delete this item?",
            confirmLabel: "Delete",
            variant: "danger",
        });

        if (confirmed) {
            // Perform delete
        }
    };

    return (
        <Button color="danger" onPress={handleDelete}>
            Delete
        </Button>
    );
}
```

## Best Practices

### Performance

1. **Use React.memo** for expensive renders
2. **Lazy load** heavy components
3. **Optimize images** with Next.js Image component
4. **Minimize re-renders** with useMemo/useCallback

### Accessibility

1. **Semantic HTML**: Use proper elements (`<button>`, `<nav>`, `<main>`)
2. **ARIA labels**: Add labels for screen readers
3. **Keyboard navigation**: Ensure all interactive elements are keyboard accessible
4. **Focus management**: Proper focus styles and order
5. **Color contrast**: Meet WCAG AA standards

### Code Organization

1. **Single Responsibility**: One component = one job
2. **Composition**: Build complex components from simple ones
3. **Extract Logic**: Move complex logic to hooks
4. **Type Safety**: Always define prop interfaces
5. **Consistent Naming**: PascalCase for components, camelCase for functions

### Error Handling

```typescript
try {
    await riskyOperation();
} catch (error) {
    console.error("Operation failed:", error);
    // Show user-friendly error message
    toast.error("Something went wrong. Please try again.");
}
```

## Testing

### Component Tests

```typescript
import { render, screen } from "@testing-library/react";
import { Component } from "./component";

describe("Component", () => {
    it("renders correctly", () => {
        render(<Component title="Test" />);
        expect(screen.getByText("Test")).toBeInTheDocument();
    });
});
```

## Common Issues & Solutions

### Issue: Hero UI TypeScript Errors

**Solution**: Add `// @ts-ignore` for Hero UI v3 components with incomplete types

```typescript
{/* @ts-ignore */}
<AlertDialog isOpen={open}>
```

### Issue: Dark Mode Not Working

**Solution**: Ensure Tailwind dark mode is configured

```javascript
// tailwind.config.js
module.exports = {
    darkMode: "class",
    // ...
};
```

### Issue: Client Component Hydration Mismatch

**Solution**: Use `"use client"` directive and avoid SSR-incompatible APIs

## Resources

-   [Hero UI Documentation](https://v3.heroui.com/docs/react/components)
-   [Next.js App Router](https://nextjs.org/docs/app)
-   [Tailwind CSS](https://tailwindcss.com/docs)
-   [TypeScript](https://www.typescriptlang.org/docs)
-   [next-intl](https://next-intl-docs.vercel.app/)

## When to Use This Skill

Use this skill when:

-   Creating new React components
-   Wrapping Hero UI components
-   Building forms or data tables
-   Implementing dark mode
-   Adding internationalization
-   Writing component documentation
-   Troubleshooting Hero UI issues
-   Following project conventions
