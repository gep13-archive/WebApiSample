INSERT INTO [dbo].[AspNetUsers]
		   ([Id]
		   ,[Email]
		   ,[EmailConfirmed]
		   ,[PasswordHash]
		   ,[SecurityStamp]
		   ,[PhoneNumber]
		   ,[PhoneNumberConfirmed]
		   ,[TwoFactorEnabled]
		   ,[LockoutEndDateUtc]
		   ,[LockoutEnabled]
		   ,[AccessFailedCount]
		   ,[UserName])
	 VALUES
		   ('02948062-d11e-49b5-b0f6-b75d52b8ec93'
		   ,'gep13@gep13.co.uk'
		   ,0
		   ,'AKoRBesbJnjVSryyZhbX4ZA8H8LasYiMbAZDMt5sS+ou+BXjLCIk3xf8xpGmQo3aow=='
		   ,'42957593-4ed5-4c07-8c6b-735f86278f59'
		   ,NULL
		   ,0
		   ,0
		   ,NULL
		   ,0
		   ,0
		   ,'gep13@gep13.co.uk')
GO

INSERT INTO [dbo].[AspNetRoles]
		   ([Id]
		   ,[Name])
	 VALUES
		   ('6324727A-6854-4315-B507-65119A8DBA96'
		   ,'Admin')
GO

INSERT INTO [dbo].[AspNetUserRoles]
		   ([UserId]
		   ,[RoleId])
	 VALUES
		   ('02948062-d11e-49b5-b0f6-b75d52b8ec93'
		   ,'6324727A-6854-4315-B507-65119A8DBA96')
GO

