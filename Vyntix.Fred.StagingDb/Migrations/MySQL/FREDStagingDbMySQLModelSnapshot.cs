﻿// <auto-generated />
using System;
using LeaderAnalytics.Vyntix.Fred.StagingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeaderAnalytics.Vyntix.Fred.StagingDb.Migrations.MySQL
{
    [DbContext(typeof(FREDStagingDbMySQL))]
    partial class FREDStagingDbMySQLModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("NativeID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("ParentID")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "parent_id");

                    b.HasKey("ID");

                    b.HasIndex("NativeID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredCategoryTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(0)");

                    b.Property<string>("GroupID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "group_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.Property<int>("Popularity")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "popularity");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("GroupID");

                    b.ToTable("CategoryTags");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredObservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ObsDate")
                        .HasColumnType("datetime(0)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18,6)")
                        .HasAnnotation("Relational:JsonPropertyName", "value");

                    b.Property<DateTime>("VintageDate")
                        .HasColumnType("datetime(0)");

                    b.HasKey("ID");

                    b.HasIndex("ObsDate");

                    b.HasIndex("Symbol");

                    b.HasIndex("VintageDate");

                    b.ToTable("Observations");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredRelatedCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RelatedCategoryID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.HasKey("ID");

                    b.ToTable("RelatedCategories");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredRelease", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsPressRelease")
                        .HasColumnType("tinyint(1)")
                        .HasAnnotation("Relational:JsonPropertyName", "press_release");

                    b.Property<string>("Link")
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("NativeID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.Property<DateTime>("RTStart")
                        .HasColumnType("datetime(0)")
                        .HasAnnotation("Relational:JsonPropertyName", "realtime_start");

                    b.HasKey("ID");

                    b.HasIndex("NativeID");

                    b.ToTable("Releases");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredReleaseDate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReleased")
                        .HasColumnType("datetime(0)")
                        .HasAnnotation("Relational:JsonPropertyName", "date");

                    b.Property<string>("ReleaseID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "release_id");

                    b.HasKey("ID");

                    b.HasIndex("ReleaseID");

                    b.ToTable("ReleaseDates");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredSeries", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Frequency")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "frequency");

                    b.Property<bool?>("HasVintages")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastMetadataCheck")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastObsCheck")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastUpdated")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "last_updated");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.Property<int>("Popularity")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "popularity");

                    b.Property<DateTime>("RTStart")
                        .HasColumnType("datetime(0)")
                        .HasAnnotation("Relational:JsonPropertyName", "realtime_start");

                    b.Property<string>("ReleaseID")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SeasonalAdj")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "seasonal_adjustment_short");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.Property<string>("Units")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "units");

                    b.HasKey("ID");

                    b.HasIndex("Symbol");

                    b.ToTable("Series");

                    b.HasAnnotation("Relational:JsonPropertyName", "seriess");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredSeriesCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.ToTable("SeriesCategories");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredSeriesTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(0)");

                    b.Property<string>("GroupID")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "group_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.Property<int>("Popularity")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "popularity");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.HasIndex("Symbol");

                    b.ToTable("SeriesTags");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredSource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("NativeID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.HasKey("ID");

                    b.HasIndex("NativeID");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("LeaderAnalytics.Vyntix.Fred.Model.FredSourceRelease", b =>
                {
                    b.Property<string>("SourceNativeID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ReleaseNativeID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("SourceNativeID", "ReleaseNativeID");

                    b.ToTable("SourceReleases");
                });
#pragma warning restore 612, 618
        }
    }
}
