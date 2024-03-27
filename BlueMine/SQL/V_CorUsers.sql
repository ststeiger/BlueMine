ALTER VIEW COR.V_CorUsers AS 
SELECT TOP 99999999 
	  users.id AS user_id 
	 ,users.login AS user_login 
	 ,LTRIM(RTRIM(ISNULL(NULLIF(users.firstname, '') + ' ', '') + ISNULL(users.lastname, ''))) AS user_firstname_lastname 
	 ,LTRIM(RTRIM(ISNULL(NULLIF(users.lastname, '') + ' ', '') + ISNULL(users.firstname, ''))) AS user_lastname_firstname 
	 ,users.firstname AS  user_firstname 
	 ,users.lastname AS user_lastname 
FROM users 
JOIN groups_users AS gu 
	ON users.id = gu.user_id 
	AND gu.group_id IN 
	(
		SELECT u2.id FROM users AS u2 
		WHERE (1=1) 
		AND type = 'Group' 
		AND 
		(
			u2.lastname = 'COR Mitarbeiter (Gruppe)' 
			OR 
			u2.id = 17 
		)
	)

WHERE (1=1) 
AND NOT 
(
	users.login = 'admin' 
	OR 
	users.id = 1 
)
AND NOT 
(
	users.login = 'servicedesk' 
	OR 
	users.id = 14 
)

ORDER BY 
	 -- users.lastname, users.firstname 
	 users.firstname, users.lastname 
