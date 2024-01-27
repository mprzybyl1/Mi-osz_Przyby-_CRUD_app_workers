
CREATE DATABASE company
GO

use company
GO

CREATE TABLE Workers (
	id int PRIMARY KEY NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	city varchar(50) NOT NULL,
	department varchar(60) NOT NULL
)
GO



INSERT INTO Workers VALUES 
	(1, 'Jan', 'Kowalski', 'Lodz', 'IT'),
	(2, 'Pawel', 'Nowak', 'Poznan', 'Dzial obslugi klienta'),
	(3, 'Adam', 'Krawczyk', 'Poznan', 'Dzial wsparcia technicznego'),
	(4, 'Janusz', 'Nosowski', 'Poznan', 'Dzial Operacyjny'),
	(5, 'Tomasz', 'Kot', 'Warszawa', 'Dzial oprogramowania');

GO
