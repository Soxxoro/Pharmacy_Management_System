using Serilog;
using PharmacyManagement.Dao;
using PharmacyManagement.Bo;
using PharmacyManagement.Facade;
using PharmacyManagement.Service;

namespace PharmacyManagement.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();

            Log.Information("Application Started");

            string connectionString = "Server=.\\SQLEXPRESS;Database=Pharmacy_Management;Trusted_Connection=True;TrustServerCertificate=True;";
            
            IMedicineDao dao = new MedicineDao(connectionString);
            MedicineBo bo = new MedicineBo(dao);
            IMedicineFacade facade = new MedicineFacade(bo);
            IMedicineService service = new MedicineService(facade);

            service.StartMenu();

            Log.Information("Application Stopped");
            Log.CloseAndFlush();
        }
    }
}
