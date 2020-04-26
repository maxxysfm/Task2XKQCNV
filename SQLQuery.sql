CREATE DATABASE Task2;
USE Task2;
CREATE TABLE Books (
    Title varchar(255),
    Author varchar(255),
    Published date
);

INSERT INTO Books (Title, Author, Published)
VALUES 
	('Attila and Life', 'Szabó Attila', '2018-11-20'),
	('Attila and Life Part 2', 'Szabó Attila', '2018-11-22'),
	('Unity Programming book', 'Some Programmer', '2020-5-22'),
	('Wow Classic vs Wow Retail', 'Brother Brother', '2018-11-22'),
	('How to avoid Valorant malware', 'Brother Brother', '2019-11-22'),
	('Brother and this Epic Brother', 'Brother Brother', '2020-11-22'),
	('Guide to Epic Twitch Streaming', 'Brother Brother', '2020-11-22'),
	('Future of Doom','John Romero', '2022-11-22');