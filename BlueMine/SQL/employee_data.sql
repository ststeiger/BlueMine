USE [NestedSet]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 20.03.2024 10:07:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] NOT NULL,
	[ManagerID] [int] NULL,
	[DisplayName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (1, 3, N'Vivian')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (2, 26, N'Lynne')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (3, 26, N'Bob')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (6, 17, N'Eric')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (8, 3, N'Bill')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (12, 8, N'Megan')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (14, 8, N'Kim')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (17, 2, N'Butch')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (18, 39, N'Lisa')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (20, 3, N'Natalie')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (21, 39, N'Hemer')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (26, NULL, N'Jim')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (39, 26, N'Ken')
GO
INSERT [dbo].[Employee] ([EmployeeID], [ManagerID], [DisplayName]) VALUES (40, 26, N'Marge')
GO
