CREATE TABLE [dbo].[user_favorite_stay]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [user_id] INT NULL, 
    [stay_id] INT NULL,
    
    CONSTRAINT FK_FAVORITESTAY_USER FOREIGN KEY (user_id) REFERENCES [user](id),
    CONSTRAINT FK_FAVORITESTAY_STAY FOREIGN KEY (stay_id) REFERENCES [stay](id)
)
