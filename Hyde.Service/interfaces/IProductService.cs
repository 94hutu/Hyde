using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Result.Operation;
using Hyde.Domain.Model;
using Hyde.Repository;
using Hyde.Service.Models;
namespace Hyde.Service
{
    public interface IProductService : IService
    {

        Task<OperationResult<productDto>> GetProductAsync(string code);

        Task<OperationResult<List<productDto>>> GetProductListAsync(string[] productcodes);

        Task<OperationResult> AddProductAsync(IEnumerable<productDto> items);

        Task<OperationResult<PageResult<SaleProductModel>>> GetProductListAsync(int pageindex = 1, int pagesize = 20, IEnumerable<int> productids = null, int? genderid = null, int? categroyid = null, int? brandid = null);

    }
}
