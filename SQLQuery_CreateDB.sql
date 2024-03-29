
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
insert into Chairmans VALUES ('�������','���������','��������')
insert into Chairmans VALUES ('���������','�����','�����������')
GO
create table Specialty(
Specialty_ID nvarchar(8) not NULL, -- ����� �����������
Specialty_decryption nvarchar(50) not NULL,
Chairman_ID int not NULL ,
CONSTRAINT PK_UNIQUE_Specialty  PRIMARY KEY (Specialty_ID),
CONSTRAINT chk_Specialty_ID CHECK ((Specialty_ID like '[0-9][0-9].[0-9][0-9].[0-9][0-9]') and (Specialty_ID != '00.00.00')),
FOREIGN KEY (Chairman_ID) REFERENCES
dbo.Chairmans (Chairman_ID)
ON DELETE CASCADE
ON UPDATE CASCADE
)
insert into Specialty VALUES ('09.02.03','���������������� � ������������ ��������',1)
GO
create table Disciplines(
Discipline_ID int IDENTITY(1,1),
Discipline_name nvarchar(50) not NULL,
Specialty_ID nvarchar(8) not NULL,
CONSTRAINT PK_UNIQUE_Disciplines PRIMARY KEY (Discipline_ID),
FOREIGN KEY (Specialty_ID) references
dbo.Specialty (Specialty_ID) 
ON DELETE CASCADE
ON UPDATE CASCADE
)
insert into Disciplines VALUES ('��������� ����������������','09.02.03')
insert into Disciplines VALUES ('����������� ������������ ������','09.02.03')
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
FOREIGN KEY (Role_ID) REFERENCES Roles (Role_ID)
ON DELETE CASCADE
ON UPDATE CASCADE,
CHECK ((Gender='�') or (Gender='�'))
)
insert into Workers VALUES ('Admin','Admin','Admin','�','Admin','Admin',1)
GO
create table Teaching(
Discipline_ID int not NULL,
Worker_ID int not NULL,
CONSTRAINT PK_UNIQUE_Teaching PRIMARY KEY (Discipline_ID,Worker_ID),
FOREIGN KEY (Discipline_ID) REFERENCES
Disciplines (Discipline_ID)
ON DELETE CASCADE
ON UPDATE CASCADE,
FOREIGN KEY (Worker_ID) REFERENCES
Workers (Worker_ID)
ON DELETE CASCADE
ON UPDATE CASCADE
)
insert into Teaching VALUES (1,1)
GO
create table Levels(
Level_ID int IDENTITY (1,1),
Lever_Decryption nvarchar(50) not NULL,
CONSTRAINT PK_UNIQUE_Levels PRIMARY KEY (Level_ID)
)
insert into Levels VALUES ('Halyava')
insert into Levels VALUES ('Hard')
GO
create table TypesTask(
Types_Task_ID int,
Types_Task_Decryption nvarchar(50) not NULL,
CONSTRAINT PK_UNIQUE_TypesTask PRIMARY KEY (Types_Task_ID)
)
insert into TypesTask VALUES (1,'��������')
insert into TypesTask VALUES (2,'������')
GO
create table Tasks(
Task_ID int IDENTITY (1,1),
Discipline_ID int not NULL,
Task_decryption nvarchar(500) not NULL,
Level_ID INT not NULL,
Types_Task_ID int not NULL,
Worker_ID int not NULL,
CONSTRAINT PK_UNIQIE_Tasks PRIMARY KEY (TASK_ID,Discipline_ID),
FOREIGN KEY (Discipline_ID,Worker_ID) REFERENCES
Teaching (Discipline_ID,Worker_ID)
ON DELETE CASCADE
ON UPDATE CASCADE,
FOREIGN KEY (Types_Task_ID) REFERENCES
TypesTask (Types_Task_ID)
ON DELETE CASCADE
ON UPDATE CASCADE,
FOREIGN KEY (Level_ID) REFERENCES Levels (Level_ID)
ON DELETE CASCADE
ON UPDATE CASCADE
)
--���� �������
insert into Tasks VALUES (1,N'���������� � ��������� ����������������. ����� ���������� ����������������. ����������� ������ ���������� ����������������.'
,1,2,1)
insert into Tasks VALUES (1,N' ����������� ����������. ������� 9, 4c, 3F, 40 ���������� 21h. �������� �����/������ ����� �� ����������.'
,1,2,1)
insert into Tasks VALUES (1,N'������� ����� ����� ��.'
,1,2,1)
insert into Tasks VALUES (1,N'�������� ����� ��.'
,1,2,1)
insert into Tasks VALUES (1,N' ������ � ������� �� ����� ��'
,1,2,1)
insert into Tasks VALUES (1,N'����������� ��������� ����� ��. ��������� � ������� ����� ��. va_arg.'
,1,2,1)
insert into Tasks VALUES (1,N'���� �������������.'
,1,2,1)
insert into Tasks VALUES (1,N'�������� ����� static, extern, auto, register, const, restrict, volatile, _Alignas, _Atomic'
,1,2,1)
insert into Tasks VALUES (1,N' �������� ������ � �������������� � �������� ��������� �� ����� ��.'
,1,2,1)
insert into Tasks VALUES (1,N'����������� ������������� ������: make, cmake. ����������� � ������������ ����������. �������� �� �������� � �������������.'
,1,2,1)
insert into Tasks VALUES (1,N'Unit-������������. �������. ��������. ���������.'
,1,2,1)
insert into Tasks VALUES (1,N'������ �������� ��������� � �������: CreateThread, fork, thread ����� C. ����������� ������. ��������. Mutex.'
,1,2,1)
--����� �������
insert into Tasks VALUES (1,N'��� ������� ������������ ����� ������� ����� ������ ���� ��� ����. ������ �������������� � ����� dosbox, ����������� ��������� ������� �� ����� ����������.'
,1,1,1)
insert into Tasks VALUES (1,N'������������ ��������� ��� Linux � Windows, ��������� API ������������ ������. � ������� ������ (regular file), � ������� ��� �������� ����� ��������������� ������ ��������� � ����� ����� 1000. ��������������, ��� ���������� ����� ����� ��������, �������� �������� ������� �� ��������� 999.'
,1,1,1)
insert into Tasks VALUES (1,N' ������������ ��������� ��� Linux � Windows, ��������� API ������������ ������. ������� �� ���� ������� ������ (regular file) ��� �����..'
,1,1,1)
insert into Tasks VALUES (1,N' ������������ �������, � ������� ��������� ����� ����� n, ����� ��������� ����� x1 � ������� f1, ����� x2 � f2 � ��� ����� �� xn � fn. ������� f1 . . . fn ����������� ������������ ����� � ������������ �����. ����������� ������ ������� ������ ���� f1(x1)+. . .+fn(xn) (��� n � ���������� ���������� �������). ������������ make-���� � ���������� ������� � ������������ � ����������� ����������.'
,1,1,1)
insert into Tasks VALUES (1,N' ������������ �������, � ������� ��������� ������������ ����� x, ����� ����� n � n ������� fi , ������� ����������� ������������ ����� � ������������ �����. ����������� ������ ������� ������ ���� 8 ������� ����� ����� f1(x), . . . , fn(x) (��� n � ���������� ���������� �������). ������������ UNIT-����� � �������������� ����������.'
,1,1,1)
insert into Tasks VALUES (1,N'������������ ������� char * strrchr (char * s,char * c) � ������� ������ ������� ������������� ������� c � ������ s. ���������� ����� ���������� ������� ��� 0, ���� ������ �� ������. ����� ������� � ����� ������.'
,1,1,1)
insert into Tasks VALUES (1,N'������������ ��������� ��� Linux, ����� ������ ������� ������������� ��������� C-������� (thread), ��������� API ������������ ������. ����� ���� (�������� �� ����� ��� ���), ������� ���������� ����� ����� ������, ������������� � ������� �������� ��� ��� ����������� ����� ������� �����������.'
,1,1,1)
insert into Tasks VALUES (1,N'����������� ���������� �� ���������� � �������������� SIMD. �������� ���� ��������� ������� ����� ������������ �����.'
,1,1,1)
insert into Tasks VALUES (1,N'����������� ���������� �� ���������� � �������������� SIMD. �������� ���� ��������� ������� ������������ ����� ��������� ��������'
,1,1,1)
insert into Tasks VALUES (1,N'����������� ���������� �� ���������� � �������������� SIMD. ���������� ��������� ����� ���� ��������� ������� ������������ ����� ��������� ��������.'
,1,1,1)
insert into Tasks VALUES (1,N'������������ ������� wchar_t * upcase (wchar_t * dst, char * src) � ����������� ������ src, ��������� �� ������������ ��������, � ������ dst (��������� �� ������� ��������), ��� ���� �������� ��������� ����� ���������� �� ��������������� ���������. ��������� � dst.'
,1,1,1)
insert into Tasks VALUES (1,N'������������ ������� char * strncat (char * dest,char * src,unsigned int n) � ������� ����������� � ������ dest ������ src (�������� n ������������ ��������). ���������� dest.'
,1,1,1)


GO
create table Tickets(
Ticket_ID int, --IDENTITY (1,1),
TaskNumber TINYINT not NULL, --�������� ��� �������� ����� 1 � 3, ��� �������
Task_ID int not NULL,
Discipline_ID int not NULL,
CONSTRAINT PK_UNIQUE_Tickets PRIMARY KEY (Ticket_ID,TaskNumber),
FOREIGN KEY (Task_ID,Discipline_ID) references
Tasks (Task_ID,Discipline_ID)
ON DELETE CASCADE
ON UPDATE CASCADE
)
if exists(select * from sys.objects where type='v' and name = 'Table_names')
drop view Table_names

GO
