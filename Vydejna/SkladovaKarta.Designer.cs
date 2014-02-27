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
            this.components = new System.ComponentModel.Container();
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
            this.contextMenuStripZmeny = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.opravaÚdajuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapujcenoNaKartuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOK = new System.Windows.Forms.Button();
            this.numericUpDownCenaKs = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUcetCenaKs = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUcetCena = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFyzStav = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.listBoxNazev = new System.Windows.Forms.ListBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).BeginInit();
            this.contextMenuStripZmeny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaKs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCenaKs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCena)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFyzStav)).BeginInit();
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
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(17, 461);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
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
            this.textBoxNazev.Size = new System.Drawing.Size(318, 20);
            this.textBoxNazev.TabIndex = 1;
            this.textBoxNazev.TextChanged += new System.EventHandler(this.textBoxNazev_TextChanged);
            // 
            // textBoxJK
            // 
            this.textBoxJK.Location = new System.Drawing.Point(103, 32);
            this.textBoxJK.MaxLength = 15;
            this.textBoxJK.Name = "textBoxJK";
            this.textBoxJK.ReadOnly = true;
            this.textBoxJK.Size = new System.Drawing.Size(318, 20);
            this.textBoxJK.TabIndex = 3;
            // 
            // textBoxCSN
            // 
            this.textBoxCSN.Location = new System.Drawing.Point(103, 58);
            this.textBoxCSN.MaxLength = 15;
            this.textBoxCSN.Name = "textBoxCSN";
            this.textBoxCSN.ReadOnly = true;
            this.textBoxCSN.Size = new System.Drawing.Size(161, 20);
            this.textBoxCSN.TabIndex = 4;
            // 
            // textBoxDIN
            // 
            this.textBoxDIN.Location = new System.Drawing.Point(103, 85);
            this.textBoxDIN.MaxLength = 15;
            this.textBoxDIN.Name = "textBoxDIN";
            this.textBoxDIN.ReadOnly = true;
            this.textBoxDIN.Size = new System.Drawing.Size(161, 20);
            this.textBoxDIN.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Rozměr :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Výrobce :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Cena/Ks :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Účet. cena/Ks :";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(621, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Účetní cena :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(56, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Účet :";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(250, 189);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Min. stav :";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(608, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Účetní stav(ks) :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 241);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Poznámka :";
            // 
            // textBoxRozmer
            // 
            this.textBoxRozmer.Location = new System.Drawing.Point(103, 109);
            this.textBoxRozmer.MaxLength = 20;
            this.textBoxRozmer.Name = "textBoxRozmer";
            this.textBoxRozmer.ReadOnly = true;
            this.textBoxRozmer.Size = new System.Drawing.Size(318, 20);
            this.textBoxRozmer.TabIndex = 6;
            // 
            // textBoxVyrobce
            // 
            this.textBoxVyrobce.Location = new System.Drawing.Point(103, 135);
            this.textBoxVyrobce.MaxLength = 40;
            this.textBoxVyrobce.Name = "textBoxVyrobce";
            this.textBoxVyrobce.ReadOnly = true;
            this.textBoxVyrobce.Size = new System.Drawing.Size(318, 20);
            this.textBoxVyrobce.TabIndex = 7;
            // 
            // textBoxUcet
            // 
            this.textBoxUcet.Location = new System.Drawing.Point(103, 187);
            this.textBoxUcet.MaxLength = 5;
            this.textBoxUcet.Name = "textBoxUcet";
            this.textBoxUcet.ReadOnly = true;
            this.textBoxUcet.Size = new System.Drawing.Size(100, 20);
            this.textBoxUcet.TabIndex = 11;
            // 
            // numericUpDownMinStav
            // 
            this.numericUpDownMinStav.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownMinStav.Location = new System.Drawing.Point(321, 187);
            this.numericUpDownMinStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMinStav.Name = "numericUpDownMinStav";
            this.numericUpDownMinStav.ReadOnly = true;
            this.numericUpDownMinStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownMinStav.TabIndex = 12;
            this.numericUpDownMinStav.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetStav
            // 
            this.numericUpDownUcetStav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownUcetStav.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownUcetStav.Location = new System.Drawing.Point(708, 188);
            this.numericUpDownUcetStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetStav.Name = "numericUpDownUcetStav";
            this.numericUpDownUcetStav.ReadOnly = true;
            this.numericUpDownUcetStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetStav.TabIndex = 13;
            this.numericUpDownUcetStav.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // textBoxPoznamka
            // 
            this.textBoxPoznamka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPoznamka.Location = new System.Drawing.Point(98, 238);
            this.textBoxPoznamka.MaxLength = 60;
            this.textBoxPoznamka.Name = "textBoxPoznamka";
            this.textBoxPoznamka.ReadOnly = true;
            this.textBoxPoznamka.Size = new System.Drawing.Size(710, 20);
            this.textBoxPoznamka.TabIndex = 15;
            // 
            // dataGridViewZmeny
            // 
            this.dataGridViewZmeny.AllowUserToAddRows = false;
            this.dataGridViewZmeny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewZmeny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZmeny.ContextMenuStrip = this.contextMenuStripZmeny;
            this.dataGridViewZmeny.Location = new System.Drawing.Point(98, 264);
            this.dataGridViewZmeny.MultiSelect = false;
            this.dataGridViewZmeny.Name = "dataGridViewZmeny";
            this.dataGridViewZmeny.ReadOnly = true;
            this.dataGridViewZmeny.Size = new System.Drawing.Size(710, 185);
            this.dataGridViewZmeny.TabIndex = 16;
            // 
            // contextMenuStripZmeny
            // 
            this.contextMenuStripZmeny.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opravaÚdajuToolStripMenuItem,
            this.zapujcenoNaKartuToolStripMenuItem});
            this.contextMenuStripZmeny.Name = "contextMenuStripZmeny";
            this.contextMenuStripZmeny.Size = new System.Drawing.Size(179, 70);
            this.contextMenuStripZmeny.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripZmeny_Opening);
            // 
            // opravaÚdajuToolStripMenuItem
            // 
            this.opravaÚdajuToolStripMenuItem.Name = "opravaÚdajuToolStripMenuItem";
            this.opravaÚdajuToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.opravaÚdajuToolStripMenuItem.Text = "Oprava údaju";
            this.opravaÚdajuToolStripMenuItem.Click += new System.EventHandler(this.ContextMenu_opravaUdaju);
            // 
            // zapujcenoNaKartuToolStripMenuItem
            // 
            this.zapujcenoNaKartuToolStripMenuItem.Name = "zapujcenoNaKartuToolStripMenuItem";
            this.zapujcenoNaKartuToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.zapujcenoNaKartuToolStripMenuItem.Text = "Zapujceno na kartu";
            this.zapujcenoNaKartuToolStripMenuItem.Click += new System.EventHandler(this.contextMenu_ZapujcenoNaKartu);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(730, 461);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(78, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "Budiž";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // numericUpDownCenaKs
            // 
            this.numericUpDownCenaKs.DecimalPlaces = 2;
            this.numericUpDownCenaKs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.numericUpDownCenaKs.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownCenaKs.Location = new System.Drawing.Point(103, 161);
            this.numericUpDownCenaKs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownCenaKs.Name = "numericUpDownCenaKs";
            this.numericUpDownCenaKs.ReadOnly = true;
            this.numericUpDownCenaKs.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownCenaKs.TabIndex = 8;
            this.numericUpDownCenaKs.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetCenaKs
            // 
            this.numericUpDownUcetCenaKs.DecimalPlaces = 2;
            this.numericUpDownUcetCenaKs.ForeColor = System.Drawing.Color.Red;
            this.numericUpDownUcetCenaKs.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownUcetCenaKs.Location = new System.Drawing.Point(321, 161);
            this.numericUpDownUcetCenaKs.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetCenaKs.Name = "numericUpDownUcetCenaKs";
            this.numericUpDownUcetCenaKs.ReadOnly = true;
            this.numericUpDownUcetCenaKs.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetCenaKs.TabIndex = 9;
            this.numericUpDownUcetCenaKs.ValueChanged += new System.EventHandler(this.numericUpDownUcetCenaKs_ValueChanged);
            this.numericUpDownUcetCenaKs.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownUcetCena
            // 
            this.numericUpDownUcetCena.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownUcetCena.DecimalPlaces = 2;
            this.numericUpDownUcetCena.ForeColor = System.Drawing.Color.Red;
            this.numericUpDownUcetCena.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownUcetCena.Location = new System.Drawing.Point(708, 161);
            this.numericUpDownUcetCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetCena.Name = "numericUpDownUcetCena";
            this.numericUpDownUcetCena.ReadOnly = true;
            this.numericUpDownUcetCena.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetCena.TabIndex = 10;
            this.numericUpDownUcetCena.Enter += new System.EventHandler(this.numericUpDownSK_Enter);
            // 
            // numericUpDownFyzStav
            // 
            this.numericUpDownFyzStav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFyzStav.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownFyzStav.Location = new System.Drawing.Point(708, 212);
            this.numericUpDownFyzStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownFyzStav.Name = "numericUpDownFyzStav";
            this.numericUpDownFyzStav.ReadOnly = true;
            this.numericUpDownFyzStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownFyzStav.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(606, 214);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Fyzický stav(ks) :";
            // 
            // listBoxNazev
            // 
            this.listBoxNazev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxNazev.Enabled = false;
            this.listBoxNazev.FormattingEnabled = true;
            this.listBoxNazev.Location = new System.Drawing.Point(436, 6);
            this.listBoxNazev.Name = "listBoxNazev";
            this.listBoxNazev.Size = new System.Drawing.Size(372, 147);
            this.listBoxNazev.TabIndex = 2;
            this.listBoxNazev.Click += new System.EventHandler(this.listBoxNazev_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(636, 461);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(78, 23);
            this.buttonCopy.TabIndex = 19;
            this.buttonCopy.Text = "Kopie";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // SkladovaKarta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(824, 500);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.listBoxNazev);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.numericUpDownFyzStav);
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
            this.MinimumSize = new System.Drawing.Size(718, 500);
            this.Name = "SkladovaKarta";
            this.Text = "SkladovaKarta";
            this.Activated += new System.EventHandler(this.SkladovaKarta_Activated);
            this.SizeChanged += new System.EventHandler(this.SkladovaKarta_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).EndInit();
            this.contextMenuStripZmeny.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaKs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCenaKs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetCena)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFyzStav)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownFyzStav;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox listBoxNazev;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripZmeny;
        private System.Windows.Forms.ToolStripMenuItem opravaÚdajuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapujcenoNaKartuToolStripMenuItem;
        private System.Windows.Forms.Button buttonCopy;
    }
}