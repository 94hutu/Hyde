using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain.Model;
using Hyde.Result.Operation;
namespace Hyde.Service
{
    public interface IGenderService : IService
    {
        Task<OperationResult<List<genderDto>>> GetGenderListAsync(bool? shutout=null);

        Task<OperationResult> AddGenderAsync(IEnumerable<genderDto> items);
    }
}
