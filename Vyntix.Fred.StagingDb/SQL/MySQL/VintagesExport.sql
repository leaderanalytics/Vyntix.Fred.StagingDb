-- delimiter //
drop procedure if exists fred_staging.VintagesExport; -- //
create procedure fred_staging.VintagesExport (symbol varchar(50), dataProviderID int, out vintageInsertCount int, out obsInsertCount int )
this_proc:
begin
declare vintageID bigint;
declare nextVintageDate datetime;
declare done int default false;
declare dtoCursor cursor for select v.VintageDate
from 
(
	select distinct
	VintageDate 
	from fred_staging.Observations dtoo
	where dtoo.symbol = symbol 
	and dtoo.VintageDate > 
	(
		select ifnull(max(v2.vintagedate),'1700-01-01')
		from Vintages.Vintages v2 
		where v2.nativeID = symbol and v2.SeriesDataProviderID = dataProviderID and v2.DataProviderID = dataProviderID
	)
) v;
declare continue handler for not found set done = true;

set vintageInsertCount = 0;
set obsInsertCount = 0;

if (not exists(select 1 from vintages.series s where s.nativeID = symbol and s.dataproviderID = dataproviderID)) then 
	LEAVE this_proc;
end if;
set transaction isolation level read uncommitted;
open dtoCursor;
dtoCursorLoop: LOOP
	fetch dtoCursor into nextVintageDate;
	
    if done then 
		leave dtoCursorLoop; 
	end if;
    
    insert 
    into vintages.vintages
    (name, DataProviderID, SeriesDataProviderID, ReleaseStatus, NativeID, VintageDate)
    values (DATE_FORMAT(nextVintageDate, '%Y-%m-%d'), dataProviderID, dataProviderID, 3, symbol, nextVintageDate);
    
    set vintageID = (select LAST_INSERT_ID());
    set vintageInsertCount = vintageInsertCount + 1;
    
    insert into vintages.Observations
    (ObsDate, Open, High, Low, Close, VintageID)
    select distinct dtoo.obsDate, dtoo.value, dtoo.value, dtoo.value, dtoo.value, vintageID
    from FRED_Staging.Observations dtoo
    where dtoo.Symbol = symbol and dtoo.VintageDate = nextVintageDate;

	set obsInsertCount = obsInsertCount + row_count();    
    
end loop;
close dtoCursor;


end; -- //
-- delimiter ;

