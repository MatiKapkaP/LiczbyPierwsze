namespace LiczbyPierwszeProjekt
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.lblStatusPracy = new System.Windows.Forms.Label();
            this.btOdczytajLiczbe = new System.Windows.Forms.Button();
            this.lblInfoLiczbaPierwsza = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(50, 37);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(133, 48);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(216, 37);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(126, 48);
            this.btStop.TabIndex = 1;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // lblStatusPracy
            // 
            this.lblStatusPracy.AutoSize = true;
            this.lblStatusPracy.Location = new System.Drawing.Point(50, 363);
            this.lblStatusPracy.Name = "lblStatusPracy";
            this.lblStatusPracy.Size = new System.Drawing.Size(0, 17);
            this.lblStatusPracy.TabIndex = 2;
            // 
            // btOdczytajLiczbe
            // 
            this.btOdczytajLiczbe.Location = new System.Drawing.Point(53, 110);
            this.btOdczytajLiczbe.Name = "btOdczytajLiczbe";
            this.btOdczytajLiczbe.Size = new System.Drawing.Size(289, 40);
            this.btOdczytajLiczbe.TabIndex = 3;
            this.btOdczytajLiczbe.Text = "Wyświetl ostatnią liczbe pierwszą";
            this.btOdczytajLiczbe.UseVisualStyleBackColor = true;
            this.btOdczytajLiczbe.Click += new System.EventHandler(this.btOdczytajLiczbe_Click);
            // 
            // lblInfoLiczbaPierwsza
            // 
            this.lblInfoLiczbaPierwsza.AutoSize = true;
            this.lblInfoLiczbaPierwsza.Location = new System.Drawing.Point(58, 177);
            this.lblInfoLiczbaPierwsza.MaximumSize = new System.Drawing.Size(300, 0);
            this.lblInfoLiczbaPierwsza.Name = "lblInfoLiczbaPierwsza";
            this.lblInfoLiczbaPierwsza.Size = new System.Drawing.Size(20, 17);
            this.lblInfoLiczbaPierwsza.TabIndex = 4;
            this.lblInfoLiczbaPierwsza.Text = "...";
            this.lblInfoLiczbaPierwsza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.lblInfoLiczbaPierwsza);
            this.Controls.Add(this.btOdczytajLiczbe);
            this.Controls.Add(this.lblStatusPracy);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Name = "Form1";
            this.Text = "Wyznaczanie liczb pierwszych";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label lblStatusPracy;
        private System.Windows.Forms.Button btOdczytajLiczbe;
        private System.Windows.Forms.Label lblInfoLiczbaPierwsza;
    }
}

