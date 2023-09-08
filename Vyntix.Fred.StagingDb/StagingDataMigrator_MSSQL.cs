namespace LeaderAnalytics.Vyntix.Fred.StagingDb;

public class StagingDataMigrator_MSSQL : IDatabaseInitializer
{
    private FREDStagingDb context;

    public StagingDataMigrator_MSSQL(FREDStagingDb context)
    {
        this.context = context ?? throw new ArgumentNullException("context");
    }

    // Consider using this:  Set all sql files to "Embedded Resource" - no need to copy to output dir.  
    //using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eSlideManager.DataServer.Services.SQL.CreateAlphanumericSortValue.sql"))
    //    {
    //        using (StreamReader reader = new StreamReader(stream))
    //        {
    //            script = reader.ReadToEnd();
    //        }
    //    }


    // This is NOT test data

    public async Task Seed(string migrationName)
    {
        using (DbCommand cmd = context.Database.GetDbConnection().CreateCommand())
        {
            // 
            // Remember to set .sql file to "Copy to output directory"
            //

            await cmd.Connection.OpenAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\ToggleObservationsIndex.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\ToggleSeriesIndex.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();
            
            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\TruncateStagingTables.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\TagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\SeriesTagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\CategoryTagsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\DataRequestsExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\SeriesExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = System.IO.File.ReadAllText(AppContext.BaseDirectory + @"StagingDatabase\SQL\MSSQL\VintagesExport.sql", Encoding.ASCII);
            await cmd.ExecuteNonQueryAsync();

            cmd.Connection.Close();
        }

        await context.SaveChangesAsync();
    }
}
