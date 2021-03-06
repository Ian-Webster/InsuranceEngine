CREATE TABLE [dbo].[Question](
	[Question_ID] [int] IDENTITY(1,1) NOT NULL,
	[Scheme_ID] [int] NOT NULL,
	[Question_Template_ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Question_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Question_Template] FOREIGN KEY([Question_Template_ID])
REFERENCES [dbo].[Question_Template] ([Question_Template_ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Question_Template]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Scheme] FOREIGN KEY([Scheme_ID])
REFERENCES [dbo].[Scheme] ([Scheme_ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Scheme]
GO
