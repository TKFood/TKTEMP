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

            this.IsMdiContainer = true;
        }

        private void 開啟匯入POS銷售ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //檢查子視窗是否存在
            //關閉其他所有子視窗
            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm.GetType() == typeof(Form1))
                {
                    // 子視窗存在，設定為焦點並結束函式
                    childForm.Focus();
                    return;
                }

                childForm.Close();
            }

            // 建立目標 WinForm 物件
            Form1 Form1 = new Form1();

            // 將目標 WinForm 顯示在螢幕上           
            Form1.MdiParent = this;
            Form1.Dock= DockStyle.Fill;
            Form1.Show();
        }

        private void 團務作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //檢查子視窗是否存在
            //關閉其他所有子視窗
            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm.GetType() == typeof(frmGROUPSALES))
                {
                    // 子視窗存在，設定為焦點並結束函式
                    childForm.Focus();
                    return;
                }

                childForm.Close();
            }


            // 建立目標 WinForm 物件
            frmGROUPSALES frmGROUPSALES = new frmGROUPSALES();

            // 將目標 WinForm 顯示在螢幕上  
            frmGROUPSALES.MdiParent = this;
            frmGROUPSALES.Dock = DockStyle.Fill;
            frmGROUPSALES.Show();          
        }
    }
}
