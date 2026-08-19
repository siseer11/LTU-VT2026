import ActorCardLoading from "@/components/ActorCardLoading";
import ActorFilterSection from "@/components/ActorFilterSection";
import ActorsResults from "@/components/ActorsResults";
import PaginationLoading from "@/components/PaginationLoading";
import { notFound } from "next/navigation";
import { Suspense } from "react";
interface Props {
  searchParams: Promise<{
    page?: string;
    query?: string;
  }>;
}

export default async function Actors({ searchParams }: Props) {
  const { page = "1", query } = await searchParams;
  const pageNumber = Number(page);

  if (isNaN(pageNumber) || pageNumber < 1) {
    return notFound();
  }

  return (
    <div className="pt-8 pb-8 px-4">
      <ActorFilterSection initialQuery={query} />
      <Suspense
        key={`actors-page-${pageNumber}-${query ?? ""}`}
        fallback={
          <>
            <ActorCardLoading />
            <PaginationLoading />
          </>
        }
      >
        <ActorsResults page={pageNumber} query={query} />
      </Suspense>
    </div>
  );
}
