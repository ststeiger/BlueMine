
CREATE TRIGGER dbo.Trigger_Locked 
	ON [dbo].[issues] AFTER UPDATE 
AS 
BEGIN
	IF EXISTS(select * from deleted where [id] in (SELECT issue_id FROM locked)) 
	BEGIN 
		PRINT 'Nö';
		ROLLBACK TRANSACTION;
	END; 
END; 
GO

ALTER TABLE dbo.issues ENABLE TRIGGER Trigger_Locked; 
GO


