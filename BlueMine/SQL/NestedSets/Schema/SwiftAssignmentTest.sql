SELECT 
	 -- '123abc' AS AssignmentDate 
	 ga.AssignmentDate 
	,assignments.asgn_time_from
	,assignments.asgn_time_to
	,assignment_day.day_name  
FROM assignments 
INNER JOIN assignment_day ON assignment_day.day_id = assignments.asgn_assignment_day 
-- Apply GenerateAssignments with the determined date range 
INNER JOIN GenerateAssignments('2024-01-09', '2024-01-31') AS ga 
	ON ga.day_of_week_start_monday = assignment_day.day_id
