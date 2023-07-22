-- delimiter //
drop procedure if exists fred_staging.SeriesExport;  -- //
create procedure fred_staging.SeriesExport(sym nvarchar(50), dpID int, out insertCount int, out updateCount int)
begin
set insertCount = 0;
set updateCount = 0;

update 
	vintages.series s
    join fred_staging.Series dto on s.NativeID = dto.symbol and s.DataProviderID = dpID
	join vintages.nativefrequencies f on dto.frequency = f.NativeID and f.DataProviderID = dpID
set
	s.IsDiscontinued = case locate('DISCONTINUED', upper(dto.Title)) when 0 then 0 else 1 end,
	s.LastUpdated = now(),
    s.name = dto.Title,
    s.NativeFreqNativeID = f.NativeID,
    s.Notes = dto.Notes,
    s.Popularity = dto.Popularity,
    s.RTStart = dto.RTStart,
    s.SeasonalAdj = dto.SeasonalAdj,
    s.Units = dto.Units
where
	sym is null or dto.symbol = sym;

set updateCount = row_count();

insert into 
	vintages.series
	(dataProviderID, isDiscontinued, lastUpdated, name, NativeFreqDataProviderID, NativeFreqNativeID, notes, popularity, rtStart, seasonalAdj, nativeID, units)
select  
	dpID, 
    case locate('DISCONTINUED', upper(dto.Title)) when 0 then 0 else 1 end,
    now(),
    dto.Title,
	dpID,
    dto.frequency,
    dto.Notes, dto.Popularity, dto.RTStart,
    dto.SeasonalAdj, dto.Symbol,
    dto.Units
from 
	(
		select s.frequency, s.lastUpdated, s.notes, s.popularity, s.RTStart, s.releaseID, s.seasonalAdj, s.symbol, s.title, s.units 
		from
		(
			select symbol, max(id) as id from fred_staging.Series s1
			where (sym is null or s1.symbol = sym)
			group by symbol
		) groups
		join fred_staging.Series s on s.id = groups.id
		join vintages.nativefrequencies f on s.frequency = f.NativeID and f.DataProviderID = dpID
	) dto
where 
	(sym is null or dto.symbol = sym)
    and not exists(select 1 from vintages.series s2 where s2.NativeID = dto.symbol and s2.DataProviderID = dpID limit 1);
   
set insertCount = row_count();    

insert into vintages.ReleaseSeries (SeriesNativeID, SeriesDataProviderID, ReleaseNativeID, ReleaseDataProviderID, AssociationDataProviderID)
select distinct s.NativeID, s.DataProviderID, r.NativeID, r.DataProviderID, r.DataProviderID
from vintages.Series s
join FRED_Staging.Series d on s.NativeID = d.Symbol
join vintages.Releases r on d.ReleaseID = r.NativeID
where
s.DataProviderID = dpID
and r.DataProviderID = dpID
and not exists(select 1 from vintages.ReleaseSeries rs where rs.SeriesNativeID = s.NativeID and rs.SeriesDataProviderID = s.DataProviderID and rs.ReleaseNativeID = r.NativeID and rs.ReleaseDataProviderID = r.DataProviderID and rs.AssociationDataProviderID = dpID);

end; -- //
-- delimiter ;
