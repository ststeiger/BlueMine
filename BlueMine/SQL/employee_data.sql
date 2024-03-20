
-- IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'Employee') THEN 
CREATE TABLE IF NOT EXISTS dbo.Employee  
( 
     EmployeeID int NOT NULL 
    ,ManagerID int NULL 
    ,DisplayName national character varying(255) NULL 
    ,CONSTRAINT PK_Employee PRIMARY KEY (EmployeeID) 
); 



GO



INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (1, 3, N'Vivian');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (2, 26, N'Lynne');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (3, 26, N'Bob');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (6, 17, N'Eric');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (8, 3, N'Bill');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (12, 8, N'Megan');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (14, 8, N'Kim');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (17, 2, N'Butch');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (18, 39, N'Lisa');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (20, 3, N'Natalie');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (21, 39, N'Hemer');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (26, NULL, N'Jim');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (39, 26, N'Ken');
INSERT INTO dbo.Employee(EmployeeID, ManagerID, DisplayName) VALUES (40, 26, N'Marge'); 

