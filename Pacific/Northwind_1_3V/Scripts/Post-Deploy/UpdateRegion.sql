IF EXISTS (SELECT * FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U'))
BEGIN
	INSERT INTO [dbo].[Regions]
	SELECT * FROM [dbo].[Region]

	DROP TABLE [dbo].[Region]
END
