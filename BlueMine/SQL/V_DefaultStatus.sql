
CREATE VIEW COR.V_DefaultStatus AS 
SELECT TOP 99999999 
	 issue_statuses.id 
	,issue_statuses.name 
	,issue_statuses.position 
	,issue_statuses.is_closed 
FROM issue_statuses 
WHERE (1=1) 
AND issue_statuses.is_closed <> 1 
AND issue_statuses.name NOT IN 
( 
	 'Erledigt'
	,'Installiert (Test)'
	,'Installiert (produktiv)'
	,'In Abklärung (Kunde)'
	-- ,'In Abklärung (COR)'
	,'unbestimmt aufgeschoben'
) 

ORDER BY position 
