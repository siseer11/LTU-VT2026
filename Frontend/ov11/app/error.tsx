"use client";

import { Button } from "@/components/ui/button";

export default function Error({
  error,
  reset,
}: {
  error: Error;
  reset: () => void;
}) {
  return (
    <div className="pt-14 pb-8 px-4 text-center h-full flex flex-col items-center justify-center flex-1">
      <h1 className="text-5xl mb-4">🫠</h1>
      <h2 className="text-2xl font-bold mb-2">Something went wrong!</h2>
      <p className="mb-4">{error.message}</p>
      <Button
        variant="secondary"
        className="px-10 py-6 rounded"
        onClick={() => reset()}
      >
        Try again
      </Button>
    </div>
  );
}
