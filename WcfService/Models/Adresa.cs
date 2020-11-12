using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WcfService.Models
{
    [Serializable()]
    [XmlRoot("Adresa")]
    public class Adresa
    {
        [XmlElement("Ulica")]
        public string Ulica { get; set; }
        [XmlElement("Cislo")]
        public int Cislo { get; set; }
    }
}