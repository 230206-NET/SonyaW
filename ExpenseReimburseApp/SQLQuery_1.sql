DROP TABLE Manager
CREATE TABLE Manager (
    ManagerEmail VARCHAR(64) PRIMARY KEY NOT NULL,
    ManagerName VARCHAR(64),
    ManagerPW VARCHAR(64),
)

DROP TABLE Employee
CREATE TABLE Employee(
    EmplEmail VARCHAR(64) PRIMARY KEY NOT NULL,
    EmplName VARCHAR(64),
    EmplPW VARCHAR(64) NOT NULL,
)

DROP TABLE Expense
CREATE TABLE Expense (
    EmplEmail VARCHAR(64) REFERENCES Employee(emplEmail),
    Amount INT NOT NULL,
    ExpenseDescription VARCHAR(256),
    TicketStatus VARCHAR(32) DEFAULT "Pending"
)

DROP TABLE ManagerEmployee
CREATE TABLE ManagerEmployee (
    ManagerEmail VARCHAR(50) FOREIGN KEY REFERENCES Manager(ManagerEmail),
    EmplEmail VARCHAR(50) FOREIGN KEY REFERENCES Employee(EmplEmail)
)