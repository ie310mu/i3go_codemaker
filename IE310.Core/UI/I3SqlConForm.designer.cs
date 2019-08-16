namespace IE310.Core.UI
{
    internal partial class I3SqlConForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbDataBase = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbPassWord = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbSql = new System.Windows.Forms.RadioButton();
            this.rbWindows = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(130, 374);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 15;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(211, 374);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 16;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.cbDataBase);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.tbPassWord);
            this.panel4.Controls.Add(this.tbUserName);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.rbSql);
            this.panel4.Controls.Add(this.rbWindows);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.cbDataSource);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(4, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(290, 351);
            this.panel4.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "枚举";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(207, 315);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(8, 296);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 1);
            this.panel3.TabIndex = 28;
            // 
            // cbDataBase
            // 
            this.cbDataBase.FormattingEnabled = true;
            this.cbDataBase.Location = new System.Drawing.Point(21, 255);
            this.cbDataBase.Name = "cbDataBase";
            this.cbDataBase.Size = new System.Drawing.Size(261, 20);
            this.cbDataBase.TabIndex = 27;
            this.cbDataBase.DropDown += new System.EventHandler(this.cbDataBase_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "在服务器上选择数据库:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(8, 208);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 1);
            this.panel2.TabIndex = 25;
            // 
            // tbPassWord
            // 
            this.tbPassWord.Enabled = false;
            this.tbPassWord.Location = new System.Drawing.Point(80, 166);
            this.tbPassWord.Name = "tbPassWord";
            this.tbPassWord.PasswordChar = '*';
            this.tbPassWord.Size = new System.Drawing.Size(202, 21);
            this.tbPassWord.TabIndex = 24;
            // 
            // tbUserName
            // 
            this.tbUserName.Enabled = false;
            this.tbUserName.Location = new System.Drawing.Point(79, 133);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(203, 21);
            this.tbUserName.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "密码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "用户名:";
            // 
            // rbSql
            // 
            this.rbSql.AutoSize = true;
            this.rbSql.Location = new System.Drawing.Point(7, 111);
            this.rbSql.Name = "rbSql";
            this.rbSql.Size = new System.Drawing.Size(149, 16);
            this.rbSql.TabIndex = 20;
            this.rbSql.Text = "使用SqlServer验证模式";
            this.rbSql.UseVisualStyleBackColor = true;
            this.rbSql.Click += new System.EventHandler(this.rbWindows_Click);
            // 
            // rbWindows
            // 
            this.rbWindows.AutoSize = true;
            this.rbWindows.Checked = true;
            this.rbWindows.Location = new System.Drawing.Point(7, 77);
            this.rbWindows.Name = "rbWindows";
            this.rbWindows.Size = new System.Drawing.Size(161, 16);
            this.rbWindows.TabIndex = 19;
            this.rbWindows.TabStop = true;
            this.rbWindows.Text = "使用Windows身份验证模式";
            this.rbWindows.UseVisualStyleBackColor = true;
            this.rbWindows.Click += new System.EventHandler(this.rbWindows_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(8, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 1);
            this.panel1.TabIndex = 18;
            // 
            // cbDataSource
            // 
            this.cbDataSource.FormattingEnabled = true;
            this.cbDataSource.Location = new System.Drawing.Point(64, 15);
            this.cbDataSource.Name = "cbDataSource";
            this.cbDataSource.Size = new System.Drawing.Size(137, 20);
            this.cbDataSource.TabIndex = 16;
            this.cbDataSource.DropDown += new System.EventHandler(this.cbDataSource_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "服务器名:";
            // 
            // I3SqlConForm
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(298, 407);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "I3SqlConForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据连接　";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbDataBase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbPassWord;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbSql;
        private System.Windows.Forms.RadioButton rbWindows;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbDataSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}