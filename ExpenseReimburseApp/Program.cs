namespace UI;

using System.Text;
using System.Threading;
using Models;
public class MainMenu {

    public static void Main(string[] args) {

    /* MVPs
        Allow use to sign in/create account
        Allow employees to submit a ticket
        Allow Managers to Accet/Reject a ticket
    */
    int acceptedUse = initPrompt();
        switch (acceptedUse) {
            case 1:
                LogIn();
            break;
            case 2:
                CreateAcct();
            break;
        }
    }

    private static int initPrompt(){
        Console.WriteLine(@"
    [1] Sign in
    [2] Create Account");
        int res = 0;
        bool valRes = int.TryParse(Console.ReadLine(), out res);
        return valRes ? res : -1;
    }

    private static void LogIn(){
        Console.WriteLine("Signing in...\n     Enter email:");
    }

    private static void CreateAcct() {
        Console.WriteLine("Setting up new account...\n     Enter your email:");
        // read console for email, use regex

        Console.WriteLine("     Enter you password: ");
        StringBuilder tempPW = new StringBuilder();
        while(true) {
            var key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter) {
                if(tempPW.Length > 5) {
                    Console.WriteLine();
                    break;
                }
                else {
                    Console.WriteLine("\nYour password is not long enough. It must have a length of at least 5.");
                    tempPW.Remove(0, tempPW.Length);
                    continue;
                }
            } else {
                if(key.Key == ConsoleKey.Backspace){
                    tempPW.Remove(tempPW.Length - 1, 1);
                    continue;
                }
                tempPW.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        // Console.WriteLine(tempPW.ToString());

        Console.WriteLine("Creating account...");
        // thread
        Models.Employee newEmp = new Models.Employee("sonya", "hello world", "sonakz612@gmail.com", "Revature");

        Console.WriteLine("My name is {0} ", newEmp.name);
    }
}