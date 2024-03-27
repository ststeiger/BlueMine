
;WITH CTE AS 
(
	SELECT 
		 issue_statuses.name AS issue_status_name
		,issue_status_neu.id AS status_new_id 
		,status_abklaerung_cor.id AS status_abklaerung_cor_id 
		
		-- ,tracker_anpassung.name AS tracker_anpassung 
		,tracker_anpassung.id AS tracker_anpassung_id 
		  
		,issues.* 
	FROM issues 

	LEFT JOIN issue_statuses ON issue_statuses.id = issues.status_id 

	-- SELECT * FROM 
	LEFT JOIN issue_statuses AS issue_status_neu ON issue_status_neu.name = 'Neu' 

	LEFT JOIN trackers AS tracker_anpassung ON tracker_anpassung.name = 'Anpassung'
	LEFT JOIN issue_statuses AS status_abklaerung_cor ON status_abklaerung_cor.name = 'In Abklärung (COR)'
	
	WHERE issues.id = 2898  
)
SELECT * FROM CTE 
-- UPDATE CTE SET status_id = status_new_id 
-- UPDATE CTE SET tracker_id = tracker_anpassung_id 
-- UPDATE CTE SET status_id = status_abklaerung_cor_id 



-- SELECT * FROM issue_statuses 

-- http://cordb2022/ReportServer/Pages/ReportViewer.aspx?%2fRedmine%2fPendenzen&rs:Command=Render&rc:LinkTarget=_blank
