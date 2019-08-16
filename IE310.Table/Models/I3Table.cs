using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using IE310.Table.Editors;
using IE310.Table.Events;
using IE310.Table.Models;
using IE310.Table.Renderers;
using IE310.Table.Sorting;
using IE310.Table.Themes;
using IE310.Table.Win32;
using IE310.Table.Cell;
using IE310.Table.Header;
using IE310.Table.Column;
using IE310.Table.Row;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using IE310.Table.Design;
using System.Collections.Generic;


namespace IE310.Table.Models
{
    /// <summary>
    /// Summary description for Table.
    /// </summary>
    [DesignTimeVisible(true),
    ToolboxItem(true),
    ToolboxBitmap(typeof(I3Table))]
    public class I3Table : Control, ISupportInitialize
    {
        #region Event Handlers

        #region Cells

        /// <summary>
        /// Cell���Ըı��¼�
        /// Occurs when the value of a Cells property changes
        /// </summary>
        public event I3CellEventHandler CellPropertyChanged;

        #region Focus

        /// <summary>
        /// Cell��ý����¼�
        /// Occurs when a Cell gains focus
        /// </summary>
        public event I3CellFocusEventHandler CellGotFocus;

        /// <summary>
        /// Cellʧȥ�����¼�
        /// Occurs when a Cell loses focus
        /// </summary>
        public event I3CellFocusEventHandler CellLostFocus;

        #endregion

        #region Keys

        /// <summary>
        /// Cell���̰����¼�
        /// Occurs when a key is pressed when a Cell has focus
        /// </summary>
        public event I3CellKeyEventHandler CellKeyDown;

        /// <summary>
        /// Cell���̵����¼�
        /// Occurs when a key is released when a Cell has focus
        /// </summary>
        public event I3CellKeyEventHandler CellKeyUp;

        #endregion

        #region Mouse

        /// <summary>
        /// Cell�������¼�
        /// Occurs when the mouse pointer enters a Cell
        /// </summary>
        public event I3CellMouseEventHandler CellMouseEnter;

        /// <summary>
        /// Cell����뿪�¼�
        /// Occurs when the mouse pointer leaves a Cell
        /// </summary>
        public event I3CellMouseEventHandler CellMouseLeave;

        /// <summary>
        /// Cell��갴���¼�
        /// Occurs when a mouse pointer is over a Cell and a mouse button is pressed
        /// </summary>
        public event I3CellMouseEventHandler CellMouseDown;

        /// <summary>
        /// Cell��굯���¼�
        /// Occurs when a mouse pointer is over a Cell and a mouse button is released
        /// </summary>
        public event I3CellMouseEventHandler CellMouseUp;

        /// <summary>
        /// Cell����ƶ��¼�
        /// Occurs when a mouse pointer is moved over a Cell
        /// </summary>
        public event I3CellMouseEventHandler CellMouseMove;

        /// <summary>
        /// Cell�����ͣ�¼�
        /// Occurs when the mouse pointer hovers over a Cell
        /// </summary>
        public event I3CellMouseEventHandler CellMouseHover;

        /// <summary>
        /// Cell����¼�
        /// Occurs when a Cell is clicked
        /// </summary>
        public event I3CellMouseEventHandler CellClick;

        /// <summary>
        /// Cell˫���¼�
        /// Occurs when a Cell is double-clicked
        /// </summary>
        public event I3CellMouseEventHandler CellDoubleClick;

        #endregion


        #region CheckBox

        /// <summary>
        /// Cell������CheckBox�ı��¼�
        /// Occurs when a Cell's Checked value changes
        /// </summary>
        public event I3CellCheckBoxEventHandler CellCheckChanged;

        #endregion

        #endregion

        #region Column

        /// <summary>
        /// �����Ըı��¼�
        /// Occurs when a Column's property changes
        /// </summary>
        public event I3ColumnEventHandler ColumnPropertyChanged;

        #endregion

        #region Column/Row Headers

        /// <summary>
        /// ColumnHearder�������¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseEnter;

        /// <summary>
        /// ColumnHearder����뿪�¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseLeave;

        /// <summary>
        /// ColumnHearder��갴���¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseDown;

        /// <summary>
        /// ColumnHearder��굯���¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseUp;

        /// <summary>
        /// ColumnHearder����ƶ��¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseMove;

        /// <summary>
        /// ColumnHearder�����ͣ�¼�
        /// Occurs when the mouse pointer hovers over a Column Header
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderMouseHover;

        /// <summary>
        /// ColumnHearder����¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderClick;

        /// <summary>
        /// ColumnHearder˫���¼�
        /// </summary>
        public event I3ColumnHeaderMouseEventHandler ColumnHeaderDoubleClick;

        /// <summary>
        /// ��ͷ�߶ȸı��¼�
        /// </summary>
        public event EventHandler ColumnHeaderHeightChanged;

        /// <summary>
        /// RowHearder�������¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseEnter;

        /// <summary>
        /// RowHearder����뿪�¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseLeave;

        /// <summary>
        /// RowHearder��갴���¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseDown;

        /// <summary>
        /// RowHearder��굯���¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseUp;

        /// <summary>
        /// RowHearder����ƶ��¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseMove;

        /// <summary>
        /// RowHearder�����ͣ�¼�
        /// Occurs when the mouse pointer hovers over a Column Header
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderMouseHover;

        /// <summary>
        /// RowHearder����¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderClick;

        /// <summary>
        /// RowHearder˫���¼�
        /// </summary>
        public event I3RowHeaderMouseEventHandler RowHeaderDoubleClick;

        /// <summary>
        /// ��ͷ��ȸı��¼�
        /// </summary>
        public event EventHandler RowHeaderWidthChanged;

        #endregion

        #region ColumnModel

        /// <summary>
        /// �м��ϸı��¼�
        /// Occurs when the value of the Table's ColumnModel property changes 
        /// </summary>
        public event EventHandler ColumnModelChanged;

        /// <summary>
        /// �������¼�
        /// Occurs when a Column is added to the ColumnModel
        /// </summary>
        public event I3ColumnModelEventHandler ColumnAdded;

        /// <summary>
        /// �Ƴ����¼�
        /// Occurs when a Column is removed from the ColumnModel
        /// </summary>
        public event I3ColumnModelEventHandler ColumnRemoved;

        #endregion

        #region Editing

        /// <summary>
        /// Cell��ʼ�༭�¼�
        /// Occurs when the Table begins editing a Cell
        /// </summary>
        public event I3CellEditEventHandler BeginEditing;

        /// <summary>
        /// Cell�����༭�¼�
        /// Occurs when the Table stops editing a Cell
        /// </summary>
        public event I3CellEditEventHandler EditingStopped;

        /// <summary>
        /// Cell�˳��༭�¼�
        /// Occurs when the editing of a Cell is cancelled
        /// </summary>
        public event I3CellEditEventHandler EditingCancelled;

        #endregion

        #region Rows

        /// <summary>
        /// Row�����Cell�¼�
        /// Occurs when a Cell is added to a Row
        /// </summary>
        public event I3RowEventHandler CellAdded;

        /// <summary>
        /// Row���Ƴ�Cell�¼�
        /// Occurs when a Cell is removed from a Row
        /// </summary>
        public event I3RowEventHandler CellRemoved;

        /// <summary>
        /// Row���Ըı��¼�
        /// Occurs when the value of a Rows property changes
        /// </summary>
        public event I3RowEventHandler RowPropertyChanged;

        #endregion

        #region Sorting

        /// <summary>
        /// �п�ʼ�����¼�
        /// Occurs when a Column is about to be sorted
        /// </summary>
        public event I3ColumnEventHandler BeginSort;

        /// <summary>
        /// �н��������¼�
        /// Occurs after a Column has finished sorting
        /// </summary>
        public event I3ColumnEventHandler EndSort;

        #endregion

        #region Painting

        /// <summary>
        /// Cell��ʼPaintǰ�¼�
        /// Occurs before a Cell is painted
        /// </summary>
        public event I3PaintCellEventHandler BeforePaintCell;

        /// <summary>
        /// Cell Paint���¼�
        /// Occurs after a Cell is painted
        /// </summary>
        public event I3PaintCellEventHandler AfterPaintCell;

        /// <summary>
        /// ��Header��ʼ����ǰ�¼�
        /// </summary>
        public event I3PaintColumnHeaderEventHandler BeforePaintColumnHeader;

        /// <summary>
        /// ��Header ���ƺ��¼�
        /// </summary>
        public event I3PaintColumnHeaderEventHandler AfterPaintColumnHeader;

        /// <summary>
        /// ��Header��ʼ����ǰ�¼�
        /// </summary>
        public event I3PatinRowHeaderEventHandler BeforePaintRowHeader;

        /// <summary>
        /// ��Header ���ƺ��¼�
        /// </summary>
        public event I3PatinRowHeaderEventHandler AfterPaintRowHeader;

        #endregion

        #region TableModel

        /// <summary>
        /// TableModel�ı��¼�
        /// Occurs when the value of the Table's TableModel property changes 
        /// </summary>
        public event EventHandler TableModelChanged;

        /// <summary>
        /// TableModel���������¼�
        /// Occurs when a Row is added into the TableModel
        /// </summary>
        public event I3TableModelEventHandler RowAdded;

        /// <summary>
        /// TableModel���Ƴ����¼�
        /// Occurs when a Row is removed from the TableModel
        /// </summary>
        public event I3TableModelEventHandler RowRemoved;

        /// <summary>
        /// TableModel�е�ǰѡ����ı��¼�
        /// Occurs when the value of the TableModel Selection property changes
        /// </summary>
        public event I3SelectionEventHandler SelectionChanged;

        /// <summary>
        /// TableModel���и߸ı��¼�
        /// Occurs when the value of the RowHeight property changes
        /// </summary>
        public event EventHandler RowHeightChanged;

        #endregion

        #endregion


        #region Class Data

        /// <summary>
        /// �ƶ���ʱ�Ŀ����
        /// </summary>
        private Rectangle columnMoveRect;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #region Border

        /// <summary>
        /// �߿�ķ��
        /// The style of the Table's border
        /// </summary>
        private BorderStyle borderStyle;

        #endregion

        #region Cells

        /// <summary>
        /// �����󾭹���Cell��λ��
        /// The last known cell position that the mouse was over
        /// </summary>
        private I3CellPos lastMouseCell;

        /// <summary>
        /// �������µ�Cell��λ��
        /// The last known cell position that the mouse's left 
        /// button was pressed in
        /// </summary>
        private I3CellPos lastMouseDownCell;

        /// <summary>
        /// ��ǰFocus��Cell��λ��
        /// The position of the Cell that currently has focus
        /// </summary>
        private I3CellPos focusedCell;

        /// <summary>
        /// ��ǰ�༭��Cell��λ��
        /// The Cell that is currently being edited
        /// </summary>
        private I3CellPos editingCell;

        /// <summary>
        /// ��ǰ�༭��Cell��ICellEditor
        /// The ICellEditor that is currently being used to edit a Cell
        /// </summary>
        private II3CellEditor curentCellEditor;

        /// <summary>
        /// ��ʼ�༭�������
        /// The action that must be performed on a Cell to start editing
        /// </summary>
        private I3EditStartAction editStartAction;

        /// <summary>
        /// ���б༭���Զ����ȼ�
        /// The key that must be pressed for editing to start when 
        /// editStartAction is set to EditStartAction.CustomKey
        /// </summary>
        private Keys customEditKey;

        /// <summary>
        /// �ǻ��೤ʱ���������ͣ�¼�
        /// The amount of time (in milliseconds) that that the 
        /// mouse pointer must hover over a Cell or Column Header before 
        /// a MouseHover event is raised
        /// </summary>
        private int hoverTime;

        /// <summary>
        /// ���׷���¼���
        /// A TRACKMOUSEEVENT used to set the hoverTime
        /// </summary>
        private I3TRACKMOUSEEVENT trackMouseEvent;

        #endregion

        #region Columns

        /// <summary>
        /// �Ƿ�����ƶ���
        /// </summary>
        private bool canMoveColumn;

        private I3ColumnHeaderDisplayMode columnHeaderDisplayMode = I3ColumnHeaderDisplayMode.Text;

        /// <summary>
        /// �м���
        /// The ColumnModel of the Table
        /// </summary>
        private I3ColumnModel columnModel;

        /// <summary>
        /// ���Ƿ���Զ�����
        /// Whether the Table supports column resizing
        /// </summary>
        private bool columnResizing;

        private bool extendLastCol;

        /// <summary>
        /// ��ǰ���ض����ȵ���
        /// The index of the column currently being resized
        /// </summary>
        private int resizingColumnIndex;



        /// <summary>
        /// ��ǰ���ض����ȵ��е�Left
        /// The x coordinate of the currently resizing column
        /// </summary>
        private int resizingColumnAnchor;

        /// <summary>
        /// ��ǰ���ض����ȵ��иı�Ŀ��
        /// The horizontal distance between the resize starting
        /// point and the right edge of the resizing column
        /// </summary>
        private int resizingColumnOffset;

        /// <summary>
        /// ��ǰ���ض����ȵ��м������еĿ��
        /// The width that the resizing column will be set to 
        /// once column resizing is finished
        /// </summary>
        private int resizingColumnWidth;

        /// <summary>
        /// ��ǰ���µ���
        /// The index of the current pressed column
        /// </summary>
        private int pressedColumn;

        /// <summary>
        /// ��ǰ��hot��
        /// </summary>
        private int hotColumn;


        /// <summary>
        /// ����������
        /// The index of the last sorted column
        /// </summary>
        private int lastSortedColumn;

        /// <summary>
        /// ������еı���ɫ
        /// The Color of a sorted Column's background
        /// </summary>
        //private Color sortedColumnBackColor;

        private int frozenColumnCount;

        #endregion

        #region Rows
        private I3RowHeaderDisplayMode rowHeaderDisplayMode = I3RowHeaderDisplayMode.Num;

        /// <summary>
        /// ���Ƿ���Զ�����
        /// </summary>
        private bool rowResizing;
        /// <summary>
        /// ��ǰ���ض���߶ȵ���
        /// </summary>
        private int resizingRowIndex;
        /// <summary>
        /// ��ǰ���ض����ȵ��е�Top
        /// </summary>
        private int resizingRowAnchor;
        /// <summary>
        /// ��ǰ���ض���߶ȵ��иı�ĸ߶�
        /// </summary>
        private int resizingRowOffset;
        /// <summary>
        /// ��ǰ���ض����ȵ��м������еĸ߶�
        /// </summary>
        private int resizingRowHeight;
        /// <summary>
        /// ��ǰ���µ���
        /// </summary>
        private int pressedRow;
        /// <summary>
        /// ��ǰ��hot��
        /// </summary>
        private int hotRow;

        #endregion

        #region Grid

        /// <summary>
        /// �Ƿ���ʾ������
        /// Indicates whether grid lines appear between the rows and columns 
        /// containing the rows and cells in the Table
        /// </summary>
        private I3GridLines gridLines;

        /// <summary>
        /// �����ߵ���ɫ
        /// The color of the grid lines
        /// </summary>
        private Color gridColor;

        /// <summary>
        /// �����ߵķ��
        /// The line style of the grid lines
        /// </summary>
        private I3GridLineStyle gridLineStyle;

        #endregion

        #region Header

        /// <summary>
        /// ��ͷ��ʽ
        /// The styles of the column headers 
        /// </summary>
        private ColumnHeaderStyle _columnHeaderStyle;


        /// <summary>
        /// ��ʶ��ͷ�Ƿ����
        /// </summary>
        private bool _rowHeaderVisible;

        /// <summary>
        /// ��ͷ����Ⱦ��
        /// The Renderer used to paint the column headers
        /// </summary>
        private I3HeaderRenderer _headerRenderer;

        /// <summary>
        /// ��ͷ������
        /// The font used to draw the text in the column header
        /// </summary>
        private Font headerFont;

        /// <summary>
        /// ��ͷ�ĵ����˵�
        /// The ContextMenu for the column headers
        /// </summary>
        private I3ColumnHeaderContextMenu _columnHeaderContextMenu;

        #endregion

        #region Items

        /// <summary>
        /// �м���
        /// The TableModel of the Table
        /// </summary>
        private I3TableModel tableModel;

        private int frozenRowCount;

        #endregion

        #region Scrollbars

        /// <summary>
        /// �Ƿ�ʹ�ù�����
        /// Indicates whether the Table will allow the user to scroll to any 
        /// columns or rows placed outside of its visible boundaries
        /// </summary>
        private bool scrollable;

        /// <summary>
        /// ˮƽ������
        /// The Table's horizontal ScrollBar
        /// </summary>
        private HScrollBar hScrollBar;

        /// <summary>
        /// ��ֱ������
        /// The Table's vertical ScrollBar
        /// </summary>
        private VScrollBar vScrollBar;

        #endregion

        #region Selection

        /// <summary>
        /// �С����Ƿ�ɱ�ѡ��
        /// Specifies whether rows and cells can be selected
        /// </summary>
        private bool allowSelection;

        /// <summary>
        /// �Ƿ�ɶ�ѡ
        /// Specifies whether multiple rows and cells can be selected
        /// </summary>
        private bool multiSelect;

        private bool selectByRightButton;

        /// <summary>
        /// ����һ����ʱ���Ƿ�ѡ�����е���
        /// Specifies whether clicking a row selects all its cells
        /// </summary>
        private bool fullRowSelect;

        /// <summary>
        /// Tabelʧȥ����ʱ���Ƿ�����ѡ����
        /// Specifies whether the selected rows and cells in the Table remain 
        /// highlighted when the Table loses focus
        /// </summary>
        private bool hideSelection;

        /// <summary>
        /// ѡ����ı���ɫ
        /// The background color of selected rows and cells
        /// </summary>
        private Color selectionBackColor;

        /// <summary>
        /// ѡ�����ǰ��ɫ
        /// The foreground color of selected rows and cells
        /// </summary>
        private Color selectionForeColor;

        /// <summary>
        /// ����Focus��ѡ����ı���ɫ
        /// The background color of selected rows and cells when the Table 
        /// doesn't have focus
        /// </summary>
        private Color unfocusedSelectionBackColor;

        /// <summary>
        /// ����Focus��ѡ�����ǰ��ɫ
        /// The foreground color of selected rows and cells when the Table 
        /// doesn't have focus
        /// </summary>
        private Color unfocusedSelectionForeColor;

        /// <summary>
        /// ѡ�������ʽ
        /// Determines how selected Cells are hilighted
        /// </summary>
        private I3SelectionStyle selectionStyle;

        #endregion

        #region Table

        /// <summary>
        /// Table��״̬
        /// The state of the table
        /// </summary>
        private I3TableState tableState;

        /// <summary>
        /// Table��ǰ�Ƿ����ڽ��г�ʼ��
        /// Is the Table currently initialising
        /// </summary>
        private bool init;

        /// <summary>
        /// BeginUpdate�Ѿ������õĴ���
        /// The number of times BeginUpdate has been called
        /// </summary>
        private int beginUpdateCount;

        /// <summary>
        /// ���˵������
        /// The ToolTip used by the Table to display cell and column tooltips
        /// </summary>
        private ToolTip toolTip;

        /// <summary>
        /// �����е���ɫ
        /// The alternating row background color
        /// </summary>
        private Color alternatingRowColor;

        /// <summary>
        /// Table����������ʾʱ����ʾ����
        /// The text displayed in the Table when it has no data to display
        /// </summary>
        private string noItemsText;

        /// <summary>
        /// ��ʶTable�Ƿ����м��ϱ༭�ĵ�Ԥ����ͼģʽ
        /// Specifies whether the Table is being used as a preview Table 
        /// in a ColumnColection editor
        /// </summary>
        private bool preview;

        /*/// <summary>
        /// Specifies whether pressing the Tab key while editing moves the 
        /// editor to the next available cell
        /// </summary>
        private bool tabMovesEditor;*/

        #endregion

        private object userData;

        public object UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }


        #endregion


        #region Constructor

        /// <summary>
        /// ���캯�� 
        /// Initializes a new instance of the Table class with default settings
        /// </summary>
        public I3Table()
        {
            // starting setup
            this.init = true;

            // Ϊ�����׼��
            // This call is required by the Windows.Forms Form Designer.
            components = new System.ComponentModel.Container();

            //ʹ���Ի滭��˫����ģʽ
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            columnMoveRect = Rectangle.Empty;

            //����Ĭ�ϴ�С
            this.Size = new Size(150, 150);

            //����Ĭ�ϱ���ɫ
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            //�м��ϡ��м��϶�Ϊ��
            this.columnModel = null;
            this.tableModel = null;
            this.canMoveColumn = true;

            // ����Ĭ�ϵ��е���ʽ
            // header
            this._columnHeaderStyle = ColumnHeaderStyle.Clickable;
            this.headerFont = this.Font;
            //if (!this.headerFont.Bold)
            //{
            //    this.headerFont = new Font(this.headerFont, this.headerFont.Style | FontStyle.Bold);
            //}
            //this.headerRenderer = new XPHeaderRenderer();
            this._headerRenderer = new I3GradientHeaderRenderer();
            //this.headerRenderer = new FlatHeaderRenderer();
            this._headerRenderer.Font = this.headerFont;
            this._columnHeaderContextMenu = new I3ColumnHeaderContextMenu();

            this._rowHeaderVisible = true;

            //�����п�����õȱ�����Ĭ��ֵ
            this.columnResizing = true;
            this.extendLastCol = false;
            this.resizingColumnIndex = -1;
            this.resizingColumnWidth = -1;
            this.hotColumn = -1;
            this.pressedColumn = -1;
            this.lastSortedColumn = -1;
            //this.sortedColumnBackColor = Color.WhiteSmoke;
            this.frozenColumnCount = 0;

            //
            this.rowResizing = false;
            this.resizingRowIndex = -1;
            this.resizingRowHeight = -1;
            this.hotRow = -1;
            this.pressedRow = -1;
            this.frozenRowCount = 0;

            //���ñ߿���ʽ
            // borders
            this.borderStyle = BorderStyle.Fixed3D;

            //�����Ƿ�ɹ���
            // scrolling
            this.scrollable = true;

            //����ˮƽ������
            this.hScrollBar = new HScrollBar();
            this.hScrollBar.Visible = false;
            this.hScrollBar.Location = new Point(this.BorderWidth, this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight);
            this.hScrollBar.Width = this.Width - (this.BorderWidth * 2) - SystemInformation.VerticalScrollBarWidth;
            this.hScrollBar.Scroll += new ScrollEventHandler(this.OnHorizontalScroll);
            this.Controls.Add(this.hScrollBar);

            //������ֱ������
            this.vScrollBar = new VScrollBar();
            this.vScrollBar.Visible = false;
            this.vScrollBar.Location = new Point(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth, this.BorderWidth);
            this.vScrollBar.Height = this.Height - (this.BorderWidth * 2) - SystemInformation.HorizontalScrollBarHeight;
            this.vScrollBar.Scroll += new ScrollEventHandler(this.OnVerticalScroll);
            this.Controls.Add(this.vScrollBar);

            //����������
            this.gridLines = I3GridLines.Both;
            this.gridColor = SystemColors.ActiveCaption;
            this.gridLineStyle = I3GridLineStyle.Solid;

            //����Selection�� 
            this.allowSelection = true;
            this.multiSelect = false;
            this.fullRowSelect = false;
            this.hideSelection = false;
            this.selectionBackColor = SystemColors.Highlight;
            this.selectionForeColor = SystemColors.HighlightText;
            this.unfocusedSelectionBackColor = SystemColors.Highlight;
            this.unfocusedSelectionForeColor = SystemColors.HighlightText;
            this.selectionStyle = I3SelectionStyle.Grid;
            this.alternatingRowColor = Color.Transparent;

            //����Table״̬
            // current table state
            this.tableState = I3TableState.Normal;

            //����lastMouseCell��
            this.lastMouseCell = new I3CellPos(-1, -1);
            this.lastMouseDownCell = new I3CellPos(-1, -1);
            this.focusedCell = new I3CellPos(-1, -1);
            this.hoverTime = 1000;
            this.trackMouseEvent = null;
            this.ResetMouseEventArgs();

            //����toolTip
            this.toolTip = new ToolTip(this.components);
            this.toolTip.Active = false;
            this.toolTip.InitialDelay = 1000;//��ʾǰ�ȴ�ʱ��

            //������������ʾ�ı�
            this.noItemsText = "��������ʾ";

            //����editing��Ϣ
            this.editingCell = new I3CellPos(-1, -1);
            this.curentCellEditor = null;
            this.editStartAction = I3EditStartAction.DoubleClick_DataInputKey;
            this.customEditKey = Keys.F5;
            //this.tabMovesEditor = true;

            //������ʼ��
            // finished setting up
            this.beginUpdateCount = 0;
            this.init = false;
            this.preview = false;
        }

        #endregion


        #region Methods

        #region ����ת�� Coordinate Translation ����������-->��������ʱ��Ҫ�ȼ����߿򡢹���������ͷ����ͷ����������ֻ��������������

        #region �����������꣨����ڿؼ����Ͻǵ����꣩ת��Ϊ�������꣨����ڻ�����ʼ������꣬������ʼ�����Ϊ��ֵ��ClientToDisplayRect

        /// <summary>
        /// �����������꣨����ڿؼ����Ͻǵ����꣩ת��Ϊ�������꣨����ڻ�����ʼ������꣬������ʼ�����Ϊ��ֵ��
        /// Computes the location of the specified client point into coordinates 
        /// relative to the display rectangle
        /// </summary>
        /// <param name="x">The client x coordinate to convert</param>
        /// <param name="y">The client y coordinate to convert</param>
        /// <returns>A Point that represents the converted coordinates (x, y), 
        /// relative to the display rectangle</returns>
        public Point ClientToDisplay(int x, int y)
        {
            int xPos = x - this.BorderWidth - this.RowHeaderWidth;
            for (int i = 0; i <= this.frozenColumnCount - 1; i++)
            {
                if (this.columnModel.Columns[i].Visible)
                {
                    xPos -= this.columnModel.Columns[i].Width;
                }
            }
            if (this.HScroll)
            {
                xPos += this.hScrollBar.Value;
            }

            int yPos = y - this.BorderWidth - this.ColumnHeaderHeight;
            if (this.VScroll)
            {
                //yPos += this.TopIndex * this.RowHeight;
                for (int i = 0; i <= this.TopIndex - 1; i++)
                {
                    if (this.TableModel.Rows[i].Visible)
                    {
                        yPos = yPos + this.TableModel.Rows[i].Height;
                    }
                }
            }

            return new Point(xPos, yPos);
        }


        /// <summary>
        /// �����������꣨����ڿؼ����Ͻǵ����꣩ת��Ϊ�������꣨����ڻ�����ʼ������꣬������ʼ�����Ϊ��ֵ��
        /// Computes the location of the specified client point into coordinates 
        /// relative to the display rectangle
        /// </summary>
        /// <param name="p">The client coordinate Point to convert</param>
        /// <returns>A Point that represents the converted Point, p, 
        /// relative to the display rectangle</returns>
        public Point ClientToDisplay(Point p)
        {
            return this.ClientToDisplay(p.X, p.Y);
        }


        /// <summary>
        /// �����������꣨����ڿؼ����Ͻǵ����꣩ת��Ϊ�������꣨����ڻ�����ʼ������꣬������ʼ�����Ϊ��ֵ��
        /// Converts the location of the specified Rectangle into coordinates 
        /// relative to the display rectangle
        /// </summary>
        /// <param name="rect">The Rectangle to convert whose location is in 
        /// client coordinates</param>
        /// <returns>A Rectangle that represents the converted Rectangle, rect, 
        /// relative to the display rectangle</returns>
        public Rectangle ClientToDisplay(Rectangle rect)
        {
            return new Rectangle(this.ClientToDisplay(rect.Location), rect.Size);
        }

        #endregion

        #region ����������ת��Ϊ���������� DisplayRectToClient

        /// <summary>
        /// ����������ת��Ϊ����������
        /// Computes the location of the specified point relative to the display 
        /// rectangle point into client coordinates 
        /// </summary>
        /// <param name="x">The x coordinate to convert relative to the display rectangle</param>
        /// <param name="y">The y coordinate to convert relative to the display rectangle</param>
        /// <returns>A Point that represents the converted coordinates (x, y) relative to 
        /// the display rectangle in client coordinates</returns>
        public Point DisplayToClient(int x, int y)
        {
            int xPos = x + this.BorderWidth + this.RowHeaderWidth;
            for (int i = 0; i <= this.frozenColumnCount - 1; i++)
            {
                if (this.columnModel.Columns[i].Visible)
                {
                    xPos += this.columnModel.Columns[i].Width;
                }
            }
            if (this.HScroll)
            {
                xPos -= this.hScrollBar.Value;
            }

            int yPos = y + this.BorderWidth + this.ColumnHeaderHeight;
            for (int i = 0; i <= this.frozenRowCount - 1; i++)
            {
                yPos += this.tableModel.Rows[i].Height;
            }
            if (this.VScroll)
            {
                //yPos -= this.TopIndex * this.RowHeight;
                for (int i = this.frozenRowCount; i <= this.TopIndex - 1; i++)
                {
                    if (this.TableModel.Rows[i].Visible)
                    {
                        yPos = yPos - this.TableModel.Rows[i].Height;
                    }
                }
            }

            return new Point(xPos, yPos);
        }


        /// <summary>
        /// ����������ת��Ϊ����������
        /// Computes the location of the specified point relative to the display 
        /// rectangle into client coordinates 
        /// </summary>
        /// <param name="p">The point relative to the display rectangle to convert</param>
        /// <returns>A Point that represents the converted Point relative to 
        /// the display rectangle, p, in client coordinates</returns>
        public Point DisplayToClient(Point p)
        {
            return this.DisplayToClient(p.X, p.Y);
        }


        /// <summary>
        /// ����������ת��Ϊ����������
        /// Converts the location of the specified Rectangle relative to the display 
        /// rectangle into client coordinates 
        /// </summary>
        /// <param name="rect">The Rectangle to convert whose location is relative to 
        /// the display rectangle</param>
        /// <returns>A Rectangle that represents the converted Rectangle relative to 
        /// the display rectangle, rect, in client coordinates</returns>
        public Rectangle DisplayToClient(Rectangle rect)
        {
            return new Rectangle(this.DisplayToClient(rect.Location), rect.Size);
        }

        #endregion

        #region Cells

        /// <summary>
        /// ���ع��������괦��Cell
        /// Returns the Cell at the specified client coordinates
        /// </summary>
        /// <param name="x">The client x coordinate of the Cell</param>
        /// <param name="y">The client y coordinate of the Cell</param>
        /// <returns>The Cell at the specified client coordinates, or
        /// null if it does not exist</returns>
        public I3Cell CellAtClient(int x, int y)
        {
            int row = this.RowIndexAtClient(x, y);
            int column = this.ColumnIndexAtClient(x, y);

            // return null if the row or column don't exist
            if (row == -1 || row >= this.TableModel.Rows.Count || column == -1 || column >= this.TableModel.Rows[row].Cells.Count)
            {
                return null;
            }

            return this.TableModel[row, column];
        }


        /// <summary>
        /// ���ع��������괦��Cell
        /// Returns the Cell at the specified client Point
        /// </summary>
        /// <param name="p">The point of interest</param>
        /// <returns>The Cell at the specified client Point, 
        /// or null if not found</returns>
        public I3Cell CellAtClient(Point p)
        {
            return this.CellAtClient(p.X, p.Y);
        }


        /// <summary>
        /// ����Cell�Ĺ�������������
        /// Returns a Rectangle that specifies the size and location the cell at 
        /// the specified row and column indexes in client coordinates
        /// </summary>
        /// <param name="row">The index of the row that contains the cell</param>
        /// <param name="column">The index of the column that contains the cell</param>
        /// <returns>A Rectangle that specifies the size and location the cell at 
        /// the specified row and column indexes in client coordinates</returns>
        public Rectangle CellClientRect(int row, int column)
        {
            // return null if the row or column don't exist
            if (row == -1 || row >= this.TableModel.Rows.Count || column == -1 || column >= this.TableModel.Rows[row].Cells.Count)
            {
                return Rectangle.Empty;
            }

            Rectangle columnRect = this.ColumnClientRect(column);

            if (columnRect == Rectangle.Empty)
            {
                return columnRect;
            }

            Rectangle rowRect = this.RowClientRect(row);

            if (rowRect == Rectangle.Empty)
            {
                return rowRect;
            }

            Rectangle result = new Rectangle(columnRect.X, rowRect.Y, columnRect.Width, rowRect.Height);
            return result;
        }


        /// <summary>
        /// ����Cell�Ĺ�������������
        /// Returns a Rectangle that specifies the size and location the cell at 
        /// the specified cell position in client coordinates
        /// </summary>
        /// <param name="cellPos">The position of the cell</param>
        /// <returns>A Rectangle that specifies the size and location the cell at 
        /// the specified cell position in client coordinates</returns>
        public Rectangle CellClientRect(I3CellPos cellPos)
        {
            return this.CellClientRect(cellPos.Row, cellPos.Column);
        }


        /// <summary>
        /// ����Cell�Ĺ�������������
        ///  Returns a Rectangle that specifies the size and location of the 
        ///  specified cell in client coordinates
        /// </summary>
        /// <param name="cell">The cell whose bounding rectangle is to be retrieved</param>
        /// <returns>A Rectangle that specifies the size and location the specified 
        /// cell in client coordinates</returns>
        public Rectangle CellClientRect(I3Cell cell)
        {
            if (cell == null || cell.Row == null || cell.InternalIndex == -1)
            {
                return Rectangle.Empty;
            }

            if (this.TableModel == null || this.ColumnModel == null)
            {
                return Rectangle.Empty;
            }

            int row = this.TableModel.Rows.IndexOf(cell.Row);
            int col = cell.InternalIndex;

            return this.CellClientRect(row, col);
        }


        /// <summary>
        /// ��֤Cell�Ƿ����
        /// Returns whether Cell at the specified row and column indexes 
        /// is not null
        /// </summary>
        /// <param name="row">The row index of the cell</param>
        /// <param name="column">The column index of the cell</param>
        /// <returns>True if the cell at the specified row and column indexes 
        /// is not null, otherwise false</returns>
        protected internal bool IsValidCell(int row, int column)
        {
            if (this.TableModel != null && this.ColumnModel != null)
            {
                if (row >= 0 && row < this.TableModel.Rows.Count)
                {
                    if (column >= 0 && column < this.ColumnModel.Columns.Count)
                    {
                        return (this.TableModel.Rows[row].Cells[column] != null);
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// ��֤Cell�Ƿ����
        /// Returns whether Cell at the specified cell position is not null
        /// </summary>
        /// <param name="cellPos">The position of the cell</param>
        /// <returns>True if the cell at the specified cell position is not 
        /// null, otherwise false</returns>
        protected internal bool IsValidCell(I3CellPos cellPos)
        {
            return this.IsValidCell(cellPos.Row, cellPos.Column);
        }


        /// <summary>
        /// ������һ���ɼ��ġ����õ�CellPos
        /// Returns a CellPos that specifies the next Cell that is visible 
        /// and enabled from the specified Cell
        /// </summary>
        /// <param name="start">A CellPos that specifies the Cell to start 
        /// searching from</param>
        /// <param name="wrap">Specifies whether to move to the start of the 
        /// next Row when the end of the current Row is reached</param>
        /// <param name="forward">Specifies whether the search should travel 
        /// in a forward direction (top to bottom, left to right) through the Cells</param>
        /// <param name="includeStart">Indicates whether the specified starting 
        /// Cell is included in the search</param>
        /// <param name="checkOtherCellsInRow">Specifies whether all Cells in 
        /// the Row should be included in the search</param>
        /// <returns>A CellPos that specifies the next Cell that is visible 
        /// and enabled, or CellPos.Empty if there are no Cells that are visible 
        /// and enabled</returns>
        protected I3CellPos FindNextVisibleEnabledCell(I3CellPos start, bool wrap, bool forward, bool includeStart, bool checkOtherCellsInRow)
        {
            if (this.ColumnCount == 0 || this.RowsCount == 0)
            {
                return I3CellPos.Empty;
            }

            int startRow = start.Row != -1 ? start.Row : 0;
            int startCol = start.Column != -1 ? start.Column : 0;

            bool first = true;

            if (forward)
            {
                for (int i = startRow; i < this.RowsCount; i++)
                {
                    int j = (first || !checkOtherCellsInRow ? startCol : 0);

                    for (; j < this.TableModel.Rows[i].Cells.Count; j++)
                    {
                        if (i == startRow && j == startCol)
                        {
                            if (!first)
                            {
                                return I3CellPos.Empty;
                            }

                            first = false;

                            if (!includeStart)
                            {
                                if (!checkOtherCellsInRow)
                                {
                                    break;
                                }

                                continue;
                            }
                        }

                        if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Enabled && this.ColumnModel.Columns[j].Enabled && this.ColumnModel.Columns[j].Visible)
                        {
                            return new I3CellPos(i, j);
                        }

                        if (!checkOtherCellsInRow)
                        {
                            continue;
                        }
                    }

                    if (wrap)
                    {
                        if (i + 1 == this.TableModel.Rows.Count)
                        {
                            i = -1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = startRow; i >= 0; i--)
                {
                    int j = (first || !checkOtherCellsInRow ? startCol : this.TableModel.Rows[i].Cells.Count);

                    for (; j >= 0; j--)
                    {
                        if (i == startRow && j == startCol)
                        {
                            if (!first)
                            {
                                return I3CellPos.Empty;
                            }

                            first = false;

                            if (!includeStart)
                            {
                                if (!checkOtherCellsInRow)
                                {
                                    break;
                                }

                                continue;
                            }
                        }

                        if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Enabled && this.ColumnModel.Columns[j].Enabled && this.ColumnModel.Columns[j].Visible)
                        {
                            return new I3CellPos(i, j);
                        }

                        if (!checkOtherCellsInRow)
                        {
                            continue;
                        }
                    }

                    if (wrap)
                    {
                        if (i - 1 == -1)
                        {
                            i = this.TableModel.Rows.Count;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return I3CellPos.Empty;
        }

        /// <summary>
        /// ������һ���ɱ༭��CellPos
        /// Returns a CellPos that specifies the next Cell that able to be 
        /// edited from the specified Cell
        /// </summary>
        /// <param name="start">A CellPos that specifies the Cell to start 
        /// searching from</param>
        /// <param name="wrap">Specifies whether to move to the start of the 
        /// next Row when the end of the current Row is reached</param>
        /// <param name="forward">Specifies whether the search should travel 
        /// in a forward direction (top to bottom, left to right) through the Cells</param>
        /// <param name="includeStart">Indicates whether the specified starting 
        /// Cell is included in the search</param>
        /// <returns>A CellPos that specifies the next Cell that is able to
        /// be edited, or CellPos.Empty if there are no Cells that editable</returns>
        protected I3CellPos FindNextEditableCell(I3CellPos start, bool wrap, bool forward, bool includeStart)
        {
            if (this.ColumnCount == 0 || this.RowsCount == 0)
            {
                return I3CellPos.Empty;
            }

            int startRow = start.Row != -1 ? start.Row : 0;
            int startCol = start.Column != -1 ? start.Column : 0;

            bool first = true;

            if (forward)
            {
                for (int i = startRow; i < this.RowsCount; i++)
                {
                    int j = (first ? startCol : 0);

                    for (; j < this.TableModel.Rows[i].Cells.Count; j++)
                    {
                        if (i == startRow && j == startCol)
                        {
                            if (!first)
                            {
                                return I3CellPos.Empty;
                            }

                            first = false;

                            if (!includeStart)
                            {
                                continue;
                            }
                        }

                        if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Editable && this.ColumnModel.Columns[j].Editable)
                        {
                            return new I3CellPos(i, j);
                        }
                    }

                    if (wrap)
                    {
                        if (i + 1 == this.TableModel.Rows.Count)
                        {
                            i = -1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = startRow; i >= 0; i--)
                {
                    int j = (first ? startCol : this.TableModel.Rows[i].Cells.Count);

                    for (; j >= 0; j--)
                    {
                        if (i == startRow && j == startCol)
                        {
                            if (!first)
                            {
                                return I3CellPos.Empty;
                            }

                            first = false;

                            if (!includeStart)
                            {
                                continue;
                            }
                        }

                        if (this.IsValidCell(i, j) && this.IsValidColumn(j) && this.TableModel[i, j].Editable && this.ColumnModel.Columns[j].Editable)
                        {
                            return new I3CellPos(i, j);
                        }
                    }

                    if (wrap)
                    {
                        if (i - 1 == -1)
                        {
                            i = this.TableModel.Rows.Count;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return I3CellPos.Empty;
        }

        #endregion

        #region Columns

        /// <summary>
        /// ����ָ�����������괦��Column
        /// Returns the index of the Column at the specified client coordinates
        /// </summary>
        /// <param name="x">The client x coordinate of the Column</param>
        /// <param name="y">The client y coordinate of the Column</param>
        /// <returns>The index of the Column at the specified client coordinates, or
        /// -1 if it does not exist</returns>
        public int ColumnIndexAtClient(int x, int y)
        {
            if (this.ColumnModel == null)
            {
                return -1;
            }

            int start = this.BorderWidth + this.RowHeaderWidth;
            for (int i = 0; i <= this.frozenColumnCount - 1; i++)
            {
                if (!this.columnModel.Columns[i].Visible)
                {
                    continue;
                }
                if (x < start + this.columnModel.Columns[i].Width)
                {
                    return i;
                }
                x += this.columnModel.Columns[i].Width;
            }

            Point displayPoint = this.ClientToDisplay(new Point(x, y));
            int result = this.ColumnModel.ColumnIndexAtDisplayX(displayPoint.X);
            if (result == -1 && this.extendLastCol)//ColumnModel.ColumnIndexAtDisplayX����ʱû�п���extendLastCol����
            {
                I3TableRegion region = this.HitTest(x, y);
                if (region == I3TableRegion.Cells || region == I3TableRegion.ColumnHeader)
                {
                    return this.columnModel.LastVisibleColumnIndex;
                }
            }
            return result;
        }


        /// <summary>
        /// ����ָ�����������괦��Column��Index
        /// Returns the index of the Column at the specified client point
        /// </summary>
        /// <param name="p">The point of interest</param>
        /// <returns>The index of the Column at the specified client point, or
        /// -1 if it does not exist</returns>
        public int ColumnIndexAtClient(Point p)
        {
            return this.ColumnIndexAtClient(p.X, p.Y);
        }


        /// <summary>
        /// ����Column��ColumnHeader�Ĺ�������������(����ͷ��˵����������Ϳͻ���������һ����)
        /// Returns the bounding rectangle of the specified 
        /// column's header in client coordinates
        /// </summary>
        /// <param name="column">The index of the column</param>
        /// <returns>The bounding rectangle of the specified 
        /// column's header</returns>
        public Rectangle ColumnHeaderClientRect(int column)
        {
            if (this.ColumnModel == null)
            {
                return Rectangle.Empty;
            }

            Rectangle rect = this.ColumnModel.ColumnHeaderDisplayRect(column);

            if (rect == Rectangle.Empty)
            {
                return rect;
            }

            rect = this.DisplayToClient(rect);
            rect.Y = this.BorderWidth;
            if (column == this.columnModel.LastVisibleColumnIndex && this.extendLastCol)
            {
                if (rect.Right < this.ClientRectWithOutBorder_ScrollBar_Header.Right)
                {
                    rect.Width = rect.Width + this.ClientRectWithOutBorder_ScrollBar_Header.Right - rect.Right;
                }
            }

            return rect;
        }


        /// <summary>
        /// ����Column��ColumnHeader�Ĺ�������������
        /// Returns the bounding rectangle of the specified 
        /// column's header in client coordinates
        /// </summary>
        /// <param name="column">The column</param>
        /// <returns>The bounding rectangle of the specified 
        /// column's header</returns>
        public Rectangle ColumnHeaderClientRect(I3Column column)
        {
            if (this.ColumnModel == null)
            {
                return Rectangle.Empty;
            }

            return this.ColumnHeaderClientRect(this.ColumnModel.Columns.IndexOf(column));
        }


        /// <summary>
        /// ����Column�Ĺ�������������
        /// Returns the bounding rectangle of the column at the 
        /// specified index in client coordinates
        /// </summary>
        /// <param name="column">The column</param>
        /// <returns>The bounding rectangle of the column at the 
        /// specified index</returns>
        public Rectangle ColumnClientRect(int column)
        {
            if (this.ColumnModel == null)
            {
                return Rectangle.Empty;
            }

            Rectangle rect = this.ColumnHeaderClientRect(column);

            if (rect == Rectangle.Empty)
            {
                return rect;
            }

            rect.Y += this.ColumnHeaderHeight;
            rect.Height = this.VisibleRowsHeight;

            return rect;
        }


        /// <summary>
        /// ����Column�Ĺ�������������
        /// Returns the bounding rectangle of the specified column 
        /// in client coordinates
        /// </summary>
        /// <param name="column">The column</param>
        /// <returns>The bounding rectangle of the specified 
        /// column</returns>
        public Rectangle ColumnClientRect(I3Column column)
        {
            if (this.ColumnModel == null)
            {
                return Rectangle.Empty;
            }

            return this.ColumnClientRect(this.ColumnModel.Columns.IndexOf(column));
        }

        #endregion

        #region Rows

        /// <summary>
        /// ��ȡָ���Ĺ��������괦��Row��Index
        /// Returns the index of the Row at the specified client coordinates
        /// </summary>
        /// <param name="x">The client x coordinate of the Row</param>
        /// <param name="y">The client y coordinate of the Row</param>
        /// <returns>The index of the Row at the specified client coordinates, or
        /// -1 if it does not exist</returns>
        public int RowIndexAtClient(int x, int y)
        {
            if (this.TableModel == null)
            {
                return -1;
            }

            //if (this.ColumnHeaderStyle != ColumnHeaderStyle.None)
            //{
            //    y -= this.ColumnHeaderHeight;
            //}

            //y -= this.BorderWidth;

            //if (y < 0)
            //{
            //    return -1;
            //}

            //if (this.VScroll)
            //{
            //    //y += this.TopIndex * this.RowHeight;
            //    for (int i = 0; i <= this.TopIndex - 1; i++)
            //    {
            //        y = y + this.TableModel.Rows[i].Height;
            //    }
            //}

            Point displayPoint = this.ClientToDisplay(new Point(x, y));

            return this.TableModel.RowIndexAtDisplayY(displayPoint.Y);
        }


        /// <summary>
        /// ��ȡָ���Ĺ��������괦��Row��Index
        /// Returns the index of the Row at the specified client point
        /// </summary>
        /// <param name="p">The point of interest</param>
        /// <returns>The index of the Row at the specified client point, or
        /// -1 if it does not exist</returns>
        public int RowIndexAtClient(Point p)
        {
            return this.RowIndexAtClient(p.X, p.Y);
        }




        /// <summary>
        /// ��ȡָ���е���ͷ�Ŀͻ�������
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Rectangle RowHeaderClientRect(int row)
        {
            if (this.TableModel == null)
            {
                return Rectangle.Empty;
            }

            Rectangle rect = this.RowClientRect(row);

            if (rect == Rectangle.Empty)
            {
                return rect;
            }

            rect.X = this.BorderWidth;
            rect.Width = this.RowHeaderWidth;
            return rect;
        }

        /// <summary>
        /// ��ȡָ���е���ͷ�Ŀͻ�������
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Rectangle RowHeaderClientRect(I3Row row)
        {
            if (this.TableModel == null)
            {
                return Rectangle.Empty;
            }

            return this.RowHeaderClientRect(this.TableModel.Rows.IndexOf(row));
        }

        /// <summary>
        /// ����Row�Ĺ�������������
        /// Returns the bounding rectangle of the row at the 
        /// specified index in client coordinates
        /// </summary>
        /// <param name="row">The index of the row</param>
        /// <returns>The bounding rectangle of the row at the 
        /// specified index</returns>
        public Rectangle RowClientRect(int row)
        {
            if (this.TableModel == null || this.ColumnModel == null || row == -1 || row > this.TableModel.Rows.Count)
            {
                return Rectangle.Empty;
            }

            Rectangle rect = new Rectangle();

            //rect.X = this.DisplayRectangle.X;
            rect.X = this.BorderWidth;
            if (this.RowHeaderVisible)
            {
                rect.X += this.RowHeaderWidth;
            }

            //rect.Y = this.BorderWidth + ((row - this.TopIndex) * this.RowHeight);
            rect.Y = this.BorderWidth;
            for (int i = this.TopIndex; i <= row - 1; i++)
            {
                rect.Y = rect.Y + this.TableModel.Rows[i].Height;
            }
            if (this.ColumnHeaderStyle != ColumnHeaderStyle.None)
            {
                rect.Y += this.ColumnHeaderHeight;
            }

            rect.Width = this.ColumnModel.VisibleColumnsWidth;
            if (rect.Right < this.ClientRectWithOutBorder_ScrollBar_Header.Right && this.extendLastCol)
            {
                rect.Width = rect.Width + this.ClientRectWithOutBorder_ScrollBar_Header.Right - rect.Right;
            }
            //rect.Height = this.RowHeight;
            rect.Height = this.TableModel.Rows[row].Height;


            return rect;
        }


        /// <summary>
        /// ����Row�Ĺ�������������
        /// </summary>
        /// <param name="row">The row</param>
        /// <returns>The bounding rectangle of the specified 
        /// row</returns>
        public Rectangle RowClientRect(I3Row row)
        {
            if (this.TableModel == null)
            {
                return Rectangle.Empty;
            }

            return this.RowClientRect(this.TableModel.Rows.IndexOf(row));
        }

        #endregion

        #region Hit Tests

        /// <summary>
        /// ����ָ���Ĺ��������򴦵� TableRegion(Table����)
        /// Returns a TableRegions value that represents the table region at 
        /// the specified client coordinates
        /// </summary>
        /// <param name="x">The client x coordinate</param>
        /// <param name="y">The client y coordinate</param>
        /// <returns>A TableRegions value that represents the table region at 
        /// the specified client coordinates</returns>
        public I3TableRegion HitTest(int x, int y)
        {
            if (this.RowHeaderVisible && this.RowColumnHeaderClientRectangle.Contains(x, y))
            {
                return I3TableRegion.RowColumnHeader;
            }
            else if (this.ColumnHeaderStyle != ColumnHeaderStyle.None && this.ColumnHeaderClientRectangle.Contains(x, y))
            {
                return I3TableRegion.ColumnHeader;
            }
            else if (this.RowHeaderVisible && this.RowHeaderClientRectangle.Contains(x, y))
            {
                return I3TableRegion.RowHeader;
            }
            else if (this.ClientRectWithOutBorder_ScrollBar_Header.Contains(x, y))
            {
                Rectangle rect = this.ClientRectWithOutBorder_ScrollBar_Header;
                rect.Height = this.VisibleRowsHeight;
                rect.Width = this.ColumnModel == null ? 0 : this.ColumnModel.VisibleColumnsWidth;
                if (rect.Right < this.ClientRectWithOutBorder_ScrollBar_Header.Right && this.extendLastCol)
                {
                    rect.Width = rect.Width + this.ClientRectWithOutBorder_ScrollBar_Header.Right - rect.Right;
                }
                if (rect.Contains(x, y))
                {
                    return I3TableRegion.Cells;
                }
                else
                {
                    return I3TableRegion.NonClientArea;
                }
            }
            else if (!this.Bounds.Contains(x, y))
            {
                return I3TableRegion.NoWhere;
            }

            return I3TableRegion.NonClientArea;
        }


        /// <summary>
        /// ����ָ���Ĺ��������򴦵� TableRegion(Table����)
        /// Returns a TableRegions value that represents the table region at 
        /// the specified client point
        /// </summary>
        /// <param name="p">The point of interest</param>
        /// <returns>A TableRegions value that represents the table region at 
        /// the specified client point</returns>
        public I3TableRegion HitTest(Point p)
        {
            return this.HitTest(p.X, p.Y);
        }

        #endregion

        #endregion

        #region Dispose

        /// <summary>
        /// Releases the unmanaged resources used by the Control and optionally 
        /// releases the managed resources
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged 
        /// resources; false to release only unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }


        /// <summary>
        /// Removes the ColumnModel and TableModel from the Table
        /// </summary>
        public void Clear()
        {
            if (this.ColumnModel != null)
            {
                this.ColumnModel = null;
            }

            if (this.TableModel != null)
            {
                this.TableModel = null;
            }
        }

        #endregion

        #region Editing

        /// <summary>
        /// ��¼��ǰ���༭��Cell����ʹ�õı༭��
        /// Records the Cell that is currently being edited and the 
        /// ICellEditor used to edit the Cell
        /// </summary>
        /// <param name="cell">The Cell that is currently being edited</param>
        /// <param name="editor">The ICellEditor used to edit the Cell</param>
        private void SetEditingCell(I3Cell cell, II3CellEditor editor)
        {
            this.SetEditingCell(new I3CellPos(cell.Row.InternalIndex, cell.InternalIndex), editor);
        }


        /// <summary>
        /// ��¼��ǰ���༭��Cell����ʹ�õı༭��
        /// Records the Cell that is currently being edited and the 
        /// ICellEditor used to edit the Cell
        /// </summary>
        /// <param name="cellPos">The Cell that is currently being edited</param>
        /// <param name="editor">The ICellEditor used to edit the Cell</param>
        private void SetEditingCell(I3CellPos cellPos, II3CellEditor editor)
        {
            this.editingCell = cellPos;
            this.curentCellEditor = editor;
        }


        /// <summary>
        /// ��ʼ�༭ָ����Cell
        /// Starts editing the Cell at the specified row and column indexes
        /// </summary>
        /// <param name="row">The row index of the Cell to be edited</param>
        /// <param name="column">The column index of the Cell to be edited</param>
        public void EditCell(int row, int column)
        {
            this.EditCell(new I3CellPos(row, column));
        }


        /// <summary>
        /// ��ʼ�༭ָ����Cell
        /// Starts editing the Cell at the specified CellPos
        /// </summary>
        /// <param name="cellPos">A CellPos that specifies the Cell to be edited</param>
        public bool EditCell(I3CellPos cellPos)
        {
            // don't bother if the cell doesn't exists or the cell's
            // column is not visible or the cell is not editable
            if (!this.IsValidCell(cellPos) || !this.ColumnModel.Columns[cellPos.Column].Visible || !this.IsCellEditable(cellPos))
            {
                return false;
            }

            // check if we're currently editing a cell
            if (this.EditingCell != I3CellPos.Empty)
            {
                // don't bother if we're already editing the cell.  
                // if we're editing a different cell stop editing
                if (this.EditingCell == cellPos)
                {
                    return false;
                }
                else
                {
                    this.EditingCellEditor.StopEditing();
                }
            }

            I3Cell cell = this.TableModel[cellPos];
            II3CellEditor editor = this.ColumnModel.GetCellEditor(cellPos.Column);

            // make sure we have an editor and that the cell 
            // and the cell's column are editable
            if (editor == null || !cell.Editable || !this.ColumnModel.Columns[cellPos.Column].Editable)
            {
                return false;
            }

            if (this.EnsureVisible(cellPos))
            {
                this.Refresh();
            }

            Rectangle cellRect = this.CellClientRect(cellPos);

            // give anyone subscribed to the table's BeginEditing
            // event the first chance to cancel editing
            I3CellEditEventArgs e = new I3CellEditEventArgs(cell, editor, this, cellPos.Row, cellPos.Column, cellRect);

            this.OnBeginEditing(e);

            //
            if (!e.Cancel)
            {
                // get the editor ready for editing.  if PrepareForEditing
                // returns false, someone who subscribed to the editors 
                // BeginEdit event has cancelled editing
                if (!editor.PrepareForEditing(cell, this, cellPos, cellRect, e.Handled))
                {
                    return false;
                }

                // keep track of the editing cell and editor 
                // and start editing
                this.editingCell = cellPos;
                this.curentCellEditor = editor;

                editor.StartEditing();
            }

            return true;
        }


        /*/// <summary>
        /// Stops editing the current Cell and starts editing the next editable Cell
        /// </summary>
        /// <param name="forwards">Specifies whether the editor should traverse 
        /// forward when looking for the next editable Cell</param>
        protected internal void EditNextCell(bool forwards)
        {
            if (this.EditingCell == CellPos.Empty)
            {
                return;
            }
				
            CellPos nextCell = this.FindNextEditableCell(this.FocusedCell, true, forwards, false);

            if (nextCell != CellPos.Empty && nextCell != this.EditingCell)
            {
                this.StopEditing();

                this.EditCell(nextCell);
            }
        }*/


        /// <summary>
        /// �����༭
        /// Stops editing the current Cell and commits any changes
        /// </summary>
        public void StopEditing()
        {
            // don't bother if we're not editing
            if (this.EditingCell == I3CellPos.Empty)
            {
                return;
            }

            this.EditingCellEditor.StopEditing();

            this.Invalidate(this.RowClientRect(this.editingCell.Row));

            this.editingCell = I3CellPos.Empty;
            this.curentCellEditor = null;
        }


        /// <summary>
        /// �˳��༭
        /// Cancels editing the current Cell and ignores any changes
        /// </summary>
        public void CancelEditing()
        {
            // don't bother if we're not editing
            if (this.EditingCell == I3CellPos.Empty)
            {
                return;
            }

            this.EditingCellEditor.CancelEditing();

            this.editingCell = I3CellPos.Empty;
            this.curentCellEditor = null;
        }


        /// <summary>
        /// �ж�Cell�Ƿ�ɱ��༭
        /// Returns whether the Cell at the specified row and column is able 
        /// to be edited by the user
        /// </summary>
        /// <param name="row">The row index of the Cell to check</param>
        /// <param name="column">The column index of the Cell to check</param>
        /// <returns>True if the Cell at the specified row and column is able 
        /// to be edited by the user, false otherwise</returns>
        public bool IsCellEditable(int row, int column)
        {
            return this.IsCellEditable(new I3CellPos(row, column));
        }


        /// <summary>
        /// �ж�Cell�Ƿ�ɱ��༭
        /// Returns whether the Cell at the specified CellPos is able 
        /// to be edited by the user
        /// </summary>
        /// <param name="cellpos">A CellPos that specifies the Cell to check</param>
        /// <returns>True if the Cell at the specified CellPos is able 
        /// to be edited by the user, false otherwise</returns>
        public bool IsCellEditable(I3CellPos cellpos)
        {
            // don't bother if the cell doesn't exists or the cell's
            // column is not visible
            if (!this.IsValidCell(cellpos) || !this.ColumnModel.Columns[cellpos.Column].Visible)
            {
                return false;
            }

            return (this.TableModel[cellpos].Editable &&
                this.ColumnModel.Columns[cellpos.Column].Editable);
        }


        /// <summary>
        /// �ж�Cell�Ƿ�Enabled
        /// Returns whether the Cell at the specified row and column is able 
        /// to respond to user interaction
        /// </summary>
        /// <param name="row">The row index of the Cell to check</param>
        /// <param name="column">The column index of the Cell to check</param>
        /// <returns>True if the Cell at the specified row and column is able 
        /// to respond to user interaction, false otherwise</returns>
        public bool IsCellEnabled(int row, int column)
        {
            return this.IsCellEnabled(new I3CellPos(row, column));
        }


        /// <summary>
        /// �ж�Cell�Ƿ�Enabled
        /// Returns whether the Cell at the specified CellPos is able 
        /// to respond to user interaction
        /// </summary>
        /// <param name="cellpos">A CellPos that specifies the Cell to check</param>
        /// <returns>True if the Cell at the specified CellPos is able 
        /// to respond to user interaction, false otherwise</returns>
        public bool IsCellEnabled(I3CellPos cellpos)
        {
            // don't bother if the cell doesn't exists or the cell's
            // column is not visible
            if (!this.IsValidCell(cellpos) || !this.ColumnModel.Columns[cellpos.Column].Visible)
            {
                return false;
            }

            return (this.TableModel[cellpos].Enabled &&
                this.ColumnModel.Columns[cellpos.Column].Enabled);
        }

        #endregion

        #region Invalidate

        /// <summary>
        /// �ػ�Cell
        /// Invalidates the specified Cell
        /// </summary>
        /// <param name="cell">The Cell to be invalidated</param>
        public void InvalidateCell(I3Cell cell)
        {
            this.InvalidateCell(cell.Row.Index, cell.Index);
        }


        /// <summary>
        /// �ػ�Cell
        /// Invalidates the Cell located at the specified row and column indicies
        /// </summary>
        /// <param name="row">The row index of the Cell to be invalidated</param>
        /// <param name="column">The column index of the Cell to be invalidated</param>
        public void InvalidateCell(int row, int column)
        {
            Rectangle cellRect = this.CellClientRect(row, column);

            if (cellRect == Rectangle.Empty)
            {
                return;
            }

            if (cellRect.IntersectsWith(this.ClientRectWithOutBorder_ScrollBar_Header))
            {
                this.Invalidate(Rectangle.Intersect(this.ClientRectWithOutBorder_ScrollBar_Header, cellRect), false);
            }
        }


        /// <summary>
        /// �ػ�Cell
        /// Invalidates the Cell located at the specified CellPos
        /// </summary>
        /// <param name="cellPos">A CellPos that specifies the Cell to be invalidated</param>
        public void InvalidateCell(I3CellPos cellPos)
        {
            this.InvalidateCell(cellPos.Row, cellPos.Column);
        }


        /// <summary>
        /// �ػ���
        /// Invalidates the specified Row
        /// </summary>
        /// <param name="row">The Row to be invalidated</param>
        public void InvalidateRow(I3Row row)
        {
            this.InvalidateRow(row.Index);
        }


        /// <summary>
        /// �ػ���
        /// Invalidates the Row located at the specified row index
        /// </summary>
        /// <param name="row">The row index of the Row to be invalidated</param>
        public void InvalidateRow(int row)
        {
            Rectangle rowRect = this.RowClientRect(row);

            if (rowRect == Rectangle.Empty)
            {
                return;
            }

            if (rowRect.IntersectsWith(this.ClientRectWithOutBorder_ScrollBar_Header))
            {
                this.Invalidate(Rectangle.Intersect(this.ClientRectWithOutBorder_ScrollBar_Header, rowRect), false);
            }
        }


        /// <summary>
        /// �ػ���
        /// Invalidates the Row located at the specified CellPos
        /// </summary>
        /// <param name="cellPos">A CellPos that specifies the Row to be invalidated</param>
        public void InvalidateRow(I3CellPos cellPos)
        {
            this.InvalidateRow(cellPos.Row);
        }

        #endregion

        #region Keys

        /// <summary>
        /// �жϱ����µļ��Ƿ�Table�ı�����
        /// Determines whether the specified key is reserved for use by the Table
        /// </summary>
        /// <param name="key">One of the Keys values</param>
        /// <returns>true if the specified key is reserved for use by the Table; 
        /// otherwise, false</returns>
        protected internal bool IsReservedKey(Keys key)
        {
            if ((key & Keys.Alt) != Keys.Alt)
            {
                Keys k = key & Keys.KeyCode;

                return (k == Keys.Up ||
                    k == Keys.Down ||
                    k == Keys.Left ||
                    k == Keys.Right ||
                    k == Keys.PageUp ||
                    k == Keys.PageDown ||
                    k == Keys.Home ||
                    k == Keys.End ||
                    k == Keys.Tab);
            }

            return false;
        }


        /// <summary>
        /// ȷ��ָ���ļ��Ƿ���һ����ͨ����������������ҪԤ����ļ���
        /// Determines whether the specified key is a regular input key or a special 
        /// key that requires preprocessing
        /// </summary>
        /// <param name="keyData">One of the Keys values</param>
        /// <returns>true if the specified key is a regular input key; otherwise, false</returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) != Keys.Alt)
            {
                Keys key = keyData & Keys.KeyCode;

                switch (key)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Prior:
                    case Keys.Next:
                    case Keys.End:
                    case Keys.Home:
                        {
                            return true;
                        }
                }

                if (base.IsInputKey(keyData))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Layout

        /// <summary>
        /// ��ֹTable�ػ�ֱ��������Ӧ��EndUpdateȫ��������
        /// Prevents the Table from drawing until the EndUpdate method is called
        /// </summary>
        public void BeginUpdate()
        {
            if (this.IsHandleCreated)
            {
                if (this.beginUpdateCount == 0)
                {
                    I3NativeMethods.SendMessage(this.Handle, 11, 0, 0);
                }

                this.beginUpdateCount++;
            }
        }


        /// <summary>
        /// �ָ��ػ棬��BeginUpdate���Ӧ
        /// Resumes drawing of the Table after drawing is suspended by the 
        /// BeginUpdate method
        /// </summary>
        public void EndUpdate()
        {
            if (this.beginUpdateCount <= 0)
            {
                return;
            }

            this.beginUpdateCount--;

            if (this.beginUpdateCount == 0)
            {
                I3NativeMethods.SendMessage(this.Handle, 11, -1, 0);

                this.PerformLayout();
                this.Invalidate(true);
            }
        }


        /// <summary>
        /// ���Table��ʼ��ʼ��
        /// Signals the object that initialization is starting
        /// </summary>
        public void BeginInit()
        {
            this.init = true;
        }


        /// <summary>
        /// ����Table�ĳ�ʼ��
        /// Signals the object that initialization is complete
        /// </summary>
        public void EndInit()
        {
            this.init = false;

            this.PerformLayout();
        }


        /// <summary>
        /// ����Table��ǰ�Ƿ����ڳ�ʼ��
        /// Gets whether the Table is currently initializing
        /// </summary>
        [Browsable(false)]
        public bool Initializing
        {
            get
            {
                return this.init;
            }
        }

        #endregion

        #region Mouse

        /// <summary>
        /// ����������������
        /// This member supports the .NET Framework infrastructure and is not 
        /// intended to be used directly from your code
        /// </summary>
        public new void ResetMouseEventArgs()
        {
            if (this.trackMouseEvent == null)
            {
                this.trackMouseEvent = new I3TRACKMOUSEEVENT();
                this.trackMouseEvent.dwFlags = 3;
                this.trackMouseEvent.hwndTrack = base.Handle;
            }

            this.trackMouseEvent.dwHoverTime = this.HoverTime;

            I3NativeMethods.TrackMouseEvent(this.trackMouseEvent);
        }

        #endregion

        #region Scrolling

        /// <summary>
        /// ���¹�����״̬
        /// Updates the scrollbars to reflect any changes made to the Table
        /// </summary>
        public void UpdateScrollBars()
        {
            if (!this.Scrollable || this.ColumnModel == null)
            {
                return;
            }

            // fix: Add width/height check as otherwise minimize 
            //      causes a crash
            //      Portia4ever (kangxj@126.com)
            //      13/09/2005
            //      v1.0.1
            if (this.Width == 0 || this.Height == 0)
            {
                return;
            }

            //����Ƿ���Ҫˮƽ������
            bool hscroll = (this.ColumnModel.VisibleColumnsWidth > this.Width - (this.BorderWidth * 2) - this.RowHeaderWidth);
            //����Ƿ���Ҫ��ֱ������
            bool vscroll = this.TotalRowAndHeaderHeight > (this.Height - (this.BorderWidth * 2) - (hscroll ? SystemInformation.HorizontalScrollBarHeight : 0));
            //�ٴμ���Ƿ���Ҫˮƽ����������ֱ�������ĳ��ֿ��ܵ���ԭ������Ҫˮƽ������������ǰ��Ҫ�ˣ�
            if (vscroll)
            {
                hscroll = (this.ColumnModel.VisibleColumnsWidth > this.Width - (this.BorderWidth * 2) - SystemInformation.VerticalScrollBarWidth - this.RowHeaderWidth);
            }

            //����ˮƽ������
            if (hscroll)
            {
                //��ȡˮƽ�������Ŀͻ�������
                Rectangle hscrollBounds = new Rectangle(this.BorderWidth,
                    this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
                    this.Width - (this.BorderWidth * 2),
                    SystemInformation.HorizontalScrollBarHeight);
                //�����ʾ��ֱ�������������˿��
                if (vscroll)
                {
                    hscrollBounds.Width -= SystemInformation.VerticalScrollBarWidth;
                }

                this.hScrollBar.Visible = true;//���� 
                this.hScrollBar.Bounds = hscrollBounds;//λ��
                this.hScrollBar.Minimum = 0;//��Сֵ
                this.hScrollBar.Maximum = this.ColumnModel.VisibleColumnsWidth - 2;//���ֵ
                this.hScrollBar.SmallChange = I3Column.MinimumWidth_Const;//С�̶�
                this.hScrollBar.LargeChange = Math.Max(0, hscrollBounds.Width - this.RowHeaderWidth - 1);//��̶�

                //�����ǰֵ�����һ����̶������ڣ����ĵ�ǰֵ������һ����̶ȵ�����  //???????��ʲô���ã�
                //if (this.hScrollBar.Value > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
                //{
                //    this.hScrollBar.Value = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
                //}
            }
            else
            {
                this.hScrollBar.Visible = false;//����
                this.hScrollBar.Value = 0;//��ǰֵ����
            }

            //���ô�ֱ������
            if (vscroll)
            {
                //��ȡ��ֱ�������Ŀͻ�������
                Rectangle vscrollBounds = new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
                    this.BorderWidth,
                    SystemInformation.VerticalScrollBarWidth,
                    this.Height - (this.BorderWidth * 2));

                //�����ʾˮƽ�������������˸߶�
                if (hscroll)
                {
                    vscrollBounds.Height -= SystemInformation.HorizontalScrollBarHeight;
                }

                this.vScrollBar.Visible = true;//����
                this.vScrollBar.Bounds = vscrollBounds;//λ��
                this.vScrollBar.Minimum = 0;//��Сֵ
                this.vScrollBar.Maximum = (this.RowsCount > this.VisibleRowsCount ? this.RowsCount - 1 : this.VisibleRowsCount);//���ֵ
                this.vScrollBar.SmallChange = 1;//С�̶�
                this.vScrollBar.LargeChange = Math.Max(0, this.VisibleRowsCount - 1);//��̶�
                //this.vScrollBar.LargeChange = 3;//��̶�

                //�����ǰֵ�����һ����̶������ڣ����ĵ�ǰֵ������һ����̶ȵ�����    //???????��ʲô���ã�
                //if (this.vScrollBar.Value > this.vScrollBar.Maximum - this.vScrollBar.LargeChange)
                //{
                //    this.vScrollBar.Value = this.vScrollBar.Maximum - this.vScrollBar.LargeChange;
                //}
            }
            else
            {
                this.vScrollBar.Visible = false;//����
                this.vScrollBar.Value = 0;//��ǰֵ����
            }
        }


        /// <summary>
        /// ˮƽ��������ֵ
        /// Scrolls the contents of the Table horizontally to the specified value
        /// </summary>
        /// <param name="value">The value to scroll to</param>
        protected void HorizontalScroll(int value)
        {
            //�������ֵ����
            int scrollVal = this.hScrollBar.Value - value;

            if (scrollVal != 0)
            {
                //��ȡ�ͻ�������RECT�ṹ��Rectangle�ṹ��
                Rectangle invalidateRect = this.ClientRectWithOutBorder_ScrollBar;//�ػ�����
                invalidateRect.X += this.RowHeaderWidth;
                invalidateRect.Width -= this.RowHeaderWidth;
                I3RECT scrollRect = I3RECT.FromRectangle(invalidateRect);//�ƶ�����

                //�ƶ�����Ļ滭��
                I3NativeMethods.ScrollWindow(this.Handle, scrollVal, 0, ref scrollRect, ref scrollRect);

                //�����ػ�����
                if (scrollVal < 0)
                {
                    invalidateRect.X = invalidateRect.Right + scrollVal;
                }
                invalidateRect.Width = Math.Abs(scrollVal);

                //�ػ�������
                this.Invalidate(invalidateRect, false);


                //˫������ʱ�ػ����½�����
                //if (this.VScroll)   //ΪʲôҪ�ػ棿�ػ���������ӳ�
                //{
                //    this.Invalidate(new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
                //        this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
                //        SystemInformation.VerticalScrollBarWidth,
                //        SystemInformation.HorizontalScrollBarHeight),
                //        false);
                //}
            }
        }


        /// <summary>
        /// ��ֱ��������ֵ
        /// Scrolls the contents of the Table vertically to the specified value
        /// </summary>
        /// <param name="value">The value to scroll to</param>
        protected void VerticalScroll(int value)
        {
            //�����������ֵ
            int scrollVal = this.vScrollBar.Value - value;

            if (scrollVal != 0)  //����ֵ���ʱ����Ҫ������
            {
                //��ȡ�ͻ�������RECT�ṹ��Rectangle�ṹ��
                I3RECT scrollRect = I3RECT.FromRectangle(this.ClientRectWithOutBorder_ScrollBar_Header);
                scrollRect.left -= this.RowHeaderWidth;
                Rectangle invalidateRect = scrollRect.ToRectangle();

                //scrollVal *= this.RowHeight;
                int total = 0;
                //for (int i = 0; i <= scrollVal - 1; i++)
                int min = value < this.vScrollBar.Value ? value : this.vScrollBar.Value;
                int max = value > this.vScrollBar.Value ? value : this.vScrollBar.Value;
                for (int i = min; i < max; i++)
                {
                    total = total + this.TableModel.Rows[i].Height;
                }
                scrollVal = value < this.vScrollBar.Value ? total : 0 - total;

                scrollRect.top += 1;
                I3NativeMethods.ScrollWindow(this.Handle, 0, scrollVal, ref scrollRect, ref scrollRect);

                if (scrollVal < 0)
                {
                    invalidateRect.Y = invalidateRect.Bottom + scrollVal;
                }
                invalidateRect.Height = Math.Abs(scrollVal) + 1;  //�ػ�����߶����ӣ���Ȼ���Ϲ���ʱ�����ػ�������
                this.Invalidate(invalidateRect, false);

                //˫������ʱ�ػ����½�����
                //if (this.HScroll)    //ΪʲôҪ�ػ棿�ػ���������ӳ�
                //{
                //    this.Invalidate(new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
                //        this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
                //        SystemInformation.VerticalScrollBarWidth,
                //        SystemInformation.HorizontalScrollBarHeight),
                //        false);
                //}
            }

            //this.UpdateScrollBars();
        }

        public int GetVerticalScrollValue()
        {
            return this.vScrollBar.Value;
        }

        public void SetVerticalScrollValue(int value)
        {
            this.vScrollBar.Value = value;
            this.Invalidate();
        }



        public int GetHerticalScrollValue()
        {
            return this.hScrollBar.Value;
        }

        public void SetHerticalScrollValue(int value)
        {
            this.hScrollBar.Value = value;
            this.Invalidate();
        }


        /// <summary>
        /// ʹ������������ȷ��ָ�������д���Cell����
        /// Ensures that the Cell at the specified row and column is visible 
        /// within the Table, scrolling the contents of the Table if necessary
        /// </summary>
        /// <param name="row">The zero-based index of the row to scroll into view</param>
        /// <param name="column">The zero-based index of the column to scroll into view</param>
        /// <returns>true if the Table scrolled to the Cell at the specified row 
        /// and column, false otherwise</returns>
        public bool EnsureVisible(int row, int column)
        {
            if (!this.Scrollable || (!this.HScroll && !this.VScroll) || row == -1)
            {
                return false;
            }

            if (column == -1)
            {
                if (this.FocusedCell.Column != -1)
                {
                    column = this.FocusedCell.Column;
                }
                else
                {
                    column = 0;
                }
            }

            int hscrollVal = this.hScrollBar.Value;
            int vscrollVal = this.vScrollBar.Value;
            bool moved = false;

            if (this.HScroll)
            {
                if (column < 0)
                {
                    column = 0;
                }
                else if (column >= this.ColumnCount)
                {
                    column = this.ColumnCount - 1;
                }

                if (this.ColumnModel.Columns[column].Visible)
                {
                    if (this.ColumnModel.Columns[column].Left < this.hScrollBar.Value)
                    {
                        hscrollVal = this.ColumnModel.Columns[column].Left;
                    }
                    else if (this.ColumnModel.Columns[column].Right > this.hScrollBar.Value + this.ClientRectWithOutBorder_ScrollBar_Header.Width)
                    {
                        hscrollVal = this.ColumnModel.Columns[column].Right - this.ClientRectWithOutBorder_ScrollBar_Header.Width;
                    }

                    if (hscrollVal > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
                    {
                        hscrollVal = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
                    }
                }
            }

            if (this.VScroll)
            {
                if (row < 0)
                {
                    vscrollVal = 0;
                }
                else if (row >= this.RowsCount)
                {
                    vscrollVal = this.RowsCount - 1;
                }
                else
                {
                    if (row < vscrollVal)
                    {
                        vscrollVal = row;
                    }
                    else if (row > vscrollVal + this.vScrollBar.LargeChange)
                    {
                        vscrollVal += row - (vscrollVal + this.vScrollBar.LargeChange);
                    }
                }

                if (vscrollVal > this.vScrollBar.Maximum - this.vScrollBar.LargeChange)
                {
                    vscrollVal = (this.vScrollBar.Maximum - this.vScrollBar.LargeChange) + 1;
                }
            }

            if (this.RowClientRect(row).Bottom > this.ClientRectWithOutBorder_ScrollBar_Header.Bottom)
            {
                vscrollVal++;
            }
            //if (this.VisibleRowsHeight >= this.ClientRectWithOutBorder_ScrollBar_Header.Height)
            //{
            //    vscrollVal = this.vScrollBar.Value + 1;
            //}
            //else
            //{
            //    vscrollVal = this.vScrollBar.Value;
            //}

            moved = (this.hScrollBar.Value != hscrollVal || this.vScrollBar.Value != vscrollVal);

            if (moved)
            {
                this.hScrollBar.Value = hscrollVal > this.hScrollBar.Maximum ? this.hScrollBar.Maximum : hscrollVal;
                this.vScrollBar.Value = vscrollVal > this.vScrollBar.Maximum ? this.vScrollBar.Maximum : vscrollVal;

                this.Invalidate(this.ClientRectWithOutBorder_ScrollBar);
            }

            return moved;
        }


        /// <summary>
        /// ʹ������������ȷ��ָ�������д���Cell����
        /// Ensures that the Cell at the specified CellPos is visible within 
        /// the Table, scrolling the contents of the Table if necessary
        /// </summary>
        /// <param name="cellPos">A CellPos that contains the zero-based index 
        /// of the row and column to scroll into view</param>
        /// <returns></returns>
        public bool EnsureVisible(I3CellPos cellPos)
        {
            return this.EnsureVisible(cellPos.Row, cellPos.Column);
        }


        /// <summary>
        /// ��ȡ��һ�����ӵ�Column�����
        /// Gets the index of the first visible Column currently displayed in the Table
        /// </summary>
        [Browsable(false)]
        public int FirstVisibleColumn
        {
            get
            {
                if (this.ColumnModel == null || this.ColumnModel.VisibleColumnCount == 0)
                {
                    return -1;
                }

                return this.ColumnModel.ColumnIndexAtDisplayX(this.hScrollBar.Value);
            }
        }


        /// <summary>
        /// ��ȡ���һ�����ӵ�Column�����
        /// Gets the index of the last visible Column currently displayed in the Table
        /// </summary>
        [Browsable(false)]
        public int LastVisibleColumn
        {
            get
            {
                if (this.ColumnModel == null || this.ColumnModel.VisibleColumnCount == 0)
                {
                    return -1;
                }

                int rightEdge = this.hScrollBar.Value + this.ClientRectWithOutBorder_ScrollBar.Right;

                if (this.VScroll)
                {
                    rightEdge -= this.vScrollBar.Width;
                }

                int col = this.ColumnModel.ColumnIndexAtDisplayX(rightEdge);

                if (col == -1)
                {
                    return this.ColumnModel.PreviousVisibleColumn(this.ColumnModel.Columns.Count);
                }
                else if (!this.ColumnModel.Columns[col].Visible)
                {
                    return this.ColumnModel.PreviousVisibleColumn(col);
                }

                return col;
            }
        }

        #endregion

        #region Sorting

        /// <summary>
        /// ��������ʱ������ǰ����˳��������򣬷��򣬽���ǰ�����а������������
        /// Sorts the last sorted column opposite to its current sort order, 
        /// or sorts the currently focused column in ascending order if no 
        /// columns have been sorted
        /// </summary>
        public void Sort()
        {
            this.Sort(true);
        }


        /// <summary>
        /// ��������ʱ�����䵱ǰ����˳��������򣬷��򣬽���ǰ�����а������������  
        /// stable����ָ���Ƿ�ʹ���ȶ�����ʽ
        /// Sorts the last sorted column opposite to its current sort order, 
        /// or sorts the currently focused column in ascending order if no 
        /// columns have been sorted
        /// </summary>
        /// <param name="stable">Specifies whether a stable sorting method 
        /// should be used to sort the column</param>
        public void Sort(bool stable)
        {
            // don't allow sorting if we're being used as a 
            // preview table in a ColumnModel editor
            if (this.Preview)
            {
                return;
            }

            // if we don't have a sorted column already, check if 
            // we can use the column of the cell that has focus
            if (!this.IsValidColumn(this.lastSortedColumn))
            {
                if (this.IsValidColumn(this.focusedCell.Column))
                {
                    this.lastSortedColumn = this.focusedCell.Column;
                }
            }

            // make sure the last sorted column exists
            if (this.IsValidColumn(this.lastSortedColumn))
            {
                // don't bother if the column won't let us sort
                if (!this.ColumnModel.Columns[this.lastSortedColumn].Sortable)
                {
                    return;
                }

                // work out which direction we should sort
                SortOrder newOrder = SortOrder.Ascending;

                I3Column column = this.ColumnModel.Columns[this.lastSortedColumn];

                if (column.SortOrder == SortOrder.Ascending)
                {
                    newOrder = SortOrder.Descending;
                }

                this.Sort(this.lastSortedColumn, column, newOrder, stable);
            }
        }


        /// <summary>
        /// ��������ʱ�����䵱ǰ����˳��������򣬷��򣬽�ָ���а������������
        /// Sorts the specified column opposite to its current sort order, 
        /// or in ascending order if the column is not sorted
        /// </summary>
        /// <param name="column">The index of the column to sort</param>
        public void Sort(int column)
        {
            this.Sort(column, true);
        }


        /// <summary>
        /// ��������ʱ�����䵱ǰ����˳��������򣬷��򣬽�ָ���а������������
        /// stable����ָ���Ƿ�ʹ���ȶ�����ʽ
        /// Sorts the specified column opposite to its current sort order, 
        /// or in ascending order if the column is not sorted
        /// </summary>
        /// <param name="column">The index of the column to sort</param>
        /// <param name="stable">Specifies whether a stable sorting method 
        /// should be used to sort the column</param>
        public void Sort(int column, bool stable)
        {
            // don't allow sorting if we're being used as a 
            // preview table in a ColumnModel editor
            if (this.Preview)
            {
                return;
            }

            // make sure the column exists
            if (this.IsValidColumn(column))
            {
                // don't bother if the column won't let us sort
                if (!this.ColumnModel.Columns[column].Sortable)
                {
                    return;
                }

                // if we already have a different sorted column, set 
                // its sort order to none
                if (column != this.lastSortedColumn)
                {
                    if (this.IsValidColumn(this.lastSortedColumn))
                    {
                        this.ColumnModel.Columns[this.lastSortedColumn].InternalSortOrder = SortOrder.None;
                    }
                }

                this.lastSortedColumn = column;

                // work out which direction we should sort
                SortOrder newOrder = SortOrder.Ascending;

                I3Column col = this.ColumnModel.Columns[column];

                if (col.SortOrder == SortOrder.Ascending)
                {
                    newOrder = SortOrder.Descending;
                }

                this.Sort(column, col, newOrder, stable);
            }
        }


        /// <summary>
        /// ��ָ���а�ָ��������ʽ��������
        /// Sorts the specified column in the specified sort direction
        /// </summary>
        /// <param name="column">The index of the column to sort</param>
        /// <param name="sortOrder">The direction the column is to be sorted</param>
        public void Sort(int column, SortOrder sortOrder)
        {
            this.Sort(column, sortOrder, true);
        }


        /// <summary>
        /// ��ָ���а�ָ��������ʽ��������
        /// stable����ָ���Ƿ�ʹ���ȶ�����ʽ
        /// Sorts the specified column in the specified sort direction
        /// </summary>
        /// <param name="column">The index of the column to sort</param>
        /// <param name="sortOrder">The direction the column is to be sorted</param>
        /// <param name="stable">Specifies whether a stable sorting method 
        /// should be used to sort the column</param>
        public void Sort(int column, SortOrder sortOrder, bool stable)
        {
            // don't allow sorting if we're being used as a 
            // preview table in a ColumnModel editor
            if (this.Preview)
            {
                return;
            }

            // make sure the column exists
            if (this.IsValidColumn(column))
            {
                // don't bother if the column won't let us sort
                if (!this.ColumnModel.Columns[column].Sortable)
                {
                    return;
                }

                // if we already have a different sorted column, set 
                // its sort order to none
                if (column != this.lastSortedColumn)
                {
                    if (this.IsValidColumn(this.lastSortedColumn))
                    {
                        this.ColumnModel.Columns[this.lastSortedColumn].InternalSortOrder = SortOrder.None;
                    }
                }

                this.lastSortedColumn = column;

                this.Sort(column, this.ColumnModel.Columns[column], sortOrder, stable);
            }
        }


        /// <summary>
        /// ��ָ���а�ָ��������ʽ��������
        /// stable����ָ���Ƿ�ʹ���ȶ�����ʽ
        /// ΪʲôҪͬʱָ�������е���ź������У�����
        /// Sorts the specified column in the specified sort direction
        /// </summary>
        /// <param name="index">The index of the column to sort</param>
        /// <param name="column">The column to sort</param>
        /// <param name="sortOrder">The direction the column is to be sorted</param>
        /// <param name="stable">Specifies whether a stable sorting method 
        /// should be used to sort the column</param>
        private void Sort(int index, I3Column column, SortOrder sortOrder, bool stable)
        {
            // make sure a null comparer type doesn't sneak past

            I3ComparerBase comparer = null;

            if (column.Comparer != null)
            {
                comparer = (I3ComparerBase)Activator.CreateInstance(column.Comparer, new object[] { this.TableModel, index, sortOrder });
            }
            else if (column.DefaultComparerType != null)
            {
                comparer = (I3ComparerBase)Activator.CreateInstance(column.DefaultComparerType, new object[] { this.TableModel, index, sortOrder });
            }
            else
            {
                return;
            }

            column.InternalSortOrder = sortOrder;

            // create the comparer
            I3SorterBase sorter = null;

            // work out which sort method to use.
            // - InsertionSort/MergeSort are stable sorts, 
            //   whereas ShellSort/HeapSort are unstable
            // - InsertionSort/ShellSort are faster than 
            //   MergeSort/HeapSort on small lists and slower 
            //   on large lists
            // so we choose based on the size of the list and
            // whether the user wants a stable sort
            if (this.TableModel.Rows.Count < 1000)
            {
                if (stable)
                {
                    sorter = new I3InsertionSorter(this.TableModel, index, comparer, sortOrder);
                }
                else
                {
                    sorter = new I3ShellSorter(this.TableModel, index, comparer, sortOrder);
                }
            }
            else
            {
                if (stable)
                {
                    sorter = new I3MergeSorter(this.TableModel, index, comparer, sortOrder);
                }
                else
                {
                    sorter = new I3HeapSorter(this.TableModel, index, comparer, sortOrder);
                }
            }

            // don't let the table redraw
            this.BeginUpdate();

            this.OnBeginSort(new I3ColumnEventArgs(column, index, I3ColumnEventType.Sorting, null));

            sorter.Sort();

            this.OnEndSort(new I3ColumnEventArgs(column, index, I3ColumnEventType.Sorting, null));

            // redraw any changes
            this.EndUpdate();
        }


        /// <summary>
        /// �ж�ָ����������Ƿ���ColumnModel��
        /// Returns whether a Column exists at the specified index in the 
        /// Table's ColumnModel
        /// </summary>
        /// <param name="column">The index of the column to check</param>
        /// <returns>True if a Column exists at the specified index in the 
        /// Table's ColumnModel, false otherwise</returns>
        public bool IsValidColumn(int column)
        {
            if (this.ColumnModel == null)
            {
                return false;
            }

            return (column >= 0 && column < this.ColumnModel.Columns.Count);
        }

        #endregion

        #endregion


        #region Properties

        #region Borders

        /// <summary>
        /// ��ȡ�����ñ߿����ʽ
        /// Gets or sets the border style for the Table
        /// </summary>
        [Category("Appearance"),
        DefaultValue(BorderStyle.Fixed3D),
        Description("Indicates the border style for the Table")]
        public BorderStyle BorderStyle
        {
            get
            {
                return this.borderStyle;
            }

            set
            {
                if (!Enum.IsDefined(typeof(BorderStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
                }

                if (borderStyle != value)
                {
                    this.borderStyle = value;

                    this.Invalidate(true);
                }
            }
        }


        /// <summary>
        /// ��ȡ�߿�Ŀ��
        /// Gets the width of the Tables border
        /// </summary>
        protected int BorderWidth
        {
            get
            {
                if (this.BorderStyle == BorderStyle.Fixed3D)
                {
                    return SystemInformation.Border3DSize.Width;
                }
                else if (this.BorderStyle == BorderStyle.FixedSingle)
                {
                    return 1;
                }

                return 0;
            }
        }

        #endregion

        #region Cells

        /// <summary>
        /// ��ȡ�����󾭹��ĵ�Ԫ��λ��
        /// Gets the last known cell position that the mouse was over
        /// </summary>
        [Browsable(false)]
        public I3CellPos LastMouseCell
        {
            get
            {
                return this.lastMouseCell;
            }
        }


        /// <summary>
        /// ��ȡ�������µĵ�Ԫ��λ��
        /// Gets the last known cell position that the mouse's left 
        /// button was pressed in
        /// </summary>
        [Browsable(false)]
        public I3CellPos LastMouseDownCell
        {
            get
            {
                return this.lastMouseDownCell;
            }
        }


        /// <summary>
        /// ��ȡ�����õ�ǰFocus�ĵ�Ԫ���λ��
        /// Gets or sets the position of the Cell that currently has focus
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public I3CellPos FocusedCell
        {
            get
            {
                return this.focusedCell;
            }
            set
            {
                if (!this.IsValidCell(value))
                {
                    return;
                }

                if (!this.TableModel[value].Enabled)
                {
                    return;
                }

                if (this.focusedCell != value)
                {
                    if (!this.focusedCell.IsEmpty)
                    {
                        this.RaiseCellLostFocus(this.focusedCell);
                    }

                    this.focusedCell = value;

                    if (!value.IsEmpty)
                    {
                        this.EnsureVisible(value);

                        this.RaiseCellGotFocus(value);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ��������ͣ�¼�������ʱ�� 
        /// Gets or sets the amount of time (in milliseconds) that that the 
        /// mouse pointer must hover over a Cell or Column Header before 
        /// a MouseHover event is raised
        /// </summary>
        [Category("Behavior"),
        DefaultValue(1000),
        Description("The amount of time (in milliseconds) that that the mouse pointer must hover over a Cell or Column Header before a MouseHover event is raised")]
        public int HoverTime
        {
            get
            {
                return this.hoverTime;
            }

            set
            {
                if (value < 100)
                {
                    throw new ArgumentException("HoverTime cannot be less than 100", "value");
                }

                if (this.hoverTime != value)
                {
                    this.hoverTime = value;

                    this.ResetMouseEventArgs();
                }
            }
        }

        #endregion

        #region ClientRectangle  (�ͻ������꣬�ؼ����Ͻ�����Ϊ0,0)

        /// <summary>
        /// ��ȡ�ؼ��Ŀͻ�����(ȥ�����߿�͹�����)(�ͻ������꣬�ؼ����Ͻ�����Ϊ0,ʼ)
        /// Gets the rectangle that represents the "client area" of the control.
        /// (The rectangle excludes the borders and scrollbars)
        /// </summary>
        [Browsable(false)]
        public Rectangle ClientRectWithOutBorder_ScrollBar
        {
            get
            {
                Rectangle clientRect = this.ClientRectWithOutBorder;

                if (this.HScroll)
                {
                    clientRect.Height -= SystemInformation.HorizontalScrollBarHeight;
                }

                if (this.VScroll)
                {
                    clientRect.Width -= SystemInformation.VerticalScrollBarWidth;
                }

                return clientRect;
            }
        }


        /// <summary>
        /// ��ȡ�������򣬲������߿򡢹���������ͷ(�ͻ������꣬�ؼ����Ͻ�����Ϊ0,ʼ)
        /// Gets the rectangle that represents the "cell data area" of the control.
        /// (The rectangle excludes the borders, column headers and scrollbars)
        /// </summary>
        [Browsable(false)]
        public Rectangle ClientRectWithOutBorder_ScrollBar_Header
        {
            get
            {
                Rectangle clientRect = this.ClientRectWithOutBorder_ScrollBar;

                if (this.ColumnHeaderStyle != ColumnHeaderStyle.None && this.ColumnCount > 0)
                {
                    clientRect.Y += this.ColumnHeaderHeight;
                    clientRect.Height -= this.ColumnHeaderHeight;
                }

                if (this.RowHeaderVisible)
                {
                    clientRect.X += this.RowHeaderWidth;
                    clientRect.Width -= this.RowHeaderWidth;
                }

                return clientRect;
            }
        }


        /// <summary>
        /// ��ȡȥ���߿�������(�ͻ������꣬�ؼ����Ͻ�����Ϊ0,ʼ)
        /// </summary>
        private Rectangle ClientRectWithOutBorder
        {
            get
            {
                return new Rectangle(this.BorderWidth,
                    this.BorderWidth,
                    this.Width - (this.BorderWidth * 2),
                    this.Height - (this.BorderWidth * 2));
            }
        }

        #endregion

        #region ColumnModel

        /// <summary>
        /// �Ƿ�����ƶ���
        /// </summary>
        [Category("Columns")]
        [DefaultValue(true)]
        public bool CanMoveColumn
        {
            get
            {
                return this.canMoveColumn;
            }
            set
            {
                this.canMoveColumn = value;
            }
        }

        /// <summary>
        /// ��ȡ�������м���
        /// Gets or sets the ColumnModel that contains all the Columns
        /// displayed in the Table
        /// </summary>
        [Category("Columns"),
        DefaultValue(null),
        Description("Specifies the ColumnModel that contains all the Columns displayed in the Table")]
        public I3ColumnModel ColumnModel
        {
            get
            {
                return this.columnModel;
            }

            set
            {
                if (this.columnModel != value)
                {
                    if (this.columnModel != null && this.columnModel.Table == this)
                    {
                        this.columnModel.InternalTable = null;
                        this.columnModel.ColumnPositionChanged -= new ColumnPositionChangedEvent(columnModel_ColumnPositionChanged);
                    }

                    this.columnModel = value;

                    if (value != null)
                    {
                        value.InternalTable = this;
                        value.ColumnPositionChanged += new ColumnPositionChangedEvent(columnModel_ColumnPositionChanged);
                    }

                    this.OnColumnModelChanged(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// ��ȡ���������Ƿ���Զ�����
        /// Gets or sets whether the Table allows users to resize Column widths
        /// </summary>
        [Category("Columns"),
        DefaultValue(true),
        Description("Specifies whether the Table allows users to resize Column widths")]
        public bool ColumnResizing
        {
            get
            {
                return this.columnResizing;
            }

            set
            {
                if (this.columnResizing != value)
                {
                    this.columnResizing = value;
                }
            }
        }

        [Category("Columns"),
        DefaultValue(false),
        Description("")]
        public bool ExtendLastCol
        {
            get
            {
                return this.extendLastCol;
            }
            set
            {
                this.extendLastCol = value;
            }
        }

        [Category("Items"),
        DefaultValue(false),
        Description("Specifies whether the Table allows users to resize Row heights")]
        public bool RowResizing
        {
            get
            {
                return this.rowResizing;
            }

            set
            {
                if (this.rowResizing != value)
                {
                    this.rowResizing = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ�еĸ���
        /// Returns the number of Columns in the Table
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnCount
        {
            get
            {
                if (this.ColumnModel == null)
                {
                    return -1;
                }

                return this.ColumnModel.Columns.Count;
            }
        }


        /// <summary>
        /// ��ȡ����������
        /// Returns the index of the currently sorted Column
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SortingColumn
        {
            get
            {
                return this.lastSortedColumn;
            }
        }


        /// <summary>
        /// ��ȡ������������еı���ɫ
        /// Gets or sets the background Color for the currently sorted column
        /// </summary>
        //[Category("Columns"),
        //Description("The background Color for a sorted Column")]
        //public Color SortedColumnBackColor
        //{
        //    get
        //    {
        //        return this.sortedColumnBackColor;
        //    }

        //    set
        //    {
        //        if (this.sortedColumnBackColor != value)
        //        {
        //            this.sortedColumnBackColor = value;

        //            if (this.IsValidColumn(this.lastSortedColumn))
        //            {
        //                Rectangle columnRect = this.ColumnClientRect(this.lastSortedColumn);

        //                if (this.ClientRectWithOutBorder_ScrollBar.IntersectsWith(columnRect))
        //                {
        //                    this.Invalidate(Rectangle.Intersect(this.ClientRectWithOutBorder_ScrollBar, columnRect));
        //                }
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// ��ȡ�Ƿ���Ҫ���л������б���ɫ��ΪWhiteSmokeʱΪĬ��ֵ������Ҫ���л�
        /// Specifies whether the Table's SortedColumnBackColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the SortedColumnBackColor property should be 
        /// serialized, False otherwise</returns>
        //private bool ShouldSerializeSortedColumnBackColor()
        //{
        //    return this.sortedColumnBackColor != Color.WhiteSmoke;
        //}

        [Category("Frozen"),
        DefaultValue(0),
        Description("��������")]
        public int FrozenColumnCount
        {
            get
            {
                return this.frozenColumnCount;
            }
            set
            {
                this.frozenColumnCount = value;
            }
        }

        #endregion

        #region DisplayRectangle  ������꣨����ScrollBar��λ�ƺ���h/vScrollBar.Value=100,��ʼ����������Ϊ-100,-100��

        /// <summary>
        /// ��ȡ�������Ļ����������򣨰�����Ч��������п����ǴӸ�ֵ��ʼ��
        /// ��ע�⣬��ͻ�������ת���ɻ������겻��һ���£���Ҫ������
        /// Gets the rectangle that represents the display area of the Table
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle displayRect = this.ClientRectWithOutBorder_ScrollBar_Header;

                if (!this.init)
                {
                    displayRect.X -= this.hScrollBar.Value;
                    //displayRect.Y -= this.vScrollBar.Value;
                    for (int i = 0; i < this.vScrollBar.Value; i++)
                    {
                        displayRect.Y -= this.tableModel.Rows[i].Height;
                    }
                    //displayRect = ClientToDisplayRect(displayRect);
                }

                if (this.ColumnModel == null)
                {
                    return displayRect;
                }

                //��������ȣ�ȡ�ϴ��ֵ
                //if (this.ColumnModel.TotalColumnWidth <= this.ClientRectWithOutBorder_ScrollBar_Header.Width)
                //2012.04.03 ie:Ӧ����VisibleColumnsWidth
                if (this.ColumnModel.VisibleColumnsWidth <= this.ClientRectWithOutBorder_ScrollBar_Header.Width)
                {
                    displayRect.Width = this.ClientRectWithOutBorder_ScrollBar_Header.Width;
                }
                else
                {
                    displayRect.Width = this.ColumnModel.VisibleColumnsWidth;
                }

                //�������߶ȣ�ȡ�ϴ��ֵ
                if (this.TotalRowsHeight <= this.ClientRectWithOutBorder_ScrollBar_Header.Height)
                //if (this.VisibleRowsHeight <= this.ClientRectWithOutBorder_ScrollBar_Header.Height)
                {
                    displayRect.Height = this.ClientRectWithOutBorder_ScrollBar_Header.Height;
                }
                else
                {
                    //displayRect.Height = this.VisibleRowsHeight;
                    displayRect.Height = this.TotalRowsHeight;
                }

                return displayRect;
            }
        }

        #endregion

        #region Editing

        /// <summary>
        /// ��ȡTable��ǰ�Ƿ����ڱ༭״̬��
        /// Gets whether the Table is currently editing a Cell
        /// </summary>
        [Browsable(false)]
        public bool IsEditing
        {
            get
            {
                return !this.EditingCell.IsEmpty;
            }
        }


        /// <summary>
        /// ��ȡ��ǰ���༭�ĵ�Ԫ���λ��
        /// Gets a CellPos that specifies the position of the Cell that 
        /// is currently being edited
        /// </summary>
        [Browsable(false)]
        public I3CellPos EditingCell
        {
            get
            {
                return this.editingCell;
            }
        }


        /// <summary>
        /// ��ȡ��ǰ���༭�ĵ�Ԫ��ı༭��
        /// Gets the ICellEditor that is currently being used to edit a Cell
        /// </summary>
        [Browsable(false)]
        public II3CellEditor EditingCellEditor
        {
            get
            {
                return this.curentCellEditor;
            }
        }


        /// <summary>
        /// ��ȡ�����ý���༭�������
        /// Gets or sets the action that causes editing to be initiated
        /// </summary>
        [Category("Editing"),
        DefaultValue(I3EditStartAction.DoubleClick_DataInputKey),
        Description("The action that causes editing to be initiated")]
        public I3EditStartAction EditStartAction
        {
            get
            {
                return this.editStartAction;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3EditStartAction), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3EditStartAction));
                }

                if (this.editStartAction != value)
                {
                    this.editStartAction = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ�������Զ���༭�ȼ�
        /// Gets or sets the custom key used to initiate Cell editing
        /// </summary>
        [Category("Editing"),
        DefaultValue(Keys.F5),
        Description("The custom key used to initiate Cell editing")]
        public Keys CustomEditKey
        {
            get
            {
                return this.customEditKey;
            }

            set
            {
                if (this.IsReservedKey(value))
                {
                    throw new ArgumentException("CustomEditKey cannot be one of the Table's reserved keys " +
                        "(Up arrow, Down arrow, Left arrow, Right arrow, PageUp, " +
                        "PageDown, Home, End, Tab)", "value");
                }

                if (this.customEditKey != value)
                {
                    this.customEditKey = value;
                }
            }
        }


        /*/// <summary>
        /// Gets or sets whether pressing the Tab key during editing moves
        /// the editor to the next editable Cell
        /// </summary>
        [Category("Editing"),
        DefaultValue(true),
        Description("")]
        public bool TabMovesEditor
        {
            get
            {	
                return this.tabMovesEditor;
            }

            set
            {
                this.tabMovesEditor = value;
            }
        }*/

        #endregion

        #region Grid

        /// <summary>
        /// ��ȡ������ �Ƿ���ʾ������
        /// Gets or sets how grid lines are displayed around rows and columns
        /// </summary>
        [Category("Grid"),
        DefaultValue(I3GridLines.Both),
        Description("Determines how grid lines are displayed around rows and columns")]
        public I3GridLines GridLines
        {
            get
            {
                return this.gridLines;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3GridLines), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3GridLines));
                }

                if (this.gridLines != value)
                {
                    this.gridLines = value;

                    this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, false);
                }
            }
        }


        /// <summary>
        /// ��ȡ������ �����ߵ���ʽ
        /// Gets or sets the style of the lines used to draw the grid
        /// </summary>
        [Category("Grid"),
        DefaultValue(I3GridLineStyle.Solid),
        Description("The style of the lines used to draw the grid")]
        public I3GridLineStyle GridLineStyle
        {
            get
            {
                return this.gridLineStyle;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3GridLineStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3GridLineStyle));
                }

                if (this.gridLineStyle != value)
                {
                    this.gridLineStyle = value;

                    if (this.GridLines != I3GridLines.None)
                    {
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ������ �����ߵ���ɫ
        /// Gets or sets the Color of the grid lines
        /// </summary>
        [Category("Grid"),
        Description("The color of the grid lines")]
        public Color GridColor
        {
            get
            {
                return this.gridColor;
            }

            set
            {
                if (this.gridColor != value)
                {
                    this.gridColor = value;

                    if (this.GridLines != I3GridLines.None)
                    {
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ ��������ɫ�Ƿ���Ҫ���о���ΪSystemColors.ControlʱΪĬ��ֵ������Ҫ���л�
        /// Specifies whether the Table's GridColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the GridColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeGridColor()
        {
            return (this.GridColor != SystemColors.Control);
        }


        /// <summary>
        /// ��ȡ�����ñ���ɫ
        /// </summary>
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
            }
        }


        /// <summary>
        /// ��ȡ����ɫ�Ƿ���Ҫ���о���ΪColor.WhiteʱΪĬ��ֵ������Ҫ���л�
        /// Specifies whether the Table's BackColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the BackColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeBackColor()
        {
            return (this.BackColor != Color.White);
        }

        #endregion

        #region Header

        /// <summary>
        /// ��ȡ��������ͷ��ʽ
        /// </summary>
        [Category("Columns"),
        DefaultValue(ColumnHeaderStyle.Clickable),
        Description("The style of the column headers")]
        public ColumnHeaderStyle ColumnHeaderStyle
        {
            get
            {
                return this._columnHeaderStyle;
            }

            set
            {
                if (!Enum.IsDefined(typeof(ColumnHeaderStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ColumnHeaderStyle));
                }

                if (this._columnHeaderStyle != value)
                {
                    this._columnHeaderStyle = value;

                    this.pressedColumn = -1;
                    this.hotColumn = -1;

                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// ��ȡ��������ͷ�Ƿ����
        /// </summary>
        [Category("Items"),
        DefaultValue(true),
        Description("The visible of the row headers")]
        public bool RowHeaderVisible
        {
            get
            {
                return this._rowHeaderVisible;
            }

            set
            {
                if (this._rowHeaderVisible != value)
                {
                    this._rowHeaderVisible = value;

                    this.Invalidate();
                }
            }
        }


        /// <summary>
        /// ��ȡ��ͷ�ĸ߶�
        /// </summary>
        [Browsable(false)]
        public int ColumnHeaderHeight
        {
            get
            {
                if (this.ColumnModel == null || this.ColumnHeaderStyle == ColumnHeaderStyle.None)
                {
                    return 0;
                }

                return this.ColumnModel.ColumnHeaderHeight;
            }
        }

        /// <summary>
        /// ��ȡ��������ͷ��ʾģʽ
        /// </summary>
        [Category("Columns"),
        Description("The display mode of the column headers")]
        public I3ColumnHeaderDisplayMode ColumnHeaderDisplayMode
        {
            get
            {
                return this.columnHeaderDisplayMode;
            }
            set
            {
                if (this.columnHeaderDisplayMode != value)
                {
                    this.columnHeaderDisplayMode = value;
                    this.Invalidate(this.ColumnHeaderClientRectangle);
                }
            }
        }

        /// <summary>
        /// ��ȡ��ͷ�Ŀ��
        /// </summary>
        [Browsable(false)]
        public int RowHeaderWidth
        {
            get
            {
                if (this.TableModel == null || !this._rowHeaderVisible)
                {
                    return 0;
                }

                return this.TableModel.RowHeaderWidth;
            }
        }

        /// <summary>
        /// ��ȡ��������ͷ��ʾģʽ
        /// </summary>
        [Category("Items"),
        Description("The display mode of the row headers")]
        public I3RowHeaderDisplayMode RowHeaderDisplayMode
        {
            get
            {
                return this.rowHeaderDisplayMode;
            }
            set
            {
                if (this.rowHeaderDisplayMode != value)
                {
                    this.rowHeaderDisplayMode = value;
                    this.Invalidate(this.RowHeaderClientRectangle);
                }
            }
        }


        /// <summary>
        /// ��ȡ��ͷ����
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ColumnHeaderClientRectangle
        {
            get
            {
                return new Rectangle(this.BorderWidth + this.RowHeaderWidth, this.BorderWidth, this.ClientRectWithOutBorder_ScrollBar_Header.Width, this.ColumnHeaderHeight);
            }
        }

        /// <summary>
        /// ��ͷ��ͷ��������
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle RowColumnHeaderClientRectangle
        {
            get
            {
                if (!this.RowHeaderVisible)
                {
                    return Rectangle.Empty;
                }
                return new Rectangle(this.BorderWidth, this.BorderWidth, this.RowHeaderWidth, this.ColumnHeaderHeight);
            }
        }

        /// <summary>
        /// ��ȡ��ͷ����
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle RowHeaderClientRectangle
        {
            get
            {
                return new Rectangle(this.BorderWidth, this.BorderWidth + this.ColumnHeaderHeight, this.RowHeaderWidth, this.ClientRectWithOutBorder_ScrollBar_Header.Height);
            }
        }


        /// <summary>
        /// ��ȡ��������ͷ����ͷ������
        /// Gets or sets the font used to draw the text in the column headers
        /// </summary>
        [Category("Columns"),
        Description("The font used to draw the text in the column headers")]
        public Font HeaderFont
        {
            get
            {
                return this.headerFont;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HeaderFont cannot be null");
                }

                if (this.headerFont != value)
                {
                    this.headerFont = value;

                    this.HeaderRenderer.Font = value;

                    this.Invalidate(this.ColumnHeaderClientRectangle, false);
                }
            }
        }


        /// <summary>
        /// ��ȡ��ͷ�����Ƿ���Ҫ���л�
        /// Specifies whether the Table's HeaderFont property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the HeaderFont property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeHeaderFont()
        {
            return this.HeaderFont != this.Font;
        }


        /// <summary>
        /// ��ȡ��������ͷ����Ⱦ��
        /// Gets or sets the HeaderRenderer used to draw the Column headers
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public I3HeaderRenderer HeaderRenderer
        {
            get
            {
                if (this._headerRenderer == null)
                {
                    this._headerRenderer = new I3XPHeaderRenderer();
                }

                return this._headerRenderer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HeaderRenderer cannot be null");
                }

                if (this._headerRenderer != value)
                {
                    this._headerRenderer = value;
                    this._headerRenderer.Font = this.HeaderFont;

                    this.Invalidate(this.ColumnHeaderClientRectangle, false);
                }
            }
        }


        /// <summary>
        /// ��ȡ��ͷ�ĵ����˵�
        /// Gets the ContextMenu used for Column Headers
        /// </summary>
        [Browsable(false)]
        public I3ColumnHeaderContextMenu ColumnHeaderContextMenu
        {
            get
            {
                return this._columnHeaderContextMenu;
            }
        }


        /// <summary>
        /// ��ȡ��������ͷ�����˵���Enabled
        /// Gets or sets whether the HeaderContextMenu is able to be 
        /// displayed when the user right clicks on a Column Header
        /// </summary>
        [Category("Columns"),
        DefaultValue(true),
        Description("Indicates whether the HeaderContextMenu is able to be displayed when the user right clicks on a Column Header")]
        public bool EnableColumnHeaderContextMenu
        {
            get
            {
                return this.ColumnHeaderContextMenu.Enabled;
            }

            set
            {
                this.ColumnHeaderContextMenu.Enabled = value;
            }
        }

        #endregion

        #region Rows

        /// <summary>
        /// ��ȡ�и�
        /// Gets or sets the height of each row
        /// </summary>
        [Browsable(false)]
        public int DefaultRowHeight
        {
            get
            {
                if (this.TableModel == null)
                {
                    return 0;
                }

                return this.TableModel.DefaultRowHeight;
            }
        }


        /// <summary>
        /// ��ȡ�����е��и�֮��  (��Ҫ��Ϊ�и߸ı�ʱ�Զ����㣬������ÿ���ػ涼��ȡ)
        /// Gets the combined height of all the rows in the Table
        /// </summary>
        [Browsable(false)]
        protected int TotalRowsHeight
        {
            get
            {
                if (this.TableModel == null)
                {
                    return 0;
                }

                return this.TableModel.TotalRowHeight;
            }
        }

        /// <summary>
        /// ���п����еĸ߶�
        /// </summary>
        protected int VisibleRowsHeight
        {
            get
            {
                if (this.TableModel == null || this.TableModel.Rows.Count == 0)
                {
                    return 0;
                }

                int total = 0;
                for (int i = this.TopIndex; i < this.TableModel.Rows.Count; i++)
                {
                    total = total + this.TableModel.Rows[i].Height;
                    if (total >= this.ClientRectWithOutBorder_ScrollBar_Header.Height)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                return total;
            }
        }


        /// <summary>
        /// ��ȡ�����к���ͷ�ĸ߶�֮��
        /// Gets the combined height of all the rows in the Table 
        /// plus the height of the column headers
        /// </summary>
        [Browsable(false)]
        protected int TotalRowAndHeaderHeight
        {
            get
            {
                return this.TotalRowsHeight + this.ColumnHeaderHeight;
            }
        }


        /// <summary>
        /// ��ȡ����
        /// Returns the number of Rows in the Table
        /// </summary>
        [Browsable(false)]
        public int RowsCount
        {
            get
            {
                if (this.TableModel == null)
                {
                    return 0;
                }

                return this.TableModel.Rows.Count;
            }
        }


        /// <summary>
        /// ��ȡ���ӵ�����
        /// Gets the number of rows that are visible in the Table
        /// </summary>
        [Browsable(false)]
        public int VisibleRowsCount
        {
            get
            {
                //int count = this.CellDataRect.Height / this.RowHeight;

                //if ((this.CellDataRect.Height % this.RowHeight) > 0)
                //{
                //    count++;
                //}

                //return count;


                int count = 0;
                int total = 0;
                Rectangle rect = this.ClientRectWithOutBorder_ScrollBar_Header;
                for (int i = 0; i <= this.frozenRowCount - 1; i++)
                {
                    total = total + this.TableModel.Rows[i].Height;
                    if (total >= rect.Height)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        count++;
                        continue;
                    }
                }
                for (int i = this.TopIndex; i < this.TableModel.Rows.Count; i++)
                {
                    if (i < 0)
                    {
                        continue;
                    }
                    total = total + this.TableModel.Rows[i].Height;
                    if (total >= rect.Height)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        count++;
                        continue;
                    }
                }


                return count;
            }
        }


        /// <summary>
        /// ��ȡTable�е�һ�������е�Index
        /// Gets the index of the first visible row in the Table
        /// </summary>
        [Browsable(false)]
        public int TopIndex
        {
            get
            {
                if (this.TableModel == null || this.TableModel.Rows.Count == 0)
                {
                    return -1;
                }

                if (this.VScroll)
                {
                    return this.vScrollBar.Value + this.frozenRowCount;
                }

                return 0 + this.frozenRowCount;
            }
        }


        /// <summary>
        /// ��ȡ��ǰ��һ��������
        /// Gets the first visible row in the Table
        /// </summary>
        [Browsable(false)]
        public I3Row TopItem
        {
            get
            {
                if (this.TableModel == null || this.TableModel.Rows.Count == 0)
                {
                    return null;
                }

                return this.TableModel.Rows[this.TopIndex];
            }
        }


        /// <summary>
        /// ��ȡ�����ý����е���ɫ
        /// Gets or sets the background color of odd-numbered rows in the Table
        /// </summary>
        [Category("Appearance"),
        DefaultValue(typeof(Color), "Transparent"),
        Description("The background color of odd-numbered rows in the Table")]
        public Color AlternatingRowColor
        {
            get
            {
                return this.alternatingRowColor;
            }

            set
            {
                if (this.alternatingRowColor != value)
                {
                    this.alternatingRowColor = value;

                    this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                }
            }
        }


        #endregion

        #region Scrolling

        /// <summary>
        /// ��ȡ������ �Ƿ�ʹ�ù�����
        /// Gets or sets a value indicating whether the Table will 
        /// allow the user to scroll to any columns or rows placed 
        /// outside of its visible boundaries
        /// </summary>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Indicates whether the Table will display scroll bars if it contains more items than can fit in the client area")]
        public bool Scrollable
        {
            get
            {
                return this.scrollable;
            }

            set
            {
                if (this.scrollable != value)
                {
                    this.scrollable = value;

                    this.PerformLayout();
                }
            }
        }


        /// <summary>
        /// ��ȡˮƽ�������Ƿ���ʾ
        /// Gets a value indicating whether the horizontal 
        /// scroll bar is visible
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HScroll
        {
            get
            {
                if (this.hScrollBar == null)
                {
                    return false;
                }

                return this.hScrollBar.Visible;
            }
        }


        /// <summary>
        /// ��ȡ��ֱ�������Ƿ���ʾ
        /// Gets a value indicating whether the vertical 
        /// scroll bar is visible
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VScroll
        {
            get
            {
                if (this.vScrollBar == null)
                {
                    return false;
                }

                return this.vScrollBar.Visible;
            }
        }


        #endregion

        #region Selection

        /// <summary>
        /// ��ȡ������ �С����Ƿ�ɱ�ѡ��
        /// Gets or sets whether cells are allowed to be selected
        /// </summary>
        [Category("Selection"),
        DefaultValue(true),
        Description("Specifies whether cells are allowed to be selected")]
        public bool AllowSelection
        {
            get
            {
                return this.allowSelection;
            }

            set
            {
                if (this.allowSelection != value)
                {
                    this.allowSelection = value;

                    if (!value && this.TableModel != null)
                    {
                        this.TableModel.Selections.Clear();
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ������ѡ�������ʽ
        /// Gets or sets how selected Cells are drawn by a Table
        /// </summary>
        [Category("Selection"),
        DefaultValue(I3SelectionStyle.Grid),
        Description("Determines how selected Cells are drawn by a Table")]
        public I3SelectionStyle SelectionStyle
        {
            get
            {
                return this.selectionStyle;
            }

            set
            {
                if (!Enum.IsDefined(typeof(I3SelectionStyle), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(I3SelectionStyle));
                }

                if (this.selectionStyle != value)
                {
                    this.selectionStyle = value;

                    if (this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ������ �Ƿ�ɶ�ѡ
        /// Gets or sets whether multiple cells are allowed to be selected
        /// </summary>
        [Category("Selection"),
        DefaultValue(false),
        Description("Specifies whether multiple cells are allowed to be selected")]
        public bool MultiSelect
        {
            get
            {
                return this.multiSelect;
            }

            set
            {
                if (this.multiSelect != value)
                {
                    this.multiSelect = value;
                }
            }
        }

        public bool SelectByRightButton
        {
            get
            {
                return this.selectByRightButton;
            }

            set
            {
                if (this.selectByRightButton != value)
                {
                    this.selectByRightButton = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ������ ��Ԫһ����Ԫ��ʱ���Ƿ�ѡ��������
        /// Gets or sets whether all other cells in the row are highlighted 
        /// when a cell is selected
        /// </summary>
        [Category("Selection"),
        DefaultValue(false),
        Description("Specifies whether all other cells in the row are highlighted when a cell is selected")]
        public bool FullRowSelect
        {
            get
            {
                return this.fullRowSelect;
            }

            set
            {
                if (this.fullRowSelect != value)
                {
                    this.fullRowSelect = value;

                    if (this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ������ Tableʧȥ����ʱ���Ƿ�����ѡ����
        /// Gets or sets whether highlighting is removed from the selected 
        /// cells when the Table loses focus
        /// </summary>
        [Category("Selection"),
        DefaultValue(false),
        Description("Specifies whether highlighting is removed from the selected cells when the Table loses focus")]
        public bool HideSelection
        {
            get
            {
                return this.hideSelection;
            }

            set
            {
                if (this.hideSelection != value)
                {
                    this.hideSelection = value;

                    if (!this.Focused && this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ������ѡ����ı���ɫ
        /// Gets or sets the background color of a selected cell
        /// </summary>
        [Category("Selection"),
        Description("The background color of a selected cell")]
        public Color SelectionBackColor
        {
            get
            {
                return this.selectionBackColor;
            }

            set
            {
                if (this.selectionBackColor != value)
                {
                    this.selectionBackColor = value;

                    if (this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡѡ�����ɫ�Ƿ���Ҫ���л�
        /// Specifies whether the Table's SelectionBackColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the SelectionBackColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeSelectionBackColor()
        {
            return (this.selectionBackColor != SystemColors.Highlight);
        }


        /// <summary>
        /// ��ȡ������ѡ�����ǰ��ɫ
        /// Gets or sets the foreground color of a selected cell
        /// </summary>
        [Category("Selection"),
        Description("The foreground color of a selected cell")]
        public Color SelectionForeColor
        {
            get
            {
                return this.selectionForeColor;
            }

            set
            {
                if (this.selectionForeColor != value)
                {
                    this.selectionForeColor = value;

                    if (this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡѡ�����ǰ��ɫ�Ƿ���Ҫ���л�
        /// Specifies whether the Table's SelectionForeColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the SelectionForeColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeSelectionForeColor()
        {
            return (this.selectionForeColor != SystemColors.HighlightText);
        }


        /// <summary>
        /// ��ȡ�����ò���Focus��ѡ����ı���ɫ
        /// Gets or sets the background color of a selected cell when the 
        /// Table doesn't have the focus
        /// </summary>
        [Category("Selection"),
        Description("The background color of a selected cell when the Table doesn't have the focus")]
        public Color UnfocusedSelectionBackColor
        {
            get
            {
                return this.unfocusedSelectionBackColor;
            }

            set
            {
                if (this.unfocusedSelectionBackColor != value)
                {
                    this.unfocusedSelectionBackColor = value;

                    if (!this.Focused && !this.HideSelection && this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ ����Focus��ѡ����ı���ɫ �Ƿ���Ҫ���л�
        /// Specifies whether the Table's UnfocusedSelectionBackColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the UnfocusedSelectionBackColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeUnfocusedSelectionBackColor()
        {
            return (this.unfocusedSelectionBackColor != SystemColors.Control);
        }


        /// <summary>
        /// ��ȡ�����ò���Focus��ѡ�����ǰ��ɫ
        /// Gets or sets the foreground color of a selected cell when the 
        /// Table doesn't have the focus
        /// </summary>
        [Category("Selection"),
        Description("The foreground color of a selected cell when the Table doesn't have the focus")]
        public Color UnfocusedSelectionForeColor
        {
            get
            {
                return this.unfocusedSelectionForeColor;
            }

            set
            {
                if (this.unfocusedSelectionForeColor != value)
                {
                    this.unfocusedSelectionForeColor = value;

                    if (!this.Focused && !this.HideSelection && this.TableModel != null)
                    {
                        //this.Invalidate(Rectangle.Intersect(this.CellDataRect, this.TableModel.Selections.SelectionBounds), false);
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                    }
                }
            }
        }


        /// <summary>
        /// ��ȡ ����Focus��ѡ�����ǰ��ɫ �Ƿ���Ҫ���л�
        /// Specifies whether the Table's UnfocusedSelectionForeColor property 
        /// should be serialized at design time
        /// </summary>
        /// <returns>True if the UnfocusedSelectionForeColor property should be 
        /// serialized, False otherwise</returns>
        private bool ShouldSerializeUnfocusedSelectionForeColor()
        {
            return (this.unfocusedSelectionForeColor != SystemColors.ControlText);
        }


        /// <summary>
        /// ��ȡ��ѡ����е�����
        /// Gets an array that contains the currently selected Rows
        /// </summary>
        [Browsable(false)]
        public I3Row[] SelectedItems
        {
            get
            {
                if (this.TableModel == null)
                {
                    return new I3Row[0];
                }

                return this.TableModel.Selections.SelectedItems;
            }
        }


        /// <summary>
        /// ��ȡ��ѡ����е�index������
        /// Gets an array that contains the indexes of the currently selected Rows
        /// </summary>
        [Browsable(false)]
        public int[] SelectedIndicies
        {
            get
            {
                if (this.TableModel == null)
                {
                    return new int[0];
                }

                return this.TableModel.Selections.SelectedIndicies;
            }
        }

        #endregion

        #region TableModel

        /// <summary>
        /// ��ȡ�������м���
        /// Gets or sets the TableModel that contains all the Rows
        /// and Cells displayed in the Table
        /// </summary>
        [Category("Items"),
        DefaultValue(null),
        Description("Specifies the TableModel that contains all the Rows and Cells displayed in the Table")]
        public I3TableModel TableModel
        {
            get
            {
                return this.tableModel;
            }

            set
            {
                if (this.tableModel != value)
                {
                    if (this.tableModel != null && this.tableModel.Table == this)
                    {
                        this.tableModel.InternalTable = null;
                    }

                    this.tableModel = value;

                    if (value != null)
                    {
                        value.InternalTable = this;
                    }

                    this.OnTableModelChanged(EventArgs.Empty);
                }
            }
        }


        [Category("Frozen"),
        DefaultValue(0),
        Description("��������")]
        public int FrozenRowCount
        {
            get
            {
                return this.frozenRowCount;
            }
            set
            {
                this.frozenRowCount = value;
            }
        }


        /// <summary>
        /// ��ȡ������Table����������ʾʱ����ʾ����
        /// Gets or sets the text displayed by the Table when it doesn't 
        /// contain any items
        /// </summary>
        [Category("Appearance"),
        DefaultValue("��������ʾ"),
        Description("Specifies the text displayed by the Table when it doesn't contain any items")]
        public string NoItemsText
        {
            get
            {
                return this.noItemsText;
            }

            set
            {
                if (!this.noItemsText.Equals(value))
                {
                    this.noItemsText = value;

                    if (this.ColumnModel == null || this.TableModel == null || this.TableModel.Rows.Count == 0)
                    {
                        this.Invalidate(this.ClientRectWithOutBorder_ScrollBar);
                    }
                }
            }
        }

        #endregion

        #region TableState

        /// <summary>
        /// ��ȡ������Table��״̬
        /// Gets or sets the current state of the Table
        /// </summary>
        protected I3TableState TableState
        {
            get
            {
                return this.tableState;
            }

            set
            {
                this.tableState = value;
            }
        }


        /// <summary>
        /// ������ָ�����������괦��Table״̬
        /// Calculates the state of the Table at the specified 
        /// client coordinates
        /// </summary>
        /// <param name="x">The client x coordinate</param>
        /// <param name="y">The client y coordinate</param>
        protected void CalcTableState(int x, int y)
        {
            I3TableRegion region = this.HitTest(x, y);


            if (region == I3TableRegion.ColumnHeader)
            {
                #region ColumnHeader
                int column = this.ColumnIndexAtClient(x, y);

                // get out of here if we aren't in a column
                if (column == -1)
                {
                    this.TableState = I3TableState.Normal;
                    return;
                }

                // get the bounding rectangle for the column's header
                Rectangle columnRect = this.ColumnHeaderClientRect(column);

                // are we in a resizing section on the left
                if (x < columnRect.Left + I3Column.ResizePadding_Const)
                {
                    #region ���ұߣ��к�Ҫ��1
                    this.TableState = I3TableState.ColumnResizing;

                    while (column != 0)
                    {
                        if (this.ColumnModel.Columns[column - 1].Visible)
                        {
                            break;
                        }

                        column--;
                    }

                    // if we are in the first visible column or the next column 
                    // to the left is disabled, then we should be potentialy 
                    // selecting instead of resizing
                    if (column == 0 || !this.ColumnModel.Columns[column - 1].Enabled)
                    {
                        this.TableState = I3TableState.ColumnSelecting;
                    }
                    #endregion
                }
                // or a resizing section on the right
                else if (x > columnRect.Right - I3Column.ResizePadding_Const)
                {
                    this.TableState = I3TableState.ColumnResizing;
                }
                // looks like we're somewhere in the middle of 
                // the column header
                else
                {
                    this.TableState = I3TableState.ColumnSelecting;
                }
                #endregion
            }
            else if (region == I3TableRegion.RowHeader)
            {
                #region RowHeader
                int row = this.RowIndexAtClient(x, y);

                // get out of here if we aren't in a row
                if (row == -1)
                {
                    this.TableState = I3TableState.Normal;
                    return;
                }

                // get the bounding rectangle for the column's header
                Rectangle rowRect = this.RowHeaderClientRect(row);

                // are we in a resizing section on the left
                if (y < rowRect.Top + I3Row.ResizePadding_Const)
                {
                    if (row == this.TopIndex)
                    {
                        this.TableState = I3TableState.Normal;
                    }
                    else
                    {
                        #region �����棬�к�Ҫ��1
                        this.TableState = I3TableState.RowResizing;

                        while (row != 0)
                        {
                            if (this.TableModel.Rows[row - 1].Visible)
                            {
                                break;
                            }

                            row--;
                        }

                        // if we are in the first visible column or the next column 
                        // to the left is disabled, then we should be potentialy 
                        // selecting instead of resizing
                        if (row == 0 || !this.TableModel.Rows[row - 1].Enabled)
                        {
                            this.TableState = I3TableState.RowSelecting;
                        }
                        #endregion
                    }
                }
                // or a resizing section on the right
                else if (y > rowRect.Bottom - I3Row.ResizePadding_Const)
                {
                    this.TableState = I3TableState.RowResizing;
                }
                // looks like we're somewhere in the middle of 
                // the column header
                else
                {
                    this.TableState = I3TableState.RowSelecting;
                }
                #endregion
            }
            else if (region == I3TableRegion.Cells)
            {
                this.TableState = I3TableState.Selecting;
            }
            else
            {
                this.TableState = I3TableState.Normal;
            }

            if (this.TableState == I3TableState.ColumnResizing && !this.ColumnResizing)
            {
                this.TableState = I3TableState.ColumnSelecting;
            }
            if (this.TableState == Models.I3TableState.RowResizing && !this.RowResizing)
            {
                this.TableState = I3TableState.RowSelecting;
            }
        }


        /// <summary>
        /// ��ȡ��ǰTable�Ƿ�������¼�
        /// Gets whether the Table is able to raise events
        /// </summary>
        protected internal bool CanRaiseEvents
        {
            get
            {
                return (this.IsHandleCreated && this.beginUpdateCount == 0);
            }
        }


        /// <summary>
        /// ��ȡ  Table�Ƿ����м��ϱ༭�ĵ�Ԥ����ͼģʽ
        /// Gets or sets whether the Table is being used as a preview Table in 
        /// a ColumnCollectionEditor
        /// </summary>
        internal bool Preview
        {
            get
            {
                return this.preview;
            }

            set
            {
                this.preview = value;
            }
        }

        #endregion

        #region ToolTips

        /// <summary>
        /// ��ȡ ���˵������
        /// Gets the internal tooltip component
        /// </summary>
        internal ToolTip ToolTip
        {
            get
            {
                return this.toolTip;
            }
        }


        /// <summary>
        /// ��ȡ������ ���˵�����ڵ�Enabled
        /// Gets or sets whether ToolTips are currently enabled for the Table
        /// </summary>
        [Category("ToolTips"),
        DefaultValue(false),
        Description("Specifies whether ToolTips are enabled for the Table.")]
        public bool EnableToolTips
        {
            get
            {
                return this.toolTip.Active;
            }

            set
            {
                this.toolTip.Active = value;
            }
        }


        /// <summary>
        /// ��ȡ������ ToolTip���Զ��ӳ�ʱ��
        /// ��ȡ�����ù�����ʾ���Զ��ӳ١�  �Զ��ӳ٣��Ժ���Ϊ��λ����Ĭ��ֵΪ 500��
        /// Gets or sets the automatic delay for the Table's ToolTip
        /// </summary>
        [Category("ToolTips"),
        DefaultValue(500),
        Description("Specifies the automatic delay for the Table's ToolTip.")]
        public int ToolTipAutomaticDelay
        {
            get
            {
                return this.toolTip.AutomaticDelay;
            }

            set
            {
                if (value > 0 && this.toolTip.AutomaticDelay != value)
                {
                    this.toolTip.AutomaticDelay = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ�����õ���겻��ʱ��ToolTip��ʾ�󱣳ֵ�ʱ��
        /// Gets or sets the period of time the Table's ToolTip remains visible if 
        /// the mouse pointer is stationary within a Cell with a valid ToolTip text
        /// </summary>
        [Category("ToolTips"),
        DefaultValue(5000),
        Description("Specifies the period of time the Table's ToolTip remains visible if the mouse pointer is stationary within a cell with specified ToolTip text.")]
        public int ToolTipAutoPopDelay
        {
            get
            {
                return this.toolTip.AutoPopDelay;
            }

            set
            {
                if (value > 0 && this.toolTip.AutoPopDelay != value)
                {
                    this.toolTip.AutoPopDelay = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ�����ù�����ʾ��ʾ֮ǰ������ʱ��
        /// Gets or sets the time that passes before the Table's ToolTip appears
        /// </summary>
        [Category("ToolTips"),
        DefaultValue(1000),
        Description("Specifies the time that passes before the Table's ToolTip appears.")]
        public int ToolTipInitialDelay
        {
            get
            {
                return this.toolTip.InitialDelay;
            }

            set
            {
                if (value > 0 && this.toolTip.InitialDelay != value)
                {
                    this.toolTip.InitialDelay = value;
                }
            }
        }


        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ���ʾ������ʾ���ڣ����������丸�ؼ������ʱ��
        /// Gets or sets whether the Table's ToolTip window is 
        /// displayed even when its parent control is not active
        /// </summary>
        [Category("ToolTips"),
        DefaultValue(false),
        Description("Specifies whether the Table's ToolTip window is displayed even when its parent control is not active.")]
        public bool ToolTipShowAlways
        {
            get
            {
                return this.toolTip.ShowAlways;
            }

            set
            {
                if (this.toolTip.ShowAlways != value)
                {
                    this.toolTip.ShowAlways = value;
                }
            }
        }


        /// <summary>
        /// ����TooTip
        /// </summary>
        private void ResetToolTip()
        {
            bool tooltipActive = this.ToolTip.Active;

            if (tooltipActive)
            {
                this.ToolTip.Active = false;
            }

            this.ResetMouseEventArgs();

            this.ToolTip.SetToolTip(this, null);

            if (tooltipActive)
            {
                this.ToolTip.Active = true;
            }
        }

        #endregion

        #endregion


        #region Events

        #region Cells

        /// <summary>
        /// ���ô˺������׳�CellPropertyChanged�¼�
        /// Raises the CellPropertyChanged event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        protected internal virtual void OnCellPropertyChanged(I3CellEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.InvalidateCell(e.Row, e.Column);

                if (CellPropertyChanged != null)
                {
                    CellPropertyChanged(this, e);
                }

                if (e.EventType == I3CellEventType.CheckStateChanged)
                {
                    this.OnCellCheckChanged(new I3CellCheckBoxEventArgs(e.Cell, e.Column, e.Row));
                }
            }
        }


        /// <summary>
        /// Cells�����Ըı��¼��� ִ�д���
        /// Handler for a Cells PropertyChanged event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        private void cell_PropertyChanged(object sender, I3CellEventArgs e)
        {
            this.OnCellPropertyChanged(e);
        }


        #region Buttons


        #endregion

        #region CheckBox

        /// <summary>
        /// ���ô˺������׳�CellCheckChanged�¼�
        /// Raises the CellCheckChanged event
        /// </summary>
        /// <param name="e">A CellCheckChanged that contains the event data</param>
        protected internal virtual void OnCellCheckChanged(I3CellCheckBoxEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (CellCheckChanged != null)
                {
                    CellCheckChanged(this, e);
                }
            }
        }

        #endregion

        #region Focus

        /// <summary>
        /// ���ô˺������׳�CellGotFocus�¼�
        /// Raises the CellGotFocus event
        /// </summary>
        /// <param name="e">A CellFocusEventArgs that contains the event data</param>
        protected virtual void OnCellGotFocus(I3CellFocusEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnGotFocus(e);
                }

                if (CellGotFocus != null)
                {
                    CellGotFocus(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellGotFocus�¼�
        /// Raises the GotFocus event for the Cell at the specified position
        /// </summary>
        /// <param name="cellPos">The position of the Cell that gained focus</param>
        protected void RaiseCellGotFocus(I3CellPos cellPos)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(cellPos.Column);

            if (renderer != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellFocusEventArgs cfea = new I3CellFocusEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column));

                this.OnCellGotFocus(cfea);
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellLostFocus�¼�
        /// Raises the CellLostFocus event
        /// </summary>
        /// <param name="e">A CellFocusEventArgs that contains the event data</param>
        protected virtual void OnCellLostFocus(I3CellFocusEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnLostFocus(e);
                }

                if (CellLostFocus != null)
                {
                    CellLostFocus(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellLostFocus�¼�
        /// Raises the LostFocus event for the Cell at the specified position
        /// </summary>
        /// <param name="cellPos">The position of the Cell that lost focus</param>
        protected void RaiseCellLostFocus(I3CellPos cellPos)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(cellPos.Column);

            if (renderer != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel[cellPos.Row, cellPos.Column];
                }

                I3CellFocusEventArgs cfea = new I3CellFocusEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column));

                this.OnCellLostFocus(cfea);
            }
        }

        #endregion

        #region Keys

        /// <summary>
        /// ���ô˺������׳�CellKeyDown�¼�
        /// Raises the CellKeyDown event
        /// </summary>
        /// <param name="e">A CellKeyEventArgs that contains the event data</param>
        protected virtual void OnCellKeyDown(I3CellKeyEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnKeyDown(e);
                }

                if (CellKeyDown != null)
                {
                    CellKeyDown(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellKeyDown�¼�
        /// Raises a KeyDown event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        /// <param name="e">A KeyEventArgs that contains the event data</param>
        protected void RaiseCellKeyDown(I3CellPos cellPos, KeyEventArgs e)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (!this.TableModel[cellPos].Enabled)
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellKeyEventArgs ckea = new I3CellKeyEventArgs(cell, this, cellPos, this.CellClientRect(cellPos.Row, cellPos.Column), e);

                this.OnCellKeyDown(ckea);
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellKeyUp�¼�
        /// Raises the CellKeyUp event
        /// </summary>
        /// <param name="e">A CellKeyEventArgs that contains the event data</param>
        protected virtual void OnCellKeyUp(I3CellKeyEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnKeyUp(e);
                }

                if (CellKeyUp != null)
                {
                    CellKeyUp(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellKeyUp�¼�
        /// Raises a KeyUp event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        /// <param name="e">A KeyEventArgs that contains the event data</param>
        protected void RaiseCellKeyUp(I3CellPos cellPos, KeyEventArgs e)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (!this.TableModel[cellPos].Enabled)
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellKeyEventArgs ckea = new I3CellKeyEventArgs(cell, this, cellPos, this.CellClientRect(cellPos.Row, cellPos.Column), e);

                this.OnCellKeyUp(ckea);
            }
        }

        #endregion

        #region Mouse

        #region MouseEnter

        /// <summary>
        /// ���ô˺������׳�CellMouseEnter�¼�
        /// Raises the CellMouseEnter event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        protected virtual void OnCellMouseEnter(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnMouseEnter(e);
                }

                if (CellMouseEnter != null)
                {
                    CellMouseEnter(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellMouseEnter�¼�
        /// Raises a MouseEnter event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        protected void RaiseCellMouseEnter(I3CellPos cellPos)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellMouseEventArgs mcea = new I3CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column));

                this.OnCellMouseEnter(mcea);
            }
        }

        #endregion

        #region MouseLeave

        /// <summary>
        /// ���ô˺������׳�CellMouseLeave�¼�
        /// Raises the CellMouseLeave event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        protected virtual void OnCellMouseLeave(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnMouseLeave(e);
                }

                if (CellMouseLeave != null)
                {
                    CellMouseLeave(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellMouseLeave�¼�
        /// Raises a MouseLeave event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        protected internal void RaiseCellMouseLeave(I3CellPos cellPos)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellMouseEventArgs mcea = new I3CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column));

                this.OnCellMouseLeave(mcea);
            }
        }

        #endregion

        #region MouseUp

        /// <summary>
        /// ���ô˺������׳�CellMouseUp�¼�
        /// Raises the CellMouseUp event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        protected virtual void OnCellMouseUp(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnMouseUp(e);
                }

                if (CellMouseUp != null)
                {
                    CellMouseUp(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellMouseUp�¼�
        /// Raises a MouseUp event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected void RaiseCellMouseUp(I3CellPos cellPos, MouseEventArgs e)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (!this.TableModel[cellPos].Enabled)
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellMouseEventArgs mcea = new I3CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column), e);

                this.OnCellMouseUp(mcea);
            }
        }

        #endregion

        #region MouseDown

        /// <summary>
        /// ���ô˺������׳�CellMouseDown�¼�
        /// Raises the CellMouseDown event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        protected virtual void OnCellMouseDown(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnMouseDown(e);
                }

                if (CellMouseDown != null)
                {
                    CellMouseDown(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellMouseDown�¼�
        /// Raises a MouseDown event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected void RaiseCellMouseDown(I3CellPos cellPos, MouseEventArgs e)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (!this.TableModel[cellPos].Enabled)
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellMouseEventArgs mcea = new I3CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column), e);

                this.OnCellMouseDown(mcea);
            }
        }

        #endregion

        #region MouseMove

        /// <summary>
        /// ���ô˺������׳�CellMouseMove�¼�
        /// Raises the CellMouseMove event
        /// </summary>
        /// <param name="e">A CellMouseEventArgs that contains the event data</param>
        protected virtual void OnCellMouseMove(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(e.Column);

                if (renderer != null)
                {
                    renderer.OnMouseMove(e);
                }

                if (CellMouseMove != null)
                {
                    CellMouseMove(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellMouseMove�¼�
        /// Raises a MouseMove event for the Cell at the specified cell position
        /// </summary>
        /// <param name="cellPos">The position of the Cell</param>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected void RaiseCellMouseMove(I3CellPos cellPos, MouseEventArgs e)
        {
            if (!this.IsValidCell(cellPos))
            {
                return;
            }

            if (this.ColumnModel.GetCellRenderer(cellPos.Column) != null)
            {
                I3Cell cell = null;

                if (cellPos.Column < this.TableModel.Rows[cellPos.Row].Cells.Count)
                {
                    cell = this.TableModel.Rows[cellPos.Row].Cells[cellPos.Column];
                }

                I3CellMouseEventArgs mcea = new I3CellMouseEventArgs(cell, this, cellPos.Row, cellPos.Column, this.CellClientRect(cellPos.Row, cellPos.Column), e);

                this.OnCellMouseMove(mcea);
            }
        }


        /// <summary>
        /// ������ƶ����հ״�ʱ������lastMouseCellΪCellPos.Empty�����׳�CellMouseLeave�¼�
        /// Resets the last known cell position that the mouse was over to empty
        /// </summary>
        internal void ResetLastMouseCell()
        {
            if (!this.lastMouseCell.IsEmpty)
            {
                this.ResetMouseEventArgs();

                I3CellPos oldLastMouseCell = this.lastMouseCell;
                this.lastMouseCell = I3CellPos.Empty;

                this.RaiseCellMouseLeave(oldLastMouseCell);
            }
        }

        #endregion

        #region MouseHover

        /// <summary>
        /// ���ô˺������׳�CellMouseHover�¼�
        /// Raises the CellHover event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        protected virtual void OnCellMouseHover(I3CellMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (CellMouseHover != null)
                {
                    CellMouseHover(e.Cell, e);
                }
            }
        }

        #endregion

        #region Click

        /// <summary>
        /// ���ô˺������׳�CellClick�¼�
        /// Raises the CellClick event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        protected virtual void OnCellMouseClick(I3CellMouseEventArgs e)
        {
            if (!this.IsCellEnabled(e.CellPos))
            {
                return;
            }

            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(this.LastMouseCell.Column);

                if (renderer != null)
                {
                    renderer.OnClick(e);
                }

                if (CellClick != null)
                {
                    CellClick(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellDoubleClick�¼�
        /// Raises the CellDoubleClick event
        /// </summary>
        /// <param name="e">A CellEventArgs that contains the event data</param>
        protected virtual void OnCellMouseDoubleClick(I3CellMouseEventArgs e)
        {
            if (!this.IsCellEnabled(e.CellPos))
            {
                return;
            }

            if (this.CanRaiseEvents)
            {
                II3CellRenderer renderer = this.ColumnModel.GetCellRenderer(this.LastMouseCell.Column);

                if (renderer != null)
                {
                    renderer.OnDoubleClick(e);
                }

                if (CellDoubleClick != null)
                {
                    CellDoubleClick(e.Cell, e);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Columns

        /// <summary>
        /// ���ô˺������׳�ColumnPropertyChanged�¼�
        /// Raises the ColumnPropertyChanged event
        /// </summary>
        /// <param name="e">A ColumnEventArgs that contains the event data</param>
        protected internal virtual void OnColumnPropertyChanged(I3ColumnEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                Rectangle columnHeaderRect;

                if (e.Index != -1)
                {
                    columnHeaderRect = this.ColumnHeaderClientRect(e.Index);
                }
                else
                {
                    columnHeaderRect = this.ColumnHeaderClientRect(e.Column);
                }

                switch (e.EventType)
                {
                    case I3ColumnEventType.VisibleChanged:
                    case I3ColumnEventType.WidthChanged:
                        {
                            if (e.EventType == I3ColumnEventType.VisibleChanged)
                            {
                                if (e.Column.Visible && e.Index != this.lastSortedColumn)
                                {
                                    e.Column.InternalSortOrder = SortOrder.None;
                                }

                                if (e.Index == this.FocusedCell.Column && !e.Column.Visible)
                                {
                                    int index = this.ColumnModel.NextVisibleColumn(e.Index);

                                    if (index == -1)
                                    {
                                        index = this.ColumnModel.PreviousVisibleColumn(e.Index);
                                    }

                                    if (index != -1)
                                    {
                                        this.FocusedCell = new I3CellPos(this.FocusedCell.Row, index);
                                    }
                                    else
                                    {
                                        this.FocusedCell = I3CellPos.Empty;
                                    }
                                }
                            }

                            if (columnHeaderRect.X <= 0)
                            {
                                this.Invalidate(this.ClientRectWithOutBorder_ScrollBar);
                            }
                            else if (columnHeaderRect.Left <= this.ClientRectWithOutBorder_ScrollBar.Right)
                            {
                                this.Invalidate(new Rectangle(columnHeaderRect.X,
                                    this.ClientRectWithOutBorder_ScrollBar.Top,
                                    this.ClientRectWithOutBorder_ScrollBar.Right - columnHeaderRect.X,
                                    this.ClientRectWithOutBorder_ScrollBar.Height));
                            }

                            this.UpdateScrollBars();

                            break;
                        }

                    case I3ColumnEventType.TextChanged:
                    case I3ColumnEventType.StateChanged:
                    case I3ColumnEventType.ImageChanged:
                    case I3ColumnEventType.HeaderAlignmentChanged:
                        {
                            if (columnHeaderRect.IntersectsWith(this.ColumnHeaderClientRectangle))
                            {
                                this.Invalidate(columnHeaderRect);
                            }

                            break;
                        }

                    case I3ColumnEventType.AlignmentChanged:
                    case I3ColumnEventType.RendererChanged:
                    case I3ColumnEventType.EnabledChanged:
                        {
                            if (e.EventType == I3ColumnEventType.EnabledChanged)
                            {
                                if (e.Index == this.FocusedCell.Column)
                                {
                                    this.FocusedCell = I3CellPos.Empty;
                                }
                            }

                            if (columnHeaderRect.IntersectsWith(this.ColumnHeaderClientRectangle))
                            {
                                this.Invalidate(new Rectangle(columnHeaderRect.X,
                                    this.ClientRectWithOutBorder_ScrollBar.Top,
                                    columnHeaderRect.Width,
                                    this.ClientRectWithOutBorder_ScrollBar.Height));
                            }

                            break;
                        }
                }

                if (ColumnPropertyChanged != null)
                {
                    ColumnPropertyChanged(e.Column, e);
                }
            }
        }


        private void columnModel_ColumnPositionChanged(ColumnPositionChangedEventArgs args)
        {
            if (this.tableModel == null || this.tableModel.Rows == null || this.tableModel.Rows.Count == 0)
            {
                return;
            }
            foreach (I3Row row in this.tableModel.Rows)
            {
                row.Cells.CheckCellsCount(this.columnModel.Columns.Count);
                row.Cells.MoveCell(args.MoveColumnIndex, args.NewIndex);
            }
        }

        #endregion

        #region Column/Row Headers

        #region MouseEnter

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseEnter�¼�
        /// Raises the ColumnHeaderMouseEnter event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseEnter(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseEnter(e);
                }

                if (ColumnHeaderMouseEnter != null)
                {
                    ColumnHeaderMouseEnter(e.Column, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseEnter�¼�
        /// Raises a ColumnMouseEnter event for the Column header at the specified colunm 
        /// index position
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        protected void RaiseColumnHeaderMouseEnter(int index)
        {
            if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Column column = this.ColumnModel.Columns[index];

                I3ColumnHeaderMouseEventArgs mhea = new I3ColumnHeaderMouseEventArgs(column, this, index, this.ColumnHeaderClientRect(index));

                this.OnColumnHeaderMouseEnter(mhea);
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseEnter�¼�
        /// Raises the RowHeaderMouseEnter event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseEnter(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseEnter(e);
                }

                if (RowHeaderMouseEnter != null)
                {
                    RowHeaderMouseEnter(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseEnter�¼�
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        protected void RaiseRowHeaderMouseEnter(int index)
        {
            if (index < 0 || this.TableModel == null || index >= this.TableModel.Rows.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Row row = this.TableModel.Rows[index];

                I3RowHeaderMouseEventArgs mhea = new I3RowHeaderMouseEventArgs(row, this, index, this.RowHeaderClientRect(index));

                this.OnRowHeaderMouseEnter(mhea);
            }
        }

        #endregion

        #region MouseLeave

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseLeave�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseLeave(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseLeave(e);
                }

                if (ColumnHeaderMouseLeave != null)
                {
                    ColumnHeaderMouseLeave(e.Column, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseLeave�¼�
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        protected void RaiseColumnHeaderMouseLeave(int index)
        {
            if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Column column = this.ColumnModel.Columns[index];

                I3ColumnHeaderMouseEventArgs mhea = new I3ColumnHeaderMouseEventArgs(column, this, index, this.ColumnHeaderClientRect(index));

                this.OnColumnHeaderMouseLeave(mhea);
            }
        }



        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseLeave�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseLeave(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseLeave(e);
                }

                if (RowHeaderMouseLeave != null)
                {
                    RowHeaderMouseLeave(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseLeave�¼�
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        protected void RaiseRowHeaderMouseLeave(int index)
        {
            if (index < 0 || this.TableModel == null || index >= this.TableModel.Rows.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Row row = this.TableModel.Rows[index];

                I3RowHeaderMouseEventArgs mhea = new I3RowHeaderMouseEventArgs(row, this, index, this.RowHeaderClientRect(index));

                this.OnRowHeaderMouseLeave(mhea);
            }
        }

        #endregion

        #region MouseUp

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseUp�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseUp(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseUp(e);
                }

                if (ColumnHeaderMouseUp != null)
                {
                    ColumnHeaderMouseUp(e.Column, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseUp�¼�
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseColumnHeaderMouseUp(int index, MouseEventArgs e)
        {
            if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Column column = this.ColumnModel.Columns[index];

                I3ColumnHeaderMouseEventArgs mhea = new I3ColumnHeaderMouseEventArgs(column, this, index, this.ColumnHeaderClientRect(index), e);

                this.OnColumnHeaderMouseUp(mhea);
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseUp�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseUp(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseUp(e);
                }

                if (RowHeaderMouseUp != null)
                {
                    RowHeaderMouseUp(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseUp�¼�
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseRowHeaderMouseUp(int index, MouseEventArgs e)
        {
            if (index < 0 || this.TableModel == null || index >= this.TableModel.Rows.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Row row = this.TableModel.Rows[index];

                I3RowHeaderMouseEventArgs mhea = new I3RowHeaderMouseEventArgs(row, this, index, this.RowHeaderClientRect(index), e);

                this.OnRowHeaderMouseUp(mhea);
            }
        }

        #endregion

        #region MouseDown

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseDown�¼�
        /// Raises the ColumnHeaderMouseDown event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseDown(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseDown(e);
                }

                if (ColumnHeaderMouseDown != null)
                {
                    ColumnHeaderMouseDown(e.Column, e);
                }
            }
        }

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseDown�¼�
        /// Raises a ColumnMouseDown event for the Column header at the specified colunm 
        /// index position
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseColumnHeaderMouseDown(int index, MouseEventArgs e)
        {
            if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Column column = this.ColumnModel.Columns[index];

                I3ColumnHeaderMouseEventArgs mhea = new I3ColumnHeaderMouseEventArgs(column, this, index, this.ColumnHeaderClientRect(index), e);

                this.OnColumnHeaderMouseDown(mhea);
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseDown�¼�
        /// Raises the RowHeaderMouseDown event
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseDown(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseDown(e);
                }

                if (RowHeaderMouseDown != null)
                {
                    RowHeaderMouseDown(e.Row, e);
                }
            }
        }
        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseDown�¼�
        /// Raises a RowMouseDown event for the Row header at the specified row 
        /// index position
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseRowHeaderMouseDown(int index, MouseEventArgs e)
        {
            if (index < 0 || this.TableModel == null || index >= this.TableModel.Rows.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Row row = this.TableModel.Rows[index];

                I3RowHeaderMouseEventArgs mhea = new I3RowHeaderMouseEventArgs(row, this, index, this.RowHeaderClientRect(index), e);

                this.OnRowHeaderMouseDown(mhea);
            }
        }

        #endregion

        #region MouseMove

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseMove�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseMove(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseMove(e);
                }

                if (ColumnHeaderMouseMove != null)
                {
                    ColumnHeaderMouseMove(e.Column, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseMove�¼�
        /// index position
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseColumnHeaderMouseMove(int index, MouseEventArgs e)
        {
            if (index < 0 || this.ColumnModel == null || index >= this.ColumnModel.Columns.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Column column = this.ColumnModel.Columns[index];

                I3ColumnHeaderMouseEventArgs mhea = new I3ColumnHeaderMouseEventArgs(column, this, index, this.ColumnHeaderClientRect(index), e);

                this.OnColumnHeaderMouseMove(mhea);
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseMove�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseMove(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnMouseMove(e);
                }

                if (RowHeaderMouseMove != null)
                {
                    RowHeaderMouseMove(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseMove�¼�
        /// index position
        /// </summary>
        /// <param name="index">The index of the column to recieve the event</param>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected void RaiseRowHeaderMouseMove(int index, MouseEventArgs e)
        {
            if (index < 0 || this.TableModel == null || index >= this.TableModel.Rows.Count)
            {
                return;
            }

            if (this.HeaderRenderer != null)
            {
                I3Row row = this.TableModel.Rows[index];

                I3RowHeaderMouseEventArgs mhea = new I3RowHeaderMouseEventArgs(row, this, index, this.RowHeaderClientRect(index), e);

                this.OnRowHeaderMouseMove(mhea);
            }
        }


        /// <summary>
        /// �����ȵ��У����׳�ColumnHeaderMouseLeave�¼�
        /// </summary>
        internal void ResetHotColumn()
        {
            if (this.hotColumn != -1)
            {
                this.ResetMouseEventArgs();

                int oldHotColumn = this.hotColumn;
                this.hotColumn = -1;

                this.RaiseColumnHeaderMouseLeave(oldHotColumn);
            }
        }

        /// <summary>
        /// �����ȵ��У����׳�RowHeaderMouseLeave�¼�
        /// </summary>
        internal void ResetHotRow()
        {
            if (this.hotRow != -1)
            {
                this.ResetMouseEventArgs();

                int oldHotRow = this.hotRow;
                this.hotRow = -1;

                this.RaiseRowHeaderMouseLeave(oldHotRow);
            }
        }

        #endregion

        #region MouseHover

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderMouseHover�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseHover(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (ColumnHeaderMouseHover != null)
                {
                    ColumnHeaderMouseHover(e.Column, e);
                }
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderMouseHover�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseHover(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (RowHeaderMouseHover != null)
                {
                    RowHeaderMouseHover(e.Row, e);
                }
            }
        }

        #endregion

        #region Click

        /// <summary>
        /// ���ô˺������׳�ColumnHeaderClick�¼�
        /// ���¼������������Ϣ
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseClick(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnClick(e);
                }

                if (ColumnHeaderClick != null)
                {
                    ColumnHeaderClick(e.Column, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderDoubleClick�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnColumnHeaderMouseDoubleClick(I3ColumnHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnDoubleClick(e);
                }

                if (ColumnHeaderDoubleClick != null)
                {
                    ColumnHeaderDoubleClick(e.Column, e);
                }
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderClick�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseClick(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnClick(e);
                }

                if (RowHeaderClick != null)
                {
                    RowHeaderClick(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeaderDoubleClick�¼�
        /// </summary>
        /// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
        protected virtual void OnRowHeaderMouseDoubleClick(I3RowHeaderMouseEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (this.HeaderRenderer != null)
                {
                    this.HeaderRenderer.OnDoubleClick(e);
                }

                if (RowHeaderDoubleClick != null)
                {
                    RowHeaderDoubleClick(e.Row, e);
                }
            }
        }

        #endregion

        #endregion

        #region ColumnModel

        /// <summary>
        /// ���ô˺������׳�ColumnModelChanged�¼�
        /// Raises the ColumnModelChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected virtual void OnColumnModelChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (ColumnModelChanged != null)
                {
                    ColumnModelChanged(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnAdded�¼�
        /// Raises the ColumnAdded event
        /// </summary>
        /// <param name="e">A ColumnModelEventArgs that contains the event data</param>
        protected internal virtual void OnColumnAdded(I3ColumnModelEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (ColumnAdded != null)
                {
                    ColumnAdded(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnRemoved�¼�
        /// Raises the ColumnRemoved event
        /// </summary>
        /// <param name="e">A ColumnModelEventArgs that contains the event data</param>
        protected internal virtual void OnColumnRemoved(I3ColumnModelEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (ColumnRemoved != null)
                {
                    ColumnRemoved(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�ColumnHeaderHeightChanged�¼�
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected internal virtual void OnColumnHeaderHeightChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (ColumnHeaderHeightChanged != null)
                {
                    ColumnHeaderHeightChanged(this, e);
                }
            }
        }

        /// <summary>
        /// ���ô˺������׳�RowHeaderWidthChanged�¼�
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected internal virtual void OnRowHeaderWidthChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (RowHeaderWidthChanged != null)
                {
                    RowHeaderWidthChanged(this, e);
                }
            }
        }

        #endregion

        #region Editing

        /// <summary>
        /// ���ô˺������׳�BeginEditing�¼�
        /// Raises the BeginEditing event
        /// </summary>
        /// <param name="e">A CellEditEventArgs that contains the event data</param>
        protected internal virtual void OnBeginEditing(I3CellEditEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (BeginEditing != null)
                {
                    BeginEditing(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�EditingStopped�¼�
        /// Raises the EditingStopped event
        /// </summary>
        /// <param name="e">A CellEditEventArgs that contains the event data</param>
        protected internal virtual void OnEditingStopped(I3CellEditEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (EditingStopped != null)
                {
                    EditingStopped(e.Cell, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�EditingCancelled�¼�
        /// Raises the EditingCancelled event
        /// </summary>
        /// <param name="e">A CellEditEventArgs that contains the event data</param>
        protected internal virtual void OnEditingCancelled(I3CellEditEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (EditingCancelled != null)
                {
                    EditingCancelled(e.Cell, e);
                }
            }
        }

        #endregion

        #region Focus

        /// <summary>
        /// ���ô˺������׳�GotFocus�¼�
        /// Raises the GotFocus event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (this.FocusedCell.IsEmpty)
            {
                //CellPos p = this.FindNextVisibleEnabledCell(this.FocusedCell, true, true, true, true);

                //if (this.IsValidCell(p))
                //{
                //    this.FocusedCell = p;
                //}
            }
            else
            {
                this.RaiseCellGotFocus(this.FocusedCell);
            }

            if (this.SelectedIndicies.Length > 0)
            {
                this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header);
            }

            base.OnGotFocus(e);
        }


        /// <summary>
        /// ���ô˺������׳�LostFocus�¼�
        /// Raises the LostFocus event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (!this.FocusedCell.IsEmpty)
            {
                this.RaiseCellLostFocus(this.FocusedCell);
            }

            if (this.SelectedIndicies.Length > 0)
            {
                this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header);
            }

            base.OnLostFocus(e);
        }

        #endregion

        #region Keys

        #region KeyDown

        /// <summary>
        /// ���ô˺������׳�KeyDown�¼�
        /// Raises the KeyDown event
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (this.IsValidCell(this.FocusedCell))
            {
                #region this.IsValidCell(this.FocusedCell)
                if (this.IsReservedKey(e.KeyData))
                {
                    Keys key = e.KeyData & Keys.KeyCode;

                    if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
                    {
                        #region up down left rigth
                        I3CellPos nextCell;

                        if (key == Keys.Up)
                        {
                            nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, this.FocusedCell.Row > 0, false, false, false);
                        }
                        else if (key == Keys.Down)
                        {
                            nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, this.FocusedCell.Row < this.RowsCount - 1, true, false, false);
                        }
                        else if (key == Keys.Left)
                        {
                            nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, false, false, false, true);
                        }
                        else
                        {
                            nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, false, true, false, true);
                        }

                        if (nextCell != I3CellPos.Empty)
                        {
                            this.FocusedCell = nextCell;

                            if ((e.KeyData & Keys.Modifiers) == Keys.Shift && this.MultiSelect)
                            {
                                this.TableModel.Selections.AddShiftSelectedCell(this.FocusedCell);
                            }
                            else
                            {
                                this.TableModel.Selections.SelectCell(this.FocusedCell);
                            }
                        }
                        #endregion
                    }
                    else if (e.KeyData == Keys.PageUp)
                    {
                        #region PageUp
                        if (this.RowsCount > 0)
                        {
                            I3CellPos nextCell;

                            if (!this.VScroll)
                            {
                                nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(0, this.FocusedCell.Column), true, true, true, false);
                            }
                            else
                            {
                                if (this.FocusedCell.Row > this.vScrollBar.Value && this.TableModel[this.vScrollBar.Value, this.FocusedCell.Column].Enabled)
                                {
                                    nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(this.vScrollBar.Value, this.FocusedCell.Column), true, true, true, false);
                                }
                                else
                                {
                                    nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(Math.Max(-1, this.vScrollBar.Value - (this.vScrollBar.LargeChange - 1)), this.FocusedCell.Column), true, true, true, false);
                                }
                            }

                            if (nextCell != I3CellPos.Empty)
                            {
                                this.FocusedCell = nextCell;

                                this.TableModel.Selections.SelectCell(this.FocusedCell);
                            }
                        }
                        #endregion
                    }
                    else if (e.KeyData == Keys.PageDown)
                    {
                        #region PageDown

                        if (this.RowsCount > 0)
                        {
                            I3CellPos nextCell;

                            if (!this.VScroll || this.vScrollBar.Value + this.vScrollBar.LargeChange >= this.RowsCount)
                            {
                                nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(this.RowsCount - 1, this.FocusedCell.Column), true, false, true, false);
                            }
                            else
                            {
                                if (this.FocusedCell.Row < this.vScrollBar.Value + this.vScrollBar.LargeChange)
                                {
                                    if (this.FocusedCell.Row == (this.vScrollBar.Value + this.vScrollBar.LargeChange) - 1 &&
                                        this.RowClientRect(this.vScrollBar.Value + this.vScrollBar.LargeChange).Bottom > this.ClientRectWithOutBorder_ScrollBar_Header.Bottom)
                                    {
                                        nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(Math.Min(this.RowsCount - 1, this.FocusedCell.Row - 1 + this.vScrollBar.LargeChange), this.FocusedCell.Column), true, false, true, false);
                                    }
                                    else
                                    {
                                        nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(this.vScrollBar.Value + this.vScrollBar.LargeChange - 1, this.FocusedCell.Column), true, false, true, false);
                                    }
                                }
                                else
                                {
                                    nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(Math.Min(this.RowsCount - 1, this.FocusedCell.Row + this.vScrollBar.LargeChange), this.FocusedCell.Column), true, false, true, false);
                                }
                            }

                            if (nextCell != I3CellPos.Empty)
                            {
                                this.FocusedCell = nextCell;

                                this.TableModel.Selections.SelectCell(this.FocusedCell);
                            }
                        }
                        #endregion
                    }
                    else if (e.KeyData == Keys.Home || e.KeyData == Keys.End)
                    {
                        #region Home or End
                        if (this.RowsCount > 0)
                        {
                            I3CellPos nextCell;

                            if (e.KeyData == Keys.Home)
                            {
                                nextCell = this.FindNextVisibleEnabledCell(I3CellPos.Empty, true, true, true, true);
                            }
                            else
                            {
                                nextCell = this.FindNextVisibleEnabledCell(new I3CellPos(this.RowsCount - 1, this.TableModel.Rows[this.RowsCount - 1].Cells.Count), true, false, true, true);
                            }

                            if (nextCell != I3CellPos.Empty)
                            {
                                this.FocusedCell = nextCell;

                                this.TableModel.Selections.SelectCell(this.FocusedCell);
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region EditCell
                    // check if we can start editing with the custom edit key
                    if (e.KeyData == this.CustomEditKey &&
                        (this.EditStartAction == I3EditStartAction.CustomKey || this.EditStartAction == I3EditStartAction.DoubleClick_CustomKey))
                    {
                        this.EditCell(this.FocusedCell);

                        return;
                    }

                    if (this.EditStartAction == I3EditStartAction.DataInputKey || this.EditStartAction == I3EditStartAction.DoubleClick_DataInputKey)
                    {
                        int keyValue = (int)e.KeyCode;
                        bool controlFlag = (keyValue >= 91 && keyValue <= 95) || (keyValue >= 112 && keyValue <= 123) || (keyValue <= 47 && keyValue != 32);
                        if (!controlFlag)
                        {
                            II3CellEditor editor = this.ColumnModel.GetCellEditor(this.FocusedCell.Column);
                            if (editor != null)
                            {
                                Control control = editor.GetDataInputControl();
                                if (control != null)
                                {
                                    this.EditCell(this.FocusedCell);
                                    if (this.EditingCellEditor != null)
                                    {
                                        control = this.EditingCellEditor.GetDataInputControl();
                                        if (control != null)
                                        {
                                            I3NativeMethods.PostMessage(control.Handle, (int)I3WindowMessage.WM_KEYDOWN, (int)e.KeyCode, 0);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // send all other key events to the cell's renderer
                    // for further processing
                    this.RaiseCellKeyDown(this.FocusedCell, e);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region !this.IsValidCell(this.FocusedCell)
                if (this.FocusedCell == I3CellPos.Empty)
                {
                    Keys key = e.KeyData & Keys.KeyCode;

                    if (this.IsReservedKey(e.KeyData))
                    {
                        if (key == Keys.Down || key == Keys.Right)
                        {
                            I3CellPos nextCell;

                            if (key == Keys.Down)
                            {
                                nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, true, true, true, false);
                            }
                            else
                            {
                                nextCell = this.FindNextVisibleEnabledCell(this.FocusedCell, false, true, true, true);
                            }

                            if (nextCell != I3CellPos.Empty)
                            {
                                this.FocusedCell = nextCell;

                                if ((e.KeyData & Keys.Modifiers) == Keys.Shift && this.MultiSelect)
                                {
                                    this.TableModel.Selections.AddShiftSelectedCell(this.FocusedCell);
                                }
                                else
                                {
                                    this.TableModel.Selections.SelectCell(this.FocusedCell);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }

        #endregion

        #region KeyUp

        /// <summary>
        /// ���ô˺������׳�KeyUp�¼�
        /// Raises the KeyUp event
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!this.IsReservedKey(e.KeyData))
            {
                // 
                if (e.KeyData == this.CustomEditKey && this.EditStartAction == I3EditStartAction.CustomKey)
                {
                    return;
                }

                // send all other key events to the cell's renderer
                // for further processing
                this.RaiseCellKeyUp(this.FocusedCell, e);
            }
        }

        #endregion

        #endregion

        #region Layout

        /// <summary>
        /// ���ô˺������׳�Layout�¼�
        /// Raises the Layout event
        /// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!this.IsHandleCreated || this.init)
            {
                return;
            }

            base.OnLayout(levent);

            this.UpdateScrollBars();
        }

        #endregion

        #region Mouse

        #region MouseUp

        /// <summary>
        /// ���ô˺������׳�MouseUp�¼�
        /// Raises the MouseUp event
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!this.CanRaiseEvents)
            {
                return;
            }

            // ����״̬
            this.CalcTableState(e.X, e.Y);

            I3TableRegion region = this.HitTest(e.X, e.Y);

            #region ����MouseUp�¼�
            if (region == I3TableRegion.ColumnHeader)
            {
                int c = this.ColumnIndexAtClient(e.X, e.Y);
                if (c != -1 && this.ColumnModel.Columns[c].Enabled && this.resizingColumnIndex == -1)
                {
                    this.RaiseColumnHeaderMouseUp(c, e);
                }
            }
            else if (region == I3TableRegion.RowHeader)
            {
                int r = this.RowIndexAtClient(e.X, e.Y);
                if (r != -1 && this.TableModel.Rows[r].Enabled && this.resizingRowIndex == -1)
                {
                    this.RaiseRowHeaderMouseUp(r, e);
                }
            }
            #endregion

            #region ֻ�����������
            if (e.Button == MouseButtons.Left)
            {
                #region ���֮ǰ����갴������һ��Cell�ϣ���Ϊ��Cell����MouseUp�¼�
                if (!this.LastMouseDownCell.IsEmpty)
                {
                    if (this.IsValidCell(this.LastMouseDownCell))
                    {
                        this.RaiseCellMouseUp(this.LastMouseDownCell, e);
                    }

                    // reset the lastMouseDownCell
                    this.lastMouseDownCell = I3CellPos.Empty;
                }
                #endregion

                #region ������п�����Ľ������������²���Table
                if (this.resizingColumnIndex != -1)
                {
                    if (this.resizingColumnWidth != -1)
                    {
                        this.DrawVerticalReversibleLine(this.ColumnClientRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);
                        this.ColumnModel.Columns[this.resizingColumnIndex].Width = this.resizingColumnWidth;
                    }


                    this.resizingColumnIndex = -1;
                    this.resizingColumnWidth = -1;

                    this.UpdateScrollBars();
                    this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, true);
                }
                #endregion

                #region check if the mouse was released in a column header
                if (region == I3TableRegion.ColumnHeader)
                {
                    int column = this.ColumnIndexAtClient(e.X, e.Y);

                    // if we are in the header, check if we are in the pressed column
                    if (this.pressedColumn != -1)
                    {
                        if (this.pressedColumn == column)
                        {
                            if (this.hotColumn != -1 && this.hotColumn != column)
                            {
                                this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Normal;
                            }

                            this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = I3ColumnState.Hot;

                            //this.RaiseColumnHeaderMouseUp(column, e);

                            // only sort the column if we have rows to sort
                            if (this.ColumnModel.Columns[this.pressedColumn].Sortable)
                            {
                                if (this.TableModel != null && this.TableModel.Rows.Count > 0)
                                {
                                    this.Sort(this.pressedColumn);
                                }
                            }
                        }
                        else
                        {
                            if (this.canMoveColumn)
                            {
                                //�ƶ���
                                if (column == -1)//��ColumnHeader��ʱ����ִ�е�������������ֵ��-1����϶����ƶ����������е����ұ�
                                {
                                    column = this.columnModel.Columns.Count - 1;
                                }
                                if (this.pressedColumn != column)
                                {
                                    this.columnModel.MoveColumn(this.pressedColumn, column);
                                }
                            }
                        }



                        this.pressedColumn = -1;

                        this.Invalidate(this.ColumnHeaderClientRectangle, false);
                    }

                    return;
                }
                #endregion

                #region the mouse wasn't released in a column header, so if we have a pressed column then we need to make it unpressed
                if (this.pressedColumn != -1)
                {
                    this.pressedColumn = -1;

                    this.Invalidate(this.ColumnHeaderClientRectangle, false);
                }
                #endregion



                #region ������иߵ����Ľ������������²���Table
                if (this.resizingRowIndex != -1)
                {
                    if (this.resizingRowHeight != -1)
                    {
                        this.DrawHorizontalReversibleLine(this.RowClientRect(this.resizingRowIndex).Top + this.resizingRowHeight);
                        this.TableModel.Rows[this.resizingRowIndex].Height = this.resizingRowHeight;
                    }


                    this.resizingRowIndex = -1;
                    this.resizingRowHeight = -1;

                    this.UpdateScrollBars();
                    this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, true);
                }
                #endregion

                #region check if the mouse was released in a row header
                if (region == I3TableRegion.RowHeader)
                {
                    int row = this.RowIndexAtClient(e.X, e.Y);

                    // if we are in the header, check if we are in the pressed column
                    if (this.pressedRow != -1)
                    {
                        if (this.pressedRow == row)
                        {
                            if (this.hotRow != -1 && this.hotRow != row)
                            {
                                this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Normal;
                            }

                            this.TableModel.Rows[this.pressedRow].InternalRowState = I3RowState.Hot;

                            //this.RaiseRowHeaderMouseUp(row, e);
                        }

                        this.pressedRow = -1;

                        // only sort the column if we have rows to sort
                        //if (this.ColumnModel.Columns[column].Sortable)
                        //{
                        //    if (this.TableModel != null && this.TableModel.Rows.Count > 0)
                        //    {
                        //        this.Sort(column);
                        //    }
                        //}

                        this.Invalidate(this.RowHeaderClientRectangle, false);
                    }

                    return;
                }
                #endregion

                #region the mouse wasn't released in a row header, so if we have a pressed column then we need to make it unpressed
                if (this.pressedRow != -1)
                {
                    this.pressedRow = -1;

                    this.Invalidate(this.RowHeaderClientRectangle, false);
                }
                #endregion
            }
            #endregion
        }

        #endregion

        #region MouseDown

        /// <summary>
        /// ���ô˺������׳�MouseDown�¼�
        /// Raises the MouseDown event
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!this.CanRaiseEvents)
            {
                return;
            }

            this.CalcTableState(e.X, e.Y);
            I3TableRegion region = this.HitTest(e.X, e.Y);

            int row = this.RowIndexAtClient(e.X, e.Y);
            int column = this.ColumnIndexAtClient(e.X, e.Y);

            //������ڱ༭״̬���������������ط���ȡ���༭ 
            if (this.IsEditing)
            {
                if (this.EditingCell.Row != row || this.EditingCell.Column != column)
                {
                    this.Focus();

                    if (region == I3TableRegion.ColumnHeader && e.Button != MouseButtons.Right)
                    {
                        return;
                    }
                }
            }

            try
            {
                #region RowColumnHeader
                if (region == I3TableRegion.RowColumnHeader)
                {
                    if (this.ColumnHeaderContextMenu.Enabled)
                    {
                        this.ColumnHeaderContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                    return;
                }
                #endregion

                #region ColumnHeader

                if (region == I3TableRegion.ColumnHeader)
                {
                    #region �����˵�
                    //if (e.Button == MouseButtons.Right && this.ColumnHeaderContextMenu.Enabled)
                    //{
                    //    this.ColumnHeaderContextMenu.Show(this, new Point(e.X, e.Y));
                    //    return;
                    //}
                    #endregion

                    #region δ������л��ߵ������Enabled=false���˳�
                    if (column == -1 || !this.ColumnModel.Columns[column].Enabled)
                    {
                        return;
                    }
                    #endregion

                    #region �������
                    if (e.Button == MouseButtons.Left)
                    {
                        this.FocusedCell = new I3CellPos(-1, -1);

                        // don't bother going any further if the user  double clicked
                        if (e.Clicks > 1)
                        {
                            if (e.Clicks == 2)
                            {
                                #region �Զ����
                                if (this.TableState == I3TableState.ColumnResizing)
                                {
                                    Rectangle columnRect = this.ColumnHeaderClientRect(column);
                                    int x = e.X;

                                    if (x <= columnRect.Left + I3Column.ResizePadding_Const)
                                    {
                                        column = this.ColumnModel.PreviousVisibleColumn(column);
                                    }
                                    if (column >= 0 && column <= this.columnModel.Columns.Count - 1)
                                    {
                                        float autoWidth = 0;
                                        #region �����Զ����
                                        foreach (I3Row tmpRow in this.tableModel.Rows)
                                        {
                                            if (RowClientRect(tmpRow).IntersectsWith(this.ClientRectangle))//ֻ����ʾ���вŲ������
                                            {
                                                if (column <= tmpRow.Cells.Count - 1)
                                                {
                                                    autoWidth = Math.Max(autoWidth, tmpRow.Cells[column].NeedWidth);
                                                }
                                            }
                                        }
                                        autoWidth = Math.Max(autoWidth, this.columnModel.Columns[column].NeedWidth);
                                        #endregion
                                        this.ColumnModel.Columns[column].Width = (int)Math.Floor(autoWidth);
                                    }
                                }
                                #endregion
                            }
                            return;
                        }

                        //this.RaiseColumnHeaderMouseDown(column, e);

                        if (this.TableState == I3TableState.ColumnResizing)
                        {
                            #region �����п�״̬
                            Rectangle columnRect = this.ColumnHeaderClientRect(column);
                            //int x = this.ClientToDisplay(e.X, e.Y).X;
                            int x = e.X;

                            if (x <= columnRect.Left + I3Column.ResizePadding_Const)
                            {
                                //column--;
                                column = this.ColumnModel.PreviousVisibleColumn(column);
                            }

                            this.resizingColumnIndex = column;

                            if (this.resizingColumnIndex != -1)
                            {
                                this.resizingColumnAnchor = this.ColumnHeaderClientRect(column).Left;
                                this.resizingColumnOffset = x - (this.resizingColumnAnchor + this.ColumnModel.Columns[column].Width);
                            }
                            #endregion
                        }
                        else
                        {
                            #region ����
                            if (this.ColumnHeaderStyle != ColumnHeaderStyle.Clickable /*|| !this.ColumnModel.Columns[column].Sortable*/) //��������Ҳ�а���״̬���ſ��ƶ���
                            {
                                return;
                            }

                            if (column == -1)
                            {
                                return;
                            }

                            if (this.pressedColumn != -1)
                            {
                                this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = I3ColumnState.Normal;
                            }

                            this.pressedColumn = column;
                            this.ColumnModel.Columns[column].InternalColumnState = I3ColumnState.Pressed;
                            #endregion
                        }

                        return;
                    }
                    #endregion
                }

                #endregion

                #region RowHeader

                if (region == I3TableRegion.RowHeader)
                {
                    #region �����˵�  ��ʱδʵ��
                    //if (e.Button == MouseButtons.Right && this.ColumnHeaderContextMenu.Enabled)
                    //{
                    //    this.ColumnHeaderContextMenu.Show(this, new Point(e.X, e.Y));
                    //    return;
                    //}
                    #endregion

                    #region δ������л��ߵ������Enabled=false���˳�
                    if (row == -1 || !this.TableModel.Rows[row].Enabled)
                    {
                        return;
                    }
                    #endregion

                    #region �������
                    if (e.Button == MouseButtons.Left)
                    {
                        this.FocusedCell = new I3CellPos(-1, -1);

                        // don't bother going any further if the user  double clicked
                        if (e.Clicks > 1)
                        {
                            if (e.Clicks == 2)
                            {
                                #region �Զ��߶�
                                if (this.TableState == I3TableState.RowResizing)
                                {
                                    Rectangle rowRect = this.RowHeaderClientRect(row);
                                    int y = e.Y;

                                    if (y <= rowRect.Top + I3Row.ResizePadding_Const)
                                    {
                                        row--;
                                        while (!this.TableModel.Rows[row].Visible && row != this.TopIndex)
                                        {
                                            row--;
                                        }
                                    }
                                    if (row >= 0 && row <= this.tableModel.Rows.Count - 1)
                                    {
                                        float autoHeight = 0;
                                        #region �����Զ��߶�
                                        foreach (I3Cell tmpCell in this.tableModel.Rows[row].Cells)
                                        {
                                            //if (CellClientRect(tmpCell).IntersectsWith(this.ClientRectangle))  //����ʾ����Ҳ�������
                                            //{
                                            autoHeight = Math.Max(autoHeight, tmpCell.NeedHeight);
                                            //}
                                        }
                                        autoHeight = Math.Max(autoHeight, this.tableModel.Rows[row].NeedHeight);
                                        #endregion
                                        this.tableModel.Rows[row].Height = (int)Math.Floor(autoHeight);
                                    }
                                }
                                #endregion
                            }
                            return;
                        }

                        //this.RaiseRowHeaderMouseDown(row, e);

                        if (this.TableState == I3TableState.RowResizing)
                        {
                            #region �����и�״̬
                            Rectangle rowRect = this.RowHeaderClientRect(row);
                            int y = e.Y;

                            if (y <= rowRect.Top + I3Row.ResizePadding_Const)
                            {
                                row--;
                                while (!this.TableModel.Rows[row].Visible && row != this.TopIndex)
                                {
                                    row--;
                                }
                            }

                            this.resizingRowIndex = row;

                            if (this.resizingRowIndex != -1)
                            {
                                this.resizingRowAnchor = rowRect.Top;
                                this.resizingRowOffset = y - (this.resizingRowAnchor + this.TableModel.Rows[row].Height);
                            }
                            #endregion
                        }
                        else
                        {
                            #region ����  �в���Ҫ������
                            //if (this.ColumnHeaderStyle != ColumnHeaderStyle.Clickable || !this.ColumnModel.Columns[column].Sortable)
                            //{
                            //    return;
                            //}

                            if (row == -1)
                            {
                                return;
                            }

                            if (this.pressedRow != -1)
                            {
                                this.TableModel.Rows[this.pressedRow].InternalRowState = I3RowState.Normal;
                            }

                            this.pressedRow = row;
                            this.TableModel.Rows[row].InternalRowState = I3RowState.Pressed;
                            //this.Invalidate(this.RowHeaderClientRect(row));
                            #endregion
                        }

                        return;
                    }
                    #endregion
                }
                #endregion

                #region Cells

                if (region == I3TableRegion.Cells)
                {
                    if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                    {
                        return;
                    }

                    if (!this.IsValidCell(row, column) || !this.IsCellEnabled(row, column))
                    {
                        // clear selections
                        this.TableModel.Selections.Clear();

                        return;
                    }

                    //2016.12.28�����������Ƿ�������Ҽ�ѡ���ѡ��
                    if (e.Button == MouseButtons.Left || this.SelectByRightButton)
                    {
                        this.FocusedCell = new I3CellPos(row, column);
                    }

                    // don't bother going any further if the user 
                    // double clicked or we're not allowed to select
                    if (e.Clicks > 1 || !this.AllowSelection)
                    {
                        return;
                    }

                    this.lastMouseDownCell.Row = row;
                    this.lastMouseDownCell.Column = column;

                    //
                    this.RaiseCellMouseDown(new I3CellPos(row, column), e);

                    if (!this.ColumnModel.Columns[column].Selectable)
                    {
                        return;
                    }

                    //

                    if ((ModifierKeys & Keys.Shift) == Keys.Shift && this.MultiSelect)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            return;
                        }

                        this.TableModel.Selections.AddShiftSelectedCell(row, column);

                        return;
                    }

                    if ((ModifierKeys & Keys.Control) == Keys.Control && this.MultiSelect)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            return;
                        }

                        if (this.TableModel.Selections.IsCellSelected(row, column))
                        {
                            this.TableModel.Selections.RemoveCell(row, column);
                        }
                        else
                        {
                            this.TableModel.Selections.AddCell(row, column);
                        }

                        return;
                    }

                    //2016.12.28�����������Ƿ�������Ҽ�ѡ���ѡ��
                    if (e.Button == MouseButtons.Left || this.SelectByRightButton)
                    {
                        this.TableModel.Selections.SelectCell(row, column);
                    }
                }

                #endregion
            }
            finally
            {
                #region ����MouseDown�¼�
                if (region == I3TableRegion.ColumnHeader)
                {
                    if (column != -1 && this.ColumnModel.Columns[column].Enabled && this.resizingColumnIndex == -1)
                    {
                        this.RaiseColumnHeaderMouseDown(column, e);
                    }
                }
                else if (region == I3TableRegion.RowHeader)
                {
                    if (row != -1 && this.TableModel.Rows[row].Enabled && this.resizingRowIndex == -1)
                    {
                        this.RaiseRowHeaderMouseDown(row, e);
                    }
                }
                #endregion
            }
        }

        #endregion

        #region MouseMove

        /// <summary>
        /// ���ô˺������׳�MouseMove�¼�
        /// Raises the MouseMove event
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // don't go any further if the table is editing
            if (this.TableState == I3TableState.Editing)
            {
                return;
            }

            // if the left mouse button is down, check if the LastMouseDownCell 
            // references a valid cell.  if it does, send the mouse move message 
            // to the cell and then exit (this will stop other cells/headers 
            // from getting the mouse move message even if the mouse is over 
            // them - this seems consistent with the way windows does it for 
            // other controls)
            if (e.Button == MouseButtons.Left)
            {
                if (!this.LastMouseDownCell.IsEmpty)
                {
                    if (this.IsValidCell(this.LastMouseDownCell))
                    {
                        this.RaiseCellMouseMove(this.LastMouseDownCell, e);

                        //return;
                    }
                }
            }

            // are we resizing a column?
            if (this.resizingColumnIndex != -1)
            {
                if (this.resizingColumnWidth != -1)
                {
                    this.DrawVerticalReversibleLine(this.ColumnClientRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);
                }

                // calculate the new width for the column
                //int width = this.ClientToDisplay(e.X, e.Y).X - this.resizingColumnAnchor - this.resizingColumnOffset;
                int width = e.X - this.resizingColumnAnchor - this.resizingColumnOffset;

                // make sure the new width isn't smaller than the minimum allowed
                // column width, or larger than the maximum allowed column width
                if (width < I3Column.MinimumWidth_Const)
                {
                    width = I3Column.MinimumWidth_Const;
                }
                else if (width > I3Column.MaximumWidth_Const)
                {
                    width = I3Column.MaximumWidth_Const;
                }

                this.resizingColumnWidth = width;

                this.DrawVerticalReversibleLine(this.ColumnClientRect(this.resizingColumnIndex).Left + this.resizingColumnWidth);

                return;
            }

            // are we resizing a row?
            if (this.resizingRowIndex != -1)
            {
                if (this.resizingRowHeight != -1)
                {
                    this.DrawHorizontalReversibleLine(this.RowClientRect(this.resizingRowIndex).Top + this.resizingRowHeight);
                }

                int height = e.Y - this.resizingRowAnchor - this.resizingRowOffset;

                // make sure the new width isn't smaller than the minimum allowed
                // column width, or larger than the maximum allowed column width
                if (height < I3Row.MinimumHeight_Const)
                {
                    height = I3Row.MinimumHeight_Const;
                }
                else if (height > I3Row.MaximumHeight_Const)
                {
                    height = I3Row.MaximumHeight_Const;
                }

                this.resizingRowHeight = height;

                this.DrawHorizontalReversibleLine(this.RowHeaderClientRect(this.resizingRowIndex).Top + this.resizingRowHeight);

                return;
            }

            // work out the potential state of play
            this.CalcTableState(e.X, e.Y);

            I3TableRegion hitTest = this.HitTest(e.X, e.Y);

            #region ColumnHeader

            if (hitTest == I3TableRegion.ColumnHeader)
            {
                // this next bit is pretty complicated. need to work 
                // out which column is displayed as pressed or hot 
                // (so we have the same behaviour as a themed ListView
                // in Windows XP)

                int column = this.ColumnIndexAtClient(e.X, e.Y);

                // if this isn't the current hot column, reset the
                // hot columns state to normal and set this column
                // to be the hot column
                if (this.hotColumn != column)
                {
                    if (this.hotColumn != -1)
                    {
                        this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Normal;

                        this.RaiseColumnHeaderMouseLeave(this.hotColumn);
                    }

                    if (this.TableState != I3TableState.ColumnResizing)
                    {
                        this.hotColumn = column;

                        if (this.hotColumn != -1 && this.ColumnModel.Columns[column].Enabled)
                        {
                            this.ColumnModel.Columns[column].InternalColumnState = I3ColumnState.Hot;

                            if (this.resizingColumnIndex == -1)
                            {
                                this.RaiseColumnHeaderMouseEnter(column);
                            }
                        }
                    }
                }
                else
                {
                    if (column != -1 && this.ColumnModel.Columns[column].Enabled)
                    {
                        if (this.resizingColumnIndex == -1)
                        {
                            this.RaiseColumnHeaderMouseMove(column, e);
                        }
                    }
                }

                // if this isn't the pressed column, then the pressed columns
                // state should be set back to normal
                if (this.pressedColumn != -1 && this.pressedColumn != column)
                {
                    this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = I3ColumnState.Normal;
                    //��һ����
                    //if (columnMoveRect != Rectangle.Empty)
                    //{
                    //    ControlPaint.FillReversibleRectangle(columnMoveRect, Color.LightGray);
                    //}
                    //columnMoveRect = new Rectangle(this.PointToScreen(new Point(e.X - 40, e.Y - 10)), new Size(80, 20));
                    //ControlPaint.FillReversibleRectangle(columnMoveRect, Color.LightGray);
                }
                // else if this is the pressed column and its state is not
                // pressed, then we had better set it
                else if (column != -1 && this.pressedColumn == column && this.ColumnModel.Columns[this.pressedColumn].ColumnState != I3ColumnState.Pressed)
                {
                    this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = I3ColumnState.Pressed;
                }

                // set the cursor to a resizing cursor if necesary
                if (this.TableState == I3TableState.ColumnResizing)
                {
                    Rectangle columnRect = this.ColumnHeaderClientRect(column);
                    //int x = this.ClientToDisplay(e.X, e.Y).X;
                    int x = e.X;

                    this.Cursor = Cursors.VSplit;

                    // if the left mouse button is down, we don't want
                    // the resizing cursor so set it back to the default
                    if (e.Button == MouseButtons.Left)
                    {
                        this.Cursor = Cursors.Default;
                    }

                    // if the mouse is in the left side of the column, 
                    // the first non-hidden column to the left needs to
                    // become the hot column (so the user knows which
                    // column would be resized if a resize action were
                    // to take place
                    if (x < columnRect.Left + I3Column.ResizePadding_Const)
                    {
                        int col = column;

                        while (col != 0)
                        {
                            col--;

                            if (this.ColumnModel.Columns[col].Visible)
                            {
                                break;
                            }
                        }

                        if (col != -1)
                        {
                            if (this.ColumnModel.Columns[col].Enabled)
                            {
                                if (this.hotColumn != -1)
                                {
                                    this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Normal;
                                }

                                this.hotColumn = col;
                                this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Hot;

                                this.RaiseColumnHeaderMouseEnter(col);
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                    else
                    {
                        if (this.ColumnModel.Columns[column].Enabled)
                        {
                            // this mouse is in the right side of the column, 
                            // so this column needs to be dsiplayed hot
                            this.hotColumn = column;
                            this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Hot;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                }
                else
                {
                    // we're not in a resizing area, so make sure the cursor
                    // is the default cursor (we may have just come from a
                    // resizing area)
                    this.Cursor = Cursors.Default;
                }

                // reset the last cell the mouse was over
                this.ResetLastMouseCell();

                return;
            }

            #endregion


            #region RowHeader

            if (hitTest == I3TableRegion.RowHeader)
            {
                // this next bit is pretty complicated. need to work 
                // out which column is displayed as pressed or hot 
                // (so we have the same behaviour as a themed ListView
                // in Windows XP)
                int row = this.RowIndexAtClient(e.X, e.Y);

                //�����ǰ�в���hotrow������hotrow����ͨ״̬��״̬��ʱδʵ�֣����������õ�ǰ��Ϊhotrow
                if (this.hotRow != row)
                {
                    if (this.hotRow != -1)
                    {
                        this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Normal;

                        this.RaiseRowHeaderMouseLeave(this.hotRow);
                    }

                    if (this.TableState != I3TableState.RowResizing)
                    {
                        this.hotRow = row;

                        if (this.hotRow != -1 && this.TableModel.Rows[row].Enabled)
                        {
                            this.TableModel.Rows[row].InternalRowState = I3RowState.Hot;

                            if (this.resizingRowIndex == -1)
                            {
                                this.RaiseRowHeaderMouseEnter(row);
                            }
                        }
                    }
                }   //end if (this.hotRow != row)
                else
                {
                    if (row != -1 && this.TableModel.Rows[row].Enabled)
                    {
                        if (this.resizingRowIndex == -1)
                        {
                            this.RaiseRowHeaderMouseMove(row, e);
                        }
                    }
                }

                // if this isn't the pressed column, then the pressed columns
                // state should be set back to normal
                //�����е����ã���ʱδʵ�֣�
                if (this.pressedRow != -1 && this.pressedRow != row)
                {
                    this.TableModel.Rows[this.pressedRow].InternalRowState = I3RowState.Normal;
                }
                // else if this is the pressed column and its state is not
                // pressed, then we had better set it
                else if (row != -1 && this.pressedRow == row && this.TableModel.Rows[this.pressedRow].RowState != I3RowState.Pressed)
                {
                    this.TableModel.Rows[this.pressedRow].InternalRowState = I3RowState.Pressed;
                }

                //���ù��
                if (this.TableState == I3TableState.RowResizing)
                {
                    Rectangle rowRect = this.RowHeaderClientRect(row);
                    int y = e.Y;

                    this.Cursor = Cursors.HSplit;

                    // if the left mouse button is down, we don't want
                    // the resizing cursor so set it back to the default
                    if (e.Button == MouseButtons.Left)
                    {
                        this.Cursor = Cursors.Default;
                    }

                    // if the mouse is in the left side of the column, 
                    // the first non-hidden column to the left needs to
                    // become the hot column (so the user knows which
                    // column would be resized if a resize action were
                    // to take place
                    if (y < rowRect.Top + I3Row.ResizePadding_Const)
                    {
                        int r = row;

                        while (r != this.TopIndex)
                        {
                            r--;

                            if (this.TableModel.Rows[r].Visible)
                            {
                                break;
                            }
                        }

                        if (r != -1)
                        {
                            if (this.TableModel.Rows[r].Enabled)
                            {
                                if (this.hotRow != -1)
                                {
                                    this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Normal;
                                }

                                this.hotRow = r;
                                this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Hot;

                                this.RaiseRowHeaderMouseEnter(r);
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                    else
                    {
                        if (this.TableModel.Rows[row].Enabled)
                        {
                            // this mouse is in the right side of the column, 
                            // so this column needs to be dsiplayed hot
                            this.hotRow = row;
                            this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Hot;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                }
                else
                {
                    // we're not in a resizing area, so make sure the cursor
                    // is the default cursor (we may have just come from a
                    // resizing area)
                    this.Cursor = Cursors.Default;
                }

                // reset the last cell the mouse was over
                this.ResetLastMouseCell();

                return;
            }

            #endregion

            // we're outside of the header, so if there is a hot column,
            // it need to be reset
            if (this.hotColumn != -1)
            {
                this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Normal;

                this.ResetHotColumn();
            }

            // if there is a pressed column, its state need to beset to normal
            if (this.pressedColumn != -1)
            {
                this.ColumnModel.Columns[this.pressedColumn].InternalColumnState = I3ColumnState.Normal;
            }

            // we're outside of the header, so if there is a hot column,
            // it need to be reset
            if (this.hotRow != -1)
            {
                this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Normal;

                this.ResetHotRow();
            }

            // if there is a pressed column, its state need to beset to normal
            if (this.pressedRow != -1)
            {
                this.TableModel.Rows[this.pressedRow].InternalRowState = I3RowState.Normal;
            }

            #region Cells

            if (hitTest == I3TableRegion.Cells)
            {
                this.Cursor = Cursors.Default;
                // find the cell the mouse is over
                I3CellPos cellPos = new I3CellPos(this.RowIndexAtClient(e.X, e.Y), this.ColumnIndexAtClient(e.X, e.Y));

                if (!cellPos.IsEmpty)
                {
                    #region !cellPos.IsEmpty
                    if (cellPos != this.lastMouseCell)
                    {
                        // check if the cell exists (ie is not null)
                        if (this.IsValidCell(cellPos))
                        {
                            I3CellPos oldLastMouseCell = this.lastMouseCell;

                            if (!oldLastMouseCell.IsEmpty)
                            {
                                this.ResetLastMouseCell();
                            }

                            this.lastMouseCell = cellPos;

                            this.RaiseCellMouseEnter(cellPos);

                    #endregion
                        }
                        else
                        {
                            this.ResetLastMouseCell();

                            // make sure the cursor is the default cursor 
                            // (we may have just come from a resizing area in the header)
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        this.RaiseCellMouseMove(cellPos, e);
                        #region ��ѡ
                        if (this.IsCellEnabled(cellPos) && this.MultiSelect && this.columnModel.Columns[cellPos.Column].Selectable)
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                this.TableModel.Selections.AddShiftSelectedCell(cellPos.Row, cellPos.Column);
                            }
                        }
                    }
                        #endregion
                }
                else
                {
                    this.ResetLastMouseCell();

                    if (this.TableModel == null)
                    {
                        this.ResetToolTip();
                    }
                }

                return;
            }
            else
            {
                this.ResetLastMouseCell();

                if (!this.lastMouseDownCell.IsEmpty)
                {
                    this.RaiseCellMouseLeave(this.lastMouseDownCell);
                }

                if (this.TableModel == null)
                {
                    this.ResetToolTip();
                }

                // make sure the cursor is the default cursor 
                // (we may have just come from a resizing area in the header)
                this.Cursor = Cursors.Default;
            }

            #endregion
        }

        #endregion

        #region MouseLeave

        /// <summary>
        /// ���ô˺������׳�MouseLeave�¼�
        /// Raises the MouseLeave event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            // we're outside of the header, so if there is a hot column,
            // it needs to be reset (this shouldn't happen, but better 
            // safe than sorry ;)
            if (this.hotColumn != -1)
            {
                this.ColumnModel.Columns[this.hotColumn].InternalColumnState = I3ColumnState.Normal;

                this.ResetHotColumn();
            }

            if (this.hotRow != -1)
            {
                this.TableModel.Rows[this.hotRow].InternalRowState = I3RowState.Normal;

                this.ResetHotRow();
            }
        }

        #endregion

        #region MouseWheel

        /// <summary>
        /// ���ô˺������׳�MouseWheel�¼�
        /// Raises the MouseWheel event
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!this.Scrollable || (!this.HScroll && !this.VScroll))
            {
                return;
            }

            if (this.IsEditing)
            {
                if (this.EditingCellEditor != null && this.EditingCellEditor.HandleMouseWheel())
                {
                    return;
                }
                else
                {
                    this.Focus();
                }
            }

            if (this.VScroll)
            {
                int newVal = this.vScrollBar.Value - ((e.Delta / 120) * SystemInformation.MouseWheelScrollLines);

                if (newVal < 0)
                {
                    newVal = 0;
                }
                else if (newVal > this.vScrollBar.Maximum - this.vScrollBar.LargeChange + 1)
                {
                    newVal = this.vScrollBar.Maximum - this.vScrollBar.LargeChange + 1;
                }

                if (this.VisibleRowsHeight < this.ClientRectWithOutBorder_ScrollBar_Header.Height && newVal > this.vScrollBar.Value)
                {
                }
                else
                {
                    this.VerticalScroll(newVal);
                    this.vScrollBar.Value = newVal;
                }
            }
            else if (this.HScroll)
            {
                int newVal = this.hScrollBar.Value - ((e.Delta / 120) * I3Column.MinimumWidth_Const);

                if (newVal < 0)
                {
                    newVal = 0;
                }
                else if (newVal > this.hScrollBar.Maximum - this.hScrollBar.LargeChange)
                {
                    newVal = this.hScrollBar.Maximum - this.hScrollBar.LargeChange;
                }

                this.HorizontalScroll(newVal);
                this.hScrollBar.Value = newVal;
            }
        }

        #endregion

        #region MouseHover

        /// <summary>
        /// ���ô˺������׳�MouseHover�¼�
        /// Raises the MouseHover event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            if (this.IsValidCell(this.LastMouseCell))
            {
                this.OnCellMouseHover(new I3CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellClientRect(this.LastMouseCell)));
            }
            else if (this.hotColumn != -1 && this.resizingColumnIndex == -1)
            {
                this.OnColumnHeaderMouseHover(new I3ColumnHeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.ColumnHeaderClientRect(this.hotColumn)));
            }
            else if (this.hotRow != -1 && this.resizingRowIndex == -1)
            {
                this.OnRowHeaderMouseHover(new I3RowHeaderMouseEventArgs(this.TableModel.Rows[this.hotRow], this, this.hotRow, this.RowHeaderClientRect(this.hotRow)));
            }
        }

        #endregion

        #region Click


        /// <summary>
        /// ���ô˺������׳�Click�¼�
        /// Raises the Click event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        //protected override void OnClick(EventArgs e)
        //{
        //    base.OnClick(e);

        //    if (this.IsValidCell(this.LastMouseCell))
        //    {
        //        this.OnCellClick(new CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellClientRect(this.LastMouseCell)));
        //    }
        //    else if (this.hotColumn != -1)
        //    {
        //        this.OnColumnHeaderClick(new ColumnHeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.ColumnHeaderClientRect(this.hotColumn)));
        //    }
        //    else if (this.hotRow != -1)
        //    {
        //        this.OnRowHeaderClick(new RowHeaderMouseEventArgs(this.TableModel.Rows[this.hotRow], this, this.hotRow, this.RowHeaderClientRect(this.hotRow)));
        //    }
        //}

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //base.OnClick(e);
            base.OnMouseClick(e);

            if (this.IsValidCell(this.LastMouseCell))
            {
                this.OnCellMouseClick(new I3CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellClientRect(this.LastMouseCell), e));
            }
            else if (this.hotColumn != -1 && this.resizingColumnIndex == -1)
            {
                this.OnColumnHeaderMouseClick(new I3ColumnHeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.ColumnHeaderClientRect(this.hotColumn), e));
            }
            else if (this.hotRow != -1 && this.resizingRowIndex == -1)
            {
                this.OnRowHeaderMouseClick(new I3RowHeaderMouseEventArgs(this.TableModel.Rows[this.hotRow], this, this.hotRow, this.RowHeaderClientRect(this.hotRow), e));
            }
        }


        /// <summary>
        /// ���ô˺������׳�DoubleClick�¼�
        /// Raises the DoubleClick event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        //protected override void OnDoubleClick(EventArgs e)
        //{
        //    base.OnDoubleClick(e);

        //    if (this.IsValidCell(this.LastMouseCell))
        //    {
        //        Rectangle cellRect = this.CellClientRect(this.LastMouseCell);

        //        this.OnCellDoubleClick(new CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellClientRect(this.LastMouseCell)));
        //    }
        //    else if (this.hotColumn != -1)
        //    {
        //        this.OnColumnHeaderDoubleClick(new ColumnHeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.ColumnHeaderClientRect(this.hotColumn)));
        //    }
        //    else if (this.hotRow != -1)
        //    {
        //        this.OnRowHeaderDoubleClick(new RowHeaderMouseEventArgs(this.TableModel.Rows[this.hotRow], this, this.hotRow, this.RowHeaderClientRect(this.hotRow)));
        //    }
        //}

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            //base.OnDoubleClick(e);
            base.OnMouseDoubleClick(e);


            if (this.IsValidCell(this.LastMouseCell))
            {
                Rectangle cellRect = this.CellClientRect(this.LastMouseCell);

                this.OnCellMouseDoubleClick(new I3CellMouseEventArgs(this.TableModel[this.LastMouseCell], this, this.LastMouseCell, this.CellClientRect(this.LastMouseCell), e));
            }
            else if (this.hotColumn != -1 && this.resizingColumnIndex == -1)
            {
                this.OnColumnHeaderMouseDoubleClick(new I3ColumnHeaderMouseEventArgs(this.ColumnModel.Columns[this.hotColumn], this, this.hotColumn, this.ColumnHeaderClientRect(this.hotColumn), e));
            }
            else if (this.hotRow != -1 && this.resizingRowIndex == -1)
            {
                this.OnRowHeaderMouseDoubleClick(new I3RowHeaderMouseEventArgs(this.TableModel.Rows[this.hotRow], this, this.hotRow, this.RowHeaderClientRect(this.hotRow), e));
            }
        }

        #endregion

        #endregion

        #region Paint


        /// <summary>
        /// ���ô˺������׳�PaintBackground�¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }


        /// <summary>
        /// ���ô˺������׳�Paint�¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            //����Ƿ���Ҫ�滭
            if (this.Width == 0 || this.Height == 0)
            {
                return;
            }

            //ColumnModel��Ϊ�ղŽ��л滭
            if (this.ColumnModel != null)
            {
                //���浱ǰ�Ļ��Ƽ�������
                Region clip = e.Graphics.Clip;

                //�ػ���
                if (this.TableModel != null && this.TableModel.Rows.Count > 0)
                {
                    this.OnPaintRows(e);

                    // �����ػ�����
                    e.Graphics.Clip = clip;
                }

                //�ػ���ͷ
                if (this.ColumnHeaderStyle != ColumnHeaderStyle.None && this.ColumnModel.Columns.Count > 0)
                {
                    Rectangle columnHeaderRect = this.ColumnHeaderClientRectangle;
                    if (!I3RectangleHelper.IsEmpty(columnHeaderRect) && columnHeaderRect.IntersectsWith(e.ClipRectangle))
                    {
                        this.OnPaintColumnHeader(e);

                        // �����ػ�����
                        e.Graphics.Clip = clip;
                    }
                }

                //�ػ���ͷ
                if (this.RowHeaderVisible && this.TableModel != null && this.TableModel.Rows.Count > 0)
                {
                    if (this.RowHeaderClientRectangle.IntersectsWith(e.ClipRectangle))
                    {
                        this.OnPaintRowHeader(e);

                        // �����ػ�����
                        e.Graphics.Clip = clip;
                    }
                }

                //�ػ�����  ��������󻭣����ⱻ�������ݸ���
                if (this.GridLines != I3GridLines.None)
                {
                    this.OnPaintGrid(e);

                    // �����ػ�����
                    e.Graphics.Clip = clip;
                }
            }

            //��������ʱ���ı���ʾ
            this.OnPaintEmptyTableText(e);

            //���߿�
            this.OnPaintBorder(e);
        }


        /// <summary>
        /// ָ�������������Xֵ���Ӵ˴�������Table�����£���һ��������  (�Ƚ���˸����Ҫ�޸�)
        /// ����������ͨ���϶������еĿ��ʱ
        /// </summary>
        /// <param name="x"></param>
        private void DrawVerticalReversibleLine(int x)
        {
            Point start = this.PointToScreen(new Point(x - 1, this.ClientRectWithOutBorder_ScrollBar.Top));

            ControlPaint.DrawReversibleLine(start, new Point(start.X, start.Y + this.ClientRectWithOutBorder_ScrollBar.Height), this.BackColor);
        }

        /// <summary>
        /// ָ�������������yֵ���Ӵ˴�������Table�����£���һ��������  (�Ƚ���˸����Ҫ�޸�)
        /// ����������ͨ���϶������еĿ��ʱ
        /// </summary>
        /// <param name="x"></param>
        private void DrawHorizontalReversibleLine(int y)
        {
            Point start = this.PointToScreen(new Point(this.ClientRectWithOutBorder_ScrollBar.Left, y - 1));

            ControlPaint.DrawReversibleLine(start, new Point(start.X + this.ClientRectWithOutBorder_ScrollBar.Width, start.Y), this.BackColor);
        }



        #region Border

        /// <summary>
        /// ����Table��Border
        /// Paints the Table's border
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        protected void OnPaintBorder(PaintEventArgs e)
        {
            //e.Graphics.SetClip(e.ClipRectangle);

            if (this.BorderStyle == BorderStyle.Fixed3D)
            {
                if (I3ThemeManager.VisualStylesEnabled)
                {
                    I3TextBoxStates state = I3TextBoxStates.Normal;
                    if (!this.Enabled)
                    {
                        state = I3TextBoxStates.Disabled;
                    }

                    // draw the left border
                    Rectangle clipRect = new Rectangle(0, 0, SystemInformation.Border3DSize.Width, this.Height);
                    if (clipRect.IntersectsWith(e.ClipRectangle))
                    {
                        I3ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
                    }

                    // draw the top border
                    clipRect = new Rectangle(0, 0, this.Width, SystemInformation.Border3DSize.Height);
                    if (clipRect.IntersectsWith(e.ClipRectangle))
                    {
                        I3ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
                    }

                    // draw the right border
                    clipRect = new Rectangle(this.Width - SystemInformation.Border3DSize.Width, 0, this.Width, this.Height);
                    if (clipRect.IntersectsWith(e.ClipRectangle))
                    {
                        I3ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
                    }

                    // draw the bottom border
                    clipRect = new Rectangle(0, this.Height - SystemInformation.Border3DSize.Height, this.Width, SystemInformation.Border3DSize.Height);
                    if (clipRect.IntersectsWith(e.ClipRectangle))
                    {
                        I3ThemeManager.DrawTextBox(e.Graphics, this.ClientRectangle, clipRect, state);
                    }
                }
                else
                {
                    ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.Sunken);
                }
            }
            else if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
            }

            if (this.HScroll && this.VScroll)
            {
                Rectangle rect = new Rectangle(this.Width - this.BorderWidth - SystemInformation.VerticalScrollBarWidth,
                    this.Height - this.BorderWidth - SystemInformation.HorizontalScrollBarHeight,
                    SystemInformation.VerticalScrollBarWidth,
                    SystemInformation.HorizontalScrollBarHeight);

                if (rect.IntersectsWith(e.ClipRectangle))
                {
                    e.Graphics.FillRectangle(SystemBrushes.Control, rect);
                }
            }
        }

        #endregion

        #region Cells

        /// <summary>
        /// ����Cell�����׳�BeforePaintCell�¼��� OnAfterPaintCell�¼�
        /// Paints the Cell at the specified row and column indexes
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        /// <param name="row">The index of the row that contains the cell to be painted</param>
        /// <param name="column">The index of the column that contains the cell to be painted</param>
        /// <param name="cellRect">The bounding Rectangle of the Cell</param>
        protected void OnPaintCell(PaintEventArgs e, int row, int column, Rectangle cellRect)
        {
            if (row == 0 && column == 1)
            {
                column = 1;
            }


            // get the renderer for the cells column
            II3CellRenderer renderer = this.ColumnModel.Columns[column].Renderer;
            if (renderer == null)
            {
                // get the default renderer for the column
                renderer = this.ColumnModel.GetCellRenderer(this.ColumnModel.Columns[column].GetDefaultRendererName());
            }

            // if the renderer is still null (which it shouldn't)
            // the get out of here
            if (renderer == null)
            {
                return;
            }

            I3PaintCellEventArgs pcea = new I3PaintCellEventArgs(e.Graphics, cellRect);
            pcea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, cellRect));

            if (column < this.TableModel.Rows[row].Cells.Count)
            {
                // is the cell selected
                bool selected = false;

                if (this.FullRowSelect)
                {
                    selected = this.TableModel.Selections.IsRowSelected(row);
                }
                else
                {
                    //if (this.SelectionStyle == SelectionStyle.ListView)
                    //{
                    //if (this.TableModel.Selections.IsRowSelected(row) && this.ColumnModel.PreviousVisibleColumn(column) == -1)
                    //{
                    //    selected = true;
                    //}
                    //}
                    //else if (this.SelectionStyle == SelectionStyle.Grid)
                    //{
                    if (this.TableModel.Selections.IsCellSelected(row, column))
                    {
                        selected = true;
                    }
                    //}
                }

                //
                bool editable = this.TableModel[row, column].Editable && this.TableModel.Rows[row].Editable && this.ColumnModel.Columns[column].Editable;
                bool enabled = this.TableModel[row, column].Enabled && this.TableModel.Rows[row].Enabled && this.ColumnModel.Columns[column].Enabled;

                // draw the cell
                pcea.SetCell(this.TableModel[row, column]);
                pcea.SetRow(row);
                pcea.SetColumn(column);
                pcea.SetTable(this);
                pcea.SetSelected(selected);
                pcea.SetFocused(this.Focused && this.FocusedCell.Row == row && this.FocusedCell.Column == column);
                pcea.SetSorted(column == this.lastSortedColumn);
                pcea.SetEditable(editable);
                pcea.SetEnabled(enabled);
                pcea.SetCellRect(cellRect);
            }
            else
            {
                // there isn't a cell for this column, so send a 
                // null value for the cell and the renderer will 
                // take care of the rest (it should draw an empty cell)

                pcea.SetCell(null);
                pcea.SetRow(row);
                pcea.SetColumn(column);
                pcea.SetTable(this);
                pcea.SetSelected(false);
                pcea.SetFocused(false);
                pcea.SetSorted(false);
                pcea.SetEditable(false);
                pcea.SetEnabled(false);
                pcea.SetCellRect(cellRect);
            }

            // let the user get the first crack at painting the cell
            this.OnBeforePaintCell(pcea);

            // only send to the renderer if the user hasn't 
            // set the handled property
            if (!pcea.Handled)
            {
                renderer.OnPaintCell(pcea);
            }

            // let the user have another go
            this.OnAfterPaintCell(pcea);
        }


        /// <summary>
        /// ���ô˺������׳�BeforePaintCell�¼�
        /// Raises the BeforePaintCell event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        protected virtual void OnBeforePaintCell(I3PaintCellEventArgs e)
        {
            if (BeforePaintCell != null)
            {
                BeforePaintCell(this, e);
            }
        }


        /// <summary>
        /// ���ô˺������׳�AfterPaintCell�¼�
        /// Raises the AfterPaintCell event
        /// </summary>
        /// <param name="e">A PaintCellEventArgs that contains the event data</param>
        protected virtual void OnAfterPaintCell(I3PaintCellEventArgs e)
        {
            if (AfterPaintCell != null)
            {
                AfterPaintCell(this, e);
            }
        }

        #endregion

        #region Grid

        /// <summary>
        /// ����������
        /// Paints the Table's grid
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        protected void OnPaintGrid(PaintEventArgs e)
        {
            if (this.GridLines == I3GridLines.None)
            {
                return;
            }

            //
            //e.Graphics.SetClip(e.ClipRectangle);

            if (this.ColumnModel == null || this.ColumnModel.Columns.Count == 0)
            {
                return;
            }

            //e.Graphics.SetClip(e.ClipRectangle);

            if (this.ColumnModel != null)
            {
                using (Pen gridPen = new Pen(this.GridColor))
                {
                    //
                    gridPen.DashStyle = (DashStyle)this.GridLineStyle;

                    // check if we can draw column lines
                    int right = this.DisplayRectangle.X;
                    if ((this.GridLines & I3GridLines.Columns) == I3GridLines.Columns)
                    {
                        for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
                        {
                            if (this.ColumnModel.Columns[i].Visible)
                            {
                                //right += this.ColumnModel.Columns[i].Width;
                                //if (i == this.columnModel.LastVisibleColumnIndex && this.extendLastCol)
                                //{
                                //    //right = e.ClipRectangle.Right;
                                //    right = this.ClientRectWithOutBorder_ScrollBar_Header.Right;
                                //}
                                right = ColumnHeaderClientRect(i).Right;

                                if (right >= e.ClipRectangle.Left && right <= e.ClipRectangle.Right)
                                {
                                    //e.Graphics.DrawLine(gridPen, right - 1, e.ClipRectangle.Top, right - 1, e.ClipRectangle.Bottom);
                                    int bottom = this.BorderWidth + this.ColumnHeaderHeight + VisibleRowsHeight;
                                    bottom = bottom < e.ClipRectangle.Bottom ? bottom : e.ClipRectangle.Bottom;
                                    e.Graphics.DrawLine(gridPen, right - 1, this.ClientRectWithOutBorder_ScrollBar_Header.Y - 1, right - 1, bottom - 1);
                                }
                            }
                        }
                    }

                    if (this.TableModel != null)
                    {
                        // check if we can draw row lines
                        if ((this.GridLines & I3GridLines.Rows) == I3GridLines.Rows)
                        {
                            //int y = this.CellDataRect.Y + this.RowHeight - 1;

                            //for (int i = y; i <= e.ClipRectangle.Bottom; i += this.RowHeight)
                            //{
                            //    if (i >= this.CellDataRect.Top)
                            //    {
                            //        e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, i, e.ClipRectangle.Right, i);
                            //    }
                            //}


                            int count = 0;
                            int total = 0 - 1;
                            if (this.TableModel != null && this.TableModel.Rows.Count > 0)
                            {
                                while (true)
                                {
                                    if (this.TopIndex + count > this.TableModel.Rows.Count - 1)
                                    {
                                        break;
                                    }

                                    total = total + this.TableModel.Rows[this.TopIndex + count].Height;
                                    if (total <= this.ClientRectWithOutBorder_ScrollBar_Header.Height)
                                    {
                                        count++;
                                        e.Graphics.DrawLine(gridPen,
                                            this.ClientRectWithOutBorder_ScrollBar_Header.X,
                                            this.ClientRectWithOutBorder_ScrollBar_Header.Y + total,
                                            //e.ClipRectangle.Right, 
                                            right - 1,
                                            this.ClientRectWithOutBorder_ScrollBar_Header.Y + total);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Header ���Ʊ�ͷ

        /// <summary>
        /// ������ͷ�����׳�BeforePaintColumnHeader�¼���AfterPaintColumnHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected void OnPaintColumnHeader(PaintEventArgs e)
        {
            //�����ͷ�����Ƿ����ػ������ཻ
            if (!this.ColumnHeaderClientRectangle.IntersectsWith(e.ClipRectangle) && !this.RowColumnHeaderClientRectangle.IntersectsWith(e.ClipRectangle))
            {
                return;
            }

            int xPos = this.DisplayRectangle.Left;
            bool needDummyHeader = !this.extendLastCol;//����Ƿ���Ҫ��ģ���У�������ͷ̫�ѿ�

            I3PaintColumnHeaderEventArgs phea = new I3PaintColumnHeaderEventArgs(e.Graphics, e.ClipRectangle);


            //������ͷ��Ӧ����ͷ
            if (this.RowHeaderVisible)
            {
                Rectangle rowHeaderColHeaderRect = this.RowColumnHeaderClientRectangle;
                if (!I3RectangleHelper.IsEmpty(rowHeaderColHeaderRect) && rowHeaderColHeaderRect.IntersectsWith(e.ClipRectangle))
                {
                    this._headerRenderer.Bounds = rowHeaderColHeaderRect;
                    phea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, this._headerRenderer.Bounds));
                    phea.SetColumn(null);
                    phea.SetColumnIndex(-1);
                    phea.SetTable(this);
                    phea.SetHeaderStyle(this.ColumnHeaderStyle);
                    phea.SetHeaderRect(rowHeaderColHeaderRect);
                    this.OnBeforePaintColumnHeader(phea);
                    if (!phea.Handled)
                    {
                        this._headerRenderer.OnPaintColumnHeader(phea);
                    }
                    this.OnAfterPaintColumnHeader(phea);
                }
            }

            //����������ͷ
            Rectangle clipRectangle = e.ClipRectangle;
            if (e.ClipRectangle.X <= this.BorderWidth + this.RowHeaderWidth)
            {
                clipRectangle.Width -= (this.BorderWidth + this.RowHeaderWidth - clipRectangle.X);
                clipRectangle.X = this.BorderWidth + this.RowHeaderWidth;
            }
            for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
            {
                // ������Ƿ����
                if (this.ColumnModel.Columns[i].Visible)
                {
                    Rectangle colHeaderRect = ColumnHeaderClientRect(i);

                    // �����е������Ƿ����ػ������ཻ
                    if (clipRectangle.IntersectsWith(colHeaderRect))
                    {
                        // ����headerRenderer��λ�ã�ע�⣺headerRenderer�����õģ�
                        //this._headerRenderer.Bounds = new Rectangle(xPos, this.BorderWidth, this.ColumnModel.Columns[i].Width, this.ColumnHeaderHeight);
                        this._headerRenderer.Bounds = new Rectangle(xPos, this.BorderWidth, colHeaderRect.Width, this.ColumnHeaderHeight);

                        // �����ػ�����Ϊ������ͷ��ռ�õ�����
                        phea.Graphics.SetClip(Rectangle.Intersect(clipRectangle, this._headerRenderer.Bounds));

                        // ���ò���
                        phea.SetColumn(this.ColumnModel.Columns[i]);
                        phea.SetColumnIndex(i);
                        phea.SetTable(this);
                        phea.SetHeaderStyle(this.ColumnHeaderStyle);
                        phea.SetHeaderRect(this._headerRenderer.Bounds);

                        // ����BeforePaintHeader�¼�
                        this.OnBeforePaintColumnHeader(phea);

                        // ���phea.Handled���û�����Ϊtrue�����ʾ���¼��Ѿ��������������ػ�
                        if (!phea.Handled)
                        {
                            //�ػ����ͷ
                            this._headerRenderer.OnPaintColumnHeader(phea);
                        }

                        // ����AfterPaintHeader�¼�
                        this.OnAfterPaintColumnHeader(phea);
                    }

                    // ������һ���еĿ�ʼX��λ��
                    xPos += this.ColumnModel.Columns[i].Width;

                    // �����һ���еĿ�ʼX��λ���Ѿ����ڻ��ߵ����ػ������Right����˵������Ҫ��������ͷ�����ػ���
                    if (xPos >= clipRectangle.Right)
                    {
                        return;
                    }

                    // �����һ���еĿ�ʼX��λ���Ѿ����ڻ��ߵ��ڿͻ�������Ŀ�ȣ���˵������Ҫ�ٻ�����������
                    if (xPos >= this.ClientRectangle.Width)
                    {
                        needDummyHeader = false;

                        break;
                    }
                }
            }

            //����������(������ͷ�տ���������)
            if (needDummyHeader)
            {
                // move and resize the headerRenderer
                this._headerRenderer.Bounds = new Rectangle(xPos, this.BorderWidth, this.ClientRectangle.Width - xPos + 2, this.ColumnHeaderHeight);

                phea.Graphics.SetClip(Rectangle.Intersect(clipRectangle, this._headerRenderer.Bounds));

                phea.SetColumn(null);
                phea.SetColumnIndex(-1);
                phea.SetTable(this);
                phea.SetHeaderStyle(this.ColumnHeaderStyle);
                phea.SetHeaderRect(this._headerRenderer.Bounds);

                // let the user get the first crack at painting the header
                this.OnBeforePaintColumnHeader(phea);

                // only send to the renderer if the user hasn't 
                // set the handled property
                if (!phea.Handled)
                {
                    this._headerRenderer.OnPaintColumnHeader(phea);
                }

                // let the user have another go
                this.OnAfterPaintColumnHeader(phea);
            }
        }


        /// <summary>
        /// ���ô˺������׳�BeforePaintColumnHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBeforePaintColumnHeader(I3PaintColumnHeaderEventArgs e)
        {
            if (BeforePaintColumnHeader != null)
            {
                BeforePaintColumnHeader(this, e);
            }
        }


        /// <summary>
        /// ���ô˺������׳�AfterPaintColumnHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAfterPaintColumnHeader(I3PaintColumnHeaderEventArgs e)
        {
            if (AfterPaintColumnHeader != null)
            {
                AfterPaintColumnHeader(this, e);
            }
        }



        /// <summary>
        /// ������ͷ�����׳�BeforePaintRowHeader�¼���AfterPaintRowHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected void OnPaintRowHeader(PaintEventArgs e)
        {
            //�����ͷ�����Ƿ����ػ������ཻ
            if (!this.RowHeaderClientRectangle.IntersectsWith(e.ClipRectangle))
            {
                return;
            }

            //int yPos = this.DisplayRectangle.Top;
            int yPos = this.RowHeaderClientRectangle.Y;//ֱ�Ӵ�this.TopIndex��ʼ���ƣ����Ӧ������һ�п�ʼ��Y��ֵ

            I3PaintRowHeaderEventArgs phea = new I3PaintRowHeaderEventArgs(e.Graphics, e.ClipRectangle);

            for (int i = this.TopIndex; i < this.TableModel.Rows.Count; i++)
            {
                // ������Ƿ����
                if (this.TableModel.Rows[i].Visible)
                {
                    Rectangle rowHeaderRect = new Rectangle(this.BorderWidth, yPos, this.RowHeaderWidth, this.TableModel.Rows[i].Height);
                    //if (i == this.TableModel.Rows.Count - 1)
                    //{
                    //    rowHeaderRect.Height++;//�໭һ�����أ�ʹ���������߲���
                    //}

                    // �����е������Ƿ����ػ������ཻ
                    if (e.ClipRectangle.IntersectsWith(rowHeaderRect))
                    {
                        // ����headerRenderer��λ�ã�ע�⣺headerRenderer�����õģ�
                        this._headerRenderer.Bounds = rowHeaderRect;

                        // �����ػ�����Ϊ������ͷ��ռ�õ�����
                        phea.Graphics.SetClip(Rectangle.Intersect(e.ClipRectangle, this._headerRenderer.Bounds));

                        // ���ò���
                        phea.SetRow(this.TableModel.Rows[i]);
                        phea.SetRowIndex(i);
                        phea.SetTable(this);
                        phea.SetHeaderRect(this._headerRenderer.Bounds);

                        // ����BeforePaintHeader�¼�
                        this.OnBeforePaintRowHeader(phea);

                        // ���phea.Handled���û�����Ϊtrue�����ʾ���¼��Ѿ��������������ػ�
                        if (!phea.Handled)
                        {
                            //�ػ����ͷ
                            this._headerRenderer.OnPaintRowHeader(phea);
                        }

                        // ����AfterPaintHeader�¼�
                        this.OnAfterPaintRowHeader(phea);
                    }

                    // ������һ���еĿ�ʼy��λ��
                    yPos += this.TableModel.Rows[i].Height;

                    // �����һ���еĿ�ʼy��λ���Ѿ����ڻ��ߵ����ػ������Bottom����˵������Ҫ��������ͷ�����ػ���
                    if (yPos >= e.ClipRectangle.Bottom)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ���ô˺������׳�BeforePaintRowHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBeforePaintRowHeader(I3PaintRowHeaderEventArgs e)
        {
            if (BeforePaintRowHeader != null)
            {
                BeforePaintRowHeader(this, e);
            }
        }


        /// <summary>
        /// ���ô˺������׳�AfterPaintRowHeader�¼�
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAfterPaintRowHeader(I3PaintRowHeaderEventArgs e)
        {
            if (AfterPaintRowHeader != null)
            {
                AfterPaintRowHeader(this, e);
            }
        }

        #endregion

        #region Rows

        /// <summary>
        /// �����У�
        /// Paints the Table's Rows
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        protected void OnPaintRows(PaintEventArgs e)
        {
            int xPos = this.DisplayRectangle.Left;//x��ȡ��������
            int yPos = this.ClientRectWithOutBorder_ScrollBar.Top;//y��ȡ�ͻ�������
            //int yPos = this.DisplayRectangle.Top;//y��ȡ�ͻ�������

            if (this.ColumnHeaderStyle != ColumnHeaderStyle.None)
            {
                yPos += this.ColumnHeaderHeight;
            }

            Rectangle rowRect = new Rectangle(xPos, yPos, this.ColumnModel.TotalColumnsWidth, this.DefaultRowHeight);
            if (rowRect.Right < this.ClientRectWithOutBorder_ScrollBar_Header.Right && this.extendLastCol)
            {
                rowRect.Width = rowRect.Width + this.ClientRectWithOutBorder_ScrollBar_Header.Right - rowRect.Right;
            }

            List<int> rowIndexList = new List<int>();
            //������
            for (int i = 0; i <= this.frozenRowCount - 1; i++)
            {
                rowIndexList.Add(i);
            }
            //������  TODO
            for (int i = this.TopIndex; i < Math.Min(this.TableModel.Rows.Count, this.TopIndex + this.VisibleRowsCount + 1); i++)
            {
                rowIndexList.Add(i);
            }

            //����
            foreach (int i in rowIndexList)
            {
                int tmpHeigth = this.TableModel.Rows[i].Height;
                rowRect.Height = tmpHeigth;

                if (rowRect.IntersectsWith(e.ClipRectangle))
                {
                    this.OnPaintRow(e, i, rowRect);
                }
                else if (rowRect.Top > e.ClipRectangle.Bottom)
                {
                    break;
                }

                // move to the next row
                //rowRect.Y += this.RowHeight;
                rowRect.Y += tmpHeigth;
            }

            //���������еı���ɫ
            //if (this.IsValidColumn(this.lastSortedColumn))
            //{
            //    if (rowRect.Y < this.ClientRectWithOutBorder_ScrollBar.Bottom)
            //    {
            //        Rectangle columnRect = this.ColumnClientRect(this.lastSortedColumn);
            //        columnRect.Y = rowRect.Y;
            //        columnRect.Height = this.ClientRectWithOutBorder_ScrollBar.Bottom - rowRect.Y;

            //        if (columnRect.IntersectsWith(e.ClipRectangle))
            //        {
            //            columnRect.Intersect(e.ClipRectangle);

            //            e.Graphics.SetClip(columnRect);

            //            using (SolidBrush brush = new SolidBrush(this.SortedColumnBackColor))
            //            {
            //                e.Graphics.FillRectangle(brush, columnRect);
            //            }
            //        }
            //    }
            //}
        }


        /// <summary>
        /// ����ָ��Index����
        /// Paints the Row at the specified index
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        /// <param name="row">The index of the Row to be painted</param>
        /// <param name="rowRect">The bounding Rectangle of the Row to be painted</param>
        protected void OnPaintRow(PaintEventArgs e, int row, Rectangle rowRect)
        {
            Rectangle cellRect = new Rectangle(rowRect.X, rowRect.Y, 0, rowRect.Height);

            //e.Graphics.SetClip(rowRect);

            for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
            {
                if (this.ColumnModel.Columns[i].Visible)
                {
                    cellRect = CellClientRect(row, i);
                    //cellRect.Width = this.ColumnModel.Columns[i].Width;

                    if (cellRect.IntersectsWith(e.ClipRectangle))
                    {
                        this.OnPaintCell(e, row, i, cellRect);
                    }
                    else if (cellRect.Left > e.ClipRectangle.Right)
                    {
                        break;
                    }

                    //cellRect.X += this.ColumnModel.Columns[i].Width;
                    cellRect.X += cellRect.Width;
                }
            }
        }

        #endregion

        #region Empty Table Text

        /// <summary>
        /// ����������ʱ����ʾ�ı�
        /// Paints the message that is displayed when the Table doen't 
        /// contain any items
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data</param>
        protected void OnPaintEmptyTableText(PaintEventArgs e)
        {
            if (this.ColumnModel == null || this.RowsCount == 0)
            {
                Rectangle client = this.ClientRectWithOutBorder_ScrollBar_Header;

                client.Y += 10;
                client.Height -= 10;

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;

                using (SolidBrush brush = new SolidBrush(this.ForeColor))
                {
                    if (this.DesignMode)
                    {
                        if (this.ColumnModel == null || this.TableModel == null)
                        {
                            string text = null;

                            if (this.ColumnModel == null)
                            {
                                if (this.TableModel == null)
                                {
                                    text = "Table does not have a ColumnModel or TableModel";
                                }
                                else
                                {
                                    text = "Table does not have a ColumnModel";
                                }
                            }
                            else if (this.TableModel == null)
                            {
                                text = "Table does not have a TableModel";
                            }

                            e.Graphics.DrawString(text, this.Font, brush, client, format);
                        }
                        else if (this.TableModel != null && this.TableModel.Rows.Count == 0)
                        {
                            if (this.NoItemsText != null && this.NoItemsText.Length > 0)
                            {
                                e.Graphics.DrawString(this.NoItemsText, this.Font, brush, client, format);
                            }
                        }
                    }
                    else
                    {
                        if (this.NoItemsText != null && this.NoItemsText.Length > 0)
                        {
                            e.Graphics.DrawString(this.NoItemsText, this.Font, brush, client, format);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Rows

        /// <summary>
        /// ���ô˺������׳�RowPropertyChanged�¼�
        /// Raises the RowPropertyChanged event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        protected internal virtual void OnRowPropertyChanged(I3RowEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                int index = e.Index;
                if (index == -1)
                {
                    index = e.Row.Index;
                }

                this.InvalidateRow(index);
                if (e.EventType == I3RowEventType.StateChanged)//�������״̬�ı䣬�ػ���ͷ
                {
                    this.Invalidate(this.RowHeaderClientRect(index));
                }

                if (RowPropertyChanged != null)
                {
                    RowPropertyChanged(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellAdded�¼�
        /// Raises the CellAdded event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        protected internal virtual void OnCellAdded(I3RowEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.InvalidateRow(e.Index);

                if (CellAdded != null)
                {
                    CellAdded(e.Row, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�CellRemoved�¼�
        /// Raises the CellRemoved event
        /// </summary>
        /// <param name="e">A RowEventArgs that contains the event data</param>
        protected internal virtual void OnCellRemoved(I3RowEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.InvalidateRow(e.Index);

                if (CellRemoved != null)
                {
                    CellRemoved(this, e);
                }

                if (e.CellFromIndex == -1 && e.CellToIndex == -1)
                {
                    if (this.FocusedCell.Row == e.Index)
                    {
                        this.focusedCell = I3CellPos.Empty;
                    }
                }
                else
                {
                    for (int i = e.CellFromIndex; i <= e.CellToIndex; i++)
                    {
                        if (this.FocusedCell.Row == e.Index && this.FocusedCell.Column == i)
                        {
                            this.focusedCell = I3CellPos.Empty;

                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Scrollbars

        /// <summary>
        /// ��Ӧˮƽ�����������¼�
        /// Occurs when the Table's horizontal scrollbar is scrolled
        /// </summary>
        /// <param name="sender">The object that Raised the event</param>
        /// <param name="e">A ScrollEventArgs that contains the event data</param>
        protected void OnHorizontalScroll(object sender, ScrollEventArgs e)
        {
            // stop editing as the editor doesn't move while 
            // the table scrolls
            if (this.IsEditing)
            {
                this.StopEditing();
            }

            if (this.CanRaiseEvents)
            {
                // non-solid row lines develop artifacts while scrolling 
                // with the thumb so we invalidate the table once thumb 
                // scrolling has finished to make them look nice again
                //SB_THUMBPOSITION����ק������λ�ã�SB_THUMBTRACK����ק�����е�λ��  //ThumbPosition��ס�������ƶ����ɿ�
                if (e.Type == ScrollEventType.ThumbPosition)  
                {
                    if (this.GridLineStyle != I3GridLineStyle.Solid)
                    {
                        //�����߲���ʵ��ʱ��������������ƣ����߶ν��������λ��ƶ��ɵģ��Է�ʵ����˵�����ܻ�Ч����̫��
                        if (this.GridLines == I3GridLines.Rows || this.GridLines == I3GridLines.Both)
                        {
                            this.Invalidate(this.ClientRectWithOutBorder_ScrollBar_Header, false);
                        }
                    }

                    // same with the focus rect  //??????????
                    if (this.FocusedCell != I3CellPos.Empty)
                    {
                        this.Invalidate(this.CellClientRect(this.FocusedCell), false);
                    }
                }
                else
                {
                    this.HorizontalScroll(e.NewValue);
                }
            }
        }


        /// <summary>
        /// ��Ӧ��ֱ�����������¼�
        /// Occurs when the Table's vertical scrollbar is scrolled
        /// </summary>
        /// <param name="sender">The object that Raised the event</param>
        /// <param name="e">A ScrollEventArgs that contains the event data</param>
        protected void OnVerticalScroll(object sender, ScrollEventArgs e)
        {
            if (this.VisibleRowsHeight < this.ClientRectWithOutBorder_ScrollBar_Header.Height)
            {
                if (e.NewValue > this.vScrollBar.Value)
                {
                    e.NewValue = this.vScrollBar.Value;
                    return;
                }
            }

            // stop editing as the editor doesn't move while 
            // the table scrolls
            if (this.IsEditing)
            {
                this.StopEditing();
            }

            if (this.CanRaiseEvents)
            {
                // non-solid column lines develop artifacts while scrolling 
                // with the thumb so we invalidate the table once thumb 
                // scrolling has finished to make them look nice again
                if (e.Type == ScrollEventType.ThumbPosition)
                {
                    if (this.GridLineStyle != I3GridLineStyle.Solid)
                    {
                        if (this.GridLines == I3GridLines.Columns || this.GridLines == I3GridLines.Both)
                        {
                            this.Invalidate(this.ClientRectWithOutBorder_ScrollBar, false);
                        }
                    }
                }
                else
                {
                    this.VerticalScroll(e.NewValue);
                }
            }
        }


        /// <summary>
        /// ��Ӧ������GotFocus�¼�
        /// Handler for a ScrollBars GotFocus event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">An EventArgs that contains the event data</param>
        private void scrollBar_GotFocus(object sender, EventArgs e)
        {
            // don't let the scrollbars have focus 
            // (appears to slow scroll speed otherwise)
            this.Focus();
        }

        #endregion

        #region Sorting

        /// <summary>
        /// ���ô˺������׳�BeginSort�¼�
        /// Raises the BeginSort event
        /// </summary>
        /// <param name="e">A ColumnEventArgs that contains the event data</param>
        protected virtual void OnBeginSort(I3ColumnEventArgs e)
        {
            if (BeginSort != null)
            {
                BeginSort(this, e);
            }
        }


        /// <summary>
        /// ���ô˺������׳�EndSort�¼�
        /// Raises the EndSort event
        /// </summary>
        /// <param name="e">A ColumnEventArgs that contains the event data</param>
        protected virtual void OnEndSort(I3ColumnEventArgs e)
        {
            if (EndSort != null)
            {
                EndSort(this, e);
            }
        }

        #endregion

        #region TableModel

        /// <summary>
        /// ���ô˺������׳�TableModelChanged�¼�
        /// Raises the TableModelChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected internal virtual void OnTableModelChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (TableModelChanged != null)
                {
                    TableModelChanged(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�SelectionChanged�¼�
        /// Raises the SelectionChanged event
        /// </summary>
        /// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected internal virtual void OnSelectionChanged(I3SelectionEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                if (e.OldSelectionBounds != Rectangle.Empty)
                {
                    Rectangle invalidateRect = new Rectangle(this.DisplayToClient(e.OldSelectionBounds.Location), e.OldSelectionBounds.Size);

                    //if (this.ColumnHeaderStyle != ColumnHeaderStyle.None)
                    //{
                    //    invalidateRect.Y += this.ColumnHeaderHeight;
                    //}

                    this.Invalidate(invalidateRect);
                }

                if (e.NewSelectionBounds != Rectangle.Empty)
                {
                    Rectangle invalidateRect = new Rectangle(this.DisplayToClient(e.NewSelectionBounds.Location), e.NewSelectionBounds.Size);

                    //if (this.ColumnHeaderStyle != ColumnHeaderStyle.None)
                    //{
                    //    invalidateRect.Y += this.ColumnHeaderHeight;
                    //}

                    this.Invalidate(invalidateRect);
                }

                if (SelectionChanged != null)
                {
                    SelectionChanged(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowHeightChanged�¼�
        /// Raises the RowHeightChanged event
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data</param>
        protected internal virtual void OnRowHeightChanged(EventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (RowHeightChanged != null)
                {
                    RowHeightChanged(this, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowAdded�¼�
        /// Raises the RowAdded event
        /// </summary>
        /// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected internal virtual void OnRowAdded(I3TableModelEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (RowAdded != null)
                {
                    RowAdded(e.TableModel, e);
                }
            }
        }


        /// <summary>
        /// ���ô˺������׳�RowRemoved�¼�
        /// Raises the RowRemoved event
        /// </summary>
        /// <param name="e">A TableModelEventArgs that contains the event data</param>
        protected internal virtual void OnRowRemoved(I3TableModelEventArgs e)
        {
            if (this.CanRaiseEvents)
            {
                this.PerformLayout();
                this.Invalidate();

                if (RowRemoved != null)
                {
                    RowRemoved(e.TableModel, e);
                }
            }
        }

        #endregion

        #endregion
    }
}
