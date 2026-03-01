# Frontend Coding Standards

**Activation**: Glob Pattern `frontend/**/*.{ts,tsx,jsx}`

This rule applies to all frontend development in the UniManage project.

## Tech Stack Requirements

-   **Next.js 15** (App Router)
-   **React 19**
-   **TypeScript 5+**
-   **HeroUI v3 Beta** (`@heroui/react@beta`)
-   **Tailwind CSS v4**
-   **TanStack Query v5** for data fetching
-   **React Hook Form + Zod** for forms
-   **next-intl** for i18n
-   **next-themes** for dark mode

## HeroUI v3 Component Standards

### Import Pattern

**Always** import from `@heroui/react`:

```tsx
// ✅ CORRECT
import { Button, Card, Input, Table } from "@heroui/react";

// ❌ WRONG - Old NextUI
import { Button } from "@nextui-org/react";
```

### API Conventions (React Aria)

HeroUI uses React Aria naming:

```tsx
// ✅ USE THESE
<Button
  isDisabled={false}
  onPress={() => {}}
  isLoading={false}
  isRequired={true}
>

// ❌ NOT THESE
<Button
  disabled={false}
  onClick={() => {}}
  loading={false}
  required={true}
>
```

**Common patterns:**

-   `isDisabled` not `disabled`
-   `onPress` not `onClick` (better touch support)
-   `isOpen` / `defaultOpen` for controlled/uncontrolled
-   `isRequired` not `required`
-   `isReadOnly` not `readonly`

## Styling Standards

### Three Approaches (in order of preference)

1. **Tailwind Utilities** (Preferred)

```tsx
<Button className="bg-gradient-to-r from-blue-500 to-purple-500">Custom Button</Button>
```

2. **Component Props**

```tsx
<Button
  color="primary"      // primary, secondary, success, warning, danger
  variant="shadow"     // solid, bordered, light, flat, faded, shadow, ghost
  size="lg"           // sm, md, lg
  radius="full"       // none, sm, md, lg, full
>
```

3. **Slots (for complex components)**

```tsx
<Table
  classNames={{
    wrapper: "min-h-[400px]",
    th: "bg-primary text-white"
  }}
>
```

## Component Organization

### File Structure

```
components/
├── common/         # Reusable UI atoms (Button, Input)
├── layout/         # Layout components (Sidebar, Header)
└── features/       # Feature-specific components
    ├── users/      # User-related components
    └── auth/       # Auth-related components
```

### Component Template

```tsx
"use client";

import { useState } from "react";
import { Button, Card, CardBody } from "@heroui/react";

interface MyComponentProps {
    title: string;
    onAction?: () => void;
}

export function MyComponent({ title, onAction }: MyComponentProps) {
    const [state, setState] = useState(false);

    return (
        <Card>
            <CardBody>
                <h2>{title}</h2>
                <Button onPress={onAction}>Action</Button>
            </CardBody>
        </Card>
    );
}
```

## Form Standards

### 🔥 sy_commons Data Fields (BẮT BUỘC)

**QUY TẮC:** Field nào lấy data từ bảng `sy_commons` PHẢI dùng `Select` (combobox), KHÔNG dùng `Input`.

**Ví dụ các fields từ sy_commons:**

-   Status (Active/Inactive)
-   Country (Vietnam, USA, Japan, etc.)
-   Province/City
-   District
-   Ward
-   Gender (Male/Female/Other)
-   Department Type
-   Position Level
-   Contract Type
-   Employment Status
-   Document Type
-   Priority Level

**✅ ĐÚNG - Dùng Select cho sy_commons data:**

```tsx
import { Select, SelectItem } from "@heroui/react";

// Fetch options từ API
const { data: statusOptions } = useQuery({
    queryKey: ["commons", "status"],
    queryFn: () => CommonService.getByType("Status"),
});

<Select
    {...register("status")}
    label="Status"
    placeholder="Select status"
    isInvalid={!!errors.status}
    errorMessage={errors.status?.message}
>
    {statusOptions?.map((option) => (
        <SelectItem key={option.code} value={option.code}>
            {option.name}
        </SelectItem>
    ))}
</Select>;
```

**❌ SAI - Dùng Input cho sy_commons data:**

```tsx
// ❌ WRONG - Status nên là combobox, không phải text input
<Input {...register("status")} label="Status" placeholder="Enter status" />
```

**Lý do:**

-   Đảm bảo data consistency (chỉ nhập giá trị hợp lệ từ sy_commons)
-   UX tốt hơn (user chọn thay vì gõ)
-   Tránh typo và validation errors
-   Hỗ trợ đa ngôn ngữ (sy_commons có cả NameEn và NameVn)

### React Hook Form + Zod Pattern

```tsx
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input, Button } from "@heroui/react";

const schema = z.object({
    email: z.string().email("Invalid email"),
    password: z.string().min(8, "Min 8 characters"),
});

type FormData = z.infer<typeof schema>;

export function MyForm() {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<FormData>({
        resolver: zodResolver(schema),
    });

    const onSubmit = (data: FormData) => {
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
                Submit
            </Button>
        </form>
    );
}
```

## Data Fetching Standards

### TanStack Query Pattern

```tsx
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { UserService } from "@/services/UserService";

export function UserList() {
    const queryClient = useQueryClient();

    // Query
    const { data, isLoading, error } = useQuery({
        queryKey: ["users"],
        queryFn: UserService.getList,
    });

    // Mutation
    const createMutation = useMutation({
        mutationFn: UserService.create,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["users"] });
        },
    });

    if (isLoading) return <Spinner />;
    if (error) return <div>Error: {error.message}</div>;

    return <div>{/* Render data */}</div>;
}
```

## TypeScript Standards

### Type Definitions

```tsx
// Props interface
interface ComponentProps {
    id: number;
    name: string;
    onUpdate?: (id: number) => void;
}

// API Response type
interface ApiResponse<T> {
    returnCode: number;
    message: string;
    data: T;
    errors: string[];
}

// Use type for unions
type Status = "active" | "inactive" | "pending";
```

### Avoid `any`

```tsx
// ❌ BAD
const handleData = (data: any) => {};

// ✅ GOOD
const handleData = (data: unknown) => {
    if (isValidData(data)) {
        // Type-safe handling
    }
};
```

## Accessibility Standards

All components must:

-   ✅ Have proper ARIA labels (`aria-label`, `aria-labelledby`)
-   ✅ Support keyboard navigation (built-in with HeroUI)
-   ✅ Include error messages for forms
-   ✅ Use semantic HTML (`<button>`, `<a>`, `<nav>`)

```tsx
// Good accessibility
<Button
  aria-label="Delete user"
  onPress={handleDelete}
>
  <Icon icon="solar:trash-bin-bold" />
</Button>

<Input
  label="Email"
  isRequired
  errorMessage={errors.email?.message}
  aria-describedby="email-help"
/>
```

## Internationalization Standards

Use `next-intl` for all user-facing text:

```tsx
import { useTranslations } from "next-intl";

export function MyComponent() {
    const t = useTranslations("Common");

    return <Button>{t("save")}</Button>;
}
```

## Dark Mode Standards

Use `next-themes` via HeroUI:

```tsx
import { useTheme } from "next-themes";

export function ThemeToggle() {
    const { theme, setTheme } = useTheme();

    return (
        <Button onPress={() => setTheme(theme === "dark" ? "light" : "dark")}>Toggle Theme</Button>
    );
}
```

## File Naming Conventions

-   **Components**: PascalCase - `UserCard.tsx`
-   **Hooks**: camelCase - `useAuth.ts`
-   **Utils**: camelCase - `formatDate.ts`
-   **Types**: PascalCase - `User.types.ts`
-   **Services**: PascalCase - `UserService.ts`

## Import Order

```tsx
// 1. React & Next.js
import { useState } from "react";
import { useRouter } from "next/navigation";

// 2. Third-party libraries
import { useQuery } from "@tanstack/react-query";
import { Button, Card } from "@heroui/react";

// 3. Internal imports (absolute paths)
import { UserService } from "@/services/UserService";
import { useAuth } from "@/hooks/useAuth";

// 4. Types
import type { User } from "@/types/User";

// 5. Styles (if any)
import styles from "./Component.module.css";
```

## Code Quality Checklist

Before submitting frontend code:

✅ Imports from `@heroui/react` (not NextUI)
✅ Uses `onPress` instead of `onClick`
✅ Forms use React Hook Form + Zod
✅ Data fetching uses TanStack Query
✅ All text uses `next-intl` translations
✅ TypeScript types defined (no `any`)
✅ ARIA labels for accessibility
✅ Proper error handling and loading states
✅ Responsive design with Tailwind utilities
✅ File naming follows conventions
✅ Import order organized

## Common Mistakes to Avoid

❌ Importing from old NextUI package
❌ Using `onClick` instead of `onPress`
❌ Forgetting `'use client'` directive for client components
❌ Not handling loading/error states
❌ Hardcoded text instead of translations
❌ Missing TypeScript types
❌ Not using HeroUI built-in accessibility features
