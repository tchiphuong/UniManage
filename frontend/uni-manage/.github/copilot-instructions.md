# Copilot / AI Agent Instructions (repository-specific)

This document gives concise, actionable guidance for AI coding agents working on this Next.js + TypeScript monorepo.

1. Big picture

-   **Framework & router**: This project is a Next.js App Router application (see the `app/` folder). Routes are organized under `app/[locale]/` to support localized routing.
-   **i18n**: Localization uses `next-intl` with messages stored in `messages/*.json` and runtime config in `i18n.ts`. Locale detection and routing are enforced in `middleware.ts`.
-   **Frontend layers**: UI primitives live under `components/common/`, composed components in `components/` and page layouts in `app/` (e.g. `app/[locale]/dashboard`).
-   **Client/server split**: Prefer server components by default in `app/`; mark components with `"use client"` when they require browser-only APIs or state/hooks.

2. Key integration points & files (edit carefully)

-   `package.json`: scripts: `npm run dev`, `npm run build`, `npm start`, `npm run lint`.
-   `middleware.ts`: controls locale prefixes and route matching; update when adding new top-level routes or changing locale behavior.
-   `i18n.ts` and `i18n/request`: configure locale, messages, and timezone.
-   `messages/en.json` and `messages/vi.json`: add translation keys here; pages load messages via `next-intl` imports.
-   `lib/api-client.ts` and `lib/cookies.ts`: central API/cookie helpers — use these for network/auth changes.
-   `hooks/use-auth.ts`: primary client auth hook; align new auth flows with it.
-   `types/api.ts` and `types/auth.ts`: canonical types for API payloads and auth.

3. Project-specific conventions

-   **Locale routing**: All public routes are prefixed by locale (e.g. `/vi/login`, `/en/dashboard`). Use the `app/[locale]/` structure for new localized pages.
-   **Translations**: Add keys to both `messages/*.json`. Use existing key naming patterns (dot-separated namespaces where present).
-   **API usage**: Use `lib/api-client.ts` for requests; do not create ad-hoc axios instances across components.
-   **Styling & UI**: Project uses Tailwind + `@heroui/*` components. Reuse `components/common/*` primitives where available.
-   **TypeScript-first**: Types live in `types/`. When adding endpoints, update `types/api.ts`.

4. Development & debugging tips

-   Start the dev server: `npm run dev` (root of repo).
-   Linting: `npm run lint`.
-   The app uses Next 15 + React 19 — be mindful of App Router conventions (server components, streaming, etc.).

5. How to update translations, routes, and middleware safely

-   Adding a new locale-aware page: create `app/[locale]/your-route/page.tsx`; add translation keys into `messages/*.json`; verify middleware matcher if you add special top-level paths.
-   Changing locale detection: edit `middleware.ts` and `i18n.ts` together so runtime config and middleware match.

6. Request handling & auth

-   Centralize network/auth logic in `lib/api-client.ts` and `lib/cookies.ts`. Client hooks like `hooks/use-auth.ts` rely on those utilities — update them in tandem.

7. Examples from repository

-   Localized login page: `app/[locale]/login/page.tsx`
-   Dashboard layout: `components/dashboard-layout.tsx` and `app/[locale]/dashboard/page.tsx`
-   Translation loader: `i18n.ts` -> loads `messages/${locale}.json`

8. When to ask the human

-   Any changes to authentication flows, API base URL, or middleware matcher — ask before modifying.
-   Large refactors of `app/` routing or internationalization.

Give feedback: if anything here is unclear or missing, tell me what you want added and I will iterate.
