using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Api.Validation;
namespace Hyde.Api.Models.RequestModels
{
    public class SaleProductSearch
    {
        public int[] productids { get; set; }
       
        public int? genderid { get; set; }
     
        public int? brandid { get; set; }
     
        public int? categoryid { get; set; }

    }
}
