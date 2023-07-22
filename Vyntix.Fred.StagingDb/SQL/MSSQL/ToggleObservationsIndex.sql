create procedure ToggleObservationsIndex @enable bit
as begin
declare @exists bit;
select @exists = isnull((SELECT 1 FROM SYS.INDEXES WHERE NAME = 'IX_Observations_Symbol_VintageDate' AND OBJECT_ID = OBJECT_ID('[dbo].[Observations]')), 0);

if @enable = 1 and @exists = 0
begin
	create nonclustered index [IX_Observations_Symbol_VintageDate] ON [dbo].[Observations] 
	(
		[Symbol] asc,
		[VintageDate] asc
	)with (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
end else if @enable = 0 and @exists = 1
begin
	drop index [IX_Observations_Symbol_VintageDate] ON [dbo].[Observations];
end

end
