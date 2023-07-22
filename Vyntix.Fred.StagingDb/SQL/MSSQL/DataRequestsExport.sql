create procedure DataRequestsExport  @dataProviderID int
as
begin
set nocount on;

update vintages.dbo.series
	set LastDataRequest = dr.RequestDate
from
	fred_staging.dbo.DataRequests dr
	join vintages.dbo.series s on dr.symbol = s.nativeID and s.dataproviderid = @dataProviderID

end