namespace LeaderAnalytics.Vyntix.Fred.StagingDb;

public class FREDStagingDbMSSQL : FREDStagingDb, IMigrationContext
{
    public FREDStagingDbMSSQL(DbContextOptions options) : base(options)
    {

    }
}

public class FREDStagingDbSQLite : FREDStagingDb, IMigrationContext
{
    public FREDStagingDbSQLite(DbContextOptions options) : base(options)
    {

    }
}

public class FREDStagingDbMySQL : FREDStagingDb, IMigrationContext
{
    public FREDStagingDbMySQL(DbContextOptions options) : base(options)
    {

    }
}

public class FREDStagingDb : BaseDbContext
	{
    public DbSet<FredCategory> Categories { get; set; }
    public DbSet<FredCategoryTag> CategoryTags { get; set; }
    public DbSet<FredSeriesCategory> SeriesCategories { get; set; }
    public DbSet<FredObservation> Observations { get; set; }
    public DbSet<FredRelatedCategory> RelatedCategories { get; set; }
    public DbSet<FredRelease> Releases { get; set; }
    public DbSet<FredSeries> Series { get; set; }
    public DbSet<FredSeriesTag> SeriesTags { get; set; }
    public DbSet<FredSource> Sources { get; set; }
    public DbSet<FredReleaseDate> ReleaseDates { get; set; }
    public DbSet<DataRequest> DataRequests { get; set; }

    public FREDStagingDb(Func<IDbContextOptions> dbContextOptionsFactory)
        : base(dbContextOptionsFactory().Options)
    {

    }

    public FREDStagingDb(DbContextOptions options)
        : base(options)
    {
        // https://blog.oneunicorn.com/2012/03/12/secrets-of-detectchanges-part-3-switching-off-automatic-detectchanges/
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        Database.SetCommandTimeout(360);
    }


    protected override void OnModelCreating(ModelBuilder mb)
		{
        // Categoy
        mb.Entity<FredCategory>().Ignore(x => x.Series);
        mb.Entity<FredCategory>().Ignore(x => x.Children);
        mb.Entity<FredCategory>().Ignore(x => x.Related);
        mb.Entity<FredCategory>().Ignore(x => x.CategoryTags);
        

        // SeriesCategories
        

        // CategoryTag
        mb.Entity<FredCategoryTag>().Property(x => x.CreatedDate).HasColumnType(GetDateTypeForProvider());
        mb.Entity<FredCategoryTag>().Ignore(x => x.CreatedDateString);
        mb.Entity<FredCategoryTag>().Ignore(x => x.NativeID);

        // Observation
        mb.Entity<FredObservation>().Ignore(x => x.Vintage);
        mb.Entity<FredObservation>().Ignore(x => x.VintageDateString);
        mb.Entity<FredObservation>().Ignore(x => x.ObsDateString);
        mb.Entity<FredObservation>().Property(x => x.ObsDate).HasColumnType(GetDateTypeForProvider());
        mb.Entity<FredObservation>().Property(x => x.VintageDate).HasColumnType(GetDateTypeForProvider());
        
        // RelatedCategory

        // Release
        mb.Entity<FredRelease>().Property(x => x.RTStart).HasColumnType(GetDateTypeForProvider());
        mb.Entity<FredRelease>().Ignore(x => x.SourceReleases);



        // ReleaseDate
        mb.Entity<FredReleaseDate>().Property(x => x.DateReleased).HasColumnType(GetDateTypeForProvider());
        mb.Entity<FredReleaseDate>().Ignore(x => x.DateReleaseString);

        // Series
        mb.Entity<FredSeries>().Property(x => x.RTStart).HasColumnType(GetDateTypeForProvider());

        // SeriesTag
        mb.Entity<FredSeriesTag>().Property(x => x.CreatedDate).HasColumnType(GetDateTypeForProvider());
        mb.Entity<FredSeriesTag>().Ignore(x => x.CreatedDateString);
        mb.Entity<FredSeriesTag>().Ignore(x => x.NativeID);

        // FredSource
        mb.Entity<FredSource>().Ignore(x => x.SourceReleases);
    }
	}
