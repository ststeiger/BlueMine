
-- https://www.sqlservercentral.com/articles/hierarchies-on-steroids-1-convert-an-adjacency-list-to-nested-sets


;WITH CTE AS 
(
    SELECT 
         id
        ,parent_id
        ,name
		,lft 
		,rgt 
        ,ROW_NUMBER() OVER (ORDER BY name) AS sort_order
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
					ROW_NUMBER() OVER (ORDER BY name) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1
    FROM projects
    WHERE parent_id IS NULL -- Start with root nodes
	-- AND id = 6 -- 15-Basic-V4
	AND id = 99 -- 01-Kundenprojekte 


    UNION ALL

    SELECT 
         p.id
        ,p.parent_id
        ,p.name
		,p.lft 
		,p.rgt 
        ,ROW_NUMBER() OVER (ORDER BY p.name) AS sort_order 
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
					-- p.id 
					ROW_NUMBER() OVER (ORDER BY p.name) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1  

    FROM projects AS p
    INNER JOIN CTE AS parent ON p.parent_id = parent.id
)
SELECT 
     id
	,REPLICATE('   ', depth - 1) + name AS indented_name
	,REPLICATE('   ', depth - 1) + recursive_path_1 AS indented_path  
	,depth 
    ,name
	,lft 
	,rgt 
	,recursive_path_1 
	,sort_order 
	-- ,ROW_NUMBER() OVER (ORDER BY lft) * 2 AS rgt -- Calculate right boundary
    -- ,(SELECT sort_order FROM CTE WHERE CTE.id = query.id) AS lft_computed 
    --, (SELECT sort_order FROM CTE WHERE CTE.id = parent.id) 
	--+ (SELECT COUNT(*) FROM CTE WHERE CTE.parent_id = parent.id) * 2 AS rgt
FROM CTE AS query  
ORDER BY recursive_path_1
