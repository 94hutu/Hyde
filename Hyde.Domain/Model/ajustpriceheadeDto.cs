using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain;
namespace Hyde.Domain.Model
{
    public class ajustpriceheadeDto : IEntityKey, IRowversion
    {
        public int key
        {
            get;

            set;
        }

        public byte[] lastchanged
        {
            get;

            set;
        }

        public string code { get; set; }

        public DateTime createdate { get; set; }

        public bool shutout { get; set; } = false;

        public string remark { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public virtual List<ajustpricedetailDto> details { get; set; } = new List<ajustpricedetailDto>();

    }
}
