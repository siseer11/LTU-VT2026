// 1. Inställningar (Anpassad till din HTTPS-port från Visual Studio)
const API_URL = "https://localhost:7024/api/cars";

// 2. DOM-referenser
const loadBtn = document.querySelector("#load-btn");
const carList = document.querySelector("#car-list");
const carForm = document.querySelector("#car-form");
const carIdInput = document.querySelector("#car-id");
const formTitle = document.querySelector("#form-title");
const submitBtn = document.querySelector("#submit-btn");
const cancelBtn = document.querySelector("#cancel-btn");
const form = document.querySelector("form#car-form");
const brandInput = document.querySelector("#brand");
const modelInput = document.querySelector("#model");
const yearInput = document.querySelector("#year");
const colorInput = document.querySelector("#color");

let inEditMode = false;
let carBeingEdited = undefined;
const enterEditMode = (carId) => {
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
const fetchCars = async () => {
  try {
    carList.innerHTML = "Loading...";
    const response = await fetch(API_URL);

    if (!response.ok) {
      throw new Error(`Fel vid hämtning: ${response.status}`);
    }

    const cars = await response.json();

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
						<button class="outline" style="padding: 0.25rem 0.5rem; font-size: 0.8rem;" onclick="prepareEdit(${JSON.stringify(car).replace(/"/g, "&quot;")})">Redigera</button>
						<button class="outline contrast" style="padding: 0.25rem 0.5rem; font-size: 0.8rem;" onclick="deleteCar(${car.id})">Ta bort</button>
				</div>
			`;

      carList.appendChild(card);
    });
  } catch (error) {
    console.error("Fel:", error);
    carList.innerHTML = `<p style="color: red;">Kunde inte hämta bilar. Körs ditt API på ${API_URL}?</p>`;
  }
};

// Event listener för ladda-knappen
loadBtn.addEventListener("click", fetchCars);

// ==========================================
// Create (POST)
// ==========================================
const createHandler = async () => {
  submitBtn.disabled = true;
  submitBtn.innerText = "Loading...";

  try {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        brand: brandInput.value,
        model: modelInput.value,
        year: Number(yearInput.value),
        color: colorInput.value,
      }),
    });

    if (response.ok) {
      form.reset();
      fetchCars();
    }
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

const deleteCar = async (id) => {
  try {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });

    if (response.ok) {
      fetchCars();
    }
  } catch (e) {
    console.error("Something went wrong deleting a car", e);
  }
};

// ==========================================
// Edit handler
// ==========================================
cancelBtn.addEventListener("click", () => exitEditMode());

const prepareEdit = ({ brand, color, id, model, year }) => {
  enterEditMode(id);
  brandInput.value = brand;
  colorInput.value = color;
  modelInput.value = model;
  yearInput.value = year;
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
    const response = await fetch(`${API_URL}/${carBeingEdited}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        brand: brandInput.value,
        model: modelInput.value,
        year: Number(yearInput.value),
        color: colorInput.value,
      }),
    });

    if (response.ok) {
      form.reset();
      fetchCars();
    }
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
