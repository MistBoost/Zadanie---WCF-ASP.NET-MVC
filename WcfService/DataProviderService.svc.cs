using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Xml.Serialization;
using WcfService.Models;

namespace WcfService
{
    public class DataProviderService : IDataProviderService
    {
        public OsobaCollection GetOsobyList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OsobaCollection));
            StreamReader reader = new StreamReader(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, @"App_Data\DataOsoby.xml"));
            OsobaCollection Osoby = (OsobaCollection)serializer.Deserialize(reader);
            reader.Close();
            return Osoby;
        }

        public void SaveOsobyToXml(List<Osoba> osoby)
        {
            var osobaCollection = new OsobaCollection() { OsobyList = osoby };
            XmlSerializer x = new XmlSerializer(typeof(OsobaCollection));
            TextWriter writer = new StreamWriter(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, @"App_Data\DataOsoby.xml"));
            x.Serialize(writer, osobaCollection);
            writer.Close();
        }

        public Osoba GetOsobaByID(int id)
        {
            foreach (Osoba o in GetOsobyList().OsobyList)
            {
                if (o.ID == id) return o;
            }
            throw new Exception("Osoba with this ID could not be found");
        }
        public void AddOsoba(string Meno, string Priezvisko, DateTime DatumNarodenia, string Ulica, int Cislo)
        {
            Osoba o = new Osoba();
            var osoby = GetOsobyList().OsobyList;
            o.Meno = Meno;
            o.Priezvisko = Priezvisko;
            o.DatumNarodenia = DatumNarodenia;
            o.ID = osoby.Count+1;
            o.Adresa = new Adresa() { Ulica = Ulica, Cislo = Cislo };
            osoby.Add(o);
            SaveOsobyToXml(osoby);
        }
        public int DeleteOsobaById(int Id)
        {
            var osoby = GetOsobyList().OsobyList;
            var resultList = osoby;
            try
            {
                var temp = new Osoba();
                foreach (Osoba o in osoby)
                {
                    if (o.ID == Id)
                    {
                        temp = o;
                        break;
                    }
                }
                resultList.Remove(temp);
                SaveOsobyToXml(resultList);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public void EditOsoba(int Id, string Meno, string Priezvisko, DateTime DatumNarodenia, string Ulica, int Cislo)
        {
            Osoba o = new Osoba();
            var osoby = GetOsobyList().OsobyList;
            o.ID = Id;
            o.Meno = Meno;
            o.Priezvisko = Priezvisko;
            o.DatumNarodenia = DatumNarodenia;
            o.Adresa = new Adresa() { Ulica = Ulica, Cislo = Cislo };
            for (int i = 0; i < osoby.Count; i++)
            {
                if (osoby[i].ID == Id)
                {
                    osoby[i] = o;
                    break;
                }
            }
            SaveOsobyToXml(osoby);
        }
        //level 1 functionality that is not used in level 3 assignment
        //public int GetOsobaAge(int id)
        //{
        //    Osoba o = GetOsobyList().OsobyList[id];
        //    var totalDays = (DateTime.Now.Date - o.DatumNarodenia).TotalDays;
        //    int totalYears = (int)Math.Truncate(totalDays / 365);
        //    return totalYears;
        //}

        public List<Osoba> GetOsobyByMeno(string c)
        {
            var result = new List<Osoba>();
            if (!(c == null || c == ""))
            {
                foreach (Osoba o in GetOsobyList().OsobyList)
                {
                    if (o.Meno == c) result.Add(o);
                }
            }
            return result;
        }

        public List<Osoba> GetOsobyByPriezvisko(string c, List<Osoba> s)
        {
            var result = new List<Osoba>();
            if (!(c == null || c == ""))
            {
                foreach (Osoba o in s)
                {
                    if (o.Priezvisko == c) result.Add(o);
                }
            }
            return result;
        }

        public List<Osoba> GetOsobyByDatumNarodenia(DateTime dt, List<Osoba> s)
        {
            var result = new List<Osoba>();
            if (!(dt == null))
            {
                foreach (Osoba o in s)
                {
                    if (o.DatumNarodenia.Date == dt.Date) result.Add(o);
                }
            }
            return result;
        }

        public List<Osoba> GetOsobyByUlica(string c, List<Osoba> s)
        {
            var result = new List<Osoba>();
            if (!(c == null || c == ""))
            {
                foreach (Osoba o in s)
                {
                    if (o.Adresa.Ulica == c) result.Add(o);
                }
            }
            return result;
        }

        public List<Osoba> GetOsobyByCislo(int i, List<Osoba> s)
        {
            var result = new List<Osoba>();
            if (!(i == 0))
            {
                foreach (Osoba o in s)
                {
                    if (o.Adresa.Cislo == i) result.Add(o);
                }
            }
            return result;
        }
    }
}
