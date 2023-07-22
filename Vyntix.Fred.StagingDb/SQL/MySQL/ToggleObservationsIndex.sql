-- delimiter //
drop procedure if exists fred_staging.ToggleObservationsIndex; -- //
create procedure fred_staging.ToggleObservationsIndex(enableIndex bit)
begin
declare indexExists bit;
set indexExists = exists(SELECT 1 FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Observations' AND INDEX_NAME = 'IX_Observations_Symbol_VintageDate' limit 1); 

if enableIndex = 1 and indexExists = 0 then
	alter table fred_staging.Observations add index IX_Observations_Symbol_VintageDate (symbol, VintageDate);
elseif enableIndex = 0 and indexExists = 1 then
	alter table fred_staging.Observations drop index IX_Observations_Symbol_VintageDate;
end if;

end; -- //
-- delimiter ;


