namespace LiczbyPierwszeProjekt.LiczbyPierwsze
{
    public class LiczbyPierwszeObliczenia
    {
        public static bool CzyLiczbaPierwsza(int liczbaDoSprawdzenia)
        {
           
            if (liczbaDoSprawdzenia < 2)
                return false;

            
            int gornaGranica = liczbaDoSprawdzenia % 2 == 0 ? liczbaDoSprawdzenia / 2 : (liczbaDoSprawdzenia + 1) / 2;

            for(int i = 2; i<= gornaGranica; i++)
            {
                if(liczbaDoSprawdzenia% i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CzyLiczbaPierwsza(int liczbaDoSprawdzenia, out long czasWyliczen)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            bool czyWyznaczonoLiczbe = CzyLiczbaPierwsza(liczbaDoSprawdzenia);
            watch.Stop();
            czasWyliczen = watch.ElapsedMilliseconds;

            return czyWyznaczonoLiczbe;
        }
    }
}
