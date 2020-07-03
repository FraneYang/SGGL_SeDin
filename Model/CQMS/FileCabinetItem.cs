using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileCabinetItem
    {
        public string FileCabinetId
        {
            get;
            set;
        }

        public string ProjectId
        {
            get;
            set;
        }

        public string FileType
        {
            get;
            set;
        }

        public string FileCode
        {
            get;
            set;
        }
        public string FileContent
        {
            get;
            set;
        }
        public DateTime? FileDate
        {
            get;
            set;
        }
        public string CreateManId
        {
            get;
            set;
        }
        public string CreateManName
        {
            get;
            set;
        }
        public string FileUrl
        {
            get;
            set;
        }
    }
}
