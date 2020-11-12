using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WcfService.Models
{
    [Serializable()]
    [XmlRoot("ArrayOfOsoba")]
    public class OsobaCollection
    {
        [XmlElement("Osoba")]
        public List<Osoba> OsobyList { get; set; }
    }
}