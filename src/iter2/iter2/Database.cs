using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iter2
{
    class Database
    {
        public OleDbConnection con;

        public void Open()
        {
            if (con == null)
            {
                string constr = global::iter2.Properties.Settings.Default.FoodMartConnectionString;
                con = new OleDbConnection(constr);
                con.Open();
            }
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }

        public void Close()
        {
            if (con != null)
                con.Close();
        }

        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        internal DataSet getEmployee()
        {
            string q = @"Select * from employee";
            DataSet set = null;
            try
            {
                Open();
                OleDbDataAdapter da = new OleDbDataAdapter(q, con);
                set = new DataSet();
                da.Fill(set);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                Close();
            }
            return set;
        }



        internal void addEmployee(Employee e)
        {
            string sqlcommand = "INSERT INTO employee (" +
                            "employee_id, full_name, first_name, last_name, " +
                            "position_id, position_title, store_id, department_id, birth_date, " +
                            "hire_date, end_date, supervisor_id, education_level, " +
                            "marital_status, gender, management_role) " +
                         "VALUES (" +
                            "@eid, @fulln, @fn, @ln, " +
                            "@pid, @pt, @sid, @did, @bd, " +
                            "@hd, @ed, @spid, @edu," +
                            "@ms, @g, @mr);";
            Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.InsertCommand = new OleDbCommand(sqlcommand, con);
            adapter.InsertCommand.Parameters.Add("@eid", OleDbType.Numeric).Value = Convert.ToInt32(e.idbox.Text);
            adapter.InsertCommand.Parameters.Add("@fulln", OleDbType.VarChar).Value = e.namebox.Text;
            adapter.InsertCommand.Parameters.Add("@fn", OleDbType.VarChar).Value = "a";  //
            adapter.InsertCommand.Parameters.Add("@ln", OleDbType.VarChar).Value = "b";  //

            adapter.InsertCommand.Parameters.Add("@pid", OleDbType.Numeric).Value = 1;  //
            adapter.InsertCommand.Parameters.Add("@pt", OleDbType.VarChar).Value = e.titleBox.Text;

            adapter.InsertCommand.Parameters.Add("@sid", OleDbType.Numeric).Value = Convert.ToInt32(e.storeBtn.Text);
            adapter.InsertCommand.Parameters.Add("@did", OleDbType.Numeric).Value = 1;  //department box is the name "xxx"

            adapter.InsertCommand.Parameters.Add("@bd", OleDbType.DBDate).Value = Convert.ToDateTime(e.dob.Text);
            adapter.InsertCommand.Parameters.Add("@hd", OleDbType.DBDate).Value = Convert.ToDateTime(e.hire.Text);
            adapter.InsertCommand.Parameters.Add("@ed", OleDbType.DBDate).Value = Convert.ToDateTime("2019/1/1");

            adapter.InsertCommand.Parameters.Add("@spid", OleDbType.Numeric).Value = Convert.ToInt32(e.chooseSupervisorBtn.Text);
            adapter.InsertCommand.Parameters.Add("@edu", OleDbType.VarChar).Value = e.educationBox.Text;

            adapter.InsertCommand.Parameters.Add("@ms", OleDbType.VarChar).Value = e.maritalstatusBox.Text;
            adapter.InsertCommand.Parameters.Add("@g", OleDbType.VarChar).Value = e.genderbox.Text;
            adapter.InsertCommand.Parameters.Add("@mr", OleDbType.VarChar).Value = e.managementroleBox.Text;


            //execute sql
            int counter = (int)adapter.InsertCommand.ExecuteNonQuery();
            System.Console.WriteLine("\nRow(s) Added = " + counter + "\n");

//            DataSet tmp = getEmployee();

            Close();
            //return tmp;
        }
    }
}
