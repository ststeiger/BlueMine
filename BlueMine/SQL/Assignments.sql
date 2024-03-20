
DECLARE @reporting_start_date DATE = '2024-03-01'; -- Example reporting date
DECLARE @reporting_end_date DATE = '2024-04-01';

SELECT 
	 wp.WP_UID
	,wr.WR_UID -- , ga.AssignmentDate, ga.AssignmentDescription
	,assignment.ass_range_from
	,assignment.ass_range_to 
FROM Workplace AS wp 
LEFT JOIN Assignment AS assignment ON wp.WP_UID = assignment.ass_WP_UID
LEFT JOIN Worker AS wr ON assignment.ass_WR_UID = wr.WR_UID

OUTER APPLY 
	(
		-- Determine the appropriate date range for GenerateAssignments
		SELECT 
			 CASE WHEN @reporting_start_date < assignment.date_from  THEN assignment.date_from ELSE @reporting_start_date END AS start_date 
			,CASE WHEN @reporting_end_date > assignment.date_to  THEN assignment.date_to ELSE @reporting_end_date END AS end_date 
	) AS tmp_date_range 

OUTER APPLY 
	(
		-- Apply GenerateAssignments with the determined date range
		SELECT
			 AssignmentDate 	
			,assignments_normalized.time_from
			,assignments_normalized.time_to
			,assignment_day.day_name
		FROM GenerateAssignments(tmp_date_range.start_date, tmp_date_range.end_date) AS ga 
		INNER JOIN assignments_normalized ON assignments_normalized.assignment_day = ga.day_of_week_start_monday 
		INNER JOIN assignment_day ON assignment_day.day_id = ga.day_of_week_start_monday 
	) AS t 


/*
DECLARE @StartDate DATE = '2024-03-01';
DECLARE @EndDate DATE = '2024-03-31';

SELECT AssignmentDate, day_name 
FROM GenerateAssignments(@StartDate, @EndDate) AS t 
INNER JOIN assignment_day ON day_id = day_of_week_start_monday 


-- 
   --  VALUES (<wr_uid, uniqueidentifier,>)
;WITH CTE AS (
SELECT 1 AS i 
UNION ALL SELECT i+1 FROM CTE WHERE i < 100
)
INSERT INTO worker(wr_uid)
SELECT NEWID() AS obj_uid FROM CTE 


;WITH CTE AS (
SELECT 1 AS i 
UNION ALL SELECT i+1 FROM CTE WHERE i < 100
)
INSERT INTO workplace(wp_uid)
SELECT NEWID() AS obj_uid FROM CTE 
*/
