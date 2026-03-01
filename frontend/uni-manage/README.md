# UniManage Frontend

This is the frontend application for the **UniManage** system, built with **Next.js 15 (App Router)** and **HeroUI v3**.

**HeroUI v3** is a React component library built on **Tailwind CSS v4** and **React Aria Components**. Every component comes with smooth animations, polished details, and built-in accessibility—ready to use, fully customizable.

### Why HeroUI v3?

- ✨ **Beautiful by default** — Professional look out of the box, no extra styling needed
- ♿ **Accessible** — Built on React Aria Components with WCAG compliance
- 🎨 **Flexible** — Every component is customizable with Tailwind utilities
- 🔧 **Developer-friendly** — Fully typed APIs with excellent autocompletion
- 🚀 **Modern Stack** — Built for React 19 and Tailwind v4
- 📦 **Lightweight** — Tree-shaken, only what you use goes into your app

## 🚀 Tech Stack

-   **Framework**: [Next.js 15](https://nextjs.org/) (App Router)
-   **Language**: [TypeScript](https://www.typescriptlang.org/)
-   **UI Library**: [HeroUI v3 Beta](https://v3.heroui.com/docs/react/getting-started) (formerly NextUI)
-   **Styling**: [Tailwind CSS v4](https://tailwindcss.com/)
-   **UI Foundation**: [React Aria Components](https://react-spectrum.adobe.com/react-aria/components.html) (for accessibility)
-   **React**: [React 19](https://react.dev/blog/2024/12/05/react-19)
-   **Icons**: [Iconify](https://iconify.design/)
-   **Data Fetching**: [TanStack Query v5](https://tanstack.com/query/latest)
-   **Internationalization**: `next-intl`
-   **Validation**: `zod` + `react-hook-form`

## 📂 Project Structure

```
src/
├── app/                    # Next.js App Router pages & layouts
│   ├── (dashboard)/        # Dashboard layout routes (System, HR, etc.)
│   ├── auth/               # Authentication routes (Login)
│   ├── providers.tsx       # Global providers (Theme, Query, UI)
│   └── layout.tsx          # Root layout
│
├── components/             # Reusable UI components
│   ├── common/             # Generic atoms (DataTable, Input, etc.)
│   ├── layout/             # Sidebar, Header, etc.
│   └── ...
│
├── hooks/                  # Custom React hooks (useAuth, etc.)
├── lib/                    # Utilities, API, Constants
│   ├── api-endpoints.ts    # Centralized API URLs
│   └── http-client.ts      # Axios wrapper
│
├── services/               # API service layers (UserService, AuthService)
└── types/                  # TypeScript interfaces & types
```

## 🛠️ Getting Started

### Prerequisites

-   **Node.js**: 20+ (LTS recommended)
-   **React**: 19+ (included in dependencies)
-   **Tailwind CSS**: v4 (included in dependencies)
-   **Backend API**: Running at `http://localhost:5297` (or configured URL)

### Installation

1.  **Install dependencies** (including HeroUI):
    ```bash
    npm install
    # or
    yarn install
    ```

    This will install HeroUI v3 and all required dependencies as specified in `package.json`.

2.  **Verify HeroUI Setup**:
    Ensure your `app/globals.css` includes the following imports (already configured):
    ```css
    @import "tailwindcss";
    @import "@heroui/styles";
    ```

3.  **Environment Setup**:
    Create a `.env.local` file in the root of `frontend/uni-manage`:

    ```env
    # Base URL for the Backend API
    NEXT_PUBLIC_API_URL=http://localhost:5297/api/v1
    ```

4.  **Run Development Server**:
    ```bash
    npm run dev
    ```

    Open [http://localhost:3000](http://localhost:3000) with your browser.

## 🎨 UI Guidelines

-   **Components**: Always ensure consistency by using components from `@heroui/react`.
-   **Icons**: Use Iconify icons (e.g., `<Icon icon="solar:user-bold" />`).
-   **Responsive**: Use Tailwind's utility classes (`md:`, `lg:`) to ensure responsiveness.
-   **Theme**: The app supports Dark/Light mode via `next-themes` (built into HeroUI).

## 📖 HeroUI v3 Usage

### Basic Component Example

```tsx
import { Button, Card, CardBody } from '@heroui/react';

function MyComponent() {
  return (
    <Card>
      <CardBody>
        <h1>Hello HeroUI</h1>
        <Button color="primary" onPress={() => console.log('Pressed')}>
          Click Me
        </Button>
      </CardBody>
    </Card>
  );
}
```

### Styling Components

HeroUI v3 components can be customized in multiple ways:

#### 1. Using Tailwind Utilities

```tsx
<Button className="bg-gradient-to-r from-blue-500 to-purple-500">
  Gradient Button
</Button>
```

#### 2. Using Component Props

```tsx
<Button 
  color="primary" 
  variant="shadow" 
  size="lg"
  radius="full"
>
  Custom Button
</Button>
```

#### 3. Using Slots for Complex Components

```tsx
<Table 
  classNames={{
    wrapper: "min-h-[400px]",
    th: "bg-primary text-white",
    td: "text-sm"
  }}
>
  {/* Table content */}
</Table>
```

### Common Components

-   **Layout**: `Card`, `Divider`, `Spacer`
-   **Forms**: `Input`, `Textarea`, `Select`, `Checkbox`, `Radio`
-   **Navigation**: `Button`, `Link`, `Tabs`, `Breadcrumbs`
-   **Data Display**: `Table`, `Avatar`, `Badge`, `Chip`
-   **Feedback**: `Modal`, `Popover`, `Tooltip`, `Spinner`
-   **Overlay**: `Dropdown`, `Menu`

### Accessibility Features

All HeroUI components include:

-   ✅ **Keyboard Navigation**: Full keyboard support
-   ✅ **Screen Readers**: ARIA labels and descriptions
-   ✅ **Focus Management**: Proper focus handling
-   ✅ **WCAG Compliance**: Following accessibility standards

### API Conventions

HeroUI uses consistent naming from React Aria:

-   `isDisabled` instead of `disabled`
-   `onPress` instead of `onClick` (for better touch support)
-   `isOpen` for controlled open state
-   `defaultOpen` for uncontrolled open state

## 🌍 Internationalization

We use `next-intl` for translations.
-   Run the backend to serve dynamic resources/languages if applicable, or check `messages/` folder (if using file-based).
-   Current supported locales: `vi` (Vietnamese - Default), `en` (English).

## 📦 Scripts

-   `npm run dev`: Start dev server at `http://localhost:3000`
-   `npm run build`: Build for production (optimized bundle)
-   `npm run start`: Start production server
-   `npm run lint`: Run ESLint to check code quality

## 🔗 Documentation & Resources

### Official Documentation

-   **HeroUI v3 Docs**: [https://v3.heroui.com/docs/react/getting-started](https://v3.heroui.com/docs/react/getting-started)
-   **Quick Start**: [https://v3.heroui.com/docs/react/getting-started/quick-start](https://v3.heroui.com/docs/react/getting-started/quick-start)
-   **Components**: [https://v3.heroui.com/docs/react/components](https://v3.heroui.com/docs/react/components)
-   **Styling Guide**: [https://v3.heroui.com/docs/react/getting-started/styling](https://v3.heroui.com/docs/react/getting-started/styling)
-   **Theming Guide**: [https://v3.heroui.com/docs/react/getting-started/theming](https://v3.heroui.com/docs/react/getting-started/theming)
-   **Animation**: [https://v3.heroui.com/docs/react/getting-started/animation](https://v3.heroui.com/docs/react/getting-started/animation)
-   **Figma Kit**: [HeroUI Figma Kit V3](https://www.figma.com/community/file/1546526812159103429)

### Other Resources

-   **Next.js 15 Docs**: [https://nextjs.org/docs](https://nextjs.org/docs)
-   **Tailwind CSS v4**: [https://tailwindcss.com/docs](https://tailwindcss.com/docs)
-   **React Aria**: [https://react-spectrum.adobe.com/react-aria/](https://react-spectrum.adobe.com/react-aria/)
-   **TanStack Query**: [https://tanstack.com/query/latest](https://tanstack.com/query/latest)
-   **Iconify**: [https://iconify.design/](https://iconify.design/)

### Community

-   **HeroUI GitHub**: [https://github.com/heroui-inc/heroui](https://github.com/heroui-inc/heroui)
-   **HeroUI Discord**: [https://discord.gg/9b6yyZKmH4](https://discord.gg/9b6yyZKmH4)
-   **HeroUI Discussions**: [https://github.com/heroui-inc/heroui/discussions](https://github.com/heroui-inc/heroui/discussions)

## 🐛 Troubleshooting

### Common Issues

#### 1. Styles Not Loading

Ensure `app/globals.css` has the correct import order:

```css
@import "tailwindcss";  /* Must be first */
@import "@heroui/styles";
```

#### 2. Component Not Found

Make sure you're importing from the correct package:

```tsx
// ✅ Correct
import { Button, Card } from '@heroui/react';

// ❌ Wrong
import { Button } from '@nextui-org/react';
```

#### 3. Dark Mode Not Working

Check that `next-themes` provider is configured in `app/providers.tsx` and wrapped around your app.

#### 4. TypeScript Errors

If you see type errors, try:

```bash
npm install --save-dev @types/react@19 @types/react-dom@19
```

#### 5. Build Errors with Tailwind v4

Ensure your `postcss.config.mjs` includes:

```js
export default {
  plugins: {
    '@tailwindcss/postcss': {},
  },
};
```

### Getting Help

-   Check [HeroUI GitHub Issues](https://github.com/heroui-inc/heroui/issues)
-   Ask in [HeroUI Discord](https://discord.gg/9b6yyZKmH4)
-   Review [Next.js Troubleshooting](https://nextjs.org/docs/messages)

## 📝 Notes

-   **HeroUI v3 is in beta**: Expect some breaking changes before stable release
-   **Import order matters**: Always import `tailwindcss` before `@heroui/styles`
-   **Tree-shaking**: Only imported components are included in the bundle
-   **CSS Variables**: HeroUI uses CSS variables for theming (can be customized)
-   **Server Components**: Most HeroUI components are Client Components (use `'use client'`)

## 📄 License

This project is part of the UniManage system. HeroUI v3 is released under the [MIT License](https://github.com/heroui-inc/heroui/blob/main/LICENSE).
