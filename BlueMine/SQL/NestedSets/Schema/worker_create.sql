
CREATE TABLE IF NOT EXISTS dbo.worker 
( 
     wr_uid uniqueidentifier NOT NULL 
    ,CONSTRAINT pk_worker PRIMARY KEY(wr_uid) 
); 
GO


CREATE TABLE IF NOT EXISTS dbo.workplace 
( 
     wp_uid uniqueidentifier NOT NULL 
    ,CONSTRAINT pk_workplace PRIMARY KEY(wp_uid) 
); 
GO

-- ref-table 
CREATE TABLE IF NOT EXISTS dbo.assignment_day 
( 
     day_id int NOT NULL 
    ,day_name nvarchar(100) NOT NULL 
    ,CONSTRAINT pk_assignment_day PRIMARY KEY(day_id) 
); 
GO

CREATE TABLE IF NOT EXISTS dbo.assignment_group 
( 
     ass_uid uniqueidentifier NOT NULL 
    ,ass_wp_uid uniqueidentifier NOT NULL 
    ,ass_wr_uid uniqueidentifier NOT NULL 
    ,ass_date_from date NOT NULL 
    ,ass_date_to date NOT NULL 
	,CONSTRAINT pk_assignment_group PRIMARY KEY(ass_uid) 
); 
GO


CREATE TABLE IF NOT EXISTS dbo.assignments 
( 
     asgn_ass_uid uniqueidentifier NOT NULL 
    ,asgn_assignment_day int NOT NULL 
    ,asgn_time_from time(7) NOT NULL 
    ,asgn_time_to time(7) NOT NULL 
); 
GO
