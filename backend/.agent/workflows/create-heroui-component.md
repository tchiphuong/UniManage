# Create HeroUI Component

Create a new frontend UI component using HeroUI v3 for the UniManage application.

## Steps

### 1. Clarify Requirements

Ask the user:

-   What type of component? (Form, Table, Card, Modal, Layout, etc.)
-   What is the component name?
-   What data does it display or collect?
-   Does it need to fetch data from API?
-   Does it need form validation?
-   Does it need to be responsive?
-   Any specific styling requirements?

### 2. Determine Component Category

Classify the component:

-   **Common** - Reusable UI atoms (Button, Input wrapper)
-   **Layout** - Page structure (Sidebar, Header, Footer)
-   **Features** - Domain-specific (UserForm, DepartmentTable)

Location:

```
frontend/uni-manage/components/
├── common/         # Reusable atoms
├── layout/         # Layout components
└── features/       # Feature-specific
    ├── users/
    ├── departments/
    └── ...
```

### 3. Create Component File

File naming: PascalCase - `{ComponentName}.tsx`

**Basic Component Template:**

```tsx
'use client';

import { Card, CardBody, CardHeader } from '@heroui/react';

interface {ComponentName}Props {
  title?: string;
  // Add props as needed
}

export function {ComponentName}({ title }: {ComponentName}Props) {
  return (
    <Card>
      <CardHeader>
        <h3 className="text-lg font-semibold">{title}</h3>
      </CardHeader>
      <CardBody>
        {/* Component content */}
      </CardBody>
    </Card>
  );
}
```

### 4. Add Required Imports

Based on component needs:

**Form Component:**

```tsx
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Input, Button } from "@heroui/react";
```

**Data Display Component:**

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
```

**Modal Component:**

```tsx
import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Button } from "@heroui/react";
```

### 5. Implement Component Logic

**For Form Components:**

```tsx
'use client';

import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Input, Button } from '@heroui/react';
import { useMutation } from '@tanstack/react-query';

const schema = z.object({
  field1: z.string().min(1, 'Field1 is required'),
  field2: z.string().email('Invalid email'),
});

type FormData = z.infer<typeof schema>;

interface {ComponentName}Props {
  onSuccess?: () => void;
}

export function {ComponentName}({ onSuccess }: {ComponentName}Props) {
  const { register, handleSubmit, formState: { errors } } = useForm<FormData>({
    resolver: zodResolver(schema)
  });

  const mutation = useMutation({
    mutationFn: (data: FormData) => {
      // Call API service
      return ServiceName.create(data);
    },
    onSuccess: () => {
      onSuccess?.();
    }
  });

  const onSubmit = (data: FormData) => {
    mutation.mutate(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <Input
        {...register('field1')}
        label="Field 1"
        isRequired
        isInvalid={!!errors.field1}
        errorMessage={errors.field1?.message}
      />
      <Input
        {...register('field2')}
        label="Field 2"
        type="email"
        isInvalid={!!errors.field2}
        errorMessage={errors.field2?.message}
      />
      <Button
        type="submit"
        color="primary"
        fullWidth
        isLoading={mutation.isPending}
      >
        Submit
      </Button>
    </form>
  );
}
```

**For Table Components:**

```tsx
'use client';

import { useQuery } from '@tanstack/react-query';
import { Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Spinner } from '@heroui/react';

interface {ComponentName}Props {
  filters?: any;
}

export function {ComponentName}({ filters }: {ComponentName}Props) {
  const { data, isLoading, error } = useQuery({
    queryKey: ['resource', filters],
    queryFn: () => ServiceName.getList(filters)
  });

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">
        <Spinner size="lg" color="primary" />
      </div>
    );
  }

  if (error) {
    return <div className="text-danger">Error loading data</div>;
  }

  return (
    <Table aria-label="Data table">
      <TableHeader>
        <TableColumn>COLUMN 1</TableColumn>
        <TableColumn>COLUMN 2</TableColumn>
        <TableColumn>ACTIONS</TableColumn>
      </TableHeader>
      <TableBody>
        {data?.items?.map((item) => (
          <TableRow key={item.id}>
            <TableCell>{item.field1}</TableCell>
            <TableCell>{item.field2}</TableCell>
            <TableCell>
              <Button size="sm" onPress={() => handleAction(item.id)}>
                Action
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
```

**For Modal Components:**

```tsx
'use client';

import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Button } from '@heroui/react';

interface {ComponentName}Props {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title?: string;
}

export function {ComponentName}({ isOpen, onClose, onConfirm, title }: {ComponentName}Props) {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalContent>
        <ModalHeader>
          <h3>{title || 'Confirm Action'}</h3>
        </ModalHeader>
        <ModalBody>
          <p>Are you sure you want to proceed?</p>
        </ModalBody>
        <ModalFooter>
          <Button variant="light" onPress={onClose}>
            Cancel
          </Button>
          <Button color="primary" onPress={onConfirm}>
            Confirm
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
}
```

### 6. Add Styling

Use Tailwind utilities and HeroUI props:

```tsx
<div className="p-4 space-y-4">
    <Card className="max-w-md mx-auto">
        <CardBody className="gap-4">
            <Button
                color="primary"
                variant="shadow"
                size="lg"
                radius="full"
                className="font-semibold"
            >
                Styled Button
            </Button>
        </CardBody>
    </Card>
</div>
```

For complex components, use `classNames` prop:

```tsx
<Table
  classNames={{
    wrapper: "min-h-[400px]",
    th: "bg-primary text-white font-bold",
    td: "text-sm"
  }}
>
```

### 7. Add Accessibility

Ensure accessibility:

```tsx
// ARIA labels
<Button aria-label="Delete user" onPress={handleDelete}>
  <Icon icon="solar:trash-bin-bold" />
</Button>

// Form labels
<Input
  label="Email"
  aria-describedby="email-help"
  isRequired
/>

// Loading states
{isLoading ? (
  <Spinner aria-label="Loading data" />
) : (
  <div>{/* Content */}</div>
)}
```

### 8. Add Internationalization

Use `next-intl` for text:

```tsx
import { useTranslations } from 'next-intl';

export function {ComponentName}() {
  const t = useTranslations('ComponentName');

  return (
    <div>
      <h1>{t('title')}</h1>
      <Button>{t('submit')}</Button>
    </div>
  );
}
```

Create translation files:

```json
// messages/en.json
{
  "ComponentName": {
    "title": "Component Title",
    "submit": "Submit"
  }
}

// messages/vi.json
{
  "ComponentName": {
    "title": "Tiêu đề Component",
    "submit": "Gửi"
  }
}
```

### 9. Add TypeScript Types

Create type definition:

```tsx
// types/{ComponentName}.types.ts
export interface {Entity} {
  id: number;
  field1: string;
  field2: string;
  createdAt: string;
}

export interface {ComponentName}Props {
  data?: {Entity}[];
  onAction?: (id: number) => void;
}
```

Import in component:

```tsx
import type { {Entity}, {ComponentName}Props } from '@/types/{ComponentName}.types';
```

### 10. Create API Service (if needed)

Location: `services/{ResourceName}Service.ts`

```tsx
import { httpClient } from '@/lib/http-client';

export class {Resource}Service {
  static async getList(params?: any) {
    const response = await httpClient.get('/api/v1/resource', { params });
    return response.data;
  }

  static async getById(id: number) {
    const response = await httpClient.get(`/api/v1/resource/${id}`);
    return response.data;
  }

  static async create(data: any) {
    const response = await httpClient.post('/api/v1/resource', data);
    return response.data;
  }

  static async update(id: number, data: any) {
    const response = await httpClient.put(`/api/v1/resource/${id}`, data);
    return response.data;
  }

  static async delete(id: number) {
    const response = await httpClient.delete(`/api/v1/resource/${id}`);
    return response.data;
  }
}
```

### 11. Test Component

1. **Import in a page:**

```tsx
// app/(dashboard)/resource/page.tsx
import { {ComponentName} } from '@/components/features/resource/{ComponentName}';

export default function ResourcePage() {
  return (
    <div className="container mx-auto p-4">
      <{ComponentName} />
    </div>
  );
}
```

2. **Run dev server:**

```bash
npm run dev
```

3. **Test in browser:**

-   Open http://localhost:3000/resource
-   Test interactions (form submission, button clicks, etc.)
-   Test responsive design (resize browser)
-   Test dark mode toggle
-   Test keyboard navigation
-   Check console for errors

### 12. Verify Checklist

✅ Imports from `@heroui/react` (not NextUI)
✅ Uses `onPress` instead of `onClick`
✅ Form uses React Hook Form + Zod (if applicable)
✅ Data fetching uses TanStack Query (if applicable)
✅ Text uses `next-intl` translations
✅ TypeScript types defined
✅ ARIA labels for accessibility
✅ Loading and error states handled
✅ Responsive design with Tailwind
✅ File naming follows PascalCase
✅ Imports organized properly
✅ `'use client'` directive added (if needed)

## Summary

This workflow creates a complete HeroUI component with:

-   Proper file structure and naming
-   HeroUI v3 components with correct API
-   Form validation (if applicable)
-   Data fetching (if applicable)
-   Accessibility features
-   Internationalization
-   TypeScript types
-   Responsive styling
-   Error handling
