import prisma from "@/lib/prisma";
import { cacheLife } from "next/dist/server/use-cache/cache-life";
import { cacheTag } from "next/dist/server/use-cache/cache-tag";

export default async function getFullListOfActorsName() {
  "use cache";
  cacheLife("days");
  cacheTag("actors");

  const res = await prisma.actor.findMany({
    select: {
      id: true,
      name: true,
      image: true,
    },
  });

  return res;
}
