namespace UnitTesting;

public class ManagerTest
{

    [Fact]
    public void ManagerDefaultConstructorSetsDefaultValues()
    {
        Manager mng = new Manager();

        Assert.Equal(mng.name, "Unknown");
        Assert.Equal(mng.userPW, "No security");
        Assert.Equal(mng.email, "Unknown");
    }

    [Fact]
    public void SpecificEmployeeRetrieved()
    {
        Manager mng = new Manager();
        List<Employee> emplList = new List<Employee>(){
            new Employee("Sally"),
            new Employee("Barry"),
            new Employee("Cody"),
        };
        mng.EmployeesToManage = emplList;

        Assert.Equal(mng.GetEmployee(0), emplList[0]);
        Assert.Equal(mng.GetEmployee(1), emplList[1]);
        Assert.Equal(mng.GetEmployee(2), emplList[2]);

    }

    [Fact]
    public void EmployeeSuccessfullyRemoved()
    {
        Manager mng = new Manager();
        List<Employee> emplList = new List<Employee>(){
            new Employee("Sally"),
            new Employee("Barry"),
            new Employee("Cody"),
        };
        mng.EmployeesToManage = emplList;

        Assert.Equal(mng.RemoveEmployee(2), true);
        Assert.Equal(mng.RemoveEmployee(2), false);
        Assert.Equal(mng.RemoveEmployee(1), true);
        Assert.Equal(mng.RemoveEmployee(0), true);
        Assert.Equal(mng.RemoveEmployee(0), false);

    }

    [Fact]
    public void SuccessfullyAcceptEmplTicket()
    {
        Manager mng = new Manager();
        
        Employee empl = new Employee();
        List<Expense> expensesList = new List<Expense>() {
            new Expense(50),
            new Expense(100),
            new Expense(150),
            new Expense(200)
        };
        empl.ListOfExpenses = expensesList;
        
        List<Employee> emplList = new List<Employee>(){
            new Employee("Sally"),
            new Employee("Barry"),
            new Employee("Cody"),
            empl
        };
        mng.EmployeesToManage = emplList;

        Assert.Equal(mng.AcceptEmployeeTicket(mng.EmployeesToManage[3], 3), true);
        Assert.Equal(mng.AcceptEmployeeTicket(mng.EmployeesToManage[0], 1), false);
       
    }

}