
CREATE VIEW COR.V_All_Status AS 
SELECT TOP 99999999 
	 issue_statuses.id 
	,issue_statuses.name 
	,issue_statuses.position 
	,issue_statuses.is_closed 
FROM issue_statuses 
WHERE (1=1) 

ORDER BY position 
