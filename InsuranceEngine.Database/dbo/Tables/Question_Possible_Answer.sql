CREATE TABLE [dbo].[Question_Possible_Answer](
	[Question_Possible_Answer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Question_ID] [int] NOT NULL,
	[AnswerText] [nvarchar](50) NOT NULL,
	[AnswerValue] [nvarchar](20) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_Question_Possible_Answer] PRIMARY KEY CLUSTERED 
(
	[Question_Possible_Answer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Question_Possible_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Question_Possible_Answer_Question] FOREIGN KEY([Question_ID])
REFERENCES [dbo].[Question] ([Question_ID])
GO
ALTER TABLE [dbo].[Question_Possible_Answer] CHECK CONSTRAINT [FK_Question_Possible_Answer_Question]
GO
