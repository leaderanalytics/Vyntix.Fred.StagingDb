create procedure TagsExport(@dpID int, @insertCount int output, @updateCount int output) as
begin
declare @dtoTags table
(
	nativeID varchar(100) unique,
	name varchar(100),
	groupid varchar(100),
	createdate datetime,
	popularity integer
);
set @insertCount = 0;
set @updateCount = 0;


insert into @dtoTags (nativeID, name, groupid, createdate, popularity)
select concat(name,groupid) as nativeID, name = case when (len(notes) > 0) then notes else name end, groupid, createddate, popularity from CategoryTags
	union 
select concat(name,groupid) as nativeID, name = case when (len(notes) > 0) then notes else name end, groupid, createddate, popularity from SeriesTags;

insert into Vintages.dbo.TagGroups (NativeID, DataProviderID, Name)
select distinct groupid, @dpID, groupid from @dtoTags
where not exists(select 1 from vintages.dbo.TagGroups tags3 where tags3.NativeID = [@dtoTags].groupid and tags3.DataProviderID = @dpID );

update 
	tags
set
	tags.DateCreated = [@dtoTags].createdate,
    tags.popularity = [@dtoTags].popularity
from
	vintages.dbo.tags tags
	join
    @dtoTags on tags.NativeID = [@dtoTags].nativeID and tags.DataProviderID = @dpID;


set @updateCount = @@ROWCOUNT;

insert into vintages.dbo.tags
	(dataproviderid, dateCreated, tagGroupNativeID, tagGroupDataProviderID, name, nativeID, popularity)
select 
	@dpID, [@dtoTags].createdate, [@dtoTags].groupID, @dpID, [@dtoTags].name, [@dtoTags].nativeID, [@dtoTags].popularity
from 
	@dtoTags
where not exists(select 1 from vintages.dbo.tags tags3 where tags3.NativeID = [@dtoTags].nativeID and tags3.DataProviderID = @dpID );

set @insertCount = @@ROWCOUNT;
end;