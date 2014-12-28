namespace Vydejna
{
    partial class ZapujceneNaradiKarta
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridViewZmeny = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zapůjčeníNářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vraceníNářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.informaceONářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaceOZapůjčeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPrijmeni = new System.Windows.Forms.Label();
            this.labelJmeno = new System.Windows.Forms.Label();
            this.labelOsCislo = new System.Windows.Forms.Label();
            this.labelStredisko = new System.Windows.Forms.Label();
            this.labelOddeleni = new System.Windows.Forms.Label();
            this.labelPracoviste = new System.Windows.Forms.Label();
            this.labelCisZnamky = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.písmoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vybratPísmoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.písmoAplikaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTisk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Přijmení :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Jméno :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Os. čislo :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(352, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Středisko :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(364, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Provoz :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(347, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Pracoviště :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Číslo známky :";
            // 
            // dataGridViewZmeny
            // 
            this.dataGridViewZmeny.AllowUserToAddRows = false;
            this.dataGridViewZmeny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewZmeny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZmeny.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewZmeny.Location = new System.Drawing.Point(17, 150);
            this.dataGridViewZmeny.MultiSelect = false;
            this.dataGridViewZmeny.Name = "dataGridViewZmeny";
            this.dataGridViewZmeny.ReadOnly = true;
            this.dataGridViewZmeny.Size = new System.Drawing.Size(590, 190);
            this.dataGridViewZmeny.TabIndex = 29;
            this.dataGridViewZmeny.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewZmeny_ColumnDisplayIndexChanged);
            this.dataGridViewZmeny.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewZmeny_ColumnWidthChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapůjčeníNářadíToolStripMenuItem,
            this.vraceníNářadíToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.informaceONářadíToolStripMenuItem,
            this.informaceOZapůjčeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 142);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // zapůjčeníNářadíToolStripMenuItem
            // 
            this.zapůjčeníNářadíToolStripMenuItem.Name = "zapůjčeníNářadíToolStripMenuItem";
            this.zapůjčeníNářadíToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.zapůjčeníNářadíToolStripMenuItem.Text = "Zapůjčení nářadí";
            this.zapůjčeníNářadíToolStripMenuItem.Click += new System.EventHandler(this.zapůjčeníNářadíToolStripMenuItem_Click);
            // 
            // vraceníNářadíToolStripMenuItem
            // 
            this.vraceníNářadíToolStripMenuItem.Name = "vraceníNářadíToolStripMenuItem";
            this.vraceníNářadíToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.vraceníNářadíToolStripMenuItem.Text = "Vracení nářadí";
            this.vraceníNářadíToolStripMenuItem.Click += new System.EventHandler(this.vraceníNářadíToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "Poškození nářadí";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.poskozeniNaradiToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // informaceONářadíToolStripMenuItem
            // 
            this.informaceONářadíToolStripMenuItem.Name = "informaceONářadíToolStripMenuItem";
            this.informaceONářadíToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.informaceONářadíToolStripMenuItem.Text = "Informace o nářadí";
            this.informaceONářadíToolStripMenuItem.Click += new System.EventHandler(this.informaceONaradiToolStripMenuItem_Click);
            // 
            // informaceOZapůjčeToolStripMenuItem
            // 
            this.informaceOZapůjčeToolStripMenuItem.Name = "informaceOZapůjčeToolStripMenuItem";
            this.informaceOZapůjčeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.informaceOZapůjčeToolStripMenuItem.Text = "Informace o výpůjce";
            this.informaceOZapůjčeToolStripMenuItem.Click += new System.EventHandler(this.informaceOZapůjčeToolStripMenuItem_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(15, 356);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelPrijmeni
            // 
            this.labelPrijmeni.AutoSize = true;
            this.labelPrijmeni.Location = new System.Drawing.Point(80, 35);
            this.labelPrijmeni.Name = "labelPrijmeni";
            this.labelPrijmeni.Size = new System.Drawing.Size(43, 13);
            this.labelPrijmeni.TabIndex = 31;
            this.labelPrijmeni.Text = "Prijmeni";
            // 
            // labelJmeno
            // 
            this.labelJmeno.AutoSize = true;
            this.labelJmeno.Location = new System.Drawing.Point(80, 64);
            this.labelJmeno.Name = "labelJmeno";
            this.labelJmeno.Size = new System.Drawing.Size(38, 13);
            this.labelJmeno.TabIndex = 32;
            this.labelJmeno.Text = "Jmeno";
            // 
            // labelOsCislo
            // 
            this.labelOsCislo.AutoSize = true;
            this.labelOsCislo.Location = new System.Drawing.Point(80, 90);
            this.labelOsCislo.Name = "labelOsCislo";
            this.labelOsCislo.Size = new System.Drawing.Size(45, 13);
            this.labelOsCislo.TabIndex = 33;
            this.labelOsCislo.Text = "Os Cislo";
            // 
            // labelStredisko
            // 
            this.labelStredisko.AutoSize = true;
            this.labelStredisko.Location = new System.Drawing.Point(425, 35);
            this.labelStredisko.Name = "labelStredisko";
            this.labelStredisko.Size = new System.Drawing.Size(51, 13);
            this.labelStredisko.TabIndex = 34;
            this.labelStredisko.Text = "Stredisko";
            // 
            // labelOddeleni
            // 
            this.labelOddeleni.AutoSize = true;
            this.labelOddeleni.Location = new System.Drawing.Point(425, 64);
            this.labelOddeleni.Name = "labelOddeleni";
            this.labelOddeleni.Size = new System.Drawing.Size(40, 13);
            this.labelOddeleni.TabIndex = 35;
            this.labelOddeleni.Text = "Provoz";
            // 
            // labelPracoviste
            // 
            this.labelPracoviste.AutoSize = true;
            this.labelPracoviste.Location = new System.Drawing.Point(425, 90);
            this.labelPracoviste.Name = "labelPracoviste";
            this.labelPracoviste.Size = new System.Drawing.Size(57, 13);
            this.labelPracoviste.TabIndex = 36;
            this.labelPracoviste.Text = "Pracoviste";
            // 
            // labelCisZnamky
            // 
            this.labelCisZnamky.AutoSize = true;
            this.labelCisZnamky.Location = new System.Drawing.Point(425, 119);
            this.labelCisZnamky.Name = "labelCisZnamky";
            this.labelCisZnamky.Size = new System.Drawing.Size(60, 13);
            this.labelCisZnamky.TabIndex = 37;
            this.labelCisZnamky.Text = "Cis znamky";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.písmoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(619, 24);
            this.menuStrip1.TabIndex = 38;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // písmoToolStripMenuItem
            // 
            this.písmoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vybratPísmoToolStripMenuItem,
            this.písmoAplikaceToolStripMenuItem});
            this.písmoToolStripMenuItem.Name = "písmoToolStripMenuItem";
            this.písmoToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.písmoToolStripMenuItem.Text = "Písmo";
            // 
            // vybratPísmoToolStripMenuItem
            // 
            this.vybratPísmoToolStripMenuItem.Name = "vybratPísmoToolStripMenuItem";
            this.vybratPísmoToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.vybratPísmoToolStripMenuItem.Text = "Vybrat písmo";
            this.vybratPísmoToolStripMenuItem.Click += new System.EventHandler(this.vybratPísmoToolStripMenuItem_Click);
            // 
            // písmoAplikaceToolStripMenuItem
            // 
            this.písmoAplikaceToolStripMenuItem.Name = "písmoAplikaceToolStripMenuItem";
            this.písmoAplikaceToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.písmoAplikaceToolStripMenuItem.Text = "Písmo aplikace";
            this.písmoAplikaceToolStripMenuItem.Click += new System.EventHandler(this.písmoAplikaceToolStripMenuItem_Click);
            // 
            // buttonTisk
            // 
            this.buttonTisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTisk.Location = new System.Drawing.Point(113, 356);
            this.buttonTisk.Name = "buttonTisk";
            this.buttonTisk.Size = new System.Drawing.Size(75, 23);
            this.buttonTisk.TabIndex = 39;
            this.buttonTisk.Text = "Tisk";
            this.buttonTisk.UseVisualStyleBackColor = true;
            this.buttonTisk.Click += new System.EventHandler(this.buttonTisk_Click);
            // 
            // ZapujceneNaradiKarta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 393);
            this.Controls.Add(this.buttonTisk);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelCisZnamky);
            this.Controls.Add(this.labelPracoviste);
            this.Controls.Add(this.labelOddeleni);
            this.Controls.Add(this.labelStredisko);
            this.Controls.Add(this.labelOsCislo);
            this.Controls.Add(this.labelJmeno);
            this.Controls.Add(this.labelPrijmeni);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewZmeny);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ZapujceneNaradiKarta";
            this.Text = "Zapůjčené nářadí na osobu";
            this.Load += new System.EventHandler(this.ZapujceneNaradiKarta_Load);
            this.Shown += new System.EventHandler(this.ZapujceneNaradiKarta_Shown);
            this.LocationChanged += new System.EventHandler(this.ZapujceneNaradiKarta_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.ZapujceneNaradiKarta_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridViewZmeny;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPrijmeni;
        private System.Windows.Forms.Label labelJmeno;
        private System.Windows.Forms.Label labelOsCislo;
        private System.Windows.Forms.Label labelStredisko;
        private System.Windows.Forms.Label labelOddeleni;
        private System.Windows.Forms.Label labelPracoviste;
        private System.Windows.Forms.Label labelCisZnamky;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zapůjčeníNářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vraceníNářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaceONářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaceOZapůjčeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem písmoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vybratPísmoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem písmoAplikaceToolStripMenuItem;
        private System.Windows.Forms.Button buttonTisk;
    }
}