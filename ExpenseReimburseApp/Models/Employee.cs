namespace Models;
using System.Collections.Generic;
using System.Threading;

public class Employee
{
    public string name { get; set; } = "Unknown";
    public string userPW { get; set; } = "No security";
    public string email { get; set; } = "Unknown";
    public string manager { get; set; } = "Unknown";
    // public bool promote { get; set; } = false;
    public List<Expense> ? ListOfExpenses = new List<Expense>();

    public Employee(){}

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

    public void createTicket(int value, string description) {
        // Console.WriteLine("Inside createTicket");
        Expense newExpense = new Expense(value, description);
        // Console.WriteLine("Created instance of Expense");
        ListOfExpenses.Add(newExpense);
    }

    public void ToString() {
        Console.WriteLine(@"
   ------------------------------
         ACCOUNT SUMMARY         
   ------------------------------
       Name:    {0}
       Email:   {1}
       Manager: {2}",
    this.name, this.email, this.manager);
    }

    public void displayTickets() {
        Console.WriteLine(@"
 Row  |  Price |   Ticket Status  | Description
---------------------------------------------------");
        int i = 1;
        foreach(Expense ticket in ListOfExpenses) {
            Console.Write("  {0}   |", i);
            ticket.ToString();
            i++;
            Thread.Sleep(100);
        }
    }

    public void deleteTicket(int input) {
        ListOfExpenses.RemoveAt(input - 1);
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
        if(row <= 0 || row > ListOfExpenses.Count()) return null;
        return ListOfExpenses[row-1];
    }

}
