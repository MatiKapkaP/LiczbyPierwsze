using System;
using System.IO;
using System.Text;
using System.Xml;

namespace LiczbyPierwszeProjekt.OperacjeNaPlikach
{
    public class PlikOperacje
    {
        public static string PobierzFolderDoZapisu()
        {
            string mojeDokumenty = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(mojeDokumenty, "LiczbyPierwsze");
        }

        public static string UtworzPlikXml(string folderDoZapisu,string nazwaPliku, string rootName)
        {
            Directory.CreateDirectory(folderDoZapisu);
            string pelnaSciezka = Path.Combine(folderDoZapisu, nazwaPliku);

            if(!File.Exists(pelnaSciezka))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter writer = XmlWriter.Create(pelnaSciezka, settings))
                {
                    writer.WriteStartElement(rootName);
                    writer.WriteEndElement();
                    writer.Flush();       
                }
            }
            return pelnaSciezka;
        }

        public static void DopiszDoPlikuXML(string path, Func<XmlDocument,XmlElement> ustawNowyElement)
        {
            XmlDocument doc = new XmlDocument();   
            doc.Load(path);

            XmlElement root = doc.DocumentElement;
            XmlElement nowyElement = ustawNowyElement(doc);
           
            root.AppendChild(nowyElement);

            using (XmlTextWriter w = new XmlTextWriter(path, Encoding.UTF8))
            {
                w.Formatting = Formatting.Indented;
                w.Indentation = 4;
                root.WriteTo(w);
            }
        }
    }
}
