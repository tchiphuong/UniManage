# UniManage Frontend# UniManage Frontend

Frontend application cho hệ thống UniManage, được xây dựng với **React 18**, **TypeScript**, **Vite** và **Tailwind CSS**.Frontend application cho hệ thống UniManage.

## 🚀 Tech Stack## Tech Stack

-   **React**: 18.3+ với TypeScript- **Framework**: React 18+ / Angular 17+ (To be decided)

-   **Build Tool**: Vite 6.0+- **Language**: TypeScript

-   **Styling**: Tailwind CSS 3.4+- **UI Library**: Tailwind CSS

-   **State Management**: Zustand (với persist middleware)- **State Management**: Redux Toolkit / NgRx

-   **HTTP Client**: Axios- **API Client**: Axios / Angular HttpClient

-   **Routing**: React Router DOM 6.28+- **Form Validation**: React Hook Form / Angular Reactive Forms

-   **Linting**: ESLint với TypeScript- **i18n**: react-i18next / @ngx-translate/core

## 📁 Cấu trúc thư mục## Getting Started

````### Prerequisites

frontend/

├── public/              # Static assets-   Node.js 18+ LTS

│   └── vite.svg-   npm 9+ hoặc yarn

│

├── src/### Installation

│   ├── assets/         # Images, fonts, icons

│   │   └── react.svg```bash

│   │# Install dependencies

│   ├── components/     # Reusable componentsnpm install

│   │   ├── common/    # Buttons, Inputs, Cards, etc.

│   │   └── layout/    # Header, Footer, Sidebar, etc.# Start development server

│   │npm start

│   ├── pages/          # Page components

│   │# Build for production

│   ├── services/       # API servicesnpm run build

│   │   ├── apiClient.ts      # Axios instance with interceptors```

│   │   └── authService.ts    # Authentication APIs

│   │## Project Structure (Example - React)

│   ├── store/          # Zustand stores

│   │   └── authStore.ts      # Auth state management```

│   │frontend/

│   ├── hooks/          # Custom React hooks├── src/

│   ││   ├── components/          # Reusable components

│   ├── utils/          # Helper functions│   │   ├── common/         # Common UI components

│   │   ├── constants.ts      # API endpoints, routes│   │   ├── layout/         # Layout components

│   │   └── helpers.ts        # Date, number formatting│   │   └── forms/          # Form components

│   ││   │

│   ├── types/          # TypeScript types│   ├── pages/              # Page components

│   │   └── api.ts           # API response types│   │   ├── auth/           # Login, Register

│   ││   │   ├── dashboard/      # Dashboard

│   ├── App.tsx         # Root component│   │   ├── master/         # Master data pages

│   ├── main.tsx        # Entry point│   │   └── system/         # System settings

│   ├── index.css       # Global styles + Tailwind│   │

│   └── vite-env.d.ts   # Vite environment types│   ├── services/           # API services

││   │   ├── api.ts          # Axios config

├── index.html          # HTML template│   │   ├── auth.service.ts

├── package.json        # Dependencies│   │   ├── user.service.ts

├── vite.config.ts      # Vite configuration│   │   └── unit.service.ts

├── tailwind.config.js  # Tailwind configuration│   │

├── tsconfig.json       # TypeScript config│   ├── store/              # Redux store

├── .eslintrc.cjs       # ESLint config│   │   ├── slices/         # Redux slices

└── .env.example        # Environment variables template│   │   └── store.ts        # Store config

```│   │

│   ├── hooks/              # Custom hooks

## ⚡ Cài đặt & Chạy│   ├── utils/              # Utility functions

│   ├── types/              # TypeScript types

### 1. Install dependencies│   ├── styles/             # Global styles

│   └── App.tsx             # Root component

```bash│

npm install├── public/                 # Static files

```├── package.json

├── tsconfig.json

### 2. Tạo file `.env`└── tailwind.config.js

````

Copy `.env.example` thành `.env` và cấu hình:

## API Integration

```env

VITE_API_BASE_URL=http://localhost:5000/apiBackend API endpoint: `https://localhost:5001`

```

### Configuration

### 3. Chạy development server

Create `.env` file:

````bash

npm run dev```env

```REACT_APP_API_URL=https://localhost:5001

REACT_APP_IDENTITY_URL=https://localhost:5002

Ứng dụng sẽ chạy tại: **http://localhost:3000** ✨```



### 4. Build cho production### API Response Format



```bash```typescript

npm run build// Standard response

```interface ApiResponse<T> {

    returnCode: number;

Build output sẽ ở thư mục `dist/`    errors: string[];

    message: string;

### 5. Preview production build    data: T;

}

```bash

npm run preview// Paged response

```interface PagedResponse<T> {

    returnCode: number;

## 📜 Scripts    errors: string[];

    message: string;

| Command              | Mô tả                          |    data: {

| -------------------- | ------------------------------ |        items: T[];

| `npm run dev`        | Chạy dev server với HMR        |        paging: {

| `npm run build`      | Build TypeScript + Vite        |            pageIndex: number;

| `npm run lint`       | Chạy ESLint                    |            pageSize: number;

| `npm run preview`    | Preview production build       |            totalItems: number;

            totalPages: number;

## 🎯 Tính năng        };

    };

### API Client (Axios)}

````

-   ✅ Axios instance với interceptors

-   ✅ Tự động thêm Bearer token vào headers## Authentication

-   ✅ Xử lý 401 Unauthorized tự động (redirect login)

-   ✅ Typed responses với TypeScriptUsing OpenID Connect / OAuth 2.0 with IdentityServer:

### State Management (Zustand)```typescript

// Login flow

-   ✅ Global state với Zustandconst login = async (username: string, password: string) => {

-   ✅ Persist middleware cho auth state (localStorage) const response = await authService.login(username, password);

-   ✅ Type-safe stores // Store token

    localStorage.setItem("access_token", response.data.accessToken);

### Styling (Tailwind CSS)};

````

-   ✅ JIT mode

-   ✅ Custom animations (spin-slow)## Localization

-   ✅ Dark mode ready

-   ✅ Responsive design utilitiesFrontend sử dụng resources từ backend API:



### API Response Format```typescript

// Get resources

Tất cả API responses tuân theo chuẩn backend:const resources = await apiClient.get("/api/resources", {

    params: { languageCode: "vi-VN" },

**Response thường:**});



```typescript// Use in component

{<label>{resources["User_lbl_Username"]}</label>;

  returnCode: 0,```

  errors: [],

  message: "Thao tác thành công",## Development

  data: { ... }

}```bash

```# Run development server

npm start              # Port 3000 (React) / 4200 (Angular)

**Response paging:**

# Run tests

```typescriptnpm test

{

  returnCode: 0,# Lint code

  errors: [],npm run lint

  message: "Lấy danh sách thành công",

  data: {# Format code

    items: [...],npm run format

    paging: {```

      pageIndex: 1,

      pageSize: 20,## Build & Deploy

      totalItems: 100,

      totalPages: 5```bash

    }# Production build

  }npm run build

}

```# Output: build/ (React) hoặc dist/ (Angular)

````

## 💡 Development Guidelines

## TODO

### Component Pattern

-   [ ] Chọn framework (React hoặc Angular)

```tsx- [ ] Setup project boilerplate

import { FC } from 'react';-   [ ] Configure TypeScript & ESLint

-   [ ] Setup Tailwind CSS

interface Props {-   [ ] Configure API client

	title: string;-   [ ] Implement authentication flow

	onClick?: () => void;-   [ ] Create common components

}-   [ ] Setup routing

-   [ ] Implement state management

export const MyComponent: FC<Props> = ({ title, onClick }) => {-   [ ] Add i18n support

	return (

		<button ## Contributing

			onClick={onClick}

			className="px-4 py-2 bg-blue-600 hover:bg-blue-700 rounded"1. Create feature branch

		>2. Make changes

			{title}3. Test thoroughly

		</button>4. Create pull request

	);

};## Contact

```

Frontend Team - UniManage Project

### API Service Pattern

```typescript
import { apiClient } from "./apiClient";
import type { User } from "@/types/api";

export const userService = {
    async getList(params: { pageIndex: number; pageSize: number }) {
        return apiClient.getPaged<User>("/users", { params });
    },

    async getById(id: number) {
        return apiClient.get<User>(`/users/${id}`);
    },

    async create(data: CreateUserDto) {
        return apiClient.post<{ id: number }>("/users", data);
    },
};
```

### Zustand Store Pattern

```typescript
import { create } from "zustand";

interface CounterStore {
    count: number;
    increment: () => void;
    decrement: () => void;
}

export const useCounterStore = create<CounterStore>((set) => ({
    count: 0,
    increment: () => set((state) => ({ count: state.count + 1 })),
    decrement: () => set((state) => ({ count: state.count - 1 })),
}));
```

### Custom Hook Pattern

```typescript
import { useState, useEffect } from "react";
import { userService } from "@/services/userService";

export const useUsers = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUsers = async () => {
            const response = await userService.getList({ pageIndex: 1, pageSize: 20 });
            if (response.returnCode === 0) {
                setUsers(response.data.items);
            }
            setLoading(false);
        };
        fetchUsers();
    }, []);

    return { users, loading };
};
```

## ⚙️ Configuration

### Vite (vite.config.ts)

-   **Port**: 3000
-   **Proxy**: `/api` → `http://localhost:5000`
-   **Path alias**: `@/` → `./src/`

### TypeScript (tsconfig.json)

-   **Strict mode**: enabled
-   **Path mapping**: `@/*` → `src/*`
-   **No unused locals/parameters**: enabled
-   **Module resolution**: bundler (ESNext)

### Tailwind (tailwind.config.js)

-   **Content**: `./index.html`, `./src/**/*.{js,ts,jsx,tsx}`
-   **Custom animations**: `spin-slow` (20s)
-   **Plugins**: ready for custom plugins

## 🔗 Tích hợp Backend

-   Backend API: `http://localhost:5000/api`
-   Vite proxy forward `/api` → backend
-   Auth: Bearer token trong header `Authorization`
-   CORS: Backend cần enable CORS cho `http://localhost:3000`

## 🌐 Browser Support

-   ✅ Chrome (latest)
-   ✅ Firefox (latest)
-   ✅ Edge (latest)
-   ✅ Safari (latest)

## 📦 Dependencies

### Production

-   `react`, `react-dom`: ^18.3.1
-   `react-router-dom`: ^6.28.0
-   `axios`: ^1.7.9
-   `zustand`: ^5.0.2

### Development

-   `vite`: ^6.0.1
-   `@vitejs/plugin-react`: ^4.3.4
-   `typescript`: ^5.7.2
-   `tailwindcss`: ^3.4.15
-   `postcss`, `autoprefixer`: latest
-   `eslint`, `@typescript-eslint/*`: latest

## 🎨 UI Components (Coming Soon)

-   Button, Input, Select, Checkbox, Radio
-   Card, Modal, Drawer, Tabs
-   Table với pagination
-   Form với validation
-   Toast notifications
-   Loading spinners

## 📝 License

Private - UniManage Project
