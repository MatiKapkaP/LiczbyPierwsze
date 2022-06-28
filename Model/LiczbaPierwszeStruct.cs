namespace LiczbyPierwszeProjekt.Model
{
    public struct LiczbaPierwszeStruct
    {
        public int LiczbaPierwsza;
        public int NrCyklu;
        public double CzasTrwaniaCyklu;
        public double CzasWyznaczania;

        public LiczbaPierwszeStruct(int liczbaPierwsza)
        {
            LiczbaPierwsza = liczbaPierwsza;
            NrCyklu = 0;
            CzasTrwaniaCyklu = 0.0;
            CzasWyznaczania = 0.0;
            
        }

        public override string ToString()
        {
            return string.Format("Nr cyklu: {0}, Liczba pierwsza: {1}, Czas trwania cyklu: {2}, Czas wyznaczania {3}", 
                NrCyklu, LiczbaPierwsza, CzasTrwaniaCyklu, CzasWyznaczania);
        }
    }
}

