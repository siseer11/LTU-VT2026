import getFullListOfActorsName from "@/lib/data/getFullListOfActorsName";
import getGenres from "@/lib/data/getGenres";
import MovieCreateForm from "@/components/MovieCreateForm";

export default async function CreateMoviePage() {
  const [genres, actors] = await Promise.all([
    getGenres(),
    getFullListOfActorsName(),
  ]);

  return (
    <div className="py-10">
      <MovieCreateForm genres={genres} actors={actors} />
    </div>
  );
}
