using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.DAO
{
    public class ContractDAO : DAO
    {
        public List<Contract> GetAllContract()
        {
            Connect();
            List<Contract> ContractList = new List<Contract>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Contract", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contract contract = new Contract();
                    contract.id = Convert.ToInt32(reader["id"]);
                    contract.series = Convert.ToString(reader["series"]);
                    contract.number = Convert.ToInt32(reader["number"]);
                    contract.insurant_f = Convert.ToString(reader["insurant_f"]);
                    contract.insurant_i = Convert.ToString(reader["insurant_i"]);
                    contract.insurant_o = Convert.ToString(reader["insurant_o"]);
                    contract.tax = Convert.ToInt32(reader["tax"]);
                    contract.date = Convert.ToDateTime(reader["date"]);
                    contract.coef = Convert.ToInt32(reader["coef"]);
                    contract.cost = Convert.ToDecimal(reader["cost"]);
                    ContractList.Add(contract);
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                Disconnect();
            }
            return ContractList;
        }

        public bool Add(Contract contract)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                "INSERT INTO Contract(series, number, insurant_f, insurant_i, insurant_o, tax, date, coef, cost)" +
                "VALUES (@series, @number, @insurant_f, @insurant_i, @insurant_o, @tax, @date, @coef, @cost)", Connection);
                cmd.Parameters.Add(new SqlParameter("@series", contract.series));
                cmd.Parameters.Add(new SqlParameter("@number", contract.number));
                cmd.Parameters.Add(new SqlParameter("@insurant_f", contract.insurant_f));
                cmd.Parameters.Add(new SqlParameter("@insurant_i", contract.insurant_i));
                cmd.Parameters.Add(new SqlParameter("@insurant_o", contract.insurant_o));
                cmd.Parameters.Add(new SqlParameter("@tax", contract.tax));
                cmd.Parameters.Add(new SqlParameter("@date", contract.date));
                cmd.Parameters.Add(new SqlParameter("@coef", contract.coef));
                cmd.Parameters.Add(new SqlParameter("@cost", contract.cost));

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }

        public void Edit(Contract contract)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("UPDATE Contract SET series = @series," +
                "number = @number, insurant_f = @insurant_f, insurant_i = @insurant_i, insurant_o = @insurant_o," +
                "tax = @tax, date = @date, coef = @coef, cost = @cost WHERE id = @id", Connection);
                cmd.Parameters.Add(new SqlParameter("@id", contract.id));
                cmd.Parameters.Add(new SqlParameter("@series", contract.series));
                cmd.Parameters.Add(new SqlParameter("@number", contract.number));
                cmd.Parameters.Add(new SqlParameter("@insurant_f", contract.insurant_f));
                cmd.Parameters.Add(new SqlParameter("@insurant_i", contract.insurant_i));
                cmd.Parameters.Add(new SqlParameter("@insurant_o", contract.insurant_o));
                cmd.Parameters.Add(new SqlParameter("@tax", contract.tax));
                cmd.Parameters.Add(new SqlParameter("@date", contract.date));
                cmd.Parameters.Add(new SqlParameter("@coef", contract.coef));
                cmd.Parameters.Add(new SqlParameter("@cost", contract.cost));

                cmd.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public void Delete(int id)
        {
            try
            {
                Connect();
                string str = "DELETE FROM Contract WHERE id =" + id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public Contract FindId(int id)
        {
            List<Contract> contract = GetAllContract();
            foreach (var c in contract)
            {
                if (c.id == id)
                {
                    return c;
                }
            }
            return new Contract("Error");
        }
    }
}