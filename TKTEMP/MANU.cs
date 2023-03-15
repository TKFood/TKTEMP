using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TKTEMP
{
    public partial class MANU : Form
    {
        public MANU()
        {
            InitializeComponent();
        }

        private void 開啟匯入POS銷售ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // 建立目標 WinForm 物件
            Form1 Form1 = new Form1();

            // 將目標 WinForm 顯示在螢幕上           
            Form1.Show();
        }

        private void 團務作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 建立目標 WinForm 物件
            frmGROUPSALES frmGROUPSALES = new frmGROUPSALES();

            // 將目標 WinForm 顯示在螢幕上           
            frmGROUPSALES.Show();
        }
    }
}
