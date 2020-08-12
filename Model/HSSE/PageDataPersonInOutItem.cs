using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PageDataPersonInOutItem
    {
        public string PersonId
        {
            get;
            set;
        }

        public DateTime? ChangeTime
        {
            get;
            set;
        }

        public bool? IsIn
        {
            get;
            set;
        }
        public string WorkPostId
        {
            get;
            set;
        }

        public string PostType
        {
            get;
            set;
        }
    }
}