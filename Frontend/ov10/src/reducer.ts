import { GOAL_VALUE } from "./constants";
import type { AppState, CounterType, Action } from "./types";

const calculateCountersTotal = (counters: CounterType) => {
  return Object.values(counters).reduce((acc, counter) => {
    acc += counter.value;
    return acc;
  }, 0);
};

export const reducer = (state: AppState, action: Action): AppState => {
  switch (action.type) {
    case "increment": {
      const { counterId } = action;
      const newCounters = {
        ...state.counters,
        [counterId]: {
          ...state.counters[counterId],
          value: state.counters[counterId].value + 1,
        },
      };

      const newCountersTotal = calculateCountersTotal(newCounters);
      // reset values, if goal reached
      if (newCountersTotal >= GOAL_VALUE) {
        const resetCounters = Object.fromEntries(
          Object.entries(newCounters).map(([id, counter]) => [
            id,
            {
              ...counter,
              value: 0,
            },
          ]),
        );
        return {
          ...state,
          counters: resetCounters,
          countersTotal: 0,
          goalAchievedCount: state.goalAchievedCount + 1,
        };
      }
      return {
        ...state,
        counters: newCounters,
        countersTotal: newCountersTotal,
      };
    }
    case "addCounter": {
      const { counterNumber } = action;
      return {
        ...state,
        counters: {
          ...state.counters,
          [`r${counterNumber}`]: {
            value: 0,
            name: `Räknare ${counterNumber}`,
            id: `r${counterNumber}`,
          },
        },
      };
    }
    case "removeCounter": {
      const newCounters = { ...state.counters };
      delete newCounters[action.counterId];

      const newCountersTotal = calculateCountersTotal(newCounters);
      return {
        ...state,
        counters: newCounters,
        countersTotal: newCountersTotal,
      };
    }
    case "resetValues": {
      const newCounters = { ...state.counters };
      Object.keys(newCounters).forEach((key) => (newCounters[key].value = 0));

      return {
        ...state,
        counters: newCounters,
        countersTotal: 0,
        goalAchievedCount: 0,
      };
    }
  }
};
