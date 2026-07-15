export interface CarCreate {
  brand: string;
  model: string;
  year: number;
  color: string;
}

export interface Car extends CarCreate {
  id: number;
}
