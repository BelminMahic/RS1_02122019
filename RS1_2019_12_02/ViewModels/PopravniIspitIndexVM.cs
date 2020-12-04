using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitIndexVM
    {
        public List<Rows> rows { get; set; }

        public class Rows
        {
            public int OdjeljenjeId { get; set; }
            public string OdjeljenjeOznaka { get; set; }
            public string SkolaNaziv { get; set; }
            public string SkolskaGodinaNaziv { get; set; }
        }
    }
}
