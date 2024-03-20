
-- https://www.sqlservercentral.com/articles/hierarchies-on-steroids-1-convert-an-adjacency-list-to-nested-sets


;WITH CTE AS 
(
    SELECT 
         nour_id
        ,nour_parent
        ,nour_name 
        ,ROW_NUMBER() OVER (ORDER BY nour_name) AS sort_order 
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
					nour_id
					-- ROW_NUMBER() OVER (ORDER BY nour_name) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1

		,CAST(nour_name AS varchar(MAX)) AS recursive_name_1 
    FROM nourishment
    WHERE nour_parent IS NULL -- Start with root nodes
	-- AND id = 6 -- 15-Basic-V4
	-- AND id = 99 -- 01-Kundenprojekte 


    UNION ALL

    SELECT 
         p.nour_id
        ,p.nour_parent
        ,p.nour_name
        ,ROW_NUMBER() OVER (ORDER BY p.nour_name) AS sort_order 
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
					-- p.nour_id 
					p.nour_id
					-- ROW_NUMBER() OVER (ORDER BY p.nour_name) 
					AS varchar(36)
				)
				, 10
			) 
			AS varchar(MAX)
		) AS recursive_path_1  

		,CAST(parent.recursive_name_1 + ' | ' + p.nour_name AS varchar(MAX)) AS recursive_name_1 
    FROM nourishment AS p 
    INNER JOIN CTE AS parent ON p.nour_parent = parent.nour_id 
)
SELECT 
     nour_id 
	,REPLICATE('   ', depth - 1) + nour_name AS indented_name
	,REPLICATE('   ', depth - 1) + recursive_path_1 AS indented_path  
	,nour_name
	,recursive_path_1 
	,recursive_name_1 
	-- ,sort_order 
	-- ,ROW_NUMBER() OVER (ORDER BY lft) * 2 AS rgt -- Calculate right boundary
    -- ,(SELECT sort_order FROM CTE WHERE CTE.id = query.id) AS lft_computed 
    --, (SELECT sort_order FROM CTE WHERE CTE.id = parent.id) 
	--+ (SELECT COUNT(*) FROM CTE WHERE CTE.parent_id = parent.id) * 2 AS rgt
	,depth 
	,ROW_NUMBER() OVER(PARTITION BY depth ORDER BY recursive_name_1) AS rn 
FROM CTE AS query  
ORDER BY recursive_path_1
