GO
CREATE DATABASE DrivingSchool_V2
GO
USE DrivingSchool_V2
SET DATEFORMAT DMY

--DROP DATABASE DrivingSchool_V2

create table Licenses(
	LicenseID int primary key identity,
	LicenseName nvarchar(10),
	[Level] int
)

create table [Status](
	StatusID int primary key identity,
	StatusName nvarchar(50)
)

create TABLE Learners (
	LearnerID INT PRIMARY KEY IDENTITY,
	FullName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(11),
    Email NVARCHAR(100),
    [Address] NVARCHAR(255),
    CitizenID NVARCHAR(12) unique,
	Nationality nvarchar(100),
    StatusID int default 1,
    Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
	CitizenID nvarchar(12) unique,
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(11) unique,
    Email NVARCHAR(100),
	Nationality nvarchar(100),
    [Address] NVARCHAR(255),
    EmploymentDate DATE,
    LicenseID INT,
	LicenseNumber nvarchar(12) unique,
	BeginningDate DATE,
    StatusID int default 1,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Vehicles (
    VehicleID INT PRIMARY KEY IDENTITY,
	VehicleName nvarchar(100),
    VehicleNumber NVARCHAR(50) unique,
    IsTruck bit,
    IsPassengerCar bit,
	IsMaintenance bit,
    ManufacturerYear int,
    [Weight] INT,
	Seats int,
	StatusID int default 1,
	Notes nvarchar(max),
	StartMaintenaceDate date,
	EndMaintenaceDate date,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY,
    CourseName NVARCHAR(100) unique,
	LicenseID INT,
    Fee int,
	StatusID int,
    DurationInHours INT,
	HoursStudied int,
	StartDate date,
	EndDate date,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create table Enrollments(
	EnrollmentID int primary key identity,
	CourseID int,
	LearnerID int,
	EnrollmentDate date,
	IsComplete bit default 0,
)

-- chỉ cho phép học viên học 1 khóa học
CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY IDENTITY,
    EnrollmentID INT,
    TeacherID INT,
    VehicleID INT,
    SessionID INT,
	SessionDate DATE,
	StatusID int default 1,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create table [Sessions](
	SessionID int primary key identity,
	[Session] nvarchar(100),
	Created_At DATETIME,
    Updated_At DATETIME
)

CREATE TABLE Invoices (
    InvoiceID int primary key identity,
    InvoiceCode nvarchar(100) unique, -- VD: INV-{HourMinuteSecondDayMonthYear}
	EnrollmentID int,
    TotalAmount int,
	Notes nvarchar(255),
	IsPaid bit, -- 1 là Paid, 0 là Pending
	StatusID int default 1,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY,
    InvoiceID int,
    PaymentDate DATE, 
    Amount int,
    PaymentMethod NVARCHAR(50),
    Created_At DATETIME,
    Updated_At DATETIME 
)
GO

create table Accounts(
	AccountID int primary key identity,
	Email nvarchar(100),
	[Password] nvarchar(100),
	Permission bit,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

alter table Learners
add constraint FK_Learner_Status foreign key (StatusID) references [Status](StatusID)
go

alter table Teachers
add constraint FK_Teacher_License foreign key (LicenseID) references Licenses(LicenseID)
alter table Teachers
add constraint FK_Teacher_Status foreign key (StatusID) references [Status](StatusID)
go

alter table Courses
add constraint FK_Courses_License foreign key (LicenseID) references Licenses(LicenseID)
alter table Courses
add constraint FK_Courses_Status foreign key (StatusID) references [Status](StatusID)
go

alter table Enrollments
add constraint FK_Enrollment_Learner foreign key (LearnerID) references Learners(LearnerID)
alter table Enrollments
add constraint FK_Enrollment_Course foreign key (CourseID) references Courses(CourseID)
go

CREATE UNIQUE INDEX IX_Learner_ActiveCourse
ON Enrollments (LearnerID)
WHERE IsComplete = 0;
go

alter table Vehicles
add constraint FK_Vehicles_Status foreign key (StatusID) references [Status](StatusID)
go

alter table Schedules
add constraint FK_Schedules_Enrollment foreign key (EnrollmentID) references Enrollments(EnrollmentID)
alter table Schedules
add constraint FK_Schedules_Teacher foreign key (TeacherID) references Teachers(TeacherID)
alter table Schedules
add constraint FK_Schedules_Vehicle foreign key (VehicleID) references Vehicles(VehicleID)
alter table Schedules
add constraint FK_Schedules_Session foreign key (SessionID) references [Sessions](SessionID)
alter table Schedules
add constraint FK_Schedules_Status foreign key (StatusID) references [Status](StatusID)
go

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Teacher_SessionDate UNIQUE (TeacherID, SessionDate, SessionID);

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Schedule_Learner UNIQUE (EnrollmentID, SessionDate, SessionID);

ALTER TABLE Schedules
ADD CONSTRAINT UQ_Schedule_Vehicle UNIQUE (VehicleID, SessionDate, SessionID);

alter table Invoices
add constraint FK_Invoices_Enrollment foreign key (EnrollmentID) references Enrollments(EnrollmentID)
alter table Invoices
add constraint FK_Invoices_Status foreign key (StatusID) references [Status](StatusID)
go

alter table Payments
add constraint FK_Payments_Invoice foreign key (InvoiceID) references Invoices(InvoiceID)

INSERT INTO Licenses (LicenseName, [Level])
VALUES('B', 1), ('C', 2), ('D', 3), ('E', 4);

INSERT INTO [Status] (StatusName)
VALUES ('Active'), ('Inactive');

INSERT INTO Learners (FullName, DateOfBirth, Gender, PhoneNumber, Email, [Address], CitizenID, Nationality, Created_At, Updated_At)
VALUES 
('Mai Nguyen Hoang Vu', '1996-07-22', 'Male', '0354377798', 'mainguyenhoangvu.tdc4304@gmail.com', '123 ABC Street', '012345678123', 'Viet Nam', GETDATE(), GETDATE()),
('Le Nguyen Xuan Duoc','1995-05-15', 'Male', '0912345678', 'lenguyenxuanduoc@gmail.com', '123 ABC Street', '012345234123', 'Viet Nam', GETDATE(), GETDATE()),
('Truong Anh Thanh Cong', '1998-07-20', 'Female', '0987654321', 'xcongit@gmail.com', '456 XYZ Street', '987654321432', 'Viet Nam', GETDATE(), GETDATE()),
('Nguyen Van A', '1990-01-01', 'Male', '0912345679', 'nguyenvana@gmail.com', '456 DEF Street', '012345678124', 'Viet Nam', GETDATE(), GETDATE()),
('Tran Thi B', '1992-02-02', 'Female', '0912345680', 'tranthib@gmail.com', '789 GHI Street', '012345678125', 'Viet Nam', GETDATE(), GETDATE()),
('Le Van C', '1988-03-03', 'Male', '0912345681', 'levanc@gmail.com', '321 JKL Street', '012345678126', 'Viet Nam', GETDATE(), GETDATE()),
('Pham Thi D', '1993-04-04', 'Female', '0912345682', 'phamthid@gmail.com', '654 MNO Street', '012345678127', 'Viet Nam', GETDATE(), GETDATE()),
('Hoang Van E', '1991-05-05', 'Male', '0912345683', 'hoangvane@gmail.com', '987 PQR Street', '012345678128', 'Viet Nam', GETDATE(), GETDATE()),
('Nguyen Van F', '1994-06-06', 'Male', '0912345684', 'nguyenvanf@gmail.com', '135 STU Street', '012345678129', 'Viet Nam', GETDATE(), GETDATE()),
('Tran Thi G', '1989-07-07', 'Female', '0912345685', 'tranthig@gmail.com', '246 VWX Street', '012345678130', 'Viet Nam', GETDATE(), GETDATE()),
('Le Van H', '1996-08-08', 'Male', '0912345686', 'levanh@gmail.com', '357 YZ Street', '012345678131', 'Viet Nam', GETDATE(), GETDATE()),
('Pham Thi I', '1997-09-09', 'Female', '0912345687', 'phamthi@gmail.com', '468 ABCD Street', '012345678132', 'Viet Nam', GETDATE(), GETDATE());

INSERT INTO Teachers (FullName, CitizenID, DateOfBirth, Gender, PhoneNumber, Email, Nationality, [Address], EmploymentDate, LicenseID, LicenseNumber, BeginningDate, Created_At, Updated_At)
VALUES 
('Le Van C', 123456789534, '1980-11-25', 'Male', '0909123456', '22211tt4304@mail.tdc.edu.vn', 'Vietnam', '789 DEF Street', '2010-06-15', 1, 923481231231, '2009-06-01', GETDATE(), GETDATE()),
('Pham Thi D', 987654321756, '1985-03-30', 'Female', '0909876543', 'thid@gmail.com', 'Vietnam', '101 GHI Street', '2015-08-20', 2, 873456123456, '2014-07-15', GETDATE(), GETDATE()),
('Nguyen Thi A', 987654321345, '1985-03-20', 'Female', '0909123457', 'thia@gmail.com', 'Vietnam', '123 ABC Street', '2012-07-10', 2, 762341237891, '2010-05-20', GETDATE(), GETDATE()),
('Tran Van B', 192837465875, '1990-08-14', 'Male', '0909123458', 'vanb@gmail.com', 'Vietnam', '456 GHI Street', '2015-09-25', 1, 634567238910, '2014-05-10', GETDATE(), GETDATE()),
('Pham Thi D', 123456780876, '1992-12-30', 'Female', '0909123459', 'thid@gmail.com', 'Vietnam', '321 JKL Street', '2016-01-15', 4, 123987456321, '2015-06-01', GETDATE(), GETDATE()),
('Nguyen Van E', 234567891546, '1988-02-18', 'Male', '0909123460', 'vane@gmail.com', 'Vietnam', '654 MNO Street', '2013-03-28', 3, 456789231234, '2012-05-25', GETDATE(), GETDATE()),
('Hoang Thi F', 345678912645, '1995-05-11', 'Female', '0909123461', 'thif@gmail.com', 'Vietnam', '987 PQR Street', '2018-11-05', 4, 324159678231, '2017-07-15', GETDATE(), GETDATE()),
('Le Van G', 456789023756, '1991-07-21', 'Male', '0909123462', 'vang@gmail.com', 'Vietnam', '258 STU Street', '2017-10-30', 3, 789132456876, '2016-09-01', GETDATE(), GETDATE()),
('Tran Van H', 567890134534, '1989-04-15', 'Male', '0909123463', 'vanh@gmail.com', 'Vietnam', '369 VWX Street', '2011-08-20', 1, 564738291034, '2010-06-10', GETDATE(), GETDATE()),
('Pham Thi I', 678901245876, '1993-01-12', 'Female', '0909123464', 'thii@gmail.com', 'Vietnam', '147 YZ Street', '2014-05-30', 2, 987654321123, '2013-04-01', GETDATE(), GETDATE()),
('Nguyen Van J', 789012356536, '1994-10-05', 'Male', '0909123465', 'vanj@gmail.com', 'Vietnam', '258 ABCD Street', '2019-04-20', 3, 321456789012, '2018-03-15', GETDATE(), GETDATE());

INSERT INTO Vehicles (VehicleNumber, VehicleName, IsTruck, IsPassengerCar, IsMaintenance, ManufacturerYear, [Weight], Seats, Notes, StatusID, Created_At, Updated_At)
VALUES 
-- Xe dành cho bằng B (xe con dưới 9 chỗ)
('61A-56478', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, GETDATE(), GETDATE()),
('61A-23467', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, GETDATE(), GETDATE()),
('61A-89625', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, GETDATE(), GETDATE()),
('61A-75832', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, GETDATE(), GETDATE()),
('61A-84784', 'Toyota Camry', 0, 1, 1, 2018, null, 5, 'Sedan, good condition', 1, GETDATE(), GETDATE()),
-- Xe dành cho bằng C (xe tải)
('61C-23523', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, GETDATE(), GETDATE()),
('61C-56434', 'Isuzu Truck', 1, 0, 0, 2016, 5000, null, '', 1, GETDATE(), GETDATE()),
('61C-96793', 'Isuzu Truck', 1, 0, 0, 2019, 5000, null, '', 1, GETDATE(), GETDATE()),
('61C-02354', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, GETDATE(), GETDATE()),
('61C-87345', 'Isuzu Truck', 1, 0, 1, 2021, 5000, null, '', 1, GETDATE(), GETDATE()),
('61C-80346', 'Isuzu Truck', 1, 0, 1, 2020, 5000, null, '', 1, GETDATE(), GETDATE()),
-- Xe dành cho bằng D (xe khách từ 10 đến 30 chỗ)
('61D-98765', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', 1, GETDATE(), GETDATE()),
('61D-42345', 'Mercedes-Benz Bus', 0, 1, 1, 2019, null, 20, '', 1, GETDATE(), GETDATE()),
('61D-64565', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', 1, GETDATE(), GETDATE()),
('61D-53463', 'Mercedes-Benz Bus', 0, 1, 0, 2018, null, 20, '', 1, GETDATE(), GETDATE()),
('61D-32435', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, '', 1, GETDATE(), GETDATE()),
-- Xe dành cho bằng E (xe khách trên 30 chỗ)
('70E-54321', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, GETDATE(), GETDATE()),
('70E-65346', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, GETDATE(), GETDATE()),
('70E-32455', 'Hyundai Universe', 0, 1, 1, 2016, null, 40, '', 1, GETDATE(), GETDATE()),
('70E-47478', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, GETDATE(), GETDATE()),
('70E-69632', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, GETDATE(), GETDATE());

INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, HoursStudied, StatusID, StartDate, EndDate, Created_At, Updated_At)
VALUES 
('B-090532131024', 1, 11000000, 588, 0, 1, '2024-01-01', DATEADD(MONTH, 6, '2024-01-01'), GETDATE(), GETDATE()),
('B-080756241024', 1, 11000000, 588, 0, 1, '2024-02-01', DATEADD(MONTH, 6, '2024-02-01'), GETDATE(), GETDATE()),
('B-080756241027', 1, 11000000, 588, 584, 1, '2024-03-01', DATEADD(MONTH, 6, '2024-03-01'), GETDATE(), GETDATE()),
('C-020654131027', 2, 12000000, 920, 0, 1, '2024-04-01', DATEADD(MONTH, 6, '2024-04-01'), GETDATE(), GETDATE()),
('C-053354131024', 2, 12000000, 920, 0, 1, '2024-05-01', DATEADD(MONTH, 6, '2024-05-01'), GETDATE(), GETDATE()),
('C-540856241024', 2, 12000000, 920, 0, 1, '2024-06-01', DATEADD(MONTH, 6, '2024-06-01'), GETDATE(), GETDATE()),
('D-091122131024', 3, 15000000, 192, 0, 1, '2024-07-01', DATEADD(MONTH, 6, '2024-07-01'), GETDATE(), GETDATE()),
('D-450456241024', 3, 15000000, 192, 0, 1, '2024-08-01', DATEADD(MONTH, 6, '2024-08-01'), GETDATE(), GETDATE()),
('E-092433131024', 4, 20000000, 192, 0, 1, '2024-09-01', DATEADD(MONTH, 6, '2024-09-01'), GETDATE(), GETDATE()),
('E-540826241024', 4, 20000000, 192, 0, 1, '2024-10-01', DATEADD(MONTH, 6, '2024-10-01'), GETDATE(), GETDATE());

INSERT INTO [Sessions] ([Session], Created_At, Updated_At)
VALUES 
('7H30-9H30', GETDATE(), GETDATE()),
('9H30-11H30', GETDATE(), GETDATE()),
('13H00-15H00', GETDATE(), GETDATE()),
('15H00-17H00', GETDATE(), GETDATE());

insert into Enrollments(LearnerID, CourseID, EnrollmentDate)
values
(1, 1, GETDATE()),
(2, 2, GETDATE()),
(3, 4, GETDATE());

INSERT INTO Schedules (EnrollmentID, TeacherID, VehicleID, SessionID, SessionDate, StatusID, Created_At, Updated_At)
VALUES
(1, 1, 1, 1, '2024-10-15', 1, GETDATE(), GETDATE()),
(2, 2, 2, 2, '2024-10-20', 1, GETDATE(), GETDATE()),
(3, 2, 4, 3, '2024-10-25', 1, GETDATE(), GETDATE());

INSERT INTO Invoices (InvoiceCode, EnrollmentID, TotalAmount, IsPaid, StatusID, Created_At, Updated_At)
VALUES 
('INV-092444131024', 1, 11000000, 0, 1, GETDATE(), GETDATE()),
('INV-093023131024', 2, 11000000, 1, 1, GETDATE(), GETDATE());

INSERT INTO Payments (InvoiceID, PaymentDate, Amount, PaymentMethod, Created_At, Updated_At)
VALUES 
(1, '2024-10-12', 7000000, 'Credit Card', GETDATE(), GETDATE()),
(2, '2024-10-13', 5000000, 'Cash', GETDATE(), GETDATE());

INSERT INTO Accounts (Email, [Password], Permission, Created_At, Updated_At)
VALUES 
('mainguyenhoangvu2212@gmail.com', 'admin1', 1, GETDATE(), GETDATE());


INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, HoursStudied, StatusID, StartDate, EndDate, Created_At, Updated_At)
VALUES 
('C-103232021124', 2, 12000000, 920, 0, 1, '2024-11-02', DATEADD(MONTH, 6, '2024-11-02'), GETDATE(), GETDATE())