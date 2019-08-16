using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IE310.Core.Utils
{
    public static class I3TreeViewUtil
    {
        /// <summary>
        /// 从DataTable创建一颗树，需要指定keyField、parentKeyField、sortField、nameField
        /// 注：sortFiled可以为空
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="table"></param>
        /// <param name="keyField"></param>
        /// <param name="parentKeyField"></param>
        /// <param name="sortField"></param>
        /// <param name="nameField"></param>
        public static void CreateNodes(TreeView treeView, DataTable table, string keyField, string parentKeyField, string sortField, string nameField)
        {
            treeView.Nodes.Clear();
            CreateNodes(treeView, null, "-1", table, keyField, parentKeyField, sortField, nameField);
        }

        /// <summary>
        /// 直接指定pid，创建所有下级结点（递归）
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="parentNode"></param>
        /// <param name="pid"></param>
        /// <param name="table"></param>
        /// <param name="keyField"></param>
        /// <param name="parentKeyField"></param>
        /// <param name="sortField"></param>
        /// <param name="nameField"></param>
        public static void CreateNodes(TreeView treeView, TreeNode parentNode, string pid, DataTable table, string keyField, string parentKeyField, string sortField, string nameField)
        {
            DataRow[] rows;
            if (string.IsNullOrEmpty(sortField))
            {
                rows = table.Select(parentKeyField + "='" + pid + "'");
            }
            else
            {
                rows = table.Select(parentKeyField + "='" + pid + "'", sortField);
            }

            foreach (DataRow row in rows)
            {
                TreeNode node = CreateNode(treeView, parentNode, row, nameField);
                CreateNodes(treeView, node, row[keyField].ToString(), table, keyField, parentKeyField, sortField, nameField);
            }
        }

        /// <summary>
        /// 从DataRow创建一个结点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="parentNode"></param>
        /// <param name="row"></param>
        /// <param name="nameField"></param>
        /// <returns></returns>
        private static TreeNode CreateNode(TreeView treeView, TreeNode parentNode, DataRow row, string nameField)
        {
            TreeNode node;
            if (parentNode == null)
            {
                node = treeView.Nodes.Add(row[nameField].ToString());
            }
            else
            {
                node = parentNode.Nodes.Add(row[nameField].ToString());
            }
            node.Tag = row;
            return node;
        }

        public static TreeNode FindNodeAfterDelete(TreeNode deleteNode)
        {
            if (deleteNode == null)
            {
                return null;
            }
            TreeNode node = deleteNode.NextNode;
            if (node == null)
            {
                node = deleteNode.PrevNode;
            }
            if (node == null)
            {
                node = deleteNode.Parent;
            }
            return node;
        }
    }
}
