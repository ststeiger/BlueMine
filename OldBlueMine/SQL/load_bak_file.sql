
RESTORE FILELISTONLY 
FROM DISK = 'D:\temp\SQL\COR_Basic_SwissLife_PAV_sts.bak' 

RESTORE HEADERONLY 
FROM DISK = 'D:\temp\SQL\COR_Basic_SwissLife_PAV_sts.bak' 

-- BackupName -- Backup-Name 
-- BackupTypeDescription -- Komponente 
-- RecoveryModel -- Typ 
-- ServerName	-- Server 
-- DatabaseName -- Datenbank 
-- Position 


-- FirstLSN -- Erste LSN 
-- LastLSN -- Letzte LSN 
-- CheckpointLSN -- Prüfpunkt-LSN 
-- DatabaseBackupLSN -- Vollständige LSN 
-- BackupStartDate	 -- Startdatum 
-- BackupFinishDate -- Beendigungsdatum 
-- BackupSize -- Grösse 
-- UserName -- Benutzername 
-- ExpirationDate -- Ablaufdatum 
-- Collation 


/*
RESTORE DATABASE backup_lookup 
	FROM DISK = 'C:\backup.bak' 
WITH REPLACE, 
	MOVE 'Old Database Name' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\backup_lookup.mdf', 
	MOVE 'Old Database Name_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\backup_lookup_log.ldf' 
GO 
*/
