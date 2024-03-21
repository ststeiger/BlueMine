
DECLARE @reporting_start_date DATE = '2024-01-09'; -- Example reporting date 
-- DECLARE @reporting_end_date DATE = '2024-04-01'; 
DECLARE @reporting_end_date DATE = '2024-01-09'; 


SELECT * 
FROM GenerateAssignments(@reporting_start_date, @reporting_end_date) AS ga 

INNER JOIN assignment_day ON day_id = day_of_week_start_monday 

WHERE day_of_week_start_monday = 1