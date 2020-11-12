using MVCApplication.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using WcfService;
using WcfService.Models;

namespace MVCApplication.Controllers
{
    public class OsobyController : Controller
    {
        readonly DataProviderServiceClient WcfServiceClient = new DataProviderServiceClient();

        //can be improved by creating separate class FilterHander (would be more readable)
        public ActionResult Index(string menoSearchString, string priezviskoSearchString, string datumNarodeniaSearchDate, string ulicaSearchStrirng, string cisloSearchString)
        {
            //list of Osoba from xml deserialization
            List<Osoba> result = WcfServiceClient.GetOsobyList().OsobyList;

            //these operations below can be put into separate class OsobyListFilter or be included in OsobaCollection in Wcf model
            if (menoSearchString != null && menoSearchString != "")
            {
                result = new List<Osoba>();
                var temp = WcfServiceClient.GetOsobyByMeno(menoSearchString).ToList();
                if (temp.Count > 0)
                {
                    result.AddRange(temp);
                }
            }
            if (priezviskoSearchString != null && priezviskoSearchString != "" && result.Count > 0)
            {
                var temp = (WcfServiceClient.GetOsobyByPriezvisko(priezviskoSearchString, result.ToArray())).ToList();
                result = new List<Osoba>();
                if (temp.Count > 0)
                {
                    result.AddRange(temp);
                }
            }
            if (datumNarodeniaSearchDate != null && datumNarodeniaSearchDate != "" && result.Count > 0)
            {
                //first "datumNarodeniaSearchDate" should be checked for correct format but if it is expected to have it fixed then there is no need
                var datumNarodeniaSearchDateString = Convert.ToDateTime(datumNarodeniaSearchDate);
                var temp = (WcfServiceClient.GetOsobyByDatumNarodenia(datumNarodeniaSearchDateString, result.ToArray())).ToList();
                result = new List<Osoba>();
                if (temp.Count > 0)
                {
                    result.AddRange(temp);
                }
            }
            if (ulicaSearchStrirng != null && ulicaSearchStrirng != "" && result.Count > 0)
            {
                var temp = (WcfServiceClient.GetOsobyByUlica(ulicaSearchStrirng, result.ToArray())).ToList();
                result = new List<Osoba>();
                if (temp.Count > 0)
                {
                    result.AddRange(temp);
                }
            }
            //street number will not contain big numbers so Int16 is more than enough
            if (cisloSearchString != null && cisloSearchString != "" && result.Count > 0)
            {
                var temp = (WcfServiceClient.GetOsobyByCislo(Convert.ToInt16(cisloSearchString), result.ToArray())).ToList();
                result = new List<Osoba>();
                if (temp.Count > 0)
                {
                    result.AddRange(temp);
                }
            }
            return View(result.Distinct());
        }

        public ActionResult Details(int id)
        {
            if (id < 0) id = 0;
            Osoba detail = WcfServiceClient.GetOsobyList().OsobyList.Find(x => x.ID == id);
            return View(detail);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Osoba o)
        {
            WcfServiceClient.AddOsoba(o.Meno, o.Priezvisko, o.DatumNarodenia, o.Adresa.Ulica, o.Adresa.Cislo);
            return RedirectToAction("Index", "Osoby");

        }
        public ActionResult Delete(int id)
        {
            int retval = WcfServiceClient.DeleteOsobaById(id);
            if (retval > 0)
            {
                return RedirectToAction("Index", "Osoby");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {

            return View(WcfServiceClient.GetOsobaByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Osoba o)
        {
            if (ModelState.IsValid)
            {
                WcfServiceClient.EditOsoba(o.ID, o.Meno, o.Priezvisko, o.DatumNarodenia, o.Adresa.Ulica, o.Adresa.Cislo);
                return RedirectToAction("Index");
            }
            return View(o);
        }

        //function of level 1 assignment (not used in t his project so it's commented out)
        //public ActionResult OsobyWithLessAge(int age)
        //{
        //    List<Osoba> lstRecord = new List<Osoba>();
        //    foreach (Osoba o in WcfServiceClient.GetOsobyList().OsobyList)
        //    {
        //        if (WcfServiceClient.GetOsobaAge(o.ID) < age)
        //        {
        //            lstRecord.Add(o);
        //        }
        //    }
        //    return View(lstRecord);
        //}

    }
}