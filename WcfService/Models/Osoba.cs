using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WcfService.Models
{
    [Serializable()]
    public class Osoba
    {
        [XmlElement("ID")]
        public int ID { get; set; }
        [XmlElement("Meno")]
        public string Meno { get; set; }
        [XmlElement("Priezvisko")]
        public string Priezvisko { get; set; }
        [XmlElement("DatumNarodenia")]
        public DateTime DatumNarodenia { get; set; }
        [XmlElement("Adresa")]
        public Adresa Adresa { get; set; }
    }
}