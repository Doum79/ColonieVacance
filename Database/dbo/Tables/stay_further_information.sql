CREATE TABLE [dbo].[stay_further_information]
(
  [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [stay_id] INT NULL,
  [start_date] DATETIME2 NULL,
  [end_date] DATETIME2 NULL,
  [with_transport] BIT NULL,
  [start_city] VARCHAR(MAX) NULL,
  [price] FLOAT NULL,
  [redirection_link] VARCHAR(MAX) NULL,
  CONSTRAINT FK_STAYSTAYFURTHERINFORMATION_STAY FOREIGN KEY ([stay_id]) REFERENCES [stay](id) 
)
