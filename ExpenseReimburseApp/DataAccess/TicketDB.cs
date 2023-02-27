
namespace DataAccess;
public class TicketDB
{
    public List<Expense> GetAllExpense(){
        List<Expense> allExpenses = new();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        using SqlCommand command = new SqlCommand("SELECT * FROM Expenses", connection);
        using SqlDataReader reader = command.ExecuteReader();

        if(reader.HasRows){
            while(reader.Read()) {
                allExpenses.Add(new Expense ( (int) reader["Amount"], (string) reader["ExpenseDescription"] ));
            }
            return allExpenses;
        }
        return null;
    }

    public List<Expense> GetExpenseForEmpl(string email) {

        List<Expense> expenseForEmployee = new List<Expense>();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try{
            using SqlCommand command = new SqlCommand("SELECT * FROM Expenses WHERE EmplEmail = @Email", connection);
            command.Parameters.AddWithValue("@Email", email);
            using SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows) {
                while(reader.Read()) {
                    expenseForEmployee.Add(new Expense((int) reader["Id"], (int)reader["Amount"], (string)reader["ExpenseDescription"], (int)reader["TicketStatus"]));
                }
                return expenseForEmployee;
            }
            return new List<Expense>(0);
        } catch(Exception e) {
            return new List<Expense>(0);
        }
    }

    public int CreateExpense(Expense expenseToCreate, Employee employee) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand("INSERT INTO Expenses(EmplEmail, Amount, ExpenseDescription) OUTPUT INSERTED.Id VALUES (@emplEmail, @expenseAmount, @expenseDesc)", connection);  
        command.Parameters.AddWithValue("@expenseAmount", expenseToCreate.amount);
        command.Parameters.AddWithValue("@expenseDesc", expenseToCreate.description);
        command.Parameters.AddWithValue("@emplEmail", employee.email);
        int createdId = (int) command.ExecuteScalar();
        expenseToCreate.id = createdId;
        return createdId;
    }

    public bool DeleteExpense(int idNum) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try{
            using SqlCommand command = new SqlCommand("DELETE FROM Expenses WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", idNum);
            command.ExecuteNonQuery();
            return true;
        } catch (Exception e) {
            Console.WriteLine("  !! Unable to proceed with deletion due to invalid expense ticket !!");
        }
        return false;
    }

    public bool AcceptTicket(int idNum) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try{
            using SqlCommand command = new SqlCommand("UPDATE Expenses SET TicketStatus = 1 WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", idNum);
            command.ExecuteNonQuery();
            return true;
        } catch (Exception e) {
            Console.WriteLine(e);
        }
        return false;
    }

    public bool RejectTicket(int idNum) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try{
            using SqlCommand command = new SqlCommand("UPDATE Expenses SET TicketStatus = -1 WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", idNum);
            command.ExecuteNonQuery();
            return true;
        } catch (Exception e) {
            Console.WriteLine("  !! Unable to accept this ticket !! ");
        }
        return false;
    }



}
