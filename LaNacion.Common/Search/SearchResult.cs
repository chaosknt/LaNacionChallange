using System.Collections.Generic;

namespace LaNacion.Common.Search
{
    public class SearchResult<TItemType>
    {
        public IEnumerable<TItemType> Items { get; }

        public int ItemsTotal { get; }

        public SearchResult(IEnumerable<TItemType> items, int itemsTotal)
        {
            this.Items = items;
            this.ItemsTotal = itemsTotal;
        }
    }
}
