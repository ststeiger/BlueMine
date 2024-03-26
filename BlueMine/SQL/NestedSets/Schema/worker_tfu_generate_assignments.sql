
-- DROP FUNCTION dbo.tfu_assignment_range; 
-- GO 


IF NOT EXISTS 
( 
	SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE SPECIFIC_SCHEMA = 'dbo' 
	AND SPECIFIC_NAME = 'tfu_assignment_range' 
	AND ROUTINE_TYPE = 'FUNCTION' 
)
BEGIN
EXECUTE('CREATE FUNCTION dbo.tfu_assignment_range ( ) RETURNS table AS RETURN ( SELECT 123 AS abc ); ');
END


GO


ALTER FUNCTION dbo.tfu_assignment_range( @StartDate DATE, @EndDate DATE )
    RETURNS table 
AS 
RETURN
( 
    WITH DateRange AS 
    ( 
        SELECT 
             @StartDate AS assignment_date 
             -- ,(DATEPART(WEEKDAY, @StartDate) + (@@DATEFIRST - 1)) % 7 AS day_of_week_start_sunday 
            ,((DATEPART(dw, @StartDate) + (@@DATEFIRST - 1)+ 6) % 7) AS day_of_week_start_monday 
        WHERE @EndDate >= @StartDate 

        UNION ALL 

        SELECT 
             DATEADD(DAY, 1, assignment_date) AS assignment_date 
             -- ,(DateRange.day_of_week_start_sunday + 1) %7AS day_of_week_start_sunday 
            ,(DateRange.day_of_week_start_monday + 1) %7AS day_of_week_start_monday 
        FROM DateRange 
        WHERE assignment_date < @EndDate 
    ) 
    SELECT 
         assignment_date 
         -- ,day_of_week_start_sunday 
        ,day_of_week_start_monday 
    FROM DateRange 
);


GO 

