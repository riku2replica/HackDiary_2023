using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DiaryBase
{
    public class DiaryData
    {
        public String ID { get; set; }
        public String Title { get; set; }
        public String Entry { get; set; }
        public String Date { get; set; }
        
        public String Image { get; set; }

        public String Mood { get; set; }

        public static explicit operator DiaryData(string v)
        {
            throw new NotImplementedException();
        }
    }
}
