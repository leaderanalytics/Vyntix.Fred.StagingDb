create procedure SeriesTagsExport  @dataProviderID int, @insertCount int output
as
begin
set nocount on;

insert into Vintages.dbo.EntityTags
(EntityNativeID, EntityDataProviderID, TagNativeID, TagDataProviderID, AssociationDataProviderID, TaggableType)
select distinct
	s.NativeID,
	s.dataProviderID,
	tags.nativeID,
	@dataProviderID,
	@dataProviderID,
	1

from fred_staging.dbo.SeriesTags dto (nolock)
join Vintages.dbo.Series s on dto.Symbol = s.NativeID
join Vintages.dbo.Tags tags on dto.Name + dto.GroupID = tags.NativeID
where s.DataProviderID = @dataProviderID and tags.DataProviderID = @dataProviderID
and not exists(
	select 1 from Vintages.dbo.EntityTags st (nolock)
	where st.EntityNativeID = s.NativeID
	and st.EntityDataProviderID = s.DataProviderID
	and  st.TagNativeID = tags.NativeID
	and st.TagDataProviderID = @dataProviderID
	and st.AssociationDataProviderID = @dataProviderID
	and st.TaggableType = 1
)

select @insertCount = @@ROWCOUNT;
end