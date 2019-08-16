

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using IE310.Table.Events;
using IE310.Table.Models; 
using IE310.Table.Win32;
using IE310.Table.Cell;


namespace IE310.Table.Editors
{
	/// <summary>
    /// Cell editors�Ļ���
	/// Base class for Cell editors
	/// </summary>
	public abstract class I3CellEditor : II3CellEditor, II3MouseMessageFilterClient, II3KeyMessageFilterClient
	{
		#region Event Handlers

		/// <summary>
        /// ��ʼ�༭�¼�
		/// Occurs when the CellEditor begins editing a Cell
		/// </summary>
		public event I3CellEditEventHandler BeginEdit;

		/// <summary>
        /// �����༭�¼�  ע�⣬e.Cancel������
		/// Occurs when the CellEditor stops editing a Cell
		/// </summary>
		public event I3CellEditEventHandler EndEdit;

		/// <summary>
        /// �˳��༭�¼� ע�⣬e.Cancel������
		/// Occurs when the editing of a Cell is cancelled
		/// </summary>
		public event I3CellEditEventHandler CancelEdit;

		#endregion


		#region Class Data

		/// <summary>
        /// �༭�ؼ�
		/// The Control that is performing the editing
		/// </summary>
		private Control control;

		/// <summary>
        /// ���༭��Cell
		/// The Cell that is being edited
		/// </summary>
		internal I3Cell cell;

		/// <summary>
        /// ���༭��Table
		/// The Table that contains the Cell being edited
		/// </summary>
        private I3Table table;

		/// <summary>
        /// ���༭��Cell��λ��
		/// A CellPos that represents the position of the Cell being edited
		/// </summary>
		private I3CellPos cellPos;

		/// <summary>
        /// ���༭��Cell��λ�úʹ�С
		/// The Rectangle that represents the Cells location and size
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
        /// �����Ϣ������
		/// A MouseMessageFilter that receives mouse messages before they 
		/// are dispatched to their destination
		/// </summary>
		private I3MouseMessageFilter mouseMessageFilter;

		/// <summary>
        /// ������Ϣ������
		/// A KeyMessageFilter that receives key messages before they 
		/// are dispatched to their destination
		/// </summary>
		private I3KeyMessageFilter keyMessageFilter;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEditor class with default settings
		/// </summary>
		protected I3CellEditor()
		{
			this.control = null;
			this.cell = null;
			this.table = null;
			this.cellPos = I3CellPos.Empty;
			this.cellRect = Rectangle.Empty;

			this.mouseMessageFilter = new I3MouseMessageFilter(this);
			this.keyMessageFilter = new I3KeyMessageFilter(this);
		}

		#endregion


		#region Methods

        /// <summary>
        /// �༭���Ƿ���������м��¼�
        /// </summary>
        /// <returns></returns>
        public virtual bool HandleMouseWheel()
        {
            return false;
        }

		/// <summary>
        /// ���ⷽ����׼���༭����ICellEditor�ж���
		/// Prepares the CellEditor to edit the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be edited</param>
		/// <param name="table">The Table that contains the Cell</param>
		/// <param name="cellPos">A CellPos representing the position of the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the Cells location and size</param>
		/// <param name="userSetEditorValues">Specifies whether the ICellEditors 
		/// starting value has already been set by the user</param>
		/// <returns>true if the ICellEditor can continue editing the Cell, false otherwise</returns>
        public virtual bool PrepareForEditing(I3Cell cell, I3Table table, I3CellPos cellPos, Rectangle cellRect, bool userSetEditorValues)
		{
			//��ʼ��ֵ
			this.cell = cell;
			this.table = table;
			this.cellPos = cellPos;
			this.cellRect = cellRect;

            //����û��Ƿ��Ѿ��Լ������˱༭�ؼ���ֵ�����û������� SetEditValue() ������������
			// check if the user has already set the editors value for us
			if (!userSetEditorValues)
			{
				this.SetEditValue();
			}

            //���ñ༭�ؼ���λ�úʹ�С
			this.SetEditLocation(cellRect);

            //������ʼ�༭�¼�
			// raise the BeginEdit event
			I3CellEditEventArgs e = new I3CellEditEventArgs(cell, this, table, cellPos.Row, cellPos.Column, cellRect);
			e.Handled = userSetEditorValues;

			this.OnBeginEdit(e);
			
            //����༭���˳����ƶ��༭�ؼ�������false
			// if the edit has been canceled, remove the editor and return false
			if (e.Cancel)
			{
				this.RemoveEditControl();

				return false;
			}

			return true;
		}


		/// <summary>
        /// ���󷽷�����λ�úʹ�С
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected abstract void SetEditLocation(Rectangle cellRect);


		/// <summary>
        /// ���󷽷������ñ༭��ֵ
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected abstract void SetEditValue();


		/// <summary>
        /// ���󷽷�������Cell��ֵ
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected abstract void SetCellValue();


		/// <summary>
        /// ���ⷽ������ʾ�༭�ؼ�
		/// Displays the editor to the user and adds it to the Table's Control
		/// collection
		/// </summary>
		protected virtual void ShowEditControl()
		{
			this.control.Parent = this.table;

			this.control.Visible = true;
		}


		/// <summary>
        /// ���ⷽ�������ر༭�ؼ�
		/// Conceals the editor from the user, but does not remove it from the 
		/// Table's Control collection
		/// </summary>
		protected virtual void HideEditControl()
		{
			this.control.Visible = false;
		}


		/// <summary>
        /// ���ⷽ������Table���Ƴ��༭�ؼ�
		/// Conceals the editor from the user and removes it from the Table's 
		/// Control collection
		/// </summary>
		protected virtual void RemoveEditControl()
		{
			this.control.Visible = false;
			this.control.Parent = null;

			this.table.Focus();

			this.cell = null;
			this.table = null;
			this.cellPos = I3CellPos.Empty;
			this.cellRect = Rectangle.Empty;
		}


		/// <summary>
        /// ���ⷽ������ʼ�༭����ICellEditor�ж���
		/// Starts editing the Cell
		/// </summary>
        public virtual void StartEditing()
        {
            this.ShowEditControl();

            Application.AddMessageFilter(this.keyMessageFilter);
            Application.AddMessageFilter(this.mouseMessageFilter);
        }


		/// <summary>
        /// ���ⷽ���������༭����ICellEditor�ж���
		/// Stops editing the Cell and commits any changes
		/// </summary>
        public virtual void StopEditing()
        {
            Application.RemoveMessageFilter(this.keyMessageFilter);
            Application.RemoveMessageFilter(this.mouseMessageFilter);

            //
            I3CellEditEventArgs e = new I3CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

            this.OnEndEdit(e);

            if (!e.Cancel && !e.Handled)
            {
                this.SetCellValue();
            }
            this.table.OnEditingStopped(e);

            this.RemoveEditControl();
        }


		/// <summary>
        /// ���ⷽ�����˳��༭����ICellEditor�ж���
		/// Stops editing the Cell and ignores any changes
		/// </summary>
        public virtual void CancelEditing()
        {
            Application.RemoveMessageFilter(this.keyMessageFilter);
            Application.RemoveMessageFilter(this.mouseMessageFilter);

            //
            I3CellEditEventArgs e = new I3CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

            this.table.OnEditingCancelled(e);
            this.OnCancelEdit(e);

            this.RemoveEditControl();
        }


		/// <summary>
        /// ���ⷽ�������������Ϣ
		/// Filters out a mouse message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessMouseMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			if (msg == I3WindowMessage.WM_LBUTTONDOWN || msg == I3WindowMessage.WM_RBUTTONDOWN || 
				msg == I3WindowMessage.WM_MBUTTONDOWN || msg == I3WindowMessage.WM_XBUTTONDOWN || 
				msg == I3WindowMessage.WM_NCLBUTTONDOWN || msg == I3WindowMessage.WM_NCRBUTTONDOWN || 
				msg == I3WindowMessage.WM_NCMBUTTONDOWN || msg == I3WindowMessage.WM_NCXBUTTONDOWN)
			{	
				Point cursorPos = Cursor.Position;
				
				if (target != this.EditingTable && target != this.Control)
				{
					this.EditingTable.StopEditing();
				}
			}
			
			return false;
		}


		/// <summary>
        /// ���ⷽ�������˼�����Ϣ
		/// Filters out a key message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessKeyMessage(Control target, I3WindowMessage msg, int wParam, int lParam)
		{
			return false;
		}

		#endregion


		#region Properties

		/// <summary>
        /// ��ȡ������Control
		/// Gets or sets the Control that is being used to edit the Cell
		/// </summary>
		protected Control Control
		{
			get
			{
				return this.control;
			}

			set
			{
				this.control = value;
			}
		}


		/// <summary>
        /// ��ȡ���༭��Cell
		/// Gets the Cell that is being edited
		/// </summary>
		public I3Cell EditingCell
		{
			get
			{
				return this.cell;
			}
		}


		/// <summary>
        /// ��ȡ���༭��Table
		/// Gets the Table that contains the Cell being edited
		/// </summary>
        public I3Table EditingTable
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
        /// ��ȡ���༭��Cell��λ��
		/// Gets a CellPos that represents the position of the Cell being edited
		/// </summary>
		public I3CellPos EditingCellPos
		{
			get
			{
				return this.cellPos;
			}
		}


		/// <summary>
        /// ��ȡ��ǰ�Ƿ����ڱ༭һ��Cell
		/// Gets whether the CellEditor is currently editing a Cell
		/// </summary>
		public bool IsEditing
		{
			get
			{
				return this.cell != null;
			}
		}

		#endregion


		#region Events

		/// <summary>
        /// ������ʼ�༭�¼�
		/// Raises the BeginEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnBeginEdit(I3CellEditEventArgs e)
		{
			if (this.BeginEdit != null)
			{
				this.BeginEdit(this, e);
			}
		}


		/// <summary>
        /// ���������༭�¼�
		/// Raises the EndEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnEndEdit(I3CellEditEventArgs e)
		{
			if (this.EndEdit != null)
			{
				this.EndEdit(this, e);
			}
		}


		/// <summary>
        /// ����ȡ���༭�¼�
		/// Raises the CancelEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnCancelEdit(I3CellEditEventArgs e)
		{
			if (this.CancelEdit != null)
			{
				this.CancelEdit(this, e);
			}
		}

		#endregion


        public virtual Control GetDataInputControl()
        {
            return null;
        }
    }
}
