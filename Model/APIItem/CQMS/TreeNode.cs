using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class TreeNode
    {
        public string Title { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public int Depth { get; set; }
        public List<TreeNode> child{get;set;}
    }
}