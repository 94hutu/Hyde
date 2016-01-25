using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Domain;
namespace Hyde.Domain.Model
{
    public class genderDto : IEntityKey
    {
        public int key
        {
            get;

            set;
        }

        public string name { get; set; }

        public bool shutout { get; set; }

        public string imgpath { get; set; }
    }
}
