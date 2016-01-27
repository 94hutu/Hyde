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
using Hyde.Service.Models;
namespace Hyde.Api.Controllers
{
    [InvalidModelStateFilter]
    public class ProductController : ApiController
    {
        private IProductService productService;
        private IAdjustPriceService ajustpriceService;
        public ProductController(IProductService ProductService, IAdjustPriceService AjustPriceService)
        {

            productService = ProductService;
            ajustpriceService = AjustPriceService;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> GetSaleProductList([FromBody]SaleProductSearch vstockparameter, PageCommand page)
        {
            PageCommand Page = page ?? new PageCommand();

            var saleproductcollection = await productService.GetProductListAsync(Page.PageIndex, Page.PageSize, vstockparameter?.productids, vstockparameter?.genderid, vstockparameter?.categoryid, vstockparameter?.brandid);

            if (saleproductcollection.err_code != ErrorEnum.success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new OperationResult()
                {
                    err_code = saleproductcollection.err_code,
                    err_info = saleproductcollection.err_info
                });
            }

            var salepricecollection = await ajustpriceService.GetSalePriceListAsync(DateTime.Now);

            if (salepricecollection.err_code != ErrorEnum.success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new OperationResult()
                {
                    err_code = salepricecollection.err_code,
                    err_info = salepricecollection.err_info
                });
            }

            var result = (from l in saleproductcollection.entity.PageList
                          join r in salepricecollection.entity on l.productid equals r.productid into g
                          from x in g.DefaultIfEmpty()
                          select new { l, x }).ToList();

            result.ForEach(t => t.l.saleprice = t.x?.saleprice ?? t.l.unitprice);

            saleproductcollection.entity.PageList = result.Select(t => t.l);

            return Request.CreateResponse(HttpStatusCode.OK, saleproductcollection);
        }

        [HttpGet]
        //[EmptyParameterFilter("code")]
        public async Task<HttpResponseMessage> GetProduct(string code)
        {
            var result = await productService.GetProductAsync(code);

            if (result.err_code != ErrorEnum.success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new OperationResult()
                {
                    err_code = result.err_code,
                    err_info = result.err_info
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
