
-- DROP FUNCTION IF EXISTS dbo.CheckOverlappingRanges; 
-- GO

IF NOT EXISTS (
    SELECT 1
    FROM information_schema.routines
    WHERE SPECIFIC_SCHEMA = 'dbo'
    AND SPECIFIC_NAME = 'CheckOverlappingRanges'
    AND routine_type = 'FUNCTION'
)
BEGIN
    EXEC('CREATE FUNCTION dbo.CheckOverlappingRanges( @in_when datetime) RETURNS bit BEGIN RETURN NULL; END ; ');
END;


GO


ALTER FUNCTION dbo.CheckOverlappingRanges
(
     @new_asgn_ass_uid uniqueidentifier
    ,@new_asgn_assignment_day int 
    ,@new_asgn_time_from TIME 
    ,@new_asgn_time_to TIME 
)
	RETURNS bit 
BEGIN
    DECLARE @overlapping_count int;
	DECLARE @overlapping bit;

	SET @overlapping_count = (
		SELECT COUNT(*)
		FROM assignments AS t2 
		WHERE t2.asgn_ass_uid = @new_asgn_ass_uid
		  AND t2.asgn_assignment_day = @new_asgn_assignment_day
		  AND t2.asgn_time_from < @new_asgn_time_to
		  AND t2.asgn_time_to > @new_asgn_time_from
	);

	SET @overlapping = 0; 
	IF @overlapping_count > 0 
	BEGIN
		SET @overlapping = 1;
	END

    RETURN @overlapping;
END 