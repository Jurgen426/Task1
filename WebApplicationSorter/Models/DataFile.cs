using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationSorter.Models
{
    public class DataFile
    {
        public List<RawData> list { get; set; }

        public DataFile()
        {
            list = new List<RawData>();
        }
          
    }
}