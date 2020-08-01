using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AdressBook.Models
{
    public class PhoneBookDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        public List<PhoneBook> GetPhoneBook()
        {
            connection();
            List<PhoneBook> personList = new List<PhoneBook>();

            SqlCommand cmd = new SqlCommand("GetAllInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                personList.Add(
                    new PhoneBook
                    {
                        PersonId = Convert.ToInt32(dr["PersonId"]),
                        PhoneId = Convert.ToInt32(dr["PhoneId"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        SurName = Convert.ToString(dr["SurName"]),
                        Address = Convert.ToString(dr["Address"]),
                        PersonAge = Convert.ToInt32(dr["PersonAge"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        PhoneStatus = Convert.ToString(dr["PhoneStatus"])
                    });
            }
            return personList;
        }
        // **************** ADD NEW RECORD *********************
        public bool AddPhoneBookRecord(PhoneBook phoneBook)
        {
            connection();
            //Add Person
            SqlCommand cmd = new SqlCommand("AddNewPerson", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", phoneBook.FirstName);
            cmd.Parameters.AddWithValue("@SurName", phoneBook.SurName);
            cmd.Parameters.AddWithValue("@Address", phoneBook.Address);
            cmd.Parameters.AddWithValue("@PersonAge", phoneBook.PersonAge);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            //Get Current Id of Person
            SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT('Person');", con);
            con.Open();
            decimal currentId = (decimal)sqlCommand.ExecuteScalar(); 
            con.Close();
            Debug.WriteLine(currentId);
            //Add Phone to Person
            SqlCommand command = new SqlCommand("AddPhone", con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PersonId", currentId);
            command.Parameters.AddWithValue("@PhoneNumber", phoneBook.PhoneNumber);
            command.Parameters.AddWithValue("@PhoneStatus", phoneBook.PhoneStatus);

            con.Open();
            int j = command.ExecuteNonQuery();
            con.Close();

            if (j >= 1)
                return true;
            else
                return false;
        }       

        // ********************** DELETE PHONE BOOK RECORD DETAILS *******************
        public bool DeletePhoneBookRecord(int phoneId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeletePhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PhoneId", phoneId);          

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