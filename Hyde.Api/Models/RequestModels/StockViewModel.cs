using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyde.Api.Models.RequestModels
{
    public class StockViewModel
    {
        public int productid { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string brandname { get; set; }

        public string categoryname { get; set; }

        public string gendername { get; set; }

        public decimal unitprice { get; set; }

        public decimal saleprice { get; set; }

        public string imgpath { get; set; }

        public List<StockdetailViewModel> details { get; set; } = new List<StockdetailViewModel>();
    }

    public class StockdetailViewModel
    {
        public int productid { get; set; }

        public string size { get; set; }

        public string displaysize { get; set; }

        public int sizeid { get; set; }

        public decimal stockquantity { get; set; }
    }
}
