
namespace BlueMine.Data.General.RSS
{


    public class CustomDate
        : System.Xml.Serialization.IXmlSerializable
    {

        protected System.DateTime m_value;

        private static System.Collections.Generic.Dictionary<string, string> s_timeZones =
            new System.Collections.Generic.Dictionary<string, string>() {
            {"ACDT", "+1030"},
            {"ACST", "+0930"},
            {"ADT", "-0300"},
            {"AEDT", "+1100"},
            {"AEST", "+1000"},
            {"AHDT", "-0900"},
            {"AHST", "-1000"},
            {"AST", "-0400"},
            {"AT", "-0200"},
            {"AWDT", "+0900"},
            {"AWST", "+0800"},
            {"BAT", "+0300"},
            {"BDST", "+0200"},
            {"BET", "-1100"},
            {"BST", "-0300"},
            {"BT", "+0300"},
            {"BZT2", "-0300"},
            {"CADT", "+1030"},
            {"CAST", "+0930"},
            {"CAT", "-1000"},
            {"CCT", "+0800"},
            {"CDT", "-0500"},
            {"CED", "+0200"},
            {"CET", "+0100"},
            {"CEST", "+0200"},
            {"CST", "-0600"},
            {"EAST", "+1000"},
            {"EDT", "-0400"},
            {"EED", "+0300"},
            {"EET", "+0200"},
            {"EEST", "+0300"},
            {"EST", "-0500"},
            {"FST", "+0200"},
            {"FWT", "+0100"},
            //{"GMT", "GMT"},
            {"GMT", "+0000"},
            {"GST", "+1000"},
            {"HDT", "-0900"},
            {"HST", "-1000"},
            {"IDLE", "+1200"},
            {"IDLW", "-1200"},
            {"IST", "+0530"},
            {"IT", "+0330"},
            {"JST", "+0900"},
            {"JT", "+0700"},
            {"MDT", "-0600"},
            {"MED", "+0200"},
            {"MET", "+0100"},
            {"MEST", "+0200"},
            {"MEWT", "+0100"},
            {"MST", "-0700"},
            {"MT", "+0800"},
            {"NDT", "-0230"},
            {"NFT", "-0330"},
            {"NT", "-1100"},
            {"NST", "+0630"},
            {"NZ", "+1100"},
            {"NZST", "+1200"},
            {"NZDT", "+1300"},
            {"NZT", "+1200"},
            {"PDT", "-0700"},
            {"PST", "-0800"},
            {"ROK", "+0900"},
            {"SAD", "+1000"},
            {"SAST", "+0900"},
            {"SAT", "+0900"},
            {"SDT", "+1000"},
            {"SST", "+0200"},
            {"SWT", "+0100"},
            {"USZ3", "+0400"},
            {"USZ4", "+0500"},
            {"USZ5", "+0600"},
            {"USZ6", "+0700"},
            {"UT", "-0000"},
            {"UTC", "-0000"},
            {"UZ10", "+1100"},
            {"WAT", "-0100"},
            {"WET", "-0000"},
            {"WST", "+0800"},
            {"YDT", "-0800"},
            {"YST", "-0900"},
            {"ZP4", "+0400"},
            {"ZP5", "+0500"},
            {"ZP6", "+0600"}
        };


        private static System.Collections.Generic.Dictionary<string, string> s_timeZoneOffsets =
            new System.Collections.Generic.Dictionary<string, string>() {
            {"ACDT", "+10:30"},
            {"ACST", "+09:30"},
            {"ADT", "-03:00"},
            {"AEDT", "+11:00"},
            {"AEST", "+10:00"},
            {"AHDT", "-09:00"},
            {"AHST", "-10:00"},
            {"AST", "-04:00"},
            {"AT", "-02:00"},
            {"AWDT", "+09:00"},
            {"AWST", "+08:00"},
            {"BAT", "+03:00"},
            {"BDST", "+02:00"},
            {"BET", "-11:00"},
            {"BST", "-03:00"},
            {"BT", "+03:00"},
            {"BZT2", "-03:00"},
            {"CADT", "+10:30"},
            {"CAST", "+09:30"},
            {"CAT", "-10:00"},
            {"CCT", "+08:00"},
            {"CDT", "-05:00"},
            {"CED", "+02:00"},
            {"CET", "+01:00"},
            {"CEST", "+02:00"},
            {"CST", "-06:00"},
            {"EAST", "+10:00"},
            {"EDT", "-04:00"},
            {"EED", "+03:00"},
            {"EET", "+02:00"},
            {"EEST", "+03:00"},
            {"EST", "-05:00"},
            {"FST", "+02:00"},
            {"FWT", "+01:00"},
            {"GMT", "+00:00"},
            {"GST", "+10:00"},
            {"HDT", "-09:00"},
            {"HST", "-10:00"},
            {"IDLE", "+12:00"},
            {"IDLW", "-12:00"},
            {"IST", "+05:30"},
            {"IT", "+03:30"},
            {"JST", "+09:00"},
            {"JT", "+07:00"},
            {"MDT", "-06:00"},
            {"MED", "+02:00"},
            {"MET", "+01:00"},
            {"MEST", "+02:00"},
            {"MEWT", "+01:00"},
            {"MST", "-07:00"},
            {"MT", "+08:00"},
            {"NDT", "-02:30"},
            {"NFT", "-03:30"},
            {"NT", "-11:00"},
            {"NST", "+06:30"},
            {"NZ", "+11:00"},
            {"NZST", "+12:00"},
            {"NZDT", "+13:00"},
            {"NZT", "+12:00"},
            {"PDT", "-07:00"},
            {"PST", "-08:00"},
            {"ROK", "+09:00"},
            {"SAD", "+10:00"},
            {"SAST", "+09:00"},
            {"SAT", "+09:00"},
            {"SDT", "+10:00"},
            {"SST", "+02:00"},
            {"SWT", "+01:00"},
            {"USZ3", "+04:00"},
            {"USZ4", "+05:00"},
            {"USZ5", "+06:00"},
            {"USZ6", "+07:00"},
            {"UT", "-00:00"},
            {"UTC", "-00:00"},
            {"UZ10", "+11:00"},
            {"WAT", "-01:00"},
            {"WET", "-00:00"},
            {"WST", "+08:00"},
            {"YDT", "-08:00"},
            {"YST", "-09:00"},
            {"ZP4", "+04:00"},
            {"ZP5", "+05:00"},
            {"ZP6", "+06:00"}
        };


        public CustomDate()
        { }

        public CustomDate(System.DateTime d)
        { this.m_value = d; }


        // User-defined conversion from CustomDate to System.DateTime
        public static implicit operator System.DateTime(CustomDate d)
        {
            return d.m_value;
        }


        //  User-defined conversion from System.DateTime to CustomDate
        public static implicit operator CustomDate(System.DateTime d)
        {
            return new CustomDate(d);
        }


        public override string ToString()
        {
            // https://msdn.microsoft.com/en-us/library/zdtaw1bw(v=vs.110).aspx
            return this.m_value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }


        private static void Test()
        {
            CustomDate dig = new CustomDate(System.DateTime.Now);
            //This call invokes the implicit "double" operator
            System.DateTime num = dig;
            //This call invokes the implicit "Digit" operator
            CustomDate dig2 = System.DateTime.Today;
            System.Console.WriteLine("num = {0} dig2 = {1}", num, dig2.ToString());
            System.Console.ReadLine();
        }


        // https://stackoverflow.com/questions/279534/proper-way-to-implement-ixmlserializable
        // https://www.codeproject.com/Articles/43237/How-to-Implement-IXmlSerializable-Correctly
        System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema()
        {
            return null;
        }


        void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            // string Name = reader.GetAttribute("Name");
            bool isEmptyElement = reader.IsEmptyElement;
            reader.ReadStartElement();

            if (!isEmptyElement)
            {
                //string es = reader.ReadElementString("pubDate");

                //this.m_value = System.DateTime.ParseExact(
                //    reader.ReadElementString("pubDate")
                //    , "ddd, dd MMM yyyy HH:mm z"
                //    , System.Globalization.CultureInfo.InvariantCulture
                //);

                string dateString = reader.ReadContentAsString();
                int timeZonePos = dateString.LastIndexOf(' ') + 1;
                string tz = dateString.Substring(timeZonePos);
                dateString = dateString.Substring(0, dateString.Length - tz.Length );
                dateString += s_timeZoneOffsets[tz];

                // https://msdn.microsoft.com/en-us/library/w2sa9yss(v=vs.110).aspx
                this.m_value = System.DateTime.ParseExact(
                      dateString
                    , "ddd, dd MMM yyyy HH:mm zzz"
                    , System.Globalization.CultureInfo.InvariantCulture
                );

                reader.ReadEndElement();
            } // End if (!isEmptyElement) 

        } // End Sub ReadXml 


        void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            // [XmlAttribute(AttributeName = "type")]
            // [XmlElement(ElementName = "name", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]

            // writer.WriteAttributeString("Name", "bla");

            if (this.m_value != System.DateTime.MinValue)
                //writer.WriteElementString("Birthday", this.m_value.ToString("r"));
                writer.WriteValue(this.m_value.ToString("r", System.Globalization.CultureInfo.InvariantCulture));
        } // End Sub WriteXml 


    } // End Class CustomDate 


} // End Namespace BlueMine.Data.General.RSS 
