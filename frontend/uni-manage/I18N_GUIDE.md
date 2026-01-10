# i18n (Internationalization) Setup Guide

## Đã cài đặt

✅ **next-intl** - Modern i18n solution cho Next.js App Router
✅ **Middleware** - Auto-detect locale và prefix URLs
✅ **Translations** - Vietnamese (vi) & English (en)
✅ **Language Switcher** - Component chuyển đổi ngôn ngữ

## Cấu trúc

```
app/
  [locale]/          # Dynamic locale route
    layout.tsx       # Layout với NextIntlClientProvider
    page.tsx         # Home page với translations
  page.tsx           # Root redirect to default locale

i18n/
  request.ts         # i18n config & locale list
  navigation.ts      # Link, redirect, useRouter với i18n

messages/
  vi.json            # Tiếng Việt translations
  en.json            # English translations

middleware.ts        # Locale detection & routing
i18n.ts             # Next-intl plugin config
```

## URL Structure

-   `/` → redirect to `/vi` (default locale)
-   `/vi` → Vietnamese version
-   `/en` → English version
-   `/vi/dashboard` → Vietnamese dashboard
-   `/en/dashboard` → English dashboard

## Sử dụng

### 1. Trong Server Components:

```tsx
import { useTranslations } from "next-intl";

export default function ServerPage() {
    const t = useTranslations("home");

    return <h1>{t("title")}</h1>;
}
```

### 2. Trong Client Components:

```tsx
"use client";

import { useTranslations } from "next-intl";

export default function ClientPage() {
    const t = useTranslations("auth");

    return <button>{t("login")}</button>;
}
```

### 3. Navigation với i18n:

```tsx
import { Link, useRouter } from "@/i18n/navigation";

// Link component tự động add locale
<Link href="/dashboard">Dashboard</Link>;

// Router với locale support
const router = useRouter();
router.push("/users");
```

### 4. Language Switcher:

```tsx
import { LanguageSwitcher } from "@/components";

<LanguageSwitcher />;
```

## Thêm ngôn ngữ mới

### 1. Thêm locale vào config:

```typescript
// i18n/request.ts
export const locales = ["vi", "en", "ja"] as const; // Thêm 'ja'
```

### 2. Tạo translation file:

```bash
# Tạo messages/ja.json
```

### 3. Update middleware:

```typescript
// middleware.ts - tự động detect từ locales array
```

## Translation Keys

Xem chi tiết trong:

-   [messages/vi.json](./messages/vi.json)
-   [messages/en.json](./messages/en.json)

### Categories:

-   `common` - Buttons, actions chung
-   `auth` - Authentication
-   `nav` - Navigation menu
-   `home` - Home page
-   `validation` - Form validation
-   `error` - Error messages
-   `success` - Success messages
-   `pagination` - Pagination labels

## Best Practices

1. **Organize translations by domain:**

    ```json
    {
        "users": {
            "list": "Danh sách người dùng",
            "create": "Tạo người dùng"
        }
    }
    ```

2. **Use nested keys:**

    ```tsx
    t("users.list"); // "Danh sách người dùng"
    t("users.create"); // "Tạo người dùng"
    ```

3. **Parameterized translations:**

    ```json
    {
        "welcome": "Xin chào, {name}!"
    }
    ```

    ```tsx
    t("welcome", { name: "John" }); // "Xin chào, John!"
    ```

4. **Pluralization:**
    ```json
    {
        "items": "{count, plural, =0 {No items} =1 {One item} other {# items}}"
    }
    ```

## Testing

```bash
npm run dev

# Test URLs:
# http://localhost:3000      → redirects to /vi
# http://localhost:3000/vi   → Vietnamese
# http://localhost:3000/en   → English
```

## Notes

-   Locale auto-detected từ `Accept-Language` header
-   URL luôn có locale prefix (`/vi/...`, `/en/...`)
-   Switching language giữ nguyên current path
-   Middleware cache locale preferences
