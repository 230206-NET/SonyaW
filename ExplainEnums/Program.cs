namespace ExplainEnums;

using System.Text.RegularExpressions;
using System;

public class MainMenu {

    // BASIC SYNTAX OF DECLARING AN ENUM:
    enum Days { None = 0, Sunday = 1, Monday = 2, Tuesday = 3, Wednesday = 4, Thursday = 5, Friday = 6, Saturday = 7 }; 
    
    // What if you do not assign a value to enum variables?

    enum rpsValues { rock, paper, scissor };
    
    public static void Main() {
        Console.WriteLine();
/*
        string[] stringOfDays = Enum.GetNames(typeof(Days)); // What is this parameter?
        foreach(string value in stringOfDays) {
            Console.Write("{0} ", value);
        }
        Console.WriteLine("\n");
//*/

/*     ITERATING THROUGH AN ENUM
        foreach(int i in Enum.GetValues(typeof(Days))) {
            Console.WriteLine("Day {0} is {1}.", i, (Days)i);
        }
        Console.WriteLine();
//*/

/*     DIFFERENT WAYS TO RETRIEVING ENUM VALUES
        Days friday = Days.Friday;
        Days fridayInt = (Days) 6;
        Days fridayToObject = (Days) Enum.ToObject(typeof(Days), 6); // include (Days)
        // Enum.Parse()
        int fridayNum = (int)Days.Friday;

        Console.WriteLine("Calling a value from an enumerator:  {0}", friday);
        Console.WriteLine("Casting with the enumerator's type:  {0}", fridayInt);
        Console.WriteLine("Using ToObject() function:           {0}", fridayToObject);
        Console.WriteLine("Using Enum.Parse:                    {0}", Enum.Parse( typeof(Days), "Friday")); // "Friday" or "6"      
        Console.WriteLine("Friday is the {0}th day of the week.", fridayNum);
        Console.WriteLine();
//*/

/*    ~USING BITWISE OPERATIONS
        Days Meetings = Days.Monday | Days.Wednesday;
        Days LunchMeetings = Days.Tuesday | Days.Friday;
        if(Meetings == LunchMeetings) {
            Console.WriteLine("Sooooo many meetings T_T");
            // Days temp = (Days) Meetings & LunchMeetings; // trying to find overlapping values
            // Console.WriteLine(temp);
        }
        else Console.WriteLine("I'm free!!!");
        Console.WriteLine();
//*/

/*     What if it's not in the enumeration?
        string[] weapons = {"hammer", "rock", "machete"};
        foreach(string value in weapons){
            if(Enum.IsDefined(typeof(rpsValues), value)) {
                Console.WriteLine("Accepted {0}.", value);
            } else Console.WriteLine("Unaccepted {0}.", value);
        }
        Console.WriteLine();
//*/

/*     Another way of displaying the values
        string[] dayOfWeek = Enum.GetNames(typeof(Days));
        Console.WriteLine("Values of {0}: ", typeof(Days).Name);
        foreach(var dow in dayOfWeek) {
            Days tempVal = (Days) Enum.Parse(typeof(Days), dow);
            Console.WriteLine(" {0} {0:D}", tempVal);
        }
        Console.WriteLine( " {0} ({0:D})", dayOfWeek);
//*/


        /* Notes:
            - enums are used to represent a list of named integer constants
            - enum constants are public, static, and final
            - you cannot define a method inside the definition of an enumeration type
                To add functionality to an enumeration type, create an extension method
            - should not create an enum type whose underlying type is non-integral or char
                You may use reflection to craete it but the resulting types are unreliable and may throw additional exceptions
            - 
        */
    }

}

/*


*/