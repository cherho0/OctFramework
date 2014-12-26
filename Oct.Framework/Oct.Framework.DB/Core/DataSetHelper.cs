using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Core
{
    public class DataSetHelper
    {
        public static List<T> DataSetToList<T>(DataSet ds) where T : BaseEntity<T>, new()
        {
            var entities = new List<T>();
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataRow(row));
            }
            return entities;
        }
    }
}
