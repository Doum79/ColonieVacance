CREATE TABLE [dbo].[structure]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[email] VARCHAR(MAX) NULL, 
    [password] VARCHAR(MAX) NULL,
    [profil] VARCHAR(MAX) NULL, 
	[street] VARCHAR(MAX) NULL, 
    [city] VARCHAR(MAX) NULL, 
    [post_code] VARCHAR(MAX) NULL, 
    [department] VARCHAR(MAX) NULL, 
    [state] VARCHAR(MAX) NULL, 
    [country] VARCHAR(MAX) NULL,
    [phone] INT NULL,
    [longitude] VARCHAR(MAX) NULL, 
    [latitude] VARCHAR(MAX) NULL, 
    [siret] VARCHAR(MAX) NULL, 
    [name] VARCHAR(MAX) NULL, 
)
