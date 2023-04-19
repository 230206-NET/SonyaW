namespace DataAccess;

public class Secrets {
    private static string _connectionString = "Server=tcp:staging-projects-sw.database.windows.net,1433;Initial Catalog=Staging-Flashcards;Persist Security Info=False;User ID=CloudSAc9c8c357;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public static string getConnectionString() {
        return _connectionString;
    }
}