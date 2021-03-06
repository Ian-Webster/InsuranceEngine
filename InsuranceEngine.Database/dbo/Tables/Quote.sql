CREATE TABLE [dbo].[Quote](
	[Quote_ID] [int] IDENTITY(1,1) NOT NULL,
	[Scheme_ID] [int] NOT NULL,
	[Reference] [nvarchar](50) NOT NULL,
	[QuoteDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED 
(
	[Quote_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Scheme] FOREIGN KEY([Scheme_ID])
REFERENCES [dbo].[Scheme] ([Scheme_ID])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_Scheme]
GO
