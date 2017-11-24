using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.DAO
{
    public class TaxDAO : DAO
    {
        public List<Tax> GetAllTax()
        {
            Connect();
            List<Tax> TaxList = new List<Tax>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT Tax.name, Category.title, Tax.property, Tax.price FROM Tax JOIN Category ON Tax.category = Category.id", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tax tax = new Tax();
                    tax.name = Convert.ToString(reader["name"]);
                    tax.title = Convert.ToString(reader["title"]);
                    tax.property = Convert.ToString(reader["property"]);
                    tax.price = Convert.ToDecimal(reader["price"]);
                    TaxList.Add(tax);
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
            return TaxList;
        }

        public bool Add(Tax tax)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                "INSERT INTO Tax(name, category, property, price)" +
                "VALUES (@name, @category, @property, @price)", Connection);
                cmd.Parameters.Add(new SqlParameter("@name", tax.name));
                cmd.Parameters.Add(new SqlParameter("@category", tax.category));
                cmd.Parameters.Add(new SqlParameter("@property", tax.property));
                cmd.Parameters.Add(new SqlParameter("@price", tax.price));

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

        public void Edit(Tax tax)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("UPDATE Tax SET name = @name, category = @category, property = @property, price = @price" +
                    "WHERE id = @id", Connection);
                cmd.Parameters.Add(new SqlParameter("@name", tax.name));
                cmd.Parameters.Add(new SqlParameter("@category", tax.category));
                cmd.Parameters.Add(new SqlParameter("@property", tax.property));
                cmd.Parameters.Add(new SqlParameter("@price", tax.price));

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
                string str = "DELETE FROM Tax WHERE id =" + id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public Tax FindId(int id)
        {
            List<Tax> tax = GetAllTax();
            foreach (var t in tax)
            {
                if (t.id == id)
                {
                    return t;
                }
            }
            return new Tax("Error");
        }
    }
}
