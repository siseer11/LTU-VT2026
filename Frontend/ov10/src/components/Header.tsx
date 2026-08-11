import { GOAL_VALUE } from "@/constants";

const Header: React.FC = () => (
  <div>
    <h1 className="text-primary text-4xl font-bold text-center">
      Samarbetsräknaren
    </h1>
    <p className="text-muted-foreground text-lg text-center">
      Hjälp räknarna att nå målet{" "}
      <span className="font-bold text-foreground">{GOAL_VALUE}</span>{" "}
      tillsmmanas!
    </p>
  </div>
);

export default Header;
