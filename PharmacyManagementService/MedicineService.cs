using System;
using System.Collections.Generic;
using PharmacyManagementVo;
using PharmacyManagement.Facade;
using Serilog;
using PharmacyException;

namespace PharmacyManagement.Service
{
    public class MedicineService : IMedicineService
    {
        private IMedicineFacade facade;

        public MedicineService(IMedicineFacade facade)
        {
            this.facade = facade;
        }

        public void StartMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Pharmacy Menu ---");
                Console.WriteLine("1. Add Medicine");
                Console.WriteLine("2. View All Medicines");
                Console.WriteLine("3. Find Medicine by ID");
                Console.WriteLine("4. Update Medicine");
                Console.WriteLine("5. Exit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1") AddMedicine();
                else if (choice == "2") GetAllMedicines();
                else if (choice == "3") FindById();
                else if (choice == "4") UpdateMedicine();
                else if (choice == "5") break;
                else Console.WriteLine("Invalid option.");
            }
        }

        public ResponseObject AddMedicine()
        {
            Log.Information("AddMedicine started");
            ResponseObject res = new ResponseObject();
            try
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Dosage: ");
                string dosage = Console.ReadLine();
                Console.Write("Enter Price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                MedicineVo med = new MedicineVo 
                { 
                    Medicine_Name = name, 
                    Medicine_Dosage = dosage, 
                    Medicine_Price = price 
                };

                res.Flag = facade.AddMedicine(med);
                res.Message = res.Flag ? "Saved successfully!" : "Failed to save!";
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                res.Flag = false;
                res.Message = ex.Message;
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("AddMedicine failed: " + ex.Message);
            }
            Log.Information("AddMedicine finished. Flag: " + res.Flag);
            return res;
        }

        public ResponseObject FindById()
        {
            Log.Information("FindById started");
            ResponseObject res = new ResponseObject();
            try
            {
                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                MedicineVo med = facade.FindById(id);
                if (med != null)
                {
                    res.Flag = true;
                    res.Message = "Found!";
                    res.Data = med;

                    Console.WriteLine("ID: " + med.Medicine_Id_PK);
                    Console.WriteLine("Name: " + med.Medicine_Name);
                    Console.WriteLine("Dosage: " + med.Medicine_Dosage);
                    Console.WriteLine("Price: " + med.Medicine_Price);
                }
                else
                {
                    res.Flag = false;
                    res.Message = "Not found!";
                    Console.WriteLine(res.Message);
                }
            }
            catch (Exception ex)
            {
                res.Flag = false;
                res.Message = ex.Message;
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("FindById failed: " + ex.Message);
            }
            Log.Information("FindById finished. Flag: " + res.Flag);
            return res;
        }

        public ResponseObject GetAllMedicines()
        {
            Log.Information("GetAllMedicines started");
            ResponseObject res = new ResponseObject();
            try
            {
                List<MedicineVo> list = facade.GetAllMedicines();
                res.Flag = true;
                res.Message = "Loaded!";
                res.Data = list;

                Console.WriteLine("ID\t|\tName\t|\tDosage\t|\tPrice");
                Console.WriteLine("---------------------------------------------");
                foreach (MedicineVo med in list)
                {
                    Console.WriteLine(med.Medicine_Id_PK + "\t|\t" + med.Medicine_Name + "\t|\t" + med.Medicine_Dosage + "\t|\t" + med.Medicine_Price);
                }
            }
            catch (Exception ex)
            {
                res.Flag = false;
                res.Message = ex.Message;
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("GetAllMedicines failed: " + ex.Message);
            }
            Log.Information("GetAllMedicines finished. Flag: " + res.Flag);
            return res;
        }

        public ResponseObject UpdateMedicine()
        {
            Log.Information("UpdateMedicine started");
            ResponseObject res = new ResponseObject();
            try
            {
                Console.Write("Enter ID to update: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter New Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter New Dosage: ");
                string dosage = Console.ReadLine();
                Console.Write("Enter New Price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                MedicineVo med = new MedicineVo 
                { 
                    Medicine_Id_PK = id, 
                    Medicine_Name = name, 
                    Medicine_Dosage = dosage, 
                    Medicine_Price = price 
                };

                res.Flag = facade.UpdateMedicine(med);
                res.Message = res.Flag ? "Updated successfully!" : "Failed to update!";
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                res.Flag = false;
                res.Message = ex.Message;
                Console.WriteLine("Error: " + ex.Message);
                Log.Error("UpdateMedicine failed: " + ex.Message);
            }
            Log.Information("UpdateMedicine finished. Flag: " + res.Flag);
            return res;
        }
    }
}
