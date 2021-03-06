CREATE TABLE [dbo].[Validation_Type](
	[Validation_Type_ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DataType] [nvarchar](100) NULL,
 CONSTRAINT [PK_Validation_Type] PRIMARY KEY CLUSTERED 
(
	[Validation_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
