using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Result.Operation;
using Hyde.Domain.Model;
namespace Hyde.Service
{
    public interface IBrandService : IService
    {
        Task<OperationResult<List<brandDto>>> GetBrandListAsync(bool? shutout = null);

        Task<OperationResult> AddBrandAsync(IEnumerable<brandDto> items);

    }
}
