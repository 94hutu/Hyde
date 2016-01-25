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
        private IRepository<v_stockDto> stockRepo;
        public StockService(IUnitOfWork UnitOfWork)
        {
            unitofwork = UnitOfWork;
            stockRepo = unitofwork.GetRepository<v_stockDto>();
        }

        public async Task<OperationResult<PageResult<v_stockDto>>> GetStockListAsync(IEnumerable<int> productids, int? genderid = default(int?), int? categroyid = default(int?), int? brandids = null, int pageindex = 1, int pagesize = 20)
        {
            var Query = stockRepo.Find(t => productids.Contains(t.productid), t => t.size, t => t.product);

            if (genderid.HasValue)
                Query = Query.Where(t => t.productid == genderid.Value);

            if (categroyid.HasValue)
                Query = Query.Where(t => t.product.categroyid == categroyid.Value);

            if (brandids.HasValue)
                Query = Query.Where(t => t.product.brandid == brandids.Value);

            var result = await Task.Factory.StartNew(() => Query.OrderByDescending(t => t.product.arrivaldate).AsNoTracking().ToPagedList(pageindex, pagesize));

            return new OperationResult<PageResult<v_stockDto>>()
            {
                err_code = ErrorEnum.success,
                err_info = ErrorEnum.sys_error.ToString(),
                entity = result.ToPageResult()
            };

        }
    }
}
