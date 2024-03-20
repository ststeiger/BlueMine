
CREATE TABLE dbo.nourishment 
( 
	 nour_id int NOT NULL 
    ,nour_parent int NULL 
	,nour_name national character varying(255) NULL 
	,CONSTRAINT pk_nourishment PRIMARY KEY ( nour_id )  
); 
GO



INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (1, NULL, N'Food');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (2, 1, N'Fruit');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (3, 2, N'Red');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (4, 3, N'Cherry');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (5, 2, N'Yellow');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (6, 5, N'Banana');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (7, 1, N'Meat');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (8, 7, N'Beef');
INSERT INTO dbo.nourishment(nour_id, nour_parent, nour_name) VALUES (9, 7, N'Pork');
