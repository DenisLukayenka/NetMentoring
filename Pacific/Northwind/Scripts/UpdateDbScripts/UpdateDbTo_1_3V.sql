IF EXISTS (SELECT * FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U'))
BEGIN
	EXECUTE sp_rename
		@objname = N'[dbo].[Region]',
		@newname = N'Regions',
			@objtype = N'OBJECT';
END;

GO
IF EXISTS (SELECT * FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
BEGIN
	ALTER TABLE [dbo].[Customers]
		ADD [CreateDate] [datetime] NULL
END

