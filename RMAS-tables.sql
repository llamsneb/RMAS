USE RMAS_db  
GO

DROP TABLE [dbo].[Event]
GO

DROP TABLE [dbo].[Room]
GO

CREATE TABLE dbo.Room  
(
	RoomNumber int PRIMARY KEY NOT NULL, --For auto increment uncomment: IDENTITY(1,1),  
	RoomType varchar(45) NOT NULL
)  
GO  

CREATE TABLE dbo.Event
(
  EventName varchar(45) NOT NULL,
  EventDate date NOT NULL,
  BeginTime time NOT NULL,
  EndTime time NOT NULL,
  RoomNumber int NOT NULL,
  UserId nvarchar(450) NOT NULL,
  PRIMARY KEY (EventDate, BeginTime, EndTime, RoomNumber),
  CONSTRAINT RoomNumber FOREIGN KEY (RoomNumber) REFERENCES Room (RoomNumber) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT UserId FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE ON UPDATE CASCADE
) 
GO


