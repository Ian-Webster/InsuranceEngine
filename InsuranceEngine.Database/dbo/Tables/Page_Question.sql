CREATE TABLE [dbo].[Page_Question](
	[Page_Question_ID] [int] IDENTITY(1,1) NOT NULL,
	[Page_ID] [int] NOT NULL,
	[Question_ID] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[QuestionText] [nvarchar](250) NOT NULL,
	[QuestionName] [nvarchar](50) NOT NULL,
	[DefaultShow] [bit] NOT NULL,
 CONSTRAINT [PK_Page_Question] PRIMARY KEY CLUSTERED 
(
	[Page_Question_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Page_Question]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Page] FOREIGN KEY([Page_ID])
REFERENCES [dbo].[Page] ([Page_ID])
GO
ALTER TABLE [dbo].[Page_Question] CHECK CONSTRAINT [FK_Page_Question_Page]
GO
ALTER TABLE [dbo].[Page_Question]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Question] FOREIGN KEY([Question_ID])
REFERENCES [dbo].[Question] ([Question_ID])
GO
ALTER TABLE [dbo].[Page_Question] CHECK CONSTRAINT [FK_Page_Question_Question]
GO
