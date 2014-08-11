namespace Vydejna
{
    partial class ZmenyOprava
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
            this.textBoxPoznamka = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelDatum = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelOperace = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxVevcislo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelPrijem = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.labelVydej = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.labelZustatek = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.labelOsCislo = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPoznamka
            // 
            this.textBoxPoznamka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPoznamka.Location = new System.Drawing.Point(6, 18);
            this.textBoxPoznamka.MaxLength = 22;
            this.textBoxPoznamka.Name = "textBoxPoznamka";
            this.textBoxPoznamka.Size = new System.Drawing.Size(132, 20);
            this.textBoxPoznamka.TabIndex = 0;
            this.textBoxPoznamka.TextChanged += new System.EventHandler(this.textBoxPoznamka_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelDatum);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datum";
            // 
            // labelDatum
            // 
            this.labelDatum.AutoSize = true;
            this.labelDatum.Location = new System.Drawing.Point(6, 21);
            this.labelDatum.Name = "labelDatum";
            this.labelDatum.Size = new System.Drawing.Size(35, 13);
            this.labelDatum.TabIndex = 0;
            this.labelDatum.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelOperace);
            this.groupBox2.Location = new System.Drawing.Point(116, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(69, 48);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operace";
            // 
            // labelOperace
            // 
            this.labelOperace.AutoSize = true;
            this.labelOperace.Location = new System.Drawing.Point(6, 21);
            this.labelOperace.Name = "labelOperace";
            this.labelOperace.Size = new System.Drawing.Size(35, 13);
            this.labelOperace.TabIndex = 0;
            this.labelOperace.Text = "label1";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textBoxPoznamka);
            this.groupBox3.Location = new System.Drawing.Point(191, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 48);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Poznámka";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.textBoxVevcislo);
            this.groupBox4.Location = new System.Drawing.Point(343, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(80, 50);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pom. inv. č.";
            // 
            // textBoxVevcislo
            // 
            this.textBoxVevcislo.Location = new System.Drawing.Point(6, 18);
            this.textBoxVevcislo.MaxLength = 12;
            this.textBoxVevcislo.Name = "textBoxVevcislo";
            this.textBoxVevcislo.Size = new System.Drawing.Size(65, 20);
            this.textBoxVevcislo.TabIndex = 0;
            this.textBoxVevcislo.TextChanged += new System.EventHandler(this.textBoxVevcislo_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.labelPrijem);
            this.groupBox5.Location = new System.Drawing.Point(429, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(56, 50);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Příjem";
            // 
            // labelPrijem
            // 
            this.labelPrijem.AutoSize = true;
            this.labelPrijem.Location = new System.Drawing.Point(7, 21);
            this.labelPrijem.Name = "labelPrijem";
            this.labelPrijem.Size = new System.Drawing.Size(35, 13);
            this.labelPrijem.TabIndex = 0;
            this.labelPrijem.Text = "label1";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.labelVydej);
            this.groupBox6.Location = new System.Drawing.Point(491, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(56, 50);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Výdej";
            // 
            // labelVydej
            // 
            this.labelVydej.AutoSize = true;
            this.labelVydej.Location = new System.Drawing.Point(7, 21);
            this.labelVydej.Name = "labelVydej";
            this.labelVydej.Size = new System.Drawing.Size(35, 13);
            this.labelVydej.TabIndex = 0;
            this.labelVydej.Text = "label1";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.labelZustatek);
            this.groupBox7.Location = new System.Drawing.Point(553, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(56, 50);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Zůs.";
            // 
            // labelZustatek
            // 
            this.labelZustatek.AutoSize = true;
            this.labelZustatek.Location = new System.Drawing.Point(7, 21);
            this.labelZustatek.Name = "labelZustatek";
            this.labelZustatek.Size = new System.Drawing.Size(35, 13);
            this.labelZustatek.TabIndex = 0;
            this.labelZustatek.Text = "label1";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.labelOsCislo);
            this.groupBox8.Location = new System.Drawing.Point(615, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(56, 50);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Zapůj.";
            // 
            // labelOsCislo
            // 
            this.labelOsCislo.AutoSize = true;
            this.labelOsCislo.Location = new System.Drawing.Point(7, 21);
            this.labelOsCislo.Name = "labelOsCislo";
            this.labelOsCislo.Size = new System.Drawing.Size(35, 13);
            this.labelOsCislo.TabIndex = 0;
            this.labelOsCislo.Text = "label1";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 68);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(593, 68);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(78, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "Budiž";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonRetry
            // 
            this.buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRetry.Location = new System.Drawing.Point(93, 68);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(78, 23);
            this.buttonRetry.TabIndex = 18;
            this.buttonRetry.Text = "Obnovit";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // ZmenyOprava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 103);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(650, 137);
            this.Name = "ZmenyOprava";
            this.Text = "Změna údajů";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPoznamka;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxVevcislo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label labelDatum;
        private System.Windows.Forms.Label labelOperace;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label labelPrijem;
        private System.Windows.Forms.Label labelVydej;
        private System.Windows.Forms.Label labelZustatek;
        private System.Windows.Forms.Label labelOsCislo;
    }
}