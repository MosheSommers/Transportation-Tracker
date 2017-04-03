-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here
INSERT INTO [dbo].[users]
           ([email_address]
           ,[password]
           ,[phone_number]
           ,[is_admin])
     VALUES
           (<email_address, varchar(150),>
           ,<password, varchar(50),>
           ,<phone_number, varchar(10),>
           ,<is_admin, bit,>)
GO

COMMIT;