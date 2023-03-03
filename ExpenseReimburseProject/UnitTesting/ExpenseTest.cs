
namespace UnitTesting;

public class ExpenseTest
{
    [Fact]
    public void DefaultConstructorSetsDefaultValues()
    {
        Expense expense = new Expense();
        Assert.Equal(expense.amount, 0);
        Assert.Equal(expense.ticketStatus, 0);
        Assert.Equal(expense.description, "");
    }

    [Fact]
    public void TicketIsAccepted()
    {
        Expense expense = new Expense();
        Assert.Equal(expense.ticketStatus, 0);
        expense.AcceptTicket();
        Assert.Equal(expense.ticketStatus, 1);
    }

    [Fact]
    public void TicketIsRejected()
    {
        Expense expense = new Expense();
        Assert.Equal(expense.ticketStatus, 0);
        expense.RejectTicket();
        Assert.Equal(expense.ticketStatus, -1);
    }

}