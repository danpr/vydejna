namespace Vydejna
{
    partial class Vydejna
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
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.pracovníciProvozuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapujceniNářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.údržbaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.nahráníDatZDBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvoreniTabulekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvoreniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smazániToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vymazáníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.konecProgramuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.přidatPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opravitPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smazatPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.příjemMaterialuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poškozeníNářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapůjčeníNářadíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prohledáváníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelView = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.labelStateConnection = new System.Windows.Forms.Label();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarMain
            // 
            this.progressBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMain.Location = new System.Drawing.Point(0, 426);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(787, 17);
            this.progressBarMain.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem1,
            this.údržbaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pracovníciProvozuToolStripMenuItem,
            this.zapujceniNářadíToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(89, 20);
            this.toolStripMenuItem4.Text = "Pohyb nářadí";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // pracovníciProvozuToolStripMenuItem
            // 
            this.pracovníciProvozuToolStripMenuItem.Name = "pracovníciProvozuToolStripMenuItem";
            this.pracovníciProvozuToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.pracovníciProvozuToolStripMenuItem.Text = "Pracovníci provozu";
            this.pracovníciProvozuToolStripMenuItem.Click += new System.EventHandler(this.pracovníciProvozuToolStripMenuItem_Click);
            // 
            // zapujceniNářadíToolStripMenuItem
            // 
            this.zapujceniNářadíToolStripMenuItem.Name = "zapujceniNářadíToolStripMenuItem";
            this.zapujceniNářadíToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.zapujceniNářadíToolStripMenuItem.Text = "Zapůjčení / Vrácení nářadí";
            this.zapujceniNářadíToolStripMenuItem.Click += new System.EventHandler(this.zapujceniNaradi_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem8,
            this.toolStripSeparator1,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 20);
            this.toolStripMenuItem1.Text = "Výdejna nářadí";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem2.Text = "Výdejna nářadi - přehled";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem8.Text = "Založení nové skladové karty";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.NovaSklKarta);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem6.Text = "Poškozené nářadí - přehled";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem5.Text = "Vrácené nářadí přehled";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem3.Text = "Archív zrušených karet";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // údržbaToolStripMenuItem
            // 
            this.údržbaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.toolStripMenuItem11,
            this.toolStripMenuItem7,
            this.nahráníDatZDBaseToolStripMenuItem,
            this.vytvoreniTabulekToolStripMenuItem,
            this.toolStripMenuItem10,
            this.konecProgramuToolStripMenuItem1});
            this.údržbaToolStripMenuItem.Name = "údržbaToolStripMenuItem";
            this.údržbaToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.údržbaToolStripMenuItem.Text = "Údržba";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem9.Text = "Písmo";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItemFont_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem7.Text = "Nastavení DB";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // nahráníDatZDBaseToolStripMenuItem
            // 
            this.nahráníDatZDBaseToolStripMenuItem.Name = "nahráníDatZDBaseToolStripMenuItem";
            this.nahráníDatZDBaseToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.nahráníDatZDBaseToolStripMenuItem.Text = "Nahrání dat z dBase";
            this.nahráníDatZDBaseToolStripMenuItem.Click += new System.EventHandler(this.nahráníDatZDBaseToolStripMenuItem_Click);
            // 
            // vytvoreniTabulekToolStripMenuItem
            // 
            this.vytvoreniTabulekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vytvoreniToolStripMenuItem,
            this.smazániToolStripMenuItem,
            this.vymazáníToolStripMenuItem});
            this.vytvoreniTabulekToolStripMenuItem.Name = "vytvoreniTabulekToolStripMenuItem";
            this.vytvoreniTabulekToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.vytvoreniTabulekToolStripMenuItem.Text = "Tabulky";
            this.vytvoreniTabulekToolStripMenuItem.Click += new System.EventHandler(this.vytvoreniTabulekToolStripMenuItem_Click);
            // 
            // vytvoreniToolStripMenuItem
            // 
            this.vytvoreniToolStripMenuItem.Name = "vytvoreniToolStripMenuItem";
            this.vytvoreniToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.vytvoreniToolStripMenuItem.Text = "Vytvoření";
            this.vytvoreniToolStripMenuItem.Click += new System.EventHandler(this.vytvoreniToolStripMenuItem_Click);
            // 
            // smazániToolStripMenuItem
            // 
            this.smazániToolStripMenuItem.Name = "smazániToolStripMenuItem";
            this.smazániToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.smazániToolStripMenuItem.Text = "Zrušení";
            this.smazániToolStripMenuItem.Click += new System.EventHandler(this.smazániToolStripMenuItem_Click);
            // 
            // vymazáníToolStripMenuItem
            // 
            this.vymazáníToolStripMenuItem.Name = "vymazáníToolStripMenuItem";
            this.vymazáníToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.vymazáníToolStripMenuItem.Text = "Vymazání";
            this.vymazáníToolStripMenuItem.Click += new System.EventHandler(this.vymazáníToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem10.Text = "Vytvoření / obnovení indexů";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.IndexesToolStripMenuItem_Click);
            // 
            // konecProgramuToolStripMenuItem1
            // 
            this.konecProgramuToolStripMenuItem1.Name = "konecProgramuToolStripMenuItem1";
            this.konecProgramuToolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
            this.konecProgramuToolStripMenuItem1.Text = "Konec programu";
            this.konecProgramuToolStripMenuItem1.Click += new System.EventHandler(this.konecProgramuToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 48);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(919, 374);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.přidatPoložkuToolStripMenuItem,
            this.opravitPoložkuToolStripMenuItem,
            this.smazatPoložkuToolStripMenuItem,
            this.příjemMaterialuToolStripMenuItem,
            this.poškozeníNářadíToolStripMenuItem,
            this.zapůjčeníNářadíToolStripMenuItem,
            this.prohledáváníToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 180);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // přidatPoložkuToolStripMenuItem
            // 
            this.přidatPoložkuToolStripMenuItem.Name = "přidatPoložkuToolStripMenuItem";
            this.přidatPoložkuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.přidatPoložkuToolStripMenuItem.Text = "Přidat položku";
            this.přidatPoložkuToolStripMenuItem.Click += new System.EventHandler(this.ConMenuAddItem);
            // 
            // opravitPoložkuToolStripMenuItem
            // 
            this.opravitPoložkuToolStripMenuItem.Name = "opravitPoložkuToolStripMenuItem";
            this.opravitPoložkuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.opravitPoložkuToolStripMenuItem.Text = "Opravit položku";
            this.opravitPoložkuToolStripMenuItem.Click += new System.EventHandler(this.ConMenuEditItem);
            // 
            // smazatPoložkuToolStripMenuItem
            // 
            this.smazatPoložkuToolStripMenuItem.Name = "smazatPoložkuToolStripMenuItem";
            this.smazatPoložkuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.smazatPoložkuToolStripMenuItem.Text = "Smazat položku";
            this.smazatPoložkuToolStripMenuItem.Click += new System.EventHandler(this.ConMenuDeleteItem);
            // 
            // příjemMaterialuToolStripMenuItem
            // 
            this.příjemMaterialuToolStripMenuItem.Name = "příjemMaterialuToolStripMenuItem";
            this.příjemMaterialuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.příjemMaterialuToolStripMenuItem.Text = "Příjem na kartu";
            this.příjemMaterialuToolStripMenuItem.Click += new System.EventHandler(this.conMenuAddMat);
            // 
            // poškozeníNářadíToolStripMenuItem
            // 
            this.poškozeníNářadíToolStripMenuItem.Name = "poškozeníNářadíToolStripMenuItem";
            this.poškozeníNářadíToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.poškozeníNářadíToolStripMenuItem.Text = "Poškození nářadí";
            this.poškozeníNářadíToolStripMenuItem.Click += new System.EventHandler(this.conMenuDelMat_Click);
            // 
            // zapůjčeníNářadíToolStripMenuItem
            // 
            this.zapůjčeníNářadíToolStripMenuItem.Name = "zapůjčeníNářadíToolStripMenuItem";
            this.zapůjčeníNářadíToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.zapůjčeníNářadíToolStripMenuItem.Text = "Zapůjčení/Vrácení nářadí";
            this.zapůjčeníNářadíToolStripMenuItem.Click += new System.EventHandler(this.ConMenuPujcNaradi_Click);
            // 
            // prohledáváníToolStripMenuItem
            // 
            this.prohledáváníToolStripMenuItem.Name = "prohledáváníToolStripMenuItem";
            this.prohledáváníToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.prohledáváníToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.prohledáváníToolStripMenuItem.Text = "Prohledávání";
            this.prohledáváníToolStripMenuItem.Click += new System.EventHandler(this.ConMenuProhledávani_Click);
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(16, 32);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(0, 13);
            this.labelView.TabIndex = 4;
            // 
            // labelStateConnection
            // 
            this.labelStateConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateConnection.AutoSize = true;
            this.labelStateConnection.Location = new System.Drawing.Point(793, 428);
            this.labelStateConnection.Name = "labelStateConnection";
            this.labelStateConnection.Size = new System.Drawing.Size(35, 13);
            this.labelStateConnection.TabIndex = 5;
            this.labelStateConnection.Text = "label1";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem11.Text = "Správa uživatelských účtů";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // Vydejna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 446);
            this.Controls.Add(this.labelStateConnection);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.progressBarMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Vydejna";
            this.Text = "Výdejna";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem údržbaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nahráníDatZDBaseToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem vytvoreniTabulekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vytvoreniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smazániToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vymazáníToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem pracovníciProvozuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem přidatPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opravitPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smazatPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecProgramuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem příjemMaterialuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poškozeníNářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapůjčeníNářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapujceniNářadíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prohledáváníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.Label labelStateConnection;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
    }
}

