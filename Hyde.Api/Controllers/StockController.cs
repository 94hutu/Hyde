using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Hyde.Api.Filters;
using System.Net;
using System.Net.Http;
using Hyde.Service;
using Hyde.Api.Models.RequestCommands;
using Hyde.Result.Operation;
using Hyde.Api.Models.RequestModels;
using Hyde.Domain.Model;
namespace Hyde.Api.Controllers
{
    [InvalidModelStateFilter]
    public class StockController : ApiController
    {

        private IStockService stockService;
        private IProductService productService;

        public StockController(IStockService StockService, IProductService ProductService)
        {
            stockService = StockService;
            productService = ProductService;
        }
        [HttpPost]

        public async Task<HttpResponseMessage> GetVstockList(PageCommand page = null)
        {

            PageCommand Page = page ?? new PageCommand();

            var result = await stockService.GetVstockListAsync(Page.PageIndex, Page.PageSize);

            if (result.err_code != ErrorEnum.success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new OperationResult()
                {
                    err_code = result.err_code,
                    err_info = result.err_info
                });
            }

            var entity = Convert(result.entity.PageList);

            return Request.CreateResponse(HttpStatusCode.OK, new OperationResult<PageResult<StockViewModel>>()
            {
                err_code = result.err_code,
                err_info = result.err_info,
                entity = new PageResult<StockViewModel>()
                {
                    PageList = entity,
                    PageIndex = result.entity.PageIndex,
                    PageSize = result.entity.PageSize,
                    TotalItem = result.entity.TotalItem,
                    TotalPage = result.entity.TotalPage
                }
            });

        }

        private IEnumerable<StockViewModel> Convert(IEnumerable<productDto> source)
        {
            if (source == null) return null;

            return source.Select(t =>
            {
                var data = new StockViewModel()
                {
                    brandname = t.brand.name,
                    categoryname = t.category.name,
                    gendername = t.gender.name,
                    unitprice = t.unitprice,
                    code = t.code,
                    name = t.name,
                    saleprice = t.unitprice * 0.7M,
                    imgpath = t.imagemain,
                    productid = t.key,

                };

                data.details.AddRange(t.v_stocks.Select(o => new StockdetailViewModel()
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
    }
}
