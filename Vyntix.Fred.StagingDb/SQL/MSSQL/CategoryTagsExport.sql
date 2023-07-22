create procedure CategoryTagsExport  @dataProviderID int, @insertCount int output
as
begin
set nocount on;


insert into Vintages.dbo.EntityTags
(EntityNativeID, EntityDataProviderID, TagNativeID, TagDataProviderID, AssociationDataProviderID, TaggableType)
select distinct
	cats.nativeID,
	cats.dataProviderID,
	tags.nativeID,
	@dataProviderID,
	@dataProviderID,
	0
from fred_staging.dbo.CategoryTags fredTags (nolock)
join Vintages.dbo.Categories cats on fredTags.CategoryID = cats.NativeID 
join Vintages.dbo.Tags tags on fredTags.Name + fredTags.GroupID = tags.NativeID
where cats.DataProviderID = @dataProviderID and tags.DataProviderID = @dataProviderID
and not exists(
	select 1 from Vintages.dbo.EntityTags ct (nolock)
	where ct.EntityNativeID = cats.NativeID 
	and ct.EntityDataProviderID = cats.DataProviderID
	and  ct.TagNativeID = tags.NativeID
	and ct.TagDataProviderID = @dataProviderID
	and ct.AssociationDataProviderID = @dataProviderID
	and ct.TaggableType = 0
	)

 

select @insertCount = @@ROWCOUNT;
end