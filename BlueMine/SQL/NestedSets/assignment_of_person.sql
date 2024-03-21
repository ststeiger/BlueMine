SELECT 
	 assignment_group.ass_uid 
	
	,assignment_group.ass_wr_uid 
	,assignment_group.ass_wp_uid 
	,assignment_group.ass_date_from 
	,assignment_group.ass_date_to 
	 
	,assignment_day.day_name
	,assignments.asgn_time_from 
	,assignments.asgn_time_to 
	,assignments.asgn_assignment_day 	 
FROM assignment_group 

LEFT JOIN assignments ON assignments.asgn_ass_uid = assignment_group.ass_uid 
INNER JOIN assignment_day ON assignment_day.day_id = assignments.asgn_assignment_day
--WHERE ass_wp_uid = 'E95F6E5A-5DFF-43A0-ABEB-626AB0FEB886'
WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 


-- SELECT * FROM assignment_group WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 
-- SELECT * FROM assignments 
