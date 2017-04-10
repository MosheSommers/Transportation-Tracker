-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE groups;
DROP TABLE users;
DROP TABLE waypoints;
DROP TABLE routes;
DROP TABLE users_in_groups



-- CREATE statements go here
CREATE table users
(
	user_id int Identity(1,1),
	email_address varChar(150) not null,
	password varChar(50) not null,
	salt varChar(50) not null,
	phone_number varChar(10),
	is_admin bit,

	CONSTRAINT PK_users PRIMARY KEY (user_id)
);

CREATE table routes
(
	route_id int Identity(1,1),
	route_name varChar(100) not null,
	is_Private bit,

	CONSTRAINT PK_routes PRIMARY KEY (route_id)
);

CREATE table groups
(
	group_id int Identity(1,1),
	group_name varChar(50) not null,

	CONSTRAINT PK_groups PRIMARY KEY (group_id)
);

CREATE table waypoints
(
	waypoint_id int Identity(1,1),
	waypoint_position varChar(200),
	stop_time Time,

	route_id int FOREIGN KEY REFERENCES routes(route_id),

	CONSTRAINT PK_waypoint_id PRIMARY KEY (waypoint_id)	
);

CREATE table users_groups
(
	user_id int FOREIGN KEY REFERENCES users(user_id),
	group_id int FOREIGN KEY REFERENCES groups(group_id),

	CONSTRAINT PK_users_groups PRIMARY KEY (user_id, group_id)
);

SET IDENTITY_INSERT users ON;

update users set is_admin = 1 where user_id = 1;

