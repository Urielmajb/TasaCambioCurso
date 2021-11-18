using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class JsonModel
    {
       public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }
        public string href { get; set; }
        public string function { get; set; }
        public JsonModel()
        {
            this.response = false;
            this.message = "Error favor revisar...";
        }
        public void SetResponse (bool r, string m="")
        {
            this.response = r;
            this.message = m;
            if (!r && message == "") this.message = "Error favor revisar.";
        }

    }
}
