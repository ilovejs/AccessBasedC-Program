using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iter2
{
    public partial class ChooseEmployee : Form
    {
        public Employee empPointer;
        public string method = "";

        public ChooseEmployee()
        {
            InitializeComponent();
        }

        private void ChooseEmployee_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“foodMartDataSet.employee”中。您可以根据需要移动或删除它。
            this.employeeTableAdapter.Fill(this.foodMartDataSet.employee);

        }

        private void employeeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (method == "getemployee")
            {
                //column , row
                empPointer.chooseSupervisorBtn.Text = employeeDataGridView[0, e.RowIndex].Value.ToString();
                this.Close();
            }
        }
    }
}
