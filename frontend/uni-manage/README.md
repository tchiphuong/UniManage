# UniManage Frontend

This is the frontend application for the **UniManage** system, built with **Next.js 15 (App Router)** and **HeroUI v3**.

## 🚀 Tech Stack

-   **Framework**: [Next.js 15](https://nextjs.org/) (App Router)
-   **Language**: [TypeScript](https://www.typescriptlang.org/)
-   **UI Library**: [HeroUI v3](https://www.heroui.com/) (formerly NextUI)
-   **Styling**: [Tailwind CSS](https://tailwindcss.com/)
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

-   Node.js 20+
-   Backend API running (usually at `http://localhost:5297`)

### Installation

1.  **Install dependencies**:
    ```bash
    npm install
    # or
    yarn install
    ```

2.  **Environment Setup**:
    Create a `.env.local` file in the root of `frontend/uni-manage`:

    ```env
    # Base URL for the Backend API
    NEXT_PUBLIC_API_URL=http://localhost:5297/api/v1
    ```

3.  **Run Development Server**:
    ```bash
    npm run dev
    ```

    Open [http://localhost:3000](http://localhost:3000) with your browser.

## 🎨 UI Guidelines

-   **Components**: Always ensure consistency by using components from `@heroui/react`.
-   **Icons**: Use Iconify icons (e.g., `<Icon icon="solar:user-bold" />`).
-   **Responsive**: Use Tailwind's utility classes (`md:`, `lg:`) to ensure responsiveness.
-   **Theme**: The app supports Dark/Light mode via `next-themes` (built into HeroUI).

## 🌍 Internationalization

We use `next-intl` for translations.
-   Run the backend to serve dynamic resources/languages if applicable, or check `messages/` folder (if using file-based).
-   Current supported locales: `vi` (Vietnamese - Default), `en` (English).

## 📦 Scripts

-   `npm run dev`: Start dev server.
-   `npm run build`: Build for production.
-   `npm run start`: Start production server.
-   `npm run lint`: Run ESLint.
