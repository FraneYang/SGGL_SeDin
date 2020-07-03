using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class ToDoModel
    {
        public string Title { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public object Data { get; set; }
    }
}