
-- https://www.redmine.org/projects/redmine/wiki/rest_users
-- https://www.redmine.org/projects/redmine/repository/svn/entry/trunk/app/models/user.rb#L22

CREATE VIEW COR.V_UserStatuses AS 
          SELECT 1 AS user_status_id, 'Active' AS user_status_name, 'User can login and use their account' AS user_status_description 
UNION ALL SELECT 2 AS user_status_id, 'Registered' AS user_status_name, 'User has registered but not yet confirmed their email address or was not yet activated by an administrator. User can not login' AS user_status_description 
UNION ALL SELECT 3 AS user_status_id, 'Locked' AS user_status_name, 'User was once active and is now locked, User can not login' AS user_status_description 
