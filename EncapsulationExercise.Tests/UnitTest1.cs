namespace EncapsulationExercise.Tests;

public class PersonTests
{
	[Fact]
	public void IncreaseSalary_Increases_Sallary_Correctly_For_Over_30()
	{
		decimal salary = 1000;
		decimal bonus = 10;
		// Arrange
		Person Mary = new("Mary", "Mariana", 35, salary);
		// Act
		Mary.IncreaseSalary(bonus);
		// Assert
		decimal correctSalaryAfterIncrease = salary + (salary * (bonus / 100));
		Assert.Equal(correctSalaryAfterIncrease, Mary.Salary);
	}

	[Fact]
	public void IncreaseSalary_Increases_Sallary_Correctly_For_Under_30()
	{
		decimal salary = 1000;
		decimal bonus = 10;
		// Arrange
		Person John = new("John", "Jonny", 25, salary);
		// Act
		John.IncreaseSalary(bonus);
		// Assert
		bonus /= 2;
		decimal correctSalaryAfterIncrease = salary + (salary * (bonus / 100));
		Assert.Equal(correctSalaryAfterIncrease, John.Salary);
	}

	[Fact]
	public void ToString_Returns_Correct_String()
	{
		decimal salary = 1000;
		string firstName = "John";
		string lastName = "Jonny";

		// Arrange
		Person John = new(firstName, lastName, 25, salary);

		// Act
		string JohnDetails = John.ToString();

		// Assert
		Assert.Equal($"{firstName} {lastName} receives {salary} dollars.", JohnDetails);
	}
}
