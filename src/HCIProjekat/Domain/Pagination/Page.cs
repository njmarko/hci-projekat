using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination
{
    public class Page<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Entities;
        public int TotalElements { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int Size { get; set; }
        public bool IsFirst => PageNumber == 0;
        public bool IsLast => PageCount == 0 || PageNumber == PageCount - 1;
    }
}
