using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace iter2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            FrmLogin loginform = new FrmLogin();
            loginform.ShowDialog();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Application.ThreadException += MyThreadException;
                //this.WindowState = FormWindowState.Maximized;
                //TODO: set bg / icon
                
            }
            catch
            {
                this.Close();
            }
            
        }

        private void MyThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string MyInfo = "Exception name：" + e.Exception.Source + "，Info：" + e.Exception.Message;
            MessageBox.Show(MyInfo, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You sure to exit？", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        protected override void WndProc(ref Message SystemMessage)
        {      
            switch (SystemMessage.Msg)
            {
                case 0x0112:
                    if (((int)SystemMessage.WParam) == 61536)
                    {
                        button1_Click(null, null);
                    }
                    else
                    {
                        base.WndProc(ref SystemMessage);
                    }
                    break;
                default:
                    base.WndProc(ref SystemMessage);
                    break;
            }
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Employee().Show();
        }
    }
}
