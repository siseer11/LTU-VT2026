"use client";

import { useTransition } from "react";
import { Button } from "./ui/button";
import { toast } from "@/components/ui/toast";
import { deleteMovie } from "@/actions/actions";

export default function DeleteMovieButton({ id }: { id: number }) {
  const [isPending, startTransition] = useTransition();

  const handleDelete = async () => {
    if (!confirm("Are you sure you want to delete this movie?")) {
      return;
    }

    startTransition(async () => {
      const result = await deleteMovie(id);

      if (!result.success) {
        toast.add({
          type: "error",
          description: result.error,
          timeout: 3000,
        });
      } else {
        toast.add({
          type: "success",
          description: "Movie deleted successfully!",
          timeout: 3000,
        });
      }
    });
  };

  return (
    <Button
      onClick={handleDelete}
      disabled={isPending}
      variant="destructive"
      className="px-4 cursor-pointer"
    >
      {isPending ? "Deleting..." : "Delete"}
    </Button>
  );
}
