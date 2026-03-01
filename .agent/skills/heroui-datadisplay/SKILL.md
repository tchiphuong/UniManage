---
name: heroui-datadisplay
description: Hero UI Data Display components (Chip, Badge, Avatar, Table, Code, Kbd). Use for showing user info, status, data tables in UniManage with Hero UI v3. Full docs at https://v3.heroui.com/docs/react/components
---

# Hero UI Data Display Components

Complete guide for Hero UI Data Display components. For detailed examples, visit the official documentation links below.

## Chip Component

**Docs**: https://v3.heroui.com/docs/components/chip  
**Import**: `import { Chip } from '@heroui/react';`

Small inline element for tags, labels, status indicators.

### Usage

```tsx
import { Chip } from '@heroui/react';

<Chip>Default</Chip>
<Chip variant="primary">Primary</Chip>
<Chip variant="success">Success</Chip>
```

### Key Props

-   `variant`: `'default' | 'primary' | 'secondary' | 'success' | 'warning' | 'danger'`
-   `size`: `'sm' | 'md' | 'lg'` (default: `'md'`)
-   `onClose`: `() => void` - Close button handler
-   `isClosable`: `boolean` - Show close button
-   `startContent`: `ReactNode` - Leading icon/content
-   `endContent`: `ReactNode` - Trailing icon/content

### Variants

```tsx
<Chip variant="default">Default</Chip>
<Chip variant="primary">Primary</Chip>
<Chip variant="secondary">Secondary</Chip>
<Chip variant="success">Success</Chip>
<Chip variant="warning">Warning</Chip>
<Chip variant="danger">Danger</Chip>
```

### With Close Button

```tsx
<Chip onClose={() => console.log("Chip closed")}>Closable</Chip>
```

### With Icon

```tsx
import { CheckIcon } from "lucide-react";

<Chip startContent={<CheckIcon size={16} />} variant="success">
    Verified
</Chip>;
```

### Sizes

```tsx
<Chip size="sm">Small</Chip>
<Chip size="md">Medium</Chip>
<Chip size="lg">Large</Chip>
```

**Full docs**: https://v3.heroui.com/docs/components/chip

---

## Badge Component

**Docs**: https://v3.heroui.com/docs/components/badge  
**Import**: `import { Badge } from '@heroui/react';`

Notification indicator overlaying another element.

### Usage

```tsx
import { Badge, Button } from "@heroui/react";

<Badge content="5">
    <Button>Messages</Button>
</Badge>;
```

### Key Props

-   `content`: `ReactNode` - Badge content (number, icon, text)
-   `variant`: `'default' | 'primary' | 'success' | 'warning' | 'danger'`
-   `size`: `'sm' | 'md' | 'lg'`
-   `placement`: `'top-right' | 'top-left' | 'bottom-right' | 'bottom-left'`
-   `isInvisible`: `boolean` - Hide badge
-   `showOutline`: `boolean` - White outline around badge

### Notification Badge

```tsx
<Badge content={12} variant="danger">
    <Button>Notifications</Button>
</Badge>
```

### Dot Badge (Status Indicator)

```tsx
<Badge content="" variant="success">
    <Avatar src="/user.jpg" />
</Badge>
```

### Placement

```tsx
<Badge content="NEW" placement="top-left">
    <Card>...</Card>
</Badge>
```

**Full docs**: https://v3.heroui.com/docs/components/badge

---

## Avatar Component

**Docs**: https://v3.heroui.com/docs/components/avatar  
**Import**: `import { Avatar, AvatarGroup } from '@heroui/react';`

Display user profile pictures or initials.

### Usage

```tsx
import { Avatar } from '@heroui/react';

<Avatar src="/user.jpg" alt="User name" />
<Avatar name="John Doe" />
```

### Key Props

-   `src`: `string` - Image URL
-   `name`: `string` - User name (for fallback initials)
-   `alt`: `string` - Alt text
-   `size`: `'sm' | 'md' | 'lg' | 'xl'` (default: `'md'`)
-   `variant`: `'circular' | 'square'` (default: `'circular'`)
-   `color`: `'primary' | 'secondary' | 'success' | 'warning' | 'danger'`
-   `isBordered`: `boolean` - Show border

### With Fallback Initials

```tsx
<Avatar name="John Doe" />;
{
    /* Shows "JD" */
}
```

### Sizes

```tsx
<Avatar src="/user.jpg" size="sm" />
<Avatar src="/user.jpg" size="md" />
<Avatar src="/user.jpg" size="lg" />
<Avatar src="/user.jpg" size="xl" />
```

### Variants

```tsx
<Avatar src="/user.jpg" variant="circular" />
<Avatar src="/user.jpg" variant="square" />
```

### With Badge (Status)

```tsx
<Badge content="" variant="success" placement="bottom-right">
    <Avatar src="/user.jpg" />
</Badge>
```

### Avatar Group

```tsx
import { AvatarGroup, Avatar } from "@heroui/react";

<AvatarGroup max={3}>
    <Avatar src="/user1.jpg" />
    <Avatar src="/user2.jpg" />
    <Avatar src="/user3.jpg" />
    <Avatar src="/user4.jpg" />
    <Avatar src="/user5.jpg" />
</AvatarGroup>;
{
    /* Shows first 3 avatars + "+2" */
}
```

**Full docs**: https://v3.heroui.com/docs/components/avatar

---

## Table Component

**Docs**: https://v3.heroui.com/docs/components/table  
**Import**: `import { Table } from '@heroui/react';`

Display tabular data with sorting, selection, pagination.

### Usage

```tsx
import { Table } from "@heroui/react";

<Table>
    <Table.Header>
        <Table.Column>Name</Table.Column>
        <Table.Column>Email</Table.Column>
        <Table.Column>Status</Table.Column>
    </Table.Header>
    <Table.Body>
        <Table.Row key="1">
            <Table.Cell>John Doe</Table.Cell>
            <Table.Cell>john@example.com</Table.Cell>
            <Table.Cell>
                <Chip variant="success">Active</Chip>
            </Table.Cell>
        </Table.Row>
        <Table.Row key="2">
            <Table.Cell>Jane Smith</Table.Cell>
            <Table.Cell>jane@example.com</Table.Cell>
            <Table.Cell>
                <Chip variant="warning">Pending</Chip>
            </Table.Cell>
        </Table.Row>
    </Table.Body>
</Table>;
```

### With Selection

```tsx
function SelectableTable() {
    const [selected, setSelected] = useState(new Set());

    return (
        <Table selectionMode="multiple" selectedKeys={selected} onSelectionChange={setSelected}>
            <Table.Header>
                <Table.Column>Name</Table.Column>
                <Table.Column>Email</Table.Column>
            </Table.Header>
            <Table.Body>
                <Table.Row key="1">
                    <Table.Cell>John Doe</Table.Cell>
                    <Table.Cell>john@example.com</Table.Cell>
                </Table.Row>
            </Table.Body>
        </Table>
    );
}
```

### With Sorting

```tsx
<Table>
    <Table.Header>
        <Table.Column allowsSorting>Name</Table.Column>
        <Table.Column allowsSorting>Email</Table.Column>
        <Table.Column>Actions</Table.Column>
    </Table.Header>
    <Table.Body>{/* rows */}</Table.Body>
</Table>
```

**Full docs**: https://v3.heroui.com/docs/components/table

---

## Code Component

**Docs**: https://v3.heroui.com/docs/components/code  
**Import**: `import { Code } from '@heroui/react';`

Display inline or block code snippets.

### Usage

```tsx
import { Code } from '@heroui/react';

<p>Use <Code>npm install</Code> to install</p>

<Code block language="typescript">
{`function greet(name: string) {
  console.log(\`Hello, \${name}!\`);
}`}
</Code>
```

### Key Props

-   `block`: `boolean` - Block vs inline code
-   `language`: `string` - Syntax highlighting language
-   `showLineNumbers`: `boolean` - Show line numbers (block code)
-   `size`: `'sm' | 'md' | 'lg'`

### Inline Code

```tsx
<p>
    Install with <Code>npm install @heroui/react</Code>
</p>
```

### Block Code

```tsx
<Code block language="bash">
    npm install @heroui/react
</Code>
```

**Full docs**: https://v3.heroui.com/docs/components/code

---

## Kbd Component

**Docs**: https://v3.heroui.com/docs/components/kbd  
**Import**: `import { Kbd } from '@heroui/react';`

Display keyboard shortcuts.

### Usage

```tsx
import { Kbd } from '@heroui/react';

<p>Press <Kbd>Ctrl</Kbd> + <Kbd>S</Kbd> to save</p>
<p>Press <Kbd>⌘</Kbd> + <Kbd>K</Kbd> to search</p>
```

### Common Keys

```tsx
<Kbd>Ctrl</Kbd>
<Kbd>Alt</Kbd>
<Kbd>Shift</Kbd>
<Kbd>Enter</Kbd>
<Kbd>Esc</Kbd>
<Kbd>⌘</Kbd>  {/* Command/Cmd */}
<Kbd>⌥</Kbd>  {/* Option */}
```

**Full docs**: https://v3.heroui.com/docs/components/kbd

---

## Common Patterns

### User Status Display

```tsx
function UserCard({ user }) {
    return (
        <Card>
            <Card.Body className="flex items-center gap-4">
                <Badge
                    content=""
                    variant={user.isOnline ? "success" : "default"}
                    placement="bottom-right"
                >
                    <Avatar src={user.avatar} name={user.name} />
                </Badge>
                <div>
                    <h3>{user.name}</h3>
                    <p className="text-sm text-gray-500">{user.email}</p>
                    <Chip size="sm" variant={user.status === "active" ? "success" : "warning"}>
                        {user.status}
                    </Chip>
                </div>
            </Card.Body>
        </Card>
    );
}
```

### Tag Filter

```tsx
function TagFilter() {
    const [selected, setSelected] = useState(["tag1", "tag2"]);

    const removeTag = (tag) => {
        setSelected(selected.filter((t) => t !== tag));
    };

    return (
        <div className="flex flex-wrap gap-2">
            {selected.map((tag) => (
                <Chip key={tag} onClose={() => removeTag(tag)}>
                    {tag}
                </Chip>
            ))}
        </div>
    );
}
```

### Data Table with Actions

```tsx
function UsersTable({ users }) {
    return (
        <Table>
            <Table.Header>
                <Table.Column>User</Table.Column>
                <Table.Column>Role</Table.Column>
                <Table.Column>Status</Table.Column>
                <Table.Column>Actions</Table.Column>
            </Table.Header>
            <Table.Body>
                {users.map((user) => (
                    <Table.Row key={user.id}>
                        <Table.Cell>
                            <div className="flex items-center gap-2">
                                <Avatar src={user.avatar} name={user.name} size="sm" />
                                <div>
                                    <p className="font-medium">{user.name}</p>
                                    <p className="text-sm text-gray-500">{user.email}</p>
                                </div>
                            </div>
                        </Table.Cell>
                        <Table.Cell>
                            <Chip size="sm">{user.role}</Chip>
                        </Table.Cell>
                        <Table.Cell>
                            <Chip
                                size="sm"
                                variant={user.status === "active" ? "success" : "default"}
                            >
                                {user.status}
                            </Chip>
                        </Table.Cell>
                        <Table.Cell>
                            <Button size="sm" variant="ghost">
                                Edit
                            </Button>
                        </Table.Cell>
                    </Table.Row>
                ))}
            </Table.Body>
        </Table>
    );
}
```

### Notification Badge

```tsx
function NotificationBell() {
    const [count, setCount] = useState(5);

    return (
        <Badge content={count} variant="danger">
            <Button isIconOnly variant="ghost">
                <BellIcon />
            </Button>
        </Badge>
    );
}
```

### Keyboard Shortcut Help

```tsx
function ShortcutHelp() {
    return (
        <div className="space-y-2">
            <div className="flex justify-between">
                <span>Search</span>
                <div className="flex gap-1">
                    <Kbd>⌘</Kbd>
                    <Kbd>K</Kbd>
                </div>
            </div>
            <div className="flex justify-between">
                <span>Save</span>
                <div className="flex gap-1">
                    <Kbd>Ctrl</Kbd>
                    <Kbd>S</Kbd>
                </div>
            </div>
            <div className="flex justify-between">
                <span>Close</span>
                <Kbd>Esc</Kbd>
            </div>
        </div>
    );
}
```

---

## Best Practices

1. **Use Chip for status indicators** (active, pending, completed)
2. **Use Badge for notification counts** (messages, updates)
3. **Use Avatar for user representation** (profile pics, initials)
4. **Use Table for structured data** (user lists, reports)
5. **Use Code for technical content** (commands, code snippets)
6. **Use Kbd for keyboard shortcuts** (help docs, tooltips)
7. **Show avatar fallback** with initials when image fails
8. **Keep chip labels short** (1-2 words)
9. **Use appropriate badge placement** (top-right for notifications)
10. **Make tables responsive** (scroll on mobile)

---

## Accessibility Notes

-   **Chip**: `role="status"` for status chips
-   **Badge**: Announce count changes to screen readers
-   **Avatar**: Always provide `alt` text or `name` for fallback
-   **Table**: Proper `<thead>`, `<tbody>`, `<th>`, `<td>` structure
-   **Code**: `<code>` element with language attribute
-   **Kbd**: Semantic `<kbd>` element
-   **Focus indicators**: Visible for keyboard navigation

---

## When to Use This Skill

Use this skill when:

-   Displaying user profiles
-   Showing status indicators
-   Creating tag/label systems
-   Building data tables
-   Showing notification counts
-   Displaying code examples
-   Documenting keyboard shortcuts
-   Creating user lists
-   Building admin panels
-   Showing technical documentation
