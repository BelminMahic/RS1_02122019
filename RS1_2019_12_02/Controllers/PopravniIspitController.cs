using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_2019_12_02.EF;
using RS1_2019_12_02.EntityModels;
using RS1_2019_12_02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.Controllers
{
    public class PopravniIspitController : Controller
    {
        private readonly MojContext db;

        public PopravniIspitController(MojContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var index = new PopravniIspitIndexVM
            {
                rows=db.Odjeljenje.Select(
                    x=>new PopravniIspitIndexVM.Rows
                    {
                        OdjeljenjeId=x.Id,
                        OdjeljenjeOznaka=x.Oznaka,
                        SkolaNaziv=x.Skola.Naziv,
                        SkolskaGodinaNaziv=x.SkolskaGodina.Naziv
                    }
                    ).OrderBy(t=>t.SkolskaGodinaNaziv).ToList()
            };
            return View(index);
        }
        public IActionResult Display(int id)
        {
            var odjeljenje = db.Odjeljenje.Include(x=>x.Skola).Include(x=>x.SkolskaGodina).Where(x=>x.Id==id).FirstOrDefault();

            //provjeriti
            var prikazi = new PopravniIspitDisplayVM
            {
                OdjeljenjeId = odjeljenje.Id,
                SkolaId=odjeljenje.Skola.Id,
                SkolaNaziv=odjeljenje.Skola.Naziv,
                OdjeljenjeOznaka=odjeljenje.Oznaka,
                SkolskaId=odjeljenje.SkolskaGodina.Id,
                SkolskaGodinaNaziv=odjeljenje.SkolskaGodina.Naziv,
                rows=db.PopravniIspit.Where(x=>x.OdjeljenjeId==odjeljenje.Id && x.Skola.Id==odjeljenje.Skola.Id && x.SkolskaGodina.Id==odjeljenje.SkolskaGodina.Id)
                     .Select(x=>new PopravniIspitDisplayVM.Rows { 
                        PopravniIspitId=x.Id,
                        DatumPopravnogIspita=x.Datum.ToString("dd.MM.yyyy"),
                        PredmetId=x.Predmet.Id,
                        PredmetNaziv=x.Predmet.Naziv,
                        BrojUcenikaNaPopravnomIspitu=db.PopravniIspitStavka.Where(i=>i.PopravniIspitId==x.Id).Count(),//potencijalno pucanje
                        BrojUcenikaKojiJesuPolozili=db.PopravniIspitStavka.Where(i=>i.Bodovi>50 && i.PopravniIspitId==x.Id).Count() //potencijalno pucanje
                     
                     }).ToList()

            };



            return View(prikazi);
        }
        public IActionResult Add(int skolskaId,int skolaId,int odjeljenjeId)
        {

            var dodaj = new PopravniIspitDodajVM
            {
                SkolaId=db.Skola.Find(skolaId).Id,
                SkolaNaziv= db.Skola.Find(skolaId).Naziv,
                OdjeljenjeId= db.Odjeljenje.Find(odjeljenjeId).Id,
                OdjeljenjeNaziv = db.Odjeljenje.Find(odjeljenjeId).Oznaka,
                SkolskaGodId=db.SkolskaGodina.Find(skolskaId).Id,
                SkolskaNaziv = db.SkolskaGodina.Find(skolskaId).Naziv,
                predmeti=db.Predmet.Select(
                     x=>new SelectListItem
                     {
                         Value=x.Id.ToString(),
                         Text=x.Naziv,
                         Selected=false
                     }
                     
                     ).ToList(),
                DatumIspita=DateTime.Now

            };


            return View(dodaj);
        }

        public IActionResult Save(PopravniIspitDodajVM model)
        {
            var popravniIspit = new PopravniIspit
            {
                OdjeljenjeId=model.OdjeljenjeId,
                SkolaId=model.SkolaId,
                SkolskaGodinaId=model.SkolskaGodId,
                PredmetId=model.PredmetId,
                Datum=model.DatumIspita
                
            };
            db.Add(popravniIspit);
            db.SaveChanges();

            var ucenici = db.OdjeljenjeStavka.Where(x => x.Odjeljenje.Skola.Id == model.SkolaId && x.Odjeljenje.Id == model.OdjeljenjeId && x.Odjeljenje.SkolskaGodina.Id == model.SkolskaGodId).ToList();

            foreach (var u in ucenici)
            {
                var negativnaOcjena = db.DodjeljenPredmet.Where(x => x.Predmet.Id == model.PredmetId && x.OdjeljenjeStavkaId == u.Id && x.ZakljucnoKrajGodine == 1).ToList();

                var triNegativne = db.DodjeljenPredmet.Where(x => x.ZakljucnoKrajGodine == 1 && x.OdjeljenjeStavka.Id == u.Id).ToList();

                if(triNegativne.Count()>=3)
                {
                    var popravniIspitStavka = new PopravniIspitStavka
                    {
                        PopravniIspitId = popravniIspit.Id,
                        OdjeljenjeStavkaId = u.Id,
                        Bodovi = 0,
                        IsPristupio = null

                    };
                    db.Add(popravniIspitStavka);
                    db.SaveChanges();
                }
                else if(negativnaOcjena.Any())
                {
                    var popravniIspitStavka = new PopravniIspitStavka
                    {
                        PopravniIspitId = popravniIspit.Id,
                        OdjeljenjeStavkaId = u.Id,
                        IsPristupio = false
                    };
                    db.Add(popravniIspitStavka);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index","PopravniIspit");//ne zaboravi redirect
        }

        public IActionResult Edit(int id)
        {

            var popravniIspit = db.PopravniIspit.Where(x => x.Id == id)
                .Include(x => x.Odjeljenje)
                .Include(x => x.Skola)
                .Include(x => x.SkolskaGodina)
                .Include(x=>x.Predmet)
                .FirstOrDefault();

            var uredi = new PopravniIspitUrediVM
            {
                PopravniIspitId=id,
                OdjeljenjeId=popravniIspit.OdjeljenjeId,
                OdjeljenjeNaziv=popravniIspit.Odjeljenje.Oznaka,
                PredmetId=popravniIspit.Predmet.Id,
                PredmetNaziv=popravniIspit.Predmet.Naziv,
                DatumIspita=popravniIspit.Datum.ToString("dd.MM.yyyy"),
                SkolaId=popravniIspit.Skola.Id,
                SkolaNaziv=popravniIspit.Skola.Naziv,
                SkolskaId=popravniIspit.SkolskaGodina.Id,
                SkolskaGodNaziv=popravniIspit.SkolskaGodina.Naziv
            };


            return View(uredi);
        }
    }
}
