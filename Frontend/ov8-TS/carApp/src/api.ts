import type { Car, CarCreate } from "./types";

const API_URL = "https://localhost:7024/api/cars";

export const fetchCars = async (): Promise<Car[]> => {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error(`Fel vid hämtning: ${response.status}`);
  }

  const cars: Car[] = await response.json();

  return cars;
};

export const createCar = async ({
  brand,
  color,
  model,
  year,
}: CarCreate): Promise<void> => {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      brand,
      model,
      year,
      color,
    }),
  });

  if (!response.ok) {
    throw new Error(`Error while creating car: ${response.status}`);
  }
};

export const deleteCar = async (id: number): Promise<void> => {
  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(`Error while deleting car: ${response.status}`);
  }
};

export const updateCar = async (
  carId: number,
  { brand, color, model, year }: CarCreate,
): Promise<void> => {
  const response = await fetch(`${API_URL}/${carId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      brand,
      model,
      year,
      color,
    }),
  });

  if (!response.ok) {
    throw new Error(`Error while editing car: ${response.status}`);
  }
};
