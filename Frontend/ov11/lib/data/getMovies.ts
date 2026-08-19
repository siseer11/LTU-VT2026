import { MovieWhereInput } from "@/app/generated/prisma/models";
import prisma from "@/lib/prisma";
import { cacheLife, cacheTag } from "next/cache";

interface Props {
  page: number;
  queryTerm?: string;
  genres?: string[];
  pageSize: number;
}

export default async function getMovies({
  page,
  queryTerm,
  genres,
  pageSize,
}: Props) {
  "use cache";
  cacheLife("hours");
  cacheTag("movies");

  const where: MovieWhereInput = {
    ...(queryTerm && {
      title: {
        contains: queryTerm,
        mode: "insensitive",
      },
    }),
    ...(genres &&
      genres.length > 0 && {
        genre: {
          some: {
            name: {
              in: genres,
            },
          },
        },
      }),
  };

  return Promise.all([
    prisma.movie.findMany({
      where,
      take: pageSize,
      skip: (page - 1) * pageSize,
      select: {
        title: true,
        id: true,
        poster: true,
        releaseDate: true,
        description: true,
      },
      orderBy: {
        updatedAt: "asc",
      },
    }),
    prisma.movie.count({ where }),
  ]);
}
