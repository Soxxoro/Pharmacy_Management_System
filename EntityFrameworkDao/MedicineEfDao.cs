using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkDao.Models;

namespace EntityFrameworkDao
{
    public class MedicineEfDao
    {
        // 1. Insert Method
        public bool Insert(Medicine med)
        {
            PharmacyManagementContext db = new PharmacyManagementContext();
            db.Medicines.Add(med);
            return db.SaveChanges() > 0;
        }

        // 2. FindById Method
        public Medicine FindById(int id)
        {
            PharmacyManagementContext db = new PharmacyManagementContext();
            return db.Medicines.Find(id);
        }

        // 3. FindAll Method
        public List<Medicine> FindAll()
        {
            PharmacyManagementContext db = new PharmacyManagementContext();
            return db.Medicines.ToList();
        }

        // 4. FindAllUsingJoins Method
        public void FindAllUsingJoins()
        {
            PharmacyManagementContext db = new PharmacyManagementContext();

            // Simple LINQ Join query
            var query = from m in db.Medicines
                        join c in db.Categories on m.CategoryId equals c.CategoryId
                        select new { m.MedicineName, c.CategoryName };

            foreach (var item in query)
            {
                Console.WriteLine(item.MedicineName + " - " + item.CategoryName);
            }
        }

        // 5. LazyLoadingDemo (using Explicit Loading to avoid proxy packages)
        public void LazyLoadingDemo()
        {
            PharmacyManagementContext db = new PharmacyManagementContext();

            // Load a category first
            Category cat = db.Categories.FirstOrDefault();
            if (cat != null)
            {
                Console.WriteLine("Category Loaded: " + cat.CategoryName);

                // Explicitly load the related Medicines on-demand (Lazy Loading equivalent)
                db.Entry(cat).Collection(c => c.Medicines).Load();

                foreach (var med in cat.Medicines)
                {
                    Console.WriteLine(" - " + med.MedicineName);
                }
            }
        }

        // 6. FilteredDemo Method
        public List<Medicine> FilteredDemo(decimal minPrice)
        {
            PharmacyManagementContext db = new PharmacyManagementContext();
            return db.Medicines.Where(m => m.MedicinePrice > minPrice).ToList();
        }
    }
}
