-- delimiter //
drop procedure if exists fred_staging.ToggleSeriesIndex; -- //
create procedure fred_staging.ToggleSeriesIndex(enableIndex bit)
begin
declare indexExists bit;
set indexExists = exists(SELECT 1 FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Series' AND INDEX_NAME = 'IX_series_Symbol' limit 1); 

if enableIndex = 1 and indexExists = 0 then
	alter table fred_staging.Series add index IX_series_Symbol (symbol);
elseif enableIndex = 0 and indexExists = 1 then
	alter table fred_staging.Series drop index IX_series_Symbol;
end if;

end; -- //
-- delimiter ;
