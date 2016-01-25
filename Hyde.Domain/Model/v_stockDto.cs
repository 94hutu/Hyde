using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyde.Domain.Model
{
    public class v_stockDto
    {
        public int warehouseid { get; set; }

        public int productid { get; set; }

        public int sizeid { get; set; }

        public decimal stockquantity { get; set; }

        public decimal usablequantity { get; set; }
          
        public int supplyid { get; set; }

        public warehouseDto warehouse { get; set; }

        public productDto product { get; set; }

        public sizeDto size { get; set; }

        public supplyDto supply { get; set; }


    }
}
