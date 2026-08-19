import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import generatePaginationPages from "@/lib/utils/generatePaginationPages";

interface Props {
  currentPage: number;
  maxNumberOfPages: number;
  path: string;
}

const PaginationFull: React.FC<Props> = ({
  currentPage,
  maxNumberOfPages,
  path,
}) => {
  const hasPrev = currentPage > 1;
  const hasNext = currentPage < maxNumberOfPages;
  return (
    <Pagination>
      <PaginationContent>
        <PaginationItem
          className={!hasPrev ? "pointer-events-none opacity-55" : ""}
        >
          <PaginationPrevious href={`/${path}?page=${currentPage - 1}`} />
        </PaginationItem>

        {generatePaginationPages(currentPage, maxNumberOfPages)?.map(
          (page, idx) => {
            if (page === "spacer") {
              return (
                <PaginationItem key={`spacer-${idx}`}>
                  <PaginationEllipsis />
                </PaginationItem>
              );
            }
            return (
              <PaginationItem key={page}>
                <PaginationLink
                  href={`/${path}?page=${page}`}
                  isActive={page == currentPage}
                >
                  {page}
                </PaginationLink>
              </PaginationItem>
            );
          },
        )}
        <PaginationItem
          className={!hasNext ? "pointer-events-none opacity-55" : ""}
        >
          <PaginationNext href={`/${path}?page=${currentPage + 1}`} />
        </PaginationItem>
      </PaginationContent>
    </Pagination>
  );
};

export default PaginationFull;
