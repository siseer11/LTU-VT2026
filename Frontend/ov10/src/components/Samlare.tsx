import { GOAL_VALUE } from "@/constants";
import {
  Card,
  CardFooter,
  CardHeader,
  CardTitle,
  CardContent,
} from "./ui/card";

interface Props {
  goalAchievedCount: number;
}

const Samlare: React.FC<Props> = ({ goalAchievedCount }) => (
  <Card>
    <CardHeader>
      <CardTitle className="text-center text-xl">SAMLAREN</CardTitle>
    </CardHeader>
    <CardContent>
      <h2 className="text-8xl font-bold text-center">{goalAchievedCount}</h2>
    </CardContent>
    <CardFooter>
      <p className="text-center text-muted-foreground w-full">
        Totala poäng insamlade (Mål {GOAL_VALUE})
      </p>
    </CardFooter>
  </Card>
);

export default Samlare;
