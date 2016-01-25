using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Result.Operation;
using Hyde.Domain.Model;
namespace Hyde.Service
{
    public interface IStockService : IService
    {
        Task<OperationResult<PageResult<productDto>>> GetVstockListAsync(int pageindex = 1, int pagesize = 20, IEnumerable<int> productids = null, int? genderid = null, int? categroyid = null, int? brandids = null);

        Task<OperationResult> AddStockAsync(IEnumerable<stockDto> items);

    }
}
