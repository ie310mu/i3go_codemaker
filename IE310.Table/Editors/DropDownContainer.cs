


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Renderers;
using IE310.Table.Win32;


namespace IE310.Table.Editors
{
	/// <summary>
	/// Summary description for DropDownContainer.
	/// </summary>
	[ToolboxItem(false)]
	public class I3DropDownContainer : Form
	{
		#region Class Data

		/// <summary>
        /// 相关联的下拉编辑器
		/// The DropDownCellEditor that owns the DropDownContainer
		/// </summary>
		private I3DropDownCellEditor editor;

		/// <summary>
        /// 下拉窗口中显示的控件
		/// The Control displayed in the DropDownContainer
		/// </summary>
		private Control dropdownControl;

		/// <summary>
        /// 一个Panel，用于显示一个黑框，包含下拉窗口中显示的控件
		/// A Panel that provides the black border around the DropDownContainer
		/// </summary>
		private Panel panel;

		#endregion

		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DropDownContainer class with the 
		/// specified DropDownCellEditor owner
		/// </summary>
		public I3DropDownContainer(I3DropDownCellEditor editor) : base()
		{
			if (editor == null)
			{
				throw new ArgumentNullException("editor", "DropDownCellEditor cannot be null");
			}
			
			this.editor = editor;
			
			this.ControlBox = false;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.FormBorderStyle = FormBorderStyle.None;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.Manual;
			this.TabStop = false;
			this.TopMost = true;

			this.dropdownControl = null;

			this.panel = new Panel();
			this.panel.AutoScroll = false;
			this.panel.BorderStyle = BorderStyle.FixedSingle;
			this.panel.Size = this.Size;
			this.Controls.Add(this.panel);
			this.SizeChanged += new EventHandler(DropDownContainer_SizeChanged);
		}

		#endregion


		#region Methods

		/// <summary>
        /// 显示下拉框
		/// Displays the DropDownContainer to the user
		/// </summary>
		public void ShowDropDown()
		{
			this.FlushPaintMessages();

			this.Show();
		}


		/// <summary>
        /// 隐藏下拉框
		/// Hides the DropDownContainer from the user
		/// </summary>
		public void HideDropDown()
		{
			this.FlushPaintMessages();

			this.Hide();
		}


		/// <summary>
        /// 处理所有Paint事件
		/// Processes any Paint messages in the message queue
		/// </summary>
		private void FlushPaintMessages()
		{
			I3MSG msg = new I3MSG();
			
			while (I3NativeMethods.PeekMessage(ref msg, IntPtr.Zero, (int) I3WindowMessage.WM_PAINT, (int) I3WindowMessage.WM_PAINT, 1 /*PM_REMOVE*/))
			{
				I3NativeMethods.TranslateMessage(ref msg);
				I3NativeMethods.DispatchMessage(ref msg);
			}
		}

		#endregion


		#region Properties

		/// <summary>
        /// 获取或设置下拉框中显示的控件
		/// Gets or sets the Control displayed in the DropDownContainer
		/// </summary>
		public Control Control
		{
			get
			{
				return this.dropdownControl;
			}
			set
			{
				if (value != this.dropdownControl)
				{
                    this.Width = (value == null ? 0 : value.Width) + this.editor.WidthIncrement();
                    this.Height = (value == null ? 0 : value.Height) + this.editor.HeightIncrement();
                    if (value != null)
                    {
                        value.Location = new Point(0, 0);
                    }

					this.panel.Controls.Clear();

					this.dropdownControl = value;

					if (value != null)
					{
						this.panel.Controls.Add(value);
					}
				}
			}
		}

        public BorderStyle PanelBorderStyle
        {
            get
            {
                return this.panel.BorderStyle;
            }
            set
            {
                this.panel.BorderStyle = value;
            }
        }


		/// <summary>
        /// 获取CreateParams对象
		/// Gets the required creation parameters when the control handle is created
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cparams = base.CreateParams;

				cparams.ExStyle |= (int) I3WindowExtendedStyles.WS_EX_TOOLWINDOW;

				if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major > 5)
				{
					cparams.ExStyle |= (int) I3WindowExtendedStyles.WS_EX_NOACTIVATE;
				}

				cparams.ClassStyle |= 0x800 /*CS_SAVEBITS*/;
				
				return cparams;
			}
		}


		/// <summary>
        /// 大小改变事件
		/// Handler for the DropDownContainer's SizeChanged event
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void DropDownContainer_SizeChanged(object sender, EventArgs e)
		{
			this.panel.Size = this.Size;
		}

		#endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DropDownContainer
            // 
            this.ClientSize = new System.Drawing.Size(293, 269);
            this.Name = "DropDownContainer";
            this.ResumeLayout(false);

        }
	}
}
