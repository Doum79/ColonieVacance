CREATE TABLE [dbo].[stay_team]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [partner_name] VARCHAR(MAX) NULL,
    CONSTRAINT FK_STAYTEAM_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
)
