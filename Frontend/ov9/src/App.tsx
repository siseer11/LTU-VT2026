import { useState } from "react";

interface ICar {
  regNumber: string;
  brand: string;
}

export default function App() {
  const [cars, setCars] = useState<ICar[]>([]);

  const [regNumber, setRegNumber] = useState("");
  const [brand, setBrand] = useState("");

  const [error, setError] = useState("");

  const handleAddCar = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    const validRegNr = /[A-Z]{3}(?=[A-Z]*[0-9][A-Z]*$)[A-Z0-9]{3}/i.test(
      regNumber,
    );

    if (brand == "") {
      setError("Brand must be filled!");
      return;
    }

    if (!validRegNr) {
      setError("Not a valid registration number!");
      return;
    }

    const carAlreadyParked = cars.some((car) => car.regNumber === regNumber);

    if (carAlreadyParked) {
      setError("The car is already parked!");
      return;
    }

    setCars((prevState) => [
      ...prevState,
      {
        regNumber,
        brand,
      },
    ]);

    setError("");
    setRegNumber("");
    setBrand("");
  };

  const handleDeleteCar = (regNr: string) => {
    setCars((prevState) => {
      return [...prevState].filter((car) => car.regNumber !== regNr);
    });
  };

  return (
    <div className="min-h-screen bg-slate-900 text-slate-100 p-8 font-sans">
      <div className="max-w-xl mx-auto">
        <header className="mb-8 text-center">
          <h1 className="text-3xl font-bold text-emerald-400 mb-2">
            Minigaraget (Startskal)
          </h1>
          <p className="text-slate-400">
            Antal bilar i garaget:{" "}
            <span className="font-semibold text-white">{cars.length}</span>
          </p>
        </header>

        {/* Formulär för att parkera fordon */}
        <div className="bg-slate-800 p-6 rounded-xl border border-slate-700 shadow-lg mb-8">
          <h2 className="text-xl font-semibold mb-4 text-emerald-300">
            Parkera nytt fordon
          </h2>

          {error && (
            <div className="mb-4 p-3 bg-red-500/20 border border-red-500 text-red-300 rounded-lg text-sm">
              {error}
            </div>
          )}

          <form onSubmit={handleAddCar} className="space-y-4">
            <div>
              <label
                htmlFor="regNr"
                className="block text-sm font-medium text-slate-300 mb-1"
              >
                Regnummer
              </label>
              <input
                type="text"
                id="regNr"
                value={regNumber}
                onChange={(e) => setRegNumber(e.target.value)}
                placeholder="t.ex. ABC123"
                className="w-full bg-slate-900 border border-slate-700 rounded-lg px-4 py-2 text-white uppercase focus:outline-none focus:border-emerald-500"
              />
            </div>

            <div>
              <label
                htmlFor="brand"
                className="block text-sm font-medium text-slate-300 mb-1"
              >
                Märke
              </label>
              <input
                id="brand"
                type="text"
                value={brand}
                onChange={(e) => setBrand(e.target.value)}
                placeholder="t.ex. Volvo"
                className="w-full bg-slate-900 border border-slate-700 rounded-lg px-4 py-2 text-white focus:outline-none focus:border-emerald-500"
              />
            </div>

            <button
              type="submit"
              className="w-full bg-emerald-600 hover:bg-emerald-500 text-white font-semibold py-2.5 rounded-lg transition-colors cursor-pointer shadow-md"
            >
              Parkera bil
            </button>
          </form>
        </div>

        {/* Lista över parkerade fordon */}
        <div>
          <h2 className="text-xl font-semibold mb-4 text-emerald-300">
            Parkerade fordon
          </h2>

          {cars.length === 0 ? (
            <p className="text-slate-500 text-center py-8 bg-slate-800/50 rounded-xl border border-slate-800">
              Garaget är helt tomt.
            </p>
          ) : (
            <div className="space-y-3">
              {cars.map((car) => {
                const deleteHandler = () => handleDeleteCar(car.regNumber);
                return (
                  <div
                    key={car.regNumber}
                    className="bg-slate-800 p-4 rounded-xl border border-slate-700 flex justify-between items-center shadow-md"
                  >
                    <div className="flex items-center gap-3">
                      <span className="bg-emerald-500/20 text-emerald-300 text-xs font-mono px-2.5 py-1 rounded border border-emerald-500/30 uppercase font-semibold">
                        {car.regNumber}
                      </span>
                      <h3 className="text-lg font-bold text-white">
                        {car.brand}
                      </h3>
                    </div>

                    <button
                      onClick={deleteHandler}
                      className="bg-red-600/20 hover:bg-red-600 text-red-400 hover:text-white px-4 py-2 rounded-lg text-sm font-medium border border-red-500/30 transition-all cursor-pointer"
                    >
                      Ta bort
                    </button>
                  </div>
                );
              })}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
