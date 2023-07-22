create procedure VintagesExport @symbol varchar(50), @dataProviderID int, @vintageinsertCount int output, @obsinsertcount int output
as
begin
set nocount on;

declare @vintageIDs table (id int);

if not exists(select 1 from vintages.dbo.series (nolock) where nativeID = @symbol and dataproviderID = @dataproviderID)
	return;

insert 
into vintages.dbo.vintages
(name, DataProviderID, SeriesDataProviderID, ReleaseStatus, NativeID, VintageDate)
output inserted.ID into @vintageIDs
select convert(char(10), v.vintagedate, 111), @dataProviderID, @dataProviderID, 3, @symbol, v.VintageDate
from 
(
	select distinct
	VintageDate 
	from fred_staging.dbo.Observations (nolock) dtoo
	where symbol = @symbol
	and dtoo.VintageDate > 
	(
		select isnull(max(v2.vintagedate),'1700-01-01')
		from Vintages.dbo.Vintages (nolock) v2 
		where v2.NativeID = @symbol and v2.SeriesDataProviderID = @dataProviderID and v2.DataProviderID = @dataProviderID
	)
) v
select @vintageinsertCount = @@ROWCOUNT;

insert into vintages.dbo.Observations
(ObsDate, [Open], High, Low, [Close], VintageID)
select distinct dtoo.obsDate, dtoo.value, dtoo.value, dtoo.value, dtoo.value, v.ID
from FRED_Staging.dbo.Observations (nolock) dtoo
join Vintages.dbo.Vintages v on v.NativeID = dtoo.Symbol and v.VintageDate = dtoo.VintageDate
join @vintageIDs newIDs on v.ID = newIDs.id -- we only want to insert observations if the parent vintage has been newly inserted
where dtoo.Symbol = @symbol and v.DataProviderID = @dataProviderID and v.SeriesDataProviderID = @dataProviderID

select @obsinsertcount = @@ROWCOUNT;
end