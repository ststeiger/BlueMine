
DECLARE @in_issue_assigned_to int; 
DECLARE @in_issue_status varchar(MAX); 

-- TODO: Set parameter values here.
SET @in_issue_assigned_to = 5; 
SET @in_issue_status =  STUFF
						(
							(
								SELECT 
									',' + CAST(id AS varchar(36)) AS [text()]
								FROM COR.V_DefaultStatus 
								FOR XML PATH(''), TYPE
							).value('.', 'varchar(MAX)')
							,1, 1, ''
						)
; 

-- SELECT @in_issue_status; 

EXECUTE COR.get_issues_by_assigned_and_status @in_issue_assigned_to, @in_issue_status; 
