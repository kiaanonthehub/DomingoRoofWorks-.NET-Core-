DROP DATABASE Domingo_Roof_Works;
USE HR;

--CREATE DATABSE
CREATE DATABASE Domingo_Roof_Works;

--USE THIS DATABASE
USE Domingo_Roof_Works;

-- CREATE TABLES
CREATE TABLE Customer
(
CustomerID INT NOT NULL IDENTITY(1,1),
Name VARCHAR(25) NOT NULL,
Surname VARCHAR(25) NOT NULL,
Address VARCHAR(60) NOT NULL,
City VARCHAR(25) NOT NULL,
PostalCode CHAR(5) NOT NULL
PRIMARY KEY(CustomerID),
);

CREATE TABLE Material
(
MaterialID INT NOT NULL IDENTITY(1,1),
Description VARCHAR(250) NOT NULL,
PRIMARY KEY (MaterialID) 
);

CREATE TABLE Employee
(
EmployeeID VARCHAR(6) NOT NULL,
Name VARCHAR(25) NOT NULL,
Surname VARCHAR(25) NOT NULL,
PRIMARY KEY (EmployeeID)
);

CREATE TABLE JobType
(
JobTypeID INT NOT NULL IDENTITY(1,1),
JobType VARCHAR(20) NOT NULL,
Rate DECIMAL NOT NULL,
PRIMARY KEY (JobTypeID)
);

DROP TABLE Job
DROP TABLE Job_Material
DROP TABLE Job_Employee

CREATE TABLE Job
(
JobCardID INT NOT NULL,
CustomerID INT NOT NULL,
JobTypeID INT NOT NULL,
Days INT NOT NULL,
PRIMARY KEY (JobCardID),
FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID),
FOREIGN KEY(JobTypeID) REFERENCES JobType(JobTypeID)
);

CREATE TABLE Job_Material
(
JobMaterialID INT NOT NULL IDENTITY(1,1), 
JobCardID INT NOT NULL,
MaterialID INT NOT NULL,
Quantity INT NOT NULL,
PRIMARY KEY(JobMaterialID),
FOREIGN KEY(JobCardID) REFERENCES Job(JobCardID),
FOREIGN KEY(MaterialID) REFERENCES Material(MaterialID),
);

CREATE TABLE Job_Employee
(
JobEmployeeID INT NOT NULL  IDENTITY(1,1),
JobCardID INT NOT NULL,
EmployeeID VARCHAR(6) NOT NULL ,
PRIMARY KEY (JobEmployeeID),
FOREIGN KEY(JobCardID) REFERENCES Job(JobCardID),
FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID)
);


--INSERT INTO TABLES
INSERT INTO Customer(Name, Surname, Address, City, PostalCode) VALUES 
('Tendai', 'Ndoro', '3 Leos Place 457 Church Str', 'PRETORIA', '0002'),
('Donald', 'Puttingh', '408 Oubos 368 Prinsloo Street', 'PRETORIA', '0001'),
('Tracy', 'Samson', '206 Albertros 269 Stead Avenue', 'PRETORIA', '0186'),
('Jacob', 'Smith', 'A201 Ocerton 269 Debouvlrde Str', 'PRETORIA', '0002'),
('Thato', 'Molepo', '11 Luttig Court 289 MALTZAN Str ', 'PRETORIA', '0001'),
('Dakalo', 'Mudau', '1182 CEBINIA Str', 'PRETORIA', '0082'),
('Sfiso', 'Myeni', '503 Hamilton Gardens 337 Visagie Str ', 'PRETORIA', '0001'),
('Ricardo', 'Keyl', '10 Silville 614 Jasmyn Str', 'PRETORIA', '0184'),
('Smallboy', 'Mtshali', '307 FEORA East', 'PRETORIA-WEST', '0183'),
('Wilson', 'Jansen', '701 Monticchico Flat 251 Jacob Mare Str', 'PRETORIA', '0002');


INSERT INTO Employee(EmployeeID,Name,Surname) VALUES
('EMP100','Albert','Malose'),
('EMP920','Chris','Byne'),
('EMP010', 'John','Hendriks'),
('EMP771', 'Smallboy','Modipa'),
('EMP681', 'Stanley','Jacobs');

INSERT INTO Employee(EmployeeID,Name,Surname) VALUES
('N/A','N/A','N/A');


INSERT INTO JobType(JobType,Rate) VALUES
('Full Conversion', 1200.00),
('Semi Conversion', 1080.00),
('Floor Boarding', 900.00); 
-- SET RATE TO 2 DP

SELECT * FROM JobType
SELECT * FROM Customer
DELETE FROM Customer;
DELETE FROM Customer WHERE CustomerID=2
DBCC CHECKIDENT ('Customer', RESEED, 0)

INSERT INTO Material(Description) VALUES
('standard floorboards'),
('power points'),
('metres standard electrical wiring'),
('standard stairs pack');

INSERT INTO Job(JobCardID,CustomerID,JobTypeID,Days) VALUES
(11000,1,1,7),
(10478,2,2,2),
(14253,3,3,2),
(11258,4,1,8),
(12058,5,2,3),
(13697,6,1,7),
(10211,7,1,7),
(10471,8,2,2),
(13521,9,2,3),
(10102,10,3,2);

INSERT INTO Job_Employee(JobCardID,EmployeeID) VALUES
(11000,'EMP100'),
(11000,'EMP920'),
(11000,'EMP010'),
(10478,'EMP920'),
(14253,'EMP771'),
(11258,'EMP681'),
(11258,'EMP010'),
(11258,'EMP771'),
(12058,'EMP681');

INSERT INTO Job_Employee(JobCardID,EmployeeID) VALUES
(13697,'N/A'),
(10211,'N/A'),
(10471,'N/A'),
(13521,'N/A'),
(10102,'N/A');


INSERT INTO Job_Material(JobCardID,MaterialID,Quantity) VALUES

--11000,FC
(11000,1,90),
(11000,2,3),
(11000,3,20),
(11000,4,1),

--10478,SC
(10478,1,50),
(10478,2,1),
(10478,3,10),

--12053,FLC
(14253,1,40),

--11258,FC
(11258,1,80),
(11258,2,3),
(11258,3,20),
(11258,4,1),

--12058,SC
(12058,1,60),
(12058,2,2),
(12058,3,15),

--13697,FC
(13697,1,80),
(13697,2,4),
(13697,3,40),
(13697,4,1),

--10211,FC
(10211,1,100),
(10211,2,5),
(10211,3,30),
(10211,4,1),

--10471,SC
(10471,1,40),
(10471,2,1),
(10471,3,8),

--13521,SC
(13521,1,65),
(13521,2,3),
(13521,3,18),

--10121,FLC
(10102,1,70);

SELECT JobCardID,  
FROM JOB;

