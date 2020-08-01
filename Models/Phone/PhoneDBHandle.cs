using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdressBook.Models
{
    public class PhoneDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW PHONE *********************
        public bool AddPhone(Phone phone)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddPhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonId", phone.PersonId);
            cmd.Parameters.AddWithValue("@PhoneNumber", phone.PhoneNumber);
            cmd.Parameters.AddWithValue("@PhoneStatus", phone.PhoneStatus);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** GET ALL ********************
        public List<Phone> GetPhones()
        {
            connection();
            List<Phone> personList = new List<Phone>();

            SqlCommand cmd = new SqlCommand("GetPhoneDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                personList.Add(
                    new Phone
                    {
                        PhoneId = Convert.ToInt32(dr["PhoneId"]),
                        PersonId = Convert.ToInt32(dr["PersonId"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        PhoneStatus = Convert.ToString(dr["PhoneStatus"])
                    });
            }
            return personList;
        }

        // ***************** UPDATE PHONE DETAILS *********************
        public bool UpdatePhoneDetails(Phone person)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdatePhoneDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PhoneId", person.PhoneId);
            cmd.Parameters.AddWithValue("@PersonId", person.PersonId);
            cmd.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
            cmd.Parameters.AddWithValue("@PhoneStatus", person.PhoneStatus);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE PHONE DETAILS *******************
        public bool DeletePhone(int PhoneId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeletePhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PhoneId", PhoneId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}