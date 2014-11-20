/****** Object:  Table [dbo].[HazardInfos]    Script Date: 10/06/2014 11:59:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].HazardInfos(
	[ChemicalId] [int] NOT NULL,
	[Name] [varchar](128) NULL,
	[Danger] [varchar](128) NULL,
	[RowVersion] ROWVERSION NOT NULL,
);

GO
CREATE NONCLUSTERED INDEX [IX_ChemicalId]
	ON [dbo].[HazardInfos]([ChemicalId] ASC);

GO
ALTER TABLE [dbo].[HazardInfos]
	ADD CONSTRAINT [PK_dbo.HazardInfo] PRIMARY KEY CLUSTERED ([ChemicalId] ASC);

GO
ALTER TABLE [dbo].[HazardInfos]
	ADD CONSTRAINT [FK_dbo.HazardInfos_dbo.Chemicals_ChemicalId] FOREIGN KEY ([ChemicalId]) REFERENCES [dbo].[Chemicals]([Id]);