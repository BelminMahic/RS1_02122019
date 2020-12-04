using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitDisplayVM
    {
        public int OdjeljenjeId { get; set; }
        public string OdjeljenjeOznaka { get; set; }
        public int SkolskaId { get; set; }
        public string SkolskaGodinaNaziv { get; set; }
        public int SkolaId { get; set; }
        public string SkolaNaziv { get; set; }

        public List<Rows> rows { get; set; }

        public class Rows
        {
            public int PopravniIspitId { get; set; }
            public string DatumPopravnogIspita { get; set; }
            public int PredmetId { get; set; }
            public string PredmetNaziv { get; set; }
            public int BrojUcenikaNaPopravnomIspitu { get; set; }
            public int BrojUcenikaKojiJesuPolozili { get; set; }
        }
    }
}
