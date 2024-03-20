
CREATE TABLE [dbo].[worker](
	[wr_uid] [uniqueidentifier] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[workplace](
	[WP_UID] [uniqueidentifier] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[assignment](
	[ass_uid] [uniqueidentifier] NOT NULL,
	[ass_wp_uid] [uniqueidentifier] NOT NULL,
	[ass_wr_uid] [uniqueidentifier] NOT NULL,
	[ass_range_from] [date] NULL,
	[ass_range_to] [date] NULL,
	[date_from] [date] NULL,
	[date_to] [date] NULL
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[assignments_normalized](
	[ass_uid] [uniqueidentifier] NULL,
	[assignment_day] [int] NULL,
	[time_from] [time](7) NULL,
	[time_to] [time](7) NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[assignment_day](
	[day_id] [int] NULL,
	[day_name] [nvarchar](100) NULL
) ON [PRIMARY]
GO

