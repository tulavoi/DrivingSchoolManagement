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
	LicenseID int,
	LicenseNumber nvarchar(12) unique,
	BeginningDate DATETIME,
	FullName NVARCHAR(100),
    DateOfBirth DATETIME,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(11),
    Email NVARCHAR(100),
    [Address] NVARCHAR(255),
    CitizenID NVARCHAR(12) unique,
	Nationality nvarchar(100),
    StatusID int default 1,
	IsPass bit,
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
alter table Learners
add constraint FK_Learner_License foreign key (LicenseID) references Licenses(LicenseID)
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
VALUES('B', 1), ('C', 2), ('D', 3), ('E', 4), ('None', 5);

INSERT INTO [Status] (StatusName)
VALUES ('Active'), ('Inactive');

INSERT INTO Learners (FullName, LicenseID, LicenseNumber, BeginningDate, DateOfBirth, Gender, PhoneNumber, Email, [Address], CitizenID, Nationality, IsPass, Created_At, Updated_At)
VALUES 
('Mai Nguyen Hoang Vu', 5, '123456789012', '22/07/2015', '22/07/1996', 'Male', '0354377798', 'mainguyenhoangvu.tdc4304@gmail.com', '123 ABC Street', '012345678123', 'Viet Nam', 0, '22/11/2024', GETDATE()),
('Le Nguyen Xuan Duoc', 5, '234567890123', '15/05/2016', '15/05/1995', 'Male', '0912345678', 'lenguyenxuanduoc@gmail.com', '123 ABC Street', '012345234123', 'Viet Nam', 0, '24/11/2024', GETDATE()),
('Truong Anh Thanh Cong', 5, '345678901234', '20/07/2017', '20/07/1998', 'Female', '0987654321', 'xcongit@gmail.com', '456 XYZ Street', '987654321432', 'Viet Nam', 0, '02/10/2024', GETDATE()),
('Nguyen Van A', 5, '456789012345', '01/01/2018', '01/01/1990', 'Male', '0912345679', 'nguyenvana@gmail.com', '456 DEF Street', '012345678124', 'Viet Nam', 0, '03/04/2024', GETDATE()),
('Tran Thi B', 5, '567890123456', '02/02/2019', '02/02/1992', 'Female', '0912345680', 'tranthib@gmail.com', '789 GHI Street', '012345678125', 'Viet Nam', 0, '03/04/2024', GETDATE()),
('Le Van C', 5, '678901234567', '03/03/2020', '03/03/1988', 'Male', '0912345681', 'levanc@gmail.com', '321 JKL Street', '012345678126', 'Viet Nam', 0, '03/04/2024', GETDATE()),
('Le Van H', 5, '789012345678', '08/08/2021', '08/08/1996', 'Male', '0912345686', 'levanh@gmail.com', '357 YZ Street', '012345678131', 'Viet Nam', 0, '03/04/2024', GETDATE()),
('Pham Thi I', 1, '890123456789', '09/09/2022', '09/09/1997', 'Female', '0912345687', 'phamthi@gmail.com', '468 ABCD Street', '012345678132', 'Viet Nam', 0, '20/11/2024', GETDATE()),
('Nguyen Thanh Binh', 1, '901234567890', '15/04/2023', '15/04/1993', 'Male', '0912345682', 'nguyenthanhbinh@gmail.com', '654 QWE Street', '012345678127', 'Viet Nam', 0, '02/11/2024', GETDATE()),
('Doan Thi Lan', 5, '012345678901', '25/12/2023', '25/12/1995', 'Female', '0912345683', 'doanlan@gmail.com', '852 RTY Street', '012345678128', 'Viet Nam', 0, '03/05/2024', GETDATE()),
('Pham Van D', 5, '123456780001', '18/10/2015', '18/10/1989', 'Male', '0912345684', 'phamvand@gmail.com', '963 UIO Street', '012345678129', 'Viet Nam', 0, '05/03/2024', GETDATE()),
('Hoang Thi Hanh', 5, '123456780002', '07/03/2016', '07/03/1997', 'Female', '0912345685', 'hoanghanh@gmail.com', '147 ASD Street', '012345678130', 'Viet Nam', 0, '05/03/2024', GETDATE()),
('Bui Minh Chau', 5, '123456780003', '23/05/2017', '23/05/1994', 'Male', '0912345688', 'buiminhchau@gmail.com', '951 FGH Street', '012345678133', 'Viet Nam', 0, '05/03/2024', GETDATE()),
('Ngo Van Lam', 5, '123456780004', '13/07/2018', '13/07/1987', 'Male', '0912345689', 'ngovanlam@gmail.com', '753 VBN Street', '012345678134', 'Viet Nam', 0, '05/03/2024', GETDATE()),
('Trinh Thi Dao', 2, '123456780005', '21/11/2019', '21/11/1996', 'Female', '0912345690', 'trinhdao@gmail.com', '258 MNO Street', '012345678135', 'Viet Nam', 0, '15/11/2024', GETDATE()),
('Vu Van Tan', 1, '123456780006', '30/06/2020', '30/06/1998', 'Male', '0912345691', 'vuvantan@gmail.com', '369 PQR Street', '012345678136', 'Viet Nam', 0, '16/11/2024', GETDATE()),
('Ngo Thi Hue', 1, '123456780007', '19/01/2021', '19/01/1995', 'Female', '0912345692', 'ngothihue@gmail.com', '159 XYZ Street', '012345678137', 'Viet Nam', 0, '06/05/2024', GETDATE()),
('Tran Van Huy', 2, '123456780008', '19/10/2022', '19/10/1996', 'Male', '0912432692', 'tranvanhuy@gmail.com', '321 ASD Street', '012345219347', 'Viet Nam', 0, '16/11/2024', GETDATE()),
('Mai Nam Hai', 2, '123456780009', '21/10/2023', '21/10/1993', 'Male', '0903235692', 'mainamhai@gmail.com', '534 VCX Street', '012335341237', 'Viet Nam', 0, '30/04/2024', GETDATE()),
('Pham Van Khoa', 3, '123456780010', '10/02/2024', '10/02/1992', 'Male', '0912345693', 'phamkhoa@gmail.com', '753 KLM Street', '012345678138', 'Viet Nam', 0, '22/11/2024', GETDATE());

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
('61A-56478', 'Toyota Camry', 0, 1, 0, 2018, null, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-23467', 'Toyota Camry', 0, 1, 0, 2018, null, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-89625', 'Toyota Camry', 0, 1, 0, 2018, null, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-75832', 'Toyota Camry', 0, 1, 0, 2018, null, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-84784', 'Toyota Camry', 0, 1, 1, 2018, null, 5, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61A-65456', 'Hyundai Sonata', 0, 1, 0, 2018, NULL, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-83551', 'Hyundai Sonata', 0, 1, 0, 2018, NULL, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61A-56853', 'Hyundai Sonata', 0, 1, 1, 2018, NULL, 5, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61A-65846', 'Hyundai Sonata', 0, 1, 1, 2018, NULL, 5, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61A-35672', 'Hyundai Sonata', 0, 1, 0, 2018, NULL, 5, '', 1, NULL, NULL, GETDATE(), GETDATE()),
-- Xe dành cho bằng C (xe tải)
('61C-23523', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-56434', 'Isuzu Truck', 1, 0, 0, 2016, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-96793', 'Isuzu Truck', 1, 0, 0, 2019, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-02354', 'Isuzu Truck', 1, 0, 0, 2020, 5000, null, '', 1, NULL, NULL, GETDATE(), GETDATE()), 
('61C-87345', 'Isuzu Truck', 1, 0, 1, 2021, 5000, null, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61C-80346', 'Isuzu Truck', 1, 0, 1, 2020, 5000, null, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 120, GETDATE()), DATEADD(DAY, -RAND() * 60, GETDATE()), GETDATE(), GETDATE()),
('61C-65474', 'Hino Truck', 1, 0, 0, 2020, 5200, NULL, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-16342', 'Hino Truck', 1, 0, 0, 2020, 5200, NULL, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-45987', 'Hino Truck', 1, 0, 1, 2020, 5200, NULL, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61C-03943', 'Hino Truck', 1, 0, 0, 2020, 5200, NULL, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61C-80923', 'Hino Truck', 1, 0, 0, 2020, 5200, NULL, '', 1, NULL, NULL, GETDATE(), GETDATE()),
-- Xe dành cho bằng D (xe khách từ 10 đến 30 chỗ)
('61D-98765', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 100, GETDATE()), DATEADD(DAY, -RAND() * 50, GETDATE()), GETDATE(), GETDATE()),
('61D-42345', 'Mercedes-Benz Bus', 0, 1, 1, 2019, null, 20, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 120, GETDATE()), DATEADD(DAY, -RAND() * 60, GETDATE()), GETDATE(), GETDATE()),
('61D-64565', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 130, GETDATE()), DATEADD(DAY, -RAND() * 70, GETDATE()), GETDATE(), GETDATE()),
('61D-53463', 'Mercedes-Benz Bus', 0, 1, 0, 2018, null, 20, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61D-32435', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('61D-20942', 'Mercedes-Benz Bus', 0, 1, 1, 2017, null, 20, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 130, GETDATE()), DATEADD(DAY, -RAND() * 70, GETDATE()), GETDATE(), GETDATE()),
('61D-14853', 'Mercedes-Benz Bus', 0, 1, 0, 2017, null, 20, '', 1, NULL, NULL, GETDATE(), GETDATE()),
-- Xe dành cho bằng E (xe khách trên 30 chỗ)
('70E-54321', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-65346', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-32455', 'Hyundai Universe', 0, 1, 1, 2016, null, 40, '', 1, DATEADD(DAY, -RAND() * 90, GETDATE()), DATEADD(DAY, -RAND() * 40, GETDATE()), GETDATE(), GETDATE()),
('70E-47478', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-90230', 'Hyundai Universe', 0, 1, 1, 2017, null, 40, 'Vehicle is currently under warranty', 1, DATEADD(DAY, -RAND() * 90, GETDATE()), DATEADD(DAY, -RAND() * 40, GETDATE()), GETDATE(), GETDATE()),
('70E-57203', 'Hyundai Universe', 0, 1, 0, 2017, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE()),
('70E-69632', 'Hyundai Universe', 0, 1, 0, 2016, null, 40, '', 1, NULL, NULL, GETDATE(), GETDATE());

INSERT INTO Courses (CourseName, LicenseID, Fee, DurationInHours, HoursStudied, StatusID, StartDate, EndDate, Created_At, Updated_At)
VALUES 
('B-112342231024', 1, 11000000, 588, 0, 1, '22/11/2024', DATEADD(MONTH, 6, CAST('2024-11-22' AS DATE)), '22/11/2024', GETDATE()),
('B-080756241024', 1, 11000000, 588, 0, 1, '24/11/2024', DATEADD(MONTH, 6, CAST('2024-11-24' AS DATE)), '24/11/2024', GETDATE()),
('B-080702341024', 1, 11000000, 588, 0, 1, '02/10/2024', DATEADD(MONTH, 6, CAST('2024-10-02' AS DATE)), '02/10/2024', GETDATE()),
('B-092312351024', 1, 11000000, 588, 588, 1, '03/04/2024', DATEADD(MONTH, 6, CAST('2024-04-03' AS DATE)), '03/04/2024', GETDATE()),
('B-030420030424', 1, 11000000, 588, 588, 1, '03/04/2024', DATEADD(MONTH, 6, CAST('2024-04-03' AS DATE)), '03/04/2024', GETDATE()),
('B-100425030424', 1, 11000000, 588, 588, 1, '03/04/2024', DATEADD(MONTH, 6, CAST('2024-04-03' AS DATE)), '03/04/2024', GETDATE()),
('B-100515030424', 1, 11000000, 588, 588, 1, '03/04/2024', DATEADD(MONTH, 6, CAST('2024-04-03' AS DATE)), '03/04/2024', GETDATE()),

('C-252034571024', 2, 5500000, 192, 0, 1, '20/11/2024', DATEADD(MONTH, 6, CAST('2024-11-20' AS DATE)), '20/11/2024', GETDATE()),
('C-020654131024', 2, 5500000, 192, 918, 1, '02/11/2024', DATEADD(MONTH, 6, CAST('2024-11-02' AS DATE)), '02/11/2024', GETDATE()),
('C-053354131024', 2, 12000000, 920, 920, 1, '03/05/2024', DATEADD(MONTH, 6, CAST('2024-05-03' AS DATE)), '03/05/2024', GETDATE()),
('C-540856241024', 2, 12000000, 920, 920, 1, '05/03/2024', DATEADD(MONTH, 6, CAST('2024-03-05' AS DATE)), '05/03/2024', GETDATE()),
('C-029230531024', 2, 12000000, 920, 920, 1, '05/03/2024', DATEADD(MONTH, 6, CAST('2024-03-05' AS DATE)), '05/03/2024', GETDATE()),
('C-237502331024', 2, 12000000, 920, 920, 1, '05/03/2024', DATEADD(MONTH, 6, CAST('2024-03-05' AS DATE)), '05/03/2024', GETDATE()),
('C-042043261024', 2, 12000000, 920, 920, 1, '05/03/2024', DATEADD(MONTH, 6, CAST('2024-03-05' AS DATE)), '05/03/2024', GETDATE()),

('D-091122131024', 3, 5500000, 192, 0, 1, '15/11/2024', DATEADD(MONTH, 6, CAST('2024-11-15' AS DATE)), '15/11/2024', GETDATE()),
('D-450456241024', 3, 7000000, 336, 0, 1, '16/11/2024', DATEADD(MONTH, 6, CAST('2024-11-16' AS DATE)), '16/11/2024', GETDATE()),
('D-852304231024', 3, 7000000, 336, 336, 1, '06/05/2024', DATEADD(MONTH, 6, CAST('2024-05-06' AS DATE)), '06/05/2024', GETDATE()),
('D-023178521024', 3, 5500000, 192, 0, 1, '16/11/2024', DATEADD(MONTH, 6, CAST('2024-11-16' AS DATE)), '16/11/2024', GETDATE()),

('E-092433131024', 4, 7500000, 336, 336, 1, '30/04/2024', DATEADD(MONTH, 6, CAST('2024-04-30' AS DATE)), '30/04/2024', GETDATE()),
('E-092412351024', 4, 7000000, 192, 0, 1, '22/11/2024', DATEADD(MONTH, 6, CAST('2024-11-22' AS DATE)), '22/11/2024', GETDATE()),

('B-058295241024', 1, 0, 0, 0, 1,  GETDATE(), DATEADD(MONTH, 6, CAST(GETDATE() AS DATE)),  GETDATE(), GETDATE()),
('C-038265201124', 2, 0, 0, 0, 1,  GETDATE(), DATEADD(MONTH, 6, CAST(GETDATE() AS DATE)),  GETDATE(), GETDATE()),
('D-054834571124', 3, 0, 0, 0, 1,  GETDATE(), DATEADD(MONTH, 6, CAST(GETDATE() AS DATE)),  GETDATE(), GETDATE()),
('E-034245371124', 4, 0, 0, 0, 1,  GETDATE(), DATEADD(MONTH, 6, CAST(GETDATE() AS DATE)),  GETDATE(), GETDATE());

INSERT INTO [Sessions] ([Session], Created_At, Updated_At)
VALUES 
('7H30-9H30', GETDATE(), GETDATE()),
('9H30-11H30', GETDATE(), GETDATE()),
('13H00-15H00', GETDATE(), GETDATE()),
('15H00-17H00', GETDATE(), GETDATE());

insert into Enrollments(LearnerID, CourseID, EnrollmentDate)
values
(1, 1, '22/11/2024'),
(2, 2, '24/11/2024'),
(3, 3, '02/10/2024'),
(4, 4, '03/04/2024'),
(5, 5, '03/04/2024'),
(6, 6, '03/04/2024'),
(7, 7, '03/04/2024'),
(8, 8, '20/11/2024'),

(9, 9, '02/11/2024'),
(10, 10, '03/05/2024'),
(11, 11, '05/03/2024'),
(12, 12, '05/03/2024'),
(13, 13, '05/03/2024'),
(14, 14, '05/03/2024'),
(15, 15, '15/11/2024'),

(16, 16, '16/11/2024'),
(17, 17, '06/05/2024'),
(18, 18, '16/11/2024'),
(19, 19, '30/04/2024'),
(20, 20, '22/11/2024');

INSERT INTO Schedules (EnrollmentID, TeacherID, VehicleID, SessionID, SessionDate, StatusID, Created_At, Updated_At)
VALUES
(1, 1, 1, 1, '15/11/2024', 1, GETDATE(), GETDATE()),
(2, 2, 2, 2, '20/11/2024', 1, GETDATE(), GETDATE()),
(3, 2, 6, 3, '25/11/2024', 1, GETDATE(), GETDATE()),
(11, 2, 2, 2, '21/11/2024', 1, '26/11/2024', NULL),
(12, 2, 2, 1, '21/11/2024', 1, '26/11/2024', NULL),
(13, 2, 11, 3,'22/11/2024', 1, '26/11/2024', NULL),
(14, 2, 13, 2, '23/11/2024', 1, '26/11/2024', NULL),
(15, 3, 2, 3, '26/11/2024', 1, '26/11/2024', NULL),
(16, 2, 2, 2, '26/11/2024', 1, '26/11/2024', NULL),
(17, 4, 3, 2, '26/11/2024', 1, '26/11/2024', NULL);

INSERT INTO Invoices (InvoiceCode, EnrollmentID, TotalAmount, IsPaid, StatusID, Created_At, Updated_At)
VALUES 
('INV-092444131024', 1, 11000000, 1, 1, GETDATE(), GETDATE()),
('INV-093023131024', 2, 11000000, 0, 1, GETDATE(), GETDATE()),
('INV-891232321124', 3, 11000000, 0, 1, GETDATE(), GETDATE()),
('INV-012823231124', 4, 11000000, 0, 1, GETDATE(), GETDATE()),
('INV-032185231124', 5, 11000000, 1, 1, GETDATE(), GETDATE()),
('INV-093458341124', 6, 11000000, 1, 1, GETDATE(), GETDATE()),
('INV-018534031124', 7, 11000000, 1, 1, GETDATE(), GETDATE()),
('INV-080544271124', 12, 12000000, 1, 1, GETDATE(), GETDATE()),
('INV-080548271124', 13, 12000000, 1, 1, GETDATE(), GETDATE()),
('INV-080553271124', 9, 12000000, 1, 1, GETDATE(), GETDATE()),
('INV-080558271124', 10, 12000000, 0, 1, GETDATE(), GETDATE()),
('INV-080606271124', 17, 15000000, 1, 1, GETDATE(), GETDATE()),
('INV-080619271124', 11, 12000000, 1, 1, GETDATE(), GETDATE());

INSERT INTO Payments (InvoiceID, PaymentDate, Amount, PaymentMethod, Created_At, Updated_At)
VALUES 
(1, '12/10/2024', 7000000, 'Credit Card', GETDATE(), GETDATE()),
(2, '13/10/2024', 5000000, 'Cash', GETDATE(), GETDATE()),
(1, '02/10/2024', 4000000, 'Credit Card', GETDATE(), GETDATE()),
(13, '20/11/2024 08:09:26', 12000000, 'Banking', GETDATE(), NULL),
(12, '23/11/2024 08:10:09', 15000000, 'Cash', GETDATE(), NULL),
(11, '25/11/2024 08:10:24', 6000000, 'Banking', GETDATE(), NULL),
(9, '26/11/2024 08:20:45', 7000000, 'Banking', GETDATE(), NULL),
(7, '26/11/2024 10:30:45', 11000000, 'Cash', GETDATE(), NULL),
(6, '26/11/2024 15:20:43', 11000000, 'Banking', GETDATE(), NULL),
(5, '26/11/2024 20:30:43', 11000000, 'Banking', GETDATE(), NULL),
(9, '27/11/2024 08:10:45', 5000000, 'Banking', GETDATE(), NULL);

INSERT INTO Accounts (Email, [Password], Permission, Created_At, Updated_At)
VALUES 
('mainguyenhoangvu2212@gmail.com', 'admin1', 1, GETDATE(), GETDATE()),
('teacher1@gmail.com', 'teacher1', 0, GETDATE(), GETDATE());