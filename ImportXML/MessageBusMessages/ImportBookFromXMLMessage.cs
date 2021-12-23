using System;
using System.Drawing;

namespace De.HsFlensburg.ClientApp064.ImportXML.MessageBusMessages
{
    public class ImportBookFromXMLMessage
    {
        // These properties will hold the xml-data
        public String Title {get; set; }
        public String Author { get; set; }
        public String Style { get; set; }
        public String ISBN { get; set; }
        public string Publisher { get; set; }
        public String Language { get; set; }
        public Decimal Price { get; set; }
        public int Edition { get; set; }
        public int Year { get; set; }
        public String Genre { get; set; }
        public String Location { get; set; }
        public Image Cover { get; set; }
    }
}
