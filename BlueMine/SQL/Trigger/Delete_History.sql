

CREATE TRIGGER [dbo].[issues_trg_delete_history] ON [dbo].[issues] 
	FOR DELETE 
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
		,'delete' 
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
	FROM deleted
END 






GO

ALTER TABLE [dbo].[issues] ENABLE TRIGGER [issues_trg_delete_history]
GO


