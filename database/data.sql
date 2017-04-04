-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here
USE [omnibus]
GO

INSERT INTO [dbo].[routes]
           ([route_name])
     VALUES
           (<route_name, varchar(100),>)
GO

INSERT INTO [dbo].[users]
           ([email_address]
           ,[password]
           ,[salt]
           ,[phone_number]
           ,[is_admin])
     VALUES
           (<email_address, varchar(150),>
           ,<password, varchar(50),>
           ,<salt, varchar(10),>
           ,<phone_number, varchar(10),>
           ,<is_admin, bit,>)
GO

USE [omnibus]

GO

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
GO



COMMIT;