
CREATE VIEW COR.V_User_Group_Memberships AS 
SELECT 
	 users.login AS user_name 
	,groups.lastname AS group_Name 
	,users.id AS user_id 
	,groups.id AS group_id 
FROM users 

INNER JOIN groups_users AS groups_users_assignments 
	ON groups_users_assignments.user_id = users.id 

INNER JOIN users AS groups 
	ON groups.id = groups_users_assignments.group_id 
