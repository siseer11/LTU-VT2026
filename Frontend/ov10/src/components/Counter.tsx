import { cn } from "@/lib/utils";
import { Button } from "./ui/button";
import { Card, CardContent, CardFooter, CardHeader } from "./ui/card";

interface Props {
  name: string;
  value: number;
  disabled: boolean;
  increaseHandler: () => void;
  removeHandler: () => void;
}

const Counter: React.FC<Props> = ({
  name,
  disabled,
  value,
  increaseHandler,
  removeHandler,
}) => {
  return (
    <Card
      className={cn(
        "w-[30%] min-w-[32%] px-2 select-none animate-fade-in",
        disabled && "opacity-50",
      )}
    >
      <CardHeader className="flex items-center justify-between">
        <h3 className="text-muted-foreground text-">{name}</h3>
        <Button
          disabled={disabled}
          onClick={removeHandler}
          variant="destructive"
          className="px-4"
        >
          Ta bort
        </Button>
      </CardHeader>
      <CardContent>
        <h2 className="text-center font-bold text-muted-foreground text-lg">
          <span className="text-6xl text-primary ">{value}</span> / 3
        </h2>
      </CardContent>
      <CardFooter>
        <Button
          onClick={increaseHandler}
          disabled={disabled}
          className="w-full py-6"
        >
          {disabled ? "Max nått!" : "Öka värde"}
        </Button>
      </CardFooter>
    </Card>
  );
};

export default Counter;
