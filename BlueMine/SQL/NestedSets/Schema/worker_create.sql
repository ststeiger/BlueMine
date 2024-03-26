
CREATE TABLE IF NOT EXISTS dbo.worker 
( 
     wr_uid uniqueidentifier NOT NULL CONSTRAINT pk_worker PRIMARY KEY(wr_uid) 
); 
GO


CREATE TABLE IF NOT EXISTS dbo.workplace 
( 
     wp_uid uniqueidentifier NOT NULL CONSTRAINT pk_workplace PRIMARY KEY(wp_uid) 
); 
GO

-- ref-table 
CREATE TABLE IF NOT EXISTS dbo.assignment_day 
( 
     day_id int NOT NULL CONSTRAINT pk_assignment_day PRIMARY KEY(day_id) 
    ,day_name nvarchar(100) NOT NULL 
    
); 
GO

CREATE TABLE IF NOT EXISTS dbo.assignment_group 
( 
     ass_uid uniqueidentifier NOT NULL CONSTRAINT pk_assignment_group PRIMARY KEY(ass_uid) 
    ,ass_wp_uid uniqueidentifier NOT NULL CONSTRAINT fk_assignment_group_workplace FOREIGN KEY(ass_wp_uid) REFERENCES dbo.workplace (wp_uid) ON DELETE CASCADE 
    ,ass_wr_uid uniqueidentifier NOT NULL CONSTRAINT fk_assignment_group_worker FOREIGN KEY(ass_wr_uid) REFERENCES dbo.worker (wr_uid) ON DELETE CASCADE 
    ,ass_date_from date NOT NULL 
    ,ass_date_to date NOT NULL 
); 
GO


CREATE TABLE IF NOT EXISTS dbo.assignments 
( 
     asgn_ass_uid uniqueidentifier NOT NULL CONSTRAINT fk_assignments_assignment_group FOREIGN KEY(asgn_ass_uid) REFERENCES dbo.assignment_group (ass_uid) ON DELETE CASCADE 
    ,asgn_assignment_day int NOT NULL CONSTRAINT fk_assignments_assignment_day FOREIGN KEY(asgn_assignment_day) REFERENCES dbo.assignment_day (day_id) ON DELETE CASCADE 
    ,asgn_time_from time(7) NOT NULL 
    ,asgn_time_to time(7) NOT NULL 
); 
GO
