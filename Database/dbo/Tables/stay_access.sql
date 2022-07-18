CREATE TABLE [dbo].[stay_access]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [label] VARCHAR(MAX) NULL,
    CONSTRAINT FK_STAYACCESS_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
)
