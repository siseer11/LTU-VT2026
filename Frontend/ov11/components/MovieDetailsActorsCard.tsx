import Image from "next/image";
import Link from "next/link";

interface Props {
  id: number;
  name: string;
  character: string;
  image?: string | null;
}

export default function MovieDetailsActorsCard({
  id,
  name,
  character,
  image,
}: Props) {
  return (
    <Link
      className="flex-1 min-w-61.25 border border-muted rounded-sm hover:-translate-y-1 hover:shadow-lg transition-all duration-300 ease-in-out"
      href={`/actors/${id}`}
    >
      <div className="flex gap-4">
        <div className="w-25 h-full min-h-35 min-w-25 relative overflow-hidden rounded-xs bg-muted-foreground/10 flex items-center justify-center">
          {image ? (
            <Image
              src={image}
              alt={`${name} actor image`}
              fill
              className="object-cover"
            />
          ) : (
            <h2>?</h2>
          )}
        </div>
        <div className="pt-2">
          <div className="mb-4">
            <h2 className="text-white/90 font-bold">Name:</h2>
            <p className="text-white/80 text-sm line-clamp-2">{name}</p>
          </div>
          <div>
            <h2 className="text-white/90 font-bold">Role:</h2>
            <p className="text-white/80 text-sm line-clamp-2">{character}</p>
          </div>
        </div>
      </div>
    </Link>
  );
}
