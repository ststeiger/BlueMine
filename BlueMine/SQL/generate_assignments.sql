
CREATE FUNCTION dbo.GenerateAssignments( @StartDate DATE, @EndDate DATE )
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


