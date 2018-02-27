
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_Translations]') AND type in (N'U'))
BEGIN
CREATE TABLE T_Translations 
(
	 TR_Key nvarchar(4000) NULL
	,TR_Value nvarchar(4000) NULL
); 
END 
