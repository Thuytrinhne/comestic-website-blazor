using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Shared.Responses
{
    public class PaginationResult<TEntity>
     (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
     where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
    }

}
