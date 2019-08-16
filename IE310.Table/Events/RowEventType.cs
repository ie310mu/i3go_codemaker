


using System;


namespace IE310.Table.Events
{
	/// <summary>
	/// Specifies the type of event generated when the value of a 
	/// Row's property changes
	/// </summary>
    public enum I3RowEventType
    {
        /// <summary>
        /// Occurs when the Row's property change type is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Occurs when the value of a Row's BackColor property changes
        /// </summary>
        BackColorChanged = 1,

        /// <summary>
        /// Occurs when the value of a Row's ForeColor property changes
        /// </summary>
        ForeColorChanged = 2,

        /// <summary>
        /// Occurs when the value of a Row's Font property changes
        /// </summary>
        FontChanged = 3,

        /// <summary>
        /// Occurs when the value of a Row's RowStyle property changes
        /// </summary>
        StyleChanged = 4,

        /// <summary>
        /// Occurs when the value of a Row's Alignment property changes
        /// </summary>
        AlignmentChanged = 5,

        /// <summary>
        /// Occurs when the value of a Row's Enabled property changes
        /// </summary>
        EnabledChanged = 6,

        /// <summary>
        /// Occurs when the value of a Row's Editable property changes
        /// </summary>
        EditableChanged = 7,

        StateChanged = 8,
        ToolTipTextChanged = 9,
    }
}
