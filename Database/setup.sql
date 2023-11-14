USE master;
DROP DATABASE IF EXISTS skiService;
CREATE DATABASE skiService;
USE skiService;



--------------------
CREATE TABLE registrations(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[name] VARCHAR(255) not null,
	[email] VARCHAR(255) not null,
	[tel] VARCHAR(255) not null,
	[priority] VARCHAR(255) not null,
	[service] VARCHAR(255) not null,
	[startDate] DATE,
	[finishDate] DATE not null,
	[status] VARCHAR(255),
	[note] VARCHAR(255)
);


--login
----------------------------------------------
CREATE TABLE userInfo(
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	[userName] VARCHAR(255) not null,
	[password] VARCHAR(255)
);

CREATE TABLE userSessions (
    ID INT IDENTITY(1,1) PRIMARY KEY,
	session_key VARCHAR(255),
	ip_address VARCHAR(45),
    userID INT,
    FOREIGN KEY (userID) REFERENCES userInfo(ID)
);


SELECT * FROM registrations;