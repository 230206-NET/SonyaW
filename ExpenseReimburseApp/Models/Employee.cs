namespace Models;

public class Employee
{
    public string name { get; set; } = "";
    public string userPW { get; set; } = "";
    public string email { get; set; } = "";
    public string manager { get; set; } = "";
    public List<Expense> ? ListOfExpenses { get; set; }

    public Employee(string nameIn, string userPwIn, string emailIn, string managerIn) {
        name = nameIn;
        userPW = userPwIn;
        email = emailIn;
        manager = managerIn;
    }

}
