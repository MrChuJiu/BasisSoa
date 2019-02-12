using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.TreeHelper
{
    public static class MenuGenerateTools
    {
        public static List<T> MenuGroup<T>(List<T> Data, string parentId, int level = 0) where T : MenuModel<T>
        {
            List<T> entitys = Data.FindAll(t => t.parentId == parentId);
            List<T> sbdata = new List<T>();
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    item.children = MenuGroup(Data, item.key, ++level);
                    item.group = item.children.Count > 0;
                }

                sbdata.AddRange(entitys);
            }
            return sbdata;
        }
    }
}
