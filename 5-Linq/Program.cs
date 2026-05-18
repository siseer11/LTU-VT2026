
using _5_Linq;

List<Student> studenter =
[
	new Student { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 },
	new Student { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 },
	new Student { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 },
	new Student { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 },
	new Student { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 },
	new Student { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 },
	new Student { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 },
	new Student { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 }
];

void logStudentIEnumerable(string title, IEnumerable<Student> list)
{
	Console.WriteLine(title);
	Console.WriteLine("---");
	foreach (Student student in list)
	{
		Console.WriteLine($"{student.Namn} | Age: {student.Alder} | Klass: {student.Klass} | Points: {student.Poang}");
	}
	Console.WriteLine("===============\n");
}
;


#region Where

// Hämta alla studenter som är äldre än 20
IEnumerable<Student> overTwenty = studenter.Where(student => student.Alder > 20);
logStudentIEnumerable("Age over 20", overTwenty);

// Hämta alla studenter som har mer än 80 poäng.
var betygOver80 = studenter.Where(student => student.Poang > 80);
logStudentIEnumerable("Over 80 points", betygOver80);

#endregion

#region Select

// Visa bara namnen på alla studenter.
var namnPaStudenter = studenter.Select(student => student.Namn);

// Visa namn och poäng i en ny anonym typ.
var namnOchPoangPaStudenter = studenter.Select(student => new { namn = student.Namn, poang = student.Poang });

#endregion

#region OrderBy
// Sortera studenter efter ålder.
var studenterEfterAlder = studenter.OrderBy(student => student.Alder);
logStudentIEnumerable("Ordered by age, asc", studenterEfterAlder);

//Sortera studenter efter poäng, högst först.
var studenterEfterPoang = studenter.OrderByDescending(student => student.Poang);
logStudentIEnumerable("Ordered by points, desc", studenterEfterPoang);

#endregion

#region Count
// Räkna hur många studenter som finns.
var numberOfStudents = studenter.Count();
Console.WriteLine($"\nNumber of students: {numberOfStudents}\n");

// Räkna hur många som går i klass A
var numberOfStudentsInClassA = studenter.Count(studenter => studenter.Klass == "A");
Console.WriteLine($"\nNumber of students in class A: {numberOfStudentsInClassA}\n");

#endregion

#region Take
// Ta ut de tre första studenterna.
var first3Students = studenter.Take(3);

//Ta ut de två bästa poängen efter sortering.
var best2Points = studenter.OrderByDescending(student => student.Poang).Take(2).Select(student => student.Poang);

#endregion

#region Distinct
// Visa alla unika klassnamn.
var unikaKlassnamn = studenter.Select(student => student.Klass).Distinct();

// Visa alla unika åldrar.
var unikaAldrar = studenter.Select(student => student.Alder).Distinct();

#endregion

#region Any / All
// Finns det någon student med 100 poäng?
bool anyStudentWith100Points = studenter.Any(student => student.Poang == 1000);

// Har alla studenter minst 50 poäng?
bool allStundentsOver50Points = studenter.All(stundent => stundent.Poang > 50);

#endregion

#region FirstOrDefault / Last / Single
// Hämta första studenten i klass B.
var firstFromClassB = studenter.FirstOrDefault(student => student.Klass == "B");

// Hämta sista studenten i listan.
var lastStudent = studenter.Last();

// Hämta studenten med ett visst Id.
var studentWithId7 = studenter.Single(studenter => studenter.Id == 7);

#endregion

#region GroupBy
// Gruppera studenter per klass.
var studentsPerClass = studenter.GroupBy(student => student.Klass);

// Skriv ut varje klass och namnen i gruppen.
foreach (var studentClass in studentsPerClass)
{
	Console.WriteLine($"Class {studentClass.Key}");
	foreach (var student in studentClass)
	{
		Console.WriteLine($"\t{student.Namn}");
	}
}
#endregion

#region Sum / Average / Min / Max
// Summera alla poäng.
var sumOfAllPoints = studenter.Sum(student => student.Poang);

// Räkna ut medelpoäng.
var averageScore = studenter.Average(student => student.Poang);

// Hitta lägsta och högsta poäng.
var maxScore = studenter.Max(student => student.Poang);
var minScore = studenter.Min(student => student.Poang);

#endregion