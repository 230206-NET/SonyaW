namespace UnitTesting;

public class EmployeeTest {

    [Fact]
    public void EmployeeDefaultConstructorSetsDefaultValues()
    {
        Employee empl = new Employee();
        Assert.Equal(empl.name, "Unknown");
        Assert.Equal(empl.userPW, "No security");
        Assert.Equal(empl.email, "Unknown");
        Assert.Equal(empl.manager, "Unknown");
        Assert.Equal(empl.managerName, "");
    }

    [Fact]
    public void SpecificExpenseRetrieved()
    {
        Employee empl = new Employee();
        List<Expense> expensesList = new List<Expense>() {
            new Expense(50),
            new Expense(100),
            new Expense(150),
            new Expense(200)
        };
        empl.ListOfExpenses = expensesList;

        Assert.Equal(empl.GetExpense(0), expensesList[0]);
        Assert.Equal(empl.GetExpense(1), expensesList[1]);
        Assert.Equal(empl.GetExpense(2), expensesList[2]);
    }
}