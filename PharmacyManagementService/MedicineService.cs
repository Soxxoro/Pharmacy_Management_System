using System;
using System.Collections.Generic;
using PharmacyManagementVo;
using PharmacyManagementFacade;
using Serilog;

namespace PharmacyManagementService
{
    public class MedicineService : IMedicineService
    {
        private IMedicineFacade facade;

        public MedicineService(IMedicineFacade facade)
        {
            this.facade = facade;
        }

        private ResponseObject HandleResponse(bool success, string successMsg, string failMsg)
        {
            ResponseObject res = new ResponseObject();
            res.Flag = success;
            res.Message = success ? successMsg : failMsg;
            Console.WriteLine(res.Message);
            if (success)
            {
                Log.Information(res.Message);
            }
            return res;
        }

        private ResponseObject HandleException(Exception ex, string logMessage)
        {
            ResponseObject res = new ResponseObject();
            res.Flag = false;
            res.Message = ex.Message;
            Console.WriteLine("Error: " + ex.Message);
            Log.Error(logMessage + ": " + ex.Message);
            return res;
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
            try
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Dosage: ");
                string dosage = Console.ReadLine();
                Console.Write("Enter Price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                MedicineVo med = new MedicineVo();
                med.Medicine_Name = name;
                med.Medicine_Dosage = dosage;
                med.Medicine_Price = price;

                bool ok = facade.AddMedicine(med);
                return HandleResponse(ok, "Medicine Added", "Failed to add Medicine");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "AddMedicine failed");
            }
        }

        public ResponseObject FindById()
        {
            try
            {
                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                MedicineVo med = facade.FindById(id);
                if (med != null)
                {
                    Console.WriteLine("ID: " + med.Medicine_Id_PK);
                    Console.WriteLine("Name: " + med.Medicine_Name);
                    Console.WriteLine("Dosage: " + med.Medicine_Dosage);
                    Console.WriteLine("Price: " + med.Medicine_Price);

                    ResponseObject res = HandleResponse(true, "Medicine Found", "");
                    res.Data = med;
                    return res;
                }
                return HandleResponse(false, "", "Medicine Not Found");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "FindById failed");
            }
        }

        public ResponseObject GetAllMedicines()
        {
            try
            {
                List<MedicineVo> list = facade.GetAllMedicines();
                Console.WriteLine("ID\t|\tName\t|\tDosage\t|\tPrice");
                Console.WriteLine("---------------------------------------------");
                foreach (MedicineVo med in list)
                {
                    Console.WriteLine(med.Medicine_Id_PK + "\t|\t" + med.Medicine_Name + "\t|\t" + med.Medicine_Dosage + "\t|\t" + med.Medicine_Price);
                }

                ResponseObject res = HandleResponse(true, "Medicines Loaded", "");
                res.Data = list;
                return res;
            }
            catch (Exception ex)
            {
                return HandleException(ex, "GetAllMedicines failed");
            }
        }

        public ResponseObject UpdateMedicine()
        {
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

                MedicineVo med = new MedicineVo();
                med.Medicine_Id_PK = id;
                med.Medicine_Name = name;
                med.Medicine_Dosage = dosage;
                med.Medicine_Price = price;

                bool ok = facade.UpdateMedicine(med);
                return HandleResponse(ok, "Medicine Updated", "Failed to update Medicine");
            }
            catch (Exception ex)
            {
                return HandleException(ex, "UpdateMedicine failed");
            }
        }
    }
}
