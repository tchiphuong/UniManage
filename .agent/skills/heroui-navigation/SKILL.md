---
name: heroui-navigation
description: Hero UI Navigation components (Tabs, Accordion, Disclosure, Link, Breadcrumbs). Use for page navigation, content organization in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Navigation Components

Complete guide for Hero UI Navigation components. For detailed examples, visit the official documentation links below.

## Tabs Component

**Docs**: https://v3.heroui.com/docs/components/tabs  
**Import**: `import { Tabs } from '@heroui/react';`

Organize content into multiple panels with tab navigation.

### Anatomy

```tsx
<Tabs>
    <Tabs.ListContainer>
        <Tabs.List>
            <Tabs.Tab key="tab1">Tab 1</Tabs.Tab>
            <Tabs.Tab key="tab2">Tab 2</Tabs.Tab>
            <Tabs.Tab key="tab3">Tab 3</Tabs.Tab>
            <Tabs.Indicator />
        </Tabs.List>
    </Tabs.ListContainer>

    <Tabs.Panel key="tab1">Panel 1 content</Tabs.Panel>
    <Tabs.Panel key="tab2">Panel 2 content</Tabs.Panel>
    <Tabs.Panel key="tab3">Panel 3 content</Tabs.Panel>
</Tabs>
```

### Key Props (Tabs)

-   `selectedKey`: `string` - Controlled selected tab
-   `defaultSelectedKey`: `string` - Default selected tab (uncontrolled)
-   `onSelectionChange`: `(key: string) => void` - Selection change handler
-   `orientation`: `'horizontal' | 'vertical'` (default: `'horizontal'`)
-   `hideSeparator`: `boolean` - Hide tab list separator

### Key Props (Tabs.Tab)

-   `key`: `string` (required) - Unique identifier
-   `isDisabled`: `boolean` - Disable tab

### Basic Usage

```tsx
import { Tabs } from "@heroui/react";

function TabsExample() {
    return (
        <Tabs defaultSelectedKey="profile">
            <Tabs.ListContainer>
                <Tabs.List>
                    <Tabs.Tab key="profile">Profile</Tabs.Tab>
                    <Tabs.Tab key="settings">Settings</Tabs.Tab>
                    <Tabs.Tab key="billing">Billing</Tabs.Tab>
                    <Tabs.Indicator />
                </Tabs.List>
            </Tabs.ListContainer>

            <Tabs.Panel key="profile">
                <h2>Profile Information</h2>
                <p>Manage your profile details</p>
            </Tabs.Panel>

            <Tabs.Panel key="settings">
                <h2>Settings</h2>
                <p>Configure your preferences</p>
            </Tabs.Panel>

            <Tabs.Panel key="billing">
                <h2>Billing</h2>
                <p>View your billing history</p>
            </Tabs.Panel>
        </Tabs>
    );
}
```

### Controlled Tabs

```tsx
import { Tabs } from "@heroui/react";
import { useState } from "react";

function ControlledTabs() {
    const [selected, setSelected] = useState("tab1");

    return (
        <Tabs selectedKey={selected} onSelectionChange={setSelected}>
            <Tabs.ListContainer>
                <Tabs.List>
                    <Tabs.Tab key="tab1">Tab 1</Tabs.Tab>
                    <Tabs.Tab key="tab2">Tab 2</Tabs.Tab>
                </Tabs.List>
            </Tabs.ListContainer>

            <Tabs.Panel key="tab1">Content 1</Tabs.Panel>
            <Tabs.Panel key="tab2">Content 2</Tabs.Panel>
        </Tabs>
    );
}
```

### Vertical Tabs

```tsx
<Tabs orientation="vertical">
    <Tabs.ListContainer>
        <Tabs.List>
            <Tabs.Tab key="tab1">Tab 1</Tabs.Tab>
            <Tabs.Tab key="tab2">Tab 2</Tabs.Tab>
        </Tabs.List>
    </Tabs.ListContainer>

    <Tabs.Panel key="tab1">Content 1</Tabs.Panel>
    <Tabs.Panel key="tab2">Content 2</Tabs.Panel>
</Tabs>
```

### Disabled Tab

```tsx
<Tabs.Tab key="disabled" isDisabled>
    Disabled Tab
</Tabs.Tab>
```

---

## Accordion Component

**Docs**: https://v3.heroui.com/docs/components/accordion  
**Import**: `import { Accordion } from '@heroui/react';`

Collapsible content panels for organizing information.

### Anatomy

```tsx
<Accordion>
    <Accordion.Item key="item1">
        <Accordion.Trigger>
            <Accordion.Title>Item 1</Accordion.Title>
            <Accordion.Icon />
        </Accordion.Trigger>
        <Accordion.Panel>
            <Accordion.Content>Content 1</Accordion.Content>
        </Accordion.Panel>
    </Accordion.Item>
</Accordion>
```

### Key Props (Accordion)

-   `type`: `'single' | 'multiple'` (default: `'single'`)
    -   `single`: Only one item open at a time
    -   `multiple`: Multiple items can be open
-   `defaultValue`: `string | string[]` - Default expanded item(s)
-   `value`: `string | string[]` - Controlled expanded item(s)
-   `onValueChange`: `(value: string | string[]) => void`
-   `collapsible`: `boolean` - Allow all items to be collapsed (for `type="single"`)

### Basic Usage

```tsx
import { Accordion } from "@heroui/react";

function AccordionExample() {
    return (
        <Accordion defaultValue="item1">
            <Accordion.Item key="item1">
                <Accordion.Trigger>
                    <Accordion.Title>What is UniManage?</Accordion.Title>
                    <Accordion.Icon />
                </Accordion.Trigger>
                <Accordion.Panel>
                    <Accordion.Content>
                        UniManage is a comprehensive management system.
                    </Accordion.Content>
                </Accordion.Panel>
            </Accordion.Item>

            <Accordion.Item key="item2">
                <Accordion.Trigger>
                    <Accordion.Title>How to get started?</Accordion.Title>
                    <Accordion.Icon />
                </Accordion.Trigger>
                <Accordion.Panel>
                    <Accordion.Content>
                        Follow the setup guide in the documentation.
                    </Accordion.Content>
                </Accordion.Panel>
            </Accordion.Item>
        </Accordion>
    );
}
```

### Multiple Items Open

```tsx
<Accordion type="multiple" defaultValue={["item1", "item2"]}>
    <Accordion.Item key="item1">...</Accordion.Item>
    <Accordion.Item key="item2">...</Accordion.Item>
    <Accordion.Item key="item3">...</Accordion.Item>
</Accordion>
```

### Controlled Accordion

```tsx
function ControlledAccordion() {
    const [value, setValue] = useState(["item1"]);

    return (
        <Accordion type="multiple" value={value} onValueChange={setValue}>
            <Accordion.Item key="item1">...</Accordion.Item>
            <Accordion.Item key="item2">...</Accordion.Item>
        </Accordion>
    );
}
```

---

## Disclosure Component

**Docs**: https://v3.heroui.com/docs/components/disclosure  
**Import**: `import { Disclosure } from '@heroui/react';`

Single collapsible content section (simpler than Accordion).

### Usage

```tsx
import { Disclosure } from "@heroui/react";

<Disclosure>
    <Disclosure.Trigger>
        <Disclosure.Title>Click to expand</Disclosure.Title>
        <Disclosure.Icon />
    </Disclosure.Trigger>
    <Disclosure.Panel>
        <Disclosure.Content>Hidden content revealed on click</Disclosure.Content>
    </Disclosure.Panel>
</Disclosure>;
```

### Controlled Disclosure

```tsx
function ControlledDisclosure() {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <Disclosure isOpen={isOpen} onOpenChange={setIsOpen}>
            <Disclosure.Trigger>
                <Disclosure.Title>Expand me</Disclosure.Title>
                <Disclosure.Icon />
            </Disclosure.Trigger>
            <Disclosure.Panel>
                <Disclosure.Content>Content here</Disclosure.Content>
            </Disclosure.Panel>
        </Disclosure>
    );
}
```

---

## DisclosureGroup Component

**Docs**: https://v3.heroui.com/docs/components/disclosure-group  
**Import**: `import { DisclosureGroup } from '@heroui/react';`

Group multiple Disclosure components (similar to Accordion).

### Usage

```tsx
import { DisclosureGroup, Disclosure } from "@heroui/react";

<DisclosureGroup>
    <Disclosure>
        <Disclosure.Trigger>
            <Disclosure.Title>Section 1</Disclosure.Title>
            <Disclosure.Icon />
        </Disclosure.Trigger>
        <Disclosure.Panel>Content 1</Disclosure.Panel>
    </Disclosure>

    <Disclosure>
        <Disclosure.Trigger>
            <Disclosure.Title>Section 2</Disclosure.Title>
            <Disclosure.Icon />
        </Disclosure.Trigger>
        <Disclosure.Panel>Content 2</Disclosure.Panel>
    </Disclosure>
</DisclosureGroup>;
```

---

## Link Component

**Docs**: https://v3.heroui.com/docs/components/link  
**Import**: `import { Link } from '@heroui/react';`

Hyperlink component with Next.js integration.

### Basic Usage

```tsx
import { Link } from '@heroui/react';

<Link href="/about">About Us</Link>
<Link href="https://example.com" target="_blank">External Link</Link>
```

### Key Props

-   `href`: `string` - Link destination
-   `target`: `'_self' | '_blank' | '_parent' | '_top'`
-   `rel`: `string` - Relationship (e.g., `'noopener noreferrer'`)
-   `underline`: `'none' | 'hover' | 'always'` (default: `'hover'`)
-   `isDisabled`: `boolean` - Disable link
-   `variant`: `'primary' | 'secondary'` - Visual style

### Usage with Next.js

```tsx
import { Link } from "@heroui/react";
import NextLink from "next/link";

<Link as={NextLink} href="/dashboard">
    Dashboard
</Link>;
```

### External Link

```tsx
<Link href="https://docs.unimanage.com" target="_blank" rel="noopener noreferrer">
    Documentation
</Link>
```

---

## Breadcrumbs Component

**Docs**: https://v3.heroui.com/docs/components/breadcrumbs  
**Import**: `import { Breadcrumbs } from '@heroui/react';`

Navigation trail showing current location in hierarchy.

### Usage

```tsx
import { Breadcrumbs, Breadcrumb } from "@heroui/react";

<Breadcrumbs>
    <Breadcrumb href="/">Home</Breadcrumb>
    <Breadcrumb href="/products">Products</Breadcrumb>
    <Breadcrumb href="/products/laptop">Laptop</Breadcrumb>
    <Breadcrumb>Dell XPS 15</Breadcrumb>
</Breadcrumbs>;
```

### Key Props (Breadcrumbs)

-   `separator`: `ReactNode` - Custom separator (default: `/`)
-   `maxItems`: `number` - Max items before truncation
-   `itemsBeforeCollapse`: `number` - Items to show before collapse
-   `itemsAfterCollapse`: `number` - Items to show after collapse

### With Custom Separator

```tsx
<Breadcrumbs separator=">">
    <Breadcrumb href="/">Home</Breadcrumb>
    <Breadcrumb href="/docs">Docs</Breadcrumb>
    <Breadcrumb>Current Page</Breadcrumb>
</Breadcrumbs>
```

### With Collapse

```tsx
<Breadcrumbs maxItems={4} itemsBeforeCollapse={1} itemsAfterCollapse={2}>
    <Breadcrumb href="/">Home</Breadcrumb>
    <Breadcrumb href="/level1">Level 1</Breadcrumb>
    <Breadcrumb href="/level2">Level 2</Breadcrumb>
    <Breadcrumb href="/level3">Level 3</Breadcrumb>
    <Breadcrumb href="/level4">Level 4</Breadcrumb>
    <Breadcrumb>Current</Breadcrumb>
</Breadcrumbs>
```

---

## Pagination Component

**Docs**: https://v3.heroui.com/docs/components/pagination  
**Import**: `import { Pagination } from '@heroui/react';`

Navigate through pages of content.

### Usage

```tsx
import { Pagination } from "@heroui/react";

function PaginationExample() {
    const [page, setPage] = useState(1);

    return <Pagination total={10} page={page} onChange={setPage} />;
}
```

### Key Props

-   `total`: `number` - Total number of pages
-   `page`: `number` - Current page (1-indexed)
-   `defaultPage`: `number` - Default page (uncontrolled)
-   `onChange`: `(page: number) => void` - Page change handler
-   `siblings`: `number` - Sibling pages to show (default: `1`)
-   `boundaries`: `number` - Boundary pages to show (default: `1`)
-   `showControls`: `boolean` - Show prev/next buttons (default: `true`)
-   `isDisabled`: `boolean` - Disable pagination

### With Boundaries

```tsx
<Pagination total={20} page={page} onChange={setPage} siblings={1} boundaries={2} />
```

---

## Common Patterns

### Settings Page with Tabs

```tsx
function SettingsPage() {
    return (
        <div className="container">
            <h1>Settings</h1>
            <Tabs defaultSelectedKey="general">
                <Tabs.ListContainer>
                    <Tabs.List>
                        <Tabs.Tab key="general">General</Tabs.Tab>
                        <Tabs.Tab key="security">Security</Tabs.Tab>
                        <Tabs.Tab key="notifications">Notifications</Tabs.Tab>
                        <Tabs.Indicator />
                    </Tabs.List>
                </Tabs.ListContainer>

                <Tabs.Panel key="general">
                    <GeneralSettings />
                </Tabs.Panel>
                <Tabs.Panel key="security">
                    <SecuritySettings />
                </Tabs.Panel>
                <Tabs.Panel key="notifications">
                    <NotificationSettings />
                </Tabs.Panel>
            </Tabs>
        </div>
    );
}
```

### FAQ with Accordion

```tsx
function FAQ() {
    return (
        <div>
            <h1>Frequently Asked Questions</h1>
            <Accordion type="single" collapsible>
                <Accordion.Item key="q1">
                    <Accordion.Trigger>
                        <Accordion.Title>How do I reset my password?</Accordion.Title>
                        <Accordion.Icon />
                    </Accordion.Trigger>
                    <Accordion.Panel>
                        <Accordion.Content>
                            Click "Forgot Password" on the login page...
                        </Accordion.Content>
                    </Accordion.Panel>
                </Accordion.Item>

                <Accordion.Item key="q2">
                    <Accordion.Trigger>
                        <Accordion.Title>What payment methods do you accept?</Accordion.Title>
                        <Accordion.Icon />
                    </Accordion.Trigger>
                    <Accordion.Panel>
                        <Accordion.Content>
                            We accept Visa, Mastercard, and PayPal...
                        </Accordion.Content>
                    </Accordion.Panel>
                </Accordion.Item>
            </Accordion>
        </div>
    );
}
```

### Paginated List

```tsx
function UsersList() {
    const [page, setPage] = useState(1);
    const pageSize = 20;

    const { data, isLoading } = useUsers({ page, pageSize });

    return (
        <div>
            <div className="grid">
                {data?.items.map((user) => (
                    <UserCard key={user.id} user={user} />
                ))}
            </div>

            <Pagination
                total={Math.ceil(data.totalItems / pageSize)}
                page={page}
                onChange={setPage}
                className="mt-4"
            />
        </div>
    );
}
```

---

## Best Practices

1. **Use Tabs for switching views** without navigation
2. **Use Accordion for FAQ sections** and collapsible content
3. **Use Breadcrumbs for hierarchical navigation**
4. **Use Pagination for large data sets** (> 50 items)
5. **Provide keyboard navigation**: Arrow keys for tabs, Enter/Space for triggers
6. **Show active state** clearly in tabs and breadcrumbs
7. **Keep tab labels short** (1-2 words ideally)
8. **Disable tabs/links** when content is unavailable
9. **Persist tab selection** in URL query params if needed
10. **Test mobile responsiveness** - tabs should scroll horizontally

---

## Accessibility Notes

-   **Tabs**: Arrow keys navigate, Enter/Space activates
-   **Accordion**: Enter/Space toggle, Home/End for first/last
-   **Links**: Focus visible, proper href, descriptive text
-   **Breadcrumbs**: Semantic `<nav>` with `aria-label="Breadcrumb"`
-   **Pagination**: Focus management, keyboard navigation
-   **ARIA labels**: Provide context for screen readers
-   **Focus indicators**: Visible on keyboard navigation

---

## When to Use This Skill

Use this skill when:

-   Creating tabbed interfaces
-   Building settings pages
-   Implementing FAQ sections
-   Adding breadcrumb navigation
-   Creating paginated lists
-   Building collapsible sections
-   Implementing multi-step forms with tabs
-   Creating hierarchical navigation
-   Building document navigation
