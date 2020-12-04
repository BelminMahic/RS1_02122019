using Microsoft.AspNetCore.Mvc;
using RS1_2019_12_02.EF;
using RS1_2019_12_02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private readonly MojContext db;

        public AjaxStavkeController(MojContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int id)
        {
            var popravniIspit = db.PopravniIspit.Find(id);


            var index =new AjaxStavkeIndexVM
            {
                PopravniIspitId=popravniIspit.Id,
                rows=db.PopravniIspitStavka.Where(i=>i.PopravniIspitId==popravniIspit.Id)
                     .Select(x=>new AjaxStavkeIndexVM.Rows { 
                     PopravniIspitStavkaId=x.Id,
                     UcenikImePrezime=x.OdjeljenjeStavka.Ucenik.ImePrezime,
                     Bodovi=x.Bodovi,
                     OdjeljenjeOznaka=x.OdjeljenjeStavka.Odjeljenje.Oznaka,
                     BrojUDnevniku=x.OdjeljenjeStavka.BrojUDnevniku,
                     IsPristupio=x.IsPristupio
                     
                     }).ToList()

            };

            return PartialView(index);
        }
    }
}
