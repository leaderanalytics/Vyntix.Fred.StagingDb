namespace LeaderAnalytics.Vyntix.Fred.StagingDb;

public class DbContextOptions_MSSQL : IDbContextOptions
{
    public DbContextOptions Options { get; set; }

    public DbContextOptions_MSSQL(string connectionString)
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        builder.UseSqlServer(connectionString);
        Options = builder.Options;
    }
}
