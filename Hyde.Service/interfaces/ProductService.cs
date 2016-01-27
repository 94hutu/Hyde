using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Result.Operation;
using Hyde.Repository;
using System.Data.Entity;
using Hyde.Service.Models;
using PagedList;

namespace Hyde.Service
{
    public class ProductService : IProductService
    {
        IUnitOfWork unitOfwork;
        IRepository<productDto> productRepo;
        public ProductService(IUnitOfWork Unitofwork)
        {
            unitOfwork = Unitofwork;
            productRepo = unitOfwork.GetRepository<productDto>();
        }

        public async Task<OperationResult<List<productDto>>> GetProductListAsync(string[] productcodes)
        {
            try
            {
                var result = await productRepo.Find(t => productcodes.Contains(t.code)).AsNoTracking().ToListAsync();

                return new OperationResult<List<productDto>>() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString(), entity = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<List<productDto>>() { err_code = ErrorEnum.sys_error, err_info = ex.Message };
            }
        }

        public async Task<OperationResult> AddProductAsync(IEnumerable<productDto> items)
        {
            try
            {
                productRepo.Add(items);
                await unitOfwork.SaveAsync();
                return new OperationResult() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString() };
            }
            catch (Exception ex)
            {
                return new OperationResult() { err_code = ErrorEnum.sys_error, err_info = ex.Message };
            }
        }

        public async Task<OperationResult<PageResult<SaleProductModel>>> GetProductListAsync(int pageindex = 1, int pagesize = 20, IEnumerable<int> productids = null, int? genderid = default(int?), int? categroyid = default(int?), int? brandids = default(int?))
        {
            var Query = productRepo.Find(t => t.v_stocks.Sum(o => o.usablequantity) > 0, t => t.brand, t => t.category, t => t.gender, t => t.v_stocks.Select(o => o.size)).AsNoTracking();

            if (productids != null)
                Query = Query.Where(t => productids.Contains(t.key));

            if (genderid.HasValue)
                Query = Query.Where(t => t.genderid == genderid.Value);

            if (categroyid.HasValue)
                Query = Query.Where(t => t.categroyid == categroyid.Value);

            if (brandids.HasValue)
                Query = Query.Where(t => t.brandid == brandids.Value);

            var result = await Task.Factory.StartNew(() => Query.OrderByDescending(t => t.arrivaldate).ToPagedList(pageindex, pagesize));

            return new OperationResult<PageResult<SaleProductModel>>()
            {

                err_code = ErrorEnum.success,
                err_info = ErrorEnum.success.ToString(),
                entity = result.ToPageResult<productDto, SaleProductModel>(t => Convert(result))
            };
        }

        private IEnumerable<SaleProductModel> Convert(IPagedList<productDto> sources)
        {
            if (sources == null) return null;

            return sources.Select(t =>
                     {
                         var data = new SaleProductModel()
                         {
                             brandname = t.brand.name,
                             categoryname = t.category.name,
                             gendername = t.gender.name,
                             unitprice = t.unitprice,
                             code = t.code,
                             name = t.name,
                             imgpath = t.imagemain,
                             productid = t.key
                         };
                         data.details.AddRange(t.v_stocks.Select(o => new SaleProductDetailModel()
                         {
                             productid = t.key,
                             size = o.size.code,
                             displaysize = o.size.displaycode,
                             sizeid = o.sizeid,
                             stockquantity = o.stockquantity
                         }));
                         return data;
                     });
        }

        public async Task<OperationResult<productDto>> GetProductAsync(string code)
        {
            var result = productRepo.FindSingle(t => t.code == code);

            if (result == null)
                return new OperationResult<productDto>()
                {
                    err_code = ErrorEnum.data_not_found,
                    err_info = ErrorEnum.data_not_found.ToString()
                };

            return await Task.Factory.StartNew(() => new OperationResult<productDto>()
            {
                err_code = ErrorEnum.success,
                err_info = ErrorEnum.success.ToString(),
                entity = result
            });

        }
    }
}
