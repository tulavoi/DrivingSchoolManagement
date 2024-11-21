GO
CREATE DATABASE DrivingSchool_V2
GO
USE DrivingSchool_V2
--SET DATEFORMAT DMY

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
    DateOfBirth DATETIME,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(11) unique,
    Email NVARCHAR(100),
	Nationality nvarchar(100),
    [Address] NVARCHAR(255),
    EmploymentDate DATETIME,
    LicenseID INT,
	LicenseNumber nvarchar(12) unique,
	BeginningDate DATETIME,
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
	StartMaintenaceDate DATETIME,
	EndMaintenaceDate DATETIME,
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
	StartDate DATETIME,
	EndDate DATETIME,
	Created_At DATETIME,
    Updated_At DATETIME
)
go

create table Enrollments(
	EnrollmentID int primary key identity,
	CourseID int,
	LearnerID int,
	EnrollmentDate DATETIME,
	IsComplete bit default 0,
)

-- chỉ cho phép học viên học 1 khóa học
CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY IDENTITY,
    EnrollmentID INT,
    TeacherID INT,
    VehicleID INT,
    SessionID INT,
	SessionDate DATETIME,
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
    PaymentDate DATETIME, 
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
('Mai Nguyen Hoang Vu', '22/07/1996', 'Male', '0354377798', 'mainguyenhoangvu.tdc4304@gmail.com', '123 ABC Street', '012345678123', 'Viet Nam', GETDATE(), GETDATE()),
('Le Nguyen Xuan Duoc','15/05/1995', 'Male', '0912345678', 'lenguyenxuanduoc@gmail.com', '123 ABC Street', '012345234123', 'Viet Nam', GETDATE(), GETDATE()),
('Truong Anh Thanh Cong', '20/07/1998', 'Female', '0987654321', 'xcongit@gmail.com', '456 XYZ Street', '987654321432', 'Viet Nam', GETDATE(), GETDATE()),
('Nguyen Van A', '01/01/1990', 'Male', '0912345679', 'nguyenvana@gmail.com', '456 DEF Street', '012345678124', 'Viet Nam', GETDATE(), GETDATE()),
('Tran Thi B', '02/02/1992', 'Female', '0912345680', 'tranthib@gmail.com', '789 GHI Street', '012345678125', 'Viet Nam', GETDATE(), GETDATE()),
('Le Van C', '03/03/1988', 'Male', '0912345681', 'levanc@gmail.com', '321 JKL Street', '012345678126', 'Viet Nam', GETDATE(), GETDATE()),
('Le Van H', '08/08/1996', 'Male', '0912345686', 'levanh@gmail.com', '357 YZ Street', '012345678131', 'Viet Nam', GETDATE(), GETDATE()),
('Pham Thi I', '09/09/1997', 'Female', '0912345687', 'phamthi@gmail.com', '468 ABCD Street', '012345678132', 'Viet Nam', GETDATE(), GETDATE());

INSERT INTO Teachers (FullName, CitizenID, DateOfBirth, Gender, PhoneNumber, Email, Nationality, [Address], EmploymentDate, LicenseID, LicenseNumber, BeginningDate, Created_At, Updated_At) 
VALUES 
('Le Van C', 123456789534, '25/11/1980', 'Male', '0909123456', '22211tt4304@mail.tdc.edu.vn', 'Vietnam', '789 DEF Street', '15/06/2010', 1, 923481231231, '01/06/2009', GETDATE(), GETDATE()),
('Mai Thi Z', 987654321756, '30/03/1985', 'Female', '0909876543', 'thid@gmail.com', 'Vietnam', '101 GHI Street', '20/08/2015', 2, 873456123456, '15/07/2014', GETDATE(), GETDATE()),
('Nguyen Thi A', 987654321345, '20/03/1985', 'Female', '0909123457', 'thia@gmail.com', 'Vietnam', '123 ABC Street', '10/07/2012', 2, 762341237891, '20/05/2010', GETDATE(), GETDATE()),
('Tran Van B', 192837465875, '14/08/1990', 'Male', '0909123458', 'vanb@gmail.com', 'Vietnam', '456 GHI Street', '25/09/2015', 1, 634567238910, '10/05/2014', GETDATE(), GETDATE()),
('Pham Thi D', 123456780876, '30/12/1992', 'Female', '0909123459', 'thid@gmail.com', 'Vietnam', '321 JKL Street', '15/01/2016', 4, 123987456321, '01/06/2015', GETDATE(), GETDATE()),
('Nguyen Van E', 234567891546, '18/02/1988', 'Male', '0909123460', 'vane@gmail.com', 'Vietnam', '654 MNO Street', '28/03/2013', 3, 456789231234, '25/05/2012', GETDATE(), GETDATE()),
('Hoang Thi F', 345678912645, '11/05/1995', 'Female', '0909123461', 'thif@gmail.com', 'Vietnam', '987 PQR Street', '05/11/2018', 4, 324159678231, '15/07/2017', GETDATE(), GETDATE()),
('Le Van G', 456789023756, '21/07/1991', 'Male', '0909123462', 'vang@gmail.com', 'Vietnam', '258 STU Street', '30/10/2017', 3, 789132456876, '01/09/2016', GETDATE(), GETDATE()),
('Tran Van H', 567890134534, '15/04/1989', 'Male', '0909123463', 'vanh@gmail.com', 'Vietnam', '369 VWX Street', '20/08/2011', 1, 564738291034, '10/06/2010', GETDATE(), GETDATE()),
('Pham Thi I', 678901245876, '12/01/1993', 'Female', '0909123464', 'thii@gmail.com', 'Vietnam', '147 YZ Street', '30/05/2014', 2, 987654321123, '01/04/2013', GETDATE(), GETDATE()),
('Nguyen Van J', 789012356536, '05/10/1994', 'Male', '0909123465', 'vanj@gmail.com', 'Vietnam', '258 ABCD Street', '20/04/2019', 3, 321456789012, '15/03/2018', GETDATE(), GETDATE());


INSERT INTO Vehicles (VehicleNumber, VehicleName, IsTruck, IsPassengerCar, IsMaintenance, ManufacturerYear, [Weight], Seats, Notes, StatusID, StartMaintenaceDate, EndMaintenaceDate, Created_At, Updated_At)
VALUES 
-- Xe dành cho bằng B (xe con dưới 9 chỗ)
('61A-56478', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-23467', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-89625', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-75832', 'Toyota Camry', 0, 1, 0, 2018, null, 5, 'Sedan, good condition', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-84784', 'Toyota Camry', 0, 1, 1, 2018, null, 5, 'Sedan, good condition', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
-- Xe dành cho bằng C (xe tải)
('61C-23523', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-56434', 'Isuzu Truck', 1, 0, 0, 2016, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-96793', 'Isuzu Truck', 1, 0, 0, 2019, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-02354', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-87345', 'Isuzu Truck', 1, 0, 1, 2021, 5000, null, '', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61C-80346', 'Isuzu Truck', 1, 0, 1, 2020, 5000, null, '', 1, DATEADD(DAY, -RAND() * 120, GETDATE()), DATEADD(DAY, -RAND() * 60, GETDATE()), GETDATE(), GETDATE()),
-- Xe dành cho bằng D (xe khách từ 10 đến 30 chỗ)
('61D-98765', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61D-42345', 'Mercedes-Benz Bus', 0, 1, 1, 2019, null, 20, '', 1, DATEADD(DAY, -RAND() * 120, GETDATE()), DATEADD(DAY, -RAND() * 60, GETDATE()), GETDATE(), GETDATE()),
('61D-64565', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, '', 1, DATEADD(DAY, -RAND() * 130, GETDATE()), DATEADD(DAY, -RAND() * 70, GETDATE()), GETDATE(), GETDATE()),
('61D-53463', 'Mercedes-Benz Bus', 0, 1, 0, 2018, null, 20, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61D-32435', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, '', 1, NULL, NULL, GETDATE(), GETDATE()),
-- Xe dành cho bằng E (xe khách trên 30 chỗ)
('70E-54321', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-65346', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-32455', 'Hyundai Universe', 0, 1, 1, 2016, null, 40, '', 1, DATEADD(DAY, -RAND() * 90, GETDATE()), DATEADD(DAY, -RAND() * 40, GETDATE()), GETDATE(), GETDATE()),
('70E-47478', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-69632', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE());

INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, HoursStudied, StatusID, StartDate, EndDate, Created_At, Updated_At)
VALUES 
('B-090532131024', 1, 11000000, 588, 0, 1, '2024-10-01', DATEADD(MONTH, 6, CAST('2024-10-01' AS DATE)), GETDATE(), GETDATE()),
('B-080756241024', 1, 11000000, 588, 0, 1, '2024-10-02', DATEADD(MONTH, 6, CAST('2024-10-02' AS DATE)), GETDATE(), GETDATE()),
('B-080756241027', 1, 11000000, 588, 584, 1, '2024-10-03', DATEADD(MONTH, 6, CAST('2024-10-03' AS DATE)), GETDATE(), GETDATE()),
('C-020654131027', 2, 12000000, 920, 0, 1, '2024-11-02', DATEADD(MONTH, 6, CAST('2024-11-02' AS DATE)), GETDATE(), GETDATE()),
('C-053354131024', 2, 12000000, 920, 0, 1, '2024-11-03', DATEADD(MONTH, 6, CAST('2024-11-03' AS DATE)), GETDATE(), GETDATE()),
('C-540856241024', 2, 12000000, 920, 0, 1, '2024-11-04', DATEADD(MONTH, 6, CAST('2024-11-04' AS DATE)), GETDATE(), GETDATE()),
('D-091122131024', 3, 15000000, 192, 0, 1, '2024-11-05', DATEADD(MONTH, 6, CAST('2024-11-05' AS DATE)), GETDATE(), GETDATE()),
('D-450456241024', 3, 15000000, 192, 0, 1, '2024-11-06', DATEADD(MONTH, 6, CAST('2024-11-06' AS DATE)), GETDATE(), GETDATE()),
('E-092433131024', 4, 20000000, 192, 0, 1, '2024-11-07', DATEADD(MONTH, 6, CAST('2024-11-07' AS DATE)), GETDATE(), GETDATE()),
('E-540826241024', 4, 20000000, 192, 0, 1, '2024-11-07', DATEADD(MONTH, 6, CAST('2024-11-07' AS DATE)), GETDATE(), GETDATE());

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
(3, 4, GETDATE()),
(4, 3, GETDATE()),
(5, 5, GETDATE()),
(6, 6, GETDATE()),
(7, 7, GETDATE()),
(8, 8, GETDATE());

INSERT INTO Schedules (EnrollmentID, TeacherID, VehicleID, SessionID, SessionDate, StatusID, Created_At, Updated_At)
VALUES
(1, 1, 1, 1, '15/10/2024', 1, GETDATE(), GETDATE()),
(2, 2, 2, 2, '20/10/2024', 1, GETDATE(), GETDATE()),
(3, 2, 6, 3, '25/10/2024', 1, GETDATE(), GETDATE());

INSERT INTO Invoices (InvoiceCode, EnrollmentID, TotalAmount, IsPaid, StatusID, Created_At, Updated_At)
VALUES 
('INV-092444131024', 1, 11000000, 0, 1, GETDATE(), GETDATE()),
('INV-093023131024', 2, 11000000, 1, 1, GETDATE(), GETDATE());

INSERT INTO Payments (InvoiceID, PaymentDate, Amount, PaymentMethod, Created_At, Updated_At)
VALUES 
(1, '12/10/2024', 7000000, 'Credit Card', GETDATE(), GETDATE()),
(2, '13/10/2024', 5000000, 'Cash', GETDATE(), GETDATE()),
(1, '02/10/2024', 4000000, 'Credit Card', GETDATE(), GETDATE());

INSERT INTO Accounts (Email, [Password], Permission, Created_At, Updated_At)
VALUES 
('mainguyenhoangvu2212@gmail.com', 'admin1', 1, GETDATE(), GETDATE()),
('teacher1@gmail.com', 'teacher1', 0, GETDATE(), GETDATE());