using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class ResponseData<T>
    {
        private bool success;
        public bool successful {
            get { return success; }

            set
            {
                this.success = value;
                if (success)
                    code = 1;
                else
                    code = 0;
            }
        }
        public T resultValue { get; set; }
        public string resultHint
        {
            get { return message; }

            set
            {
                 
                message = value;
            }
        }
        public string errorPage { get; set; } 
        public string type   { get; set; } 
        public int count { get; set; } 
        public int code { get; set; }
        public string message { get; set; }
    }
}