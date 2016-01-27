using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Hyde.Service;
using System.Net;
namespace Hyde.Api.Controllers
{
    public class BaseDataController : ApiController
    {
        IGenderService genderService;
        ICategroyService categoryService;
        IBrandService brandService;

        public BaseDataController(IGenderService GenderService, ICategroyService CategoryService, IBrandService BrandService)
        {
            genderService = GenderService;
            categoryService = CategoryService;
            brandService = BrandService;
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetGenderList(bool? shutout = null)
        {
            var result = await genderService.GetGenderListAsync(shutout);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetBrandList(bool? shutout = null)
        {
            var result = await brandService.GetBrandListAsync(shutout);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCategoryList(bool? shutout = null)
        {
            var result = await categoryService.GetCategoryListAsync(shutout);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
