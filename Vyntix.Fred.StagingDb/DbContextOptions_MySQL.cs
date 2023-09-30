namespace LeaderAnalytics.Vyntix.Fred.StagingDb;

public class DbContextOptions_MySQL : IDbContextOptions
{
    public DbContextOptions Options { get; set; }

    public DbContextOptions_MySQL(string connectionString)
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        Options = builder.Options;
    }
}
