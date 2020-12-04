using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class AjaxStavkeIndexVM
    {
        public int PopravniIspitId { get; set; }

        public List<Rows> rows { get; set; }

        public class Rows
        {
            public int PopravniIspitStavkaId { get; set; }
            public string UcenikImePrezime { get; set; }
            public string OdjeljenjeOznaka { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool? IsPristupio { get; set; }
            public int Bodovi { get; set; }
        }
    }
}
