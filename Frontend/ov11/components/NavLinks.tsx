"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";

const LINKS = [
  { id: "home", href: "/", label: "Home" },
  { id: "movies", href: "/movies", label: "Movies" },
  { id: "actors", href: "/actors", label: "Actors" },
  { id: "create", href: "/create", label: "Create" },
];

export default function NavLinks() {
  const pathname = usePathname();

  return (
    <ul className="flex gap-x-4">
      {LINKS.map((link) => {
        const isActive = pathname === link.href;

        return (
          <li key={link.id}>
            <Link
              className={`${isActive ? "text-primary font-bold border-b-2 border-b-primary" : "text-muted-foreground"} hover:text-primary transition-colors duration-200 pb-1`}
              href={link.href}
              aria-current={isActive ? "page" : undefined}
            >
              <span className="text-lg">{link.label}</span>
            </Link>
          </li>
        );
      })}
    </ul>
  );
}
