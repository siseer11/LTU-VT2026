export type PaginationItemType = number | "spacer";

const generatePaginationPages = (anchor: number, max: number) => {
  // get max 5 items;
  const nthBeforeAndAfter = 3;

  if (max === 1) {
    return undefined;
  }

  const response: PaginationItemType[] = [];

  const startFrom = Math.max(
    anchor -
      (nthBeforeAndAfter + Math.max(anchor + nthBeforeAndAfter - max, 0)),
    1,
  );
  const goTo = Math.min(
    anchor +
      nthBeforeAndAfter +
      Math.abs(Math.min(anchor - nthBeforeAndAfter - 1, 0)),
    max,
  );

  for (let i = startFrom; i <= goTo; i++) {
    response.push(i);
  }

  const firstElement = response[0];
  if (typeof firstElement === "number") {
    if (firstElement >= 2) {
      response.shift();
      response.shift();
      response.unshift(1, "spacer");
    }
  }

  const lastElement = response[response.length - 1];
  if (typeof lastElement === "number") {
    if (lastElement <= max - 1) {
      response.pop();
      response.pop();
      response.push("spacer", max);
    }

    return response;
  }
};

export default generatePaginationPages;
