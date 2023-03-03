
namespace DataAccess;

public class EmployeeDB {

    TicketDB ticketDB = new TicketDB();

    public List<Employee> GetAllEmployees() {
        List<Employee> allEmployees = new List<Employee>();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
        using SqlDataReader reader = command.ExecuteReader();

        if(reader.HasRows){
            while(reader.Read()) {
                allEmployees.Add(new Employee( (string) reader["EmplName"], (string) reader["EmplPW"], (string) reader["EmplEmail"], (string)reader["MngEmail"]));     
            }
            return allEmployees;
        }
        return null;
    }

    // display all employees associated with a manager: happens after manager logins
    public List<Employee> GetAssociatedEmployees(string managerEmail) {
        
        List<Employee> associatedEmployees = new List<Employee>();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Employees WHERE MngEmail = @email", connection);
        command.Parameters.AddWithValue("@email", managerEmail);
        using SqlDataReader reader = command.ExecuteReader();

        if(reader.HasRows) {
            while(reader.Read()) {
                associatedEmployees.Add(new Employee( (string) reader["EmplName"], (string) reader["EmplPW"], (string) reader["EmplEmail"], (string)reader["MngEmail"]));
            }
            return associatedEmployees;
        }
        return null;
    }

    public bool DoesEmployeeExist(string email) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand("SELECT top 1 * FROM Employees WHERE EmplEmail = @Email", connection);
        command.Parameters.AddWithValue("@Email", email);
        using SqlDataReader reader = command.ExecuteReader();
        if(reader.HasRows) return true;
        return false;
    }

    public Employee EmplLogIn(string email, string password) {
       using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try {
            using SqlCommand command = new SqlCommand("SELECT top 1 * FROM Employees WHERE EmplEmail = @Email AND EmplPW = @Password", connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            using SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows) {
                reader.Read();
                // Console.WriteLine("Reader has rows: {0}, {1}, {2}, {3}", reader.GetString(1), reader.GetString(2), reader.GetString(0), reader.GetString(3));
                ManagerDB mngDB = new ManagerDB();
                return new Employee(reader.GetString(1), reader.GetString(2), reader.GetString(0), reader.GetString(3), mngDB.GetSingleManager(reader.GetString(3)).name, ticketDB.GetExpenseForEmpl(email));
            }
            return null;
        } catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }

    // create an employee account
    public Employee CreateEmployee(Employee newEmp) {
        try{
            using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
            connection.Open();

            using SqlCommand command = new SqlCommand("INSERT INTO Employees(EmplEmail, EmplName, EmplPW, MngEmail) VALUES (@email, @name, @pw, @mngEmail)", connection);

            command.Parameters.AddWithValue("@name", newEmp.name);
            command.Parameters.AddWithValue("@email", newEmp.email);
            command.Parameters.AddWithValue("@mngEmail", newEmp.manager);
            command.Parameters.AddWithValue("@pw", newEmp.userPW);

            command.ExecuteNonQuery();

            return newEmp;
        } catch(Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }

    // edit profile
    public bool EditProfile(Employee editEmployee) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try {
            using SqlCommand command = new SqlCommand("UPDATE Employees SET EmplName = @Name, EmplPW = @Password WHERE EmplEmail = @Email", connection);
            command.Parameters.AddWithValue("@Name", editEmployee.name);
            command.Parameters.AddWithValue("@Password", editEmployee.userPW);
            command.Parameters.AddWithValue("@Email", editEmployee.email);
            command.ExecuteNonQuery();
            return true;
        } catch(Exception e) {
            return false;
        }
    }

    // delete employee data
    public bool DeleteEmployee(Employee EmplToDelete) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try {
            using SqlCommand command = new SqlCommand("DELETE FROM Employees WHERE EmplEmail = @Email", connection);
            command.Parameters.AddWithValue("@Email", EmplToDelete.email);
            command.ExecuteNonQuery();

            return true;
        } catch (Exception e) {
            return false;
        }
    }

    public Employee GetSingleEmployee(string email) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try {
            using SqlCommand command = new SqlCommand("SELECT top 1 * FROM Employees WHERE EmplEmail = @Email", connection);
            command.Parameters.AddWithValue("@Email", email);
            using SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows) {
                reader.Read();
                // Console.WriteLine("Reader has rows: {0}, {1}, {2}, {3}", reader.GetString(1), reader.GetString(2), reader.GetString(0), reader.GetString(3));
                return new Employee(reader.GetString(1), reader.GetString(2), reader.GetString(0), reader.GetString(3), ticketDB.GetExpenseForEmpl(email));
            }
            return null;
        } catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }

}