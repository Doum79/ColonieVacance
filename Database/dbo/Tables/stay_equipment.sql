CREATE TABLE [dbo].[stay_equipment]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [stay_id] INT NULL, 
    [label] VARCHAR(MAX) NULL, 
    [is_included] BIT NULL,
    CONSTRAINT FK_STAYEQUIPMENT_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id),
)
