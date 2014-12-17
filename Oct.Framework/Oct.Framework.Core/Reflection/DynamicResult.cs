using System.Collections.Generic;
using System.Dynamic;

namespace Oct.Framework.Core.Reflection
{
    public class DynamicResult : DynamicObject
    {
        IDictionary<string, object> _objects = new ExpandoObject();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            return _objects.TryGetValue(name, out result);
        }

        public void Add(string name, object s)
        {
            _objects.Add(name, s);
        }
    }
}
