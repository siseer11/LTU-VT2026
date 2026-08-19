import { Skeleton } from "./ui/skeleton";

export default function ActorCardLoading() {
  return (
    <div className="grid gap-x-4 gap-y-4 grid-cols-[repeat(auto-fit,minmax(160px,1fr))] auto-rows-60 justify-items-center pb-10">
      {new Array(12).fill(null).map((_, i) => (
        <Skeleton
          key={i}
          className="group relative shadow-sm rounded-sm overflow-hidden cursor-pointer max-w-50 w-full bg-white/2"
        >
          <div className="absolute bottom-0 left-0 w-full bg-linear-to-t from-black/70 to-transparent px-4 pt-20 pb-4">
            <Skeleton className="w-[90%] mx-auto h-3 bg-muted-foreground/50 rounded-none mb-2" />
          </div>
        </Skeleton>
      ))}
    </div>
  );
}
