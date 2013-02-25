using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace iter2
{
    public partial class Employee : Form
    {
        private Database dbobject = new Database();
        private string option = "";

        public Employee()
        {
            InitializeComponent();
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.employeeTableAdapter.Update(this.foodMartDataSet.employee);
            //this.tableAdapterManager.UpdateAll(this.foodMartDataSet);
            //this.employeeDataGridView.Refresh();

            //enable button
            idbox.Enabled = true;
            bindingNavigatorAddNewItem.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.employeeTableAdapter.Fill(this.foodMartDataSet.employee);
            //select first line to generate departmentBox content, no letting it be null
            DataGridViewCell cell = employeeDataGridView[5, 0];
            int value = Convert.ToInt32(cell.Value.ToString());
            //MessageBox.Show("gridview" + cell.Value.ToString());
            departmentBox.Text = innerSearch(value);
        }

        private void storeBtn_Click(object sender, EventArgs e)
        {
            ChooseStore chooseDialog = new ChooseStore();
            //because the dialog will make changes to Employee frame,
            //have to specify this Instance of employee frame class
            chooseDialog.empPointer = this;
            chooseDialog.method = "getstore";
            chooseDialog.ShowDialog();
        }

        private void chooseSupervisorBtn_Click(object sender, EventArgs e)
        {
            ChooseEmployee chooseDialog = new ChooseEmployee();
            //because the dialog will make changes to Employee frame,
            //have to specify this Instance of employee frame class
            chooseDialog.empPointer = this;
            chooseDialog.method = "getemployee";
            chooseDialog.ShowDialog();
        }

        //convert combobox index to department id
        private void departmentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (departmentBox != null && departmentBox.SelectedIndex != -1)
            {
                string s = departmentBox.SelectedItem.ToString();
                int resultIndex = departmentBox.FindStringExact(s);
                switch (resultIndex)
                {
                    case 1: label6.Text = "1";
                        break;
                    case 2: label6.Text = "2";
                        break;
                    case 3: label6.Text = "3";
                        break;
                    case 4: label6.Text = "4";
                        break;
                    case 5: label6.Text = "5";
                        break;
                    case 6: label6.Text = "11";
                        break;
                    case 7: label6.Text = "14";
                        break;
                    case 8: label6.Text = "15";
                        break;
                    case 9: label6.Text = "16";
                        break;
                    case 10: label6.Text = "17";
                        break;
                    case 11: label6.Text = "18";
                        break;
                    case 12: label6.Text = "19";
                        break;
                }
            }
            
        }

        private string innerSearch(int value)
        {
            switch (value)
            {
                case 1:
                    return "HQ General Management";
                    
                case 2:
                    return "HQ Information Systems";
                    
                case 3:
                    return "HQ Marketing";
                    
                case 4:
                    return "HQ Human Resources";
                    
                case 5:
                    return "HQ Finance and Accounting";
                   
                case 11:
                    return "Store Management";
                    
                case 14:
                    return "Store Information Systems";
                    
                case 15:
                    return "Store Permanent Checkers";
                    
                case 16:
                    return "Store Temporary Checkers";
                   
                case 17:
                    return "Store Permanent Stockers";
                    
                case 18:
                    return "Store Temporary Stockers";
                    
                case 19:
                    return "Store Permanent Butchers";
                    
            }
            return null;
        }

        //update department id
        private void employeeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = 5; //choose department id
            int r = e.RowIndex;
            DataGridViewCell cell = employeeDataGridView[c, r];
            int value = Convert.ToInt32(cell.Value.ToString());
            //MessageBox.Show("gridview" + cell.Value.ToString());
            departmentBox.Text = innerSearch(value);
        }

        //extra event handler, no data saving and procesing
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //disable some button
            if (idbox.Enabled.Equals(true))
            {
                idbox.Enabled = false;
                bindingNavigatorAddNewItem.Enabled = false;
            }
            //clear all the field
            clearAll();
            
            //automatically setup id field 
            DataSet ds = dbobject.getEmployee();

            if (ds.Tables[0].Rows.Count == 0)
            {
                idbox.Text = "1";
            }
            else
            {
                DataTable t = ds.Tables[0]; 
                int lastid = Convert.ToInt32(t.Rows[t.Rows.Count - 1]["employee_id"]); //last row id column
                idbox.Text = Convert.ToString(lastid + 1);//inc 1
            }
        }

        private void clearAll()
        {
            idbox.Text = string.Empty;
            namebox.Text = string.Empty;
            genderbox.SelectedIndex = -1;
            titleBox.SelectedIndex = -1;
            salaryText.Text = string.Empty;
            storeBtn.Text = string.Empty;
            departmentBox.SelectedIndex = -1;
            dob.ShowCheckBox = true;
            hire.ShowCheckBox = true;
            chooseSupervisorBtn.Text = string.Empty;
            educationBox.SelectedIndex = -1;
            maritalstatusBox.SelectedIndex = -1;
            managementroleBox.SelectedIndex = -1;
        }

        private void idToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = "1";
        }

        private void partOfNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = "2";
        }

        private void positionTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = "3";
        }

        private void genderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option = "4";
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (option.Equals("1"))
                {
                    this.employeeBindingSource.Filter = "employee_id = " + this.searchText;
                }
                else if (option.Equals("2"))
                {
                    this.employeeBindingSource.Filter = "full_name LIKE '%" + this.searchText.ToString() + "%'";
                }
                else if (option.Equals("3"))
                {
                    this.employeeBindingSource.Filter = "position_title LIKE '%" + this.searchText.ToString() + "%'";
                }
                else if (option.Equals("4"))
                {
                    string s = this.searchText.ToString().ToUpper();
                    if (s == "MALE" || s == "M")
                        this.employeeBindingSource.Filter = "gender = 'M'";
                    else if (s == "FEMALE" || s == "F")
                        this.employeeBindingSource.Filter = "gender = 'F'";
                }
            }
            catch
            {
                MessageBox.Show("choose proper input");
            }
        }

        //print button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ApplicationClass excel = new ApplicationClass();
            excel.Visible = true;
            
            Workbooks workbooks = excel.Workbooks;
            //create active book
            Workbook workbook = workbooks.Add(Missing.Value);

            Worksheet worksheet = (Worksheet)workbook.Sheets.get_Item(1);
            //convert to ASCII char
            char columns = (char) (foodMartDataSet.employee.Columns.Count + 64);
            Range range = worksheet.get_Range("A6",columns.ToString() + "6");
            Object[,] data = new object[1000,35];
            int i, j;
            
            //output column header
            int counter = 0;
            foreach (DataColumn c in foodMartDataSet.employee.Columns)
            {
                data[0, counter] = c.ColumnName;
                counter += 1;
            }

            //output data from db
            j = 1;
            DataTable dt = foodMartDataSet.employee;
            foreach (DataRow r in dt.Rows)
            {
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    data[j, i] = r[i].ToString();
                }
                j += 1;
            }

            range = range.get_Resize(dt.Rows.Count + 1,
                                     dt.Columns.Count);
            range.Value2 = data;
            range.EntireColumn.AutoFit(); //style

            //add title
            worksheet.Cells[2, 2] = "AA Company Employee List";
            Range range2 = worksheet.get_Range("B2", "B2");
            range2.Font.Bold = "true";
            range2.Font.Size = "20";
            worksheet.Cells[4, 1] = "Date：" + DateTime.Now.ToShortDateString();
        }
    }
}
