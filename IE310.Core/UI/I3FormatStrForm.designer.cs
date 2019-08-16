namespace IE310.Core.UI
{
    internal partial class I3FormatStrForm
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
            this.c = new System.Windows.Forms.RadioButton();
            this.d = new System.Windows.Forms.RadioButton();
            this.e = new System.Windows.Forms.RadioButton();
            this.f = new System.Windows.Forms.RadioButton();
            this.g = new System.Windows.Forms.RadioButton();
            this.n = new System.Windows.Forms.RadioButton();
            this.p = new System.Windows.Forms.RadioButton();
            this.r = new System.Windows.Forms.RadioButton();
            this.x1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.x2 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // c
            // 
            this.c.AutoSize = true;
            this.c.Location = new System.Drawing.Point(12, 12);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(83, 16);
            this.c.TabIndex = 0;
            this.c.Text = "C或c  货币";
            this.c.UseVisualStyleBackColor = true;
            this.c.Click += new System.EventHandler(this.c_Click);
            // 
            // d
            // 
            this.d.AutoSize = true;
            this.d.Location = new System.Drawing.Point(12, 34);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(125, 16);
            this.d.TabIndex = 0;
            this.d.Text = "D或d 十进制(整型)";
            this.d.UseVisualStyleBackColor = true;
            this.d.Click += new System.EventHandler(this.c_Click);
            // 
            // e
            // 
            this.e.AutoSize = true;
            this.e.Location = new System.Drawing.Point(12, 56);
            this.e.Name = "e";
            this.e.Size = new System.Drawing.Size(149, 16);
            this.e.TabIndex = 0;
            this.e.Text = "E或e 科学计数法(指数)";
            this.e.UseVisualStyleBackColor = true;
            this.e.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.e.Click += new System.EventHandler(this.c_Click);
            // 
            // f
            // 
            this.f.AutoSize = true;
            this.f.Checked = true;
            this.f.Location = new System.Drawing.Point(12, 78);
            this.f.Name = "f";
            this.f.Size = new System.Drawing.Size(89, 16);
            this.f.TabIndex = 0;
            this.f.TabStop = true;
            this.f.Text = "F或f 固定点";
            this.f.UseVisualStyleBackColor = true;
            this.f.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.f.Click += new System.EventHandler(this.c_Click);
            // 
            // g
            // 
            this.g.AutoSize = true;
            this.g.Enabled = false;
            this.g.Location = new System.Drawing.Point(12, 100);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(83, 16);
            this.g.TabIndex = 0;
            this.g.Text = "G或g 常规*";
            this.g.UseVisualStyleBackColor = true;
            this.g.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.g.Click += new System.EventHandler(this.c_Click);
            // 
            // n
            // 
            this.n.AutoSize = true;
            this.n.Location = new System.Drawing.Point(12, 122);
            this.n.Name = "n";
            this.n.Size = new System.Drawing.Size(77, 16);
            this.n.TabIndex = 0;
            this.n.Text = "N或n 数字";
            this.n.UseVisualStyleBackColor = true;
            this.n.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.n.Click += new System.EventHandler(this.c_Click);
            // 
            // p
            // 
            this.p.AutoSize = true;
            this.p.Location = new System.Drawing.Point(12, 144);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(89, 16);
            this.p.TabIndex = 0;
            this.p.Text = "P或p 百分比";
            this.p.UseVisualStyleBackColor = true;
            this.p.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.p.Click += new System.EventHandler(this.c_Click);
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Enabled = false;
            this.r.Location = new System.Drawing.Point(12, 166);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(107, 16);
            this.r.TabIndex = 0;
            this.r.Text = "R或r 往返过程*";
            this.r.UseVisualStyleBackColor = true;
            this.r.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.r.Click += new System.EventHandler(this.c_Click);
            // 
            // x1
            // 
            this.x1.AutoSize = true;
            this.x1.Location = new System.Drawing.Point(12, 188);
            this.x1.Name = "x1";
            this.x1.Size = new System.Drawing.Size(119, 16);
            this.x1.TabIndex = 0;
            this.x1.Text = "X 十六进制(大写)";
            this.x1.UseVisualStyleBackColor = true;
            this.x1.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.x1.Click += new System.EventHandler(this.c_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(170, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "C3:  ￥12,345.000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(170, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "D8:  00012345";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(170, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "E6:  1.234500e+004";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(170, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "F4:  12345.0000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(170, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "N3:  12,345.000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(170, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "P3:  1,234,500.000%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(170, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "X8:  0A003039";
            // 
            // x2
            // 
            this.x2.AutoSize = true;
            this.x2.Location = new System.Drawing.Point(12, 210);
            this.x2.Name = "x2";
            this.x2.Size = new System.Drawing.Size(119, 16);
            this.x2.TabIndex = 0;
            this.x2.Text = "x 十六进制(小写)";
            this.x2.UseVisualStyleBackColor = true;
            this.x2.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.x2.Click += new System.EventHandler(this.c_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(170, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "x8:  0a003039";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "精度(小数位数或数字长度):";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(170, 251);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "2";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(131, 301);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(212, 301);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // I3FormatStrForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(300, 354);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.x2);
            this.Controls.Add(this.x1);
            this.Controls.Add(this.r);
            this.Controls.Add(this.p);
            this.Controls.Add(this.n);
            this.Controls.Add(this.g);
            this.Controls.Add(this.f);
            this.Controls.Add(this.e);
            this.Controls.Add(this.d);
            this.Controls.Add(this.c);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "I3FormatStrForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "格式";
            this.Load += new System.EventHandler(this.IEFS_FormatStrForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton c;
        private System.Windows.Forms.RadioButton d;
        private System.Windows.Forms.RadioButton e;
        private System.Windows.Forms.RadioButton f;
        private System.Windows.Forms.RadioButton g;
        private System.Windows.Forms.RadioButton n;
        private System.Windows.Forms.RadioButton p;
        private System.Windows.Forms.RadioButton r;
        private System.Windows.Forms.RadioButton x1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton x2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
    }
}