namespace LiczbyPierwszeProjekt.Ustawienia
{
    public struct UstawieniaLiczbyPierwsze
    {
        public int CzasObliczaniaLiczbyPierwszej { get; set; }
        public int CzasPrzerwyObliczaniaLiczbyPierwszej { get; set; }

        public UstawieniaLiczbyPierwsze (int czasObliczaniaLiczbyPierwszej, int czasPrzerwyObliczaniaLiczbyPierwszej)
        {
            CzasObliczaniaLiczbyPierwszej = czasObliczaniaLiczbyPierwszej;
            CzasPrzerwyObliczaniaLiczbyPierwszej = czasPrzerwyObliczaniaLiczbyPierwszej;
        }

        public override string ToString()
        {
            return $"Długość cyklu: {CzasObliczaniaLiczbyPierwszej}, przerwa pomiędzy cyklalmi {CzasPrzerwyObliczaniaLiczbyPierwszej}";
        }
    }
}
