CREATE TABLE [dbo].[Page_Question_Validation](
	[Page_Question_Validation_ID] [int] IDENTITY(1,1) NOT NULL,
	[Page_Question_ID] [int] NOT NULL,
	[Validation_Type_ID] [int] NOT NULL,
	[ErrorMessage] [nvarchar](1024) NOT NULL,
	[ValidationExpression] [nvarchar](max) NULL,
 CONSTRAINT [PK_Page_Question_Validation] PRIMARY KEY CLUSTERED 
(
	[Page_Question_Validation_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Page_Question_Validation]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Validation_Page_Question] FOREIGN KEY([Page_Question_ID])
REFERENCES [dbo].[Page_Question] ([Page_Question_ID])
GO
ALTER TABLE [dbo].[Page_Question_Validation] CHECK CONSTRAINT [FK_Page_Question_Validation_Page_Question]
GO
ALTER TABLE [dbo].[Page_Question_Validation]  WITH CHECK ADD  CONSTRAINT [FK_Page_Question_Validation_Validation_Type] FOREIGN KEY([Validation_Type_ID])
REFERENCES [dbo].[Validation_Type] ([Validation_Type_ID])
GO
ALTER TABLE [dbo].[Page_Question_Validation] CHECK CONSTRAINT [FK_Page_Question_Validation_Validation_Type]
GO
