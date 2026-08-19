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
      actors: true,
      reviews: true,
      genre: true,
    },
  });

  return res;
}
