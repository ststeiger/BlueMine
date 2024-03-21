
DECLARE @reporting_start_date DATE = '2023-01-09'; -- Example reporting date 
DECLARE @reporting_end_date DATE = '2023-04-01'; 

SELECT 
	 assignment.ass_uid 
	,assignment.ass_range_from 
	,assignment.ass_range_to 
	 
	,CONVERT(varchar(10), @reporting_start_date, 104) AS input 
	,tmp_date_range.start_date AS von 
	,tmp_date_range.end_date AS bis 
	 
	,wp.WP_UID
	,wr.WR_UID 
	 
	
	,t.AssignmentDate
	,t.day_name 
	,t.time_from
	,t.time_to
	


FROM Assignment AS assignment 

OUTER APPLY 
	(
		-- Determine the appropriate date range for GenerateAssignments
		SELECT 
			 CASE WHEN @reporting_start_date < assignment.date_from  THEN assignment.date_from ELSE @reporting_start_date END AS start_date 
			,CASE WHEN @reporting_end_date > assignment.date_to  THEN assignment.date_to ELSE @reporting_end_date END AS end_date 
	) AS tmp_date_range 

INNER JOIN Workplace AS wp ON wp.WP_UID = assignment.ass_WP_UID

INNER JOIN Worker AS wr ON assignment.ass_WR_UID = wr.WR_UID
/**/
OUTER APPLY 
	( 
		SELECT 
			 ga.AssignmentDate 
			 -- '123abc' AS AssignmentDate 
			,assignments_normalized.time_from
			,assignments_normalized.time_to
			,assignment_day.day_name  
		FROM assignments_normalized 
		INNER JOIN assignment_day ON assignment_day.day_id = assignments_normalized.assignment_day 
		-- Apply GenerateAssignments with the determined date range 
		INNER JOIN GenerateAssignments(tmp_date_range.start_date, tmp_date_range.end_date) AS ga 
			ON ga.day_of_week_start_monday = assignment_day.day_id 

		WHERE assignments_normalized.ass_uid = Assignment.ass_uid 
	) AS t 
	
WHERE ass_wr_uid = 'C20A2781-FFFE-47C4-ACA2-83C937EABA91' 
--SELECT * FROM assignments_normalized WHERE ass_uid = 'F3947E09-95BE-445F-853B-FD8FD78C3D78'
-- SELECT * FROM Assignment WHERE ass_uid = 'F3947E09-95BE-445F-853B-FD8FD78C3D78'

/*
DECLARE @StartDate DATE = '2024-03-01';
DECLARE @EndDate DATE = '2024-03-31';

SELECT AssignmentDate, day_name 
FROM GenerateAssignments(@StartDate, @EndDate) AS t 
INNER JOIN assignment_day ON day_id = day_of_week_start_monday 


;WITH CTE AS 
(
			  SELECT 1 AS i 
	UNION ALL SELECT i+1 FROM CTE WHERE i < 100
)
INSERT INTO worker(wr_uid)
SELECT NEWID() AS obj_uid FROM CTE 


;WITH CTE AS 
(
	          SELECT 1 AS i 
	UNION ALL SELECT i+1 FROM CTE WHERE i < 100
)
INSERT INTO workplace(wp_uid)
SELECT NEWID() AS obj_uid FROM CTE 
*/
