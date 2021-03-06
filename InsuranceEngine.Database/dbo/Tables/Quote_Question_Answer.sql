CREATE TABLE [dbo].[Quote_Question_Answer](
	[Quote_Question_Answer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Quote_ID] [int] NOT NULL,
	[Question_ID] [int] NOT NULL,
	[Question_Possible_Answer_ID] [int] NULL,
	[Answer] [nvarchar](100) NULL,
 CONSTRAINT [PK_Quote_Question_Answer] PRIMARY KEY CLUSTERED 
(
	[Quote_Question_Answer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Quote_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Question_Answer_Question] FOREIGN KEY([Question_ID])
REFERENCES [dbo].[Question] ([Question_ID])
GO
ALTER TABLE [dbo].[Quote_Question_Answer] CHECK CONSTRAINT [FK_Quote_Question_Answer_Question]
GO
ALTER TABLE [dbo].[Quote_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Question_Answer_Question_Possible_Answer] FOREIGN KEY([Question_Possible_Answer_ID])
REFERENCES [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID])
GO
ALTER TABLE [dbo].[Quote_Question_Answer] CHECK CONSTRAINT [FK_Quote_Question_Answer_Question_Possible_Answer]
GO
ALTER TABLE [dbo].[Quote_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Question_Answer_Quote] FOREIGN KEY([Quote_ID])
REFERENCES [dbo].[Quote] ([Quote_ID])
GO
ALTER TABLE [dbo].[Quote_Question_Answer] CHECK CONSTRAINT [FK_Quote_Question_Answer_Quote]
GO
