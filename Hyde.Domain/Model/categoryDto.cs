using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain;
namespace Hyde.Domain.Model
{
    public class categoryDto : IEntityKey
    {

        public categoryDto()
        {
            children = new List<categoryDto>();
        }


        public int key { get; set; }

        public string name { get; set; }

        public int? pid { get; set; }

        public bool shutout { get; set; } = false;

        public virtual List<categoryDto> children { get; set; }

    }
}
