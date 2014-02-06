namespace Vydejna
{
    partial class SeznamUzivatelu
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.přidatPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opravitPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.smazatPoložkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prohledáváníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelView = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.přidatPoložkuToolStripMenuItem,
            this.opravitPoložkuToolStripMenuItem,
            this.toolStripMenuItem1,
            this.smazatPoložkuToolStripMenuItem,
            this.prohledáváníToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 136);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // přidatPoložkuToolStripMenuItem
            // 
            this.přidatPoložkuToolStripMenuItem.Name = "přidatPoložkuToolStripMenuItem";
            this.přidatPoložkuToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.přidatPoložkuToolStripMenuItem.Text = "Přidat položku";
            this.přidatPoložkuToolStripMenuItem.Click += new System.EventHandler(this.přidatPoložkuToolStripMenuItem_Click);
            // 
            // opravitPoložkuToolStripMenuItem
            // 
            this.opravitPoložkuToolStripMenuItem.Name = "opravitPoložkuToolStripMenuItem";
            this.opravitPoložkuToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.opravitPoložkuToolStripMenuItem.Text = "Opravit položku";
            this.opravitPoložkuToolStripMenuItem.Click += new System.EventHandler(this.opravitPoložkuToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItem1.Text = "Změnit heslo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // smazatPoložkuToolStripMenuItem
            // 
            this.smazatPoložkuToolStripMenuItem.Name = "smazatPoložkuToolStripMenuItem";
            this.smazatPoložkuToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.smazatPoložkuToolStripMenuItem.Text = "Smazat položku";
            this.smazatPoložkuToolStripMenuItem.Click += new System.EventHandler(this.smazatPoložkuToolStripMenuItem_Click);
            // 
            // prohledáváníToolStripMenuItem
            // 
            this.prohledáváníToolStripMenuItem.Name = "prohledáváníToolStripMenuItem";
            this.prohledáváníToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.prohledáváníToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.prohledáváníToolStripMenuItem.Text = "Prohledávání";
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(12, 9);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(35, 13);
            this.labelView.TabIndex = 19;
            this.labelView.Text = "label1";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 352);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(651, 310);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // SeznamUzivatelu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 387);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelView);
            this.Name = "SeznamUzivatelu";
            this.Text = "Seznam uživatelů";
            this.Shown += new System.EventHandler(this.SeznamUzivatelu_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem přidatPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opravitPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smazatPoložkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prohledáváníToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}