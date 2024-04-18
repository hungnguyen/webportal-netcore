using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
        public PagedResult<T> GetRandom(int max) 
        { 
            if (Items.Count < max) return new PagedResult<T> { Items = Items };
            else
            {
                var list = new List<T>();
                var rnd = new Random();
                for(int i = 0; i < max; i++)
                {
                    list.Add(Items[rnd.Next(0, Items.Count)]);
                }
                return new PagedResult<T> { Items = list };
            }
        }

        public T GetRandomItem()
        {
            if(Items.Count == 0) return default(T);
            else
            {
                var rnd = new Random();
                return Items[rnd.Next(0, Items.Count)];
            }
        }
    }
}
