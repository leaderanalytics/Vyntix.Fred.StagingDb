create procedure dbo.SeriesExport @symbol nvarchar(50), @dataProviderID int, @insertCount int output, @updateCount int output as
begin
set nocount on;
declare @mergeOutput table (change char(20));

merge into vintages.dbo.series with (holdlock) as target
using (
		select s.frequency, s.lastUpdated, s.notes, s.popularity, s.RTStart, s.releaseID, s.seasonalAdj, s.symbol, s.title, s.units 
		from
		(
			select 
			distinct symbol 
			from fred_staging.dbo.Series s1
			where (s1.symbol = @symbol or @symbol is null)
		) sym
		join fred_staging.dbo.Series s on s.Symbol = sym.Symbol
		join vintages.dbo.NativeFrequencies f on s.Frequency = f.NativeID and f.DataProviderID = @dataProviderID
		where s.ID = (select top 1 id from fred_staging.dbo.Series s2 where s2.Symbol = sym.Symbol)
	
	) as source
	on target.NativeID = source.symbol and target.DataProviderID = @dataProviderID
when not matched then
	insert 
	(dataProviderID, isDiscontinued, lastUpdated, name, nativeFreqDataProviderID, nativeFreqNativeID, notes, popularity, rtStart, seasonalAdj, nativeID, units)
	values
	(
		@dataProviderID, 
		case charindex('DISCONTINUED', upper(source.title)) when 0 then 0 else 1 end,
		getdate(), title, @dataProviderID, frequency, notes, popularity, rtStart, seasonaladj, symbol, units
	)
when matched then
	update set
	target.IsDiscontinued = case charindex('DISCONTINUED', upper(source.title)) when 0 then 0 else 1 end,
	target.LastUpdated = getdate()
output $action into @mergeOutput;
set @insertCount = ISNULL((select count(*) from @mergeOutput where change = 'INSERT'), 0);
set @updateCount = ISNULL((select count(*) from @mergeOutput where change = 'UPDATE'), 0);

insert into vintages.dbo.ReleaseSeries (SeriesNativeID, SeriesDataProviderID, ReleaseNativeID, ReleaseDataProviderID, AssociationDataProviderID)
select distinct s.NativeID, s.DataProviderID, r.NativeID, r.DataProviderID, r.DataProviderID
from vintages.dbo.Series s
join FRED_Staging.dbo.Series d on s.NativeID = d.Symbol
join vintages.dbo.Releases r on d.ReleaseID = r.NativeID and r.DataProviderID = @dataProviderID
where not exists(select 1 from vintages.dbo.ReleaseSeries rs where rs.SeriesNativeID = s.NativeID and rs.SeriesDataProviderID = s.DataProviderID and rs.ReleaseNativeID = r.NativeID and rs.ReleaseDataProviderID = r.DataProviderID and rs.AssociationDataProviderID = @dataProviderID)
end
