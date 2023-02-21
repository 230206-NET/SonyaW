namespace UI;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Models;

/*
TO-DO:
  - create login function (putting credentials against DB)

*/

public class MainMenu {

    public static void Main(string[] args) {

        MainMenu start = new MainMenu();
        start.Start();

//*
        // Console.WriteLine("Initiating testers...");
        // List<Expense> testExpense1 = new List<Expense>() {
        //     new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        // };
        //  List<Expense> testExpense2 = new List<Expense>() {
        //     new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        // };
        //  List<Expense> testExpense3 = new List<Expense>() {
        //     new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        // };
        //  List<Expense> testExpense4 = new List<Expense>() {
        //     new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        // };
        //  List<Expense> testExpense5 = new List<Expense>() {
        //     new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        // };
        // Employee tester1 = new Employee("sonya wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense1);
        // Employee tester2 = new Employee("different wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense2);
        // Employee tester3 = new Employee("bob wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense3);
        // Employee tester4 = new Employee("joe wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense4);
        // Employee tester5 = new Employee("john wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense5);

        // List<Employee> mngEmpl = new List<Employee>() {tester1, tester2, tester3, tester4, tester5};
        // Manager newMang = new Manager("John Doe", "helloworld", "email@email.com", mngEmpl);
        
        // newMang.AcceptEmployeeTicket(newMang.GetEmployee(1).GetExpense(1)); // PASS IN AN EXPENSE OBJECT
        // newMang.RejectEmployeeTicket(newMang.GetEmployee(1).GetExpense(2)); // PASS IN AN EXPENSE OBJECT
        // Console.WriteLine("{0}", newMang.GetEmployee(1).GetExpense(1));
        // newMang.DisplayAllEmployees();
        // tester.ToString();
        // tester.displayTickets();
//*/
        
        // RunManager(newMang);

    }

    private void Start() {
        while(true) {
            string initRes = initPrompt().ToLower();
            switch (initRes) {
                case "1":
                    LogIn();
                break;
                case "2":
                    Employee newEmployee = CreateAcct();
                    if(newEmployee != null) {
                        RunEmployee(newEmployee);
                    }
                break;
                case "x":
                    Thread.Sleep(1000);
                    Console.WriteLine("Exiting application...");
                    Environment.Exit(0);  
                break;
                default:
                    Console.WriteLine("\nUnrecognized respnse. Please enter a valid value.");
                break;
            }

        }
    }

    private static string initPrompt(){
        Console.WriteLine(@"
    [1] Sign in
    [2] Create Account
    [X] Exit");
        string res = Console.ReadLine();
        return res;
    }
  
    public static bool promptExit() {
        Console.WriteLine("Are you sure you want to cancel?");
        return (Console.ReadLine().ToLower() == "yes") ? true : false;
    }
 
    private static bool LogIn(){
        Console.WriteLine("Signing in...\n     Enter email:");
        return true;
    }
  
    private static Employee CreateAcct() {      // RETURNS TRUE IF ABLE TO CREATE AN ACCOUNT
        Console.WriteLine("\nSetting up new account...");
        Thread.Sleep(1000);
        Console.Write(" Press 'X' anytime to exit.\n\n  Enter your manager's first and last name: ");

        // PROMPT FOR USER'S MANAGER
        string newEmpManager = "";
        while(true) {
            newEmpManager = Console.ReadLine();
            if(newEmpManager.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                return null;
            }
            newEmpManager = ValidateAndReturnName(newEmpManager);
            if(newEmpManager != "") break;
            else Console.Write("     ** You must have a manager to create an account. **\n     Manager name: ");
        }

        // RETRIEVE USER FULL NAME
        Console.Write("  Enter your first and last name: ");
        string newEmpName = "";
        while(true) {
            newEmpName = Console.ReadLine();
            if(newEmpName.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                return null;
            }
            newEmpName = ValidateAndReturnName(newEmpName);
            if(newEmpName != "") break;
            else Console.Write("     ** Please enter your first AND last name. **\n     Your full name: ");
        }

        // RETRIEVE EMAIL FROM USER
        Console.Write("  Enter your email: ");
        string email = "";
        while(true) {
            string tempString = Console.ReadLine();
            if(tempString.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                return null;
            }
            email = ValidateAndReturnEmail(tempString);
            if(email == "") Console.Write("     ** You have entered a short or invalid email. **\n     Email: ");
            else break;
        }

        // RETRIEVE PASSWORD FROM USER
        Console.Write("  Enter you password: ");
        StringBuilder tempPW = new StringBuilder();
        while(true) {
            var key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter) {
                if(tempPW.ToString().ToUpper() == "X") {
                    Console.WriteLine("Exiting...");
                    return null;
                }
                if(tempPW.Length > 5) {
                    Console.WriteLine("\n");
                    break;
                }
                else {
                    Console.Write("\n     ** Your password must have a length of at least 5. **\n     Password: ");
                    tempPW.Remove(0, tempPW.Length);
                    continue;
                }
            } else {
                if(key.Key == ConsoleKey.Backspace){
                    if(tempPW.Length > 0) {
                        tempPW.Remove(tempPW.Length - 1, 1);
                        continue;
                    }
                    Console.Write("-");
                }
                else {
                    tempPW.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
        }

        // FINALIZING CREATION OF ACCOUNT
        Console.WriteLine("Creating account...");
        Thread.Sleep(2000); // sleeps for 2 seconds
        Employee newEmp = new Employee(newEmpName, tempPW.ToString(), email, newEmpManager);
        newEmp.ToString();
        Console.Write("\n  ***** Welcome {0}! *****  \n", newEmp.name);

        return newEmp;
    }
  
    public static string ValidateAndReturnName (string tempInputName) {
        string namePattern = @"\w+ \w+";
        Match nameMatch = Regex.Match(tempInputName, namePattern);
        if(nameMatch.Value != "") {
            string tempName = nameMatch.Value;

            string tempFN = tempName.Substring(0, tempName.IndexOf(" "));
            string tempLN = tempName.Substring(tempName.IndexOf(" ") + 1);
            string finalName = tempFN.Substring(0, 1).ToUpper() + tempFN.Substring(1) + " " + tempLN.Substring(0, 1).ToUpper() + tempLN.Substring(1);
            return finalName;
        }
        return "";
    }
 
    public static string ValidateAndReturnEmail(string tempString){
        string pattern = @"(\w{3,10})@\w+\.(\w{3})";
        Match m = Regex.Match(tempString, pattern);
        if(m.Value != "") return m.Value;
        return "";
    }
  
    private static int RunEmployee(Employee empl) {
        while(true) {
             Console.WriteLine(@"
````````````````````````````````````````````````
What action would you like to take?
    [1]: Create reimbursement request ticket
    [2]: View all submitted tickets
    [3]: Delete a ticket
    [4]: Edit profile
    [X]: Logout");
            string res = Console.ReadLine();
            switch (res.ToUpper()) {
                case "1":
                    Console.WriteLine("Initializing new request ticket...");
                    CreateTicket(empl);
                break;
                case "2":
                    empl.displayTickets();
                    Console.WriteLine("Press any key to return to previous menu.");
                    if(Console.ReadKey() != null) continue;
                break;
                case "3":
                    DeleteTicket(empl);
                break;
                case "4":
                    EditProfile(empl);
                break;
                case "X":
                    Console.WriteLine("Logging out of account...");
                    return -1;
                break;
                default:
                    Console.WriteLine("Please enter one of the following options.");
                break;
            }
        }
        return -1;
    }
 
    public static int RunManager(Manager manager) {
        // view list of Employees
        Thread.Sleep(2000);
        while(true) {
            manager.DisplayAllEmployees();
            Thread.Sleep(1000);
            Console.WriteLine(@"
   What would you like to do?
      [1]: View an employee's information
      [2]: Remove an employee
      [X}: Exit
    ");
            string action = Console.ReadLine().ToUpper();
            switch (action) {
                case "1":
                    Console.Write("     Enter the row of the employee you would like to view: ");
                    int emplRow = 0;
                    bool validEmplRow = int.TryParse(Console.ReadLine(), out emplRow);
                    Console.WriteLine();
                    if(validEmplRow) {
                        Employee viewingEmpl = manager.GetEmployee(emplRow-1);
                        if(viewingEmpl != null) {
                            Console.WriteLine(@"
       Viewing {0}'s tickets
    ---------------------------------------", viewingEmpl.name);
                            viewingEmpl.displayTickets();
                            Console.WriteLine(@"
    Actions:
        [1]: Accept a ticket
        [2]: Reject a ticket
        [X]: Return to previous menu
        ");
                            string menu2Action = Console.ReadLine().ToUpper();
                            switch (menu2Action) {
                                case "1":
                                    Console.Write("   Enter row of ticket you would like to accept: ");
                                    int acceptTicketRow = 0;
                                    bool acceptParseRow = int.TryParse(Console.ReadLine(), out acceptTicketRow);
                                    if(acceptParseRow) {
                                        if(manager.AcceptEmployeeTicket(viewingEmpl, acceptTicketRow - 1)) {
                                            Thread.Sleep(1000);
                                            viewingEmpl.displayTickets();
                                            Thread.Sleep(1000);
                                            Console.WriteLine(@"
          *************************
              Update successful
          *************************
          ");
                                            Thread.Sleep(1000);
                                        } else {
                                            Console.WriteLine("     !! Update was unsuccessful due to invalid row number");
                                            Thread.Sleep(2000);
                                            Console.WriteLine("   Returning to main menu...");
                                            Thread.Sleep(1500);
                                        }
                                    } else {
                                        Thread.Sleep(500);
                                        Console.WriteLine(" !! Valid row number was not inputted");
                                        Thread.Sleep(2000);
                                        Console.WriteLine("   Returning to main menu...");
                                        Thread.Sleep(1500);
                                    }
                                break;
                                case "2":
                                    Console.Write("   Please enter row of ticket you would like to reject: ");
                                    int rejectTicketRow = 0;
                                    bool rejectParseRow = int.TryParse(Console.ReadLine(), out rejectTicketRow);
                                    if(rejectParseRow) {
                                        if(manager.RejectEmployeeTicket(viewingEmpl, rejectTicketRow - 1)) {
                                            Thread.Sleep(1000);
                                            viewingEmpl.displayTickets();
                                            Thread.Sleep(1000);
                                            Console.WriteLine(@"
        ************************
            Update successful
        ************************
        ");
                                            Thread.Sleep(1000);
                                        } else {
                                            Console.WriteLine("   !! Update was unsuccessful due to invalid row number.");
                                            Thread.Sleep(2000);
                                            Console.WriteLine("   Returning to main menu...");
                                            Thread.Sleep(1500);
                                        }
                                    } else {
                                        Thread.Sleep(500);
                                        Console.WriteLine(" !! Valid row number was not inputted");
                                        Thread.Sleep(2000);
                                        Console.WriteLine("   Returning to main menu...");
                                        Thread.Sleep(1500);
                                    }
                                break;
                                case "X":
                                    Console.WriteLine("   Returning to main menu");
                                break;
                                default:
                                    Console.WriteLine("!! Unrecognized response. Returning to main menu...");
                                    Thread.Sleep(500);
                                break;
                            }
                        } else {
                            Thread.Sleep(500);
                            Console.WriteLine("   !! Row number is out of range. Please enter a valid row number displayed on the left of the table.");
                            Thread.Sleep(500);
                        }
                    } else {
                        Thread.Sleep(500);
                        Console.WriteLine(" !! Valid row number was not inputted");
                        Thread.Sleep(2000);
                        Console.WriteLine("   Returning to main menu...");
                        Thread.Sleep(1500);
                    }
                break;
                case "2":
                    Console.Write("   Enter the row number of the employee you would like to remove: ");
                    int removeRow = 0;
                    bool validRemoveRow = int.TryParse(Console.ReadLine(), out removeRow);
                    if(validRemoveRow && manager.RemoveEmployee(removeRow-1)) {
                        Console.WriteLine(@"
    ***********************************
        Employee removal successful
    ***********************************
    ");
                    }
                    else {
                        Console.WriteLine("     !! Removal was unsuccessful due to invalid row !!\n");
                        Thread.Sleep(1500);
                    }
                break;
                case "X":
                    return -1;
                break;
                default:
                    Console.WriteLine("   !! Unrecognized response. Please try again");
                break;
            }  
        }
        // accept/reject tickets
        return -1;
    }
 
    public static bool CreateTicket(Employee empl) {
        // prompt for value
        Console.Write("   What do you want to get reimbursed for?\n      ");
        string ticketDescription = Console.ReadLine();
        int tempReimburseVal = 0;
        while(true) {
            Console.Write("   How much do you expect to be reimbursed?\n      $");
            bool successfulReimburse = int.TryParse(Console.ReadLine(), out tempReimburseVal);
            if(!successfulReimburse || tempReimburseVal > 1000000000) {
                Console.WriteLine("   You've entered an invalid value. Please enter a number between 1 and 1,000,000,000");
            }
            else break;
        }
        Console.WriteLine("   Entering ticket reimbursement of ${0} for {1}...", tempReimburseVal, ticketDescription);
        Thread.Sleep(2000);
        empl.createTicket(tempReimburseVal, ticketDescription);
        Console.WriteLine("   Ticket has been submitted. Awaiting response from manager.");
        // prompt for description
        return true;
    }

    public static bool DeleteTicket(Employee empl) {
        // display tickets
        empl.displayTickets();
        
        // prmopt for row
        Console.Write("   Which ticket do you want to delete? (Enter row number or [X] to exit): ");
        int numInput = -1;
        string stringInput = Console.ReadLine();
        if(stringInput.ToLower() == "x") {
            Console.WriteLine("   Cancelling. Returning to menu...\n");
            return false;
        }
        bool numParse = int.TryParse(stringInput, out numInput);
        if(!numParse) {
            Console.WriteLine("   Unable to identify row number. Returning to menu...\n");
            return false;
        } 
        if(numInput > empl.ListOfExpenses.Count || numInput <= 0) {
            Console.WriteLine("   !! Value not in range. !!\n");
            return false;
        }
        else {
            empl.deleteTicket(numInput);
            Console.WriteLine("   ... Deleteion successful.\n");
            return true;
        }
        return false;
    }

    public static void EditProfile(Employee empl) {
        Console.Write(@"
     What would you like to edit?
        [1]: Name
        [2]: Password
        [3]: Email
     Option: ");
        int userInput = 0;
        bool res = int.TryParse(Console.ReadLine(), out userInput);
        if(res) {
            switch(userInput) {
                case 1:
                    Console.Write("      New name: ");
                    string tempName = ValidateAndReturnName(Console.ReadLine());
                    while(true) {
                        if(tempName != "") {
                            empl.name = tempName;
                            Console.WriteLine("   Successfully changed name.");
                            break;
                        } else Console.WriteLine("   Invalid name. You must input a first AND last name.");
                    }
                break;
                case 2:
                    Console.Write("      Current Password: ");
                    if(Console.ReadLine() == empl.userPW){
                        Console.Write("\n      New Password: ");
                        empl.userPW = Console.ReadLine();
                        Console.WriteLine("\n");
                    } else Console.WriteLine("   Password does not match! Returning to previous prompt...");
                break;
                case 3:
                    Console.Write("      New email: ");
                    while(true) {
                        string tempEmail = ValidateAndReturnEmail(Console.ReadLine());
                        if(tempEmail == "") Console.Write("   Please enter a valid email that includes \"@\" and its' domain.\n      ");
                        else {
                            empl.email = tempEmail;
                            Console.WriteLine("   Successfully changed email.");
                            break;
                        }
                    }
                break;
                default:
                    Console.WriteLine("Input unrecognized. Returning to previous prompt...");
                break;
            }
        }
    }
}