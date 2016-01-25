using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hyde.Result.Operation;
using Hyde.Domain.Model;
namespace Hyde.Service
{
    public interface IWarehouseService : IService
    {
        Task<OperationResult<List<warehouseDto>>> GetWareHouseListAsync(bool? shutout = null);

        Task<OperationResult> AddWareHouseAsync(IEnumerable<warehouseDto> items);
    }
}
