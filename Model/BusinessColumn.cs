using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class BusinessColumn
    {
        public string title { get; set; }
        public List<string> legend { get; set; }
        public List<string> categories { get; set; }
        public List<SingleSerie> series { get; set; }
    }
}
