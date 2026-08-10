import { createCar, deleteCar, fetchCars, updateCar } from "./api";
import type { Car } from "./types";

// 1. Inställningar (Anpassad till din HTTPS-port från Visual Studio)
const API_URL = "https://localhost:7024/api/cars";

// 2. DOM-referenser
const loadBtn = document.querySelector("#load-btn") as HTMLButtonElement;
const carList = document.querySelector("#car-list") as HTMLUListElement;
// const carForm = document.querySelector("#car-form");
// const carIdInput = document.querySelector("#car-id");
// const formTitle = document.querySelector("#form-title");
const submitBtn = document.querySelector("#submit-btn") as HTMLButtonElement;
const cancelBtn = document.querySelector("#cancel-btn") as HTMLButtonElement;
const form = document.querySelector("form#car-form") as HTMLFormElement;
const brandInput = document.querySelector("#brand") as HTMLInputElement;
const modelInput = document.querySelector("#model") as HTMLInputElement;
const yearInput = document.querySelector("#year") as HTMLInputElement;
const colorInput = document.querySelector("#color") as HTMLInputElement;

let inEditMode = false;
let carBeingEdited: number | undefined = undefined;
console.log("zzz");
const enterEditMode = (carId: number) => {
  window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
  carBeingEdited = carId;
  inEditMode = true;
  cancelBtn.style.display = "block";
  submitBtn.innerText = "Update";
};

const exitEditMode = () => {
  carBeingEdited = undefined;
  inEditMode = false;
  cancelBtn.style.display = "none";
  submitBtn.innerText = "Sapara bil";
  form.reset();
};

// ==========================================
// 🟢 READ (GET) - Hämta och visa alla bilar
// ==========================================
const generateCarsList = async () => {
  try {
    carList.innerHTML = "Loading...";
    const cars: Car[] = await fetchCars();

    // Töm listan innan vi ritar ut på nytt
    carList.innerHTML = "";

    if (cars.length === 0) {
      carList.innerHTML = "<p>Det finns inga bilar i databasen.</p>";
      return;
    }

    // Loopa igenom bilarna och bygg HTML för varje kort
    cars.forEach((car) => {
      const card = document.createElement("div");
      card.className = "car-card";

      card.innerHTML = `
				<div>
						<strong>${car.brand} ${car.model}</strong> (${car.year}) <br>
						<span style="font-size: 0.9rem; color: #777;">Färg: ${car.color}</span>
				</div>
				<div class="btn-group">
						<button data-buttontype="update" class="outline" style="padding: 0.25rem 0.5rem; font-size: 0.8rem;">Redigera</button>
						<button data-buttontype="delete" class="outline contrast" style="padding: 0.25rem 0.5rem; font-size: 0.8rem;">Ta bort</button>
				</div>
			`;

      card.addEventListener("click", (e) => {
        if (e.target) {
          const target = e.target as HTMLElement;
          const targetDataType = target.dataset.buttontype;

          if (targetDataType === "update") {
            prepareEdit(car);
          } else if (targetDataType === "delete") {
            deleteCarHandler(car.id);
          }
        }
      });

      carList.appendChild(card);
    });
  } catch (error) {
    console.error("Fel:", error);
    carList.innerHTML = `<p style="color: red;">Kunde inte hämta bilar. Körs ditt API på ${API_URL}?</p>`;
  }
};

// Event listener för ladda-knappen
loadBtn.addEventListener("click", generateCarsList);

// ==========================================
// Create (POST)
// ==========================================
const createHandler = async () => {
  submitBtn.disabled = true;
  submitBtn.innerText = "Loading...";

  try {
    await createCar({
      brand: brandInput.value,
      model: modelInput.value,
      year: Number(yearInput.value),
      color: colorInput.value,
    });

    form.reset();
    generateCarsList();
  } catch (e) {
    console.error("Something went wrong adding a car", e);
  } finally {
    submitBtn.disabled = false;
    submitBtn.innerText = "Spara bil";
  }
};

// ==========================================
// Delete handler
// ==========================================
const deleteCarHandler = async (id: number) => {
  try {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      throw new Error(`Error while deleting car: ${response.status}`);
    }
  } catch (e) {
    console.error("Something went wrong deleting a car", e);
  }
};

// ==========================================
// Edit handler
// ==========================================
cancelBtn.addEventListener("click", () => exitEditMode());

const prepareEdit = ({ brand, color, id, model, year }: Car) => {
  enterEditMode(id);
  brandInput.value = brand;
  colorInput.value = color;
  modelInput.value = model;
  yearInput.value = String(year);
};

const updateHandler = async () => {
  if (!carBeingEdited) {
    console.error("Something went wrong!");
    return;
  }

  submitBtn.disabled = true;
  cancelBtn.disabled = true;
  submitBtn.innerText = "Loading...";

  try {
    await updateCar(carBeingEdited, {
      brand: brandInput.value,
      model: modelInput.value,
      year: Number(yearInput.value),
      color: colorInput.value,
    });

    form.reset();
    generateCarsList();
  } catch (e) {
    console.error("Something went wrong editing a car", e);
  } finally {
    submitBtn.disabled = false;
    cancelBtn.disabled = false;
    exitEditMode();
  }
};

// ==========================================
// Form submit handler
// ==========================================
form.addEventListener("submit", async (e) => {
  e.preventDefault();

  if (inEditMode) {
    updateHandler();
  } else {
    createHandler();
  }
});
