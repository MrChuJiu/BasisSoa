using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.TreeHelper
{
     public static class TreeGenerateTools
    {
        public static List<T> TreeGroup<T>(List<T> Data, string parentId, int level = 0) where T : TreeModel<T>
        {
            List<T> entitys = Data.FindAll(t => t.parentId == parentId);
            List<T> sbdata = new List<T>();
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    item.level = level;
                    item.children = TreeGroup(Data, item.key, level);
                    item.isLeaf = item.children.Count > 0 ? false : true;
                }

                sbdata.AddRange(entitys);
            }
            return sbdata;
        }
    }
}
