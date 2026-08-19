"use server";

import { Prisma } from "@/app/generated/prisma/client";
import prisma from "@/lib/prisma";
import { redirect } from "next/dist/client/components/navigation";
import { revalidatePath } from "next/dist/server/web/spec-extension/revalidate";

type DeleteMovieResponse =
  | { success: true }
  | { success: false; error: string };

export async function deleteMovie(id: number): Promise<DeleteMovieResponse> {
  try {
    await prisma.movie.delete({
      where: {
        id,
      },
    });
  } catch (err) {
    if (
      err instanceof Prisma.PrismaClientKnownRequestError &&
      err.code === "P2025"
    ) {
      return { success: false, error: "This movie no longer exists." };
    }
    return { success: false, error: (err as Error).message };
  }

  revalidatePath("/movies");
  redirect("/movies");

  return { success: true };
}
