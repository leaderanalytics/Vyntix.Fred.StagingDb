﻿create procedure TruncateStagingTables as
begin
	truncate table Categories;
	truncate table CategoryTags;
	truncate table Observations;
	truncate table RelatedCategories;
	truncate table ReleaseDates;
	truncate table Releases;
	truncate table Series;
	truncate table SeriesCategories;
	truncate table SeriesTags;
	truncate table Sources;
	truncate table DataRequests;
end