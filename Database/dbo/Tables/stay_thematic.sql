CREATE TABLE [dbo].[stay_thematic]
(
  	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [thematic_id] INT NULL,

    CONSTRAINT FK_STAYATHEMATIC_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
    CONSTRAINT FK_STAYTHEMATIC_THEMATIC FOREIGN KEY ([thematic_id]) REFERENCES [thematic](id)
)
