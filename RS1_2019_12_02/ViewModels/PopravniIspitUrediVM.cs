using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitUrediVM
    {
        public int PopravniIspitId { get; set; }
        public int PredmetId { get; set; }
        public string PredmetNaziv { get; set; }
        public string DatumIspita { get; set; }
        public int SkolaId { get; set; }
        public string SkolaNaziv { get; set; }
        public int SkolskaId { get; set; }
        public string SkolskaGodNaziv { get; set; }
        public int OdjeljenjeId { get; set; }
        public string OdjeljenjeNaziv { get; set; }
    }
}
