CREATE TABLE [dbo].[stay_picture]
(
  [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
  [stay_id] INT NULL,
  [picture_url] VARCHAR(MAX) NULL,
  [picture_name] VARCHAR(MAX) NULL,
  CONSTRAINT FK_STAYPICTURE_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id)
)
