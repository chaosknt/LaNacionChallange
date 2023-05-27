namespace LaNacion.Common.Search.Filter
{
    public class WhereFilter
    {
        public WhereFilter(string paramName, string filter)
        {
            this.ParamName = paramName;
            this.Filter = filter;
        }

        public string ParamName { get; }

        public string Filter { get; }
    }
}
