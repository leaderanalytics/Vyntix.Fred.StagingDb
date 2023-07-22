-- delimiter //
drop procedure if exists fred_staging.CategoryTagsExport; -- //
create procedure fred_staging.CategoryTagsExport(dataProviderID int, out insertCount int)
begin
set insertCount = 0;

insert into vintages.EntityTags
(EntityNativeID, EntityDataProviderID, TagNativeID, TagDataProviderID, AssociationDataProviderID, TaggableType)
select distinct
	cats.nativeID,
	cats.dataProviderID,
	tags.nativeID,
	dataProviderID,
	dataproviderID,
	0

from fred_staging.CategoryTags fredTags 
join vintages.Categories cats on fredTags.CategoryID = cats.NativeID
join vintages.Tags tags on concat(fredTags.Name, fredTags.GroupID) = tags.NativeID
where cats.DataProviderID = dataProviderID and tags.DataProviderID = dataProviderID
and not exists(
	select 1 from vintages.EntityTags ct 
	where ct.EntityNativeID = cats.NativeID 
	and ct.EntityDataProviderID = cats.DataProviderID
	and  ct.TagNativeID = tags.NativeID
	and ct.TagDataProviderID = dataProviderID
	and ct.AssociationDataProviderID = dataProviderID
	and ct.TaggableType = 0
	);

 

set insertCount = row_count();
end;  -- //
-- delimiter ;