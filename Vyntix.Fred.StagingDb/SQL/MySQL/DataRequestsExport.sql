-- delimiter //
create procedure DataRequestsExport(dataProviderID int)
begin

update vintages.series s
join fred_staging.DataRequests dr on dr.symbol = s.nativeID and s.dataproviderid = dataProviderID
set LastDataRequest = dr.RequestDate;



end;  -- //
-- delimiter ;