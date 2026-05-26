using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PharmacyManagement.Vo;
using Serilog;

namespace PharmacyManagement.Dao
{
    public class MedicineDao : IMedicineDao
    {
        private string connString;

        public MedicineDao(string connectionString)
        {
            connString = connectionString;
        }

        public bool AddMedicine(MedicineVo med)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sql = "INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price) VALUES (@Name, @Dosage, @Price)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", med.Medicine_Name);
                        cmd.Parameters.AddWithValue("@Dosage", med.Medicine_Dosage);
                        cmd.Parameters.AddWithValue("@Price", med.Medicine_Price);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("DAO Error - AddMedicine failed: " + ex.Message);
                throw;
            }
        }

        public MedicineVo FindById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sql = "SELECT Medicine_Id_PK, Medicine_Name, Medicine_Dosage, Medicine_Price FROM Medicine WHERE Medicine_Id_PK = @Id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                MedicineVo med = new MedicineVo();
                                med.Medicine_Id_PK = Convert.ToInt32(rdr["Medicine_Id_PK"]);
                                med.Medicine_Name = Convert.ToString(rdr["Medicine_Name"]);
                                med.Medicine_Dosage = Convert.ToString(rdr["Medicine_Dosage"]);
                                med.Medicine_Price = Convert.ToDecimal(rdr["Medicine_Price"]);
                                return med;
                            }
                        }
                        conn.Close();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("DAO Error - FindById failed for ID " + id + ": " + ex.Message);
                throw;
            }
        }

        public List<MedicineVo> GetAllMedicines()
        {
            try
            {
                List<MedicineVo> list = new List<MedicineVo>();
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sql = "SELECT Medicine_Id_PK, Medicine_Name, Medicine_Dosage, Medicine_Price FROM Medicine";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                MedicineVo med = new MedicineVo();
                                med.Medicine_Id_PK = Convert.ToInt32(rdr["Medicine_Id_PK"]);
                                med.Medicine_Name = Convert.ToString(rdr["Medicine_Name"]);
                                med.Medicine_Dosage = Convert.ToString(rdr["Medicine_Dosage"]);
                                med.Medicine_Price = Convert.ToDecimal(rdr["Medicine_Price"]);
                                list.Add(med);
                            }
                        }
                        conn.Close();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Log.Error("DAO Error - GetAllMedicines failed: " + ex.Message);
                throw;
            }
        }

        public bool UpdateMedicine(MedicineVo med)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sql = "UPDATE Medicine SET Medicine_Name = @Name, Medicine_Dosage = @Dosage, Medicine_Price = @Price WHERE Medicine_Id_PK = @Id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", med.Medicine_Id_PK);
                        cmd.Parameters.AddWithValue("@Name", med.Medicine_Name);
                        cmd.Parameters.AddWithValue("@Dosage", med.Medicine_Dosage);
                        cmd.Parameters.AddWithValue("@Price", med.Medicine_Price);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("DAO Error - UpdateMedicine failed for ID " + med.Medicine_Id_PK + ": " + ex.Message);
                throw;
            }
        }
    }
}
