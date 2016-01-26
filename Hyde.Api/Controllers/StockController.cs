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
using Hyde.Api.Models.ReponseModels;
using Hyde.Api.Models.RequestModels;
using Hyde.Domain.Model;
using Hyde.Service.Models;
namespace Hyde.Api.Controllers
{
    [InvalidModelStateFilter]
    public class StockController : ApiController
    {

        private IStockService stockService;
        private IProductService productService;
        private IAdjustPriceService ajustpriceService;
        public StockController(IStockService StockService, IProductService ProductService, IAdjustPriceService AjustPriceService)
        {
            stockService = StockService;
            productService = ProductService;
            ajustpriceService = AjustPriceService;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> GetVstockList([FromBody]RequestSelectvstock vstockparameter, PageCommand page)
        {

            PageCommand Page = page ?? new PageCommand();

            var result = await stockService.GetVstockListAsync(Page.PageIndex, Page.PageSize, vstockparameter?.productids, vstockparameter?.genderid, vstockparameter?.categoryid, vstockparameter?.brandid);

            if (result.err_code != ErrorEnum.success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new OperationResult()
                {
                    err_code = result.err_code,
                    err_info = result.err_info
                });
            }

            var entity = await Convert(result.entity.PageList);


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

        private async Task<IEnumerable<StockViewModel>> Convert(IEnumerable<productDto> sources)
        {
            if (sources == null) return null;

            var pricecollection = (await ajustpriceService.GetSalePriceListAsync(DateTime.Now)).entity;

            return (from l in sources
                    join r in pricecollection on l.key equals r.productid into g
                    from x in g.DefaultIfEmpty()
                    select new { l, x }).Select(t =>
                    {
                        var data = new StockViewModel()
                        {
                            brandname = t.l.brand.name,
                            categoryname = t.l.category.name,
                            gendername = t.l.gender.name,
                            unitprice = t.l.unitprice,
                            code = t.l.code,
                            name = t.l.name,
                            saleprice = t.x == null ? t.l.unitprice : t.x.saleprice,
                            imgpath = t.l.imagemain,
                            productid = t.l.key,

                        };
                        data.details.AddRange(t.l.v_stocks.Select(o => new StockdetailViewModel()
                        {
                            productid = t.l.key,
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
