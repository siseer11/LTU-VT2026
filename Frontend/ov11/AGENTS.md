<!-- BEGIN:nextjs-agent-rules -->

# This is NOT the Next.js you know

This version has breaking changes — APIs, conventions, and file structure may all differ from your training data. Read the relevant guide in `node_modules/next/dist/docs/` (resolved from this file's directory; in monorepos the `next` package may not be visible from the repo root) before writing any code. Heed deprecation notices.

This block is written and re-added by `next dev` — verify at `node_modules/next/dist/server/lib/generate-agent-files.js`. Removing it from a diff only re-creates the uncommitted change; committing it with your work keeps the tree clean.

<!-- END:nextjs-agent-rules -->

# Project guide

This repository is a Next.js 16 app using the App Router and TypeScript. It is primarily a Prisma-backed movie catalog with simple route pages under [app](app), shared UI in [components](components), and database access centralized in [lib/prisma.ts](lib/prisma.ts).

## Key conventions

- App routes live in [app](app) and are server components by default. In this codebase, route params and search params are often awaited promises in Next 16, so page handlers may look like `searchParams: Promise<{ page?: string }>` and then `await searchParams` before reading values.
- Shared UI lives in [components](components), while shadcn-style primitives live in [components/ui](components/ui). Reuse existing patterns before creating new component abstractions.
- The database layer is intentionally centralized in [lib/prisma.ts](lib/prisma.ts). Use the generated Prisma client under [app/generated/prisma](app/generated/prisma) instead of ad hoc database setup code.
- The Prisma schema and models live in [prisma/schema.prisma](prisma/schema.prisma). When you change schema entities or relationships, inspect [prisma/seed.ts](prisma/seed.ts) and apply the corresponding Prisma migration/generation workflow.
- Styling is Tailwind-based and the global app styling entry point is [app/globals.css](app/globals.css). Import aliases use `@/`, so prefer `@/components/...` and `@/lib/...` instead of relative imports when working inside the app.
- Read [README.md](README.md) for generic bootstrap notes and [package.json](package.json) for the exact scripts and dependency versions used in this repo.

## Repo-specific facts

- Runtime stack: Next.js 16, React 19, Prisma 7, and PostgreSQL via the Prisma Pg adapter.
- Common scripts from [package.json](package.json): `pnpm dev`, `pnpm lint`, `pnpm exec prisma generate`, and `pnpm prisma migrate dev` when schema changes are intentional.
- The app uses the App Router, not the Pages Router. When editing routes, keep the server/client boundary in mind and favor server components for data fetching.
- The generated Prisma client is intentionally checked into the app tree at [app/generated/prisma](app/generated/prisma), so do not replace it manually without regenerating the client.

## Working style

- Keep patches small and consistent with neighboring files.
- Prefer existing components, naming, and route patterns over introducing a new architecture.
- For invalid inputs or missing entities, use `notFound()` and `redirect()` from `next/navigation` rather than custom fallback markup when the app expects those semantics.
- Do not introduce custom database queries or logic when the project already has a clear Prisma pattern available.

## Useful references

- [app/page.tsx](app/page.tsx) for the home route pattern.
- [app/movies/page.tsx](app/movies/page.tsx) for pagination and async search-param handling.
- [components/MovieCard.tsx](components/MovieCard.tsx) for existing UI composition and link usage.
- [prisma/schema.prisma](prisma/schema.prisma) for the data model surface area.
