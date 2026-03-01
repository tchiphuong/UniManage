---
name: heroui-v3
description: Builds UI components using HeroUI v3 with React 19 and Tailwind CSS v4. Use when creating or modifying frontend components, forms, tables, or layouts in the UniManage frontend.
---

# HeroUI v3 Component Development

This skill guides you through building UI components using HeroUI v3 for the UniManage frontend application.

## When to use this skill

-   Creating new React components with HeroUI v3
-   Building forms, tables, modals, or any UI elements
-   Styling components with Tailwind CSS v4
-   Implementing accessible interfaces
-   Troubleshooting HeroUI component issues
-   Migrating from NextUI to HeroUI v3

## Tech Stack Requirements

-   **React**: 19+
-   **HeroUI**: v3 Beta (`@heroui/react@beta`)
-   **Tailwind CSS**: v4
-   **Next.js**: 15 (App Router)
-   **TypeScript**: 5+

## Core Principles

### 1. Import Pattern

Always import from `@heroui/react`:

```tsx
// ✅ Correct
import { Button, Card, CardBody, Input } from "@heroui/react";

// ❌ Wrong - Old NextUI
import { Button } from "@nextui-org/react";
```

### 2. API Conventions (React Aria)

HeroUI uses React Aria naming conventions:

```tsx
// ✅ Use these props
<Button
  isDisabled={false}
  onPress={() => {}}
  isLoading={false}
>

// ❌ Not these
<Button
  disabled={false}
  onClick={() => {}}
  loading={false}
>
```

**Common prop patterns:**

-   `isDisabled` not `disabled`
-   `onPress` not `onClick` (better touch support)
-   `isOpen` / `defaultOpen` for controlled/uncontrolled state
-   `isRequired` not `required`
-   `isReadOnly` not `readonly`

### 3. Styling Approaches

HeroUI components can be styled in 3 ways:

#### A. Tailwind Utilities (Preferred)

```tsx
<Button className="bg-gradient-to-r from-blue-500 to-purple-500">Custom Button</Button>
```

#### B. Component Props

```tsx
<Button
    color="primary" // primary, secondary, success, warning, danger, default
    variant="shadow" // solid, bordered, light, flat, faded, shadow, ghost
    size="lg" // sm, md, lg
    radius="full" // none, sm, md, lg, full
>
    Styled Button
</Button>
```

#### C. Slots (Complex Components)

```tsx
<Table
    classNames={{
        wrapper: "min-h-[400px]",
        th: "bg-primary text-white",
        td: "text-sm",
    }}
>
    {/* Table content */}
</Table>
```

## Component Categories

### Layout Components

```tsx
import { Card, CardHeader, CardBody, CardFooter, Divider, Spacer } from "@heroui/react";

<Card className="max-w-md">
    <CardHeader>
        <h4>Card Title</h4>
    </CardHeader>
    <Divider />
    <CardBody>
        <p>Content goes here</p>
    </CardBody>
    <CardFooter>
        <Button>Action</Button>
    </CardFooter>
</Card>;
```

### Form Components

```tsx
import { Input, Textarea, Select, SelectItem, Checkbox } from '@heroui/react';

// Input with validation
<Input
  label="Email"
  type="email"
  isRequired
  errorMessage="Please enter a valid email"
  isInvalid={!!errors.email}
/>

// Select
<Select
  label="Department"
  placeholder="Select department"
>
  <SelectItem key="hr" value="hr">Human Resources</SelectItem>
  <SelectItem key="it" value="it">Information Technology</SelectItem>
</Select>

// Checkbox
<Checkbox isSelected={isChecked} onValueChange={setIsChecked}>
  I agree to the terms
</Checkbox>
```

### Data Display

```tsx
import { Table, TableHeader, TableColumn, TableBody, TableRow, TableCell } from "@heroui/react";

<Table aria-label="User list">
    <TableHeader>
        <TableColumn>NAME</TableColumn>
        <TableColumn>EMAIL</TableColumn>
        <TableColumn>STATUS</TableColumn>
    </TableHeader>
    <TableBody>
        <TableRow key="1">
            <TableCell>John Doe</TableCell>
            <TableCell>john@example.com</TableCell>
            <TableCell>Active</TableCell>
        </TableRow>
    </TableBody>
</Table>;
```

### Navigation

```tsx
import { Button, Link, Tabs, Tab, Breadcrumbs, BreadcrumbItem } from '@heroui/react';

// Button
<Button onPress={handleClick} color="primary">
  Save Changes
</Button>

// Link
<Link href="/dashboard" color="primary">
  Go to Dashboard
</Link>

// Tabs
<Tabs>
  <Tab key="overview" title="Overview">
    Overview content
  </Tab>
  <Tab key="settings" title="Settings">
    Settings content
  </Tab>
</Tabs>
```

### Feedback Components

```tsx
import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Spinner, Tooltip } from '@heroui/react';

// Modal
<Modal isOpen={isOpen} onClose={onClose}>
  <ModalContent>
    <ModalHeader>Confirm Action</ModalHeader>
    <ModalBody>
      Are you sure you want to proceed?
    </ModalBody>
    <ModalFooter>
      <Button onPress={onClose}>Cancel</Button>
      <Button color="primary" onPress={handleConfirm}>Confirm</Button>
    </ModalFooter>
  </ModalContent>
</Modal>

// Spinner
<Spinner size="lg" color="primary" />

// Tooltip
<Tooltip content="This is helpful information">
  <Button>Hover me</Button>
</Tooltip>
```

## Common Patterns

### Form with React Hook Form + Zod

```tsx
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input, Button } from "@heroui/react";

const schema = z.object({
    email: z.string().email("Invalid email"),
    password: z.string().min(8, "Password must be at least 8 characters"),
});

function LoginForm() {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        resolver: zodResolver(schema),
    });

    const onSubmit = (data) => {
        console.log(data);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <Input
                {...register("email")}
                label="Email"
                type="email"
                isInvalid={!!errors.email}
                errorMessage={errors.email?.message}
            />
            <Input
                {...register("password")}
                label="Password"
                type="password"
                isInvalid={!!errors.password}
                errorMessage={errors.password?.message}
            />
            <Button type="submit" color="primary" fullWidth>
                Login
            </Button>
        </form>
    );
}
```

### Data Table with TanStack Query

```tsx
import { useQuery } from "@tanstack/react-query";
import {
    Table,
    TableHeader,
    TableColumn,
    TableBody,
    TableRow,
    TableCell,
    Spinner,
} from "@heroui/react";

function UserTable() {
    const { data, isLoading } = useQuery({
        queryKey: ["users"],
        queryFn: fetchUsers,
    });

    if (isLoading) {
        return <Spinner size="lg" />;
    }

    return (
        <Table aria-label="Users">
            <TableHeader>
                <TableColumn>NAME</TableColumn>
                <TableColumn>EMAIL</TableColumn>
                <TableColumn>ACTIONS</TableColumn>
            </TableHeader>
            <TableBody>
                {data?.map((user) => (
                    <TableRow key={user.id}>
                        <TableCell>{user.name}</TableCell>
                        <TableCell>{user.email}</TableCell>
                        <TableCell>
                            <Button size="sm" onPress={() => handleEdit(user.id)}>
                                Edit
                            </Button>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}
```

### Dark Mode Integration

HeroUI uses `next-themes` for dark mode:

```tsx
import { useTheme } from "next-themes";
import { Button } from "@heroui/react";

function ThemeToggle() {
    const { theme, setTheme } = useTheme();

    return (
        <Button onPress={() => setTheme(theme === "dark" ? "light" : "dark")} variant="light">
            Toggle Theme
        </Button>
    );
}
```

## Accessibility Checklist

✅ **Every component should have:**

-   Proper ARIA labels (`aria-label`, `aria-labelledby`)
-   Keyboard navigation support (built-in with HeroUI)
-   Focus indicators (automatic with React Aria)
-   Screen reader announcements for dynamic content

✅ **For forms:**

-   Associate labels with inputs
-   Include error messages
-   Mark required fields with `isRequired`
-   Use `errorMessage` prop for validation feedback

✅ **For interactive elements:**

-   Use semantic HTML (`<button>`, `<a>`)
-   Provide descriptive button text
-   Use `onPress` for better touch support

## Troubleshooting

### Issue: Styles not loading

**Solution:** Check `app/globals.css` has correct import order:

```css
@import "tailwindcss"; /* Must be first */
@import "@heroui/styles";
```

### Issue: Component not found

**Solution:** Verify you're importing from `@heroui/react@beta`:

```bash
npm list @heroui/react
# Should show: @heroui/react@3.0.0-beta.x
```

### Issue: TypeScript errors

**Solution:** Ensure React 19 types are installed:

```bash
npm install --save-dev @types/react@19 @types/react-dom@19
```

### Issue: Dark mode not working

**Solution:** Check `next-themes` provider is configured in `app/providers.tsx`:

```tsx
import { ThemeProvider } from "next-themes";

<ThemeProvider attribute="class" defaultTheme="dark">
    {children}
</ThemeProvider>;
```

## File Structure for Components

```
components/
├── common/              # Reusable UI components
│   ├── Button.tsx
│   ├── DataTable.tsx
│   └── Input.tsx
├── layout/              # Layout components
│   ├── Sidebar.tsx
│   ├── Header.tsx
│   └── Footer.tsx
└── features/            # Feature-specific components
    ├── users/
    │   ├── UserForm.tsx
    │   └── UserTable.tsx
    └── auth/
        └── LoginForm.tsx
```

## Resources

-   **Official Docs**: https://v3.heroui.com/docs/react/getting-started
-   **Components**: https://v3.heroui.com/docs/react/components
-   **Styling Guide**: https://v3.heroui.com/docs/react/getting-started/styling
-   **React Aria**: https://react-spectrum.adobe.com/react-aria/
-   **Figma Kit**: https://www.figma.com/community/file/1546526812159103429

## Decision Tree

**When creating a new component:**

1. **Is it a standard UI element?**

    - Yes → Use HeroUI component directly
    - No → Compose from HeroUI primitives

2. **Does it need custom styling?**

    - Simple → Use Tailwind utilities
    - Complex → Use `classNames` slots

3. **Is it a form?**

    - Yes → Use React Hook Form + Zod + HeroUI Input components
    - No → Continue

4. **Does it need data fetching?**

    - Yes → Use TanStack Query
    - No → Use local state

5. **Does it need accessibility beyond defaults?**
    - Yes → Add custom ARIA attributes
    - No → HeroUI handles it automatically

## Quick Reference

### Common Props

```tsx
// Button
<Button color="primary" variant="solid" size="md" radius="md" isDisabled={false} isLoading={false} />

// Input
<Input label="Name" type="text" isRequired isReadOnly={false} isInvalid={false} errorMessage="" />

// Card
<Card shadow="sm" radius="lg" isPressable isBlurred />

// Modal
<Modal isOpen={false} size="md" backdrop="opaque" scrollBehavior="inside" />
```

### Color Options

-   `primary` - Main brand color
-   `secondary` - Secondary brand color
-   `success` - Green
-   `warning` - Yellow/Orange
-   `danger` - Red
-   `default` - Gray

### Size Options

-   `sm` - Small
-   `md` - Medium (default)
-   `lg` - Large

### Variant Options (Button)

-   `solid` - Filled background
-   `bordered` - Outline only
-   `light` - Light background
-   `flat` - No shadow
-   `faded` - Faded background
-   `shadow` - With shadow
-   `ghost` - Minimal styling
