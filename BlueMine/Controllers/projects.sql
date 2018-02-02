
SELECT 
	 id
	,"name"
	,"description" 
	,homepage
	,is_public
	,parent_id
	,created_on
	,updated_on
	,identifier
	,"status"
	,lft
	,rgt
	,inherit_members
	,default_version_id
FROM projects
WHERE parent_id IS NULL
