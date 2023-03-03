SELECT * FROM Managers;
SELECT * FROM Employees;
SELECT * FROM Expenses ORDER BY EmplEmail;

SELECT * FROM Expenses WHERE EmplEmail = (SELECT * FROM Employees WHERE MngEmail = 'manager2@gmail.com')

---------------------------- MANAGERS TABLE --------------------------------------------------------------------------->

-- DROP TABLE Managers;

CREATE TABLE Managers (
    Email VARCHAR(64) PRIMARY KEY,
    managerName VARCHAR(64),
    managerPW NVARCHAR(64)
);

INSERT INTO Managers (Email, managerName, managerPW) VALUES ('manager1@gmail.com', 'Barry Bear', 'barrybear');
INSERT INTO Managers (Email, managerName, managerPW) VALUES ('manager2@gmail.com', 'Sally Seal', 'sallyseal');
INSERT INTO Managers (Email, managerName, managerPW) VALUES ('manager3@gmail.com', 'Bonnie Bunny', 'bonniebunny');

SELECT * FROM Managers;
TRUNCATE TABLE Managers
---------------------------- EMPLOYEES TABLE ------------------------------------------------------------------>

-- DROP TABLE Employees;

CREATE TABLE Employees(
    EmplEmail VARCHAR(64) PRIMARY KEY NOT NULL,
    EmplName VARCHAR(64),
    EmplPW NVARCHAR(64) NOT NULL,
    MngEmail VARCHAR(64) REFERENCES Managers(Email)
);

INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl1@gmail.com', 'Wilson Wolf', 'emplPASSWORD1', 'manager1@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl2@gmail.com', 'Cody Cub', 'emplPASSWORD2', 'manager1@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl3@gmail.com', 'Dory Dolphin', 'emplPASSWORD3', 'manager2@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl4@gmail.com', 'Sarah Sardines', 'emplPASSWORD4', 'manager2@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl5@gmail.com', 'Sean Shark', 'emplPASSWORD5', 'manager2@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) VALUES ('empl6@gmail.com', 'Harry Hare', 'emplPASSWORD6', 'manager2@gmail.com');
INSERT INTO Employees (EmplEmail, EmplName, EmplPW, MngEmail) values ('empl7@gmail.com', 'Felix Ferret', 'emplPASSWORD7', 'manager2@gmail.com')

SELECT Email FROM Managers
UNION ALL
SELECT EmplEmail FROM Employees

DELETE from Employees where EmplEmail = 'manager2@gmail.com'

SELECT * FROM Employees;
-- TRUNCATE TABLE Employees;

---------------------------- EXPENSES TABLE --------------------------------------------------------------------------->

-- DROP TABLE Expenses

CREATE TABLE Expenses (
    Id INT PRIMARY KEY IDENTITY (1,1),
    EmplEmail VARCHAR(64) REFERENCES Employees (EmplEmail) on DELETE cascade,
    Amount INT NOT NULL,
    ExpenseDescription VARCHAR(256),
    TicketStatus INT DEFAULT 0
)

INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 50, 'food');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 110, 'hotel');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 30, 'milk');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 150, 'bed');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 30, 'water');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl7@gmail.com', 60, 'food');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl4@gmail.com', 100, 'school');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl4@gmail.com', 100, 'cans');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl5@gmail.com', 80, 'razors');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl5@gmail.com', 30, 'carrots');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl6@gmail.com', 70, 'food');
INSERT INTO Expenses (EmplEmail, Amount, ExpenseDescription) VALUES ('empl6@gmail.com', 40, 'blankets');

SELECT * FROM Expenses;
TRUNCATE TABLE Expenses

---------------------------- RETRIEVING RELATIONSHIP TABLES --------------------------------------------------------------------------->

CREATE TABLE ManagerEmployee (
    MngEmail VARCHAR(50) REFERENCES Managers (Email),
    EmplEmail VARCHAR(50) REFERENCES Employees (EmplEmail)
)

---------------------------- RETRIEVING RELATIONSHIP TABLES --------------------------------------------------------------------------->

-- JOINING EMPLOYEES AND EXPENSES
SELECT Employees.EmplEmail as EmplEmail, EmplName, Expenses.EmplEmail as ExpensesEmplEmail, Amount, ExpenseDescription, TicketStatus FROM Employees LEFT JOIN Expenses on Employees.EmplEmail = Expenses.EmplEmail;
SELECT Employees.EmplEmail as ExpensesEmplEmail, Amount, ExpenseDescription, TicketStatus FROM Employees LEFT JOIN Expenses on Employees.EmplEmail = Expenses.EmplEmail;
-- SELECT ROW_NUMBER() OVER(ORDER BY row_num) as row_num FROM (SELECT * FROM Employees JOIN Expenses on Employees.EmplEmail = Expenses.EmplEmail)

-- JOINING EMPLOYEES AND EXPENSES WITH SPECIFIC EMPLOYEE
SELECT Employees.EmplEmail as ExpensesEmplEmail, Amount, ExpenseDescription, TicketStatus, Id FROM Employees LEFT JOIN Expenses on Employees.EmplEmail = Expenses.EmplEmail WHERE Employees.EmplEmail = 'empl1@gmail.com      ';

-- JOIN EMPLOYEES AND MANAGERS
SELECT Employees.MngEmail as EmplMngEmail, EmplName, EmplEmail, Managers.Email as ManagerEmail, managerName FROM Employees JOIN Managers on Employees.MngEmail = Managers.Email;
SELECT * FROM Employees JOIN Managers on Employees.MngEmail = Managers.Email;

-- UPDATING A CELL
UPDATE Expenses SET ExpenseDescription = 'insurance', Amount = 100 WHERE Id = 5;
