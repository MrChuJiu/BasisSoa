using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.ClientData
{
    /// <summary>
    /// Angular  ST专用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AngularSTResult<T> 
    {
        public List<T> list { get; set; }
        public int total { get; set; }
    }
}
