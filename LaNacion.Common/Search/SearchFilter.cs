using LaNacion.Common.Search.Sort;

namespace LaNacion.Common.Search
{
    public class SearchFilter
    {
        public int Start { get; }

        public int Length { get; }

        public string ParamName { get; set; }

        public string ParamVal { get; set; }

        public SortFilter Sorter { get; }

        public SearchFilter(string paramName, string filter, int? start = null, int? length = null, SortOrder? order = null)
        {
            this.Start = start ?? 0;
            this.Length = length ?? 10;
            this.Sorter = new SortFilter(order ?? SortOrder.Ascending);
            this.ParamName = paramName;
            this.ParamVal = filter;
        }
    }
}
