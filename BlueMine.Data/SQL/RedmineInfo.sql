
SELECT 
     [id]
    ,[name]
    ,[is_closed]
    ,[position]
    ,[default_done_ratio]
FROM [Redmine].[dbo].[issue_statuses]



SELECT * FROM [enumerations] WHERE [enumerations].[type] 
IN (N'IssuePriority') AND [enumerations].[project_id] IS NULL




SELECT [id]
      ,[name]
      ,[position]
      ,[assignable]
      ,[builtin]
      ,[permissions]
      ,[issues_visibility]
      ,[users_visibility]
      ,[time_entries_visibility]
      ,[all_roles_managed]
  FROM [Redmine].[dbo].[roles]

/*
  ---- :view_calendar- 
  :view_documents- 
  :view_gantt- 
  :save_queries
*/



SELECT [enumerations].* 
FROM [enumerations] 
WHERE [enumerations].[type] IN (N'TimeEntryActivity') 
AND [enumerations].[project_id] IS NULL  ORDER BY [enumerations].[position] ASC





SELECT  [user_preferences].* FROM [user_preferences] 
WHERE [user_preferences].[user_id] = 1 -- @0  
ORDER BY [user_preferences].[id] ASC 
OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY




EXEC sp_executesql N'SELECT [projects].[id], [projects].[name]
, [projects].[identifier], [projects].[lft], [projects].[rgt] 
FROM [projects] 
INNER JOIN [members] ON [projects].[id] = [members].[project_id] 
WHERE [members].[user_id] = @0 
AND (projects.status<>9) 
AND [projects].[status] = @1
', N'@0 int, @1 int', @0 = 1, @1 = 1        



SELECT [enumerations].* 
FROM [enumerations] 
WHERE [enumerations].[type] IN (N'DocumentCategory') 
AND [enumerations].[project_id] IS NULL  
ORDER BY [enumerations].[position] ASC 



SELECT * FROM [enumerations]

SELECT [custom_fields].* FROM [custom_fields]



EXEC sp_executesql N'SELECT *
FROM [projects] 
INNER JOIN [custom_fields_projects] 
ON [projects].[id] = [custom_fields_projects].[project_id] 
WHERE [custom_fields_projects].[custom_field_id] = @0
', N'@0 int', @0 = 2   

SELECT * FROM custom_fields_projects 
