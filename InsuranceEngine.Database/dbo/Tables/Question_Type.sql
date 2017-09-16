CREATE TABLE [dbo].[Question_Type](
	[Question_Type_ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HasPossibleAnswers] [bit] NOT NULL,
 CONSTRAINT [PK_Question_Type] PRIMARY KEY CLUSTERED 
(
	[Question_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
