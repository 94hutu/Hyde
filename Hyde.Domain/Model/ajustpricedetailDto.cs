using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyde.Domain.Model
{
    public class ajustpricedetailDto : IEntityKey
    {
        public int key
        {
            get;

            set;
        }

        public int ajustpriceheadeid { get; set; }

        public int productid { get; set; }

        public decimal saleprice { get; set; }

        public ajustpriceheadeDto ajustpriceheade { get; set; }

        public productDto product { get; set; }

 
    }
}
