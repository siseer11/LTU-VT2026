import MovieResults from "@/components/MovieResults";
import MovieFilterSection from "@/components/MovieFilterSection";

import { notFound } from "next/navigation";
import { Suspense } from "react";
import getGenres from "@/lib/data/getGenres";
import MovieCardLoading from "@/components/MovieCardLoading";
import PaginationLoading from "@/components/PaginationLoading";

interface Props {
  searchParams: Promise<{
    page?: string;
    genres?: string;
    query?: string;
  }>;
}

const Movies = async ({ searchParams }: Props) => {
  const { page = "1", query, genres } = await searchParams;
  const pageNumber = Number(page);

  if (isNaN(pageNumber) || pageNumber < 1) {
    return notFound();
  }

  const genresListFromDb = await getGenres();

  return (
    <div className="pt-8 pb-8 px-4">
      <MovieFilterSection
        genresList={genresListFromDb.map((genre) => genre.name)}
        initialQuery={query}
        initialGenres={genres ? genres.split(",") : []}
      />
      <Suspense
        key={`${pageNumber}-${query}-${genres}`}
        fallback={
          <>
            <MovieCardLoading />
            <PaginationLoading />
          </>
        }
      >
        <MovieResults page={pageNumber} query={query} genres={genres} />
      </Suspense>
    </div>
  );
};

export default Movies;
