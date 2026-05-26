CREATE DATABASE Pharmacy_Management;
GO

USE Pharmacy_Management;
GO

CREATE TABLE Medicine (
    Medicine_Id_PK INT PRIMARY KEY IDENTITY(1,1),
    Medicine_Name VARCHAR(100),
    Medicine_Dosage VARCHAR(50),
    Medicine_Price DECIMAL(10,2)
);
GO

INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price) 
VALUES ('Paracetamol', '500mg', 5.50);
INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price) 
VALUES ('Ibuprofen', '400mg', 12.00);
GO
