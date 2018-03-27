
using System.Xml;

namespace Tools.XML
{



    public abstract class SerializableXml
    {

        protected System.Xml.Serialization.XmlSerializerNamespaces _namespaces;

        [System.Xml.Serialization.XmlNamespaceDeclarations]
        public System.Xml.Serialization.XmlSerializerNamespaces Namespaces
        {
            get { return this._namespaces; }
        }

    }



    // http://www.switchonthecode.com/tutorials/csharp-tutorial-xml-serialization
    // http://www.codeproject.com/KB/XML/xml_serializationasp.aspx
    public class Serialization
	{




		public static void SerializeToXml<T>(T ThisTypeInstance, string strFileNameAndPath)
		{

            using (System.IO.FileStream fs = System.IO.File.OpenWrite(""))
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs))
                {
                    SerializeToXml<T>(ThisTypeInstance, sw);
                } // End Using sw
            } // End Using fs

		} // End Sub SerializeToXml


        public class Utf8StringWriter : System.IO.StringWriter
        {
            public override System.Text.Encoding Encoding { get { return System.Text.Encoding.UTF8; } }

            public Utf8StringWriter()
                : this(System.Globalization.CultureInfo.InvariantCulture)
            { }

            
            public Utf8StringWriter(System.IFormatProvider formatProvider)
                : base(formatProvider)
            { }

            public Utf8StringWriter(System.Text.StringBuilder sb)
                : this(sb, System.Globalization.CultureInfo.InvariantCulture)
            { }

            public Utf8StringWriter(System.Text.StringBuilder sb, System.IFormatProvider formatProvider)
                : base(sb, formatProvider)
            { }

        }


        public static string SerializeToXml<T>(T ThisTypeInstance)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			string strReturnValue = null;

            

            using (System.IO.TextWriter tw = new Utf8StringWriter(sb))
            {
                SerializeToXml<T>(ThisTypeInstance, tw);
            }

			strReturnValue = sb.ToString();
            sb.Clear();
			sb = null;


            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    using (System.IO.TextWriter tw = new System.IO.StreamWriter(ms, System.Text.Encoding.UTF8))
            //    {
            //        SerializeToXml<T>(ThisTypeInstance, tw);

            //        ms.Position = 0;

            //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ms, System.Text.Encoding.UTF8))
            //        {
            //            strReturnValue = sr.ReadToEnd();
            //        }
            //    }
            //}

            return strReturnValue;
		} // End Function SerializeToXml


        public class NoXsdXsiXmlWriter : System.Xml.XmlWriter
        {

            protected System.Xml.XmlWriter m_xtw;
            protected bool m_doWrite;


            public static System.Xml.XmlWriterSettings DefaultSettings()
            {
                System.Xml.XmlWriterSettings xs = new System.Xml.XmlWriterSettings();
                xs.OmitXmlDeclaration = false;
                xs.Encoding = System.Text.Encoding.UTF8;
                xs.Indent = true;
                xs.IndentChars = "    ";

                return xs;
            }

            public NoXsdXsiXmlWriter(System.IO.TextWriter tw, System.Xml.XmlWriterSettings settings)
            {
                this.m_doWrite = true;
                this.m_xtw = System.Xml.XmlWriter.Create(tw, settings);
            }


            public NoXsdXsiXmlWriter(System.IO.TextWriter tw)
                : this(tw, DefaultSettings())
            { }


            public override WriteState WriteState
            {
                get
                {
                    return this.m_xtw.WriteState;
                }

            }

            public override void Flush()
            {
                this.m_xtw.Flush();
            }

            public override string LookupPrefix(string ns)
            {
                return this.m_xtw.LookupPrefix(ns);
            }

            public override void WriteBase64(byte[] buffer, int index, int count)
            {
                this.m_xtw.WriteBase64(buffer, index, count);
            }

            public override void WriteCData(string text)
            {
                this.m_xtw.WriteCData(text);
            }

            public override void WriteCharEntity(char ch)
            {
                this.m_xtw.WriteCharEntity(ch);
            }

            public override void WriteChars(char[] buffer, int index, int count)
            {
                this.m_xtw.WriteChars(buffer, index, count);
            }

            public override void WriteComment(string text)
            {
                this.m_xtw.WriteComment(text);
            }

            public override void WriteDocType(string name, string pubid, string sysid, string subset)
            {
                this.m_xtw.WriteDocType(name, pubid, sysid, subset);
            }

            public override void WriteEndAttribute()
            {
                if (m_doWrite)
                {
                    this.m_xtw.WriteEndAttribute();
                }
                else
                    m_doWrite = true;
            }

            public override void WriteEndDocument()
            {
                this.m_xtw.WriteEndDocument();
            }

            public override void WriteEndElement()
            {
                this.m_xtw.WriteEndElement();
            }

            public override void WriteEntityRef(string name)
            {
                this.m_xtw.WriteEntityRef(name);
            }

            public override void WriteFullEndElement()
            {
                this.m_xtw.WriteFullEndElement();
            }

            public override void WriteProcessingInstruction(string name, string text)
            {
                this.m_xtw.WriteProcessingInstruction(name, text);
            }

            public override void WriteRaw(char[] buffer, int index, int count)
            {
                this.m_xtw.WriteChars(buffer, index, count);
            }

            public override void WriteRaw(string data)
            {
                this.m_xtw.WriteRaw(data);
            }

            public override void WriteStartAttribute(string prefix, string localName, string ns)
            {
                if (false && string.Equals(prefix, "xmlns") )
                {
                    if (string.Equals(localName, "xsi") || string.Equals(localName, "xsd"))
                    {
                        m_doWrite = false;
                        return;
                    }
                }
                
                this.m_xtw.WriteStartAttribute(prefix, localName, ns);
            }


            public override void WriteStartDocument()
            {
#if false
                STANDALONE = false;
                if (STANDALONE)
                    this.m_xtw.WriteStartDocument(STANDALONE);
                else
#endif
                this.m_xtw.WriteStartDocument();
            }

            public override void WriteStartDocument(bool standalone)
            {
                this.m_xtw.WriteStartDocument(standalone);
            }

            public override void WriteStartElement(string prefix, string localName, string ns)
            {
                this.m_xtw.WriteStartElement(prefix, localName, ns);
            }

            public override void WriteString(string text)
            {
                if (m_doWrite)
                {
                    this.m_xtw.WriteString(text);
                }
                
            }

            public override void WriteSurrogateCharEntity(char lowChar, char highChar)
            {
                this.m_xtw.WriteSurrogateCharEntity(lowChar, highChar);
            }

            public override void WriteWhitespace(string ws)
            {
                this.m_xtw.WriteWhitespace(ws);
            }
        } // End Class NoXsdXsiXmlWriter 


        public class NoNamespaceXmlWriter : System.Xml.XmlTextWriter
        {
            //Provide as many contructors as you need
            public NoNamespaceXmlWriter(System.IO.TextWriter output)
                : base(output)
            {
                
                Formatting = System.Xml.Formatting.Indented;
                base.Settings.OmitXmlDeclaration = true;
            }

            public override void WriteStartDocument() { }

            public override void WriteStartElement(string prefix, string localName, string ns)
            {
                base.WriteStartElement("", localName, "");
            }
        }

        //public interface lol
        //{
        //    System.Xml.Serialization.XmlSerializerNamespaces Namespaces { get;}
        //}


        private static class AccessorCache<T>
        {
            public static System.Func<T, System.Xml.Serialization.XmlSerializerNamespaces> s_GetNamespaces;


            public static System.Xml.Serialization.XmlSerializerNamespaces GetNamespaces(T instance)
            {
                if (s_GetNamespaces != null)
                {
                    return s_GetNamespaces(instance);
                } // End if (s_GetNamespaces != null) 

                // Works for SIMPLE xml only 
                System.Xml.Serialization.XmlSerializerNamespaces ns = 
                 new System.Xml.Serialization.XmlSerializerNamespaces();

                //Add an empty namespace and empty value
                ns.Add("", "");
                return ns;
            } // End Function GetNamespaces 


            static AccessorCache()
            {
                try
                {
                    s_GetNamespaces = null;
                    System.Linq.Expressions.ParameterExpression p = System.Linq.Expressions.Expression.Parameter(typeof(T));
                    System.Linq.Expressions.MemberExpression prop = System.Linq.Expressions.Expression.Property(p, "Namespaces");
                    System.Linq.Expressions.UnaryExpression con = System.Linq.Expressions.Expression.Convert(prop, typeof(System.Xml.Serialization.XmlSerializerNamespaces));
                    System.Linq.Expressions.LambdaExpression exp = System.Linq.Expressions.Expression.Lambda(con, p);
                    s_GetNamespaces = (System.Func<T, System.Xml.Serialization.XmlSerializerNamespaces>)exp.Compile();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine(ex.StackTrace);
                }

            } // End Static constructor 

        } // End Class AccessorCache<T> 


        public static void SerializeToXml<T>(T ThisTypeInstance, System.IO.TextWriter tw)
        {
            // System.Xml.Serialization.XmlSerializerNamespaces ns = 
            //  new System.Xml.Serialization.XmlSerializerNamespaces();

            //Add an empty namespace and empty value
            // ns.Add("", "");

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            // serializer.Serialize(tw, ThisTypeInstance, ns);
            // serializer.Serialize(tw, ThisTypeInstance);
            
            System.Xml.Serialization.XmlSerializerNamespaces xns = 
                AccessorCache<T>.GetNamespaces(ThisTypeInstance);
            
            using (NoXsdXsiXmlWriter noXsd = new NoXsdXsiXmlWriter(tw))
            {
                serializer.Serialize(noXsd, ThisTypeInstance, xns);
            } // End Using noXsd

            //using (var ix = new NoNamespaceXmlWriter(tw))
            //{
            //    serializer.Serialize(ix, ThisTypeInstance);
            //}

            serializer = null;
        } // End Sub SerializeToXml


		public static T DeserializeXmlFromFile<T>(string fileName)
		{
			T tReturnValue = default(T);

            using (System.IO.FileStream fstrm = new System.IO.FileStream(fileName, System.IO.FileMode.Open
                , System.IO.FileAccess.Read, System.IO.FileShare.Read)) 
            {
				tReturnValue = DeserializeXmlFromStream<T>(fstrm);
				fstrm.Close();
            } // End Using fstrm

			return tReturnValue;
		} // End Function DeserializeXmlFromFile


		public static T DeserializeXmlFromEmbeddedRessource<T>(string strRessourceName)
		{
            T tReturnValue = default(T);

			System.Reflection.Assembly ass = typeof(Serialization).Assembly;

			using (System.IO.Stream fstrm = ass.GetManifestResourceStream(strRessourceName)) 
            {
				tReturnValue = DeserializeXmlFromStream<T>(fstrm);
				fstrm.Close();
            } // End Using fstrm

			return tReturnValue;
		} // End Function DeserializeXmlFromEmbeddedRessource


        public static T DeserializeXmlFromString<T>(string s)
        {
            T tReturnValue = default(T);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
                {
                    writer.Write(s);
                    writer.Flush();
                    stream.Position = 0;

                    tReturnValue = DeserializeXmlFromStream<T>(stream);
                } // End Using writer

            } // End Using stream

            return tReturnValue;
        } // End Function DeserializeXmlFromString


        public static T DeserializeXmlFromUrl<T>(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {

                using (System.IO.Stream strm = client.OpenRead(url))
                {
                    return DeserializeXmlFromStream<T>(strm);
                }
            }
        }


        public static T DeserializeXmlFromUrl<T>(System.Uri uri)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                using (System.IO.Stream strm = client.OpenRead(uri))
                {
                    return DeserializeXmlFromStream<T>(strm);
                }
            }
        }


        public static void DeserializeXmlFromUrlAsync<T>(System.Uri uri, OnOpenReadCompleted_t onOpenReadCompleted)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                // http://stackoverflow.com/questions/25051674/how-to-wait-for-webclient-openreadasync-to-complete
                client.OpenReadCompleted += new System.Net.OpenReadCompletedEventHandler(onOpenReadCompleted);
                client.OpenReadAsync(uri, "userToken");


                // Lambda: 
                //client.OpenReadCompleted += (s, e) =>
                //{

                //    using (System.IO.Stream strm = e.Result)
                //    {

                //    }

                //};

                
                // Closure: 
                //client.OpenReadCompleted += delegate (object sender, System.Net.OpenReadCompletedEventArgs e)
                //{
                //    if (e.Cancelled == true)
                //    {
                //        // MessageBox.Show("Download has been canceled.");
                //        System.Console.WriteLine("Download has been canceled.");
                //        return;
                //    }
                //    else if (e.Error != null)
                //    {
                //        throw e.Error;
                //    }

                //    using (System.IO.Stream strm = e.Result)
                //    {

                //        strm.Close();
                //    }


                //    string userState = (string)e.UserState;
                //    System.Console.WriteLine("UserState: \"{0}\".", userState);
                //};
            }
        }


        public delegate void OnOpenReadCompleted_t(object sender, System.Net.OpenReadCompletedEventArgs e);

        /*
        public static void OnOpenReadCompleted(object sender, System.Net.OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // MessageBox.Show("Download has been canceled.");
                System.Console.WriteLine("Download has been canceled.");
                return;
            }
            else if (e.Error != null)
            {
                throw e.Error;
            }

            using (System.IO.Stream strm = e.Result)
            {

                strm.Close();
            }


            string userState = (string)e.UserState;
            System.Console.WriteLine("UserState: \"{0}\".", userState);
        }
        */


        public static T DeserializeXmlFromStream<T>(System.IO.Stream strm)
		{
			System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
			T ThisType = default(T);

			using (System.IO.StreamReader srEncodingReader = new System.IO.StreamReader(strm, System.Text.Encoding.UTF8)) 
            {
				ThisType = (T)deserializer.Deserialize(srEncodingReader);
				srEncodingReader.Close();
            } // End Using srEncodingReader

			deserializer = null;

			return ThisType;
		} // End Function DeserializeXmlFromStream


		#if notneeded

		public static void SerializeToXML<T>(System.Collections.Generic.List<T> ThisTypeInstance, string strConfigFileNameAndPath)
		{
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<T>));

			using (System.IO.TextWriter textWriter = new System.IO.StreamWriter(strConfigFileNameAndPath)) {
				serializer.Serialize(textWriter, ThisTypeInstance);
				textWriter.Close();
			}

			serializer = null;
		}
		// SerializeToXML


		public static System.Collections.Generic.List<T> DeserializeXmlFromFileAsList<T>(string strFileNameAndPath)
		{
			System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<T>));
			System.Collections.Generic.List<T> ThisTypeList = null;

			using (System.IO.StreamReader srEncodingReader = new System.IO.StreamReader(strFileNameAndPath, System.Text.Encoding.UTF8)) {
				ThisTypeList = (System.Collections.Generic.List<T>)deserializer.Deserialize(srEncodingReader);
				srEncodingReader.Close();
			}

			deserializer = null;

			return ThisTypeList;
		}
		// DeserializeXmlFromFileAsList

		#endif

	} // End Class Serialization


} // End Namespace COR.Tools.XML
