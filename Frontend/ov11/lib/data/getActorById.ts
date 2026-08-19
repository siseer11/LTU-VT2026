import prisma from "@/lib/prisma";
import { cacheLife, cacheTag } from "next/cache";

export default async function getActorById(actorId: number) {
  "use cache";
  cacheLife("days");
  cacheTag(`actor-${actorId}`);

  const res = await prisma.actor.findUnique({
    where: {
      id: actorId,
    },
    include: {
      movies: true,
    },
  });

  return res;
}
