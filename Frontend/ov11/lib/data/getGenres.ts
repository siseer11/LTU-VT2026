import { cacheLife, cacheTag } from "next/cache";
import prisma from "@/lib/prisma";

export default async function getGenres() {
  "use cache";
  cacheLife("days");
  cacheTag("genres");

  const genres = await prisma.genre.findMany({
    select: {
      name: true,
      id: true,
    },
  });

  return genres;
}
