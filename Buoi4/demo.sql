USE [DEMO]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 9/5/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[ID] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[address] [nchar](10) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[avartar] [varchar](100) NOT NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[USERS] ([ID], [name], [address], [password], [avartar]) VALUES (1, N'Hai', N'TPHCM     ', N'e10adc3949ba59abbe56e057f20f883e', N'h1.jpg')
INSERT [dbo].[USERS] ([ID], [name], [address], [password], [avartar]) VALUES (2, N'Hoang', N'VT        ', N'e10adc3949ba59abbe56e057f20f883e', N'h2.jpg')
GO
