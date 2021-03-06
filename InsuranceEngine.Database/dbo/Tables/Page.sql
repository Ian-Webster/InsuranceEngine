CREATE TABLE [dbo].[Page](
	[Page_ID] [int] IDENTITY(1,1) NOT NULL,
	[Page_Template_ID] [int] NOT NULL,
	[Scheme_ID] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[Next_Page_ID] [int] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Page_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Page] FOREIGN KEY([Next_Page_ID])
REFERENCES [dbo].[Page] ([Page_ID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Page]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Page_Template] FOREIGN KEY([Page_Template_ID])
REFERENCES [dbo].[Page_Template] ([Page_Template_ID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Page_Template]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Scheme] FOREIGN KEY([Scheme_ID])
REFERENCES [dbo].[Scheme] ([Scheme_ID])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Scheme]
GO
