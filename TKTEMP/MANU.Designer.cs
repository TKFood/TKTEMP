namespace TKTEMP
{
    partial class MANU
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.開啟匯入POS銷售ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開啟匯入POS銷售ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.團務ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.團務作業ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟匯入POS銷售ToolStripMenuItem,
            this.團務ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 開啟匯入POS銷售ToolStripMenuItem
            // 
            this.開啟匯入POS銷售ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟匯入POS銷售ToolStripMenuItem1});
            this.開啟匯入POS銷售ToolStripMenuItem.Name = "開啟匯入POS銷售ToolStripMenuItem";
            this.開啟匯入POS銷售ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.開啟匯入POS銷售ToolStripMenuItem.Text = "資料同步";
            // 
            // 開啟匯入POS銷售ToolStripMenuItem1
            // 
            this.開啟匯入POS銷售ToolStripMenuItem1.Name = "開啟匯入POS銷售ToolStripMenuItem1";
            this.開啟匯入POS銷售ToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.開啟匯入POS銷售ToolStripMenuItem1.Text = "開啟匯入POS銷售";
            this.開啟匯入POS銷售ToolStripMenuItem1.Click += new System.EventHandler(this.開啟匯入POS銷售ToolStripMenuItem1_Click);
            // 
            // 團務ToolStripMenuItem
            // 
            this.團務ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.團務作業ToolStripMenuItem});
            this.團務ToolStripMenuItem.Name = "團務ToolStripMenuItem";
            this.團務ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.團務ToolStripMenuItem.Text = "團務";
            // 
            // 團務作業ToolStripMenuItem
            // 
            this.團務作業ToolStripMenuItem.Name = "團務作業ToolStripMenuItem";
            this.團務作業ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.團務作業ToolStripMenuItem.Text = "團務作業";
            this.團務作業ToolStripMenuItem.Click += new System.EventHandler(this.團務作業ToolStripMenuItem_Click);
            // 
            // MANU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 727);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MANU";
            this.Text = "MANU";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 開啟匯入POS銷售ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開啟匯入POS銷售ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 團務ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 團務作業ToolStripMenuItem;
    }
}