using LiczbyPierwszeProjekt.LiczbyPierwsze;
using LiczbyPierwszeProjekt.OperacjeNaPlikach;
using System.Xml;

namespace LiczbyPierwszeProjekt.Model
{
    public class LiczbyPierwszeDane
    {
        public int NrCyklu { get; set; }
        public LiczbaPierwszeStruct AktualnaLiczbaPierwsza;
        public LiczbaPierwszeStruct PoprzedniaLiczbaPierwsza;

        public LiczbyPierwszeDane()
        {
            NrCyklu = 0;
            AktualnaLiczbaPierwsza = new LiczbaPierwszeStruct(0);
            PoprzedniaLiczbaPierwsza = new LiczbaPierwszeStruct(0);
        }

        public bool WyznaczLiczbePierwsz(int liczbaDoSprawdzenia)
        {
            bool czyWyznaczonoLiczbePierwsza = false;
            long czasWyznaczaniaLiczbyPierwszej = 0;
            if (LiczbyPierwszeObliczenia.CzyLiczbaPierwsza(liczbaDoSprawdzenia, out czasWyznaczaniaLiczbyPierwszej))
            {
                AktualnaLiczbaPierwsza.LiczbaPierwsza = liczbaDoSprawdzenia;
                AktualnaLiczbaPierwsza.CzasWyznaczania = czasWyznaczaniaLiczbyPierwszej;
                AktualnaLiczbaPierwsza.NrCyklu = NrCyklu;
                czyWyznaczonoLiczbePierwsza = true;
            }
            return czyWyznaczonoLiczbePierwsza;
        }

        public void UstawCzasCyklu(long czasTrwaniaCyklu)
        {
            AktualnaLiczbaPierwsza.CzasTrwaniaCyklu = czasTrwaniaCyklu;
        }

        public void ZapiszAktualnaLiczbePierwsza(string m_SciezkaDoPlikuXML)
        {
            ZapiszDoPlikuAktualnaLiczbe(m_SciezkaDoPlikuXML);
            PoprzedniaLiczbaPierwsza = AktualnaLiczbaPierwsza;
            AktualnaLiczbaPierwsza.LiczbaPierwsza = 0;
        }

        private void ZapiszDoPlikuAktualnaLiczbe(string m_SciezkaDoPlikuXML)
        {
            PlikOperacje.DopiszDoPlikuXML(m_SciezkaDoPlikuXML, KonwertujLiczbePierwszaNaXML);
        }

        public XmlElement KonwertujLiczbePierwszaNaXML(XmlDocument doc)
        {
            XmlElement cykl = doc.CreateElement("Cykl");

            XmlElement nrCyklu = doc.CreateElement("NrCyklu");
            nrCyklu.InnerText = AktualnaLiczbaPierwsza.NrCyklu.ToString();
            cykl.AppendChild(nrCyklu);

            XmlElement liczbaPierwsza = doc.CreateElement("LiczbaPierwsza");
            liczbaPierwsza.InnerText = AktualnaLiczbaPierwsza.LiczbaPierwsza.ToString();
            cykl.AppendChild(liczbaPierwsza);

            XmlElement czasTrwaniaCyklu = doc.CreateElement("CzasTrwaniaCyklu");
            czasTrwaniaCyklu.InnerText = AktualnaLiczbaPierwsza.CzasTrwaniaCyklu.ToString();
            cykl.AppendChild(czasTrwaniaCyklu);

            XmlElement czasWyznaczania = doc.CreateElement("CzasWyznaczaniaLiczby");
            czasWyznaczania.InnerText = AktualnaLiczbaPierwsza.CzasWyznaczania.ToString();
            cykl.AppendChild(czasWyznaczania);

            return cykl;
        }

        public bool CzyUstawionaLiczbaPierwsza()
        {
            return AktualnaLiczbaPierwsza.LiczbaPierwsza > 0;
        }

        public void ZwiekszNrCyklu()
        {
            NrCyklu++;
        }
    }
}
