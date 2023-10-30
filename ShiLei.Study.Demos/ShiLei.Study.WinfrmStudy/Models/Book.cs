using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiLei.Study.WinfrmStudy
{
    public class Book:EventArgs
    {
        public string BookName { get; set; }
        public Double Price { get; set; }
        public string BookInfo { get; set; }
    }
}
