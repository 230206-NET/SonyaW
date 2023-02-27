using System;
using System.Collections.Generic;
using System.Threading;

namespace Models;

public class Manager {

    public string name { get; set; } = "Unknown";
    public string userPW { get; set; } = "No security";
    public string email { get; set; } = "Unknown";
    public List<Employee> EmployeesToManage = new List<Employee>();

    public Manager() {}

    public Manager (string nameIn, string userPwIn, string emailIn) {
        name = nameIn;
        userPW = userPwIn;
        email = emailIn;
    }

    public Manager (string nameIn, string userPWIn, string emailIn, List<Employee> mngEmpl) {
        name = nameIn;
        userPW = userPWIn;
        email = emailIn;
        this.EmployeesToManage = mngEmpl;
    }

    public bool AcceptEmployeeTicket(Employee viewingEmpl, int row) { //Expense expenseToAccept) {
        if(viewingEmpl.ListOfExpenses.Count <= row || row < 0) return false;
        viewingEmpl.ListOfExpenses[row].AcceptTicket();
        return true;
    }

    public bool RejectEmployeeTicket(Employee viewingEmpl, int row) {
        if(viewingEmpl.ListOfExpenses.Count <= row || row < 0) return false;
        viewingEmpl.ListOfExpenses[row].RejectTicket();
        return true;
    }

    // public void PromoteEmployee(Employee promotingEmployee) {}

    // public void AddEmployee(Employee newEmployee) {
    //     EmployeesToManage.Add(newEmployee);
    // }

    public void ToString()
    {
        Console.WriteLine(@"
       ------------------------------
             ACCOUNT SUMMARY         
       ------------------------------
        Name:                {0}
        Email:               {1}
        Number of Employees: {2}",
       this.name, this.email, this.EmployeesToManage.Count);
    }

    public void DisplayAllEmployees() {
        Console.WriteLine(@"
        --------------------------------
           List of managing employees
        --------------------------------
 Row | Employee Name   | Pending | Accepted | Rejected 
----------------------------------------------------");
        int i = 1;
        foreach(Employee empl in EmployeesToManage) {
            int numPendingTix = empl.ListOfExpenses.Count(ticket => ticket.ticketStatus == 0);
            int numAcceptedTix = empl.ListOfExpenses.Count(ticket => ticket.ticketStatus == 1);
            int numRejectedTix = empl.ListOfExpenses.Count(ticket => ticket.ticketStatus == -1);
            Console.WriteLine($"  {i,-3}| {empl.name, -15} | {numPendingTix, -7} | {numAcceptedTix, -8} | {numRejectedTix, -10} ");
            i++;
            Thread.Sleep(100);
        }
        Console.WriteLine("----------------------------------------------------");
    }

    public Employee GetEmployee(int row) {
        if(row < 0 || row >= EmployeesToManage.Count()) return null;
        return EmployeesToManage[row];
    }

    public bool RemoveEmployee(int row) {
        if(row < 0 || row >= EmployeesToManage.Count()) return false;
        EmployeesToManage.RemoveAt(row);
        return true;
    }

    public void EditProfile(string newName, string newPW) {
        name = newName;
        if(newPW != "") userPW = newPW;
    }

}