using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Result.Operation;
using Hyde.Domain.Model;
using Hyde.Repository;
using System.Data.Entity;
namespace Hyde.Service
{
    public class WarehouseService : IWarehouseService
    {

        private IUnitOfWork unitofwork;
        private IRepository<warehouseDto> warehouseRepo;

        public WarehouseService(IUnitOfWork UnitOfWork)
        {
            unitofwork = UnitOfWork;
            warehouseRepo = unitofwork.GetRepository<warehouseDto>();
        }

        public async Task<OperationResult> AddWareHouseAsync(IEnumerable<warehouseDto> items)
        {
            warehouseRepo.Add(items);

            await unitofwork.SaveAsync();

            return new OperationResult() { err_code = ErrorEnum.success, err_info = ErrorEnum.sys_error.ToString() };
        }

        public async Task<OperationResult<List<warehouseDto>>> GetWareHouseListAsync(bool? shutout = default(bool?))
        {
            var Query = warehouseRepo.Find().AsNoTracking();

            if (shutout.HasValue)
            {
                Query = Query.Where(t => t.shutout == shutout);
            }

            var result = await Query.ToListAsync();

            return new OperationResult<List<warehouseDto>>()
            {
                err_code = ErrorEnum.success,
                err_info = ErrorEnum.success.ToString(),
                entity = result
            };
        }
    }
}
