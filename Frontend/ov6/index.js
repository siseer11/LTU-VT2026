let produkter = [
  { id: 1, namn: "Kodnings-kaffemugg", pris: 149, kategori: "Merch" },
  { id: 2, namn: "Mekaniskt Tangentbord", pris: 1299, kategori: "Hårdvara" },
  { id: 3, namn: "Ergonomisk Mus", pris: 799, kategori: "Hårdvara" },
  { id: 4, namn: "Klistermärke 'JS is King'", pris: 29, kategori: "Merch" },
  { id: 5, namn: "Skärmrengöring", pris: 89, kategori: "Tillbehör" },
  { id: 6, namn: "4K Webbkamera", pris: 1495, kategori: "Hårdvara" },
  { id: 7, namn: "Stilren Musmatta (Stor)", pris: 249, kategori: "Tillbehör" },
  { id: 8, namn: "Programmerar-hoodie", pris: 599, kategori: "Merch" },
  { id: 9, namn: "USB-C Hub (6-i-1)", pris: 449, kategori: "Hårdvara" },
  { id: 10, namn: "Blåljusglasögon", pris: 349, kategori: "Tillbehör" },
];

// NIVÅ 1
const hardvaraProducter = produkter.filter(
  (product) => product.kategori === "Hårdvara",
);
const hardvaraProducterUpperName = hardvaraProducter.map((product) =>
  product.namn.toUpperCase(),
);
const hardvaraProducterTotalPrice = hardvaraProducter.reduce(
  (acc, val) => acc + val.pris,
  0,
);

// NIVÅ 3
const showListButton = document.querySelector("#btn-visa");
const list = document.querySelector("#produkt-lista");
const totalPriceComponent = document.querySelector("#total-price");
const mainWrapper = document.querySelector("main");
let listShown = false;

const createElement = ({ elementType, text, classes, clickHandler }) => {
  const comp = document.createElement(elementType);

  if (text) {
    comp.innerText = text;
  }

  if (classes) {
    classes.forEach((el) => comp.classList.add(el));
  }

  if (clickHandler) {
    comp.addEventListener("click", clickHandler);
  }

  return comp;
};

const showTotalPrice = () => {
  const totalPrice = produkter.reduce((acc, val) => acc + val.pris, 0);
  totalPriceComponent.innerText = `Total: ${totalPrice}`;
};

const showProductsList = () => {
  produkter.forEach((product) => {
    const li = createElement({ elementType: "li", classes: ["product-item"] });

    const itemData = createElement({
      elementType: "div",
      classes: ["item-data"],
    });
    const name = createElement({
      elementType: "h1",
      classes: ["product-name"],
      text: product.namn,
    });
    const price = createElement({
      elementType: "h2",
      classes: ["product-price"],
      text: `Price: ${product.pris}Sek`,
    });
    const category = createElement({
      elementType: "p",
      classes: ["product-category"],
      text: product.kategori,
    });
    itemData.appendChild(name);
    itemData.appendChild(price);
    itemData.appendChild(category);

    const removeButton = createElement({
      elementType: "button",
      classes: ["remove-item"],
      text: "Remove",
      clickHandler: () => {
        produkter = produkter.filter((el) => el.id !== product.id);
        list.removeChild(li);
        showTotalPrice();
      },
    });

    li.appendChild(itemData);
    li.appendChild(removeButton);

    list.appendChild(li);
  });
  showTotalPrice();
};

showListButton.addEventListener("click", () => {
  if (listShown) return;

  listShown = true;
  mainWrapper.style.display = "block";
  showProductsList();
});
