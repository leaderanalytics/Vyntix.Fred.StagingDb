-- delimiter //
drop procedure if exists SeriesTagsExport; --  //
create procedure SeriesTagsExport(dpID int, out insertCount int)
begin
set insertCount = 0;
set transaction isolation level read uncommitted;
insert into vintages.EntityTags
(EntityNativeID,EntityDataProviderID, TagNativeID, TagDataProviderID, AssociationDataProviderID, TaggableType)
select distinct
	s.NativeID,
	s.dataProviderID,
	tags.nativeID,
	dpID,
	dpID,
	1

from fred_staging.SeriesTags dto 
join vintages.Series s on dto.Symbol = s.NativeID
join vintages.Tags tags on concat(dto.Name, dto.GroupID) = tags.NativeID
where s.DataProviderID = dpID and tags.DataProviderID = dpID
and not exists(
	select 1 from vintages.EntityTags st 
	where st.EntityNativeID = s.NativeID
	and st.EntityDataProviderID = s.DataProviderID
	and  st.TagNativeID = tags.NativeID
	and st.TagDataProviderID = dpID
	and st.AssociationDataProviderID = dpID
	and st.TaggableType = 1
);

set insertCount = row_count();
end; -- //
-- delimiter ;