-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE private_route_users;
DROP TABLE users;
DROP TABLE waypoints;
DROP TABLE routes;




-- CREATE statements go here
CREATE table users
(
	user_id int Identity(1,1),
	email_address varChar(150) not null,
	password varChar(50) not null,
	salt varChar(50) not null,
	phone_number varChar(10),
	is_admin bit,

	CONSTRAINT PK_users PRIMARY KEY (email_address)
);

CREATE table routes
(
	route_id int Identity(1,1),
	route_name varChar(100) not null,
	is_Private bit,

	CONSTRAINT PK_routes PRIMARY KEY (route_id)
);


CREATE table waypoints
(
	waypoint_id int Identity(1,1),
	waypoint_position varChar(200),
	stop_time DateTime,

	route_id int FOREIGN KEY REFERENCES routes(route_id),

	CONSTRAINT PK_waypoint_id PRIMARY KEY (waypoint_id)	
);

CREATE table private_route_users
(
	email_address varChar(150) FOREIGN KEY REFERENCES users(email_address) not null,
	route_id int FOREIGN KEY REFERENCES routes(route_id) not null
);

--SET IDENTITY_INSERT users ON;

update users set is_admin = 1 where user_id = 1;

