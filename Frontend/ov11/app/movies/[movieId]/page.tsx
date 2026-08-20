import getMovieById from "@/lib/data/getMovieById";
import { notFound } from "next/navigation";
import dayjs from "dayjs";
import Link from "next/link";
import React from "react";
import Image from "next/image";
import MovieDetailsActorsCard from "@/components/MovieDetailsActorsCard";
import { Separator } from "@/components/ui/separator";
import DeleteMovieButton from "@/components/DeleteMovieButton";

export default async function Movie({
  params,
}: {
  params: Promise<{ movieId: string }>;
}) {
  const { movieId } = await params;
  const numberMovieId = Number(movieId);

  if (isNaN(numberMovieId)) {
    return notFound();
  }

  const movieData = await getMovieById(numberMovieId);

  if (!movieData) {
    return notFound();
  }

  const nthGenres = movieData.genre.length;
  const voteScore = movieData.voteAverage
    ? Math.floor(Number(movieData.voteAverage) * 10)
    : null;

  return (
    <div className="pt-8 px-4 pb-12">
      <section>
        <div className="relative shadow-2xl">
          <div className="absolute top-0 left-0 w-full h-full z-1 overflow-hidden">
            {movieData.backdrop && (
              <Image
                src={movieData.backdrop}
                alt={`${movieData.title} backdrop`}
                fill
                style={{ objectPosition: "200px 0" }}
                className="object-cover blur-xs"
              />
            )}
          </div>
          <div className="absolute top-0 left-0 w-full h-full z-2 bg-[linear-gradient(90deg,rgb(14,14,15)_25%,rgba(18,17,19,.9),rgb(10,10,11,.75)_100%)]"></div>
          <div className="relative z-10 flex gap-6 px-8 py-8">
            <div className="w-67.5 h-100 overflow-hidden relative min-w-67.5 shadow-lg">
              {movieData.poster ? (
                <Image
                  src={movieData.poster}
                  alt={`${movieData.title} poster`}
                  fill
                  className="object-cover w-full h-full"
                />
              ) : (
                <div className="w-full h-full bg-white/10 flex items-center justify-center text-white/50 text-8xl font-bold">
                  <h1>?</h1>
                </div>
              )}
            </div>
            <div className="flex-1 flex flex-col">
              <div className="flex items-center justify-between gap-2 mb-2">
                <h1 className="text-3xl font-bold text-white line-clamp-2">
                  {movieData.title}
                </h1>
                <DeleteMovieButton id={numberMovieId} />
              </div>
              <div className="flex items-center gap-4 text-whit/90 text-sm mb-8">
                <p>{dayjs(movieData.releaseDate).format("DD/MM/YYYY")}</p>
                <span className="w-1.5 h-1.5 rounded-full bg-white inline-block"></span>
                <div className="flex items-center gap-2">
                  {movieData.genre.map((el, idx) => (
                    <React.Fragment key={el.id}>
                      <span>
                        <Link
                          href={`/movies?genres=${el.name}`}
                          className="hover:text-primary transition-colors duration-200"
                        >
                          {el.name}
                        </Link>
                      </span>
                      {idx < nthGenres - 1 && <span>|</span>}
                    </React.Fragment>
                  ))}
                </div>

                <span className="w-1.5 h-1.5 rounded-full bg-white inline-block"></span>
                <p>
                  {Math.floor(movieData.duration / 60)}h{" "}
                  {movieData.duration % 60}m
                </p>
              </div>
              {voteScore && (
                <div className="flex-1 flex gap-4 items-center">
                  <div
                    style={{
                      background: `conic-gradient(rgb(39, 174, 96) ${voteScore}%, rgb(35 35 35) ${voteScore}%)`,
                    }}
                    className="w-17.5 h-17.5 rounded-xl px-1 py-1"
                  >
                    <h2 className="w-full h-full flex items-center justify-center bg-card rounded-xl font-bold text-white/90 text-xl">
                      {voteScore}
                      <span className="text-xs font-light">%</span>
                    </h2>
                  </div>
                  <div>
                    <h1 className="text-lg font-bold text-white mb-1">
                      User score
                    </h1>
                    <p className="text-white/90 text-sm">
                      {movieData.voteCount} votes
                    </p>
                  </div>
                </div>
              )}
              <div className="mt-8">
                {movieData.tagline && (
                  <p className="text-white/90 italic mb-4">
                    &quot;{movieData.tagline}&quot;
                  </p>
                )}
                <h1 className="text-xl font-bold text-white mb-2">Overview:</h1>
                <p className="text-white/90 text-sm leading-loose">
                  {movieData.description}
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section className="mt-10">
        <Separator className="mb-8" />
        <h1 className="text-xl font-bold text-white mb-6">Actors:</h1>
        <div className="flex flex-wrap gap-4 justify-between">
          {movieData.characters?.map(({ actor, ...character }) => (
            <MovieDetailsActorsCard
              key={actor.id}
              id={actor.id}
              name={actor.name}
              character={character.name}
              image={character.image || actor.image}
            />
          ))}
        </div>
      </section>
    </div>
  );
}
