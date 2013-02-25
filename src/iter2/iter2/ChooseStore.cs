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
    public partial class ChooseStore : Form
    {

        public string method = "";
        public Employee empPointer;

        public ChooseStore()
        {
            InitializeComponent();
        }

        private void ChooseStore_Load(object sender, EventArgs e)
        {
            
            this.storeTableAdapter.Fill(this.foodMartDataSet.store);

        }

        private void storeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (method == "getstore")
            {
                //column , row
                empPointer.storeBtn.Text = storeDataGridView[0, e.RowIndex].Value.ToString();
                this.Close();
            }
        }
    }
}
