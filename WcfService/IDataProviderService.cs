using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Models;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDataProviderService
    {
        
       
        [OperationContract]
        OsobaCollection GetOsobyList();
        [OperationContract]
        void SaveOsobyToXml(List<Osoba> osoby);
        [OperationContract]
        Osoba GetOsobaByID(int id);
        [OperationContract]
        void AddOsoba(string Meno, string Priezvisko, DateTime DatumNarodenia, string Ulica, int Cislo);
        [OperationContract]
        int DeleteOsobaById(int Id);
        [OperationContract]
        void EditOsoba(int Id, string Meno, string Priezvisko, DateTime DatumNarodenia, string Ulica, int Cislo);
        //[OperationContract]
        //int GetOsobaAge(int id);
        [OperationContract]
        List<Osoba> GetOsobyByMeno(string c);
        [OperationContract]
        List<Osoba> GetOsobyByPriezvisko(string c, List<Osoba> s);
        [OperationContract]
        List<Osoba> GetOsobyByDatumNarodenia(DateTime dt, List<Osoba> s);
        [OperationContract]
        List<Osoba> GetOsobyByUlica(string c, List<Osoba> s);
        [OperationContract]
        List<Osoba> GetOsobyByCislo(int i, List<Osoba> s);
    }
}
