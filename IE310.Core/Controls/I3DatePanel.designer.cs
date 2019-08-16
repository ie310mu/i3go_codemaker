namespace IE310.Core.Controls
{
    partial class I3DatePanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gp = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btNextMonth = new System.Windows.Forms.Button();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.btPrvMonth = new System.Windows.Forms.Button();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.rbYear = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gp
            // 
            this.gp.Controls.Add(this.label1);
            this.gp.Controls.Add(this.btNextMonth);
            this.gp.Controls.Add(this.dtEnd);
            this.gp.Controls.Add(this.dtBegin);
            this.gp.Controls.Add(this.btPrvMonth);
            this.gp.Controls.Add(this.rbOther);
            this.gp.Controls.Add(this.rbDay);
            this.gp.Controls.Add(this.rbWeek);
            this.gp.Controls.Add(this.rbMonth);
            this.gp.Controls.Add(this.rbYear);
            this.gp.Controls.Add(this.rbAll);
            this.gp.Controls.Add(this.panel1);
            this.gp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gp.Location = new System.Drawing.Point(0, 0);
            this.gp.Name = "gp";
            this.gp.Size = new System.Drawing.Size(348, 86);
            this.gp.TabIndex = 0;
            this.gp.TabStop = false;
            this.gp.Text = "时间范围";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "～";
            // 
            // btNextMonth
            // 
            this.btNextMonth.Location = new System.Drawing.Point(294, 47);
            this.btNextMonth.Name = "btNextMonth";
            this.btNextMonth.Size = new System.Drawing.Size(47, 23);
            this.btNextMonth.TabIndex = 10;
            this.btNextMonth.TabStop = false;
            this.btNextMonth.Text = "下月";
            this.btNextMonth.UseVisualStyleBackColor = true;
            this.btNextMonth.Click += new System.EventHandler(this.btNextMonth_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(188, 48);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowCheckBox = true;
            this.dtEnd.Size = new System.Drawing.Size(100, 21);
            this.dtEnd.TabIndex = 9;
            this.dtEnd.Leave += new System.EventHandler(this.dtBegin_Leave);
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd";
            this.dtBegin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(59, 48);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.ShowCheckBox = true;
            this.dtBegin.Size = new System.Drawing.Size(100, 21);
            this.dtBegin.TabIndex = 7;
            this.dtBegin.Leave += new System.EventHandler(this.dtBegin_Leave);
            // 
            // btPrvMonth
            // 
            this.btPrvMonth.Location = new System.Drawing.Point(6, 47);
            this.btPrvMonth.Name = "btPrvMonth";
            this.btPrvMonth.Size = new System.Drawing.Size(47, 23);
            this.btPrvMonth.TabIndex = 6;
            this.btPrvMonth.TabStop = false;
            this.btPrvMonth.Text = "上月";
            this.btPrvMonth.UseVisualStyleBackColor = true;
            this.btPrvMonth.Click += new System.EventHandler(this.btPrvMonth_Click);
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(291, 22);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(47, 16);
            this.rbOther.TabIndex = 5;
            this.rbOther.Text = "其他";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Location = new System.Drawing.Point(234, 22);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(47, 16);
            this.rbDay.TabIndex = 4;
            this.rbDay.Text = "本日";
            this.rbDay.UseVisualStyleBackColor = true;
            this.rbDay.CheckedChanged += new System.EventHandler(this.rbDay_CheckedChanged);
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(177, 22);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(47, 16);
            this.rbWeek.TabIndex = 3;
            this.rbWeek.Text = "本周";
            this.rbWeek.UseVisualStyleBackColor = true;
            this.rbWeek.CheckedChanged += new System.EventHandler(this.rbWeek_CheckedChanged);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(120, 22);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(47, 16);
            this.rbMonth.TabIndex = 2;
            this.rbMonth.Text = "本月";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // rbYear
            // 
            this.rbYear.AutoSize = true;
            this.rbYear.Location = new System.Drawing.Point(63, 22);
            this.rbYear.Name = "rbYear";
            this.rbYear.Size = new System.Drawing.Size(47, 16);
            this.rbYear.TabIndex = 1;
            this.rbYear.Text = "本年";
            this.rbYear.UseVisualStyleBackColor = true;
            this.rbYear.CheckedChanged += new System.EventHandler(this.rbYear_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(6, 22);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(47, 16);
            this.rbAll.TabIndex = 0;
            this.rbAll.Text = "所有";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 66);
            this.panel1.TabIndex = 11;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // IECT_DatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gp);
            this.Name = "IECT_DatePanel";
            this.Size = new System.Drawing.Size(348, 86);
            this.gp.ResumeLayout(false);
            this.gp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gp;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Button btPrvMonth;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbWeek;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.RadioButton rbYear;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btNextMonth;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}
