---
name: heroui-layout
description: Hero UI Layout components (Card, Container, Separator, Divider, Surface, Stack, Grid). Use for page structure and content organization in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Layout Components

Complete guide for Hero UI Layout components. For detailed examples, visit the official documentation links below.

## Card Component

**Docs**: https://v3.heroui.com/docs/components/card  
**Import**: `import { Card } from '@heroui/react';`

Container for grouping related content.

### Anatomy

```tsx
<Card>
    <Card.Header>Header content</Card.Header>
    <Card.Body>Main content</Card.Body>
    <Card.Footer>Footer content</Card.Footer>
</Card>
```

### Basic Usage

```tsx
import { Card } from "@heroui/react";

<Card>
    <Card.Header>
        <h3>Card Title</h3>
    </Card.Header>
    <Card.Body>
        <p>Card content goes here</p>
    </Card.Body>
    <Card.Footer>
        <Button>Action</Button>
    </Card.Footer>
</Card>;
```

### Key Props (Card)

-   `variant`: `'flat' | 'bordered' | 'shadow'` (default: `'flat'`)
-   `isHoverable`: `boolean` - Hover effect
-   `isPressable`: `boolean` - Clickable card
-   `isDisabled`: `boolean` - Disabled state
-   `onPress`: `() => void` - Click handler

### Variants

```tsx
// Flat (default)
<Card variant="flat">...</Card>

// Bordered
<Card variant="bordered">...</Card>

// With shadow
<Card variant="shadow">...</Card>
```

### Clickable Card

```tsx
<Card isPressable onPress={() => console.log("Card clicked")}>
    <Card.Body>Click me</Card.Body>
</Card>
```

### With Image

```tsx
<Card>
    <Card.Header>
        <img src="/image.jpg" alt="Card image" className="w-full h-48 object-cover" />
    </Card.Header>
    <Card.Body>
        <h3>Title</h3>
        <p>Description</p>
    </Card.Body>
</Card>
```

**Full docs**: https://v3.heroui.com/docs/components/card

---

## Container Component

**Docs**: https://v3.heroui.com/docs/components/container  
**Import**: `import { Container } from '@heroui/react';`

Responsive width container for content.

### Usage

```tsx
import { Container } from "@heroui/react";

<Container>
    <h1>Page Title</h1>
    <p>Content within max-width container</p>
</Container>;
```

### Max Width Variants

```tsx
// Small (640px)
<Container maxWidth="sm">...</Container>

// Medium (768px)
<Container maxWidth="md">...</Container>

// Large (1024px)
<Container maxWidth="lg">...</Container>

// Extra Large (1280px)
<Container maxWidth="xl">...</Container>

// 2XL (1536px)
<Container maxWidth="2xl">...</Container>

// Full width
<Container maxWidth="full">...</Container>
```

**Full docs**: https://v3.heroui.com/docs/components/container

---

## Separator/Divider Component

**Docs**: https://v3.heroui.com/docs/components/separator  
**Import**: `import { Separator } from '@heroui/react';`

Visual divider between content sections.

### Usage

```tsx
import { Separator } from "@heroui/react";

<div>
    <p>Section 1</p>
    <Separator />
    <p>Section 2</p>
</div>;
```

### Orientation

```tsx
// Horizontal (default)
<Separator orientation="horizontal" />

// Vertical
<div className="flex items-center gap-4">
  <span>Item 1</span>
  <Separator orientation="vertical" className="h-6" />
  <span>Item 2</span>
</div>
```

### With Text

```tsx
<Separator>
    <span className="px-2 text-sm text-gray-500">or</span>
</Separator>
```

**Full docs**: https://v3.heroui.com/docs/components/separator

---

## Surface Component

**Docs**: https://v3.heroui.com/docs/components/surface  
**Import**: `import { Surface } from '@heroui/react';`

Background surface with elevation levels.

### Usage

```tsx
import { Surface } from "@heroui/react";

<Surface>
    <p>Content on elevated surface</p>
</Surface>;
```

### Elevation Levels

```tsx
// Level 0 (default, flat)
<Surface elevation={0}>...</Surface>

// Level 1 (subtle shadow)
<Surface elevation={1}>...</Surface>

// Level 2 (medium shadow)
<Surface elevation={2}>...</Surface>

// Level 3 (strong shadow)
<Surface elevation={3}>...</Surface>
```

**Full docs**: https://v3.heroui.com/docs/components/surface

---

## Stack Component

**Docs**: https://v3.heroui.com/docs/components/stack  
**Import**: `import { Stack, HStack, VStack } from '@heroui/react';`

Flex layout for spacing elements.

### Vertical Stack (VStack)

```tsx
import { VStack } from "@heroui/react";

<VStack spacing={4}>
    <div>Item 1</div>
    <div>Item 2</div>
    <div>Item 3</div>
</VStack>;
```

### Horizontal Stack (HStack)

```tsx
import { HStack } from "@heroui/react";

<HStack spacing={4}>
    <Button>Button 1</Button>
    <Button>Button 2</Button>
    <Button>Button 3</Button>
</HStack>;
```

### Stack Props

-   `spacing`: `number` - Gap between items (in Tailwind spacing units)
-   `align`: `'start' | 'center' | 'end' | 'stretch'` - Cross-axis alignment
-   `justify`: `'start' | 'center' | 'end' | 'between' | 'around' | 'evenly'` - Main-axis alignment

### Custom Alignment

```tsx
<VStack spacing={4} align="center">
  <div>Centered items</div>
</VStack>

<HStack spacing={4} justify="between">
  <div>Left</div>
  <div>Right</div>
</HStack>
```

**Full docs**: https://v3.heroui.com/docs/components/stack

---

## Grid Component

**Docs**: https://v3.heroui.com/docs/components/grid  
**Import**: `import { Grid } from '@heroui/react';`

CSS Grid layout system.

### Usage

```tsx
import { Grid } from "@heroui/react";

<Grid cols={3} gap={4}>
    <div>Item 1</div>
    <div>Item 2</div>
    <div>Item 3</div>
    <div>Item 4</div>
    <div>Item 5</div>
    <div>Item 6</div>
</Grid>;
```

### Responsive Columns

```tsx
<Grid cols={{ base: 1, md: 2, lg: 3 }} gap={4}>
    {/* Items */}
</Grid>
```

### Custom Gap

```tsx
<Grid cols={2} gap={8}>
    {/* Items */}
</Grid>
```

**Full docs**: https://v3.heroui.com/docs/components/grid

---

## Spacer Component

**Docs**: https://v3.heroui.com/docs/components/spacer  
**Import**: `import { Spacer } from '@heroui/react';`

Flexible space between elements.

### Usage

```tsx
import { Spacer } from "@heroui/react";

<div className="flex">
    <div>Left</div>
    <Spacer />
    <div>Right (pushed to end)</div>
</div>;
```

**Full docs**: https://v3.heroui.com/docs/components/spacer

---

## Common Patterns

### Dashboard Cards

```tsx
function Dashboard() {
    return (
        <Container maxWidth="xl">
            <h1>Dashboard</h1>
            <Grid cols={{ base: 1, md: 2, lg: 3 }} gap={6} className="mt-8">
                <Card>
                    <Card.Header>
                        <h3>Total Users</h3>
                    </Card.Header>
                    <Card.Body>
                        <p className="text-3xl font-bold">1,234</p>
                    </Card.Body>
                </Card>

                <Card>
                    <Card.Header>
                        <h3>Revenue</h3>
                    </Card.Header>
                    <Card.Body>
                        <p className="text-3xl font-bold">$45,678</p>
                    </Card.Body>
                </Card>

                <Card>
                    <Card.Header>
                        <h3>Active Sessions</h3>
                    </Card.Header>
                    <Card.Body>
                        <p className="text-3xl font-bold">89</p>
                    </Card.Body>
                </Card>
            </Grid>
        </Container>
    );
}
```

### Product List

```tsx
function ProductList({ products }) {
    return (
        <Grid cols={{ base: 1, sm: 2, lg: 3, xl: 4 }} gap={6}>
            {products.map((product) => (
                <Card key={product.id} isPressable onPress={() => viewProduct(product.id)}>
                    <Card.Header>
                        <img
                            src={product.image}
                            alt={product.name}
                            className="w-full h-48 object-cover"
                        />
                    </Card.Header>
                    <Card.Body>
                        <h3 className="font-semibold">{product.name}</h3>
                        <p className="text-sm text-gray-500">{product.description}</p>
                    </Card.Body>
                    <Card.Footer>
                        <div className="flex justify-between items-center w-full">
                            <span className="text-lg font-bold">${product.price}</span>
                            <Button size="sm">Add to Cart</Button>
                        </div>
                    </Card.Footer>
                </Card>
            ))}
        </Grid>
    );
}
```

### Settings Section

```tsx
function SettingsSection() {
    return (
        <Container maxWidth="md">
            <Card>
                <Card.Header>
                    <h2>Account Settings</h2>
                </Card.Header>
                <Card.Body>
                    <VStack spacing={6}>
                        <div>
                            <h3>Profile</h3>
                            <TextField label="Name" />
                            <TextField label="Email" />
                        </div>

                        <Separator />

                        <div>
                            <h3>Preferences</h3>
                            <Switch>
                                <Switch.Control>
                                    <Switch.Thumb />
                                </Switch.Control>
                                <Label>Email notifications</Label>
                            </Switch>
                        </div>
                    </VStack>
                </Card.Body>
                <Card.Footer>
                    <HStack spacing={4} justify="end">
                        <Button variant="ghost">Cancel</Button>
                        <Button variant="primary">Save Changes</Button>
                    </HStack>
                </Card.Footer>
            </Card>
        </Container>
    );
}
```

### Sidebar Layout

```tsx
function AppLayout({ children }) {
    return (
        <div className="flex h-screen">
            {/* Sidebar */}
            <Surface elevation={1} className="w-64 p-4">
                <VStack spacing={2}>
                    <Link href="/dashboard">Dashboard</Link>
                    <Link href="/users">Users</Link>
                    <Link href="/settings">Settings</Link>
                </VStack>
            </Surface>

            {/* Main content */}
            <div className="flex-1 overflow-auto">
                <Container maxWidth="xl" className="py-8">
                    {children}
                </Container>
            </div>
        </div>
    );
}
```

### Section with Dividers

```tsx
function ContentSections() {
    return (
        <VStack spacing={8}>
            <div>
                <h2>Section 1</h2>
                <p>Content for section 1</p>
            </div>

            <Separator />

            <div>
                <h2>Section 2</h2>
                <p>Content for section 2</p>
            </div>

            <Separator />

            <div>
                <h2>Section 3</h2>
                <p>Content for section 3</p>
            </div>
        </VStack>
    );
}
```

---

## Best Practices

1. **Use Container for page content** to constrain width
2. **Use Card for grouping related content**
3. **Use Grid for responsive layouts** instead of manual media queries
4. **Use VStack/HStack for consistent spacing**
5. **Use Separator between distinct sections**
6. **Use Surface for layered UIs** (modals over cards over background)
7. **Choose appropriate card variants** (flat for minimal, shadow for emphasis)
8. **Make cards clickable** with `isPressable` when appropriate
9. **Use responsive grid columns** for mobile-friendly layouts
10. **Test on different screen sizes**

---

## Accessibility Notes

-   **Card**: Semantic HTML structure (`<article>` or `<section>`)
-   **Clickable cards**: Use `<button>` or proper ARIA attributes
-   **Separator**: `role="separator"` for screen readers
-   **Grid**: Proper heading hierarchy within items
-   **Container**: Doesn't affect accessibility (pure layout)
-   **Focus indicators**: Visible for keyboard navigation
-   **Screen reader order**: Matches visual layout

---

## When to Use This Skill

Use this skill when:

-   Creating page layouts
-   Building dashboard UIs
-   Designing product grids
-   Creating card-based interfaces
-   Building settings pages
-   Implementing sidebar layouts
-   Creating content sections
-   Building responsive layouts
-   Organizing form sections
-   Creating landing pages
