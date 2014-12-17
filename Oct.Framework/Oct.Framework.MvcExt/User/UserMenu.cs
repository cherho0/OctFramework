using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oct.Framework.MvcExt.User
{
    [Serializable]
    public class UserMenus
    {
        public Dictionary<string, List<MeunItem>> MeunItem { get; set; }

        public UserMenus()
        {
            MeunItem = new Dictionary<string, List<MeunItem>>();
        }

        public void Add(string menuName, string name, string url)
        {
            if (!MeunItem.ContainsKey(menuName))
            {
                MeunItem.Add(menuName, new List<MeunItem>());
            }
            MeunItem[menuName].Add(new MeunItem()
            {
                Name = name,
                Url = url
            });
        }
    }

    [Serializable]
    public class MeunItem
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
