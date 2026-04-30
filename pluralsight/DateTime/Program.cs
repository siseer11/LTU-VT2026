DateTime myBirthDay = new DateTime(1993, 11, 21);
DayOfWeek myDay = myBirthDay.DayOfWeek;
bool wasDST = myBirthDay.IsDaylightSavingTime();

DateTime today = DateTime.Today;
DateTime tomorrow = today.AddDays(1);
tomorrow = tomorrow.AddHours(10).AddMinutes(20).AddSeconds(1);



DateTime date1 = new DateTime(1993, 11, 21);
DateTime date2 = new DateTime(1994, 11, 21);


bool isDate1AfterDate2 = date1.CompareTo(date2) == 1;

Console.WriteLine($"Important date: {tomorrow.ToLongDateString()} {tomorrow.ToLongTimeString()}!");


DateTime taskStart = new DateTime(2026, 04, 27, 10, 30, 0);
int daysToCompleteTask = 7;
DateTime taskDeadline = taskStart.AddDays(daysToCompleteTask);


DateTime dayStart = new DateTime(2026, 04, 27, 8, 0, 0);
TimeSpan courseDuration = new TimeSpan(9, 0, 0);
DateTime dayEndAt = dayStart.Add(courseDuration);

Console.WriteLine($"The course starts: {dayStart.ToLongDateString()} {dayStart.ToLongTimeString()} - {dayEndAt.ToLongTimeString()}");
