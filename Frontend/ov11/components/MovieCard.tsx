import Image from "next/image";
import Link from "next/link";

interface Props {
  id: number;
  title: string;
  description: string;
  image?: string | null;
}

const MovieCard: React.FC<Props> = ({ id, description, title, image }) => {
  return (
    <div className="group relative shadow-sm rounded-sm overflow-hidden cursor-pointer max-w-87.5 w-full bg-card">
      <Link href={`/movies/${id}`}>
        {image ? (
          <Image
            className="object-cover h-full w-full group-hover:scale-110 transition-transform duration-400"
            src={image}
            alt={`${title} poster`}
            width={300}
            height={450}
          />
        ) : (
          <div className="w-full h-full flex items-center justify-center bg-white/2 text-8xl font-bold">
            ?
          </div>
        )}
        <div className="absolute bottom-0 left-0 w-full bg-linear-to-t from-black to-transparent px-4 pt-30 pb-6">
          <h2 className="line-clamp-2 font-black text-foreground text-2xl mb-2 group-hover:-translate-y-2 transition-transform duration-400 delay-100">
            {title}
          </h2>
          <p className="line-clamp-2 text-foreground/85 font-light text-xs group-hover:-translate-y-2 transition-transform duration-400 delay-200">
            {description}
          </p>
        </div>
      </Link>
    </div>
  );
};

export default MovieCard;
