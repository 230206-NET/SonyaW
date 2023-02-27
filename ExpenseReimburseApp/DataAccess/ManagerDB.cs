
namespace DataAccess;

public class ManagerDB {

    EmployeeDB employeeDB = new EmployeeDB();

    public List<Manager> GetAllManagers() {
        List<Manager> allManagers = new List<Manager>();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try {
            using SqlCommand command = new SqlCommand("SELECT * FROM Managers", connection);
            using SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows) {
                while(reader.Read()) {
                    allManagers.Add(new Manager(reader.GetString(1), reader.GetString(2), reader.GetString(0)));
                }
            }
            return allManagers;
        } catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
        return null;
        
    }

    public bool DoesManagerExist(string email) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try{
            using SqlCommand command = new SqlCommand("SELECT top 1 * FROM Managers WHERE Email = @Email", connection);
            command.Parameters.AddWithValue("@Email", email);
            using SqlDataReader reader = command.ExecuteReader();

            return reader.HasRows;
        } catch(Exception e) {
            Console.WriteLine(e);
            return false;
        }
        return false;
    }

    // display all managers
    public Manager GetSingleManager(string email) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try {
            using SqlCommand command = new SqlCommand("SELECT top 1 * FROM Managers WHERE Email = @Email", connection);
            command.Parameters.AddWithValue("@Email", email);
            using SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows) {
                reader.Read();
                // Console.WriteLine("Reader has rows: {0}, {1}, {2}, {3}", reader.GetString(1), reader.GetString(2), reader.GetString(0), reader.GetString(3));
                return new Manager(reader.GetString(1), reader.GetString(2), reader.GetString(0), employeeDB.GetAssociatedEmployees(email));
            }
            return null;
        } catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }

    public Manager PromoteEmployee(Employee empl) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try {
            using SqlCommand command = new SqlCommand("INSERT INTO Managers(Email, managerName, managerPW) VALUES (@Email, @Name, @Password)", connection);
            command.Parameters.AddWithValue("@Email", empl.email);
            command.Parameters.AddWithValue("@Name", empl.name);
            command.Parameters.AddWithValue("@Password", empl.userPW);
            command.ExecuteNonQuery();

            employeeDB.DeleteEmployee(empl);

            return new Manager(empl.name, empl.userPW, empl.email);

        } catch (Exception e) {
            Console.WriteLine(e); // SERILOG
            return null;
        }
        return null;
    }

// DEMOTE MANAGER ISSUE: the demoted manager will have emails in the same tables as some of the employees
// manager emails
/*
    public Employee DemoteManager(Manager manager, string mngEmail) {
    // Steps:
    //   1. create employee object
    //   2. delete the manager object
    //   3. transfer their employees to the manager that is demoting
    //

        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();
        try {
            SqlCommand command = new SqlCommand("INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES (@Email, @Name, @Password, @ManagerEmail)", connection);
            command.Parameters.AddWithValue("@Email", manager.email);
            command.Parameters.AddWithValue("@Name", manager.name);
            command.Parameters.AddWithValue("@Password", manager.userPW);
            command.Parameters.AddWithValue("@ManagerEmail", mngEmail);

            command.ExecuteNonQuery();

            command = new SqlCommand("UPDATE Employees SET MngEmail = @ManagerEmail WHERE MngEmail = @OldMngEmail", connection);
            command.Parameters.AddWithValue("@ManagerEmail", mngEmail);
            command.Parameters.AddWithValue("@OldMngEmail", manager.email);

            command.ExecuteNonQuery();

            // transfer all of their employees to urs

            DeleteManager(manager);

            command.Dispose();

            return new Employee(manager.name, manager.userPW, manager.email, mngEmail);

        } catch (Exception e) {
            Console.WriteLine(e); // SERILOG
            return null;
        }
        return null;
    }
// */

    public bool DeleteManager(Manager managerToDelete) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try {
            using SqlCommand command = new SqlCommand("DELETE FROM Managers WHERE Email = @Email", connection);
            command.Parameters.AddWithValue("@Email", managerToDelete.email);
            command.ExecuteNonQuery();

            return true;
        } catch (Exception e) {
            return false;
        }
    }

    public bool EditProfile(Manager mng) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString());
        connection.Open();

        try{
            using SqlCommand command = new SqlCommand("UPDATE Managers SET managerName = @Name, managerPW = @Pw WHERE Email = @Email", connection);
            command.Parameters.AddWithValue("@Email", mng.email);
            command.Parameters.AddWithValue("@Name", mng.name);
            command.Parameters.AddWithValue("@Pw", mng.userPW);
            // command.Parameters.AddWithValue("@Id", mng.id);
            command.ExecuteNonQuery();
            return true;

        } catch(Exception e) {
            Console.WriteLine(e);
        }
        return false;
    }

}