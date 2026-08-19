import { Skeleton } from "./ui/skeleton";

export default function MovieCardLoading() {
  return (
    <div className="grid gap-x-5 gap-y-5 grid-cols-[repeat(auto-fit,minmax(250px,1fr))] auto-rows-107.5 justify-items-center pb-10">
      {new Array(8).fill(null).map((_, i) => (
        <Skeleton
          key={i}
          className="relative shadow-sm rounded-sm overflow-hidden cursor-pointer max-w-87.5 w-full"
        >
          <div className="absolute bottom-0 left-0 w-full bg-linear-to-t from-black to-transparent px-4 pt-30 pb-6">
            <Skeleton className="w-[80%] h-5 bg-muted-foreground rounded-none mb-4" />
            <Skeleton className="w-[90%] h-2 rounded-none mb-2" />
            <Skeleton className="w-[40%] h-2 rounded-none" />
          </div>
        </Skeleton>
      ))}
    </div>
  );
}
