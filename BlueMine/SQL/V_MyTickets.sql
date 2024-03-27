
CREATE VIEW COR.V_MyTickets AS 
-- ALTER VIEW COR.V_MyTickets AS 
SELECT TOP 99999999 
	 issues.id 
	,projects.name AS project 
	,issues.subject 
	-- ,issues.description 

	,issue_statuses.name AS status 
	,issue_priorities.name AS prio 
	-- ,assigned_user.login 
	-- ,author.login AS author 
	,LTRIM(RTRIM(ISNULL(NULLIF(author.firstname, '') + ' ', '') + ISNULL(author.lastname, ''))) AS author 
	,calc_reported_by.reported_by AS reported_by 
	 
	,trackers.name AS tracker 
	-- ,issue_categories.name AS category 
	
	,
	(
		SELECT 
			value 
		FROM settings 
		WHERE name = 'protocol' 
	) 
	+ '://' 
	+
	(
		SELECT 
			value 
		FROM settings 
		WHERE name = 'host_name' 
	)
	+ '/issues/' + CAST(issues.id AS varchar(36)) AS link 

	,issues.estimated_hours 
	,issues.due_date 
	,issues.done_ratio 
	,issues.created_on 
	 
	-- ,issues.project_id 
	-- ,issues.subject 
	-- ,issues.description 
	
	-- ,issues.fixed_version_id 
	-- ,issues.updated_on 
	-- ,issues.start_date 
	-- ,issues.parent_id 
	-- ,issues.root_id 
	-- ,issues.lft 
	-- ,issues.rgt 
	-- ,issues.is_private 
	-- ,issues.closed_on 
FROM issues 

LEFT JOIN projects ON projects.id = issues.project_id 

LEFT JOIN users AS assigned_user ON assigned_user.id = issues.assigned_to_id 
LEFT JOIN users AS author ON author.id = issues.author_id 

LEFT JOIN issue_statuses ON issue_statuses.id = issues.status_id 
LEFT JOIN trackers ON trackers.id = issues.tracker_id 
LEFT JOIN issue_categories ON issue_categories.id = issues.category_id 

LEFT JOIN enumerations AS issue_priorities 
	ON issue_priorities.type = 'IssuePriority' 
	AND issue_priorities.id = issues.priority_id 
	
OUTER APPLY 
	(
		SELECT TOP 1 
			value AS reported_by 
		FROM custom_values 
		WHERE custom_values.custom_field_id = 1 -- gemeldet von -- kommt von Tabelle custom_fields 
		AND custom_values.customized_id = issues.id 
	) AS calc_reported_by 
	
WHERE (1=1) 
AND assigned_user.id = 5 
AND issue_statuses.is_closed <> 1 
AND issue_statuses.name NOT IN 
(
	 'Erledigt'
	,'Installiert (Test)'
	,'Installiert (produktiv)'
	,'In Abklärung (Kunde)'
	,'In Abklärung (COR)'
	,'unbestimmt aufgeschoben'
) 

ORDER BY 
	 issue_priorities.id  
	,projects.name 
	,issues.id  
	 
GO

