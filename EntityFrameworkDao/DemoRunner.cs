using System;
using System.Collections.Generic;
using EntityFrameworkDao.Models;

namespace EntityFrameworkDao
{
    public class DemoRunner
    {
        public static void RunDemo()
        {
            MedicineEfDao dao = new MedicineEfDao();

            Console.WriteLine("--- EF Core DB First CRUD Demo ---");

            // 1. Insert
            Medicine newMed = new Medicine
            {
                MedicineName = "Amoxicillin-Demo",
                MedicineDosage = "250mg",
                MedicinePrice = 18.75m
            };
            bool insertOk = dao.Insert(newMed);
            Console.WriteLine("Insert Success: " + insertOk);

            // 2. FindById
            Medicine found = dao.FindById(newMed.MedicineIdPk);
            if (found != null)
            {
                Console.WriteLine("Found Medicine: " + found.MedicineName + " | Price: " + found.MedicinePrice);
            }

            // 3. FindAll
            List<Medicine> all = dao.FindAll();
            Console.WriteLine("Total Medicines: " + all.Count);

            // 4. FilteredDemo
            List<Medicine> filtered = dao.FilteredDemo(10.00m);
            Console.WriteLine("Medicines > 10.00: " + filtered.Count);

            // 5. FindAllUsingJoins
            // Console.WriteLine("\n--- EF Core Join Demo ---");
            // dao.FindAllUsingJoins();

            // 6. LazyLoadingDemo
            // Console.WriteLine("\n--- EF Core Lazy Loading Demo ---");
            // dao.LazyLoadingDemo();
        }
    }
}
