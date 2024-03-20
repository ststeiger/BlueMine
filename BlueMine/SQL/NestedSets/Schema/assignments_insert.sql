
INSERT INTO dbo.assignment
(
	 ass_uid, ass_wp_uid, ass_wr_uid 
	,ass_range_from, ass_range_to 
	,date_from, date_to 
) 
SELECT 
     NEWID() AS ass_uid 
	,wp_uid 
	,wr_uid
	,'20230101' AS ass_range_from -- date
	,'20230131' AS ass_range_to -- date
	,'20230101' AS date_from -- date
	,'20230131' AS date_to -- date
FROM 
	(
		SELECT TOP 3 * FROM worker 
	) AS worker 

CROSS JOIN 
	(
		SELECT TOP 1 * FROM workplace
	) AS workplace 


;


SELECT 
	 ass_uid 
	,ass_wp_uid 
	,ass_wr_uid 
	,ass_range_from 
	,ass_range_to 
	,date_from 
	,date_to 
FROM assignment 
