using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdressBook.Models
{
    public class PersonDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW PERSON *********************
        public bool AddPerson(Person person)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewPerson", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            cmd.Parameters.AddWithValue("@SurName", person.SurName);
            cmd.Parameters.AddWithValue("@Address", person.Address);
            cmd.Parameters.AddWithValue("@PersonAge", person.PersonAge);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW PERSON DETAILS ********************
        public List<Person> GetPerson()
        {
            connection();
            List<Person> personList = new List<Person>();

            SqlCommand cmd = new SqlCommand("GetPersonDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                personList.Add(
                    new Person
                    {
                        PersonId = Convert.ToInt32(dr["PersonId"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        SurName = Convert.ToString(dr["SurName"]),
                        Address = Convert.ToString(dr["Address"]),
                        PersonAge = Convert.ToInt32(dr["PersonAge"])
                    });
            }
            return personList;
        }

        // ***************** UPDATE PERSON DETAILS *********************
        public bool UpdateDetails(Person person)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdatePersonDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonId", person.PersonId);
            cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            cmd.Parameters.AddWithValue("@SurName", person.SurName);
            cmd.Parameters.AddWithValue("@Address", person.Address);
            cmd.Parameters.AddWithValue("@PersonAge", person.PersonAge);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE PERSON DETAILS *******************
        public bool DeletePerson(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeletePerson", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonId", id);

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