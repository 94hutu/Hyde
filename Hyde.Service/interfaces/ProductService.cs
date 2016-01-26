using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Result.Operation;
using Hyde.Repository;
using System.Data.Entity;
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
                return new OperationResult() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString() }; }
            catch(Exception ex)
            {
                return new OperationResult() { err_code = ErrorEnum.sys_error, err_info = ex.Message };
            }
        }
    }
}
