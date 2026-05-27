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

        // 4. FilteredDemo Method
        public List<Medicine> FilteredDemo(decimal minPrice)
        {
            PharmacyManagementContext db = new PharmacyManagementContext();
            return db.Medicines.Where(m => m.MedicinePrice > minPrice).ToList();
        }

        // 5. FindAllUsingJoins Method
        public void FindAllUsingJoins()
        {
            PharmacyManagementContext db = new PharmacyManagementContext();

            // Simple LINQ Join query between Medicine and Unit
            var query = from m in db.Medicines
                        join u in db.Units on m.UnitIdFk equals u.UnitIdPk
                        select new { m.MedicineName, u.UnitName };

            foreach (var item in query)
            {
                Console.WriteLine(item.MedicineName + " - " + item.UnitName);
            }
        }

        // 6. LazyLoadingDemo (using Explicit Loading to load related Medicines for a Unit)
        public void LazyLoadingDemo()
        {
            PharmacyManagementContext db = new PharmacyManagementContext();

            // Load a Unit first
            Unit unit = db.Units.FirstOrDefault();
            if (unit != null)
            {
                Console.WriteLine("Unit Loaded: " + unit.UnitName);

                // Explicitly load the related Medicines on-demand (Lazy Loading equivalent)
                db.Entry(unit).Collection(u => u.Medicines).Load();

                foreach (var med in unit.Medicines)
                {
                    Console.WriteLine(" - " + med.MedicineName);
                }
            }
        }
    }
}
