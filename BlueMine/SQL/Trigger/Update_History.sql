


CREATE TRIGGER [dbo].[issues_trg_update_history] ON [dbo].[issues] 
	FOR UPDATE   
AS 
BEGIN 
	DECLARE @operation_time AS datetime = CURRENT_TIMESTAMP 
	SET NOCOUNT ON; 


	-- INSERT INTO dbo.issues_history 
	-- SELECT SUSER_SNAME(), ''update-deleted'', @operation_time, * 
	-- FROM deleted;

	INSERT INTO dbo.issues_history
	(
		 operation_dbuser
		,operation_name
		,operation_time
		,id
		,tracker_id
		,project_id
		,subject
		,description
		,due_date
		,category_id
		,status_id
		,assigned_to_id
		,priority_id
		,fixed_version_id
		,author_id
		,lock_version
		,created_on
		,updated_on
		,start_date
		,done_ratio
		,estimated_hours
		,parent_id
		,root_id
		,lft
		,rgt
		,is_private
		,closed_on
	)
	SELECT 
		 SUSER_SNAME() 
		,'update-deleted' 
		,@operation_time
		,id
		,tracker_id
		,project_id
		,subject
		,description
		,due_date
		,category_id
		,status_id
		,assigned_to_id
		,priority_id
		,fixed_version_id
		,author_id
		,lock_version
		,created_on
		,updated_on
		,start_date
		,done_ratio
		,estimated_hours
		,parent_id
		,root_id
		,lft
		,rgt
		,is_private
		,closed_on 
	FROM deleted; 


	-- INSERT INTO dbo.issues_history 
	-- SELECT SUSER_SNAME(), ''update-inserted'', @operation_time, * 
	-- FROM inserted; 


	INSERT INTO dbo.issues_history
	(
		 operation_dbuser
		,operation_name
		,operation_time
		,id
		,tracker_id
		,project_id
		,subject
		,description
		,due_date
		,category_id
		,status_id
		,assigned_to_id
		,priority_id
		,fixed_version_id
		,author_id
		,lock_version
		,created_on
		,updated_on
		,start_date
		,done_ratio
		,estimated_hours
		,parent_id
		,root_id
		,lft
		,rgt
		,is_private
		,closed_on
	)
	SELECT 
		 SUSER_SNAME() 
		,'update-inserted' 
		,@operation_time
		,id
		,tracker_id
		,project_id
		,subject
		,description
		,due_date
		,category_id
		,status_id
		,assigned_to_id
		,priority_id
		,fixed_version_id
		,author_id
		,lock_version
		,created_on
		,updated_on
		,start_date
		,done_ratio
		,estimated_hours
		,parent_id
		,root_id
		,lft
		,rgt
		,is_private
		,closed_on 
	FROM inserted; 


END 
GO

ALTER TABLE [dbo].[issues] ENABLE TRIGGER [issues_trg_update_history]
GO


