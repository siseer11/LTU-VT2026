import { cacheLife, cacheTag } from "next/cache";
import prisma from "../prisma";
import { ActorWhereInput } from "@/app/generated/prisma/internal/prismaNamespaceBrowser";

export default async function getPaginatedActors({
  page,
  queryTerm,
  pageSize,
}: {
  page: number;
  queryTerm?: string;
  pageSize: number;
}) {
  "use cache";
  cacheLife("days");
  cacheTag("actors");

  const where: ActorWhereInput = {
    ...(queryTerm && {
      name: {
        contains: queryTerm,
        mode: "insensitive",
      },
    }),
  };

  return Promise.all([
    prisma.actor.findMany({
      where: where,
      take: pageSize,
      skip: (page - 1) * pageSize,
      select: {
        id: true,
        name: true,
        image: true,
      },
      orderBy: {
        name: "asc",
      },
    }),
    prisma.actor.count({
      where: where,
    }),
  ]);
}
