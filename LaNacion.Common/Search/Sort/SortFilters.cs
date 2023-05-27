namespace LaNacion.Common.Search.Sort
{
    public class SortFilter
    {
        public const SortOrder DefaultSortOrder = SortOrder.Ascending;

        public SortFilter(SortOrder sortOrder = DefaultSortOrder)
        {
            this.SortOrder = sortOrder;
        }

        public SortOrder SortOrder { get; }
    }
}
