GO
CREATE DATABASE DrivingSchool
GO
USE DrivingSchool
SET DATEFORMAT DMY

--DROP DATABASE DrivingSchool

create table Licenses(
	LicenseID int primary key identity(1001, 1),
	LicenseName char,
	[Level] int
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
	Nationality nvarchar(100),
    [Status] NVARCHAR(20),
    Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY IDENTITY(1001,1),
    FullName NVARCHAR(100),
	CitizenID nvarchar(12),
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
    VehicleNumber NVARCHAR(50) unique,
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

-- chỉ cho phép học viên học 1 khóa học
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

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Schedule_Teacher UNIQUE (TeacherID, SessionDate, SessionID);

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Schedule_Learner UNIQUE (LearnerID, SessionDate, SessionID);

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Schedule_Vehicle UNIQUE (VehicleID, SessionDate, SessionID);

alter table Invoices
add constraint FK_Invoices_Schedule foreign key (ScheduleID) references Schedules(ScheduleID)
go

alter table Payments
add constraint FK_Payments_Invoice foreign key (InvoiceID) references Invoices(InvoiceID)

INSERT INTO Licenses (LicenseName, [Level])
VALUES ('B', 1), ('C', 2), ('D', 3), ('E', 4);

INSERT INTO Learners (FullName, CurrentLicenseID ,DateOfBirth, Gender, PhoneNumber, Email, [Address], CitizenID, [Status], Created_At, Updated_At)
VALUES 
('Mai Nguyen Hoang Vu', 1001, '1996-07-22', 'Male', '0354377798', 'mainguyenhoangvu.tdc4304@gmail.com', '123 ABC Street', '012345678123', 'Active', GETDATE(), GETDATE()),
('Le Nguyen Xuan Duoc', 1002, '1995-05-15', 'Male', '0912345678', 'lenguyenxuanduoc@gmail.com', '123 ABC Street', '012345678123', 'Active', GETDATE(), GETDATE()),
('Truong Anh Thanh Cong', 1001, '1998-07-20', 'Female', '0987654321', 'xcongit@gmail.com', '456 XYZ Street', '987654321432', 'Inactive', GETDATE(), GETDATE()),
('Nguyen Van A', 1001, '1990-01-01', 'Male', '0912345679', 'nguyenvana@gmail.com', '456 DEF Street', '012345678124', 'Active', GETDATE(), GETDATE()),
('Tran Thi B', 1001, '1992-02-02', 'Female', '0912345680', 'tranthib@gmail.com', '789 GHI Street', '012345678125', 'Active', GETDATE(), GETDATE()),
('Le Van C', 1001, '1988-03-03', 'Male', '0912345681', 'levanc@gmail.com', '321 JKL Street', '012345678126', 'Active', GETDATE(), GETDATE()),
('Pham Thi D', 1001, '1993-04-04', 'Female', '0912345682', 'phamthid@gmail.com', '654 MNO Street', '012345678127', 'Active', GETDATE(), GETDATE()),
('Hoang Van E', 1001, '1991-05-05', 'Male', '0912345683', 'hoangvane@gmail.com', '987 PQR Street', '012345678128', 'Active', GETDATE(), GETDATE()),
('Nguyen Van F', 1001, '1994-06-06', 'Male', '0912345684', 'nguyenvanf@gmail.com', '135 STU Street', '012345678129', 'Active', GETDATE(), GETDATE()),
('Tran Thi G', 1001, '1989-07-07', 'Female', '0912345685', 'tranthig@gmail.com', '246 VWX Street', '012345678130', 'Active', GETDATE(), GETDATE()),
('Le Van H', 1001, '1996-08-08', 'Male', '0912345686', 'levanh@gmail.com', '357 YZ Street', '012345678131', 'Active', GETDATE(), GETDATE()),
('Pham Thi I', 1001, '1997-09-09', 'Female', '0912345687', 'phamthi@gmail.com', '468 ABCD Street', '012345678132', 'Active', GETDATE(), GETDATE());

INSERT INTO Teachers (FullName, CitizenID, DateOfBirth, Gender, Phone, Email, Nationality, [Address], EmploymentDate, LicenseID, [Status], GraduatedDate, Created_At, Updated_At)
VALUES 
('Le Van C', 123456789, '1980-11-25', 'Male', '0909123456', '22211tt4304@mail.tdc.edu.vn', 'Vietnam', '789 DEF Street', '2010-06-15', 1001, 'Active', '2009-06-01', GETDATE(), GETDATE()),
('Pham Thi D', 987654321, '1985-03-30', 'Female', '0909876543', 'thid@gmail.com', 'Vietnam', '101 GHI Street', '2015-08-20', 1002, 'On Leave', '2014-07-15', GETDATE(), GETDATE()),
('Nguyen Thi A', 987654321, '1985-03-20', 'Female', '0909123457', 'thia@gmail.com', 'Vietnam', '123 ABC Street', '2012-07-10', 1002, 'Active', '2010-05-20', GETDATE(), GETDATE()),
('Tran Van B', 192837465, '1990-08-14', 'Male', '0909123458', 'vanb@gmail.com', 'Vietnam', '456 GHI Street', '2015-09-25', 1001, 'Active', '2014-05-10', GETDATE(), GETDATE()),
('Pham Thi D', 123456780, '1992-12-30', 'Female', '0909123459', 'thid@gmail.com', 'Vietnam', '321 JKL Street', '2016-01-15', 1004, 'Active', '2015-06-01', GETDATE(), GETDATE()),
('Nguyen Van E', 234567891, '1988-02-18', 'Male', '0909123460', 'vane@gmail.com', 'Vietnam', '654 MNO Street', '2013-03-28', 1003, 'Active', '2012-05-25', GETDATE(), GETDATE()),
('Hoang Thi F', 345678912, '1995-05-11', 'Female', '0909123461', 'thif@gmail.com', 'Vietnam', '987 PQR Street', '2018-11-05', 1004, 'Active', '2017-07-15', GETDATE(), GETDATE()),
('Le Van G', 456789023, '1991-07-21', 'Male', '0909123462', 'vang@gmail.com', 'Vietnam', '258 STU Street', '2017-10-30', 1003, 'Active', '2016-09-01', GETDATE(), GETDATE()),
('Tran Van H', 567890134, '1989-04-15', 'Male', '0909123463', 'vanh@gmail.com', 'Vietnam', '369 VWX Street', '2011-08-20', 1001, 'Active', '2010-06-10', GETDATE(), GETDATE()),
('Pham Thi I', 678901245, '1993-01-12', 'Female', '0909123464', 'thii@gmail.com', 'Vietnam', '147 YZ Street', '2014-05-30', 1002, 'Active', '2013-04-01', GETDATE(), GETDATE()),
('Nguyen Van J', 789012356, '1994-10-05', 'Male', '0909123465', 'vanj@gmail.com', 'Vietnam', '258 ABCD Street', '2019-04-20', 1003, 'Active', '2018-03-15', GETDATE(), GETDATE());

INSERT INTO Vehicles (VehicleNumber, VehicleName, IsTruck, IsPassengerCar, IsMaintenance, ManufacturerYear, [Weight], Seats, Notes, Created_At, Updated_At)
VALUES 
-- Xe dành cho bằng B (xe con dưới 9 chỗ)
('61A-56478', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
('61A-23467', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
('61A-89625', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
('61A-75832', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
('61A-84784', 'Toyota Camry', 0, 1, 1, 2018, null, 5, 'Sedan, good condition', GETDATE(), GETDATE()),
-- Xe dành cho bằng C (xe tải)
('61C-23523', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', GETDATE(), GETDATE()),
('61C-56434', 'Isuzu Truck', 1, 0, 0, 2016, 5000, null, '', GETDATE(), GETDATE()),
('61C-96793', 'Isuzu Truck', 1, 0, 0, 2019, 5000, null, '', GETDATE(), GETDATE()),
('61C-02354', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', GETDATE(), GETDATE()),
('61C-87345', 'Isuzu Truck', 1, 0, 1, 2021, 5000, null, '', GETDATE(), GETDATE()),
('61C-80346', 'Isuzu Truck', 1, 0, 1, 2020, 5000, null, '', GETDATE(), GETDATE()),
-- Xe dành cho bằng D (xe khách từ 10 đến 30 chỗ)
('61D-98765', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', GETDATE(), GETDATE()),
('61D-42345', 'Mercedes-Benz Bus', 0, 1, 1, 2019, null, 20, '', GETDATE(), GETDATE()),
('61D-64565', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', GETDATE(), GETDATE()),
('61D-53463', 'Mercedes-Benz Bus', 0, 1, 0, 2018, null, 20, '', GETDATE(), GETDATE()),
('61D-32435', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, '', GETDATE(), GETDATE()),
-- Xe dành cho bằng E (xe khách trên 30 chỗ)
('70E-54321', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', GETDATE(), GETDATE()),
('70E-65346', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', GETDATE(), GETDATE()),
('70E-32455', 'Hyundai Universe', 0, 1, 1, 2016, null, 40, '', GETDATE(), GETDATE()),
('70E-47478', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', GETDATE(), GETDATE()),
('70E-69632', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', GETDATE(), GETDATE());

INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, Created_At, Updated_At)
VALUES 
('B-090532131024', 1001, 15000000, 340, GETDATE(), GETDATE()),
('B-080756241024', 1001, 15000000, 340, GETDATE(), GETDATE()),
('C-090654131024', 1002, 20000000, 752, GETDATE(), GETDATE()),
('C-540856241024', 1002, 20000000, 752, GETDATE(), GETDATE()),
('D-091122131024', 1003, 20000000, 192, GETDATE(), GETDATE()),
('D-450456241024', 1003, 20000000, 192, GETDATE(), GETDATE()),
('E-092433131024', 1004, 20000000, 336, GETDATE(), GETDATE()),
('E-540826241024', 1004, 20000000, 336, GETDATE(), GETDATE());

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
(1003, 1002, 1007, 1003, 1003, '2024-10-25', GETDATE(), GETDATE());

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

