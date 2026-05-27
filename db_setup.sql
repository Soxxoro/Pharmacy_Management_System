CREATE DATABASE Pharmacy_Management;
GO

USE Pharmacy_Management;
GO

-- Create Unit table first
CREATE TABLE Unit (
    Unit_Id_PK INT PRIMARY KEY IDENTITY(1,1),
    Unit_Name VARCHAR(50) NOT NULL
);
GO

-- Create Medicine table with Unit_Id_FK relationship
CREATE TABLE Medicine (
    Medicine_Id_PK INT PRIMARY KEY IDENTITY(1,1),
    Medicine_Name VARCHAR(100),
    Medicine_Dosage VARCHAR(50),
    Medicine_Price DECIMAL(10,2),
    Unit_Id_FK INT NULL,
    CONSTRAINT FK_Medicine_Unit FOREIGN KEY (Unit_Id_FK) REFERENCES Unit(Unit_Id_PK)
);
GO

-- Seed Unit table
INSERT INTO Unit (Unit_Name) VALUES ('Tablet');
INSERT INTO Unit (Unit_Name) VALUES ('Syrup');
GO

-- Seed Medicine table
INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price, Unit_Id_FK) 
VALUES ('Paracetamol', '500mg', 5.50, 1);
INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price, Unit_Id_FK) 
VALUES ('Ibuprofen', '400mg', 12.00, 1);
GO
