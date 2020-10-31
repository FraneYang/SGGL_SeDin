using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SingleSerie
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<double> data { get; set; }

        public List<string> dataString { get; set; }
    }
}
