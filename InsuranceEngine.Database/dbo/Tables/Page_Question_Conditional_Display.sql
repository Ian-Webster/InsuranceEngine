CREATE TABLE [dbo].[Page_Question_Conditional_Display](
	[Page_Question_Conditional_Display_ID] [int] IDENTITY(1,1) NOT NULL,
	[Source_Page_Question_ID] [int] NOT NULL,
	[Target_Page_Question_ID] [int] NOT NULL,
	[Trigger_Question_Possible_Answer_ID] [int] NOT NULL,
	[Hide] [bit] NOT NULL,
 CONSTRAINT [PK_Page_Question_Conditional_Display] PRIMARY KEY CLUSTERED 
(
	[Page_Question_Conditional_Display_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Conditional_Display_Page_Question] FOREIGN KEY([Source_Page_Question_ID])
REFERENCES [dbo].[Page_Question] ([Page_Question_ID])
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display] CHECK CONSTRAINT [FK_Page_Question_Conditional_Display_Page_Question]
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Conditional_Display_Page_Question1] FOREIGN KEY([Target_Page_Question_ID])
REFERENCES [dbo].[Page_Question] ([Page_Question_ID])
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display] CHECK CONSTRAINT [FK_Page_Question_Conditional_Display_Page_Question1]
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Conditional_Display_Question_Possible_Answer] FOREIGN KEY([Trigger_Question_Possible_Answer_ID])
REFERENCES [dbo].[Question_Possible_Answer] ([Question_Possible_Answer_ID])
GO
ALTER TABLE [dbo].[Page_Question_Conditional_Display] CHECK CONSTRAINT [FK_Page_Question_Conditional_Display_Question_Possible_Answer]
GO
