import { GOAL_VALUE } from "@/constants";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

interface Props {
  countersTotal: number;
}

const AktuellSumma: React.FC<Props> = ({ countersTotal }) => (
  <Card className="py-8 px-4">
    <CardHeader className="flex items-center justify-between">
      <CardTitle className="text-xl font-bold text-muted-foreground">
        Målprogress (Aktuell summa)
      </CardTitle>
      <CardTitle className="text-primary">
        {countersTotal} / {GOAL_VALUE}
      </CardTitle>
    </CardHeader>
    <CardContent>
      <div className="w-full h-5 bg-muted rounded-sm relative overflow-hidden">
        <div
          style={{
            width: `${(countersTotal / GOAL_VALUE) * 100}%`,
          }}
          className="w-[50%] h-full left-0 top-0 bg-primary transition-[width] duration-300 ease-out"
        ></div>
      </div>
    </CardContent>
  </Card>
);

export default AktuellSumma;
