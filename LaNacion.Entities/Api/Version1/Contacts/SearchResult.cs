using System.Collections.Generic;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class SearchResult<T> : BasicResponse
    {
        public T Result { get; set; }

        public int ItemsTotal { get; set; }
    }
}
