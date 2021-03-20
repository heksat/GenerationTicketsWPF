
if not exists(SELECT * FROM master.dbo.sysdatabases WHERE name = 'GenerationTickets')
create database GenerationTickets;
GO
use GenerationTickets
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Tickets')
drop table Tickets
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Tasks')
drop table Tasks
GO
if exists(select * from Sys.objects where type = 'U' and name = 'TypesTask')
drop table TypesTask
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Levels')
drop table Levels
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Teaching')
drop table Teaching
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Workers')
drop table Workers
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Roles')
drop table Roles
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Disciplines')
drop table Disciplines
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Specialty')
drop table Specialty
GO
if exists(select * from Sys.objects where type = 'U' and name = 'Chairmans')
drop table Chairmans
GO
create table Chairmans(
Chairman_ID int IDENTITY(1,1),
LName nvarchar(50) not NULL,
Fname nvarchar(50) not NULL,
SName nvarchar (50) not NULL,
CONSTRAINT PK_UNIQUE_Chairmans PRIMARY KEY (Chairman_ID)
);
GO
create table Specialty(
Specialty_ID nvarchar(8) not NULL, -- нужно ограничение
Specialty_decryption nvarchar(50) not NULL,
Chairman_ID int not NULL ,
CONSTRAINT PK_UNIQUE_Specialty  PRIMARY KEY (Specialty_ID),
FOREIGN KEY (Chairman_ID) REFERENCES
dbo.Chairmans (Chairman_ID)
)
GO
create table Disciplines(
Discipline_ID int IDENTITY(1,1),
Discipline_name nvarchar(50) not NULL,
Specialty_ID nvarchar(8) not NULL,
CONSTRAINT PK_UNIQUE_Disciplines PRIMARY KEY (Discipline_ID),
FOREIGN KEY (Specialty_ID) references
dbo.Specialty (Specialty_ID)
)
GO
Create table Roles (
Role_ID int IDENTITY (1,1),
Role_decryption nvarchar(50) not NULL,
CONSTRAINT PK_UNIQUE_Roles PRIMARY KEY (Role_ID),
)
INSERT INTO Roles VALUES ('Administrator')
INSERT INTO Roles VALUES ('Teacher')
GO
create table Workers(
Worker_ID int IDENTITY (1,1),
LName nvarchar(50) not NULL,
FName nvarchar(50) not NULL,
SName nvarchar(50) not NULL,
Gender nvarchar(1) not NULL, --Discipline_ID
Worker_Login nvarchar(20) COLLATE Latin1_General_CS_AS UNIQUE not NULL,
Worker_password nvarchar(50) COLLATE Latin1_General_CS_AS not NULL,
Role_ID int not NULL,
CONSTRAINT PK_UNIQUE_Workers PRIMARY KEY (Worker_ID),
FOREIGN KEY (Role_ID) REFERENCES Roles (Role_ID),
CHECK ((Gender='м') or (Gender='ж'))
)
GO
create table Teaching(
Discipline_ID int not NULL,
Worker_ID int not NULL,
CONSTRAINT PK_UNIQUE_Teaching PRIMARY KEY (Discipline_ID,Worker_ID),
FOREIGN KEY (Discipline_ID) REFERENCES
Disciplines (Discipline_ID),
FOREIGN KEY (Worker_ID) REFERENCES
Workers (Worker_ID)
)
GO
create table Levels(
Level_ID int IDENTITY (1,1),
Lever_Decryption nvarchar(50) not NULL,
CONSTRAINT PK_UNIQUE_Levels PRIMARY KEY (Level_ID),
)
GO
create table TypesTask(
Types_Task_ID int IDENTITY (1,1),
Types_Task_Decryption nvarchar(50) not NULL,
CONSTRAINT PK_UNIQUE_TypesTask PRIMARY KEY (Types_Task_ID)
)
GO
create table Tasks(
Task_ID int IDENTITY (1,1),
Discipline_ID int not NULL,
Task_decryption nvarchar(255) not NULL,
Level_ID INT not NULL,
Types_Task_ID int not NULL,
Worker_ID int not NULL,
CONSTRAINT PK_UNIQIE_Tasks PRIMARY KEY (TASK_ID,Discipline_ID),
FOREIGN KEY (Discipline_ID,Worker_ID) REFERENCES
Teaching (Discipline_ID,Worker_ID),
FOREIGN KEY (Types_Task_ID) REFERENCES
TypesTask (Types_Task_ID),
FOREIGN KEY (Level_ID) REFERENCES Levels (Level_ID)
)
GO
create table Tickets(
Ticket_ID int IDENTITY (1,1),
TaskNumber TINYINT not NULL, --спросить про диапазон между 1 и 3, как сделать
Task_ID int not NULL,
Discipline_ID int not NULL,
Chairman_ID int not NULL,
CONSTRAINT PK_UNIQUE_Tickets PRIMARY KEY (Ticket_ID,TaskNumber),
FOREIGN KEY (Chairman_ID) references
Chairmans (Chairman_ID),
FOREIGN KEY (Task_ID,Discipline_ID) references
Tasks (Task_ID,Discipline_ID)
)
if exists(select * from sys.objects where type='v' and name = 'Table_names')
drop view Table_names
GO
CREATE VIEW Table_names
AS SELECT name as Name
FROM sys.objects
WHERE (type = 'U')