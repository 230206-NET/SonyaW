namespace UI;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Models;

public class MainMenu {

    public static void Main(string[] args) {

    /* MVPs
        Allow use to sign in/create account
        Allow employees to submit a ticket
        Allow Managers to Accet/Reject a ticket
    */
        MainMenu start = new MainMenu();

        Console.WriteLine("Initiating testers...");
        List<Expense> testExpense = new List<Expense>() {
            new Expense(100, "stuff"), new Expense(150, "things"), new Expense(200, "airfare")
        };
        Employee tester = new Employee("sonya wong", "qienvpiq2nv", "sona@gmail.com", "juniper song", testExpense);
        
        // tester.ToString();

        // EditProfile(tester);

        // tester.ToString();

        start.Start();
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

    public static int RunManager(Employee manager) {
        // view list of Employees
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
        Console.WriteLine("   Ticket has been submitted. Awaiting response from manager.");
        empl.createTicket(tempReimburseVal, ticketDescription);
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