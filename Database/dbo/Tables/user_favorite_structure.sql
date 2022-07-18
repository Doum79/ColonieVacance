CREATE TABLE [dbo].[user_favorite_structure]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [user_id] INT NULL, 
    [structure_id] INT NULL,

     CONSTRAINT FK_FAVORITESTRUCTURE_USER FOREIGN KEY (user_id) REFERENCES [user](id),
     CONSTRAINT FK_FAVORITESTRUCTURE_STRUCTURE FOREIGN KEY (structure_id) REFERENCES [structure](id)
)
