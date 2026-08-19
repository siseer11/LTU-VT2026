"use client";
import { useState } from "react";
import {
  Combobox,
  ComboboxChip,
  ComboboxChips,
  ComboboxChipsInput,
  ComboboxContent,
  ComboboxEmpty,
  ComboboxItem,
  ComboboxList,
  ComboboxValue,
  useComboboxAnchor,
} from "./ui/combobox";
import { Field, FieldLabel } from "./ui/field";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import { useRouter, usePathname, useSearchParams } from "next/navigation";

interface Props {
  genresList: string[];
  initialQuery?: string;
  initialGenres?: string[];
}

export default function MovieFilterSection({
  genresList,
  initialQuery = "",
  initialGenres = [],
}: Props) {
  const anchor = useComboboxAnchor();
  const [genres, setGenres] = useState<string[]>(initialGenres);
  const [searchTerm, setSearchTerm] = useState<string>(initialQuery);

  // Tracks the last prop values we've already synced into draft state,
  // so we only reset on a genuine external change — not on every render.
  const [prevInitialGenres, setPrevInitialGenres] =
    useState<string[]>(initialGenres);

  if (initialGenres !== prevInitialGenres) {
    setGenres(initialGenres);
    setPrevInitialGenres(initialGenres);
  }

  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();

  const applyFilters = () => {
    // Implement the logic to apply filters based on searchTerm and genres (selected genres)
    const params = new URLSearchParams(searchParams.toString());
    if (searchTerm) {
      params.set("query", searchTerm);
    } else {
      params.delete("query");
    }

    if (genres.length > 0) {
      params.set("genres", genres.join(","));
    } else {
      params.delete("genres");
    }

    params.set("page", "1"); // Reset to page 1 when filters are applied

    router.push(`${pathname}?${params.toString()}`);
  };

  const clearFilters = () => {
    setGenres([]);
    setSearchTerm("");
    const params = new URLSearchParams(searchParams.toString());
    params.delete("query");
    params.delete("genres");
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
          placeholder="Search for a movie..."
          value={searchTerm}
          autoComplete="off"
          spellCheck={false}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </Field>
      <Field>
        <FieldLabel htmlFor="genre">Filter by genre</FieldLabel>
        <Combobox
          autoHighlight
          value={genres}
          onValueChange={setGenres}
          items={genresList}
          multiple
          id="genre"
        >
          <ComboboxChips ref={anchor}>
            <ComboboxValue>
              {genres.map((genre) => (
                <ComboboxChip key={genre}>{genre}</ComboboxChip>
              ))}
            </ComboboxValue>
            <ComboboxChipsInput placeholder="" />
          </ComboboxChips>
          <ComboboxContent>
            <ComboboxEmpty>No items found.</ComboboxEmpty>
            <ComboboxList>
              {genresList.map((item) => (
                <ComboboxItem key={item} value={item}>
                  {item}
                </ComboboxItem>
              ))}
            </ComboboxList>
          </ComboboxContent>
        </Combobox>
      </Field>
      <div className="flex gap-1">
        <Button
          className="px-4"
          disabled={genres.length === 0 && searchTerm === ""}
          onClick={applyFilters}
        >
          Apply
        </Button>
        <Button
          className="px-4"
          variant="secondary"
          disabled={genres.length === 0 && searchTerm === ""}
          onClick={clearFilters}
        >
          Clear
        </Button>
      </div>
    </section>
  );
}
