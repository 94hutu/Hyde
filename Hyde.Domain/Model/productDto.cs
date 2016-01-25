using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain;
namespace Hyde.Domain.Model
{
    public class productDto : IEntityKey
    {

        public productDto()
        {
            shutout = false;
            images = new List<productimageDto>();
            skus = new List<skuDto>();
        }

        public int key
        {
            get;
            set;
        }


        public string code { get; set; }

        public string name { get; set; }

        public DateTime arrivaldate { get; set; }

        public DateTime createdate { get; set; }

        public int categroyid { get; set; }

        public int brandid { get; set; }

        public int genderid { get; set; }

        public int? rangeid { get; set; }

        public string sizegroup { get; set; }

        public bool shutout { get; set; }

        public decimal unitprice { get; set; }

        public string description { get; set; }

        public string imagemain { get; set; }

        public virtual List<productimageDto> images { get; set; }

        public brandDto brand { get; set; }

        public categoryDto category { get; set; }

        public genderDto gender { get; set; }

        public rangeDto range { get; set; }

        public virtual List<skuDto> skus { get; set; } = new List<skuDto>();

        public virtual List<v_stockDto> v_stocks { get; set; } = new List<v_stockDto>();

    }
}
