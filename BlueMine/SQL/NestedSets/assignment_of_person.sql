SELECT 
	 assignment.ass_uid 
	
	,assignment.ass_wr_uid 
	,assignment.ass_wp_uid 
	-- ,assignment.ass_range_from 
	-- ,assignment.ass_range_to 
	,assignment.date_from 
	,assignment.date_to 
	 
	,assignment_day.day_name
	,assignments_normalized.time_from 
	,assignments_normalized.time_to 
	,assignments_normalized.assignment_day 	 
FROM assignment 

LEFT JOIN assignments_normalized ON assignments_normalized .ass_uid  = assignment .ass_uid 
INNER JOIN assignment_day ON assignment_day.day_id = assignments_normalized.assignment_day
--WHERE ass_wp_uid = 'E95F6E5A-5DFF-43A0-ABEB-626AB0FEB886'
WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 


-- SELECT * FROM assignment WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 
-- SELECT * FROM assignments_normalized 
