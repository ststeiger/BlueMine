
SELECT 
	 id
	,name
	,description
	,homepage
	,is_public
	,parent_id
	,created_on
	,updated_on
	,identifier
	,status
	,lft
	,rgt
	,inherit_members
	,default_version_id
FROM projects



SELECT 
	 project_id
	,tracker_id
FROM projects_trackers



SELECT 
	 projects_trackers.project_id
	,projects_trackers.tracker_id
	,trackers.name 
FROM projects_trackers

LEFT JOIN trackers 
	ON trackers.id = tracker_id 

WHERE project_id = 6 
