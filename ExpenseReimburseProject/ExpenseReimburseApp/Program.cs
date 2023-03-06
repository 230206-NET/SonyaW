using Serilog;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UI;

public class MainMenu {
    internal static TicketService _ticketService = new TicketService(new TicketDB());
    internal static EmployeeService _employeeService = new EmployeeService(new EmployeeDB());
    internal static ManagerService _managerService = new ManagerService(new ManagerDB());
    
    private static HttpClient client = new HttpClient();

    public static async void Main(string[] args) {
        client.BaseAddress = new Uri("http://localhost:5268/");

        // Main();

        using var logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();
        // can use:
        //  - logger.Information
        //  - logger.Warning
        //  - logger.Fatal

        // JsonContect content = JsonContent.Create<Type>();
        // await _http.Postasync("url", jsoncontent);

        try{
            MainMenu start = new MainMenu();
            logger.Information("Application starting ...");
            Start();
            logger.Information("... Application terminated");
        } catch (Exception e) {
            logger.Fatal(" !! Applictaion has been terminated due to:\n {0}\n", e);
        }
    }

    public static async Task Main(){
        string res = await client.GetStringAsync("empl");
        List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(res);
        foreach(Employee empl in employees) {
            Console.WriteLine(empl.name);
        }
    }

    private static async Task Start() {
        using var logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();

        Console.WriteLine(@"
   ***********************************
        REIMBURSEMENT APPLICATION
   *********************************** ");
        while(true) {
            string initRes = initPrompt().ToLower();
            switch (initRes) {
                case "1":
                    logger.Information("Logging in...");
                    LogIn();
                break;
                case "2":
                    logger.Information("Creating account...");
                    try {
                        // using HttpResponseMessage res = await client.GetAsync("http://localhost:5268/empl");
                        // res.EnsureSuccessStatusCode();
                        // string responseBody = await res.Content.ReadAsStringAsync();
                        // Console.WriteLine(responseBody);

                        string res = await client.GetStringAsync("empl");
                        Console.WriteLine(res);
                        // List<Employee> empls = JsonSerializer.Deserialize<List<Employee>>(responseBody);
                        // foreach(Employee empl in empls) {
                        //     Console.WriteLine(empl.name);
                        // }
                    } catch (Exception e) {
                        Console.WriteLine("Unable to process HttpClient");
                    }

                    Employee newEmployee = CreateAcct();
                    if(newEmployee != null) {
                        logger.Information("Account creation successful. Running employee...");
                        RunEmployee(newEmployee);
                    }
                break;
                case "x":
                    Thread.Sleep(1000);
                    Console.WriteLine("Exiting application...");
                    logger.Information("Exiting application...");
                    Environment.Exit(0);  
                break;
                default:
                    Console.WriteLine("\nUnrecognized respnse. Please enter a valid value.");
                break;
            }

        }
    }

    private static string initPrompt(){
        Console.WriteLine(@"    [1] Sign in
    [2] Create Account
    [X] Exit");
        string res = Console.ReadLine();
        return res;
    }
  
    private static int LogIn(){
        using var logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();
        
        Console.Write("Signing in...\n   Enter email: ");
        string emailToCheck = Console.ReadLine();
        // CHECK LOGIN CREDENTIALS FOR MANAGERS
        if(_managerService.DoesManagerExist(emailToCheck)) {
            logger.Information("Login valid. Initializing sign in for MANAGER: {0}", emailToCheck);
            Manager mngLogIn = _managerService.GetSingleManager(emailToCheck);
            if(mngLogIn == null) Console.WriteLine("Null Manager");
            else {
                Console.Write("   Enter your password: ");
                string password = PromptPassword();
                if(password == mngLogIn.userPW) {
                    Console.WriteLine("\n   Log in successful.");
                    logger.Information("   Sign in for {0} successful. Running manager...", emailToCheck);
                    Thread.Sleep(500);
                    mngLogIn.ToString();
                    return RunManager(mngLogIn);
                } else {
                    Console.WriteLine("\n   Log in failed. Returning to starting menu ...");
                    logger.Warning("Attempt to log in with {0} failed.", emailToCheck);
                    Thread.Sleep(1500);
                }
            }
        }
        // IF NOT MANAGER, CHECK LOGIN CREDENTIAL FOR EMPLOYEE
        else if(_employeeService.DoesEmployeeExist(emailToCheck)){
            logger.Information("Login valid. Initializing sign in for EMPLOYEE: {0}", emailToCheck);
            Employee emplLogIn = _employeeService.GetSingleEmployee(emailToCheck);
            if(emplLogIn == null) Console.WriteLine("Null Employee");
            else {
                Console.Write("   Enter your password: ");
                string password = PromptPassword();
                if(password == emplLogIn.userPW) {
                    Console.WriteLine("\n   Log in successful.");
                    logger.Information("   Sign in for {0} successful. Running employee...", emailToCheck);
                    Thread.Sleep(500);
                    emplLogIn.managerName = _managerService.GetSingleManager(emplLogIn.manager).name;
                    emplLogIn.ToString();
                    return RunEmployee(emplLogIn);
                } else {
                    Console.WriteLine("\n   Log in failed. Returning to starting menu ...");
                    logger.Warning("Attemp to log in with {0} failed.", emailToCheck);
                    Thread.Sleep(1500);
                }
            }
        }
        else {
            logger.Information("Attemp to log in with an unregistered email.");
            Console.WriteLine(" !! That email has not been registered. Please create an account before attempting to log in. !! ");
        }
        // if not then check in manager db
            // if so then check password then retur true if correct
        // if neither, keep asking for email until exit
            // you don't exit unless user presses exit even while typing in password
        return -1;
    }

    public static string PromptPassword() {
        StringBuilder sb = new StringBuilder();
        while(true) {
            var key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter) {
                return sb.ToString();
            } else {
                if(key.Key == ConsoleKey.Backspace){
                    if(sb.Length > 0) {
                        sb.Remove(sb.Length - 1, 1);
                        continue;
                    }
                    Console.Write("-");
                }
                else {
                    sb.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
        }
        return "";
    }
  
    private static Employee CreateAcct() {      // RETURNS TRUE IF ABLE TO CREATE AN ACCOUNT
        using var logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();
        
        logger.Information("Initializing new account set up...");
        Console.WriteLine("\nSetting up new account...");
        Thread.Sleep(500);
        Console.Write(" Press 'X' anytime to exit.\n\n  Enter your manager's email: ");

        // PROMPT FOR USER'S MANAGER
        string newEmpManager = "";
        while(true) {
            newEmpManager = Console.ReadLine();
            if(newEmpManager.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                logger.Information("   Acount set up cancelled...");
                Thread.Sleep(500);
                return null;
            }
            newEmpManager = ValidateAndReturnEmail(newEmpManager);
            if(newEmpManager != "" && _managerService.DoesManagerExist(newEmpManager)) break;
            else Console.Write("     ** You must have a manager present in our system to create an account. **\n     Manager Email: ");
        }

        // RETRIEVE USER FULL NAME
        Console.Write("  Enter your first and last name: ");
        string newEmpName = "";
        while(true) {
            newEmpName = Console.ReadLine();
            if(newEmpName.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                logger.Information("   Acount set up cancelled...");
                Thread.Sleep(500);
                return null;
            }
            newEmpName = ValidateAndReturnName(newEmpName);
            if(newEmpName != "") break;
            else Console.Write("     ** Please enter your first AND last name. **\n     Your full name: ");
        }

        // RETRIEVE EMAIL FROM USER
        Console.Write("  Enter your email: ");
        string newEmpEmail = "";
        while(true) {
            string tempString = Console.ReadLine();
            if(tempString.ToUpper() == "X") {
                Console.WriteLine("Exiting...");
                logger.Information("   Acount set up cancelled...");
                Thread.Sleep(500);
                return null;
            }
            newEmpEmail = ValidateAndReturnEmail(tempString);
            if(newEmpEmail == "") Console.Write("     ** You have entered a short or invalid email. **\n     Email: ");
            else break;
        }

        // RETRIEVE PASSWORD FROM USER
        Console.Write("  Enter you password: ");
        string newEmpPW = "";
        while(true) {
            newEmpPW = PromptPassword();
            if(newEmpPW == "X") {
                Console.WriteLine("Exiting...");
                logger.Information("   Acount set up cancelled...");
                Thread.Sleep(500);
                return null;
            }
            if(newEmpPW.Length > 5) {
                Console.WriteLine("\n");
                break;
            }
            else {
                Console.Write("\n     ** Your password must have a length of at least 5. **\n     Password: ");
                continue;
            }
        }

        // FINALIZING CREATION OF ACCOUNT
        Console.WriteLine("Creating account...");
        Thread.Sleep(1000);
        Employee newEmp = new Employee(newEmpName, newEmpPW, newEmpEmail, newEmpManager);
        newEmp.managerName = _managerService.GetSingleManager(newEmpManager).name;
        if(_employeeService.CreateEmployee(newEmp) != null) {
            newEmp.ToString();
            logger.Information("   Account creation successful: Name: {0}, Email: {1}, Password: {2}, Manager: {3}", newEmpName, newEmpEmail, newEmpPW, newEmpManager);
            Thread.Sleep(400);
            Console.Write("\n    ***** Welcome {0}! *****  \n", newEmp.name);
            return newEmp;
        } else {
            Console.WriteLine("Account creating was not successful due to invalid manager email. Returning to main menu...");
            logger.Warning("   Account could not be created. CreateEmployee from EmployeeDB returned -1.");
            Thread.Sleep(1000);
            return null;
        }
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
        Thread.Sleep(500);
        while(true) {
             Console.WriteLine(@"
````````````````````````````````````````````
What action would you like to take?
    [1]: Create reimbursement request ticket
    [2]: View all submitted tickets
    [3]: Delete a ticket
    [4]: Edit profile
    [5]: View Profile
    [X]: Logout");
            string res = Console.ReadLine();
            Thread.Sleep(200);
            switch (res.ToUpper()) {
                case "1":
                    Console.WriteLine("Initializing new request ticket...");
                    Thread.Sleep(200);
                    CreateTicket(empl);
                break;
                case "2":
                    empl.displayTickets();
                    Console.WriteLine("Press any key to continue.  ");
                    if(Console.ReadKey() != null) continue;
                break;
                case "3":
                    DeleteTicket(empl);
                break;
                case "4":
                    EditProfile(empl);
                break;
                case "5":
                    empl.ToString();
                    Console.Write("\n   Press any button to return to previous menu... ");
                    Console.ReadKey();
                break;
                case "X":
                    Console.WriteLine("Logging out of account...");
                    Thread.Sleep(1000);
                    return -1;
                break;
                default:
                    Console.WriteLine("Please enter one of the following options.");
                break;
            }
            Thread.Sleep(200);
        }
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
        Thread.Sleep(500);
        try{
            Expense newExpenseCreated = empl.createTicket(tempReimburseVal, ticketDescription);
            _ticketService.CreateExpense(newExpenseCreated, empl);
            Console.WriteLine("   Ticket has been submitted. Awaiting response from manager.");
            Thread.Sleep(200);
            return true;
        } catch(Exception e) {
            Console.WriteLine("\n  !! Unable to create ticket due to invalid user in database !! ");
            Thread.Sleep(200);
        }
        return false;
    }

    public static bool DeleteTicket(Employee empl) {
        // display tickets
        empl.displayTickets();
        
        // prmopt for row
        Console.Write("   Which ticket do you want to delete? (Enter row number or [X] to exit): ");
        int numInput = -1;
        string stringInput = Console.ReadLine();
        Thread.Sleep(200);
        if(stringInput.ToLower() == "x") {
            Console.WriteLine("   Cancelling. Returning to menu...\n");
            Thread.Sleep(200);
            return false;
        }
        bool numParse = int.TryParse(stringInput, out numInput);
        if(!numParse) {
            Console.WriteLine("   Unable to identify row number. Returning to menu...\n");
            Thread.Sleep(200);
            return false;
        } 
        else if(numInput > empl.ListOfExpenses.Count || numInput <= 0) {
            Console.WriteLine("   !! Value not in range. !!\n");
            Thread.Sleep(200);
            return false;
        }
        else {
            int expenseToRemoveID = empl.GetExpense(numInput-1).id;
            try{
                empl.deleteTicket(numInput-1);
                Thread.Sleep(200);
                if(_ticketService.DeleteExpense(expenseToRemoveID)){
                    Console.WriteLine(" ... Deletion successful.");
                    Thread.Sleep(500);
                    return true;
                } else {
                    Console.WriteLine("   Deletion was unsuccessful");
                    Thread.Sleep(500);
                    return false;
                }
            } catch(Exception e) {
                Thread.Sleep(200);
                Console.WriteLine(" !! Unable to proceed !!");
                Thread.Sleep(200);
            }
        }
        return false;
    }

    public static void EditProfile(Employee empl) {
        Thread.Sleep(200);
        Console.Write("  ** You may press ENTER to move on to the next credential**\n     Current Password (For user validation) : ");
        if(PromptPassword() == empl.userPW) {
            // Console.Write("     New Email: ");
            // string emailRes = ValidateAndReturnEmail(Console.ReadLine());
            Console.Write("\n     New name (First AND last name or else changes will not be made): ");
            string nameRes = ValidateAndReturnName(Console.ReadLine());
            Console.Write("     New Password: ");
            string pwRes = PromptPassword();
            empl.EditProfile(nameRes == "" ? empl.name : nameRes, pwRes);
            _employeeService.EditProfile(empl);
            Thread.Sleep(200);
            Console.WriteLine("\n   Updates have been made!");
        }
        else{
            Thread.Sleep(200);
            Console.WriteLine("\n   Your password does not match and unable to make changes. Exiting...");
        }

    }
    
    public static int RunManager(Manager manager) {
        while(true) {
            manager.EmployeesToManage = _employeeService.GetAssociatedEmployees(manager.email);
            foreach(Employee empl in manager.EmployeesToManage) {
                List<Expense> emplExpenses = _ticketService.GetExpenseForEmpl(empl.email);                
                empl.ListOfExpenses = emplExpenses;
            }
            manager.DisplayAllEmployees();
            Thread.Sleep(500);
            Console.Write(@"
   What would you like to do?
      [1]: View an employee's information
      [2]: Remove an employee
      [3]: Edit Profile
      [4]: View Profile
      [X}: Log out
    "); //      [3]: Promote employee to manager (Work in progress ...)
        //      [4]: Demote a manger (Work in progress ...)
            string action = Console.ReadLine().ToUpper();
            switch (action) {
                case "1":
                    Console.Write("     Enter the row of the employee you would like to view: ");
                    int emplRow = 0;
                    bool validEmplRow = int.TryParse(Console.ReadLine(), out emplRow);
                    Console.WriteLine();
                    Thread.Sleep(200);
                    if(validEmplRow) {
                        Employee viewingEmpl = manager.GetEmployee(emplRow-1);
                        if(viewingEmpl != null) {
                            viewingEmpl.ListOfExpenses = _ticketService.GetExpenseForEmpl(viewingEmpl.email);
                            Console.WriteLine(@"
       Viewing {0}'s tickets
    ---------------------------------------", viewingEmpl.name);
                            Thread.Sleep(200);
                            viewingEmpl.displayTickets();
                            bool viewingEmplTickets = true;
                            while(viewingEmplTickets) {
                                Thread.Sleep(200);
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
                                        Thread.Sleep(200);
                                        if(acceptParseRow) {

                                            if(manager.AcceptEmployeeTicket(viewingEmpl, acceptTicketRow - 1) && _ticketService.AcceptTicket(viewingEmpl.GetExpense(acceptTicketRow-1).id)) {
                                                // manager.EmployeesToManage[emplRow - 1].ListOfExpenses[acceptTicketRow - 1].ticketStatus = 1;
                                                viewingEmpl.displayTickets();
                                                Thread.Sleep(200);
                                                Console.WriteLine(@"
        ***********************
            Ticket Accepted
        ***********************");
                                            } else {
                                                Console.WriteLine(" !! Update was unsuccessful due to invalid row number");
                                                Thread.Sleep(200);
                                                Console.WriteLine("   Returning to main menu...");
                                            }
                                        } else {
                                            Console.WriteLine(" !! Not a valid row number !!");
                                            Thread.Sleep(200);
                                            Console.WriteLine("   Returning to main menu...");
                                        }
                                    break;
                                    case "2":
                                        Console.Write("   Please enter row of ticket you would like to reject: ");
                                        int rejectTicketRow = 0;
                                        bool rejectParseRow = int.TryParse(Console.ReadLine(), out rejectTicketRow);
                                        Thread.Sleep(200);
                                        if(rejectParseRow) {
                                            if(manager.RejectEmployeeTicket(viewingEmpl, rejectTicketRow - 1) && _ticketService.RejectTicket(viewingEmpl.GetExpense(rejectTicketRow - 1).id)) {
                                                viewingEmpl.displayTickets();
                                                Thread.Sleep(200);
                                                Console.WriteLine(@"
        ***********************
            Ticket Rejected
        ***********************");
                                            } else {
                                                Console.WriteLine(" !! Update was unsuccessful due to invalid row number.");
                                                Thread.Sleep(200);
                                                Console.WriteLine("   Returning to main menu...");
                                            }
                                        } else {
                                            Console.WriteLine(" !! Not a valid row number !!");
                                            Thread.Sleep(200);
                                            Console.WriteLine("   Returning to main menu...");
                                    }
                                    break;
                                    case "X":
                                        Console.WriteLine("   Returning to main menu");
                                        viewingEmplTickets = false;
                                    break;
                                    default:
                                        Console.WriteLine(" !! Unrecognized response");
                                    break;
                                }
                            }
                            
                        } else {
                            Console.WriteLine("   !! Row number is out of range. Please enter a valid row number displayed on the left of the table !! ");
                        }
                    } else {
                        Console.WriteLine(" !! Valid row number was not inputted !! ");
                        Thread.Sleep(200);
                        Console.WriteLine("   Returning to main menu...");
                    }
                break;
                case "2":
                    Console.Write("   Enter the row number of the employee you would like to remove: ");
                    int removeRow = 0;
                    bool validRemoveRow = int.TryParse(Console.ReadLine(), out removeRow);
                    Thread.Sleep(200);
                    if(validRemoveRow && _employeeService.DeleteEmployee(manager.GetEmployee(removeRow-1)) && manager.RemoveEmployee(removeRow-1)) {
                        Console.WriteLine(@"
    ***********************************
        Employee removal successful
    ***********************************
    ");
                    }
                    else {
                        Console.WriteLine("     !! Removal was unsuccessful due to invalid row !!\n");
                    }
                break;
    /*            case "3":
                    Console.Write("   Which row employee would you like to promote: ");
                    int rowNum = 0;
                    bool isRow = int.TryParse(Console.ReadLine(), out rowNum);
                    if(isRow && rowNum > 0 && rowNum <= manager.EmployeesToManage.Count) {
                        Manager promotedEmployee = _managerService.PromoteEmployee(manager.GetEmployee(rowNum-1));
                        manager.EmployeesToManage.RemoveAt(rowNum-1);
                        Console.WriteLine(@"
    ***********************************
       Employee promotion successful
    ***********************************
                        ");
                    }
                    else Console.WriteLine("   Invalid row number. Unable to promote an employee.");
                break;
    //*/

    /*            case "4":
                    List<Manager> allManagers = _managerService.GetAllManagers();
                    Console.WriteLine("         Managers\n   -----------------------");
                    int i = 1;
                    foreach(Manager mng in allManagers) {
                        Console.WriteLine($"  {i, 3} |  {mng.name}");
                        i++;
                    }
                    Console.Write("   Which row manager would you like to demote: ");
                    int rowNum = 0;
                    bool isRow = int.TryParse(Console.ReadLine(), out rowNum);
                    if(isRow && rowNum > 0 && rowNum <= allManagers.Count) {
                        Manager deleteMng = allManagers[rowNum - 1];
                        Employee demotedEmployee = _managerService.DemoteManager(deleteMng, manager.email);
                        if(demotedEmployee != null) {
                            Console.WriteLine("Employee demoted");
                            manager.EmployeesToManage.Add(_managerService.DemoteManager(deleteMng, manager.email));
                        }
                        
                        
                    }
                break; 
            //*/
                case "3":
                    Console.Write("  ** You may press ENTER to move on to the next credential**\n     Current Password (For user validation) : ");
                    if(PromptPassword() == manager.userPW) {
                        // Console.Write("     New Email: ");
                        // string emailRes = ValidateAndReturnEmail(Console.ReadLine());
                        Console.Write("\n     New name (First AND last name or else changes will not be made): ");
                        string nameRes = ValidateAndReturnName(Console.ReadLine());
                        if(nameRes == "") nameRes = manager.name;

                        Console.Write("     New Password: ");
                        string pwRes = PromptPassword();
                        if(pwRes == "") pwRes = manager.userPW;
                        // string oldEmail = manager.email;
                        manager.EditProfile(nameRes, pwRes);
                        _managerService.EditProfile(manager);
                        Thread.Sleep(200);
                        Console.WriteLine("\n   Updates have been made!");
                    }
                    else{
                        Thread.Sleep(200);
                        Console.WriteLine("\n   Your password does not match and unable to make changes. Exiting...");
                    }
                break;
                case "4":
                    manager.ToString();
                    Console.Write("\n   Press any button to return to previous menu...  ");
                    Console.ReadKey();
                break;
                case "X":
                    Thread.Sleep(200);
                    Console.WriteLine("  Logging out ...");
                    Thread.Sleep(1000);
                    return -1;
                break;
                default:
                    Thread.Sleep(200);
                    Console.WriteLine("   !! Unrecognized response. Please try again !! ");
                break;
            }
            Thread.Sleep(500);
        }
        return -1;
    }
}