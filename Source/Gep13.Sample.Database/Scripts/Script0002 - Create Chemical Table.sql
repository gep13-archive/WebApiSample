/****** Object:  Table [dbo].[Chemicals]    Script Date: 10/06/2014 11:59:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Chemicals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Balance] [float] NULL,
 CONSTRAINT [PK_Chemicals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Chemicals] ADD  CONSTRAINT [DF_Chemicals_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO

ALTER TABLE [dbo].[Chemicals] ADD  CONSTRAINT [DF_Chemicals_Balance]  DEFAULT ((0.00)) FOR [Balance]
GO
