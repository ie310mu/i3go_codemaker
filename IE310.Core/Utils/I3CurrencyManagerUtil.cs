using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace IE310.Core.Utils
{
    public static class I3CurrencyManagerUtil
    {
        /// <summary>
        /// 更新CurrencyManager的Position属性，并强制引发PositionChanged事件，而不管Position是否变化
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="index"></param>
        public static void UpdatePosition(CurrencyManager manager, int index)
        {
            //FieldInfo listpositionField = manager.GetType().GetField("listposition", BindingFlags.NonPublic | BindingFlags.Instance);
            //int listposition = (int)listpositionField.GetValue(manager);

            //if (listposition != -1)
            //{
            //    if (index < 0)
            //    {
            //        index = 0;
            //    }
            //    int count = manager.List.Count;
            //    if (index >= count)
            //    {
            //        index = count - 1;
            //    }

            //    MethodInfo changeRecordStateMethod = manager.GetType().GetMethod("ChangeRecordState", BindingFlags.NonPublic | BindingFlags.Instance);
            //    changeRecordStateMethod.Invoke(manager, new object[] { index, true, true, true, false });
            //    //this.ChangeRecordState(value, this.listposition != value, true, true, false);
            //}

            //if (manager.Position == index)
            //{
            //    MethodInfo onCurrentChangedMethod = manager.GetType().GetMethod("OnCurrentChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            //    onCurrentChangedMethod.Invoke(manager, new object[] { EventArgs.Empty });
            //    MethodInfo onPositionChangedMethod = manager.GetType().GetMethod("OnPositionChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            //    onPositionChangedMethod.Invoke(manager, new object[] { EventArgs.Empty });
            //}
            //else
            //{
            manager.Position = index;
            //}

            if (manager.Position >= 0 && manager.Position < manager.List.Count)
            {
                foreach (Binding binding in manager.Bindings)
                {
                    binding.ReadValue();
                }
            }
            else
            {
                foreach (Binding binding in manager.Bindings)
                {
                    PropertyInfo property = binding.Control.GetType().GetProperty(binding.PropertyName);
                    if (property != null)
                    {
                        property.SetValue(binding.Control, null, null);
                    }
                }
            }
        }
    }
}
