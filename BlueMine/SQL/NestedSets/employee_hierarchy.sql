
-- https://www.sqlservercentral.com/articles/hierarchies-on-steroids-1-convert-an-adjacency-list-to-nested-sets


;WITH CTE AS 
(
    SELECT 
         EmployeeID
        ,ManagerID
        ,DisplayName 
        ,ROW_NUMBER() OVER (ORDER BY DisplayName) AS sort_order 
        ,1 AS depth
		,CAST
		(
			RIGHT
			(
				REPLICATE('0', 10) 
				+ 
				CAST
				(
					-- id
					EmployeeID
					-- ROW_NUMBER() OVER (ORDER BY DisplayName) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1
    FROM Employee
    WHERE ManagerID IS NULL -- Start with root nodes
	-- AND id = 6 -- 15-Basic-V4
	-- AND id = 99 -- 01-Kundenprojekte 


    UNION ALL

    SELECT 
         p.EmployeeID
        ,p.ManagerID
        ,p.DisplayName
        ,ROW_NUMBER() OVER (ORDER BY p.DisplayName) AS sort_order 
        ,parent.depth + 1

		,parent.recursive_path_1 
		+ 
		CAST
		(
			'.' 
			+ 
			RIGHT
			(
				REPLICATE('0', 10) 
				+ 
				CAST
				(
					-- p.EmployeeID 
					p.EmployeeID
					-- ROW_NUMBER() OVER (ORDER BY p.DisplayName) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1  

    FROM Employee AS p 
    INNER JOIN CTE AS parent ON p.ManagerID = parent.EmployeeID 
)
SELECT 
     EmployeeID 
	,REPLICATE('   ', depth - 1) + DisplayName AS indented_name
	,REPLICATE('   ', depth - 1) + recursive_path_1 AS indented_path  
	,depth 
    ,DisplayName
	,recursive_path_1 
	,sort_order 
	-- ,ROW_NUMBER() OVER (ORDER BY lft) * 2 AS rgt -- Calculate right boundary
    -- ,(SELECT sort_order FROM CTE WHERE CTE.id = query.id) AS lft_computed 
    --, (SELECT sort_order FROM CTE WHERE CTE.id = parent.id) 
	--+ (SELECT COUNT(*) FROM CTE WHERE CTE.parent_id = parent.id) * 2 AS rgt
FROM CTE AS query  
ORDER BY recursive_path_1
