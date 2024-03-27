
-- ==================================================================
-- Author:		  Stefan Steiger 
-- E-Mail:        stefan.steiger@cor-management.ch 
-- Create date:   27.03.2024 Stefan Steiger
-- Description:	  Get issues by filter criteria
-- ==================================================================
ALTER PROCEDURE COR.get_issues_by_assigned_and_status 
	  @in_issue_assigned_to int 
	 ,@in_issue_status varchar(MAX) 
AS 
BEGIN 
	SET FMTONLY OFF; 
	SET NOCOUNT ON; 

	SET @in_issue_status = ',' + @in_issue_status + ','; 
	

	DECLARE @status_selection table(selected_status_id int NOT NULL PRIMARY KEY) ; 
	INSERT INTO @status_selection(selected_status_id) 
	SELECT 
		 issue_statuses.id AS selected_status_id 
	FROM issue_statuses 
	WHERE @in_issue_status LIKE '%,' + CAST(issue_statuses.id AS varchar(36))+ ',%' 
	; 

	

	

	-- CREATE VIEW COR.V_StefansTickets AS 
	-- ALTER VIEW COR.V_StefansTickets AS 
	SELECT TOP 99999999 
		 issues.id 
		,projects.name AS project 
		,issues.subject 
		-- ,issues.description 

		,issue_statuses.name AS status 
		,

		 CASE 
			WHEN    projects.name LIKE '%check%' 
				 OR issues.subject LIKE '%check%' 
				 OR issues.description LIKE '%check%' 
				THEN 'Vorrangig' 
			ELSE issue_priorities.name 
		 END AS prio 

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
	-- AND assigned_user.id = 5 -- stefan.steiger 
	AND assigned_user.id = @in_issue_assigned_to 
	AND issues.id NOT IN 
	(
		 3452 -- Räume erscheinen doppelt in Report "Raumliste nach Nutzungsart" - ist dies nicht schon erledigt ? 
		,3245 -- Portfoliorechte Bericht "Export Rohdaten"  - ist glaube ich erledigt 
		,47, 48 -- Redmine Mails von Zihlmann - nicht möglich wenn keine separaten Kundenprojekte 
		,3729 -- Stichtag Swisscom ist mit Update ZH erledigt 
	)

	AND issue_statuses.id IN ( SELECT selected_status_id FROM @status_selection ) 

	-- AND issue_statuses.is_closed <> 1 
	-- AND issue_statuses.name NOT IN 
	-- (
	--	  'Erledigt'
	--	 ,'Installiert (Test)'
	--	 ,'Installiert (produktiv)'
	--	 ,'In Abklärung (Kunde)'
	--	 ,'In Abklärung (COR)'
	--	 ,'unbestimmt aufgeschoben'
	-- ) 

	ORDER BY 
		 
		 CASE 
			WHEN issues.priority_id  < 3 AND issues.id <> 3593 THEN 0 
			WHEN issue_statuses.name = 'Fehlerhaft' THEN 1 
			WHEN issue_statuses.name = 'In Bearbeitung' THEN 2 

			WHEN projects.name LIKE '%check%' THEN 3
			WHEN issues.subject LIKE '%check%' THEN 3 
			WHEN issues.description LIKE '%check%' THEN 3
			ELSE 4
		 END 

		,issue_priorities.id 
		,issues.due_date ASC 
		,projects.name 
		,issues.id 

END 


GO




