CREATE TABLE [dbo].[stay_activity]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [activity_id] INT NULL,

    CONSTRAINT FK_STAYACTIVITY_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
    CONSTRAINT FK_STAYACTIVITY_ACTIVITY FOREIGN KEY ([activity_id]) REFERENCES [activity](id)
)
