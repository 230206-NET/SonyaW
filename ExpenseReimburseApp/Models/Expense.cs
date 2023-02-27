using System.Text;

namespace Models;
public class Expense
{
    // enum ticketStatuses { Pending = 0, Rejected = -1, Accepted = 1 }

    public int id { get; set; }
    public int amount { get; set; } = 0;
    public string description { get; set; } = "";
    public int ticketStatus { get; set; } = 0;

    public Expense(){}

    public Expense(int value) {
        this.amount = value;
    }

    public Expense(int value, string description) {
        this.amount = value;
        this.description = description;
    }

    public Expense(int id, int value, string description) {
        this.id = id;
        this.amount = value;
        this.description = description;
    }

        public Expense(int id, int value, string description, int ticketStatus) {
        this.id = id;
        this.amount = value;
        this.description = description;
        this.ticketStatus = ticketStatus;
    }

    public void ToString() {
        string ticketAcceptedString = (ticketStatus == 1) ? "Accepted" : (ticketStatus == -1) ? "Rejected" : "Pending" ;
        Console.Write($"  {amount, -5} |   ");

        if(ticketStatus == 1) Console.ForegroundColor = ConsoleColor.Green;
        else if(ticketStatus == -1) Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{ticketAcceptedString, -12}");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine($" | {description}");
    }

    public void AcceptTicket() {
        ticketStatus = 1;
    }

    public void RejectTicket() {
        ticketStatus = -1;
    }

    // public void PendingTicket() {
    //     ticketStatus = (int)ticketStatuses.Pending;
    // }

    // public void EditTicket(int value, string description) {
    //     this.amount = value;
    //     this.description = description;
    // }

}
