
;WITH CTE AS 
(
	SELECT MAX(id) AS id FROM custom_fields 

	UNION ALL 

	SELECT CTE.id-1 AS id FROM CTE 
	WHERE id > 0 
)
SELECT 
	 CTE.id 
	-- custom_fields.id 
	--,custom_fields.type
	,custom_fields.name
	--,custom_fields.field_format
	--,custom_fields.possible_values
	--,custom_fields.is_required
	--,custom_fields.default_value 

	,custom_values.id	
	--,custom_values.value 
	
	,COALESCE(custom_values.value ,custom_fields.default_value) AS val 

FROM CTE 

LEFT JOIN custom_fields 
	ON custom_fields.id = CTE.id 

LEFT JOIN custom_values 
	ON custom_values.custom_field_id = custom_fields.id 
	AND custom_values.customized_type = 'Issue' 
	AND custom_values.customized_id = 2 


ORDER BY CTE.id 





SELECT 
	 id
	,subject
	,description
	,due_date
     
	,LEN(subject) AS subjlen
	,LEN(description) AS desclen 

	,done_ratio
	,estimated_hours
FROM issues
WHERE id = 2 
ORDER BY desclen DESC 

