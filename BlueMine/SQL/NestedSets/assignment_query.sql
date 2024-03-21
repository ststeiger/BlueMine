
DECLARE @reporting_start_date DATE = '2023-01-09'; -- Example reporting date 
DECLARE @reporting_end_date DATE = '2023-04-01'; 

SELECT 
	 assignment_group.ass_uid 
	 
	,assignment_group.ass_date_from 
	,assignment_group.ass_date_to 
	 
	,CONVERT(varchar(10), @reporting_start_date, 104) AS input 
	,tmp_date_range.start_date AS von 
	,tmp_date_range.end_date AS bis 
	 
	,wp.WP_UID
	,wr.WR_UID 
	 
	
	,t.AssignmentDate
	,t.day_name 
	,t.asgn_time_from
	,t.asgn_time_to
	
FROM assignment_group  

OUTER APPLY 
	(
		-- Determine the appropriate date range for Generateassignment_group.
		SELECT 
			 CASE WHEN @reporting_start_date < assignment_group.ass_date_from  THEN assignment_group.ass_date_from ELSE @reporting_start_date END AS start_date 
			,CASE WHEN @reporting_end_date > assignment_group.ass_date_to  THEN assignment_group.ass_date_to ELSE @reporting_end_date END AS end_date 
	) AS tmp_date_range 

INNER JOIN Workplace AS wp ON wp.WP_UID = assignment_group.ass_WP_UID

INNER JOIN Worker AS wr ON assignment_group.ass_WR_UID = wr.WR_UID
/**/
OUTER APPLY 
	( 
		SELECT 
			 ga.AssignmentDate 
			 -- '123abc' AS AssignmentDate 
			,assignments.asgn_time_from
			,assignments.asgn_time_to
			,assignment_day.day_name  
		FROM assignments 
		INNER JOIN assignment_day ON assignment_day.day_id = assignments.asgn_assignment_day 
		-- Apply GenerateAssignments with the determined date range 
		INNER JOIN GenerateAssignments(tmp_date_range.start_date, tmp_date_range.end_date) AS ga 
			ON ga.day_of_week_start_monday = assignment_day.day_id 

		WHERE assignments.asgn_ass_uid = assignment_group.ass_uid 
	) AS t 
	
WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 


-- SELECT * FROM assignments WHERE asgn_ass_uid = 'F3947E09-95BE-445F-853B-FD8FD78C3D78'
-- SELECT * FROM assignment_group WHERE ass_uid = 'F3947E09-95BE-445F-853B-FD8FD78C3D78'

/*
DECLARE @StartDate DATE = '2024-03-01';
DECLARE @EndDate DATE = '2024-03-31';

SELECT AssignmentDate, day_name 
FROM GenerateAssignments(@StartDate, @EndDate) AS t 
INNER JOIN assignment_day ON day_id = day_of_week_start_monday 

*/
