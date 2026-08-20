import { notFound } from "next/navigation";
import getActorById from "@/lib/data/getActorById";
import Image from "next/image";
import { Separator } from "@/components/ui/separator";
import MovieCard from "@/components/MovieCard";
import { Card, CardFooter } from "@/components/ui/card";

interface ActorPageProps {
  params: Promise<{
    actorId: string;
  }>;
}

export default async function ActorPage({ params }: ActorPageProps) {
  const { actorId } = await params;

  if (isNaN(Number(actorId)) || Number(actorId) < 1) {
    return notFound();
  }

  const actorIdNumber = Number(actorId);
  const actor = await getActorById(actorIdNumber);

  if (!actor) {
    return notFound();
  }

  return (
    <>
      <div className="py-4 px-4 bg-[linear-gradient(180deg,#5b5b5b12,#09090b)] mt-8 rounded-xs flex gap-6">
        <div className="w-50 h-75 min-w-50 relative rounded-xs overflow-hidden shadow-lg">
          {actor.image ? (
            <Image src={actor.image} alt={`${actor.name} image`} fill />
          ) : (
            <div className="w-full h-full bg-white/5 rounded-xs text-5xl font-bold flex items-center justify-center">
              ?
            </div>
          )}
        </div>
        <div className="flex flex-col gap-4 pt-2">
          <h1 className="text-2xl font-bold text-white/80">{actor.name}</h1>
          <div>
            <h2 className="text-white/80 font-bold text-lg pb-1">Born</h2>
            <p className="text-white/70 text-sm">June 29, 1980</p>
          </div>
          <div>
            <h2 className="text-white/80 font-bold text-lg pb-1">About</h2>
            <p className="text-white/70 text-sm leading-relaxed">
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Nesciunt
              praesentium commodi temporibus quod laudantium, eum nihil at
              architecto, id nulla aliquam voluptas cum iusto, autem explicabo
              alias laboriosam adipisci expedita.
            </p>
          </div>
        </div>
      </div>
      <Separator className="my-8" />
      <div className="pb-20">
        {actor.characters.length === 0 ? (
          <div className="pt-14 pb-8 px-4 w-full text-center flex-1 flex flex-col items-center justify-center">
            <h2 className="text-5xl">😕</h2>
            <p className="text-white/70 text-sm leading-relaxed">
              No movies found for this actor.
            </p>
          </div>
        ) : (
          <>
            <h2 className="text-white/80 font-bold text-xl pb-1">
              Movies present in:
            </h2>
            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4 mt-4">
              {actor.characters.map(({ movie, ...character }) => (
                <Card key={movie.id} className="px-0 py-0 gap-0">
                  <MovieCard
                    key={movie.id}
                    id={movie.id}
                    title={movie.title}
                    image={movie.poster}
                    description={movie.description}
                  />
                  <CardFooter className="items-start">
                    <h2 className="mr-2">As:</h2>
                    <p className="font-bold line-clamp-3">{character.name}</p>
                  </CardFooter>
                </Card>
              ))}
            </div>
          </>
        )}
      </div>
    </>
  );
}
