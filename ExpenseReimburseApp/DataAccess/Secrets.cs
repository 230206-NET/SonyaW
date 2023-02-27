public class Secrets {
    private const string _connectionString = "Server=tcp:expense-reimburse-server.database.windows.net,1433;Initial Catalog=ExpenseReimburseDB;Persist Security Info=False;User ID=sonya_w_rev;Password=cK9DZ5pgabaSMKu;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public static string getConnectionString() => _connectionString;
}