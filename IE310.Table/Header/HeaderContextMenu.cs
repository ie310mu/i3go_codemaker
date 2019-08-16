
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using IE310.Table;
using IE310.Table.Events;
using IE310.Table.Win32;
using IE310.Table.Column;
using IE310.Table.Models;


namespace IE310.Table.Header
{
	/// <summary>
	/// A specialized ContextMenu for Column Headers
	/// </summary>
	[ToolboxItem(false)]
	public class I3ColumnHeaderContextMenu : ContextMenu
	{
		#region Class Data
		
		/// <summary>
		/// The ColumnModel that owns the menu
		/// </summary>
		private I3ColumnModel model;

		/// <summary>
		/// Specifies whether the menu is enabled
		/// </summary>
		private bool enabled;

		/// <summary>
		/// More columns menuitem
		/// </summary>
		private MenuItem moreMenuItem;

		/// <summary>
		/// Seperator menuitem
		/// </summary>
		private MenuItem separator;

		#endregion

        
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the HeaderContextMenu class with 
		/// no menu items specified
		/// </summary>
		public I3ColumnHeaderContextMenu() : base()
		{
			this.model = null;
			this.enabled = true;

			this.moreMenuItem = new MenuItem("More...", new EventHandler(moreMenuItem_Click));
			this.separator = new MenuItem("-");
		}

		#endregion


		#region Methods
		
		/// <summary>
		/// Displays the shortcut menu at the specified position
		/// </summary>
		/// <param name="control">A Control object that specifies the control 
		/// with which this shortcut menu is associated</param>
		/// <param name="pos">A Point object that specifies the coordinates at 
		/// which to display the menu. These coordinates are specified relative 
		/// to the client coordinates of the control specified in the control 
		/// parameter</param>
        public new void Show(Control control, Point pos)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control", "control cannot be null");
            }

            if (!(control is I3Table))
            {
                throw new ArgumentException("control must be of type Table", "control");
            }

            if (((I3Table)control).ColumnModel == null)
            {
                throw new InvalidOperationException("The specified Table does not have an associated ColumnModel");
            }

            //
            this.model = ((I3Table)control).ColumnModel;

            //
            this.MenuItems.Clear();

            //base.Show(control, pos);
            I3ShowColumnsDialog scd = new I3ShowColumnsDialog();
            scd.AddColumns(this.model);
            Screen screen = Screen.FromPoint(pos);
            if (screen == null)
            {
                screen = Screen.PrimaryScreen;
            }

            //Table table = (Table)control;
            //Form form = table.FindForm();
            //Point point = table.PointToScreen(new Point(table.TableModel.RowHeaderWidth + 20, -20));
            //scd.Left = point.X;
            //scd.Top = point.Y;
            //scd.Top = Convert.ToInt32(screen.WorkingArea.Height / 2 - scd.Height / 2);

            if (this.model.Table != null)
            {
                this.model.Table.BeginUpdate();
            }
            try
            {
                scd.ShowDialog(this.SourceControl);
                this.model.Columns.RecalcWidthCache();
            }
            finally
            {
                ColumnDisplayHelper.ReSort(model);
                if (this.model.Table != null)
                {
                    this.model.Table.EndUpdate();
                }
            }
        }


		/// <summary>
		/// 
		/// </summary>
		internal bool Enabled
		{
			get
			{
				return this.enabled;
			}

			set
			{
				this.enabled = value;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the Popup event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnPopup(EventArgs e)
		{
			if (this.model.Columns.Count > 0)
			{
				MenuItem item;
				
				for (int i=0; i<this.model.Columns.Count; i++)
				{
					if (i == 10)
					{
						this.MenuItems.Add(this.separator);
						this.MenuItems.Add(this.moreMenuItem);

						break;
					}

					item = new MenuItem(this.model.Columns[i].Caption, new EventHandler(menuItem_Click));
					item.Checked = this.model.Columns[i].Visible;

					this.MenuItems.Add(item);
				}
			}

			base.OnPopup(e);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Click(object sender, EventArgs e)
		{
			MenuItem item = (MenuItem) sender;
			
			this.model.Columns[item.Index].Visible = !item.Checked;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void moreMenuItem_Click(object sender, EventArgs e)
		{
			I3ShowColumnsDialog scd = new I3ShowColumnsDialog();
			scd.AddColumns(this.model);

            if (this.model.Table != null)
            {
                this.model.Table.BeginUpdate();
            }
            try
            {
                scd.ShowDialog(this.SourceControl);
                this.model.Columns.RecalcWidthCache();
            }
            finally
            {
                ColumnDisplayHelper.ReSort(model);
                if (this.model.Table != null)
                {
                    this.model.Table.EndUpdate();
                }
            }
		}

		#endregion

	}
}
