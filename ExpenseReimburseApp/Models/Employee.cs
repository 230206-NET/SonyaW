namespace Models;
using System.Collections.Generic;
using System.Threading;

public class Employee
{
    public string name { get; set; } = "Unknown";
    public string userPW { get; set; } = "No security";
    public string email { get; set; } = "Unknown";
    public string manager { get; set; } = "Unknown";
    public string managerName { get; set; } = "";
    public List<Expense> ? ListOfExpenses = new List<Expense>();

    public Employee(){}

    public Employee(string nameIn) {
        name = nameIn;
    }

    public Employee(string nameIn, string userPwIn, string emailIn, string managerIn) {
        name = nameIn;
        userPW = userPwIn;
        email = emailIn;
        manager = managerIn;
    }

    public Employee(string nameIn, string userPwIn, string emailIn, string managerIn, List<Expense> ListOfExpenses) {
        name = nameIn;
        userPW = userPwIn;
        email = emailIn;
        manager = managerIn;
        this.ListOfExpenses = ListOfExpenses;
    }

    public Employee(string nameIn, string userPwIn, string emailIn, string managerIn, string managerNameIn, List<Expense> ListOfExpenses) {
        name = nameIn;
        userPW = userPwIn;
        email = emailIn;
        manager = managerIn;
        managerName = managerNameIn;
        this.ListOfExpenses = ListOfExpenses;
    }

    public void ToString() {
        Console.WriteLine(@"
   ------------------------------
         ACCOUNT SUMMARY         
   ------------------------------
       Name:    {0}
       Email:   {1}
       Manager: {2}",
    this.name, this.email, this.managerName);
    }

    public void displayTickets() {
        Console.WriteLine(@"
 Row  |  Price |  Ticket Status | Description
---------------------------------------------------");
        int i = 1;
        foreach(Expense ticket in ListOfExpenses) {
            Console.Write("  {0}   |", i);
            ticket.ToString();
            i++;
            Thread.Sleep(150);
        }
        Console.WriteLine("---------------------------------------------------");
    }

    public Expense createTicket(int value, string description) {
        Expense newExpense = new Expense(value, description);
        ListOfExpenses.Add(newExpense);
        return newExpense;
    }

    public void deleteTicket(int input) {
        ListOfExpenses.RemoveAt(input);
    }

    // public bool AcceptTicket(Expense expenseToAccept) {
    //     if(ListOfExpenses.Contains(expenseToAccept)) {
    //         expenseToAccept.AcceptTicket();
    //         return true;
    //     } return false;
    // }

    // public bool RejectTicket(Expense expenseToAccept) {
    //     if(ListOfExpenses.Contains(expenseToAccept)) {
    //         expenseToAccept.RejectTicket();
    //         return true;
    //     } return false;
    // }

    public Expense GetExpense(int row) {
        if(row < 0 || row >= ListOfExpenses.Count()) return null;
        return ListOfExpenses[row];
    }

    public void EditProfile(string newName, string newPW) {
        name = newName;
        if(newPW != "") userPW = newPW;
    }

}
