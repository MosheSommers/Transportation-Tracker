-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE users;
DROP TABLE routes;
DROP TABLE waypoints;

--DROP TABLE private_group;

-- CREATE statements go here
CREATE table users
(
	user_id int Identity(1,1),
	email_address varChar(150) not null,
	password varChar(50) not null,
	salt varChar(10) not null,
	phone_number varChar(10),
	is_admin bit

	CONSTRAINT PK_users PRIMARY KEY (user_id)
);

CREATE table routes
(
	route_id int Identity(1,1),
	route_name varChar(100) not null,

	CONSTRAINT PK_routes PRIMARY KEY (route_id)
);

CREATE table waypoints
(
	waypoint_id int Identity(1,1),
	waypoint_position varChar(200),
	waypoint_name varChar(50),
	stop_number int, 
	route_id int FOREIGN KEY REFERENCES routes(route_id),
	CONSTRAINT PK_waypoint_id PRIMARY KEY (waypoint_id)	
);

--CREATE table private_group
--(
--	private_group_id int Identity(1,1),
--	private_group_name varChar(50) not null,
--	user_id int FOREIGN KEY REFERENCES users(user_id)

--	CONSTRAINT PK_private_group PRIMARY KEY (private_group_id)
--);




