export interface CounterType {
  [key: string]: { name: string; value: number; id: string };
}

export interface AppState {
  counters: CounterType;
  countersTotal: number;
  goalAchievedCount: number;
}

export type Action =
  | { type: "increment"; counterId: string }
  | { type: "addCounter"; counterNumber: number }
  | { type: "removeCounter"; counterId: string }
  | { type: "resetValues" };
