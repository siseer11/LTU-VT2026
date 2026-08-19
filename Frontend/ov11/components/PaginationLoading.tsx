import { Skeleton } from "./ui/skeleton";

export default function PaginationLoading() {
  return (
    <div className="flex items-center justify-center gap-4">
      <Skeleton className="w-[10%] h-6 rounded-none mb-2" />
      <Skeleton className="w-[30%] h-6 rounded-none mb-2" />
      <Skeleton className="w-[10%] h-6 rounded-none mb-2" />
    </div>
  );
}
