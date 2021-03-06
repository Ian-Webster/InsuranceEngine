CREATE TABLE [dbo].[Question_Template](
	[Question_Template_ID] [int] IDENTITY(1,1) NOT NULL,
	[Question_Type_ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Template] [nvarchar](max) NOT NULL,
	[LastRenderDate] [datetime] NULL,
 CONSTRAINT [PK_Question_Template] PRIMARY KEY CLUSTERED 
(
	[Question_Template_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Question_Template]  WITH CHECK ADD  CONSTRAINT [FK_Question_Template_Question_Type] FOREIGN KEY([Question_Type_ID])
REFERENCES [dbo].[Question_Type] ([Question_Type_ID])
GO
ALTER TABLE [dbo].[Question_Template] CHECK CONSTRAINT [FK_Question_Template_Question_Type]
GO
