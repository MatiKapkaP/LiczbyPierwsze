using LiczbyPierwszeProjekt.Model;
using LiczbyPierwszeProjekt.OperacjeNaPlikach;
using LiczbyPierwszeProjekt.Ustawienia;
using LiczbyPierwszeProjekt.Wyliczenia;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Timers;
using System.Windows.Forms;

namespace LiczbyPierwszeProjekt
{
    public partial class Form1 : Form
    {
        private BackgroundWorker workerLiczbyPierwsze;
        private UstawieniaLiczbyPierwsze ustawieniaLiczbyPierwsze;
        private System.Timers.Timer taimerPracaLiczbyPierwsze;
        private System.Timers.Timer taimerPrzerwaLiczbyPierwsze;
        private StanLiczbyPierwsze stanLiczbyPierwsze;
        private LiczbyPierwszeDane m_LiczbyPierwszeDane;
        private string m_SciezkaDoPlikuXML;
        private bool m_PierwszeUruchomienie = true;

        public Form1()
        {
            InitializeComponent();
            ustawieniaLiczbyPierwsze = PobierzUstawieniaDlaWorkerLiczbyPierwsze();
            InitTaimerPracaLiczbyPierwsze();
            InitTaimerPrzerwaLiczbyPierwsze();
            InitWorkerLiczbyPierwsze();
            UstawStanLiczbyPierwsze(StanLiczbyPierwsze.OczekiwanieNaUruchomienie);
            m_LiczbyPierwszeDane = new LiczbyPierwszeDane();
        }

        private void InitTaimerPracaLiczbyPierwsze()
        {
            taimerPracaLiczbyPierwsze = new System.Timers.Timer(ustawieniaLiczbyPierwsze.CzasObliczaniaLiczbyPierwszej * 1000);
            taimerPracaLiczbyPierwsze.Elapsed += TaimerPracaLiczbyPierwsze_Elapsed;
            taimerPracaLiczbyPierwsze.AutoReset = false;
            taimerPracaLiczbyPierwsze.SynchronizingObject = this;
            //taimerPracaLiczbyPierwsze.Enabled = true;
        }

        private void TaimerPracaLiczbyPierwsze_Elapsed(object sender, ElapsedEventArgs e)
        {
            UstawStanLiczbyPierwsze(StanLiczbyPierwsze.Przerwa);
        }

        private void InitTaimerPrzerwaLiczbyPierwsze()
        {
            taimerPrzerwaLiczbyPierwsze = new System.Timers.Timer(ustawieniaLiczbyPierwsze.CzasPrzerwyObliczaniaLiczbyPierwszej * 1000);
            taimerPrzerwaLiczbyPierwsze.Elapsed += TaimerPrzerwaLiczbyPierwsze_Elapsed;
            taimerPrzerwaLiczbyPierwsze.AutoReset = false;
            taimerPrzerwaLiczbyPierwsze.SynchronizingObject = this;
        }

        private void TaimerPrzerwaLiczbyPierwsze_Elapsed(object sender, ElapsedEventArgs e)
        {
            UstawStanLiczbyPierwsze(StanLiczbyPierwsze.Praca);
        }

        private void UstawStanLiczbyPierwsze(StanLiczbyPierwsze nowyStan)
        {
            string msg = string.Empty;
            switch (nowyStan)
            {
                case StanLiczbyPierwsze.OczekiwanieNaUruchomienie:
                    taimerPracaLiczbyPierwsze.Stop();
                    taimerPrzerwaLiczbyPierwsze.Stop();
                    ZatrzymajWorkerLiczbyPierwsze();
                    msg = "Oczekiwanie na uruchomienie";
                    break;
                case StanLiczbyPierwsze.Praca:
                    taimerPracaLiczbyPierwsze.Start();
                    RozpoczniPraceWorkerLiczbyPierwsze();
                    msg = "Praca";
                    break;
                case StanLiczbyPierwsze.Przerwa:
                    taimerPracaLiczbyPierwsze.Stop();
                    ZatrzymajWorkerLiczbyPierwsze();
                    taimerPrzerwaLiczbyPierwsze.Start();
                    msg = "Przerwa";
                    break;
            }
            stanLiczbyPierwsze = nowyStan;
            lblStatusPracy.Text = msg;
        }

        private void InitWorkerLiczbyPierwsze()
        {
            DisposeWorkerLiczbyPierwsze();
            workerLiczbyPierwsze = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
            workerLiczbyPierwsze.DoWork += workerLiczbyPierwsze_DoWork;
            workerLiczbyPierwsze.RunWorkerCompleted += workerLiczbyPierwsze_RunWorkerCompleted;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                if(m_PierwszeUruchomienie)
                {
                    UtworzPlikXML();
                    m_PierwszeUruchomienie = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Tworzenie pliku xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UstawStanLiczbyPierwsze(StanLiczbyPierwsze.Praca);
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            UstawStanLiczbyPierwsze(StanLiczbyPierwsze.OczekiwanieNaUruchomienie);
        }

        private void RozpoczniPraceWorkerLiczbyPierwsze()
        {
            if (workerLiczbyPierwsze != null && !workerLiczbyPierwsze.IsBusy)
            {
                workerLiczbyPierwsze.RunWorkerAsync();
            }
            else
            {
                WyswietlBlad("Worker jest uruchomiony");
            }
        }

        private void ZatrzymajWorkerLiczbyPierwsze()
        {
            if (workerLiczbyPierwsze.IsBusy)
            {
                workerLiczbyPierwsze.CancelAsync();
            }
        }
        private System.Diagnostics.Stopwatch m_Watch = new System.Diagnostics.Stopwatch();
        private void workerLiczbyPierwsze_DoWork(object sender, DoWorkEventArgs e)
        {

            m_Watch.Start();

            m_LiczbyPierwszeDane.ZwiekszNrCyklu();
            int liczbaDoSprawdzenia = m_LiczbyPierwszeDane.PoprzedniaLiczbaPierwsza.LiczbaPierwsza + 1;

            while (true)
            {
                if (m_LiczbyPierwszeDane.WyznaczLiczbePierwsz(liczbaDoSprawdzenia))
                {
                    m_Watch.Stop();
                    m_LiczbyPierwszeDane.UstawCzasCyklu(m_Watch.ElapsedMilliseconds);
                    m_Watch.Start();
                }
                liczbaDoSprawdzenia++;

                if (workerLiczbyPierwsze.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }         
            }
        }

        private void workerLiczbyPierwsze_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_Watch.Reset();
            if (e.Error != null)
            {
                WyswietlBlad(e.Error.Message);
                UstawStanLiczbyPierwsze(StanLiczbyPierwsze.OczekiwanieNaUruchomienie);
                return;
            }

            if (m_LiczbyPierwszeDane.CzyUstawionaLiczbaPierwsza())
            {
                m_LiczbyPierwszeDane.ZapiszAktualnaLiczbePierwsza(m_SciezkaDoPlikuXML);
            }
            else
            {
                UstawStanLiczbyPierwsze(StanLiczbyPierwsze.OczekiwanieNaUruchomienie);
                WyswietlBlad("Nie znaleziono liczby pierwszej");
                return;
            }
        }

        private void UtworzPlikXML()
        {
            string nazwaPliku = string.Format("LiczbyPierwsze{0}_{1}_{2}.xml", DateTime.Now.ToString("yyyy_MM_dd"), DateTime.Now.Minute, DateTime.Now.Millisecond);
            string folderDoZapisu = PlikOperacje.PobierzFolderDoZapisu();
            m_SciezkaDoPlikuXML = PlikOperacje.UtworzPlikXml(folderDoZapisu, nazwaPliku, "LiczbyPierwsze");
        }

        private void WyswietlBlad(string message)
        {
            MessageBox.Show(message, "Wiadomosc", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private UstawieniaLiczbyPierwsze PobierzUstawieniaDlaWorkerLiczbyPierwsze()
        {
            UstawieniaLiczbyPierwsze ustawienia = new UstawieniaLiczbyPierwsze();
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            ustawienia.CzasObliczaniaLiczbyPierwszej = Convert.ToInt32(appSettings["CzasObliczaniaLiczbyPierwszej"]);
            ustawienia.CzasPrzerwyObliczaniaLiczbyPierwszej = Convert.ToInt32(appSettings["CzasPrzerwyObliczaniaLiczbyPierwszej"]);
            return ustawienia;
        }

        private void DisposeWorkerLiczbyPierwsze()
        {
            if (workerLiczbyPierwsze != null)
            {
                workerLiczbyPierwsze.Dispose();
            }
        }

        private void btOdczytajLiczbe_Click(object sender, EventArgs e)
        {
            if(m_LiczbyPierwszeDane != null && m_LiczbyPierwszeDane.PoprzedniaLiczbaPierwsza.LiczbaPierwsza > 0)
            {
                lblInfoLiczbaPierwsza.Text = m_LiczbyPierwszeDane.PoprzedniaLiczbaPierwsza.ToString();
            }
            else
            {
                lblInfoLiczbaPierwsza.Text = "Jeszcze nie wyznaczono liczby pierwszej";
            }
        }
    }
}
