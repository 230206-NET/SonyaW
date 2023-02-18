namespace Models;
using System.Collections.Generic;

public class Employee
{
    public string name { get; set; } = "";
    public string userPW { get; set; } = "";
    public string email { get; set; } = "";
    public string manager { get; set; } = "";
    // public bool promote { get; set; } = false;
    public List<Expense> ? ListOfExpenses = new List<Expense>();

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
  Price |   Ticket Status  | Description
-------------------------------------------");
        foreach(Expense ticket in ListOfExpenses) {
            ticket.ToString();
        }
    }

    public void deleteTicket(int input) {
        ListOfExpenses.RemoveAt(input - 1);
    }

}
