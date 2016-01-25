using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Repository;
using Hyde.Result.Operation;
using System.Data.Entity;
namespace Hyde.Service
{
    public class GenderService : IGenderService
    {
        IUnitOfWork unitOfwork;
        IRepository<genderDto> genderRepo;
        public GenderService(IUnitOfWork UnitOfWork)
        {
            unitOfwork = UnitOfWork;
            genderRepo = unitOfwork.GetRepository<genderDto>();
        }
        public async Task<OperationResult<List<genderDto>>> GetGenderListAsync(bool? shutout = null)
        {
            var Query = genderRepo.Find().AsNoTracking();

            if (shutout.HasValue)
            {
                Query = Query.Where(t => t.shutout == shutout);
            }

            var result = await Query.ToListAsync();

            return new OperationResult<List<genderDto>>() { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString(), entity = result };
        }

        public async Task<OperationResult> AddGenderAsync(IEnumerable<genderDto> items)
        {
            genderRepo.Add(items);
            await unitOfwork.SaveAsync();

            return new OperationResult { err_code = ErrorEnum.success, err_info = ErrorEnum.success.ToString() };
        }
    }
}
