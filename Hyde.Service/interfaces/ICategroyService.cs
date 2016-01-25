using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Result.Operation;

namespace Hyde.Service
{
    public interface ICategroyService : IService
    {
        Task<OperationResult<List<categoryDto>>> GetCategoryListAsync(bool? shutout = null);

        Task<OperationResult> AddCategoryAsync(IEnumerable<categoryDto> items);
    }
}
