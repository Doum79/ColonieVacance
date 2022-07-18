CREATE TABLE [dbo].[stay_tag]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [tag_id] INT NULL,

    CONSTRAINT FK_STAYTAG_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
    CONSTRAINT FK_STAYTAG_TAG FOREIGN KEY ([tag_id]) REFERENCES [tag](id)
)