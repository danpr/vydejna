namespace Vydejna
{
    partial class OpravaKarta
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
            this.dataGridViewZmeny = new System.Windows.Forms.DataGridView();
            this.groupBoxFyzStav = new System.Windows.Forms.GroupBox();
            this.labelFyzStav = new System.Windows.Forms.Label();
            this.numericUpDownFyzStav = new System.Windows.Forms.NumericUpDown();
            this.groupBoxUcetSatv = new System.Windows.Forms.GroupBox();
            this.labelUcetStav = new System.Windows.Forms.Label();
            this.numericUpDownUcetStav = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNazev = new System.Windows.Forms.Label();
            this.labelJK = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownStartStav = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).BeginInit();
            this.groupBoxFyzStav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFyzStav)).BeginInit();
            this.groupBoxUcetSatv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartStav)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewZmeny
            // 
            this.dataGridViewZmeny.AllowUserToAddRows = false;
            this.dataGridViewZmeny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewZmeny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZmeny.Location = new System.Drawing.Point(22, 186);
            this.dataGridViewZmeny.MultiSelect = false;
            this.dataGridViewZmeny.Name = "dataGridViewZmeny";
            this.dataGridViewZmeny.ReadOnly = true;
            this.dataGridViewZmeny.Size = new System.Drawing.Size(710, 188);
            this.dataGridViewZmeny.TabIndex = 17;
            // 
            // groupBoxFyzStav
            // 
            this.groupBoxFyzStav.Controls.Add(this.labelFyzStav);
            this.groupBoxFyzStav.Controls.Add(this.numericUpDownFyzStav);
            this.groupBoxFyzStav.Location = new System.Drawing.Point(244, 88);
            this.groupBoxFyzStav.Name = "groupBoxFyzStav";
            this.groupBoxFyzStav.Size = new System.Drawing.Size(241, 78);
            this.groupBoxFyzStav.TabIndex = 18;
            this.groupBoxFyzStav.TabStop = false;
            this.groupBoxFyzStav.Text = "Fyzický stav";
            // 
            // labelFyzStav
            // 
            this.labelFyzStav.AutoSize = true;
            this.labelFyzStav.Location = new System.Drawing.Point(78, 23);
            this.labelFyzStav.Name = "labelFyzStav";
            this.labelFyzStav.Size = new System.Drawing.Size(65, 13);
            this.labelFyzStav.TabIndex = 15;
            this.labelFyzStav.Text = "labeFyzStav";
            // 
            // numericUpDownFyzStav
            // 
            this.numericUpDownFyzStav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFyzStav.Location = new System.Drawing.Point(82, 43);
            this.numericUpDownFyzStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownFyzStav.Name = "numericUpDownFyzStav";
            this.numericUpDownFyzStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownFyzStav.TabIndex = 14;
            // 
            // groupBoxUcetSatv
            // 
            this.groupBoxUcetSatv.Controls.Add(this.labelUcetStav);
            this.groupBoxUcetSatv.Controls.Add(this.numericUpDownUcetStav);
            this.groupBoxUcetSatv.Location = new System.Drawing.Point(491, 88);
            this.groupBoxUcetSatv.Name = "groupBoxUcetSatv";
            this.groupBoxUcetSatv.Size = new System.Drawing.Size(241, 78);
            this.groupBoxUcetSatv.TabIndex = 19;
            this.groupBoxUcetSatv.TabStop = false;
            this.groupBoxUcetSatv.Text = "Účetní stav";
            // 
            // labelUcetStav
            // 
            this.labelUcetStav.AutoSize = true;
            this.labelUcetStav.Location = new System.Drawing.Point(79, 23);
            this.labelUcetStav.Name = "labelUcetStav";
            this.labelUcetStav.Size = new System.Drawing.Size(74, 13);
            this.labelUcetStav.TabIndex = 15;
            this.labelUcetStav.Text = "labelUcetStav";
            // 
            // numericUpDownUcetStav
            // 
            this.numericUpDownUcetStav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownUcetStav.Location = new System.Drawing.Point(82, 43);
            this.numericUpDownUcetStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownUcetStav.Name = "numericUpDownUcetStav";
            this.numericUpDownUcetStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownUcetStav.TabIndex = 14;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(22, 389);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(654, 389);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(78, 23);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "Budiž";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonRetry
            // 
            this.buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRetry.Location = new System.Drawing.Point(325, 389);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(78, 23);
            this.buttonRetry.TabIndex = 22;
            this.buttonRetry.Text = "Obnov";
            this.buttonRetry.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Název nářadi :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Označení JK :";
            // 
            // labelNazev
            // 
            this.labelNazev.AutoSize = true;
            this.labelNazev.Location = new System.Drawing.Point(117, 19);
            this.labelNazev.Name = "labelNazev";
            this.labelNazev.Size = new System.Drawing.Size(60, 13);
            this.labelNazev.TabIndex = 25;
            this.labelNazev.Text = "labelNazev";
            // 
            // labelJK
            // 
            this.labelJK.AutoSize = true;
            this.labelJK.Location = new System.Drawing.Point(117, 53);
            this.labelJK.Name = "labelJK";
            this.labelJK.Size = new System.Drawing.Size(41, 13);
            this.labelJK.TabIndex = 26;
            this.labelJK.Text = "labelJK";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownStartStav);
            this.groupBox1.Location = new System.Drawing.Point(25, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 78);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Počateční stav";
            // 
            // numericUpDownStartStav
            // 
            this.numericUpDownStartStav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStartStav.Location = new System.Drawing.Point(52, 43);
            this.numericUpDownStartStav.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStartStav.Name = "numericUpDownStartStav";
            this.numericUpDownStartStav.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownStartStav.TabIndex = 15;
            this.numericUpDownStartStav.ValueChanged += new System.EventHandler(this.numericUpDownStartStav_ValueChanged);
            // 
            // OpravaKarta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 424);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelJK);
            this.Controls.Add(this.labelNazev);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxUcetSatv);
            this.Controls.Add(this.groupBoxFyzStav);
            this.Controls.Add(this.dataGridViewZmeny);
            this.Name = "OpravaKarta";
            this.Text = "OpravaKarta";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZmeny)).EndInit();
            this.groupBoxFyzStav.ResumeLayout(false);
            this.groupBoxFyzStav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFyzStav)).EndInit();
            this.groupBoxUcetSatv.ResumeLayout(false);
            this.groupBoxUcetSatv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUcetStav)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartStav)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewZmeny;
        private System.Windows.Forms.GroupBox groupBoxFyzStav;
        private System.Windows.Forms.GroupBox groupBoxUcetSatv;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNazev;
        private System.Windows.Forms.Label labelJK;
        private System.Windows.Forms.NumericUpDown numericUpDownFyzStav;
        private System.Windows.Forms.Label labelFyzStav;
        private System.Windows.Forms.NumericUpDown numericUpDownUcetStav;
        private System.Windows.Forms.Label labelUcetStav;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownStartStav;
    }
}