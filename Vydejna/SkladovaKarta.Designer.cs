namespace Vydejna
{
    partial class SkladovaKarta
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNazev = new System.Windows.Forms.TextBox();
            this.textBoxJK = new System.Windows.Forms.TextBox();
            this.textBoxCSN = new System.Windows.Forms.TextBox();
            this.textBoxDIN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxRozmer = new System.Windows.Forms.TextBox();
            this.textBoxVyrobce = new System.Windows.Forms.TextBox();
            this.textBoxUcet = new System.Windows.Forms.TextBox();
            this.numericUpDownMinStav = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUcetStav = new System.Windows.Forms.NumericUpDown();
            this.textBoxPoznamka = new System.Windows.Forms.TextBox();
            this.dataGridViewZmeny = new System.Windows.Forms.DataGridView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.numericUpDownCenaKs = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUcetCenaKs = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUcetCena = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaKs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCenaKs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCena)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název nářadi :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Označení JK :";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(17, 403);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Norma ČSN :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Norma DIN :";
            // 
            // textBoxNazev
            // 
            this.textBoxNazev.Location = new System.Drawing.Point(103, 6);
            this.textBoxNazev.MaxLength = 60;
            this.textBoxNazev.Name = "textBoxNazev";
            this.textBoxNazev.ReadOnly = true;
            this.textBoxNazev.Size = new System.Drawing.Size(587, 20);
            this.textBoxNazev.TabIndex = 1;
            // 
            // textBoxJK
            // 
            this.textBoxJK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxJK.Location = new System.Drawing.Point(103, 32);
            this.textBoxJK.MaxLength = 15;
            this.textBoxJK.Name = "textBoxJK";
            this.textBoxJK.ReadOnly = true;
            this.textBoxJK.Size = new System.Drawing.Size(172, 20);
            this.textBoxJK.TabIndex = 2;
            // 
            // textBoxCSN
            // 
            this.textBoxCSN.Location = new System.Drawing.Point(103, 58);
            this.textBoxCSN.MaxLength = 15;
            this.textBoxCSN.Name = "textBoxCSN";
            this.textBoxCSN.ReadOnly = true;
            this.textBoxCSN.Size = new System.Drawing.Size(172, 20);
            this.textBoxCSN.TabIndex = 3;
            // 
            // textBoxDIN
            // 
            this.textBoxDIN.Location = new System.Drawing.Point(103, 85);
            this.textBoxDIN.MaxLength = 15;
            this.textBoxDIN.Name = "textBoxDIN";
            this.textBoxDIN.ReadOnly = true;
            this.textBoxDIN.Size = new System.Drawing.Size(172, 20);
            this.textBoxDIN.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Rozměr :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Výrobce :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Cena/Ks :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(232, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Účet. cena/Ks :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(502, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Účetní cena :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(56, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Účet :";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(259, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Min. stav :";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(489, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Účetní stav(ks) :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 191);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Poznámka :";
            // 
            // textBoxRozmer
            // 
            this.textBoxRozmer.Location = new System.Drawing.Point(358, 85);
            this.textBoxRozmer.MaxLength = 20;
            this.textBoxRozmer.Name = "textBoxRozmer";
            this.textBoxRozmer.ReadOnly = true;
            this.textBoxRozmer.Size = new System.Drawing.Size(332, 20);
            this.textBoxRozmer.TabIndex = 5;
            // 
            // textBoxVyrobce
            // 
            this.textBoxVyrobce.Location = new System.Drawing.Point(103, 111);
            this.textBoxVyrobce.MaxLength = 40;
            this.textBoxVyrobce.Name = "textBoxVyrobce";
            this.textBoxVyrobce.ReadOnly = true;
            this.textBoxVyrobce.Size = new System.Drawing.Size(587, 20);
            this.textBoxVyrobce.TabIndex = 6;
            // 
            // textBoxUcet
            // 
            this.textBoxUcet.Location = new System.Drawing.Point(103, 163);
            this.textBoxUcet.MaxLength = 5;
            this.textBoxUcet.Name = "textBoxUcet";
            this.textBoxUcet.ReadOnly = true;
            this.textBoxUcet.Size = new System.Drawing.Size(100, 20);
            this.textBoxUcet.TabIndex = 10;
            // 
            // numericUpDownMinStav
            // 
            this.numericUpDownMinStav.Enabled = false;
            this.numericUpDownMinStav.Location = new System.Drawing.Point(321, 164);
            this.numericUpDownMinStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMinStav.Name = "numericUpDownMinStav";
            this.numericUpDownMinStav.ReadOnly = true;
            this.numericUpDownMinStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownMinStav.TabIndex = 11;
            this.numericUpDownMinStav.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetStav
            // 
            this.numericUpDownUcetStav.Enabled = false;
            this.numericUpDownUcetStav.Location = new System.Drawing.Point(590, 164);
            this.numericUpDownUcetStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetStav.Name = "numericUpDownUcetStav";
            this.numericUpDownUcetStav.ReadOnly = true;
            this.numericUpDownUcetStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetStav.TabIndex = 12;
            this.numericUpDownUcetStav.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // textBoxPoznamka
            // 
            this.textBoxPoznamka.Location = new System.Drawing.Point(103, 188);
            this.textBoxPoznamka.MaxLength = 60;
            this.textBoxPoznamka.Name = "textBoxPoznamka";
            this.textBoxPoznamka.ReadOnly = true;
            this.textBoxPoznamka.Size = new System.Drawing.Size(587, 20);
            this.textBoxPoznamka.TabIndex = 13;
            // 
            // dataGridViewZmeny
            // 
            this.dataGridViewZmeny.AllowUserToAddRows = false;
            this.dataGridViewZmeny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZmeny.Location = new System.Drawing.Point(103, 224);
            this.dataGridViewZmeny.MultiSelect = false;
            this.dataGridViewZmeny.Name = "dataGridViewZmeny";
            this.dataGridViewZmeny.ReadOnly = true;
            this.dataGridViewZmeny.Size = new System.Drawing.Size(587, 173);
            this.dataGridViewZmeny.TabIndex = 27;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(615, 403);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Text = "Budiž";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // numericUpDownCenaKs
            // 
            this.numericUpDownCenaKs.DecimalPlaces = 2;
            this.numericUpDownCenaKs.Enabled = false;
            this.numericUpDownCenaKs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.numericUpDownCenaKs.Location = new System.Drawing.Point(103, 138);
            this.numericUpDownCenaKs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownCenaKs.Name = "numericUpDownCenaKs";
            this.numericUpDownCenaKs.ReadOnly = true;
            this.numericUpDownCenaKs.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownCenaKs.TabIndex = 7;
            this.numericUpDownCenaKs.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetCenaKs
            // 
            this.numericUpDownUcetCenaKs.DecimalPlaces = 2;
            this.numericUpDownUcetCenaKs.Enabled = false;
            this.numericUpDownUcetCenaKs.Location = new System.Drawing.Point(321, 140);
            this.numericUpDownUcetCenaKs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetCenaKs.Name = "numericUpDownUcetCenaKs";
            this.numericUpDownUcetCenaKs.ReadOnly = true;
            this.numericUpDownUcetCenaKs.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetCenaKs.TabIndex = 8;
            this.numericUpDownUcetCenaKs.ValueChanged += new System.EventHandler(this.numericUpDownUcetCenaKs_ValueChanged);
            this.numericUpDownUcetCenaKs.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetCena
            // 
            this.numericUpDownUcetCena.DecimalPlaces = 2;
            this.numericUpDownUcetCena.Enabled = false;
            this.numericUpDownUcetCena.Location = new System.Drawing.Point(590, 138);
            this.numericUpDownUcetCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetCena.Name = "numericUpDownUcetCena";
            this.numericUpDownUcetCena.ReadOnly = true;
            this.numericUpDownUcetCena.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetCena.TabIndex = 9;
            this.numericUpDownUcetCena.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // SkladovaKarta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 438);
            this.Controls.Add(this.numericUpDownUcetCena);
            this.Controls.Add(this.numericUpDownUcetCenaKs);
            this.Controls.Add(this.numericUpDownCenaKs);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dataGridViewZmeny);
            this.Controls.Add(this.textBoxPoznamka);
            this.Controls.Add(this.numericUpDownUcetStav);
            this.Controls.Add(this.numericUpDownMinStav);
            this.Controls.Add(this.textBoxUcet);
            this.Controls.Add(this.textBoxVyrobce);
            this.Controls.Add(this.textBoxRozmer);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDIN);
            this.Controls.Add(this.textBoxCSN);
            this.Controls.Add(this.textBoxJK);
            this.Controls.Add(this.textBoxNazev);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SkladovaKarta";
            this.Text = "SkladovaKarta";
            this.Activated += new System.EventHandler(this.SkladovaKarta_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaKs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCenaKs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCena)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNazev;
        private System.Windows.Forms.TextBox textBoxJK;
        private System.Windows.Forms.TextBox textBoxCSN;
        private System.Windows.Forms.TextBox textBoxDIN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxRozmer;
        private System.Windows.Forms.TextBox textBoxVyrobce;
        private System.Windows.Forms.TextBox textBoxUcet;
        private System.Windows.Forms.NumericUpDown numericUpDownMinStav;
        private System.Windows.Forms.NumericUpDown numericUpDownUcetStav;
        private System.Windows.Forms.TextBox textBoxPoznamka;
        private System.Windows.Forms.DataGridView dataGridViewZmeny;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.NumericUpDown numericUpDownCenaKs;
        private System.Windows.Forms.NumericUpDown numericUpDownUcetCenaKs;
        private System.Windows.Forms.NumericUpDown numericUpDownUcetCena;
    }
}