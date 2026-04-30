using FootballPlayer = WorkingWithNamespaces.Football.Player;
using HandballPlayer = WorkingWithNamespaces.Handball.Player;

FootballPlayer fPlayer = new();
HandballPlayer hPlayer = new();
WorkingWithNamespaces.Basketball.Player bPlayer = new();

fPlayer.WritePlayerDetails();
hPlayer.WritePlayerDetails();
bPlayer.WritePlayerDetails();