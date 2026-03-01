---
name: heroui-feedback
description: Hero UI Feedback components (Alert, Progress, Spinner, Skeleton, CircularProgress). Use for loading states, alerts, notifications in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Feedback Components

Complete guide for Hero UI Feedback components. For detailed examples, visit the official documentation links below.

## Alert Component

**Docs**: https://v3.heroui.com/docs/components/alert  
**Import**: `import { Alert } from '@heroui/react';`

Display important messages and notifications.

### Usage

```tsx
import { Alert } from "@heroui/react";

<Alert status="success">
    <Alert.Icon />
    <Alert.Title>Success!</Alert.Title>
    <Alert.Description>Your changes have been saved.</Alert.Description>
    <Alert.CloseButton />
</Alert>;
```

### Status Variants

```tsx
// Success (green)
<Alert status="success">
  <Alert.Icon />
  <Alert.Title>Success</Alert.Title>
  <Alert.Description>Operation completed successfully</Alert.Description>
</Alert>

// Warning (yellow/orange)
<Alert status="warning">
  <Alert.Icon />
  <Alert.Title>Warning</Alert.Title>
  <Alert.Description>Please review before continuing</Alert.Description>
</Alert>

// Error (red)
<Alert status="error">
  <Alert.Icon />
  <Alert.Title>Error</Alert.Title>
  <Alert.Description>Something went wrong</Alert.Description>
</Alert>

// Info (blue)
<Alert status="info">
  <Alert.Icon />
  <Alert.Title>Info</Alert.Title>
  <Alert.Description>Here's some information</Alert.Description>
</Alert>
```

### With Close Button

```tsx
function DismissableAlert() {
    const [visible, setVisible] = useState(true);

    if (!visible) return null;

    return (
        <Alert status="info">
            <Alert.Icon />
            <Alert.Title>Tip</Alert.Title>
            <Alert.Description>You can dismiss this alert</Alert.Description>
            <Alert.CloseButton onPress={() => setVisible(false)} />
        </Alert>
    );
}
```

**Full docs**: https://v3.heroui.com/docs/components/alert

---

## Progress Component

**Docs**: https://v3.heroui.com/docs/components/progress  
**Import**: `import { Progress } from '@heroui/react';`

Linear progress indicator for task completion.

### Usage

```tsx
import { Progress } from '@heroui/react';

<Progress value={60} />
<Progress value={60} label="Uploading..." />
<Progress value={60} showValueLabel />
```

### Key Props

-   `value`: `number` (0-100) - Current progress value
-   `label`: `string` - Progress label
-   `showValueLabel`: `boolean` - Show percentage
-   `size`: `'sm' | 'md' | 'lg'` (default: `'md'`)
-   `color`: `'primary' | 'secondary' | 'success' | 'warning' | 'danger'`
-   `isIndeterminate`: `boolean` - Show loading animation

### Indeterminate (Loading)

```tsx
<Progress isIndeterminate label="Loading..." />
```

### With Value Label

```tsx
<Progress value={75} showValueLabel label="Uploading file..." />
```

### Color Variants

```tsx
<Progress value={60} color="success" />
<Progress value={60} color="warning" />
<Progress value={60} color="danger" />
```

**Full docs**: https://v3.heroui.com/docs/components/progress

---

## CircularProgress Component

**Docs**: https://v3.heroui.com/docs/components/circular-progress  
**Import**: `import { CircularProgress } from '@heroui/react';`

Circular progress indicator.

### Usage

```tsx
import { CircularProgress } from '@heroui/react';

<CircularProgress value={60} />
<CircularProgress value={60} showValueLabel />
```

### Key Props

-   `value`: `number` (0-100) - Current progress value
-   `showValueLabel`: `boolean` - Show percentage in center
-   `size`: `'sm' | 'md' | 'lg'` (default: `'md'`)
-   `color`: `'primary' | 'secondary' | 'success' | 'warning' | 'danger'`
-   `isIndeterminate`: `boolean` - Show loading animation
-   `strokeWidth`: `number` - Width of progress track

### Indeterminate (Loading)

```tsx
<CircularProgress isIndeterminate />
```

### With Custom Stroke

```tsx
<CircularProgress value={75} strokeWidth={6} size="lg" />
```

**Full docs**: https://v3.heroui.com/docs/components/circular-progress

---

## Spinner Component

**Docs**: https://v3.heroui.com/docs/components/spinner  
**Import**: `import { Spinner } from '@heroui/react';`

Simple loading spinner.

### Usage

```tsx
import { Spinner } from '@heroui/react';

<Spinner />
<Spinner size="sm" />
<Spinner size="lg" />
<Spinner label="Loading..." />
```

### Key Props

-   `size`: `'sm' | 'md' | 'lg'` (default: `'md'`)
-   `color`: `'primary' | 'secondary' | 'success' | 'warning' | 'danger'`
-   `label`: `string` - Loading text

### Size Variants

```tsx
<Spinner size="sm" />
<Spinner size="md" />
<Spinner size="lg" />
```

### With Label

```tsx
<Spinner label="Loading data..." />
```

### Color Variants

```tsx
<Spinner color="success" />
<Spinner color="warning" />
<Spinner color="danger" />
```

**Full docs**: https://v3.heroui.com/docs/components/spinner

---

## Skeleton Component

**Docs**: https://v3.heroui.com/docs/components/skeleton  
**Import**: `import { Skeleton } from '@heroui/react';`

Placeholder for content that's loading.

### Usage

```tsx
import { Skeleton } from '@heroui/react';

<Skeleton className="h-4 w-32" />
<Skeleton className="h-12 w-12 rounded-full" />
```

### Content Placeholders

```tsx
function UserCardSkeleton() {
    return (
        <div className="flex items-center gap-4">
            <Skeleton className="h-12 w-12 rounded-full" />
            <div className="space-y-2">
                <Skeleton className="h-4 w-32" />
                <Skeleton className="h-3 w-24" />
            </div>
        </div>
    );
}
```

### Loading State Pattern

```tsx
function UserProfile({ userId }) {
    const { data, isLoading } = useUser(userId);

    if (isLoading) {
        return (
            <div className="space-y-4">
                <Skeleton className="h-8 w-48" />
                <Skeleton className="h-4 w-full" />
                <Skeleton className="h-4 w-3/4" />
            </div>
        );
    }

    return <div>{/* Actual content */}</div>;
}
```

**Full docs**: https://v3.heroui.com/docs/components/skeleton

---

## Toast/Notification

**Docs**: https://v3.heroui.com/docs/components/toast  
**Import**: `import { toast, Toaster } from '@heroui/react';`

Show temporary notifications.

### Setup

```tsx
// In your root layout
import { Toaster } from "@heroui/react";

export default function RootLayout({ children }) {
    return (
        <html>
            <body>
                {children}
                <Toaster />
            </body>
        </html>
    );
}
```

### Usage

```tsx
import { toast } from "@heroui/react";

// Success toast
toast.success("User created successfully");

// Error toast
toast.error("Failed to save changes");

// Warning toast
toast.warning("Your session will expire soon");

// Info toast
toast.info("New features available");

// Custom toast
toast("Custom message", {
    duration: 5000,
    position: "top-right",
});
```

### With Actions

```tsx
toast.success("File uploaded", {
    action: {
        label: "View",
        onClick: () => console.log("View clicked"),
    },
});
```

**Full docs**: https://v3.heroui.com/docs/components/toast

---

## Common Patterns

### Form Submission Feedback

```tsx
function SubmitForm() {
    const [isLoading, setIsLoading] = useState(false);

    const handleSubmit = async (data) => {
        setIsLoading(true);
        try {
            await submitData(data);
            toast.success("Form submitted successfully");
        } catch (error) {
            toast.error("Failed to submit form");
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            {/* form fields */}
            <Button type="submit" isPending={isLoading}>
                {isLoading ? <Spinner size="sm" /> : "Submit"}
            </Button>
        </form>
    );
}
```

### File Upload Progress

```tsx
function FileUpload() {
    const [progress, setProgress] = useState(0);
    const [isUploading, setIsUploading] = useState(false);

    const handleUpload = async (file) => {
        setIsUploading(true);

        // Simulate upload progress
        const interval = setInterval(() => {
            setProgress((prev) => {
                if (prev >= 100) {
                    clearInterval(interval);
                    setIsUploading(false);
                    toast.success("File uploaded successfully");
                    return 100;
                }
                return prev + 10;
            });
        }, 500);
    };

    return (
        <div className="space-y-4">
            <input type="file" onChange={(e) => handleUpload(e.target.files[0])} />
            {isUploading && <Progress value={progress} label="Uploading..." showValueLabel />}
        </div>
    );
}
```

### Loading State

```tsx
function DataList() {
    const { data, isLoading, error } = useData();

    if (error) {
        return (
            <Alert status="error">
                <Alert.Icon />
                <Alert.Title>Error</Alert.Title>
                <Alert.Description>{error.message}</Alert.Description>
            </Alert>
        );
    }

    if (isLoading) {
        return (
            <div className="space-y-4">
                {[1, 2, 3].map((i) => (
                    <Skeleton key={i} className="h-20 w-full" />
                ))}
            </div>
        );
    }

    return (
        <div>
            {data.map((item) => (
                <ItemCard key={item.id} item={item} />
            ))}
        </div>
    );
}
```

### Info Banner

```tsx
function InfoBanner() {
    const [dismissed, setDismissed] = useState(false);

    if (dismissed) return null;

    return (
        <Alert status="info" className="mb-4">
            <Alert.Icon />
            <Alert.Title>New Feature Available</Alert.Title>
            <Alert.Description>Check out our new dashboard analytics feature.</Alert.Description>
            <Alert.CloseButton onPress={() => setDismissed(true)} />
        </Alert>
    );
}
```

---

## Best Practices

1. **Use appropriate status colors** (success=green, error=red, warning=yellow)
2. **Show loading indicators** for async operations
3. **Use Skeleton for content placeholders** (better than spinners for layouts)
4. **Keep toast messages brief** (1-2 lines max)
5. **Provide actionable feedback** (e.g., "Undo" action in toast)
6. **Auto-dismiss toasts** after 3-5 seconds
7. **Show progress for long operations** (file uploads, exports)
8. **Use CircularProgress for space-constrained areas**
9. **Group related alerts** (don't spam multiple alerts)
10. **Test loading states** thoroughly

---

## Accessibility Notes

-   **Alert**: `role="alert"` for screen readers
-   **Progress**: `aria-valuenow`, `aria-valuemin`, `aria-valuemax`
-   **Spinner**: `aria-label="Loading"` for context
-   **Toast**: Announced by screen readers
-   **Skeleton**: Use `aria-busy="true"` on parent
-   **Focus management**: Don't trap focus in loading states
-   **Color**: Don't rely solely on color (use icons/text)

---

## When to Use This Skill

Use this skill when:

-   Showing success/error/warning messages
-   Implementing loading states
-   Creating file upload progress
-   Building data fetching UIs
-   Showing notifications/toasts
-   Implementing skeleton screens
-   Creating progress indicators
-   Building async operation feedback
-   Implementing form submission feedback
