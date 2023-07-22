-- delimiter //
drop procedure if exists fred_staging.TagsExport;  -- //
create procedure fred_staging.TagsExport(dpID int, out insertCount int, out updateCount int)
begin
set insertCount = 0;
set updateCount = 0;
create temporary table dtoTags 
(
	nativeID varchar(100) unique,
	name varchar(100),
	groupid varchar(100),
	createdate datetime,
	popularity integer
);


insert into dtoTags (nativeID, name, groupid, createdate, popularity)
select concat(name,groupid) as nativeID, case when (length(notes) > 0) then notes else name end as name, groupid, createddate, popularity from CategoryTags
	union 
select concat(name,groupid) as nativeID, case when (length(notes) > 0) then notes else name end as name, groupid, createddate, popularity from SeriesTags;

insert into vintages.TagGroups (NativeID, DataProviderID, Name)
select distinct groupid, dpID, groupid  from dtoTags
where not exists(select 1 from vintages.TagGroups tags3 where tags3.NativeID = dtoTags.groupid and tags3.DataProviderID = dpID );


update 
	vintages.tags tags
	join dtoTags on tags.NativeID = dtoTags.nativeID and tags.DataProviderID = dpID
set
	tags.DateCreated = dtoTags.createdate,
    tags.popularity = dtoTags.popularity;

set updateCount = row_count();


insert into vintages.tags
	(dataproviderid, dateCreated, tagGroupNativeID, tagGroupDataProviderID, name, nativeID, popularity)
select 
	dpID, dtoTags.createdate, dtoTags.groupID, dpID, dtoTags.name, dtoTags.nativeID, dtoTags.popularity
from 
	dtoTags
where not exists(select 1 from vintages.tags tags3 where tags3.NativeID = dtoTags.nativeID and tags3.DataProviderID = dpID );
 

set insertCount = row_count();
end; -- //
-- delimiter ;