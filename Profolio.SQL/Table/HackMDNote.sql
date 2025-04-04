CREATE TABLE [dbo].[HackMDNote](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [varchar](50) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](200) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Tags] [nvarchar](255) NULL,
	[Author] [nvarchar](255) NULL,
	[PublishedAt] [datetime] NULL,
	[PublishLink] [nvarchar](100) NULL,
	[Views] INT DEFAULT 0 NOT NULL
 CONSTRAINT [PK__HackMDNo__3214EC07CF5D98DA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HackMDNote] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[HackMDNote] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO