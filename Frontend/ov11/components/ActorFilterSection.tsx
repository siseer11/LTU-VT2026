"use client";
import { useState } from "react";

import { Field, FieldLabel } from "./ui/field";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import { useRouter, usePathname, useSearchParams } from "next/navigation";

interface Props {
  initialQuery?: string;
}

export default function ActorFilterSection({ initialQuery = "" }: Props) {
  const [searchTerm, setSearchTerm] = useState<string>(initialQuery);

  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();

  const applyFilters = () => {
    const params = new URLSearchParams(searchParams.toString());
    if (searchTerm) {
      params.set("query", searchTerm);
    } else {
      params.delete("query");
    }

    params.set("page", "1"); // Reset to page 1 when filters are applied
    router.push(`${pathname}?${params.toString()}`);
  };

  const clearFilters = () => {
    setSearchTerm("");
    const params = new URLSearchParams(searchParams.toString());
    params.delete("query");
    params.set("page", "1");
    router.push(`${pathname}?${params.toString()}`);
  };

  return (
    <section className="flex gap-4 mb-8 items-end">
      <Field>
        <FieldLabel htmlFor="search">Search</FieldLabel>
        <Input
          className="py-4"
          id="search"
          placeholder="Search for an actor..."
          value={searchTerm}
          autoComplete="off"
          spellCheck={false}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </Field>
      <div className="flex gap-1">
        <Button
          className="px-4"
          disabled={searchTerm === ""}
          onClick={applyFilters}
        >
          Apply
        </Button>
        <Button
          className="px-4"
          variant="secondary"
          disabled={searchTerm === ""}
          onClick={clearFilters}
        >
          Clear
        </Button>
      </div>
    </section>
  );
}
