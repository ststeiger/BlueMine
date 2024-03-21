
INSERT INTO dbo.assignments_normalized (ass_uid, assignment_day, time_from, time_to)
SELECT 
	 'F3947E09-95BE-445F-853B-FD8FD78C3D78' AS ass_uid -- uniqueidentifier
	,1 AS assignment_day -- int
	,'08:11' AS time_from -- time(7)
	,'12:47' AS time_to -- time(7)


INSERT INTO dbo.assignments_normalized (ass_uid, assignment_day, time_from, time_to)
SELECT 
	 '8D44E0C9-791E-4224-A447-C439E1898307' AS ass_uid -- uniqueidentifier
	,2 AS assignment_day -- int
	,'08:00' AS time_from -- time(7)
	,'12:00' AS time_to -- time(7)

	
INSERT INTO dbo.assignments_normalized (ass_uid, assignment_day, time_from, time_to)
SELECT 
	 '8D44E0C9-791E-4224-A447-C439E1898307' AS ass_uid -- uniqueidentifier
	,3 AS assignment_day -- int
	,'14:00' AS time_from -- time(7)
	,'18:00' AS time_to -- time(7)



INSERT INTO dbo.assignments_normalized (ass_uid, assignment_day, time_from, time_to)
SELECT 
	 '7036ED96-9494-4A18-A0A0-4CAA95BBDADB' AS ass_uid -- uniqueidentifier
	,4 AS assignment_day -- int
	,'00:00' AS time_from -- time(7)
	,'08:00' AS time_to -- time(7)
