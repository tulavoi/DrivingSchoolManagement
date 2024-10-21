GO
CREATE DATABASE DrivingSchool
GO
USE DrivingSchool
SET DATEFORMAT DMY

--DROP DATABASE DrivingSchool

create table Licenses(
	LicenseID int primary key identity(1001, 1),
	LicenseName char,
)

create TABLE Learners (
	LearnerID INT PRIMARY KEY IDENTITY(1001,1),
	CurrentLicenseID int,
	FullName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(15),
    Email NVARCHAR(100),
    [Address] NVARCHAR(255),
    CitizenID NVARCHAR(50),
    [Status] NVARCHAR(20),
    Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY IDENTITY(1001,1),
    FullName NVARCHAR(100),
	CitizenID int,
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Phone NVARCHAR(15),
    Email NVARCHAR(100),
	Nationality nvarchar(100),
    [Address] NVARCHAR(255),
    EmploymentDate DATE,
    LicenseID INT,
    [Status] NVARCHAR(50),
	GraduatedDate DATE,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Vehicles (
    VehicleID INT PRIMARY KEY IDENTITY(1001,1),
	VehicleName nvarchar(100),
    VehicleNumber NVARCHAR(50),
    IsTruck bit,
    IsPassengerCar bit,
	IsMaintenance bit,
    ManufacturerYear int,
    [Weight] INT,
	Seats int,
	Notes nvarchar(max),
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1001,1),
    CourseName NVARCHAR(100) unique,
	LicenseID INT,
    Fee DECIMAL(18, 2),
    DurationInHours INT,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY IDENTITY(1001,1),
    LearnerID INT,
    TeacherID INT,
    VehicleID INT,
    CourseID INT,
    SessionID INT,
	SessionDate DATE,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create table [Sessions](
	SessionID int primary key identity(1001, 1),
	[Session] nvarchar(100),
	Created_At DATETIME,
    Updated_At DATETIME
)

CREATE TABLE Invoices (
    InvoiceID int primary key identity(1001, 1),
    InvoiceCode nvarchar(100) unique, -- VD: INV-{InvoiceDate}
	ScheduleID int,
    TotalAmount DECIMAL(18, 2),
	Notes nvarchar(255),
    [Status] NVARCHAR(50) CHECK ([Status] IN ('Pending', 'Paid')),
	Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID int,
    PaymentDate DATE, 
    Amount DECIMAL(18, 2),
    PaymentMethod NVARCHAR(50),
    Created_At DATETIME,
    Updated_At DATETIME 
)
GO

create table Accounts(
	AccountID int primary key identity(1001, 1),
	Email nvarchar(100),
	[Password] nvarchar(100),
	Permission bit,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

alter table Learners
add constraint FK_Learner_License foreign key (CurrentLicenseID) references Licenses(LicenseID)
go

alter table Teachers
add constraint FK_Teacher_License foreign key (LicenseID) references Licenses(LicenseID)
go

alter table Courses
add constraint FK_Courses_License foreign key (LicenseID) references Licenses(LicenseID)
go

alter table Schedules
add constraint FK_Schedules_Learner foreign key (LearnerID) references Learners(LearnerID)
alter table Schedules
add constraint FK_Schedules_Teacher foreign key (TeacherID) references Teachers(TeacherID)
alter table Schedules
add constraint FK_Schedules_Vehicle foreign key (VehicleID) references Vehicles(VehicleID)
alter table Schedules
add constraint FK_Schedules_Course foreign key (CourseID) references Courses(CourseID)
alter table Schedules
add constraint FK_Schedules_Session foreign key (SessionID) references [Sessions](SessionID)
go

alter table Invoices
add constraint FK_Invoices_Schedule foreign key (ScheduleID) references Schedules(ScheduleID)
go

alter table Payments
add constraint FK_Payments_Invoice foreign key (InvoiceID) references Invoices(InvoiceID)

INSERT INTO Licenses (LicenseName)
VALUES ('B'), ('C'), ('D'), ('E');

INSERT INTO Learners (FullName, CurrentLicenseID ,DateOfBirth, Gender, PhoneNumber, Email, [Address], CitizenID, [Status], Created_At, Updated_At)
VALUES 
('Mai Nguyen Hoang Vu', 1001, '1996-07-22', 'Male', '0354377798', 'mainguyenhoangvu.tdc4304@gmail.com', '123 ABC Street', '012345678123', 'Active', GETDATE(), GETDATE()),
('Le Nguyen Xuan Duoc', 1002, '1995-05-15', 'Male', '0912345678', 'lenguyenxuanduoc@gmail.com', '123 ABC Street', '012345678123', 'Active', GETDATE(), GETDATE()),
('Truong Anh Thanh Cong', 1001, '1998-07-20', 'Female', '0987654321', 'xcongit@gmail.com', '456 XYZ Street', '987654321432', 'Inactive', GETDATE(), GETDATE());

INSERT INTO Teachers (FullName, CitizenID, DateOfBirth, Gender, Phone, Email, Nationality, [Address], EmploymentDate, LicenseID, [Status], GraduatedDate, Created_At, Updated_At)
VALUES 
('Le Van C', 123456789, '1980-11-25', 'Male', '0909123456', 'vanc@gmail.com', 'Vietnam', '789 DEF Street', '2010-06-15', 1001, 'Active', '2009-06-01', GETDATE(), GETDATE()),
('Pham Thi D', 987654321, '1985-03-30', 'Female', '0909876543', 'thid@gmail.com', 'Vietnam', '101 GHI Street', '2015-08-20', 1002, 'On Leave', '2014-07-15', GETDATE(), GETDATE());

INSERT INTO Vehicles (VehicleNumber, VehicleName, IsTruck, IsPassengerCar, IsMaintenance, ManufacturerYear, [Weight], Seats, Notes, Created_At, Updated_At)
VALUES 
-- Xe dành cho bằng B (xe con dưới 9 chỗ)
('61A-12345', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
('61A-67890', 'Ford Everest', 0, 1, 0, 2019, null, 7, 'SUV, new condition', GETDATE(), GETDATE()),
-- Xe dành cho bằng C (xe tải)
('61C-54321', 'Isuzu Truck', 1, 0, 1, 2020, 5000, null, 'Truck, needs maintenance', GETDATE(), GETDATE()),
-- Xe dành cho bằng D (xe khách từ 10 đến 30 chỗ)
('61D-98765', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, 'Passenger bus, good condition', GETDATE(), GETDATE()),
-- Xe dành cho bằng E (xe khách trên 30 chỗ)
('70E-54321', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, 'Large bus, good condition', GETDATE(), GETDATE());

INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, Created_At, Updated_At)
VALUES 
('B-090532131024', 1001, 15000000, 340, GETDATE(), GETDATE()),
('C-090654131024', 1002, 20000000, 752, GETDATE(), GETDATE()),
('D-091122131024', 1003, 20000000, 192, GETDATE(), GETDATE()),
('E-092433131024', 1004, 20000000, 336, GETDATE(), GETDATE());

INSERT INTO [Sessions] ([Session], Created_At, Updated_At)
VALUES 
('7H30-9H30', GETDATE(), GETDATE()),
('9H30-11H30', GETDATE(), GETDATE()),
('13H00-15H00', GETDATE(), GETDATE()),
('15H00-17H00', GETDATE(), GETDATE());

INSERT INTO Schedules (LearnerID, TeacherID, VehicleID, CourseID, SessionID, SessionDate, Created_At, Updated_At)
VALUES
(1001, 1001, 1001, 1001, 1001, '2024-10-15', GETDATE(), GETDATE()),
(1002, 1002, 1003, 1002, 1002, '2024-10-20', GETDATE(), GETDATE()),
(1003, 1002, 1003, 1003, 1003, '2024-10-25', GETDATE(), GETDATE());

INSERT INTO Invoices (InvoiceCode, ScheduleID, TotalAmount, [Status], Created_At, Updated_At)
VALUES 
('INV-092444131024', 1001, 1500.00, 'Pending', GETDATE(), GETDATE()),
('INV-093023131024', 1002, 2000.00, 'Paid', GETDATE(), GETDATE());

INSERT INTO Payments (InvoiceID, PaymentDate, Amount, PaymentMethod, Created_At, Updated_At)
VALUES 
(1001, '2024-10-12', 7000000, 'Credit Card', GETDATE(), GETDATE()),
(1002, '2024-10-13', 5000000, 'Cash', GETDATE(), GETDATE());

INSERT INTO Accounts (Email, [Password], Permission, Created_At, Updated_At)
VALUES 
('mainguyenhoangvu2212@gmail.com', 'admin1', 1, GETDATE(), GETDATE());
