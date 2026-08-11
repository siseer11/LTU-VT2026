import { useRef, useReducer } from "react";
import Counter from "./components/Counter";
import Header from "./components/Header";
import { Button } from "./components/ui/button";
import { DEFAULT_COUNTERS, MAX_VALUE_PER_COUNTER } from "./constants";
import { reducer } from "./reducer";
import AktuellSumma from "./components/AktuellSumma";
import Samlare from "./components/Samlare";

export default function App() {
  const [{ counters, countersTotal, goalAchievedCount }, displatch] =
    useReducer(reducer, {
      counters: DEFAULT_COUNTERS,
      countersTotal: 0,
      goalAchievedCount: 0,
    });

  const lastCounterNumber = useRef<number>(
    Object.values(DEFAULT_COUNTERS).length,
  );

  const addCounter = () => {
    displatch({
      type: "addCounter",
      counterNumber: lastCounterNumber.current + 1,
    });
    lastCounterNumber.current += 1;
  };

  return (
    <div className="min-w-screen min-h-screen bg-background text-foreground py-10 pb-40">
      <div className="max-w-250 mx-auto flex flex-col gap-8">
        <Header />
        <AktuellSumma countersTotal={countersTotal} />
        <Samlare goalAchievedCount={goalAchievedCount} />
        <div className="w-full flex items-center justify-center gap-4">
          <Button onClick={addCounter} className="py-6 px-6">
            + Lägg till räknare
          </Button>
          <Button
            onClick={() => displatch({ type: "resetValues" })}
            className="py-6 px-6"
            variant="secondary"
          >
            Nollställ allt
          </Button>
        </div>
        <div className="flex flex-wrap gap-x-[2%] gap-y-4">
          {Object.values(counters).map((counter) => (
            <Counter
              key={counter.id}
              name={counter.name}
              disabled={counter.value === MAX_VALUE_PER_COUNTER}
              value={counter.value}
              increaseHandler={() =>
                displatch({ type: "increment", counterId: counter.id })
              }
              removeHandler={() =>
                displatch({ type: "removeCounter", counterId: counter.id })
              }
            />
          ))}
        </div>
      </div>
    </div>
  );
}
