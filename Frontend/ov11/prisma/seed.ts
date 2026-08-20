import { PrismaClient, Prisma } from "../app/generated/prisma/client";
import { PrismaPg } from "@prisma/adapter-pg";
import { faker } from "@faker-js/faker";
import "dotenv/config";

const adapter = new PrismaPg({
  connectionString: process.env.DATABASE_URL,
});

const prisma = new PrismaClient({
  adapter,
});

const genresData: Prisma.GenreCreateInput[] = [
  {
    name: "Adventure",
  },
  {
    name: "Fantasy",
  },
  {
    name: "Animation",
  },
  {
    name: "Drama",
  },
  {
    name: "Horror",
  },
  {
    name: "Action",
  },
  {
    name: "Comedy",
  },
  {
    name: "Thriller",
  },
  {
    name: "Crime",
  },
  {
    name: "Documentary",
  },
  {
    name: "Science Fiction",
  },
  {
    name: "Mystery",
  },
  {
    name: "Music",
  },
  {
    name: "Romance",
  },
  {
    name: "Family",
  },
  {
    name: "War",
  },
];

const actors: Prisma.ActorCreateInput[] = [
  {
    name: "George Lucas",

    image: "https://image.tmdb.org/t/p/w300//mDLDvsx8PaZoEThkBdyaG1JxPdf.jpg",
  },
  {
    name: "Allison Janney",

    image: "https://image.tmdb.org/t/p/w300//kSaSnQ9xU8eVNL0mTppab95dZA8.jpg",
  },
  {
    name: "Tom Hanks",

    image: "https://image.tmdb.org/t/p/w300//oFvZoKI6lvU03n4YoNGAll9rkas.jpg",
  },
  {
    name: "Mark Ruffalo",

    image: "https://image.tmdb.org/t/p/w300//5GilHMOt5PAQh6rlUKZzGmaKEI7.jpg",
  },
  {
    name: "Jamie Foxx",

    image: "https://image.tmdb.org/t/p/w300//zD8Nsy4Xrghp7WunwpCj5JKBPeU.jpg",
  },
  {
    name: "Lucy Liu",

    image: "https://image.tmdb.org/t/p/w300//9nbtjqsx3De7hO2XDtrBQ7M9VCH.jpg",
  },
  {
    name: "Kirsten Dunst",

    image: "https://image.tmdb.org/t/p/w300//yhLKGjuiMMdbGnFrR8AREkCZcVF.jpg",
  },
  {
    name: "Alfred Molina",

    image: "https://image.tmdb.org/t/p/w300//nJo91Czesn6z0d0pkfbDoVZY3sg.jpg",
  },
  {
    name: "Ke Huy Quan",

    image: "https://image.tmdb.org/t/p/w300//iestHyn7PLuVowj5Jaa1SGPboQ4.jpg",
  },
  {
    name: "Edward Norton",

    image: "https://image.tmdb.org/t/p/w300//8nytsqL59SFJTVYVrN72k6qkGgJ.jpg",
  },
  {
    name: "Penélope Cruz",

    image: "https://image.tmdb.org/t/p/w300//n5SFgmvJSENQt8poE4qYacUnKOi.jpg",
  },
  {
    name: "Jeff Bridges",

    image: "https://image.tmdb.org/t/p/w300//xms1RAY6q7Lzp7wNeRCB0kzhucn.jpg",
  },
  {
    name: "Ian McKellen",

    image: "https://image.tmdb.org/t/p/w300//5cnnnpnJG6TiYUSS7qgJheUZgnv.jpg",
  },
  {
    name: "Anna Faris",

    image: "https://image.tmdb.org/t/p/w300//tptTb0BQwCpKWETNqzkZzzf5qSZ.jpg",
  },
  {
    name: "Anne Hathaway",

    image: "https://image.tmdb.org/t/p/w300//nbccV2pMoyLTCeg5DQip24Eq0Jp.jpg",
  },
  {
    name: "Matt Damon",

    image: "https://image.tmdb.org/t/p/w300//aCvBXTAR9B1qRjIRzMBYhhbm1fR.jpg",
  },
  {
    name: "Ken Leung",

    image: "https://image.tmdb.org/t/p/w300//hpatUP6u74gkpDRmn9voNY9V43O.jpg",
  },
  {
    name: "Tobey Maguire",

    image: "https://image.tmdb.org/t/p/w300//1EtXu72Cuo7YX4IqIaEslwOhEQ9.jpg",
  },
  {
    name: "Michael Keaton",

    image: "https://image.tmdb.org/t/p/w300//tYSja1KByFnZ4Hkp3stPqkKHnNL.jpg",
  },
  {
    name: "Stanley Tucci",

    image: "https://image.tmdb.org/t/p/w300//q4TanMDI5Rgsvw4SfyNbPBh4URr.jpg",
  },
  {
    name: "Ewan McGregor",

    image: "https://image.tmdb.org/t/p/w300//aEmyadfRXTmmR7UW7OXsm5a6smS.jpg",
  },
  {
    name: "Marisa Tomei",

    image: "https://image.tmdb.org/t/p/w300//vAQ10fvGfVyuJdMNKVMA4ZkacWL.jpg",
  },
  {
    name: "Robert Downey Jr.",

    image: "https://image.tmdb.org/t/p/w300//5qHNjhtjMD4YWH3UP0rm4tKwxCL.jpg",
  },
  {
    name: "Joan Cusack",

    image: "https://image.tmdb.org/t/p/w300//59UIeHZFYrKyP20lXqijtfTXglO.jpg",
  },
  {
    name: "Naomi Watts",

    image: "https://image.tmdb.org/t/p/w300//7ysvff7ZhW388SIh2YjQ0XIryOn.jpg",
  },
  {
    name: "Colin Hanks",

    image: "https://image.tmdb.org/t/p/w300//iljyDSiJRcwJL8QXQZ2WTyU1wh5.jpg",
  },
  {
    name: "Alain Chabat",

    image: "https://image.tmdb.org/t/p/w300//xRjymE6LIJd0useKWN2R1WgKDRy.jpg",
  },
  {
    name: "Russ Meyer",

    image: "https://image.tmdb.org/t/p/w300//zNVAAdgIk9q3SQadZuQKr6wHlXM.jpg",
  },
  {
    name: "John Furlong",

    image: "https://image.tmdb.org/t/p/w300//gPfvY1lZNHeGW16TXmGvFpSkLLw.jpg",
  },
  {
    name: "Meryl Streep",

    image: "https://image.tmdb.org/t/p/w300//emAAzyK1rJ6aiMi0wsWYp51EC3h.jpg",
  },
  {
    name: "Emily Blunt",

    image: "https://image.tmdb.org/t/p/w300//5nCSG5TL1bP1geD8aaBfaLnLLCD.jpg",
  },
  {
    name: "Bonnie Hunt",

    image: "https://image.tmdb.org/t/p/w300//tT9C6uLztgN8OxJULq6F9iEzqlA.jpg",
  },
  {
    name: "Willem Dafoe",

    image: "https://image.tmdb.org/t/p/w300//ui8e4sgZAwMPi3hzEO53jyBJF9B.jpg",
  },
  {
    name: "Chiwetel Ejiofor",

    image: "https://image.tmdb.org/t/p/w300//kq5DDnqqofoRI0t6ddtRlsJnNPT.jpg",
  },
  {
    name: "Colin Firth",

    image: "https://image.tmdb.org/t/p/w300//z6wxnkqSTnzO1tcBui0ss7ehdm9.jpg",
  },
  {
    name: "Bill Nunn",

    image: "https://image.tmdb.org/t/p/w300//trxNvS6g5yvwbXzx2LK6JqutE5z.jpg",
  },
  {
    name: "John Leguizamo",

    image: "https://image.tmdb.org/t/p/w300//kwYCdxTlDh9zauUCg4mp2XTCQTw.jpg",
  },
  {
    name: "Charlize Theron",

    image: "https://image.tmdb.org/t/p/w300//9coVbqj35Fa5dWxlX5K9pDCqKfa.jpg",
  },
  {
    name: "K.K. Dodds",

    image: "https://image.tmdb.org/t/p/w300//nGou7wXgXAKr5oJEyUIuX9RCgFl.jpg",
  },
  {
    name: "Hugh Jackman",

    image: "https://image.tmdb.org/t/p/w300//4Xujtewxqt6aU0Y81tsS9gkjizk.jpg",
  },
  {
    name: "Sandra Hüller",

    image: "https://image.tmdb.org/t/p/w300//t7wGqC2dRwTEnt9Fk5pxQ0uXTqs.jpg",
  },
  {
    name: "Rena Owen",

    image: "https://image.tmdb.org/t/p/w300//648ZdDBmlx6OFDFRmgAbh6q5LBo.jpg",
  },
  {
    name: "Jared Leto",

    image: "https://image.tmdb.org/t/p/w300//ca3x0OfIKbJppZh8S1Alx3GfUZO.jpg",
  },
  {
    name: "Marlon Wayans",

    image: "https://image.tmdb.org/t/p/w300//7LYnX3vluHFBs1WCRKUjSIEDEkn.jpg",
  },
  {
    name: "Johnny Knoxville",

    image: "https://image.tmdb.org/t/p/w300//7XDKsHsLC4uNYaGsuWG1tQXWRnu.jpg",
  },
  {
    name: "Takahiro Sakurai",

    image: "https://image.tmdb.org/t/p/w300//8s8owcKmpRAuhzEGjSdRpztthUg.jpg",
  },
  {
    name: "Kenichi Suzumura",

    image: "https://image.tmdb.org/t/p/w300//vFqjmIjxfgBkh3ZmUin7QETV0sy.jpg",
  },
  {
    name: "Nia Long",

    image: "https://image.tmdb.org/t/p/w300//heVQkCGKUuKKRjRJaZIBrEKGhYA.jpg",
  },
  {
    name: "Tracie Thoms",

    image: "https://image.tmdb.org/t/p/w300//9PKUJJtqFRbZDhme6uDzIu0mRIE.jpg",
  },
  {
    name: "Kenneth Branagh",

    image: "https://image.tmdb.org/t/p/w300//AbCqqFxNi5w3nDUFdQt0DGMFh5H.jpg",
  },
  {
    name: "Robert Pattinson",

    image: "https://image.tmdb.org/t/p/w300//3qZ09UE7lN6AtorfXFRYpEtSY93.jpg",
  },
  {
    name: "Gwyneth Paltrow",

    image: "https://image.tmdb.org/t/p/w300//x040uB0CDrHjVAUSONw8bbWMvDC.jpg",
  },
  {
    name: "Mike Myers",

    image: "https://image.tmdb.org/t/p/w300//gjfDl52Kk02MPgUYFjs9bOy33OY.jpg",
  },
  {
    name: "Tim Allen",

    image: "https://image.tmdb.org/t/p/w300//woWhZzFILVhYMAvsPL171HjMY0y.jpg",
  },
  {
    name: "Justin Theroux",

    image: "https://image.tmdb.org/t/p/w300//vnI9L0rXBAw1HeC0Q8hJGeJMGAW.jpg",
  },
  {
    name: "Stanley Anderson",

    image: "https://image.tmdb.org/t/p/w300//6kHp1SlLRw0NobVdY8X6DbBAPt8.jpg",
  },
  {
    name: "Jon Favreau",

    image: "https://image.tmdb.org/t/p/w300//8MtRRnEHaBSw8Ztdl8saXiw1egP.jpg",
  },
  {
    name: "Vic Perrin",

    image: "https://image.tmdb.org/t/p/w300//mPUVJVcoGcO59gwPTxeIl76AAFZ.jpg",
  },
  {
    name: "Chris Evans",

    image: "https://image.tmdb.org/t/p/w300//3bOGNsHlrswhyW79uvIHH1V43JI.jpg",
  },
  {
    name: "James Franco",

    image: "https://image.tmdb.org/t/p/w300//bjmAntHGiibLZixH8nTNVBzaFQn.jpg",
  },
  {
    name: "Idris Elba",

    image: "https://image.tmdb.org/t/p/w300//be1bVF7qGX91a6c5WeRPs5pKXln.jpg",
  },
  {
    name: "James Purefoy",

    image: "https://image.tmdb.org/t/p/w300//nCNyifrD9wOao27A1nT5n4Soxt1.jpg",
  },
  {
    name: "Jon Abrahams",

    image: "https://image.tmdb.org/t/p/w300//xeCJMATz4Dko8e9yEifGGh0LW91.jpg",
  },
  {
    name: "Larenz Tate",

    image: "https://image.tmdb.org/t/p/w300//6BWajF1sbuwsvXHmdQDxP5MfZ5R.jpg",
  },
  {
    name: "Dwayne Johnson",

    image: "https://image.tmdb.org/t/p/w300//5QApZVV8FUFlVxQpIK3Ew6cqotq.jpg",
  },
  {
    name: "Rosemary Harris",

    image: "https://image.tmdb.org/t/p/w300//3Mu02enwaF8dFuT195f7esILXns.jpg",
  },
  {
    name: "J.K. Simmons",

    image: "https://image.tmdb.org/t/p/w300//ScmKoJ9eiSUOthAt1PDNLi8Fkw.jpg",
  },
  {
    name: "Cliff Robertson",

    image: "https://image.tmdb.org/t/p/w300//8pH2RWCPtXKzT9P33MbzgnzPlF0.jpg",
  },
  {
    name: "Seth Rogen",

    image: "https://image.tmdb.org/t/p/w300//nYl9bvQzaPQLzlf0wf75clLN6Hi.jpg",
  },
  {
    name: "Gerry Becker",

    image: "https://image.tmdb.org/t/p/w300//il1r1Iym5WFJyo8SQon60aqu4nC.jpg",
  },
  {
    name: "Jay Hernandez",

    image: "https://image.tmdb.org/t/p/w300//rh4pzTAJvy0i84HYV4xZxCXuXgV.jpg",
  },
  {
    name: "Jon Bernthal",

    image: "https://image.tmdb.org/t/p/w300//o0t6EVkJOrFAjESDilZUlf46IbQ.jpg",
  },
  {
    name: "Joe Manganiello",

    image: "https://image.tmdb.org/t/p/w300//mTdACmitdrwor0Nrv5sr0u123vZ.jpg",
  },
  {
    name: "Jack Betts",

    image: "https://image.tmdb.org/t/p/w300//aC8uOwxVW2NJ5sYUvQImFZuVKQ7.jpg",
  },
  {
    name: "Michael Papajohn",

    image: "https://image.tmdb.org/t/p/w300//hdTJBfIXfvRdlD1p8y6rra207UP.jpg",
  },
  {
    name: "Ebon Moss-Bachrach",

    image: "https://image.tmdb.org/t/p/w300//xD8GVNayMpiTZxLfahy2DseYcQq.jpg",
  },
  {
    name: "Rafi Gavron",

    image: "https://image.tmdb.org/t/p/w300//avCWoO9fLwEhbT6cvu0TJcSj49g.jpg",
  },
  {
    name: "Paul Rudd",

    image: "https://image.tmdb.org/t/p/w300//6jtwNOLKy0LdsRAKwZqgYMAfd5n.jpg",
  },
  {
    name: "Dee Bradley Baker",

    image: "https://image.tmdb.org/t/p/w300//9oFnToDZWp0I484s7Ua1EzNQQ2m.jpg",
  },
  {
    name: "Dominic Keating",

    image: "https://image.tmdb.org/t/p/w300//eRe4BJz9fTztlRKikDRHhmss6ee.jpg",
  },
  {
    name: "Lori Alan",

    image: "https://image.tmdb.org/t/p/w300//cavATg4fX0zAl2MHwlt1IZei7EI.jpg",
  },
  {
    name: "Tony Hale",

    image: "https://image.tmdb.org/t/p/w300//3dEyZgTye0Ec17VGKp0mJ3aU6ty.jpg",
  },
  {
    name: "Katharine Isabelle",

    image: "https://image.tmdb.org/t/p/w300//c9db3Cq3BQhWXCmVthyGQBN1W95.jpg",
  },
  {
    name: "Christoph Waltz",

    image: "https://image.tmdb.org/t/p/w300//jMvLGCVXLaBqjRLf5olyvEucZob.jpg",
  },
  {
    name: "Elliot Page",

    image: "https://image.tmdb.org/t/p/w300//nXO8DE4biVXY4UDYP0NdIY1zvXS.jpg",
  },
  {
    name: "Andy Richter",

    image: "https://image.tmdb.org/t/p/w300//5Qr7N6TzC8cI0ULxDm6EC5GpZ4C.jpg",
  },
  {
    name: "Benedict Wong",

    image: "https://image.tmdb.org/t/p/w300//yYfLyrC2CE6vBWSJfkpuVKL2POM.jpg",
  },
  {
    name: "Ryan Gosling",

    image: "https://image.tmdb.org/t/p/w300//lyUyVARQKhGxaxy0FbPJCQRpiaW.jpg",
  },
  {
    name: "Phil LaMarr",

    image: "https://image.tmdb.org/t/p/w300//xP2kNlsmXtzXq0geutflO4K9mSE.jpg",
  },
  {
    name: "Tandi Wright",

    image: "https://image.tmdb.org/t/p/w300//zluVB3mzZVlv8D013OgeQLhzNEw.jpg",
  },
  {
    name: "Trey Parker",

    image: "https://image.tmdb.org/t/p/w300//3tJe8t90lrcQzXj9cRcpW9wmj2c.jpg",
  },
  {
    name: "Shawn Wayans",

    image: "https://image.tmdb.org/t/p/w300//Cat25uXhB680QLmvg9Tu16W563.jpg",
  },
  {
    name: "Dave Sheridan",

    image: "https://image.tmdb.org/t/p/w300//qJF27jpleZBQOikOuYL7rfh9CPM.jpg",
  },
  {
    name: "Regina Hall",

    image: "https://image.tmdb.org/t/p/w300//jiFZ4xNrvUUZLBHnJu71CvdN4kj.jpg",
  },
  {
    name: "Andrew Garfield",

    image: "https://image.tmdb.org/t/p/w300//beO5YvbTjrr5yy8hW26KVDMSr35.jpg",
  },
  {
    name: "David Krumholtz",

    image: "https://image.tmdb.org/t/p/w300//2vaimzfyPQVxZGHbQS5M3z3tZw0.jpg",
  },
  {
    name: "Channing Tatum",

    image: "https://image.tmdb.org/t/p/w300//4TpgnS6l8YUXSne9Av9nda6mjxY.jpg",
  },
  {
    name: "Hayley Atwell",

    image: "https://image.tmdb.org/t/p/w300//x57wXHexIjD2ywly9cRA4rov7cu.jpg",
  },
  {
    name: "Kristen Wiig",

    image: "https://image.tmdb.org/t/p/w300//6U6UGztBwk7c4lg8n5BS5QOByot.jpg",
  },
  {
    name: "Jesse Eisenberg",

    image: "https://image.tmdb.org/t/p/w300//yYhwWRcxDHTn63gSEF1vnDAD7cD.jpg",
  },
  {
    name: "Erica Gavin",

    image: "https://image.tmdb.org/t/p/w300//yDtSboeTrLWxMIvsFxMq1gmy9lR.jpg",
  },
  {
    name: "Garth Pillsbury",

    image: "https://image.tmdb.org/t/p/w300//wBU5PylmxPbW9hpSfGNVrefV7x4.jpg",
  },
  {
    name: "Harrison Page",

    image: "https://image.tmdb.org/t/p/w300//uqoZdiO6ar3hVeX09u9JARVvfkA.jpg",
  },
  {
    name: "Vincene Wallace",

    image: "https://image.tmdb.org/t/p/w300//eIQcoZEkWGaS6yMoCkND6ZELgyJ.jpg",
  },
  {
    name: "Michael Donovan O'Donnell",

    image: "https://image.tmdb.org/t/p/w300//a5BsjXPmlyurUc6pCxwLxmXDDK7.jpg",
  },
  {
    name: "Mark Duplass",

    image: "https://image.tmdb.org/t/p/w300//lRDf99rAfcdqt8Cqk4LsIT7XSD2.jpg",
  },
  {
    name: "Wagner Moura",

    image: "https://image.tmdb.org/t/p/w300//yJjV1ZCQbCSSgRy05FncCKjyaY4.jpg",
  },
  {
    name: "Anthony Mackie",

    image: "https://image.tmdb.org/t/p/w300//eZSIDrtTzhvabyjrmIITQLsjx8h.jpg",
  },
  {
    name: "Aunjanue Ellis-Taylor",

    image: "https://image.tmdb.org/t/p/w300//e54DfgDyS67xdtomYODFule6Ofa.jpg",
  },
  {
    name: "Morena Baccarin",

    image: "https://image.tmdb.org/t/p/w300//4gyHyg6FJ1oFczOm5pmMkdEEo2J.jpg",
  },
  {
    name: "Taika Waititi",

    image: "https://image.tmdb.org/t/p/w300//ww6L2ksfJNMbuiIdDuvVKndUHsv.jpg",
  },
  {
    name: "Jemaine Clement",

    image: "https://image.tmdb.org/t/p/w300//6eiNbeurpHb2fxIeT0RrJ0wRI25.jpg",
  },
  {
    name: "Jeff Tremaine",

    image: "https://image.tmdb.org/t/p/w300//3R8FemTgvyY1vLn9r0lYlL45P3Q.jpg",
  },
  {
    name: "Dimitry Elyashkevich",

    image: "https://image.tmdb.org/t/p/w300//1vdt84lHs3ZoYmyYSQeF22NNCh5.jpg",
  },
  {
    name: "Bam Margera",

    image: "https://image.tmdb.org/t/p/w300//4UiQ7FKhrwo0lUxBPPLZQSL2ZUb.jpg",
  },
  {
    name: "Steve-O",

    image: "https://image.tmdb.org/t/p/w300//At4V4KPVOoBvWMtlZy6m4Wyvo3K.jpg",
  },
  {
    name: "Chris Pontius",

    image: "https://image.tmdb.org/t/p/w300//fNthW6ozHId6veZmZO6dC1ofZhI.jpg",
  },
  {
    name: "Lochlyn Munro",

    image: "https://image.tmdb.org/t/p/w300//bOJ03k0oe2R6snRgzV8M4Qtoo4O.jpg",
  },
  {
    name: "Ewen Leslie",

    image: "https://image.tmdb.org/t/p/w300//mANm2VxOszTc4bkzBsxZpAB056c.jpg",
  },
  {
    name: "Olivia Wilde",

    image: "https://image.tmdb.org/t/p/w300//eODi1QKamyVa41eSK2SjU20VAZS.jpg",
  },
  {
    name: "Liza Colón-Zayas",

    image: "https://image.tmdb.org/t/p/w300//qBb5eYEoZAlStrRXvsFlKukKJG8.jpg",
  },
  {
    name: "François Chau",

    image: "https://image.tmdb.org/t/p/w300//fRKnZpGoUBJAEx9gEOkN1bG21D8.jpg",
  },
  {
    name: "Lance Bangs",

    image: "https://image.tmdb.org/t/p/w300//sqGj6TVrE3qj4bRJU8rfsqcXwJk.jpg",
  },
  {
    name: "Aaron Hendry",

    image: "https://image.tmdb.org/t/p/w300//lOA25S5b1fkBL6fnG3u33mFtTdE.jpg",
  },
  {
    name: "Michael Hurst",

    image: "https://image.tmdb.org/t/p/w300//tBYX46fSi8jtp196U8L7Vd37KZW.jpg",
  },
  {
    name: "Gianna Jun",

    image: "https://image.tmdb.org/t/p/w300//8emMAWOMaoGKAJCQkqmApNhdS1K.jpg",
  },
  {
    name: "Craig Robinson",

    image: "https://image.tmdb.org/t/p/w300//mTyTrOWUSOBJMOlDpnd4OYx7FlJ.jpg",
  },
  {
    name: "Sahatchai Chumrum",

    image: "https://image.tmdb.org/t/p/w300//pVtfY9QvuOXNdytPo7u6fyO7VZe.jpg",
  },
  {
    name: "Philip Granger",

    image: "https://image.tmdb.org/t/p/w300//bkrnrtSiUnumWVpAOy5QDcOePiA.jpg",
  },
  {
    name: "Keith David",

    image: "https://image.tmdb.org/t/p/w300//1x0QXavmIy3QmhyrOsV7gMnJaEx.jpg",
  },
  {
    name: "Tyne Daly",

    image: "https://image.tmdb.org/t/p/w300//uR0nx2bniih1Kdhst5R3LH59smk.jpg",
  },
  {
    name: "Atanas Srebrev",

    image: "https://image.tmdb.org/t/p/w300//aW9keIvjMkH9YAMQDEyJayAs7oy.jpg",
  },
  {
    name: "Nancy Baldwin",

    image: "https://image.tmdb.org/t/p/w300//kGF3vUek0UpQjUfGonuPq1HRyBv.jpg",
  },
  {
    name: "Benedict Cumberbatch",

    image: "https://image.tmdb.org/t/p/w300//wz3MRiMmoz6b5X3oSzMRC9nLxY1.jpg",
  },
  {
    name: "Bokeem Woodbine",

    image: "https://image.tmdb.org/t/p/w300//pnPyA5pJn94zUzuLWTNkGBNZxza.jpg",
  },
  {
    name: "Krista Kosonen",

    image: "https://image.tmdb.org/t/p/w300//7iW2mxbxBgV5sX2y0fSY78lewW4.jpg",
  },
  {
    name: "Matthias Schoenaerts",

    image: "https://image.tmdb.org/t/p/w300//9mRssCj4si6f6IF3nJ2RB5w0J7g.jpg",
  },
  {
    name: "Chris Hemsworth",

    image: "https://image.tmdb.org/t/p/w300//piQGdoIQOF3C1EI5cbYZLAW1gfj.jpg",
  },
  {
    name: "Edwina Wren",

    image: "https://image.tmdb.org/t/p/w300//iMfeQvttnthKT1H1LKOKNoAZHMS.jpg",
  },
  {
    name: "Mia Wasikowska",

    image: "https://image.tmdb.org/t/p/w300//xOYlAZsLwFZ0gNHLnt1Hzuo2yqN.jpg",
  },
  {
    name: "Preston Lacy",

    image: "https://image.tmdb.org/t/p/w300//rJN0hgmardnLgQxmArsCPyDMSIf.jpg",
  },
  {
    name: "Freida Pinto",

    image: "https://image.tmdb.org/t/p/w300//tub18dfDNMxlempVIuPXh8fmZVs.jpg",
  },
  {
    name: "Elyes Gabel",

    image: "https://image.tmdb.org/t/p/w300//z9IqYTYxhVR9ADxaksbPQwiYQns.jpg",
  },
  {
    name: "Jason 'Wee Man' Acuña",

    image: "https://image.tmdb.org/t/p/w300//TYKzbOeNATeI1t4c51K7wp3Ny3.jpg",
  },
  {
    name: "Shiloh Fernandez",

    image: "https://image.tmdb.org/t/p/w300//lS3I6nLQ3VxmE6KRmjoXDskDBnn.jpg",
  },
  {
    name: "Nicholas Hope",

    image: "https://image.tmdb.org/t/p/w300//jwl6mahxUgnLvaCfmKiQLN9M2f6.jpg",
  },
  {
    name: "Conan O'Brien",

    image: "https://image.tmdb.org/t/p/w300//deRbViPut0t80miscBpP2DhBJU5.jpg",
  },
  {
    name: "Akira Ishida",

    image: "https://image.tmdb.org/t/p/w300//jnW2Gn2NlR2uwOCeyOuzypnTmkH.jpg",
  },
  {
    name: "Peter New",

    image: "https://image.tmdb.org/t/p/w300//bT71ieZH2qhgxLNTPyorMlVp3PN.jpg",
  },
  {
    name: "Xie Miao",

    image: "https://image.tmdb.org/t/p/w300//xdXmp28jBrv0r9FwLEKYTC36dnM.jpg",
  },
  {
    name: "Jarreth J. Merz",

    image: "https://image.tmdb.org/t/p/w300//ze8WDEujZmTXdCLcUy0IaymwlcS.jpg",
  },
  {
    name: "Go Soo",

    image: "https://image.tmdb.org/t/p/w300//77209yRtUHOEk3ILJY1rG3X3VUj.jpg",
  },
  {
    name: "Dionne Quan",

    image: "https://image.tmdb.org/t/p/w300//vGe0bMTfYDX1Wcw9ZCfY3YCSgai.jpg",
  },
  {
    name: "Dave England",

    image: "https://image.tmdb.org/t/p/w300//32OIQ5UTMGVCjsWoa6FNfnDeLtE.jpg",
  },
  {
    name: "Alison Brie",

    image: "https://image.tmdb.org/t/p/w300//smqYVStfIHDYKTu8T1BA2LnhdF9.jpg",
  },
  {
    name: "Eric Bauza",

    image: "https://image.tmdb.org/t/p/w300//afOlsVPQxbtkom604MeCemjlwEV.jpg",
  },
  {
    name: "Toshihiko Seki",

    image: "https://image.tmdb.org/t/p/w300//7jUPvx4hxWZWZJgyiCwd8KxWuvI.jpg",
  },
  {
    name: "Tom Hiddleston",

    image: "https://image.tmdb.org/t/p/w300//mclHxMm8aPlCPKptP67257F5GPo.jpg",
  },
  {
    name: "Colman Domingo",

    image: "https://image.tmdb.org/t/p/w300//t0cgfvtA7Thxnrtj5baRi656zlc.jpg",
  },
  {
    name: "Mamoru Miyano",

    image: "https://image.tmdb.org/t/p/w300//nuok8ueG7k9hPZ09Tpr8e7Qn0ah.jpg",
  },
  {
    name: "Tanapol Chuksrida",

    image: "https://image.tmdb.org/t/p/w300//mQiStBnFgzTyTJcWilqhUs5VoBu.jpg",
  },
  {
    name: "Kim Wayans",

    image: "https://image.tmdb.org/t/p/w300//iLcbmO6GkY0Calo1cM0PwiBR5K1.jpg",
  },
  {
    name: "B.J. Novak",

    image: "https://image.tmdb.org/t/p/w300//o8Xf1khxhKGTz3QAplXD35opRA8.jpg",
  },
  {
    name: "Vic Chao",

    image: "https://image.tmdb.org/t/p/w300//pPI9e95fOcLR3nGsIIRZsDokHJT.jpg",
  },
  {
    name: "Kristen Schaal",

    image: "https://image.tmdb.org/t/p/w300//s3LSHVTx8gxHP2twYsXEGa8JbLl.jpg",
  },
  {
    name: "Charlotte Riley",

    image: "https://image.tmdb.org/t/p/w300//cTZ8RhEk67OtOa4yosJLA4l5v5r.jpg",
  },
  {
    name: "Jason Momoa",

    image: "https://image.tmdb.org/t/p/w300//3troAR6QbSb6nUFMDu61YCCWLKa.jpg",
  },
  {
    name: "Kana Hanazawa",

    image: "https://image.tmdb.org/t/p/w300//9UTBlNRopSOKyoWnCm74tyHOfR1.jpg",
  },
  {
    name: "Hiro Shimono",

    image: "https://image.tmdb.org/t/p/w300//yrSDcgFefHtWkFmLnTrcw2t0MV.jpg",
  },
  {
    name: "Donald Glover",

    image: "https://image.tmdb.org/t/p/w300//jqVkQfeeEmdga1G0jpBwwXXwwSK.jpg",
  },
  {
    name: "Pierre Coffin",

    image: "https://image.tmdb.org/t/p/w300//eAA9uWRqHlm1LT3nZfXb7UuPfVb.jpg",
  },
  {
    name: "Ron Perkins",

    image: "https://image.tmdb.org/t/p/w300//ld3OJ0WpMv0Rcml59WuVzKoQWaB.jpg",
  },
  {
    name: "Elizabeth Marvel",

    image: "https://image.tmdb.org/t/p/w300//zJaDO2YI0XHeUDB5onVSgAVrvg2.jpg",
  },
  {
    name: "Emily Beecham",

    image: "https://image.tmdb.org/t/p/w300//cwBsTcY0FLnVYvRUY6cTutZsfPs.jpg",
  },
  {
    name: "JeeJa Yanin",

    image: "https://image.tmdb.org/t/p/w300//73xhAKFbeNSlJTwUCsQCAtryLYS.jpg",
  },
  {
    name: "Bill Skarsgård",

    image: "https://image.tmdb.org/t/p/w300//xBXLx1m0uzhXIbY3wN8lmPGeUHl.jpg",
  },
  {
    name: "Yayan Ruhian",

    image: "https://image.tmdb.org/t/p/w300//y8ZLh6FZMuLFw8PhqDxcEsyozjJ.jpg",
  },
  {
    name: "Arden Cho",

    image: "https://image.tmdb.org/t/p/w300//uXXUOCkqhfijNZYgmzjnfs7jQMw.jpg",
  },
  {
    name: "Tomokazu Sugita",

    image: "https://image.tmdb.org/t/p/w300//cv5zuPZySNsHXu24pjKLYCRzJ2J.jpg",
  },
  {
    name: "Erroll Shand",

    image: "https://image.tmdb.org/t/p/w300//75nc5lUcp1So9RTNNr08NZ0oQDG.jpg",
  },
  {
    name: "Murray Bartlett",

    image: "https://image.tmdb.org/t/p/w300//eN20zfcRB2F51bmUbTK9byQCpb9.jpg",
  },
  {
    name: "Jeremy Shamos",

    image: "https://image.tmdb.org/t/p/w300//4nEMfWgNBdBhzdzNGCOHyW941EC.jpg",
  },
  {
    name: "Anne Gee Byrd",

    image: "https://image.tmdb.org/t/p/w300//yNqUNepmVQ462yoTnZOWVbudKeG.jpg",
  },
  {
    name: "Emily Kuroda",

    image: "https://image.tmdb.org/t/p/w300//yaZhAxSsElEaX0az2U6FUBN38ET.jpg",
  },
  {
    name: "Barbara Eve Harris",

    image: "https://image.tmdb.org/t/p/w300//1WcoqAQn7KN0MadAWA5xebyt9bd.jpg",
  },
  {
    name: "Nigel Vonas",

    image: "https://image.tmdb.org/t/p/w300//hhFFUHGXU8kSDjvJZPEVFCVsagz.jpg",
  },
  {
    name: "John Tui",

    image: "https://image.tmdb.org/t/p/w300//2jIc9M5kl2GmK8fZtbtUr2s1jkS.jpg",
  },
  {
    name: "Shelby Rabara",

    image: "https://image.tmdb.org/t/p/w300//ouMiEKlhCj2LFKEQtI6fGRMDemF.jpg",
  },
  {
    name: "Avan Jogia",

    image: "https://image.tmdb.org/t/p/w300//7BX0Lg39bHlgtvWZpszTr1YjTAW.jpg",
  },
  {
    name: "Steven Yeun",

    image: "https://image.tmdb.org/t/p/w300//fOMFO2Xx4duzpNgS9Q5ytO44yGb.jpg",
  },
  {
    name: "Deon Cole",

    image: "https://image.tmdb.org/t/p/w300//3QTNxodSEL6HLDuv7Ub3vQzcNij.jpg",
  },
  {
    name: "Ehren McGhehey",

    image: "https://image.tmdb.org/t/p/w300//4q6TuSoHj5kk1AuJaI1TYLob2yd.jpg",
  },
  {
    name: "Peta Sergeant",

    image: "https://image.tmdb.org/t/p/w300//owkSN29t7P6hpazZKsUMt7FybrP.jpg",
  },
  {
    name: "Patrick Brammall",

    image: "https://image.tmdb.org/t/p/w300//l4IlGBxkYcCwqtfuKfWyHa9JCn6.jpg",
  },
  {
    name: "Kathryn Newton",

    image: "https://image.tmdb.org/t/p/w300//26OEa0uS8552sVJakaCEHYvqOao.jpg",
  },
  {
    name: "Saori Hayami",

    image: "https://image.tmdb.org/t/p/w300//gLv9lO7dlUbIsmyJUvgegqAAXki.jpg",
  },
  {
    name: "Andrey Kazakov",

    image: "https://image.tmdb.org/t/p/w300//ozbnnYZvapJdCwFQHr6mr5I8NCR.jpg",
  },
  {
    name: "Jóhannes Haukur Jóhannesson",

    image: "https://image.tmdb.org/t/p/w300//oqZftP0WS1rD5NFpR7vLp6JU52I.jpg",
  },
  {
    name: "Laurent Maurel",

    image: "https://image.tmdb.org/t/p/w300//vEbQJ8VNUyUwy10okhZ8mXjsg9o.jpg",
  },
  {
    name: "Bobby Moynihan",

    image: "https://image.tmdb.org/t/p/w300//gOvwNz5joi5yWJ7dAhuF8WA2aas.jpg",
  },
  {
    name: "Arian Moayed",

    image: "https://image.tmdb.org/t/p/w300//hYBqwt0HelUqgKSd1VDUAyvfDME.jpg",
  },
  {
    name: "Hannibal Buress",

    image: "https://image.tmdb.org/t/p/w300//mcw3Orbg4vbALXTVG4hriZjH1sj.jpg",
  },
  {
    name: "Zendaya",

    image: "https://image.tmdb.org/t/p/w300//3WdOloHpjtjL96uVOhFRRCcYSwq.jpg",
  },
  {
    name: "Henry Lloyd-Hughes",

    image: "https://image.tmdb.org/t/p/w300//m8zSYF7KmQS0x0o7wnWSuWLc0LW.jpg",
  },
  {
    name: "Sahajak Boonthanakit",

    image: "https://image.tmdb.org/t/p/w300//a36MI02S0f11bJZjBoxkZTsUDAK.jpg",
  },
  {
    name: "Dave Bautista",

    image: "https://image.tmdb.org/t/p/w300//snk6JiXOOoRjPtHU5VMoy6qbd32.jpg",
  },
  {
    name: "Vanessa Kirby",

    image: "https://image.tmdb.org/t/p/w300//tViEEsjvbhrJxWsOipUqIYjdHEb.jpg",
  },
  {
    name: "Evgeniya Loza",

    image: "https://image.tmdb.org/t/p/w300//ruLILWf9x0gxg8o9W9Yj9LCI0us.jpg",
  },
  {
    name: "Kim Jae-rok",

    image: "https://image.tmdb.org/t/p/w300//esGLG5p5tlnP7VNFSRyertWeMVS.jpg",
  },
  {
    name: "Joe Taslim",

    image: "https://image.tmdb.org/t/p/w300//bk2j6Oa8T02KtvpD4qOXXGJe4Vy.jpg",
  },
  {
    name: "Michael Mando",

    image: "https://image.tmdb.org/t/p/w300//gvM2wG66bjEpiirdeQdyG9EzUfv.jpg",
  },
  {
    name: "Scott Eastwood",

    image: "https://image.tmdb.org/t/p/w300//tKJEy1bFNxvIj7sivsB7Us2ThmY.jpg",
  },
  {
    name: "Eve Hewson",

    image: "https://image.tmdb.org/t/p/w300//1OlMfQrXEujIPTBUWt3AM0J0hJa.jpg",
  },
  {
    name: "Kwame Patterson",

    image: "https://image.tmdb.org/t/p/w300//dZUTl38nwCoWFgIAjxxixpKsw0s.jpg",
  },
  {
    name: "Wyatt Russell",

    image: "https://image.tmdb.org/t/p/w300//zIldBzXdBWskPZB7x35G2hYEVDo.jpg",
  },
  {
    name: "Miles Teller",

    image: "https://image.tmdb.org/t/p/w300//kDf3sW3USjEBDQ3Ua7lbwOfwty6.jpg",
  },
  {
    name: "Sydney Park",

    image: "https://image.tmdb.org/t/p/w300//6Gr0aT1Hux2P3KM35IXdXbXZABk.jpg",
  },
  {
    name: "Marcus Jean Pirae",

    image: "https://image.tmdb.org/t/p/w300//4XD78iRQJpx0uTQRg98Dfzgyb69.jpg",
  },
  {
    name: "Niko Nicotera",

    image: "https://image.tmdb.org/t/p/w300//ffSAiwXMmvQP1vJuPguLznNpPOI.jpg",
  },
  {
    name: "Isara Nadee",

    image: "https://image.tmdb.org/t/p/w300//rma7CNjw8zH2ohS3KqbAaUB3Pey.jpg",
  },
  {
    name: "Lorne MacFadyen",

    image: "https://image.tmdb.org/t/p/w300//y1cdclslL2nNkFw5RVj64iAHIIb.jpg",
  },
  {
    name: "Zoey Deutch",

    image: "https://image.tmdb.org/t/p/w300//csmSAm3vpJj9lB5vb5fTbRqOw9C.jpg",
  },
  {
    name: "Gavin Casalegno",

    image: "https://image.tmdb.org/t/p/w300//snYCYm31OniXr024HUcomunYpjV.jpg",
  },
  {
    name: "Saichia Wongwirot",

    image: "https://image.tmdb.org/t/p/w300//wl0ui6Ngg6Vo3C7oCF48bh6DQmo.jpg",
  },
  {
    name: "Koo Kyo-hwan",

    image: "https://image.tmdb.org/t/p/w300//aFwCpSKPGFqoiTStY6ljXnD5CwH.jpg",
  },
  {
    name: "Maisy Stella",

    image: "https://image.tmdb.org/t/p/w300//dlnP0Yzbr9EEbVg6M0PQVQ7pwUF.jpg",
  },
  {
    name: "Tom Holland",

    image: "https://image.tmdb.org/t/p/w300//xKBAaPIa1c7tzZD3Y0MhBLv4hPE.jpg",
  },
  {
    name: "Corey Hawkins",

    image: "https://image.tmdb.org/t/p/w300//wRt0Bc0chN5BSjvYzmg2evgqNzp.jpg",
  },
  {
    name: "Chaiwat Thongsaeng",

    image: "https://image.tmdb.org/t/p/w300//SIsrlRnL6hU7P2QILxbyUGVeJB.jpg",
  },
  {
    name: "Tibor Feldman",

    image: "https://image.tmdb.org/t/p/w300//b6tVKhCkNWltObwyHpGfBLuaPt6.jpg",
  },
  {
    name: "Nadech Kugimiya",

    image: "https://image.tmdb.org/t/p/w300//w58HrktpHZ7UVGY5SKoJxFkitCy.jpg",
  },
  {
    name: "Josh O'Connor",

    image: "https://image.tmdb.org/t/p/w300//pVv0zCvpNyuWoJcABq8RrM90FsK.jpg",
  },
  {
    name: "Ferdinand Kingsley",

    image: "https://image.tmdb.org/t/p/w300//arGWhGhfBl8CvNuUoKkUmfrDG0b.jpg",
  },
  {
    name: "P.J. Byrne",

    image: "https://image.tmdb.org/t/p/w300//1JVBaxRy3OxkHymD3nwBd5kVWWL.jpg",
  },
  {
    name: "Hettienne Park",

    image: "https://image.tmdb.org/t/p/w300//c5fZeiKZ6magmPWbLJkSjKm5oUX.jpg",
  },
  {
    name: "John Hopkins",

    image: "https://image.tmdb.org/t/p/w300//uHlU7Noyk784MXMwwPRa0Gl02aZ.jpg",
  },
  {
    name: "Jessica Sula",

    image: "https://image.tmdb.org/t/p/w300//2lLET7EhRy98ad5Mgc9jMk4Sb0S.jpg",
  },
  {
    name: "Yuichi Nakamura",

    image: "https://image.tmdb.org/t/p/w300//wb8behVKjBHX9XXrEydvNINCYwH.jpg",
  },
  {
    name: "Himesh Patel",

    image: "https://image.tmdb.org/t/p/w300//4mfOQgnwmewNURfQApyyHxt1qUF.jpg",
  },
  {
    name: "Sarah Hayward",

    image: "https://image.tmdb.org/t/p/w300//qJUXnOYb9pAyjf8h0Jjp4gbY2w9.jpg",
  },
  {
    name: "Yoshimasa Hosoya",

    image: "https://image.tmdb.org/t/p/w300//lUR5oN1LrqGgp25IOcI1qOH1Ud5.jpg",
  },
  {
    name: "Pedro Pascal",

    image: "https://image.tmdb.org/t/p/w300//oKcMbVn0NJTNzQt0ClKKvVXkm60.jpg",
  },
  {
    name: "Ji Chang-wook",

    image: "https://image.tmdb.org/t/p/w300//sBmHrO5Tn27Ot5hy0yAKniROmNb.jpg",
  },
  {
    name: "Bethany Anne Lind",

    image: "https://image.tmdb.org/t/p/w300//d0FjeKbWYhbTzFSimCnC9K1Maza.jpg",
  },
  {
    name: "Shin Hyun-been",

    image: "https://image.tmdb.org/t/p/w300//lU0yMFh5KgOEWZRThLYbB6ZI1hE.jpg",
  },
  {
    name: "Natsuki Hanae",

    image: "https://image.tmdb.org/t/p/w300//alTb0DlcPIbcwM08WSmxFai58sd.jpg",
  },
  {
    name: "Greta Lee",

    image: "https://image.tmdb.org/t/p/w300//6SydTis4XUcovlwIGskT59JowLX.jpg",
  },
  {
    name: "Angourie Rice",

    image: "https://image.tmdb.org/t/p/w300//iHZzyhvIYW9CSsMSLnxm9FsJohL.jpg",
  },
  {
    name: "Tony Revolori",

    image: "https://image.tmdb.org/t/p/w300//tSF6XmXDikrKZbFUeoDnafXxKjT.jpg",
  },
  {
    name: "Jade Croot",

    image: "https://image.tmdb.org/t/p/w300//j4DuuFPDdnEcDWFWCyBUuhhHkxg.jpg",
  },
  {
    name: "Tapiwa Soropa",

    image: "https://image.tmdb.org/t/p/w300//uJzIrzQzt50PAx74MpJ1Tzqes2g.jpg",
  },
  {
    name: "Kengo Kawanishi",

    image: "https://image.tmdb.org/t/p/w300//s0CxrO6LixJBgqiHEUjKelAh8ei.jpg",
  },
  {
    name: "Diarmaid Murtagh",

    image: "https://image.tmdb.org/t/p/w300//QHWy0m8t0JrxSwlxcWZrawfUdi.jpg",
  },
  {
    name: "Manatsanun Panlertwongskul",

    image: "https://image.tmdb.org/t/p/w300//zj09X1mHTrRNiyGmKpbTjJyierc.jpg",
  },
  {
    name: "Darin Toonder",

    image: "https://image.tmdb.org/t/p/w300//bD7Y9T7Y6XwDXjGJ8Suabyt1ZGo.jpg",
  },
  {
    name: "Emily Piggford",

    image: "https://image.tmdb.org/t/p/w300//ruijvaNNXzGXqGphQXsoHts1Ovc.jpg",
  },
  {
    name: "Taylor John Smith",

    image: "https://image.tmdb.org/t/p/w300//6dXZRf8ePa1oxQN9a5hFGEjDoI0.jpg",
  },
  {
    name: "Florence Pugh",

    image: "https://image.tmdb.org/t/p/w300//1Uvfh7xL4U2evkhs0M3C7BbBYFf.jpg",
  },
  {
    name: "Jodie Comer",

    image: "https://image.tmdb.org/t/p/w300//AfsBpnfw0E2h8NZK4zkFcOjYlEb.jpg",
  },
  {
    name: "Luciane Buchanan",

    image: "https://image.tmdb.org/t/p/w300//9fTzSU4310StDoO9T0nQyGOLurn.jpg",
  },
  {
    name: "Jonathan Yunger",

    image: "https://image.tmdb.org/t/p/w300//pnn5PwmyTJSBIjK2n8HPJn2GqtK.jpg",
  },
  {
    name: "Kadiff Kirwan",

    image: "https://image.tmdb.org/t/p/w300//dSCcuypWkQzspAQQq4AuejMzYfo.jpg",
  },
  {
    name: "Abraham Attah",

    image: "https://image.tmdb.org/t/p/w300//8r5GdE98NDuj8hMJktXtFWVtxxl.jpg",
  },
  {
    name: "Nicholas Galitzine",

    image: "https://image.tmdb.org/t/p/w300//hG4rH7eBMs117746bBOd8fUa4PA.jpg",
  },
  {
    name: "Caroline Piette",

    image: "https://image.tmdb.org/t/p/w300//diNg3gD3q86vbk6Xbj3QdwtagKG.jpg",
  },
  {
    name: "Kendrick Sampson",

    image: "https://image.tmdb.org/t/p/w300//AoZGTujNZEfabaXR5kUwmDUmxfe.jpg",
  },
  {
    name: "Natalie Moon",

    image: "https://image.tmdb.org/t/p/w300//cumEKvK8Nnw4wzJdrnyPoxfBL40.jpg",
  },
  {
    name: "Reina Ueda",

    image: "https://image.tmdb.org/t/p/w300//2WV61uVU7y6XGYqNHLMpP0sApdu.jpg",
  },
  {
    name: "Lana Condor",

    image: "https://image.tmdb.org/t/p/w300//vWn27Fk2GLwH7o9fBG9hBWZI6OR.jpg",
  },
  {
    name: "Rachel Bloom",

    image: "https://image.tmdb.org/t/p/w300//hfnU70X7QOdiURFEd1hM7gQIe6J.jpg",
  },
  {
    name: "Milana Vayntrub",

    image: "https://image.tmdb.org/t/p/w300//i5Cou9ExwTZvRRtl79V75CsI7oC.jpg",
  },
  {
    name: "Alfie Stewart",

    image: "https://image.tmdb.org/t/p/w300//2SJ7NrKZ4SDUvBMARPVozFN4Xx0.jpg",
  },
  {
    name: "Jord Knotter",

    image: "https://image.tmdb.org/t/p/w300//f0ufZ6YbyWnu36kFg1d2OOjcM2S.jpg",
  },
  {
    name: "Hudson Meek",

    image: "https://image.tmdb.org/t/p/w300//29S5mIoO18vNbOs0qrLC2XRVNlV.jpg",
  },
  {
    name: "Simu Liu",

    image: "https://image.tmdb.org/t/p/w300//cCx2OghLj9KN73oyRZe92i2p3Ih.jpg",
  },
  {
    name: "Olivia Rose Keegan",

    image: "https://image.tmdb.org/t/p/w300//9obcJcki6P3eGdXZ8mHnNKOVUIk.jpg",
  },
  {
    name: "Finn Bennett",

    image: "https://image.tmdb.org/t/p/w300//p4ya77nmLlhS2cyPKUZi9zVD4Mu.jpg",
  },
  {
    name: "Megan Lawless",

    image: "https://image.tmdb.org/t/p/w300//6qW63YEgB1qro01sM7T2HvhtFkh.jpg",
  },
  {
    name: "Tom Wilton",

    image: "https://image.tmdb.org/t/p/w300//9xq3MapsToSKewRkcnhiD9Z36ZZ.jpg",
  },
  {
    name: "Jonathan Ohye",

    image: "https://image.tmdb.org/t/p/w300//gaFjRHZaVPdtbr1N8NwCwQTPnXm.jpg",
  },
  {
    name: "JP Karliak",

    image: "https://image.tmdb.org/t/p/w300//7Rc3n8KmKUaztqfsIpddO1a2ggn.jpg",
  },
  {
    name: "David Serafin",

    image: "https://image.tmdb.org/t/p/w300//5BASMuxMtQnfOwiZVn09Cpt4mKY.jpg",
  },
  {
    name: "Michael Johnston",

    image: "https://image.tmdb.org/t/p/w300//fbpcCkBzu43kMdlXxEAMuLhseL8.jpg",
  },
  {
    name: "Pavel Kuzmin",

    image: "https://image.tmdb.org/t/p/w300//uIIS1FLF4zMxR0Sj53a4wc9tGU6.jpg",
  },
  {
    name: "Renate Reinsve",

    image: "https://image.tmdb.org/t/p/w300//q0ljTd4fyFSJKxPgmcrKtdg3HKo.jpg",
  },
  {
    name: "Joseph David-Jones",

    image: "https://image.tmdb.org/t/p/w300//qwX7ciIV8jwssmIqHv1eyzzqEvh.jpg",
  },
  {
    name: "Noah Jupe",

    image: "https://image.tmdb.org/t/p/w300//cBhJisZrIsZzamiUCVjOZODcqOK.jpg",
  },
  {
    name: "Sadie Sink",

    image: "https://image.tmdb.org/t/p/w300//92FddzBfK50XOUbtwjqHPraoGHy.jpg",
  },
  {
    name: "Zach Holmes",

    image: "https://image.tmdb.org/t/p/w300//q5HxqgzpKIwhR9bKy51z5V0vlzE.jpg",
  },
  {
    name: "Laura Harrier",

    image: "https://image.tmdb.org/t/p/w300//hYS8z1DxP6jvsBNXMRALiSI3nhw.jpg",
  },
  {
    name: "Eric Nam",

    image: "https://image.tmdb.org/t/p/w300//gEH0b5q9tupL49dmUFkjm9dnxP2.jpg",
  },
  {
    name: "Małgorzata Klara",

    image: "https://image.tmdb.org/t/p/w300//aOpwXHLkh2KrctgTX7cD7Cs1QN0.jpg",
  },
  {
    name: "Andrea Tivadar",

    image: "https://image.tmdb.org/t/p/w300//iYtFxJGcrNmxYTq6rhivdFsotMx.jpg",
  },
  {
    name: "Jacob Batalon",

    image: "https://image.tmdb.org/t/p/w300//53YhaL4xw4Sb1ssoHkeSSBaO29c.jpg",
  },
  {
    name: "Brandon Wilson",

    image: "https://image.tmdb.org/t/p/w300//apeJ3clonOwQ5SOUGUOKCTw3KFJ.jpg",
  },
  {
    name: "Clara Rosager",

    image: "https://image.tmdb.org/t/p/w300//aHV34ftsFREjRNaQtG23mqJP2Ae.jpg",
  },
  {
    name: "Lynn",

    image: "https://image.tmdb.org/t/p/w300//eJ2NqgzpnzNbT6Nt9EpDfzqNeZM.jpg",
  },
  {
    name: "Sean Cliver",

    image: "https://image.tmdb.org/t/p/w300//1WTaR4pzHn8zBf3GdJonUJtJbXo.jpg",
  },
  {
    name: "Maude Davey",

    image: "https://image.tmdb.org/t/p/w300//gJsnWVfgcyFrTYPVvcJrvIjIH1k.jpg",
  },
  {
    name: "Elijah Ungvary",

    image: "https://image.tmdb.org/t/p/w300//fpuW3g10qPuJHrhI4Eu6lC3WsxZ.jpg",
  },
  {
    name: "Jessica Matten",

    image: "https://image.tmdb.org/t/p/w300//EQsPxsav8AZmaeT3mpU3Evilee.jpg",
  },
  {
    name: "Lukita Maxwell",

    image: "https://image.tmdb.org/t/p/w300//g5g6XxUAtSdPfsJrSoFkl7vH51d.jpg",
  },
  {
    name: "Myrom Kingery",

    image: "https://image.tmdb.org/t/p/w300//g5cGIzYnaNv9LILT0IGtXa9Uw9k.jpg",
  },
  {
    name: "Christian Convery",

    image: "https://image.tmdb.org/t/p/w300//tOR2tSrvHPVDltE4jBDEva3GNqY.jpg",
  },
  {
    name: "Camila Mendes",

    image: "https://image.tmdb.org/t/p/w300//pZAWRHdJtJlDcWuQHlgIwX12s02.jpg",
  },
  {
    name: "Gregg Wayans",

    image: "https://image.tmdb.org/t/p/w300//lNsrZObXzbBA7jzTCBI4BFwQYdV.jpg",
  },
  {
    name: "Mia Soteriou",

    image: "https://image.tmdb.org/t/p/w300//bU4Twfk4as290gPFouByj7Wjd2.jpg",
  },
  {
    name: "Frankie Adams",

    image: "https://image.tmdb.org/t/p/w300//aAUHUSf0lh3OBRoaiCRL9ep8lfL.jpg",
  },
  {
    name: "David Corenswet",

    image: "https://image.tmdb.org/t/p/w300//yoQxpUPt3le9zY4Sab3g2ANy4CE.jpg",
  },
  {
    name: "Geraldine Viswanathan",

    image: "https://image.tmdb.org/t/p/w300//mZ1dKqL2ymRipGEudzr8TQliB52.jpg",
  },
  {
    name: "Alice Hewkin",

    image: "https://image.tmdb.org/t/p/w300//Ab92Gh6ILQCaoniNeJHwds9za2Y.jpg",
  },
  {
    name: "Sam Puefua",

    image: "https://image.tmdb.org/t/p/w300//iDcFUBYJMMPf2TIynGHu6U23cWI.jpg",
  },
  {
    name: "Mykal-Michelle Harris",

    image: "https://image.tmdb.org/t/p/w300//4TvVJYo8gEks8cegfNoZogXJgp2.jpg",
  },
  {
    name: "Shannon Berry",

    image: "https://image.tmdb.org/t/p/w300//h5qo9DHCgLAgdlpCxeGLxKqcJRH.jpg",
  },
  {
    name: "Jordan Alexa Davis",

    image: "https://image.tmdb.org/t/p/w300//itz31u3sG9hAlwuFZ6FWz1KhY1R.jpg",
  },
  {
    name: "Hunter Doohan",

    image: "https://image.tmdb.org/t/p/w300//ihno5ut6ha8TaubQFgl5Ozco2K1.jpg",
  },
  {
    name: "Michael Akinsulire",

    image: "https://image.tmdb.org/t/p/w300//9bW2Cd86NjKX5qxkRzYOQbY0gNy.jpg",
  },
  {
    name: "Savannah Lee Nassif",

    image: "https://image.tmdb.org/t/p/w300//ciJxFnNoJrePA6gvWcEJrS6HXVB.jpg",
  },
  {
    name: "Grayson Thorne Kilpatrick",

    image: "https://image.tmdb.org/t/p/w300//17xq5SJh6GTr9a2nRmDhxH3TUZ7.jpg",
  },
  {
    name: "Geoffrey Lumb",

    image: "https://image.tmdb.org/t/p/w300//c2xgAokzGEVtDMzSy9Y4eusw2x0.jpg",
  },
  {
    name: "Jed Aukin",

    image: "https://image.tmdb.org/t/p/w300//rEogcMC1EsGS0dOuIJlSjQfoYHP.jpg",
  },
  {
    name: "Marvin Jones III",

    image: "https://image.tmdb.org/t/p/w300//nS4X8TP3idFsxDdV9UqTRbz585g.jpg",
  },
  {
    name: "Travis Scott",

    image: "https://image.tmdb.org/t/p/w300//eBdm7HQS6lLGg4BocgfoJ5qB9yp.jpg",
  },
  {
    name: "Tommi Rose",

    image: "https://image.tmdb.org/t/p/w300//ffwnOrSD0D35pRaYjpwsAZB2deQ.jpg",
  },
  {
    name: "Denitra Isler",

    image: "https://image.tmdb.org/t/p/w300//gIIubxZEMjZiz9mTtfAlpQETjaw.jpg",
  },
  {
    name: "KeiLyn Durrel Jones",

    image: "https://image.tmdb.org/t/p/w300//4wDO3Zt9KJg9CARCEgjuVdhWa8x.jpg",
  },
  {
    name: "Maksim Saprykin",

    image: "https://image.tmdb.org/t/p/w300//u6c2wEvtqhXHyYRqd2LXKsyHr5a.jpg",
  },
  {
    name: "Nico Hiraga",

    image: "https://image.tmdb.org/t/p/w300//yd1Hx3C7BTevy3zvyGXUwtwvSft.jpg",
  },
  {
    name: "Tramell Tillman",

    image: "https://image.tmdb.org/t/p/w300//bEA15zMnkcXlRroYjKrFUWiiK7y.jpg",
  },
  {
    name: "Thalissa Teixeira",

    image: "https://image.tmdb.org/t/p/w300//jugWj8dbgGak8R2kCpwRsrh4e7H.jpg",
  },
  {
    name: "Souheila Yacoub",

    image: "https://image.tmdb.org/t/p/w300//A233BHgXw0dzbeOpvHfJwL9gLy1.jpg",
  },
  {
    name: "Elijah Rowen",

    image: "https://image.tmdb.org/t/p/w300//9aa2UljPdCZlp7meRE4dIzeu4Ej.jpg",
  },
  {
    name: "Milly Alcock",

    image: "https://image.tmdb.org/t/p/w300//cpcNqIORhZlpie8U2wSInhv0Hjf.jpg",
  },
  {
    name: "Davon 'Jasper' Wilson",

    image: "https://image.tmdb.org/t/p/w300//d9BHn3eHUkBURW5TiQMfBu9mSbx.jpg",
  },
  {
    name: "Mel Powell",

    image: "https://image.tmdb.org/t/p/w300//qu7T2uxliIK6TbCp1JAVY5rWPLl.jpg",
  },
  {
    name: "Tommy Martinez",

    image: "https://image.tmdb.org/t/p/w300//hPPWLxCSoehYnDaOrC6O0HxXPjT.jpg",
  },
  {
    name: "James Ortiz",

    image: "https://image.tmdb.org/t/p/w300//unzBwVsoq8rAH8BAYlPR1zsWQ4E.jpg",
  },
  {
    name: "Joey Iwanaga",

    image: "https://image.tmdb.org/t/p/w300//iwZXKVNZioAZaej7wUbOeYx1uLd.jpg",
  },
  {
    name: "Lee Joong-ok",

    image: "https://image.tmdb.org/t/p/w300//fMkBpHRElwNatevfGinSv248bYg.jpg",
  },
  {
    name: "Julia Grace",

    image: "https://image.tmdb.org/t/p/w300//5Se5ierKrANFEsUneyqQA3zUVwn.jpg",
  },
  {
    name: "Simone Ashley",

    image: "https://image.tmdb.org/t/p/w300//uL7t73kzhrRLY9ilNze1dYhHfg3.jpg",
  },
  {
    name: "George Pullar",

    image: "https://image.tmdb.org/t/p/w300//lF86e5h2iMS8IaIuExv1p81mi7R.jpg",
  },
  {
    name: "Sornchai Chatwiriyachai",

    image: "https://image.tmdb.org/t/p/w300//fO8HeBRN3ox6tT9vaxbVcWJOpH.jpg",
  },
  {
    name: "Lionel Boyce",

    image: "https://image.tmdb.org/t/p/w300//hpIxX5nkfA3pWCW8rYkEUCSBVyS.jpg",
  },
  {
    name: "Jack McEvoy",

    image: "https://image.tmdb.org/t/p/w300//pmE0tQiPxQT5yI6DZlnnjd8m8nB.jpg",
  },
  {
    name: "Brian Le",

    image: "https://image.tmdb.org/t/p/w300//cdtKmPpmBrnVAiqFQVE4urxu0ko.jpg",
  },
  {
    name: "Sam C. Wilson",

    image: "https://image.tmdb.org/t/p/w300//riwTkODF3CH1bJZG6K7U3dTUHhb.jpg",
  },
  {
    name: "Calix Fraser",

    image: "https://image.tmdb.org/t/p/w300//ddtaOOITYnRQatdxdjsaxnYHHzQ.jpg",
  },
  {
    name: "Piyaphong Vongkumlao",

    image: "https://image.tmdb.org/t/p/w300//9dQ4fUC1L0Eq1Ufo63xsckB1T4B.jpg",
  },
  {
    name: "Eric Scanlan",

    image: "https://image.tmdb.org/t/p/w300//aNPlEfrnBZUP33clsQgLtupXXjU.jpg",
  },
  {
    name: "Emma Ho",

    image: "https://image.tmdb.org/t/p/w300//mUVxoj09thSFpHhHD6xzam7XyNm.jpg",
  },
  {
    name: "Jon Xue Zhang",

    image: "https://image.tmdb.org/t/p/w300//olCEAjH9KeWte5ei24ej8MJAkZz.jpg",
  },
  {
    name: "Choi Gwang-il",

    image: "https://image.tmdb.org/t/p/w300//6vkA8s4XDPwZBRzNhN9xSLzb3II.jpg",
  },
  {
    name: "Michael-Eoin Stanney",

    image: "https://image.tmdb.org/t/p/w300//ybgdmLiPWu0P9RH1lgGBR49d0Rt.jpg",
  },
  {
    name: "Davida McKenzie",

    image: "https://image.tmdb.org/t/p/w300//4quy82rUnOFDyEe8GWLUopSBd3s.jpg",
  },
  {
    name: "Hwang Jae-yeol",

    image: "https://image.tmdb.org/t/p/w300//cyEibdvqNRurcNC63gqDSvX1qzE.jpg",
  },
  {
    name: "Inde Navarrette",

    image: "https://image.tmdb.org/t/p/w300//8mYBaOximzwBgXOYRzbS6eUnoMX.jpg",
  },
  {
    name: "Llana Barron",

    image: "https://image.tmdb.org/t/p/w300//ynxZ9Bu8L9wUGygwkBaLbQu84ie.jpg",
  },
  {
    name: "Moses Goods",

    image: "https://image.tmdb.org/t/p/w300//vo9EoB4dB4gRAtDLX2q0fV7aQvf.jpg",
  },
  {
    name: "Oh Kyung-hwa",

    image: "https://image.tmdb.org/t/p/w300//iOHLq24nLRnrsgdRtDxBw8xpOsr.jpg",
  },
  {
    name: "Christiaan Bettridge",

    image: "https://image.tmdb.org/t/p/w300//zttpdKvIWeQfQnxM4JRAwoX5NoE.jpg",
  },
  {
    name: "Rhyan Hill",

    image: "https://image.tmdb.org/t/p/w300//iPNBMJL9kKtUYmqJzbarQfQcPp4.jpg",
  },
  {
    name: "Román Zaragoza",

    image: "https://image.tmdb.org/t/p/w300//uWkOkSLqj2POMqkeLk5E5UJzebv.jpg",
  },
  {
    name: "Lewis Goody",

    image: "https://image.tmdb.org/t/p/w300//uvgQHSazVztGVWfvBb752lcGcnQ.jpg",
  },
  {
    name: "Caleb Hearon",

    image: "https://image.tmdb.org/t/p/w300//wsXM7aLnEQ9W5I9mi4k01hMuw0i.jpg",
  },
  {
    name: "Cooper Tomlinson",

    image: "https://image.tmdb.org/t/p/w300//vBMQbYT1DyWPCUp11dIiqZR9zhd.jpg",
  },
  {
    name: "Margarita Dyachenkova",

    image: "https://image.tmdb.org/t/p/w300//a3c2BqkxkZBlJNKFtZTjxZUiLvZ.jpg",
  },
  {
    name: "Chae Seo-eun",

    image: "https://image.tmdb.org/t/p/w300//gwEVDlcIO5kGrKbhyDcTiL95zaO.jpg",
  },
  {
    name: "Angelo Kern",
  },
  {
    name: "Heather Agyepong",

    image: "https://image.tmdb.org/t/p/w300//pDeJfGeKlVY5ts8ARN3Pp5IoGIa.jpg",
  },
  {
    name: "Yang Enyou",

    image: "https://image.tmdb.org/t/p/w300//dlQ4aZ418hBtDkEP58qMKBjRf0a.jpg",
  },
  {
    name: "Nattawat Sumploy",

    image: "https://image.tmdb.org/t/p/w300//agxM7KGg4DWvBPb5Ap5old8jcVH.jpg",
  },
  {
    name: "Trey Horton",

    image: "https://image.tmdb.org/t/p/w300//bS5tAhkEYanomvhQURQTMnOjg79.jpg",
  },
  {
    name: "Gabby Beans",

    image: "https://image.tmdb.org/t/p/w300//3njrvTJttrEIVNv5DjoP1OlBP5D.jpg",
  },
  {
    name: "Cameron Scott Roberts",

    image: "https://image.tmdb.org/t/p/w300//uXSh5Zss83nUNcV28sC38WbWIkD.jpg",
  },
  {
    name: "Jakub Ormaniec",
  },
  {
    name: "Kim Shin-rock",

    image: "https://image.tmdb.org/t/p/w300//x4zo2mSbehnQY80PbuldAZ7ruGm.jpg",
  },
  {
    name: "Jeremy Blewitt",

    image: "https://image.tmdb.org/t/p/w300//xkMMkOVj1VrBN1KVMj7yKsBcGU5.jpg",
  },
  {
    name: "Victory Ndukwe",

    image: "https://image.tmdb.org/t/p/w300//fAIXv88uXtJp1dH5qoMpsZxn8w1.jpg",
  },
  {
    name: "Joe Bird",

    image: "https://image.tmdb.org/t/p/w300//qUzFtIQFW1wUJC76VqMSVGy9QNb.jpg",
  },
  {
    name: "Annelle Olaleye",

    image: "https://image.tmdb.org/t/p/w300//V0UqW30EQaHjGhwDnb6dT0AWmz.jpg",
  },
  {
    name: "Haley Fitzgerald",

    image: "https://image.tmdb.org/t/p/w300//xbKqZ5Epz0IaSCPLXDnByDACw2X.jpg",
  },
  {
    name: "Kim Jong-tae",

    image: "https://image.tmdb.org/t/p/w300//vf3R5QU7UCVEr3dOGt3bdFbypzu.jpg",
  },
  {
    name: "Bastian Antonio Fuentes",

    image: "https://image.tmdb.org/t/p/w300//u7SDAocZ7ZCMOCejLRZtZFdb2Bl.jpg",
  },
  {
    name: "Sean McInerney",

    image: "https://image.tmdb.org/t/p/w300//yzp1Xt43g3QYHLjw502PfYJMxtq.jpg",
  },
  {
    name: "Asher de Silva",

    image: "https://image.tmdb.org/t/p/w300//fQinMbIfOTCCqtAFuLwMqNoXzYw.jpg",
  },
  {
    name: "Olivia Booth-Ford",

    image: "https://image.tmdb.org/t/p/w300//nBerjq4BKbhxQeTgVKaD4zvmRKs.jpg",
  },
  {
    name: "Skip Howland",

    image: "https://image.tmdb.org/t/p/w300//mXgzTq5QFTcWDi5eJe2vFQnaZ47.jpg",
  },
  {
    name: "Alexander Prismotrov-Belov",

    image: "https://image.tmdb.org/t/p/w300//3KUCgPhwzUaOMLOWOueF4il2PjZ.jpg",
  },
  {
    name: "Stacy Clausen",

    image: "https://image.tmdb.org/t/p/w300//8zwuEGXi26Yg9FQ29MntI5tWRk2.jpg",
  },
  {
    name: "Noah Alexander Sosnowski",

    image: "https://image.tmdb.org/t/p/w300//v2xlpMiOqgKCXjRAvZQ3lVyY3JJ.jpg",
  },
  {
    name: "Kapeneta Te'o-Tafiti",

    image: "https://image.tmdb.org/t/p/w300//bLR7ydoZq6l7FAaUoXrm0LenyuV.jpg",
  },
  {
    name: "Timothy Blore",

    image: "https://image.tmdb.org/t/p/w300//zavAyjmb1yQ2yinNS84X74unAgu.jpg",
  },
  {
    name: "Lara Macgregor",

    image: "https://image.tmdb.org/t/p/w300//j9YK2pAWU2klaCZcRpYDmWEnS2o.jpg",
  },
  {
    name: "Priya Kansara",

    image: "https://image.tmdb.org/t/p/w300//zv9kpaQ8AVBfjI2LAAZV58NeVQg.jpg",
  },
  {
    name: "Justice",

    image: "https://image.tmdb.org/t/p/w300//YFxKf2HHJvyavQf54NO5wXFOJX.jpg",
  },
  {
    name: "Jonathan Holman",

    image: "https://image.tmdb.org/t/p/w300//bPzzGKQE5Q04h5yq7quQeFz9e3W.jpg",
  },
  {
    name: "Beau Thompson",

    image: "https://image.tmdb.org/t/p/w300//9mGiYt7w8YqsqM6TLoGqYV9SvFY.jpg",
  },
  {
    name: "Rachel Thurow",

    image: "https://image.tmdb.org/t/p/w300//cmc9sL0MfauEeeyRPmMFvNDePhK.jpg",
  },
  {
    name: "Malcolm Kelner",

    image: "https://image.tmdb.org/t/p/w300//xlojlX1dlu1OY0Vf2owDWv5PV3l.jpg",
  },
  {
    name: "Travis Jay",

    image: "https://image.tmdb.org/t/p/w300//uVSPp04qhtO01b3qYMJTXlRKJ8i.jpg",
  },
  {
    name: "Alice Brittain",

    image: "https://image.tmdb.org/t/p/w300//qOlSX2GtTU1Rhn5iYf4K0bqQJgu.jpg",
  },
  {
    name: "Shi Zekun",

    image: "https://image.tmdb.org/t/p/w300//1O3W2fPIfMTMo6cFpUnxvhivJje.jpg",
  },
  {
    name: "Artie Wilkinson-Hunt",

    image: "https://image.tmdb.org/t/p/w300//7AmNS1dxEB2TCsuovbjc83hJ5El.jpg",
  },
  {
    name: "Gabriel Barbosa",

    image: "https://image.tmdb.org/t/p/w300//tHWVcUd8tnoSiep76nI4rsquZY5.jpg",
  },
  {
    name: "Jaafar Jackson",

    image: "https://image.tmdb.org/t/p/w300//tVSzFjQxOrLOcvnzFto0772Q9Bw.jpg",
  },
  {
    name: "Tyallah Bullock",

    image: "https://image.tmdb.org/t/p/w300//ae680Po4jGoAS3fUebpYKfQvjvn.jpg",
  },
  {
    name: "Hazel Rogers",

    image: "https://image.tmdb.org/t/p/w300//4AvhvIIRyokLjGDyob34L8LNhhf.jpg",
  },
  {
    name: "Simms May",

    image: "https://image.tmdb.org/t/p/w300//jfOkBcfi07i4xl9ayjwcOZgerhe.jpg",
  },
  {
    name: "Anthony Pavone",

    image: "https://image.tmdb.org/t/p/w300//hxiqY9gHgwXGYnXSD11Wacm5Nmi.jpg",
  },
  {
    name: "Greta van den Brink",

    image: "https://image.tmdb.org/t/p/w300//offeicvD1BnhZ4S0yz3C3YwxWZS.jpg",
  },
  {
    name: "Harrison Luna",

    image: "https://image.tmdb.org/t/p/w300//4SapY7LbHDHdNPV46uRUOujQJ2n.jpg",
  },
  {
    name: "Pittaya Saechua",

    image: "https://image.tmdb.org/t/p/w300//kQXUQhTwHYIjXjea15xSpTLdaAC.jpg",
  },
  {
    name: "Eve Ridley",

    image: "https://image.tmdb.org/t/p/w300//vmEfK4PO6rUvTJnN8bbUZrSDgII.jpg",
  },
  {
    name: "Catherine Lagaʻaia",

    image: "https://image.tmdb.org/t/p/w300//2KRIRDwy1CtY7Bge3aqVZrORelc.jpg",
  },
  {
    name: "Tony Wood",

    image: "https://image.tmdb.org/t/p/w300//y6aYdxRT0rO7R3l0rdC4vTg0ZNI.jpg",
  },
  {
    name: "Riley Chung",

    image: "https://image.tmdb.org/t/p/w300//9yKaWeYlhnUmit4JN0H2YYgmR50.jpg",
  },
  {
    name: "Alexandra Tikhonova",

    image: "https://image.tmdb.org/t/p/w300//iZL6tYLqs5uhvqE4SM7GwJHxUz3.jpg",
  },
  {
    name: "Juliano Krue Valdi",

    image: "https://image.tmdb.org/t/p/w300//1rQYmcxdPD1IwHpa78DcfVEu1EN.jpg",
  },
  {
    name: "Morgan Flanagan",

    image: "https://image.tmdb.org/t/p/w300//5Qb68qsULMlUz83At9cHixtLkDN.jpg",
  },
  {
    name: "Ivan Trushin",

    image: "https://image.tmdb.org/t/p/w300//8DWSEUJnxlr7kTP0O8iOSnsqbcs.jpg",
  },
  {
    name: "Faith Delaney",

    image: "https://image.tmdb.org/t/p/w300//jIKjTzr4dCoybQgNhyREahh9Aym.jpg",
  },
  {
    name: "Alfie Lawless",

    image: "https://image.tmdb.org/t/p/w300//761sH9H88m86M3iUuxMJpLQ2GE2.jpg",
  },
  {
    name: "Camden Brooks",

    image: "https://image.tmdb.org/t/p/w300//ge9FgIjRERhyylebW2Np9YwkqqR.jpg",
  },
  {
    name: "Benny Zielke",

    image: "https://image.tmdb.org/t/p/w300//8ul4eT5RAGvVXcCbrkqNqdqvRHU.jpg",
  },
  {
    name: "Robert Bobroczkyi",

    image: "https://image.tmdb.org/t/p/w300//3SK7NuuBKR5PSJwsQ7HO4qHIM9C.jpg",
  },
  {
    name: "Ploypaphas Fonkaewsiwaporn",

    image: "https://image.tmdb.org/t/p/w300//cFsnlfJFtVXjAHt6W1vfRERWP2P.jpg",
  },
  {
    name: "Veronika Zhuravleva",

    image: "https://image.tmdb.org/t/p/w300//bvlry6DeiYkm0onAZhonlpl4J7.jpg",
  },
  {
    name: "Priyanka Kedia",

    image: "https://image.tmdb.org/t/p/w300//trd4RCbywJ76wW5RoZbhKkt3kiP.jpg",
  },
  {
    name: "Felicity Bown",

    image: "https://image.tmdb.org/t/p/w300//kGtogGoVCt9sfO34bLDRHbWP59U.jpg",
  },
  {
    name: "Sid Edwards",

    image: "https://image.tmdb.org/t/p/w300//wvUcpP28vu5gk6UMi5nIIXyqon4.jpg",
  },
  {
    name: "Ruby Snowber",

    image: "https://image.tmdb.org/t/p/w300//5DNdyn09THeVZNrSSWW1wDyuZJs.jpg",
  },
  {
    name: "Tayme Thapthimthong",

    image: "https://image.tmdb.org/t/p/w300//Aq9AHZ7wSRFbdPZnPOupAAAHUnV.jpg",
  },
  {
    name: "Helen J. Shen",

    image: "https://image.tmdb.org/t/p/w300//78gjC8UOesWs56pzlQgZllwTW8R.jpg",
  },
  {
    name: "Scarlett Spears",

    image: "https://image.tmdb.org/t/p/w300//h0loJ4v3wbUfUA6dxUYv7B6fET1.jpg",
  },
  {
    name: "Tabitha Smyth",

    image: "https://image.tmdb.org/t/p/w300//uREVbfIi4rD2W29zWbtzNqIhP4u.jpg",
  },
  {
    name: "Chloe Breen",

    image: "https://image.tmdb.org/t/p/w300//oKBTQeGcQBM5aHNHwAqVWPjDVfz.jpg",
  },
  {
    name: "Tawny Fontana",

    image: "https://image.tmdb.org/t/p/w300//dvfNvXksUuf3jGe5DNbY7b1oR5s.jpg",
  },
  {
    name: "Daniel Vegas",

    image: "https://image.tmdb.org/t/p/w300//t9AFZX1rpTt59npITX2GY3JgySd.jpg",
  },
  {
    name: "Anthony Casabianca",

    image: "https://image.tmdb.org/t/p/w300//xNJUKqKQhMeyZ74erQvETr3AqV0.jpg",
  },
  {
    name: "Anton Solomatin",

    image: "https://image.tmdb.org/t/p/w300//rsiqKjHvpQGmDvba4TMpe8Rt8hj.jpg",
  },

  {
    name: "Keanu Karim",

    image: "https://image.tmdb.org/t/p/w300//dobjH9GFqBRhYJejTUBMaHCqWmH.jpg",
  },
  {
    name: "Ember Ambrose",

    image: "https://image.tmdb.org/t/p/w300//w7h6l4ltuWaMYanGgzwyhAHGL2y.jpg",
  },

  {
    name: "Maya Eva Hosein",

    image: "https://image.tmdb.org/t/p/w300//lDHbsnyUoBzmtbMevNl4F2n8B8B.jpg",
  },
  {
    name: "Shyamal Singh",

    image: "https://image.tmdb.org/t/p/w300//fbK6qvArrDNuyA8ASSNS11HnUqf.jpg",
  },
  {
    name: "Hyu Motoki",

    image: "https://image.tmdb.org/t/p/w300//3FxLc8wr8yzovYHFcorlg7LzEEr.jpg",
  },
  {
    name: "Anna Mtungwazi",

    image: "https://image.tmdb.org/t/p/w300//p7Ay8MsvbT8KgFPUYWsAvhbb1BO.jpg",
  },
  {
    name: "Flynn Glazebrook",

    image: "https://image.tmdb.org/t/p/w300//40eKJXhv5A5mwVwY7otfFh70Of3.jpg",
  },
  {
    name: "Lee Dam-hee",

    image: "https://image.tmdb.org/t/p/w300//wGiJAT7w7r3VBZdTXClfEQZ9IOV.jpg",
  },
  {
    name: "Mario Valdez",

    image: "https://image.tmdb.org/t/p/w300//4CShBxkTX3OLYtHjuAkMbzgKI5y.jpg",
  },
  {
    name: "Audrey Anderson",

    image: "https://image.tmdb.org/t/p/w300//aXuOgHcxoeSmhd1qY60nCISUVW9.jpg",
  },
];

const movies: Prisma.MovieCreateInput[] = [
  {
    title: "Spider-Man: Brand New Day",
    releaseDate: "2026-07-29",
    description:
      "Fighting crime full-time as Spider-Man in a world that doesn't remember him—and the pressure of seeing his old friends move on without him—sparks a change in Peter Parker he may not have the power to control. But that transformation might also be the only thing that can stop a shocking new threat to the city and those he loves - a powerful villain no one can even see.",
    duration: 145,
    poster: "https://image.tmdb.org/t/p/w300//iPOn6DinuVyLY17YM9mKuPofV08.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//qeQJx07rK2xm8SD2sJxFKhE7gs0.jpg",
    voteAverage: 7.9,
    voteCount: 1831,
    budget: 225000000,
    tagline: "A brand new day starts now.",
  },
  {
    title: "The Odyssey",
    releaseDate: "2026-07-15",
    description:
      "Odysseus, the legendary King of Ithaca, embarks on a long and perilous journey home following the Trojan War. Throughout his voyage, he is forced to confront the whims of gods, mythological monsters, and trials that stretch both his cunning and his humanity to the breaking point.",
    duration: 173,
    poster: "https://image.tmdb.org/t/p/w300//5rhTDKUhPYvpdQIijFIs5VoWsON.jpg",
    backdrop: "https://image.tmdb.org/t/p/w500//RMXG8myu1aGlNUsRjtxzmpdMK0.jpg",
    voteAverage: 7.991,
    voteCount: 2841,
    budget: 250000000,
    tagline: "Defy the gods.",
  },
  {
    title: "Spider-Man: No Way Home",
    releaseDate: "2021-12-15",
    description:
      "Peter Parker is unmasked and no longer able to separate his normal life from the high-stakes of being a super-hero. When he asks for help from Doctor Strange the stakes become even more dangerous, forcing him to discover what it truly means to be Spider-Man.",
    duration: 148,
    poster: "https://image.tmdb.org/t/p/w300//1g0dhYtq4irTY1GPXvft6k4YLjm.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//uyrOU4BDm2kbVxFsMiDFIHDhc4d.jpg",
    voteAverage: 7.94,
    voteCount: 22711,
    budget: 200000000,
    tagline: "Enter the Multiverse.",
  },
  {
    title: "The Last House",
    releaseDate: "2026-08-06",
    description:
      "A family suddenly sealed inside their home must work together to survive against dwindling resources and the ominous threat keeping them trapped.",
    duration: 110,
    poster: "https://image.tmdb.org/t/p/w300//6JU7E8Vv2M11egkctWVOScxWR75.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//1RhfevWmWCVHtEqxWBEjPOC5KG1.jpg",
    voteAverage: 6.907,
    voteCount: 778,
    budget: 0,
    tagline: "How long can you survive?",
  },
  {
    title: "Minions & Monsters",
    releaseDate: "2026-06-24",
    description:
      "This is the rambunctious, ridiculous and totally true story of how the Minions conquered Hollywood, became movie stars, lost everything, unleashed monsters onto the world and then banded together to try and save the planet from the mayhem they had just created.",
    duration: 90,
    poster: "https://image.tmdb.org/t/p/w300//sO3O1szSYuXLwtkobU5TExQ6Wfa.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//kkcwhgSFd81QDlXo8ytrpHPQjhy.jpg",
    voteAverage: 7.5,
    voteCount: 689,
    budget: 85000000,
    tagline: "Hollywood has a monster problem.",
  },
  {
    title: "Colony",
    releaseDate: "2026-05-21",
    description:
      "Professor Se-jeong is thrust into a bloody nightmare when a rapidly mutating virus is released during a biotech conference causing authorities to seal the facility. Trapped inside with no escape, Se-jeong along with a small group of survivors must fight to stay alive while the infected undergo horrific transformations.",
    duration: 123,
    poster: "https://image.tmdb.org/t/p/w300//tN799oUR0f1gUKDYdMNrDaY7I51.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//84FEpVVbSKYvKXDZJDZXOKBxCEm.jpg",
    voteAverage: 8.114,
    voteCount: 519,
    budget: 0,
    tagline: "Survive the hive.",
  },
  {
    title: "Rage of Stars",
    releaseDate: "2026-08-06",
    description:
      "A story about a woman from the International Special Forces Unit, who has a premonition about the impending end of humanity.",
    duration: 0,
    poster: "https://image.tmdb.org/t/p/w300//oLld47ZT1I3iecM3OWhIphohQUJ.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//z7lZgL5tzefTfhyRtouThFhsuUS.jpg",
    voteAverage: 2.3,
    voteCount: 3,
    budget: 0,
    tagline: "",
  },
  {
    title: "Obsession",
    releaseDate: "2026-05-13",
    description:
      'After breaking the mysterious "One Wish Willow" to win his crush\'s heart, a hopeless romantic finds himself getting exactly what he asked for but soon discovers that some desires come at a dark, sinister price.',
    duration: 109,
    poster: "https://image.tmdb.org/t/p/w300//bRwnj8WEKBCvmfeUNOukJPwB43K.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//rZfmzpixLKLR3Hg2u0WgC7XLFl8.jpg",
    voteAverage: 8.227,
    voteCount: 4766,
    budget: 750000,
    tagline: "Be careful who you wish for…",
  },
  {
    title: "Toy Story 5",
    releaseDate: "2026-06-17",
    description:
      "When Bonnie receives a Lilypad tablet as a gift and becomes obsessed, Buzz, Woody, Jessie and the rest of the gang's jobs become exponentially harder when they have to go head to head with the all-new threat to playtime.",
    duration: 102,
    poster: "https://image.tmdb.org/t/p/w300//sfQtVlIHljToOwYjhe21KPGzZWK.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//8sSKdEmlmqF4kJUd28SqthXC4yZ.jpg",
    voteAverage: 7.4,
    voteCount: 832,
    budget: 250000000,
    tagline: "It's on.",
  },
  {
    title: "The Death of Robin Hood",
    releaseDate: "2026-06-18",
    description:
      "Grappling with his past after a life of crime and murder, Robin Hood finds himself gravely injured after a battle he thought would be his last. In the hands of a mysterious woman, he is offered a chance at salvation.",
    duration: 123,
    poster: "https://image.tmdb.org/t/p/w300//92Tsfx7SFafOqWsotvrlJbHyehd.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//lh3BDkmWJh998n4fQcHYcVi7dpm.jpg",
    voteAverage: 6.311,
    voteCount: 217,
    budget: 25000000,
    tagline: "He was no hero.",
  },
  {
    title: "The End of Oak Street",
    releaseDate: "2026-08-12",
    description:
      "After a mysterious cosmic event rips Oak Street from suburbia and transports their neighborhood to someplace unknown, the Platt family soon discovers that their very survival depends on them sticking together as they navigate their now unrecognizable surroundings.",
    duration: 99,
    poster: "https://image.tmdb.org/t/p/w300//fYXqpgPmHMphSF2W30GbTeJVIa5.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//b9q9VmbXDvJmTziRqkwdEmFdwhr.jpg",
    voteAverage: 6.801,
    voteCount: 176,
    budget: 80000000,
    tagline: "Where goes the neighborhood.",
  },
  {
    title: "Evil Dead Burn",
    releaseDate: "2026-07-07",
    description:
      "After her husband's abrupt death, Alice seeks solace with his remaining family — descendants of a leading researcher on demonic possession. As her in-laws transform one by one into creatures that feed on fear, she comes to discover that the vows she took in life survive even in death.",
    duration: 110,
    poster: "https://image.tmdb.org/t/p/w300//uRxrNXQWkHoENm3nwVOZDYSCx2F.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//o0jkkpcN81QqSl8DMLScBCXyUH9.jpg",
    voteAverage: 7.889,
    voteCount: 1135,
    budget: 20000000,
    tagline: "Every family has its demons.",
  },
  {
    title: "Disclosure Day",
    releaseDate: "2026-06-10",
    description:
      "A cybersecurity expert becomes a whistleblower after uncovering secrets about aliens, putting him on the run from a corporation. Meanwhile, a meteorologist experiencing strange phenomena joins forces with him to prove there's life beyond our understanding.",
    duration: 146,
    poster: "https://image.tmdb.org/t/p/w300//AnJ8IQJI23hNpYXVNaythu061Ru.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//flxau5Iu7bChQHsESqvGZ3FQRaI.jpg",
    voteAverage: 7.454,
    voteCount: 2841,
    budget: 115000000,
    tagline: "We deserve to know.",
  },
  {
    title: "Moana",
    releaseDate: "2026-07-08",
    description:
      "Teenage Moana answers the Ocean's call and, for the first time, voyages beyond the reef of her island of Motunui with infamous demigod Maui on an unforgettable journey to restore prosperity to her people.",
    duration: 115,
    poster: "https://image.tmdb.org/t/p/w300//zKVgiv5qHCvCLT4A2ymJi5QeXDH.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//c6BPbkO5Npt1OdwttAxCFo06wtH.jpg",
    voteAverage: 6.051,
    voteCount: 197,
    budget: 250000000,
    tagline: "The ocean chose her for a reason.",
  },
  {
    title: "Spider-Man: Homecoming",
    releaseDate: "2017-07-05",
    description:
      "Following the events of Captain America: Civil War, Peter Parker, with the help of his mentor Tony Stark, tries to balance his life as an ordinary high school student in Queens, New York City, with fighting crime as his superhero alter ego Spider-Man as a new threat, the Vulture, emerges.",
    duration: 133,
    poster: "https://image.tmdb.org/t/p/w300//c24sv2weTHPsmDa7jEMN0m2P3RT.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//fn4n6uOYcB6Uh89nbNPoU2w80RV.jpg",
    voteAverage: 7.333,
    voteCount: 23658,
    budget: 175000000,
    tagline: "Homework can wait. The city can't.",
  },
  {
    title: "Supergirl",
    releaseDate: "2026-06-24",
    description:
      "When an unexpected and ruthless adversary strikes too close to home, Kara Zor-El, aka Supergirl, reluctantly joins forces with an unlikely companion on an epic, interstellar journey of vengeance and justice.",
    duration: 108,
    poster: "https://image.tmdb.org/t/p/w300//1QCWdqzTfh2x9UylVpspIU6QTuM.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//54KIfdTEzOliHDKx0OkzYGqAICx.jpg",
    voteAverage: 6.741,
    voteCount: 1910,
    budget: 175000000,
    tagline: "Truth. Justice. Whatever.",
  },
  {
    title: "Backrooms",
    releaseDate: "2026-05-27",
    description:
      "A strange doorway appears in the basement of a furniture showroom.",
    duration: 111,
    poster: "https://image.tmdb.org/t/p/w300//rhGx6E3qRNMgj3i5su2oukNHwIQ.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//dqmMWNWfLnExDRpMtIMqI97GQFR.jpg",
    voteAverage: 7.096,
    voteCount: 2809,
    budget: 10000000,
    tagline: "See how far it goes.",
  },
  {
    title: "Scary Movie",
    releaseDate: "2026-06-03",
    description:
      "Twenty-six years after outrunning a suspiciously familiar masked killer, the Core Four are back in the killer's crosshairs and no horror movie IP is safe.",
    duration: 96,
    poster: "https://image.tmdb.org/t/p/w300//znHT8peERZRWG1ME3r0Db0EV8k8.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//xWBiXclrRmTggQHMRsIn84YHavs.jpg",
    voteAverage: 6.442,
    voteCount: 1648,
    budget: 30000000,
    tagline: "Every line will be crossed.",
  },
  {
    title: "The Devil's Mouth",
    releaseDate: "2026-07-29",
    description:
      "A group of college friends' Thailand adventure turns deadly when they become trapped in submerged caves with a dangerous predator. As oxygen runs low, past conflicts emerge in their desperate fight for survival.",
    duration: 106,
    poster: "https://image.tmdb.org/t/p/w300//lH8k9uCWYn2b2gsYleqYBDPbWa8.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//4wmvU2Px3C8v3qqyNpBmgJrWQEx.jpg",
    voteAverage: 6.608,
    voteCount: 633,
    budget: 0,
    tagline: "Paradise has an appetite.",
  },
  {
    title: "The Devil's Mouth",
    releaseDate: "2026-07-29",
    description:
      "A group of college friends' Thailand adventure turns deadly when they become trapped in submerged caves with a dangerous predator. As oxygen runs low, past conflicts emerge in their desperate fight for survival.",
    duration: 106,
    poster: "https://image.tmdb.org/t/p/w300//lH8k9uCWYn2b2gsYleqYBDPbWa8.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//4wmvU2Px3C8v3qqyNpBmgJrWQEx.jpg",
    voteAverage: 6.608,
    voteCount: 633,
    budget: 0,
    tagline: "Paradise has an appetite.",
  },
  {
    title: "The Invite",
    releaseDate: "2026-06-25",
    description:
      "Joe and Angela's marriage is on thin ice. When they invite their enigmatic upstairs neighbors for a dinner party, the night spirals into unexpected places. Have they reignited the spark or lit the match that burns it all down?",
    duration: 107,
    poster: "https://image.tmdb.org/t/p/w300//b7Dr8Chzse8VagexAporUu2RtLx.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//bs32Ds4L8VADGjBVasSK1ASU7OW.jpg",
    voteAverage: 7.405,
    voteCount: 253,
    budget: 6000000,
    tagline: "It'll be fun.",
  },
  {
    title: "Project Hail Mary",
    releaseDate: "2026-03-15",
    description:
      "Science teacher Ryland Grace wakes up on a spaceship light years from home with no recollection of who he is or how he got there. As his memory returns, he begins to uncover his mission: solve the riddle of the mysterious substance causing the sun to die out. He must call on his scientific knowledge and unorthodox ideas to save everything on Earth from extinction.",
    duration: 157,
    poster: "https://image.tmdb.org/t/p/w300//yihdXomYb5kTeSivtFndMy5iDmf.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//8Tfys3mDZVp4tNoH2ktm06a0Tau.jpg",
    voteAverage: 8.653,
    voteCount: 7110,
    budget: 200000000,
    tagline: "Believe in the Hail Mary.",
  },
  {
    title: "Avatar Aang: The Last Airbender",
    releaseDate: "2026-07-24",
    description:
      "Avatar Aang, the world's last Airbender, learns of an ancient power that could save his culture from extinction. With the help of his friends, he embarks on a global quest to find it before it falls into the wrong hands and threatens to upend the peace they sacrificed everything to achieve.",
    duration: 99,
    poster: "https://image.tmdb.org/t/p/w300//3sgnSfNT27Bx5O5ukr7B26mhEQq.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//ezbrL1dMymKQZw7mDEWa2ZTzN7d.jpg",
    voteAverage: 9.235,
    voteCount: 880,
    budget: 0,
    tagline: "The legacy reawakens.",
  },
  {
    title: "Spider-Man",
    releaseDate: "2002-05-01",
    description:
      "After being bitten by a genetically altered spider at Oscorp, nerdy but endearing high school student Peter Parker is endowed with amazing powers to become the superhero known as Spider-Man.",
    duration: 121,
    poster: "https://image.tmdb.org/t/p/w300//or6XJBVpcEbIkma0V9zshnbEtx4.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//zQ8AxTPiCiS5nnwXpwTBPBHSaa5.jpg",
    voteAverage: 7.344,
    voteCount: 21158,
    budget: 139000000,
    tagline: "Go for the ultimate spin.",
  },
  {
    title: "Masters of the Universe",
    releaseDate: "2026-06-03",
    description:
      "After being separated for 15 years, the Sword of Power leads Prince Adam back to Eternia, where he discovers his home shattered under the fiendish rule of Skeletor. To save his family and his world, Adam must join forces with his closest allies, Teela and Duncan/Man-At-Arms, and embrace his true destiny as He-Man — the most powerful man in the universe.",
    duration: 141,
    poster: "https://image.tmdb.org/t/p/w300//oRuyGUHdoaQxWP3SDfafGkStxTC.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//yQIdU11DYQQp0neGtGtGxbGfRer.jpg",
    voteAverage: 7.183,
    voteCount: 1843,
    budget: 200000000,
    tagline: "Legends aren't born, they're forged.",
  },
  {
    title: "Lucky Strike",
    releaseDate: "2026-06-26",
    description:
      "A wounded American soldier fights to survive behind enemy lines during WWII's Battle of the Bulge against the Germans, relying on his instinct, spy craft and a hand-radio to evade capture and find his way back to his unit.",
    duration: 102,
    poster: "https://image.tmdb.org/t/p/w300//7AEBdyGYXumXWmMFeynE8227KeZ.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//1ebY06Gc1eUZcCoO7IoD4tWCFxm.jpg",
    voteAverage: 7.333,
    voteCount: 30,
    budget: 10000000,
    tagline: "One man's fight to survive.",
  },
  {
    title: "The Odyssey",
    releaseDate: "2026-07-03",
    description:
      "Based on the Ancient Greek epic. After ten years of war, King Odysseus sets sail for Ithaca, eager to reunite with his beloved. But his journey home is far more treacherous than the battlefield, as he must face deadly monsters and vengeful gods to survive.",
    duration: 86,
    poster: "https://image.tmdb.org/t/p/w300//xBHCRB7zLIW41w8QskfwJhm32YF.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//rOIaKlAX0SeNC25dFbM0y1mFsH6.jpg",
    voteAverage: 4.064,
    voteCount: 195,
    budget: 0,
    tagline: "The most epic voyage of all time!",
  },
  {
    title: "Leviticus",
    releaseDate: "2026-06-17",
    description:
      "Two teenage boys must escape a violent entity that takes the form of the person they desire most:  each other.",
    duration: 88,
    poster: "https://image.tmdb.org/t/p/w300//gnAsZvBygplNpp8PtjoTEYv3VPB.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//7y8zWGEjs7tresw4Hzkkf4TdkcL.jpg",
    voteAverage: 6.882,
    voteCount: 351,
    budget: 3500000,
    tagline: "It will never stop.",
  },
  {
    title: "Your Heart Will Be Broken",
    releaseDate: "2026-03-26",
    description:
      "High school student Polina is saved from bullying at her new school and makes a deal with the main bully Bars: he must pretend to be her boyfriend and protect her, and she must do everything he says. During this game, the couple develops real feelings, but her family and classmates have reasons to separate the lovers.",
    duration: 134,
    poster: "https://image.tmdb.org/t/p/w300//7wIBfBl2gejt6xHxNSK0reVIm7E.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//1x9e0qWonw634NhIsRdvnneeqvN.jpg",
    voteAverage: 7.1,
    voteCount: 120,
    budget: 3716000,
    tagline: "",
  },
  {
    title: "Avengers: Doomsday",
    releaseDate: "2026-12-16",
    description:
      "Beloved heroes from three distinct universes are set on a deadly collision course and face an existential threat unlike anything they've ever encountered.",
    duration: 165,
    poster: "https://image.tmdb.org/t/p/w300//jzPwsojjFStf5lR5Nm07w2hH56G.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//s4v0UX1anfXm0UvloLsTTJ4v222.jpg",
    voteAverage: 0,
    voteCount: 0,
    budget: 0,
    tagline: "",
  },
  {
    title: "Demon Slayer: Kimetsu no Yaiba Infinity Castle",
    releaseDate: "2025-07-18",
    description:
      "The Demon Slayer Corps are drawn into the Infinity Castle, where Tanjiro, Nezuko, and the Hashira face terrifying Upper Rank demons in a desperate fight as the final battle against Muzan Kibutsuji begins.",
    duration: 156,
    poster: "https://image.tmdb.org/t/p/w300//fWVSwgjpT2D78VUh6X8UBd2rorW.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//1RgPyOhN4DRs225BGTlHJqCudII.jpg",
    voteAverage: 8.726,
    voteCount: 1862,
    budget: 20000000,
    tagline: "It's time to have some fun.",
  },
  {
    title: "The Furious",
    releaseDate: "2026-06-10",
    description:
      "After a criminal network kidnaps Wang Wei's daughter and the corrupt police refuse to assist him, Wei sets out on his own to locate her. Navin, a tenacious journalist whose wife has mysteriously vanished, is his only ally. In this explosive martial arts showdown, the unlikely duo fights the kidnappers ruthlessly driven by a furious vengeance.",
    duration: 114,
    poster: "https://image.tmdb.org/t/p/w300//zP19YO60jwEsfKd5Qf1UvA5uJu8.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//9XwQphZxNJgGASfjL58mhIkJJpf.jpg",
    voteAverage: 8,
    voteCount: 529,
    budget: 20000000,
    tagline: "To save their loved ones, they will fight everyone.",
  },
  {
    title: "Jackass: Best and Last",
    releaseDate: "2026-06-25",
    description:
      "The fifth and final installment to Jackass franchise where the crew go on one last insane crusade.",
    duration: 92,
    poster: "https://image.tmdb.org/t/p/w300//tfgccePxnswMqhmtxafliLlcCVR.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//dUbP1HNdI0aCq1zgRJw28PWSqmk.jpg",
    voteAverage: 7.575,
    voteCount: 154,
    budget: 10000000,
    tagline: "One. Last. Ride.",
  },
  {
    title: "Michael",
    releaseDate: "2026-04-22",
    description:
      "The story of Michael Jackson, one of the most influential artists the world has ever known, and his life beyond the music. His journey from the discovery of his extraordinary talent as the lead of the Jackson Five, to the visionary artist whose creative ambition fueled a relentless pursuit to become the biggest entertainer in the world, highlighting both his life off-stage and some of the most iconic performances from his early solo career.",
    duration: 128,
    poster: "https://image.tmdb.org/t/p/w300//2AvgnCwCakNiJqWZqaQhdGjWNey.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//ufSwlnECLoUbBjPrFqEQcWBzHwc.jpg",
    voteAverage: 8.677,
    voteCount: 4052,
    budget: 250000000,
    tagline: "Discover the making of a king.",
  },
  {
    title: "Borderline",
    releaseDate: "2026-07-17",
    description:
      "Convinced they are our secret saviors, rogue U.S. operatives abduct Mexican gang members and seed them across major American cities, unleashing criminal chaos, swaying voters by playing on nativist prejudices, and forcing law enforcement into a race to expose the plot as the escalating carnage threatens democracy itself.",
    duration: 94,
    poster: "https://image.tmdb.org/t/p/w300//sAA2mjpYJFAKydtzG4P4aHC4mZk.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//wPSOPYy2fV62GjeXpVdKyweZGZu.jpg",
    voteAverage: 5.536,
    voteCount: 14,
    budget: 0,
    tagline: "",
  },
  {
    title: "Vixen!",
    releaseDate: "1968-10-15",
    description:
      "In a Canadian mountain resort, Vixen Palmer resides with her naive pilot husband Tom. While he's away flying in tourists, she sleeps with practically everybody including a husband and his wife, and even her biker brother. However, the only one she won't bed is her brother's friend... who is Black.",
    duration: 72,
    poster: "https://image.tmdb.org/t/p/w300//9KMZWDA3xTrlgrScqdMisINQmsh.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//uLu8zcwmR7qjkkF3zkScm6Sjh2v.jpg",
    voteAverage: 5.486,
    voteCount: 108,
    budget: 73000,
    tagline: "Is she woman... or animal?",
  },
  {
    title: "The Debt Collector",
    releaseDate: "2026-07-20",
    description:
      "Tormented by past mistakes and a terminal diagnosis, an ex-debt collector returns to the underworld to protect the victims of a violent organization.",
    duration: 134,
    poster: "https://image.tmdb.org/t/p/w300//qADBUb5ybrkRVGZEFF8Z4RuOwys.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//dk68ykaNd3HdrDJyPJqEGsgvag7.jpg",
    voteAverage: 8,
    voteCount: 104,
    budget: 0,
    tagline: "A Debt Can Be Settled, But Not Erased.",
  },
  {
    title: "The Devil Wears Prada 2",
    releaseDate: "2026-04-29",
    description:
      "Andy Sachs returns to Runway as Miranda Priestly navigates a new media landscape and Runway's position within. The duo reconnect with former assistant Emily Charlton, now the head of a luxury brand that possesses funding which could ensure Runway's survival.",
    duration: 119,
    poster: "https://image.tmdb.org/t/p/w300//xTI42pmsP5EDnvsNJPEDubwWBQO.jpg",
    backdrop:
      "https://image.tmdb.org/t/p/w500//Af907x5h9W1wVis8XrSd7ynTWuy.jpg",
    voteAverage: 7.07,
    voteCount: 2094,
    budget: 100000000,
    tagline: "Icons reign forever.",
  },
];

const getRandomBetween = (min: number, max: number): number => {
  return Math.round(Math.random() * (max - min) + min);
};

export async function main() {
  await prisma.genre.deleteMany();
  await prisma.actorCharacter.deleteMany();
  await prisma.movie.deleteMany();
  await prisma.actor.deleteMany();

  const dbGenres = await prisma.genre.createManyAndReturn({ data: genresData });
  const dbActors = await prisma.actor.createManyAndReturn({ data: actors });

  for (const movie of movies) {
    const movieCopy = { ...movie };

    const randomGenres = [...dbGenres]
      .sort(() => Math.random() - 0.5)
      .slice(0, getRandomBetween(1, 3));

    const randomActors = [...dbActors]
      .sort(() => Math.random() - 0.5)
      .slice(0, getRandomBetween(10, 30));

    await prisma.movie.create({
      data: {
        ...movieCopy,
        releaseDate: new Date(movieCopy.releaseDate),
        genre: {
          connect: randomGenres.map((genre) => ({
            id: genre.id,
          })),
        },
        characters: {
          create: randomActors.map((actor) => ({
            name: faker.person.fullName(),
            image: actor.image,
            role: faker.person.jobType(),
            actor: {
              connect: {
                id: actor.id,
              },
            },
          })),
        },
      },
    });
  }
}

main();
