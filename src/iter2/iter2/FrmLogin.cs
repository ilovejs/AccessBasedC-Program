using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iter2
{
    public partial class FrmLogin : Form
    {
        private int ntry = 0;
        public FrmLogin()
        {
            InitializeComponent();
            
        }

        private void login()
        {
            String connstr = global::iter2.Properties.Settings.Default.FoodMartConnectionString;
            string sql = "Select COUNT(*) From admin Where username=@usr AND password=@psw";
            OleDbConnection conn = new OleDbConnection(connstr);
            conn.Open();

            OleDbCommand command = new OleDbCommand(sql, conn);
            //add params
            OleDbParameter pname = new OleDbParameter();
            pname.ParameterName = "@usr";
            pname.Value = this.textBox1.Text;

            OleDbParameter ppsw = new OleDbParameter();
            ppsw.ParameterName = "@psw";
            ppsw.Value = this.textBox2.Text;

            //insert into command
            command.Parameters.Add(pname);
            command.Parameters.Add(ppsw);

            //execute sql
            int counter = (int)command.ExecuteScalar();

            if (counter == 1)
            {
                this.Close(); //success, goto main form
            }
            else
            {
                ntry++;
                if (ntry != 3)
                {
                    int nleft = 3 - ntry;
                    MessageBox.Show("Username or Password is wrong, " + nleft + " times left.");
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox1.Focus();
                }
                else
                {
                    button2_Click(null, null);
                }
            }
            //close databse
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        //terminate login windows
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //press enter key
            if (e.KeyValue == 13)
            {
                login();
            }
        }
    }
}
