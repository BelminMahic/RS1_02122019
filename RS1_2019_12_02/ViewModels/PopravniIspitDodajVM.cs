using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public List<SelectListItem> predmeti { get; set; }
        public int PredmetId { get; set; }
        public DateTime DatumIspita { get; set; }
        public string SkolaNaziv { get; set; }
        public int SkolaId { get; set; }
        public string SkolskaNaziv { get; set; }
        public int SkolskaGodId { get; set; }
        public string OdjeljenjeNaziv { get; set; }
        public int OdjeljenjeId { get; set; }

    }
}
