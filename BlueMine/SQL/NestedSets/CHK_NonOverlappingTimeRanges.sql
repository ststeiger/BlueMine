
-- ALTER TABLE assignments DROP CONSTRAINT CHK_NonOverlappingTimeRanges

ALTER TABLE assignments
ADD CONSTRAINT CHK_NonOverlappingTimeRanges CHECK 
(
	dbo.CheckOverlappingRanges(assignments.asgn_ass_uid,assignments.asgn_assignment_day, assignments.asgn_time_to, assignments.asgn_time_from ) = 'false' 
 -- NOT EXISTS 
 -- (
 --   SELECT 1
 --   FROM assignments AS t2 
 --   WHERE t2.asgn_ass_uid = assignments.asgn_ass_uid 
	--AND t2.asgn_assignment_day = assignments.asgn_assignment_day 
 --   AND t2.asgn_time_from < assignments.asgn_time_to 
 --   AND t2.asgn_time_to > assignments.asgn_time_from 
 -- )
);