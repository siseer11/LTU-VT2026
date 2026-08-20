import prisma from "@/lib/prisma";
import { cacheLife, cacheTag } from "next/cache";

export default async function getMovieById(movieId: number) {
  "use cache";
  cacheLife("hours");
  cacheTag(`movie-${movieId}`);

  const res = await prisma.movie.findUnique({
    where: {
      id: movieId,
    },
    include: {
      reviews: true,
      genre: true,
      characters: {
        include: {
          actor: true,
        },
      },
    },
  });

  return res;
}
