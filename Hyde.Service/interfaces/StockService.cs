using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Result.Operation;
using Hyde.Repository;
using Hyde.Domain;
using PagedList;
using System.Data.Entity;
namespace Hyde.Service
{
    public class StockService : IStockService
    {
        private IUnitOfWork unitofwork;
        private IRepository<productDto> v_stockRepo;
        private IRepository<stockDto> stockRepo;
        public StockService(IUnitOfWork UnitOfWork)
        {
            unitofwork = UnitOfWork;
            v_stockRepo = unitofwork.GetRepository<productDto>();
            stockRepo = unitofwork.GetRepository<stockDto>();
        }

        public async Task<OperationResult<PageResult<productDto>>> GetVstockListAsync(int pageindex = 1, int pagesize = 20, IEnumerable<int> productids = null, int? genderid = default(int?), int? categroyid = default(int?), int? brandids = null)
        {
            var Query = v_stockRepo.Find(t => t.v_stocks.Sum(o => o.usablequantity) > 0, t => t.brand, t => t.category, t => t.gender, t => t.v_stocks.Select(o => o.size)).AsNoTracking();

            if (productids != null)
                Query = Query.Where(t => productids.Contains(t.key));

            if (genderid.HasValue)
                Query = Query.Where(t => t.genderid == genderid.Value);

            if (categroyid.HasValue)
                Query = Query.Where(t => t.categroyid == categroyid.Value);

            if (brandids.HasValue)
                Query = Query.Where(t => t.brandid == brandids.Value);

            var result = await Task.Factory.StartNew(() => Query.OrderByDescending(t => t.arrivaldate).ToPagedList(pageindex, pagesize));

            return new OperationResult<PageResult<productDto>>()
            {
                err_code = ErrorEnum.success,
                err_info = ErrorEnum.success.ToString(),
                entity = result.ToPageResult()
            };

        }

        public async Task<OperationResult> AddStockAsync(IEnumerable<stockDto> items)
        {
            stockRepo.Add(items);

            await unitofwork.SaveAsync();

            return new OperationResult() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString() };

        }
    }
}
