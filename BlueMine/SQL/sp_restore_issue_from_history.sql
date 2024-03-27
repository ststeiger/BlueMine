

-- ==================================================================
-- Author:		  Stefan Steiger 
-- E-Mail:        
-- Create date:   27.09.2023 Stefan Steiger
-- Description:	  Restore issue from history 
-- ==================================================================
CREATE PROCEDURE COR.sp_restore_issue_from_history 
	 @in_issue int 
AS 
BEGIN 
	SET FMTONLY OFF; 
	SET NOCOUNT ON; 
	

	
	SET IDENTITY_INSERT [dbo].[issues] ON ;


	INSERT INTO [dbo].[issues]
	(
		 id
		,[tracker_id]
		,[project_id]
		,[subject]
		,[description]
		,[due_date]
		,[category_id]
		,[status_id]
		,[assigned_to_id]
		,[priority_id]
		,[fixed_version_id]
		,[author_id]
		,[lock_version]
		,[created_on]
		,[updated_on]
		,[start_date]
		,[done_ratio]
		,[estimated_hours]
		,[parent_id]
		,[root_id]
		,[lft]
		,[rgt]
		,[is_private]
		,[closed_on]
	)
	SELECT TOP 1 [id]
		  ,[tracker_id]
		  ,[project_id]
		  ,[subject]
		  ,[description]
		  ,[due_date]
		  ,[category_id]
		  ,[status_id]
		  ,[assigned_to_id]
		  ,[priority_id]
		  ,[fixed_version_id]
		  ,[author_id]
		  ,[lock_version]
		  ,[created_on]
		  ,[updated_on]
		  ,[start_date]
		  ,[done_ratio]
		  ,[estimated_hours]
		  ,[parent_id]
		  ,[root_id]
		  ,[lft]
		  ,[rgt]
		  ,[is_private]
		  ,[closed_on]
	  FROM [Redmine].[dbo].issues_history
	  WHERE id = @in_issue 
	  ORDER BY updated_on DESC 
  ;

  SET IDENTITY_INSERT [dbo].[issues] OFF;

END 


GO


