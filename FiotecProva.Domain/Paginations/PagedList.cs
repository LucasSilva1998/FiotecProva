using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Paginations
{
    public class PagedList<T>
    {
        public PagedList(int page, int pageSize, int totalCount, IEnumerable<T> items)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;
        public IEnumerable<T> Items { get; }
    }
}
