using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oct.Framework.DB.Base;

namespace Oct.Framework.DB.Core
{
    public class DataReaderHelper
    {
        public static List<T> ReaderToList<T>(IDataReader reader) where T : BaseEntity<T>, new()
        {
            var entities = new List<T>();
            while (reader.Read())
            {
                var newt = new T();
                entities.Add(newt.GetEntityFromDataReader(reader));
            }
            reader.Close();
            reader.Dispose();
            return entities;
        }
    }
}
