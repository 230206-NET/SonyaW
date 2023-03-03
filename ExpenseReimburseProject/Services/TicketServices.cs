
namespace Services;

public class TicketService
{
    private readonly TicketDB _repo;

    public TicketService(TicketDB repo) {
        _repo = repo;
    }

    public List<Expense> GetAllExpense(){
        return _repo.GetAllExpense();
    }

    public List<Expense> GetExpenseForEmpl(string email) {
        return _repo.GetExpenseForEmpl(email);
    }
    
    public Expense CreateExpense(Expense expenseToCreate, Employee employee) {
        return _repo.CreateExpense(expenseToCreate, employee);
    }

    public bool DeleteExpense(int idNum) {
        return _repo.DeleteExpense(idNum);
    }

    public bool AcceptTicket(int idNum) {
        return _repo.AcceptTicket(idNum);
    }

    public bool RejectTicket(int idNum) {
        return _repo.RejectTicket(idNum);
    }

    public List<Expense> ManagersPendingTickets(string managerEmail) {
        return _repo.ManagersPendingTickets(managerEmail);
    }
}
