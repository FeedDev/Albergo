using Albergo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionaleAlbergo.Controllers
{
    [Authorize]
    public class AlbergoController : Controller
    {
        ModelDbContext DbContext = new ModelDbContext();
        // GET: Albergo

        // da rivedere

        //public List<SelectListItem> ListaAnni
        //{
        //    get
        //    {
        //        DateTime date = DateTime.Now;
        //        string DateOut = date.ToString().Substring(6, 10);
        //        int num0 = Convert.ToInt32(DateOut);
        //        int num1 = Convert.ToInt32(DateOut) + 1;
        //        int num2 = Convert.ToInt32(DateOut) + 2;
        //        List<SelectListItem> _tempList = new List<SelectListItem>
        //        {
        //            new SelectListItem { Text = num0.ToString(), Value = num0.ToString() },
        //            new SelectListItem { Text = num1.ToString(), Value = num1.ToString() },
        //            new SelectListItem { Text = num2.ToString(), Value = num2.ToString() },
        //        };
        //        return _tempList;
        //    }
        //}

        public List<SelectListItem> ListaCamere
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Camera c in DbContext.Camera.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = c.CodiceCamera,
                        Value = c.IdCamera.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
        public List<SelectListItem> ListaServizi
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Servizio s in DbContext.Servizio.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = s.NomeServizio,
                        Value = s.IdServizio.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
        public List<SelectListItem> ListaTipologia
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Tipologia t in DbContext.Tipologia.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = t.NomeTipologia,
                        Value = t.IdTipologia.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
        public List<SelectListItem> ListaTipologiaCamera
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                    SelectListItem singola = new SelectListItem
                    {
                        Text = "Singola",
                        Value = "Singola"
                    };
                    _templist.Add(singola);

                SelectListItem doppia = new SelectListItem
                {
                    Text = "Doppia",
                    Value = "Doppia"
                };
                _templist.Add(doppia);

                return _templist;
            }
        }
        public List<SelectListItem> DropDownPrenotazioni
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Prenotazione p in DbContext.Prenotazione.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = p.IdPrenotazione.ToString(),
                        Value = p.IdPrenotazione.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User u)
        {

            User u2 = DbContext.User.Where(x => x.Username == u.Username && x.Password == u.Password).FirstOrDefault();
            if (u2.Username.Length > 0 && u2.Password.Length > 0)
            {
                FormsAuthentication.SetAuthCookie(u2.Username, false);
            }
            else
            {
                ViewBag.Errore = "Autenticazione non riuscita";
                return View();
            }
            return RedirectToAction("Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        public JsonResult RicercaPrenotazione(string s)
        {
            List<Prenotazione> listaPrenotazioni = new List<Prenotazione>();
            Prenotazione p = new Prenotazione();
            Cliente c = new Cliente();
            try
            {
                c = DbContext.Cliente.Where(x => x.CodiceFiscale == s).FirstOrDefault();
                listaPrenotazioni = DbContext.Prenotazione.Where(x => x.IdClienteFk == c.IdCliente).ToList();

                List<PrenotazioniToJson> listaToReturn = new List<PrenotazioniToJson>();
                foreach (Prenotazione prenotazione in listaPrenotazioni)
                {
                    PrenotazioniToJson prenotazioniToJson = new PrenotazioniToJson
                    {
                        IdPrenotazione = prenotazione.IdPrenotazione,
                        DataPrenotazione = prenotazione.DataPrenotazione.ToString("d"),
                        CodiceCamera = prenotazione.Camera.CodiceCamera,
                        Tipologia = prenotazione.Tipologia.NomeTipologia,
                        Anno = prenotazione.Anno,
                        InizioSoggiorno = prenotazione.InizioSoggiorno.ToString("d"),
                        FineSoggiorno = prenotazione.FineSoggiorno.ToString("d"),
                        Caparra = prenotazione.Caparra,
                        Tariffa = prenotazione.Tariffa,
                        CodiceFiscale = prenotazione.Cliente.CodiceFiscale
                    };
                    listaToReturn.Add(prenotazioniToJson);
                }

                return Json(listaToReturn, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                //ViewBag.Errore = "Errore nella ricerca";
                return Json("KO", JsonRequestBehavior.AllowGet);
            }
        }

        public class PrenotazioniToJson
        {
            public string CodiceFiscale { get; set; }
            public int IdPrenotazione { get; set; }
            public string DataPrenotazione { get; set; }
            public string Anno { get; set; }
            public string InizioSoggiorno { get; set; }
            public string FineSoggiorno { get; set; }
            public decimal Caparra { get; set; }
            public decimal Tariffa { get; set; }
            public string Tipologia { get; set; }
            public string CodiceCamera { get; set; }
            public decimal Totale { get; set; }
        }

        public JsonResult CountPensioni()
        {
            int n;
            try
            {
               n = DbContext.Prenotazione.Where(x => x.IdTipologiaFk == 2 && x.Cancellato == false).Count();
               return Json(n, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("KO", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreatePrenotazione()
        {
            ViewBag.ListaServizi = ListaServizi;
            ViewBag.ListaTipologia = ListaTipologia;
            //ViewBag.ListaAnni = ListaAnni;
            ViewBag.ListaCamere = ListaCamere;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePrenotazione(Prenotazione p)
        {
            Cliente c = new Cliente();
            Cliente c1 = new Cliente();
            if (ModelState.IsValid)
            {
                try
                {
                    c = DbContext.Cliente.Where(x => x.CodiceFiscale == p.Cliente.CodiceFiscale).FirstOrDefault();
                    if (c != null)
                    {
                        p.Cliente = c;
                    }
                    else
                    {
                        p.IdClienteFk = p.Cliente.IdCliente;
                        p.DataPrenotazione = DateTime.Now;
                    }
                    DbContext.Prenotazione.Add(p);
                    DbContext.SaveChanges();

                    return RedirectToAction("ListaPrenotazioni");
                }
                catch (Exception ex)
                {
                    ViewBag.Errore = "Errore nell'aggiunta della prenotazione";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nella compilazione";
                return View();
            }
            
        }

        public ActionResult ListaPrenotazioni()
        {
            return View(DbContext.Prenotazione.ToList());
        }

        [HttpGet]
        public ActionResult EditPrenotazione(int id)
        {
            Prenotazione p = DbContext.Prenotazione.Find(id);
            ViewBag.ListaTipologia = ListaTipologia;
            ViewBag.ListaCamere = ListaCamere;
            return View(p);
        }

        [HttpPost]
        public ActionResult EditPrenotazione(Prenotazione p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    DbContext.SaveChanges();

                    return RedirectToAction("ListaPrenotazioni");
                }
                catch (Exception ex)
                {
                    ViewBag.Errore = "Errore nella modifca della prenotazione";
                    return View();
                } 
            }
            else
            {
                ViewBag.Errore = "Errore nella modifca della prenotazione";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeletePrenotazione(int id)
        {
            Prenotazione p = DbContext.Prenotazione.Find(id);
            return View(p);
        }

        [HttpPost]
        [ActionName("DeletePrenotazione")]
        public ActionResult DeletePrenotazionePost(int id)
        {
            Prenotazione p = DbContext.Prenotazione.Find(id);
            p.Cancellato = true;
            DbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("ListaPrenotazioni");
        }

        [HttpGet]
        public ActionResult DetailsPrenotazione(int id)
        {
            Prenotazione p = DbContext.Prenotazione.Find(id);
            var ListaServizi = DbContext.PivotPrenotazioneServizio.Where(x => x.IdPrenotazione == id).ToList();


            return View(p);
        }

        public JsonResult TotaleConto(int id)
        {
            Prenotazione p = new Prenotazione();
            try
            {
                p = DbContext.Prenotazione.Find(id);
                decimal caparra = p.Caparra;
                decimal tariffa = p.Tariffa;
                decimal totale = 0;
                decimal parziale_servizi = 0;
                foreach (PivotPrenotazioneServizio pp in p.PivotPrenotazioneServizio)
                {
                    if (pp.Cancellato == false)
                    {
                        decimal singolo_servizio = 0;
                        singolo_servizio = pp.Quantità * pp.Servizio.Prezzo;
                        parziale_servizi += singolo_servizio;
                    }
                }
                totale = tariffa - caparra + parziale_servizi;
                return Json(totale, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("KO", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewCamere()
        {
            return View(DbContext.Camera.ToList());
        }

        [HttpGet]
        public ActionResult CreateCamera()
        {
            ViewBag.ListaTipologiaCamera = ListaTipologiaCamera;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCamera(Camera c)
        {
            Camera c1 = new Camera();
            if (ModelState.IsValid)
            {
                try
                {
                    c1 = DbContext.Camera.Where(x => x.CodiceCamera == c.CodiceCamera).FirstOrDefault();
                    if (c1 != null)
                    {
                        c = c1;
                        ViewBag.Errore = "La camera esiste già";
                        ViewBag.ListaTipologiaCamera = ListaTipologiaCamera;
                        return View();
                    }
                    else
                    {
                        DbContext.Camera.Add(c);
                        DbContext.SaveChanges();
                        return RedirectToAction("ViewCamere");
                                            
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Errore = "Errore nell'aggiunta della camera";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nella compilazione";
                return View();
            }

        }

        [HttpGet]
        public ActionResult EditCamera (int id)
        {
            Camera c = DbContext.Camera.Find(id);
            ViewBag.ListaTipologiaCamera = ListaTipologiaCamera;
            return View(c);
        }

        [HttpPost]
        public ActionResult EditCamera(Camera c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        DbContext.Entry(c).State = System.Data.Entity.EntityState.Modified;
                        DbContext.SaveChanges();
                        return RedirectToAction("ViewCamere");
                }
                catch (Exception)
                {
                    ViewBag.Errore = "Errore nella modifca della camera";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nella modifca della camera";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteCamera (int id)
        {
            Camera c = DbContext.Camera.Find(id);
            return View(c);
        }
        [HttpPost]

        [ActionName("DeleteCamera")]
        public ActionResult DeleteCameraPost (int id)
        {
            Camera c = DbContext.Camera.Find(id);
            c.Cancellato = true;
            DbContext.Entry(c).State = System.Data.Entity.EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("ViewCamere");

        }

        [HttpGet]
        public ActionResult ViewServizi()
        {
            return View(DbContext.Servizio.ToList());
        }

        [HttpGet]
        public ActionResult CreateServizio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateServizio(Servizio s)
        {
            Servizio s1 = new Servizio();
            if (ModelState.IsValid)
            {
                try
                {
                    s1 = DbContext.Servizio.Where(x => x.NomeServizio == s.NomeServizio).FirstOrDefault();
                    if (s1 != null)
                    {
                        s = s1;
                        ViewBag.Errore = "Il servizio esiste già";
                        return View();
                    }
                    else
                    {

                        DbContext.Servizio.Add(s);
                        DbContext.SaveChanges();
                        return RedirectToAction("ViewServizi");
                    }
                }
                catch (Exception)
                {
                    ViewBag.Errore = "Il servizio esiste già";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Il servizio esiste già";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditServizio(int id)
        {
            Servizio s = DbContext.Servizio.Find(id);
            return View(s);
        }

        [HttpPost]
        public ActionResult EditServizio(Servizio s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DbContext.Entry(s).State = System.Data.Entity.EntityState.Modified;
                    DbContext.SaveChanges();
                    return RedirectToAction("ViewServizi");
                }
                catch (Exception)
                {
                    ViewBag.Errore = "Errore nella modifca del servizio";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nella modifca del servizio";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteServizio(int id)
        {
            Servizio s = DbContext.Servizio.Find(id);
            return View(s);
        }

        [HttpPost]
        [ActionName("DeleteServizio")]
        public ActionResult DeleteServizioPost (int id)
        {
            Servizio s = DbContext.Servizio.Find(id);
            s.Cancellato = true;
            DbContext.Entry(s).State = System.Data.Entity.EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("ViewServizi");
        }

        [HttpGet]
        public ActionResult AddServizio (int id)
        {
            PivotPrenotazioneServizio p = new PivotPrenotazioneServizio();
            p.IdPrenotazione = id;
            ViewBag.ListaServizi = ListaServizi;
            //ViewBag.ListaPrenotazioni = DropDownPrenotazioni;
            return View(p);
        }

        [HttpPost] public ActionResult AddServizio (PivotPrenotazioneServizio pivot)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    if (pivot.Quantità < 1)
                    {
                        ViewBag.Errore = "Non puoi mettere quantità inferiore a 1";
                        return View();
                    }
                    pivot.DataServizio = DateTime.Now;
                    DbContext.PivotPrenotazioneServizio.Add(pivot);
                    DbContext.SaveChanges();
                    return RedirectToAction ("ListaPrenotazioni");
                }
                catch (Exception)
                {
                    ViewBag.Errore = "Errore nell'aggiunta del servizio";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nell'aggiunta del servizio";
                return View();
            }
        }


        [HttpGet]
        public ActionResult AddServizioPrenotazione(int id)
        {
     
            PivotPrenotazioneServizio p = new PivotPrenotazioneServizio();
            p.IdPrenotazione = id;
            ViewBag.ListaServizi = ListaServizi;
            //ViewBag.ListaPrenotazioni = DropDownPrenotazioni;
            return View(p);
        }


        [HttpPost]
        public ActionResult AddServizioPrenotazione(PivotPrenotazioneServizio pivot)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pivot.Quantità < 1)
                    {
                        ViewBag.Errore = "Non puoi mettere quantità inferiore a 1";
                        ViewBag.ListaServizi = ListaServizi;
                        return View();
                    }
                    pivot.DataServizio = DateTime.Now;
                    DbContext.PivotPrenotazioneServizio.Add(pivot);
                    DbContext.SaveChanges();
                    return RedirectToAction("DetailsPrenotazione", new { id = pivot.IdPrenotazione});
                }
                catch (Exception)
                {
                    ViewBag.Errore = "Errore nell'aggiunta del servizio";
                    return View();
                }
            }
            else
            {
                ViewBag.Errore = "Errore nell'aggiunta del servizio";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteServizioPrenotazione(int id)
        {
            PivotPrenotazioneServizio p = DbContext.PivotPrenotazioneServizio.Find(id);
            p.Cancellato = true;
            DbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("DetailsPrenotazione", new {id = p.IdPrenotazione});
        }
    }

}