import getPaginatedActors from "@/lib/data/getPaginatedActors";
import PaginationFull from "./PaginationFull";
import Link from "next/link";
import Image from "next/image";

const PAGE_SIZE = 18;
export default async function ActorsResults({
  page,
  query,
}: {
  page: number;
  query?: string;
}) {
  const [actors, totalActors] = await getPaginatedActors({
    page,
    queryTerm: query,
    pageSize: PAGE_SIZE,
  });

  if (actors.length === 0) {
    return (
      <div className="pt-14 pb-8 px-4 w-full text-center flex-1 flex flex-col items-center justify-center">
        <h2 className="text-5xl">😕</h2>
        <h2 className="text-2xl font-bold mb-4">No actors found</h2>
        <p className="text-gray-600">
          Try adjusting your search or filter to find what you&apos;re looking
          for.
        </p>
      </div>
    );
  }

  const maxNumberOfPages = Math.ceil(totalActors / PAGE_SIZE);

  return (
    <>
      <div className="grid gap-x-4 gap-y-4 grid-cols-[repeat(auto-fit,minmax(160px,1fr))] auto-rows-60 justify-items-center pb-10 mt-10">
        {actors.map(({ id, image, name }) => (
          <div
            key={id}
            className="group relative shadow-sm rounded-sm overflow-hidden cursor-pointer max-w-50 w-full bg-card"
          >
            <Link href={`/actors/${id}`}>
              {image ? (
                <div className="relative w-full h-full">
                  <Image
                    className="object-cover h-full w-full group-hover:scale-110 transition-transform duration-400"
                    src={image}
                    alt={`${name} poster`}
                    fill
                  />
                </div>
              ) : (
                <div className="w-full h-full flex items-center justify-center bg-white/2 text-8xl font-bold">
                  ?
                </div>
              )}
              <div className="absolute bottom-0 left-0 w-full bg-linear-to-t from-black to-transparent px-2 pt-30 pb-4">
                <h2 className="text-center line-clamp-2 font-medium text-white/80  text-sm group-hover:-translate-y-2 transition-transform duration-400 delay-100">
                  {name}
                </h2>
              </div>
            </Link>
          </div>
        ))}
      </div>
      {maxNumberOfPages > 1 && (
        <PaginationFull
          currentPage={page}
          maxNumberOfPages={maxNumberOfPages}
          path="actors"
        />
      )}
    </>
  );
}
