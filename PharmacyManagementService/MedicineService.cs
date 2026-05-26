using System;
using System.Collections.Generic;
using PharmacyManagement.Vo;
using PharmacyManagement.Facade;
using Serilog;
using PharmacyException;

namespace PharmacyManagement.Service
{
    public class MedicineService : IMedicineService
    {
        private IMedicineFacade facade;

        public MedicineService(IMedicineFacade medicineFacade)
        {
            facade = medicineFacade;
        }

        public void StartMenu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n=== Medicine Management System (Console CUI) ===");
                Console.WriteLine("1. Add Medicine");
                Console.WriteLine("2. View All Medicines");
                Console.WriteLine("3. Find Medicine by ID");
                Console.WriteLine("4. Update Medicine");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice (1-5): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddMedicine();
                }
                else if (choice == "2")
                {
                    GetAllMedicines();
                }
                else if (choice == "3")
                {
                    FindById();
                }
                else if (choice == "4")
                {
                    UpdateMedicine();
                }
                else if (choice == "5")
                {
                    keepRunning = false;
                    Console.WriteLine("Exiting program...");
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        public void AddMedicine()
        {
            Console.WriteLine("\n--- Add Medicine ---");
            Console.Write("Enter Medicine Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Dosage (e.g., 500mg): ");
            string dosage = Console.ReadLine();

            Console.Write("Enter Price: ");
            string priceInput = Console.ReadLine();

            Log.Information("Service Input - AddMedicine: Name = {Name}, Dosage = {Dosage}, PriceStr = {PriceStr}", 
                name, dosage, priceInput);

            try
            {
                decimal price = 0;
                if (!decimal.TryParse(priceInput, out price))
                {
                    throw new MedicineException("Invalid price format. Price must be a decimal number.");
                }

                MedicineVo medicine = new MedicineVo();
                medicine.Medicine_Name = name;
                medicine.Medicine_Dosage = dosage;
                medicine.Medicine_Price = price;

                bool result = facade.AddMedicine(medicine);

                if (result)
                {
                    Console.WriteLine("Success: Medicine saved successfully.");
                    Log.Information("Service Output - AddMedicine: Success");
                }
                else
                {
                    Console.WriteLine("Error: Failed to save medicine.");
                    Log.Information("Service Output - AddMedicine: Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("Service Exception - AddMedicine failed: {ErrorMessage}", ex.Message);
            }
        }

        public void FindById()
        {
            Console.WriteLine("\n--- Find Medicine by ID ---");
            Console.Write("Enter Medicine ID: ");
            string idInput = Console.ReadLine();

            Log.Information("Service Input - FindById: IDStr = {IdStr}", idInput);

            try
            {
                int id = 0;
                if (!int.TryParse(idInput, out id))
                {
                    throw new MedicineException("Invalid ID format. ID must be an integer.");
                }

                MedicineVo med = facade.FindById(id);

                if (med != null)
                {
                    Console.WriteLine("\nMedicine Details Found:");
                    Console.WriteLine("ID: " + med.Medicine_Id_PK);
                    Console.WriteLine("Name: " + med.Medicine_Name);
                    Console.WriteLine("Dosage: " + med.Medicine_Dosage);
                    Console.WriteLine("Price: " + med.Medicine_Price);
                    Log.Information("Service Output - FindById: Found ID = {Id}", id);
                }
                else
                {
                    Console.WriteLine("Error: No medicine found with ID: " + id);
                    Log.Information("Service Output - FindById: Not Found ID = {Id}", id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("Service Exception - FindById failed: {ErrorMessage}", ex.Message);
            }
        }

        public void GetAllMedicines()
        {
            Console.WriteLine("\n--- View All Medicines ---");

            Log.Information("Service Input - GetAllMedicines: Request received");

            try
            {
                List<MedicineVo> list = facade.GetAllMedicines();

                if (list == null || list.Count == 0)
                {
                    Console.WriteLine("No medicines found in the system database.");
                    Log.Information("Service Output - GetAllMedicines: No records");
                    return;
                }

                Console.WriteLine("ID\t|\tName\t|\tDosage\t|\tPrice");
                Console.WriteLine("-------------------------------------------------------------");
                foreach (MedicineVo med in list)
                {
                    Console.WriteLine(med.Medicine_Id_PK + "\t|\t" + med.Medicine_Name + "\t|\t" + med.Medicine_Dosage + "\t|\t" + med.Medicine_Price);
                }

                Log.Information("Service Output - GetAllMedicines: Success, Count = {Count}", list.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("Service Exception - GetAllMedicines failed: {ErrorMessage}", ex.Message);
            }
        }

        public void UpdateMedicine()
        {
            Console.WriteLine("\n--- Update Medicine ---");
            Console.Write("Enter Medicine ID to Update: ");
            string idInput = Console.ReadLine();

            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter New Dosage (e.g., 500mg): ");
            string dosage = Console.ReadLine();

            Console.Write("Enter New Price: ");
            string priceInput = Console.ReadLine();

            Log.Information("Service Input - UpdateMedicine: IDStr = {IdStr}, Name = {Name}, Dosage = {Dosage}, PriceStr = {PriceStr}", 
                idInput, name, dosage, priceInput);

            try
            {
                int id = 0;
                if (!int.TryParse(idInput, out id))
                {
                    throw new MedicineException("Invalid ID format. ID must be an integer.");
                }

                decimal price = 0;
                if (!decimal.TryParse(priceInput, out price))
                {
                    throw new MedicineException("Invalid price format. Price must be a decimal number.");
                }

                MedicineVo medicine = new MedicineVo();
                medicine.Medicine_Id_PK = id;
                medicine.Medicine_Name = name;
                medicine.Medicine_Dosage = dosage;
                medicine.Medicine_Price = price;

                bool result = facade.UpdateMedicine(medicine);

                if (result)
                {
                    Console.WriteLine("Success: Medicine details updated successfully.");
                    Log.Information("Service Output - UpdateMedicine: Success");
                }
                else
                {
                    Console.WriteLine("Error: Failed to update medicine details (ID may not exist).");
                    Log.Information("Service Output - UpdateMedicine: Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("Service Exception - UpdateMedicine failed: {ErrorMessage}", ex.Message);
            }
        }
    }
}
