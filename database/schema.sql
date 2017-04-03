-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************




-- CREATE statements go here
CREATE table users
(
	user_id int Identity(1,1),
	email_address varChar(150) not null,
	password varChar(50) not null,
	phone_number varChar(10),
	is_admin bit

	CONSTRAINT pk_users PRIMARY KEY (user_id)
);
