		SELECT 
			 -- ga.AssignmentDate 
			 '123abc' AS AssignmentDate 
			,assignments_normalized.time_from
			,assignments_normalized.time_to
			,assignment_day.day_name  
		FROM assignments_normalized 
		INNER JOIN assignment_day ON assignment_day.day_id = assignments_normalized.assignment_day 
		-- Apply GenerateAssignments with the determined date range 
		INNER JOIN GenerateAssignments('2024-01-09', '2024-01-31') AS ga 
			ON ga.day_of_week_start_monday = assignment_day.day_id