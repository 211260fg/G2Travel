using G10Travel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G10Travel.Requests
{
    class ListPostRequest
    {
        public string name { get; set; }
        public string location { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public virtual ICollection<Item> itemstobring { get; set; }
    }
}
