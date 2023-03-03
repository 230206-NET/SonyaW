
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

    public Expense CreateExpense(Expense expenseToCreate, Employee employee) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand("INSERT INTO Expenses(EmplEmail, Amount, ExpenseDescription) OUTPUT INSERTED.Id VALUES (@emplEmail, @expenseAmount, @expenseDesc)", connection);  
        command.Parameters.AddWithValue("@expenseAmount", expenseToCreate.amount);
        command.Parameters.AddWithValue("@expenseDesc", expenseToCreate.description);
        command.Parameters.AddWithValue("@emplEmail", employee.email);
        int createdId = (int) command.ExecuteScalar();
        expenseToCreate.id = createdId;
        return expenseToCreate;
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
            using SqlCommand command = new SqlCommand("SELECT * FROM Expenses WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", idNum);
            using SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows) {
                reader.Read();
                if (reader.GetInt32(4) != 0) {
                    command.Dispose();
                    reader.Close();
                    return false;
                }
                else {
                    command.Dispose();
                    reader.Close();
                    using SqlCommand command2 = new SqlCommand("UPDATE Expenses SET TicketStatus = 1 WHERE Id = @id", connection);
                    command2.Parameters.AddWithValue("@id", idNum);
                    command2.ExecuteNonQuery();
                    return true;
                }
            }
        } catch (Exception e) {
            Console.WriteLine(e);
        }
        return false;
    }

    public bool RejectTicket(int idNum) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try{
            using SqlCommand command = new SqlCommand("SELECT * FROM Expenses WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", idNum);
            using SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows) {
                reader.Read();
                if ((int)reader.GetValue(4) != 0) {
                    command.Dispose();
                    reader.Close();
                    return false;
                }
                else {
                    command.Dispose();
                    reader.Close();
                    using SqlCommand command2 = new SqlCommand("UPDATE Expenses SET TicketStatus = -1 WHERE Id = @id", connection);
                    command2.Parameters.AddWithValue("@id", idNum);
                    command2.ExecuteNonQuery();
                    return true;
                }
            }
        } catch (Exception e) {
            Console.WriteLine(e);
        }
        return false;
    }

    public List<Expense> ManagersPendingTickets(string managerEmail) {
        // get all employees with the manager
        EmployeeDB emplDB = new EmployeeDB();
        List<Employee> emplList = emplDB.GetAssociatedEmployees(managerEmail);
        
        // foreach employee in list, get all expenses
        if(emplList != null) {
            List<Expense> AllPendingExpenses = new List<Expense>();
            foreach(Employee empl in emplList) {
                List<Expense> expenseList = GetExpenseForEmpl(empl.email).FindAll((ticket) => ticket.ticketStatus == 0);
                AllPendingExpenses.AddRange(expenseList);
            }
            return AllPendingExpenses;
        }
        return null;
    }



}
