CREATE TABLE [dbo].[user]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [email] VARCHAR(MAX) NULL, 
    [password] VARCHAR(MAX) NULL, 
    [gender] BIT NULL, 
    [last_name] VARCHAR(MAX) NULL, 
    [first_name] VARCHAR(MAX) NULL, 
    [street] VARCHAR(MAX) NULL, 
    [city] VARCHAR(MAX) NULL, 
    [zip_code] INT NULL, 
    [country] VARCHAR(MAX) NULL,
    [phone_number] INT NULL, 
    [profil] VARCHAR(MAX) NULL
)
