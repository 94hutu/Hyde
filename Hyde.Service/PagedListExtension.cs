using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Hyde.Result.Operation;
namespace Hyde.Service
{
    public static class PagedListExtension
    {
        public static PageResult<T> ToPageResult<T>(this IPagedList<T> source)
        {
            if (source == null)
                return null;

            return new PageResult<T>()
            {
                PageIndex = source.PageNumber,
                PageSize = source.PageSize,
                TotalItem = source.TotalItemCount,
                TotalPage = source.PageCount,
                PageList = source.ToList()
            };
        }
    }
}
