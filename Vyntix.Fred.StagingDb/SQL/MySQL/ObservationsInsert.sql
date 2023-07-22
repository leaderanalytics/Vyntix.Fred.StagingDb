-- delimiter //
drop procedure if exists ObservationsInsert; -- //
create procedure ObservationsInsert(obsDate datetime, symbol char(50), val char(50), vintageDate datetime)
begin
INSERT INTO fred_staging.Observations
	(ObsDate, Symbol, Value, VintageDate)
VALUES
	(obsDate, symbol, val, vintageDate);
end; -- //
-- delimiter; 
