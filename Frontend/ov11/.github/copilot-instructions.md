# Shadcn UI Coding Standards

## Project Context

- We use Shadcn UI components built on Radix UI primitives and Tailwind CSS.
- Local Shadcn components are located in `@/components/ui/`.
- Utility functions (like `cn`) are located in `@/lib/utils`.

## Implementation Rules

1. **Never Hallucinate Imports**: Always check if a Shadcn component exists in `@/components/ui/[component-name]` before importing it.
2. **Do Not Recreate**: If a component is needed (e.g., Button, Dialog, Dropdown), use the existing Shadcn version instead of building a new one or importing directly from `@radix-ui`.
3. **Tailwind Merging**: Always use the `cn(...)` utility helper when merging custom className props with existing Tailwind classes.
4. **Theme Variables**: Use CSS variables for colors (e.g., `bg-background`, `text-primary`, `border-input`) instead of hardcoded hex values or arbitrary Tailwind colors like `bg-slate-900`.
