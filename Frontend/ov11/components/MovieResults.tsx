import getMovies from "@/lib/data/getMovies";
import MovieCard from "./MovieCard";
import PaginationFull from "./PaginationFull";

const PAGE_SIZE = 12;

interface Props {
  page: number;
  query?: string;
  genres?: string;
}

export default async function MovieResults({ page, query, genres }: Props) {
  let genresArray: string[] | undefined = undefined;

  if (genres) {
    genresArray = genres.split(",");
  }

  const [movies, totalMovies] = await getMovies({
    page,
    queryTerm: query,
    genres: genresArray,
    pageSize: PAGE_SIZE,
  });

  if (movies.length === 0) {
    return (
      <div className="pt-14 pb-8 px-4 w-full text-center flex-1 flex flex-col items-center justify-center">
        <h2 className="text-5xl">😕</h2>
        <h2 className="text-2xl font-bold mb-4">No movies found</h2>
        <p className="text-gray-600">
          Try adjusting your search or filter to find what you&apos;re looking
          for.
        </p>
      </div>
    );
  }

  const maxNumberOfPages = Math.ceil(totalMovies / PAGE_SIZE);

  return (
    <>
      <div className="grid gap-x-5 gap-y-5 grid-cols-[repeat(auto-fit,minmax(250px,1fr))] auto-rows-107.5 justify-items-center pb-10 mt-10">
        {movies.map((movie) => (
          <MovieCard
            key={movie.id}
            id={movie.id}
            title={movie.title}
            image={movie.poster}
            description={movie.description}
          />
        ))}
      </div>
      {maxNumberOfPages > 1 && (
        <PaginationFull
          currentPage={page}
          maxNumberOfPages={maxNumberOfPages}
          path="movies"
        />
      )}
    </>
  );
}
