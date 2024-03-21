
-- DROP FUNCTION dbo.GenerateAssignments; 
-- GO 


IF NOT EXISTS 
( 
	SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE SPECIFIC_SCHEMA = 'dbo' 
	AND SPECIFIC_NAME = 'GenerateAssignments' 
	AND ROUTINE_TYPE = 'FUNCTION' 
)
BEGIN
EXECUTE('CREATE FUNCTION dbo.GenerateAssignments ( ) RETURNS table AS RETURN ( SELECT 123 AS abc ); ');
END


GO


ALTER FUNCTION dbo.GenerateAssignments( @StartDate DATE, @EndDate DATE )
    RETURNS table
    AS
    RETURN
    (
    WITH DateRange AS
    (
    SELECT
    @StartDate AS AssignmentDate
    -- ,(DATEPART(WEEKDAY, @StartDate) + (@@DATEFIRST - 1)) % 7 AS day_of_week_start_sunday 
    ,((DATEPART(dw, @StartDate) + (@@DATEFIRST - 1)+ 6) % 7) AS day_of_week_start_monday
    WHERE @EndDate >= @StartDate

    UNION ALL

    SELECT
    DATEADD(DAY, 1, AssignmentDate)
    -- ,(DateRange.day_of_week_start_sunday + 1) %7AS day_of_week_start_sunday 
    ,(DateRange.day_of_week_start_monday + 1) %7AS day_of_week_start_monday
    FROM DateRange
    WHERE AssignmentDate < @EndDate
    )
    SELECT
    AssignmentDate
    -- ,day_of_week_start_sunday
    ,day_of_week_start_monday
    FROM DateRange
    );
GO


