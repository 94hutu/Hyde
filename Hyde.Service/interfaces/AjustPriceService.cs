using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Result.Operation;
using Hyde.Service.Models;
using Hyde.Domain.Model;
using Hyde.Repository;
using System.Data.Entity;
namespace Hyde.Service
{
    public class AjustPriceService : IAdjustPriceService
    {

        IRepository<ajustpriceheadeDto> headeRepo;
        IRepository<ajustpricedetailDto> detailRepo;
        IUnitOfWork unitofwork;

        public AjustPriceService(IUnitOfWork UnitOfWork)
        {
            unitofwork = UnitOfWork;

            headeRepo = unitofwork.GetRepository<ajustpriceheadeDto>();

            detailRepo = unitofwork.GetRepository<ajustpricedetailDto>();
        }


        public async Task<OperationResult<List<SalePriceModel>>> GetSalePriceListAsync(DateTime date, int[] productids = null)
        {

            var left = headeRepo.Find(t => t.start <= date && date <= t.end && t.shutout == false).AsNoTracking();

            var right = detailRepo.Find().AsNoTracking();

            if (productids != null)
                right = right.Where(t => productids.Contains(t.productid));

            var Query = await left.Join(right, t => t.key, t => t.ajustpriceheadeid, (l, r) => new
            {
                r.productid,
                r.saleprice,
                headid = l.key
            }).ToListAsync();

            var where = Query.GroupBy(t => t.productid).Select(t => new { max = t.Max(o => o.headid), t.Key });

            var result = Query.Join(where, t => new { t.headid, t.productid }, t => new { headid = t.max, productid = t.Key }, (l, r) => new { l.productid, l.saleprice }).Select(t => new SalePriceModel() { productid = t.productid, saleprice = t.saleprice }).ToList();

            return new OperationResult<List<SalePriceModel>>() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString(), entity = result };

        }

        public async Task<OperationResult<IEnumerable<SalePriceModel>>> GetSalePriceListAsync(DateTime date, productDto[] products)
        {
            var left = headeRepo.Find(t => t.start <= date && date <= t.end).AsNoTracking();

            var right = detailRepo.Find(t => products.Select(o => o.key).Contains(t.productid)).AsNoTracking();

         
            var Query = await left.Join(right, t => t.key, t => t.ajustpriceheadeid, (l, r) => new
            {
                r.productid,
                r.saleprice,
                headid = l.key
            }).ToListAsync();

            var where = Query.GroupBy(t => t.productid).Select(t => new { max = t.Max(o => o.headid), t.Key });

            var result = Query.Join(where, t => new { t.headid, t.productid }, t => new { headid = t.max, productid = t.Key }, (l, r) => new { l.productid, l.saleprice }).Select(t => new SalePriceModel() { productid = t.productid, saleprice = t.saleprice }).ToList();

            var result1 = from l in products
                          join r in result on l.key equals r.productid into g
                          from x in g.DefaultIfEmpty()
                          select new SalePriceModel() { productid = l.key, saleprice = x?.saleprice ?? l.unitprice };

            return new OperationResult<IEnumerable<SalePriceModel>>() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString(), entity = result1 };
        }
    }
}
