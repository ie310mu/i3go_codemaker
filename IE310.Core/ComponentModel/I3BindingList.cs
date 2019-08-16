using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections.ObjectModel;

namespace IE310.Core.ComponentModel
{
    /// <summary>
    /// BindingList引发的ListChanged,当事件类型为ItemDeleted时，e.NewIndex包含的是原始列表中的序号，
    /// 通过此已经无法获取删除项的信息，因此这里修改其行为，在items.RemoveItem之前即产生事件
    /// 2016.8.29  注：2.0版本里面有此bug，4.0里面已经没有了。。。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class I3BindingList<T> : BindingList<T>
    {
        //private FieldInfo addNewPosField;
        //private int AddNewPos
        //{
        //    get
        //    {
        //        if (this.addNewPosField == null)
        //        {
        //            this.addNewPosField = typeof(BindingList<T>).GetField("addNewPos", BindingFlags.NonPublic | BindingFlags.Instance);
        //        }
        //        return (int)this.addNewPosField.GetValue(this);
        //    }
        //}

        //private MethodInfo unhookPropertyChangedMethod;
        //private MethodInfo UnhookPropertyChangedMethod
        //{
        //    get
        //    {
        //        if (this.unhookPropertyChangedMethod == null)
        //        {
        //            this.unhookPropertyChangedMethod = typeof(BindingList<T>).GetMethod("UnhookPropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        //        }
        //        return this.unhookPropertyChangedMethod;
        //    }
        //}
        //private void UnhookPropertyChanged(T item)
        //{
        //    this.UnhookPropertyChangedMethod.Invoke(this, new object[] { item });
        //}

        //private MethodInfo fireListChangedMethod;
        //private MethodInfo FireListChangedMethod
        //{
        //    get
        //    {
        //        if (this.fireListChangedMethod == null)
        //        {
        //            this.fireListChangedMethod = typeof(BindingList<T>).GetMethod("FireListChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        //        }
        //        return this.fireListChangedMethod;
        //    }
        //}
        //private void FireListChanged(ListChangedType type, int index)
        //{
        //    this.FireListChangedMethod.Invoke(this, new object[] { type, index });
        //}




        //protected override void RemoveItem(int index)
        //{
        //    if (!this.AllowRemove && ((this.AddNewPos < 0) || (this.AddNewPos != index)))
        //    {
        //        throw new NotSupportedException();
        //    }
        //    this.EndNew(this.AddNewPos);
        //    if (this.RaiseListChangedEvents)
        //    {
        //        this.UnhookPropertyChanged(base[index]);
        //    }

        //    this.FireListChanged(ListChangedType.ItemDeleted, index);
        //    //base.RemoveItem(index);  //应该直接调Collection<T>的RemoveItem方法，反编译知道只是调用了items.RemoveAt
        //    this.Items.RemoveAt(index);
        //}


    }
}
