
CREATE VIEW COR.V_SearchIssues AS 
SELECT 
	 issues.id 
	,issues.project_id 
	,subject AS ticket_titel 
	,description AS ticket_beschreibung 
	,CAST(NULL AS nvarchar(MAX)) AS kommentar 
FROM issues 
-- WHERE (1=2) 
-- OR subject LIKE '%swisscom%' 
-- OR description LIKE '%T_AP_Ref_DokumentKategorie%' 


UNION ALL 


SELECT 
	 issues.id 
	,issues.project_id 
	,issues.subject AS ticket_titel 
	,issues.description AS ticket_beschreibung 
	,notes AS kommentar 
FROM journals 
LEFT JOIN issues ON issues.id = journals.journalized_id 
-- WHERE (1=2) 
-- OR notes LIKE '%T_AP_Ref_DokumentKategorie%'


/*
SELECT [id]
      ,[project_id]
      ,[ticket_titel]
      ,[ticket_beschreibung]
      ,[kommentar]
  FROM SearchIssues 
  WHERE (1=2) 
--OR [ticket_titel] LIKE '%swisscom%' 
--OR [ticket_beschreibung] LIKE '%T_AP_Ref_DokumentKategorie%' 
OR [kommentar] LIKE '%T_AP_Ref_DokumentKategorie%' 
*/


-- SELECT TOP 10 * FROM journal_details  WHERE value LIKE '%T_AP_Ref_DokumentKategorie%'
GO


