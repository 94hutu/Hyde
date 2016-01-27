using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Repository;
using Hyde.Result.Operation;
using Hyde.Service.Models;
namespace Hyde.Service
{
    public interface IAdjustPriceService : IService
    {
        Task<OperationResult<List<SalePriceModel>>> GetSalePriceListAsync(DateTime date, int[] productids = null);
        Task<OperationResult<IEnumerable<SalePriceModel>>> GetSalePriceListAsync(DateTime date, productDto[] products);
    }
}
