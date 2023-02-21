namespace Models;
public class Expense
{
    enum ticketStatuses { Pending = 0, Rejected = -1, Accepted = 1 }

    public int amount { get; set; } = 0;
    public string description { get; set; } = "";
    public int ticketStatus { get; set; } = 0;

    public Expense(int value) {
        this.amount = value;
    }

    public Expense(int value, string description) {
        this.amount = value;
        this.description = description;
    }

    public void ToString() {
        string ticketAcceptedString = (ticketStatus == (int)ticketStatuses.Accepted) ? "Accepted" : (ticketStatus == (int)ticketStatuses.Rejected) ? "Rejected" : "Pending" ;
        Console.WriteLine($"{amount, -7} | {ticketAcceptedString, -16} | {description}");
    }

    public void AcceptTicket() {
        ticketStatus = (int)ticketStatuses.Accepted;
    }

    public void RejectTicket() {
        ticketStatus = (int)ticketStatuses.Rejected;
    }

    public void PendingTicket() {
        ticketStatus = (int)ticketStatuses.Pending;
    }

    public void EditTicket(int value, string description) {
        this.amount = value;
        this.description = description;
    }

}
