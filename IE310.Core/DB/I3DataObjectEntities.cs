using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using IE310.Core.ComponentModel;
using IE310.Core.Utils;
using System.Reflection;

namespace IE310.Core.DB
{
    /// <summary>
    /// 实例集合，自动处理实体的值的变化，可直接获取EntityList、UnChangedList、ChangedList、NewList、DeleteList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class I3DataObjectEntities<T> where T : I3DataObject
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public I3DataObjectEntities()
        {
            this.entityList = new I3BindingList<T>();
            idProperty = typeof(T).GetProperty("id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            this.entityDic = new Dictionary<object, T>();
            this.originalEntityDic = new Dictionary<object, T>();
            this.unChangedList = new I3BindingList<T>();
            this.changedList = new I3BindingList<T>();
            this.newList = new I3BindingList<T>();
            this.deleteList = new I3BindingList<T>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entities"></param>
        public I3DataObjectEntities(T[] entities)
            : this()
        {
            foreach (T entity in entities)
            {
                this.AddUnChangedEntity(entity);
            }
        }

        /// <summary>
        /// unChangedList列表中实例属性改变事件，将实例移动到changeList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T entity = sender as T;
            T originalEntity = this.originalEntityDic[idProperty.GetValue(entity, null)];
            bool changed = !I3ObjectUtil.CompareProperty(entity, originalEntity);


            if (this.unChangedList.Contains(entity))  //在未改变列表中
            {
                if (changed)  //已改变
                {
                    this.unChangedList.Remove(entity);
                    this.changedList.Add(entity);
                }
            }
            else if (this.changedList.Contains(entity))  //在已改变列表中
            {
                if (!changed)  //未改变
                {
                    this.changedList.Remove(entity);
                    this.unChangedList.Add(entity);
                }
            }
        }

        /// <summary>
        /// 在unChangedList中增加一个实例
        /// </summary>
        /// <param name="entity"></param>
        public void AddUnChangedEntity(T entity)
        {
            this.entityList.Add(entity);
            this.entityDic.Add(idProperty.GetValue(entity, null), entity);
            this.AddOriginalEntity(entity);
            this.unChangedList.Add(entity);
            entity.PropertyChanged += new PropertyChangedEventHandler(entity_PropertyChanged);
        }
        /// <summary>
        /// 在unChangedList中增加多个实例
        /// </summary>
        /// <param name="entities"></param>
        public void AddUnChangedEntity(T[] entities)
        {
            foreach (T entity in entities)
            {
                this.AddUnChangedEntity(entity);
            }
        }

        /// <summary>
        /// 在newList中增加一个实例
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewEntity(T entity)
        {
            this.entityList.Add(entity);
            this.entityDic.Add(idProperty.GetValue(entity, null), entity);
            //this.AddOriginalEntity(entity);  //新建时不需要添加到原始对象，也不需要挂接事件
            this.newList.Add(entity);
        }
        /// <summary>
        /// 在newList中增加多个实例
        /// </summary>
        /// <param name="entities"></param>
        public void AddNewEntity(T[] entities)
        {
            foreach (T entity in entities)
            {
                this.AddNewEntity(entity);
            }
        }

        /// <summary>
        /// 移除一个实例
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(T entity)
        {
            if (this.unChangedList.Contains(entity))
            {
                this.entityList.Remove(entity);
                this.entityDic.Remove(idProperty.GetValue(entity, null));
                this.originalEntityDic.Remove(idProperty.GetValue(entity, null));
                this.unChangedList.Remove(entity);
                entity.PropertyChanged -= new PropertyChangedEventHandler(entity_PropertyChanged);
                this.deleteList.Add(entity);
                return;
            }

            if (this.changedList.Contains(entity))
            {
                this.entityList.Remove(entity);
                this.entityDic.Remove(idProperty.GetValue(entity, null));
                this.originalEntityDic.Remove(idProperty.GetValue(entity, null));
                this.changedList.Remove(entity);
                entity.PropertyChanged -= new PropertyChangedEventHandler(entity_PropertyChanged);
                this.deleteList.Add(entity);
                return;
            }

            if (this.newList.Contains(entity))
            {
                this.entityList.Remove(entity);
                this.entityDic.Remove(idProperty.GetValue(entity, null));
                this.originalEntityDic.Remove(idProperty.GetValue(entity, null));
                this.newList.Remove(entity);
                return;
            }
        }
        /// <summary>
        /// 移除多个实例
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveEntity(T[] entities)
        {
            foreach (T entity in entities)
            {
                this.RemoveEntity(entity);
            }
        }

        /// <summary>
        /// 移除所有实例
        /// </summary>
        private void Clear()
        {
            //while (this.newList.Count > 0)
            //{
            //    this.RemoveEntity(this.newList[0]);
            //}
            //while (this.changedList.Count > 0)
            //{
            //    this.RemoveEntity(this.changedList[0]);
            //}
            //while (this.unChangedList.Count > 0)
            //{
            //    this.RemoveEntity(this.unChangedList[0]);
            //}

            this.entityList.Clear();
            this.entityDic.Clear();
            this.originalEntityDic.Clear();
            this.unChangedList.Clear();
            this.changedList.Clear();
            this.newList.Clear();
            this.deleteList.Clear();
        }

        /// <summary>
        /// 在将信息提交到数据库后，改变所有实例的状态为unChanged，并删除deleteList中所有实例
        /// </summary>
        public void AccecptChange()
        {
            foreach (T entity in this.changedList)
            {
                this.unChangedList.Add(entity);
            }
            this.changedList.Clear();
            foreach (T entity in this.newList)
            {
                this.unChangedList.Add(entity);
                entity.PropertyChanged += new PropertyChangedEventHandler(entity_PropertyChanged);
            }
            this.newList.Clear();
            this.deleteList.Clear();

            //重新添加原始值
            this.originalEntityDic.Clear();
            foreach (T entity in this.unChangedList)
            {
                this.AddOriginalEntity(entity);
            }
        }


        private I3BindingList<T> entityList;
        /// <summary>
        /// 所有未删除的实例的列表
        /// </summary>
        public I3BindingList<T> EntityList
        {
            get
            {
                return entityList;
            }
        }

        private PropertyInfo idProperty;
        private Dictionary<object, T> entityDic;
        /// <summary>
        /// 所有未删除的实例的字典
        /// </summary>
        public Dictionary<object, T> EntityDic
        {
            get
            {
                return entityDic;
            }
        }

        private Dictionary<object, T> originalEntityDic;
        /// <summary>
        /// 记录原始对象的拷贝
        /// </summary>
        public Dictionary<object, T> OriginalEntityDic
        {
            get
            {
                return originalEntityDic;
            }
        }

        /// <summary>
        /// 添加一个原始值，如果已经有了，则覆盖
        /// </summary>
        /// <param name="entity"></param>
        private void AddOriginalEntity(T entity)
        {
            object id = idProperty.GetValue(entity, null);
            if (originalEntityDic.ContainsKey(id))
            {
                originalEntityDic.Remove(id);
            }
            T originalEntity = (T)Activator.CreateInstance(typeof(T));
            I3ObjectUtil.DeepCopyProperty(entity, originalEntity);
            originalEntityDic.Add(id, originalEntity);
        }

        private I3BindingList<T> unChangedList;
        /// <summary>
        /// 未修改的实例的列表
        /// </summary>
        public I3BindingList<T> UnChangedList
        {
            get
            {
                return unChangedList;
            }
        }

        private I3BindingList<T> changedList;
        /// <summary>
        /// 修改了的实例的列表，会通过属性改变自动移动
        /// </summary>
        public I3BindingList<T> ChangedList
        {
            get
            {
                return changedList;
            }
        }

        /// <summary>
        /// 修改了的实例的数组
        /// </summary>
        public T[] ChangedArray
        {
            get
            {
                return I3ObjectUtil.ToArray(ChangedList);
            }
        }

        private I3BindingList<T> newList;
        /// <summary>
        /// 新增的实例的列表
        /// </summary>
        public I3BindingList<T> NewList
        {
            get
            {
                return newList;
            }
        }

        /// <summary>
        /// 新增的实例的数组
        /// </summary>
        public T[] NewArray
        {
            get
            {
                return I3ObjectUtil.ToArray(NewList);
            }
        }

        private I3BindingList<T> deleteList;
        /// <summary>
        /// 删除的实例的列表
        /// </summary>
        public I3BindingList<T> DeleteList
        {
            get
            {
                return deleteList;
            }
        }

        /// <summary>
        /// 删除的实例的数组
        /// </summary>
        public T[] DeleteArray
        {
            get
            {
                return I3ObjectUtil.ToArray(DeleteList);
            }
        }

        /// <summary>
        /// 删除的实例的id的列表
        /// </summary>
        public List<string> DeleteIdList
        {
            get
            {
                List<string> result = new List<string>();
                foreach (T entity in DeleteList)
                {
                    result.Add(idProperty.GetValue(entity, null).ToString());
                }
                return result;
            }
        }

        /// <summary>
        /// 在当前存在的实例中获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            if (entityDic.ContainsKey(id))
            {
                return entityDic[id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 在原始实例列表中获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetOriginal(object id)
        {
            if (originalEntityDic.ContainsKey(id))
            {
                return originalEntityDic[id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取实例的值是否被改变了
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityState GetEntityState(T entity)
        {
            if (this.deleteList.Contains(entity))
            {
                return EntityState.Deleted;
            }
            if (this.newList.Contains(entity))
            {
                return EntityState.Added;
            }
            if (this.changedList.Contains(entity))
            {
                return EntityState.Changed;
            }
            if (this.unChangedList.Contains(entity))
            {
                return EntityState.UnChanged;
            }

            return EntityState.NotFound;
        }

        /// <summary>
        /// 获取实例的值是否被改变了
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EntityState GetEntityState(object id)
        {
            if (!this.entityDic.ContainsKey(id))
            {
                return EntityState.NotFound;
            }

            T entity = this.entityDic[id];
            return GetEntityState(entity);
        }
    }

    public enum EntityState
    {
        NotFound = 1,
        Deleted = 2,
        Changed = 3,
        UnChanged = 4,
        Added = 5,
    }
}
