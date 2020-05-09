CREATE TABLE [dbo].[Regions] (
	[RegionID]          INT        NOT NULL,
	[RegionDescription] NCHAR (50) NOT NULL,
	CONSTRAINT [PK_Regions] PRIMARY KEY NONCLUSTERED ([RegionID] ASC)
);