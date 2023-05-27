using LaNacion.Common.Search;
using LaNacion.Entities.Enums;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class FindByLocationRequest
    {
        public LocationType Type { get; set; }

        public string SearchedValue { get; set; }

        public int? Start { get; set; }

        public int? Length { get; set; }

        public SortOrder? Order { get; set; }
    }
}
