CREATE TABLE [dbo].[Rendered_Page](
	[Rendered_Page_ID] [int] IDENTITY(1,1) NOT NULL,
	[Page_ID] [int] NOT NULL,
	[PageContent] [nvarchar](max) NOT NULL,
	[LastRenderDate] [datetime] NULL,
 CONSTRAINT [PK_Rendered_Page] PRIMARY KEY CLUSTERED 
(
	[Rendered_Page_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Rendered_Page]  WITH CHECK ADD  CONSTRAINT [FK_Rendered_Page_Page] FOREIGN KEY([Page_ID])
REFERENCES [dbo].[Page] ([Page_ID])
GO
ALTER TABLE [dbo].[Rendered_Page] CHECK CONSTRAINT [FK_Rendered_Page_Page]
GO
