namespace LeaderAnalytics.Vyntix.Fred.StagingDb;

public class StagingDataMigrator_MySQL : IDatabaseInitializer
{
    private FREDStagingDb context;

    public StagingDataMigrator_MySQL(FREDStagingDb context)
    {
        this.context = context ?? throw new ArgumentNullException("context");
    }


    // This is NOT test data

    public async Task Seed(string migrationName)
    {
        using (DbCommand cmd = context.Database.GetDbConnection().CreateCommand())
        {

            // 
            // Remember to set .sql file to "Copy to output directory"
            //
            await cmd.Connection.OpenAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\ToggleObservationsIndex.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\ToggleSeriesIndex.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\TruncateStagingTables.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\ObservationsInsert.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\CategoryTagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\SeriesTagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\TagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\DataRequestsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\SeriesExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"Database\SQL\MySQL\VintagesExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.Connection.Close();
        }

        await context.SaveChangesAsync();
    }
}
