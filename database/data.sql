-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here


INSERT INTO [dbo].[routes]
           ([route_name])
     VALUES
           (<route_name, varchar(100),>)

INSERT INTO [dbo].[users]
           ([email_address]
           ,[password]
           ,[salt]
           ,[phone_number]
           ,[is_admin])
     VALUES
           (<email_address, admin@admin.com>
           ,<password, admin,>
           ,<salt, salt,>
           ,<phone_number, 123456890,>
           ,<is_admin, 1>)

INSERT INTO [dbo].[waypoints]
           ([waypoint_position]
           ,[waypoint_name]
           ,[stop_number]
           ,[route_id])
     VALUES
           (<waypoint_position, varchar(200),>
           ,<waypoint_name, varchar(50),>
           ,<stop_number, int,>
           ,<route_id, int,>)




COMMIT;