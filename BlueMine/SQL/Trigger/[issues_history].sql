

CREATE TABLE [dbo].[issues_history]
(
	[issue_history_id] [int] IDENTITY(1,1) NOT NULL,
	[operation_dbuser] [nvarchar](128) NULL,
	[operation_name] [nvarchar](128) NULL,
	[operation_time] [datetime] NULL,
	[id] [int] NOT NULL,
	[tracker_id] [int] NOT NULL,
	[project_id] [int] NOT NULL,
	[subject] [nvarchar](4000) NOT NULL,
	[description] [nvarchar](max) NULL,
	[due_date] [date] NULL,
	[category_id] [int] NULL,
	[status_id] [int] NOT NULL,
	[assigned_to_id] [int] NULL,
	[priority_id] [int] NOT NULL,
	[fixed_version_id] [int] NULL,
	[author_id] [int] NOT NULL,
	[lock_version] [int] NOT NULL,
	[created_on] [datetime] NULL,
	[updated_on] [datetime] NULL,
	[start_date] [date] NULL,
	[done_ratio] [int] NOT NULL,
	[estimated_hours] [float] NULL,
	[parent_id] [int] NULL,
	[root_id] [int] NULL,
	[lft] [int] NULL,
	[rgt] [int] NULL,
	[is_private] [bit] NOT NULL,
	[closed_on] [datetime] NULL,
	CONSTRAINT [PK_issues_history] PRIMARY KEY ( [issue_history_id] )
); 
GO


