

CREATE TABLE [dbo].[nourishment](
	[nour_id] [int] NOT NULL,
	[nour_parent] [int] NULL,
	[nour_name] [nvarchar](255) NULL,
 CONSTRAINT [PK_nourishment] PRIMARY KEY ([nour_id] )
) ;
GO



INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (1, NULL, N'Food');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (2, 1, N'Fruit');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (3, 2, N'Red');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (4, 3, N'Cherry');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (5, 2, N'Yellow');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (6, 5, N'Banana');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (7, 1, N'Meat');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (8, 7, N'Beef');
INSERT [dbo].[nourishment] ([nour_id], [nour_parent], [nour_name]) VALUES (9, 7, N'Pork');
