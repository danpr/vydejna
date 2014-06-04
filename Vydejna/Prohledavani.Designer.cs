namespace Vydejna
{
    partial class Prohledavani
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
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            this.comboBoxNumeric = new System.Windows.Forms.ComboBox();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownNumeric = new System.Windows.Forms.NumericUpDown();
            this.textBoxString = new System.Windows.Forms.TextBox();
            this.checkBoxUpcase = new System.Windows.Forms.CheckBox();
            this.checkBoxFromStart = new System.Windows.Forms.CheckBox();
            this.checkBoxFromFirstChar = new System.Windows.Forms.CheckBox();
            this.checkBoxDiacritism = new System.Windows.Forms.CheckBox();
            this.checkBoxWildCard = new System.Windows.Forms.CheckBox();
            this.comboBoxRegex = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(146, 12);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(135, 21);
            this.comboBoxColumns.TabIndex = 0;
            this.comboBoxColumns.SelectedIndexChanged += new System.EventHandler(this.comboBoxColumns_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prohledávaný sloupec :";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(15, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(234, 284);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "Hledat";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxDate);
            this.groupBox1.Controls.Add(this.comboBoxNumeric);
            this.groupBox1.Controls.Add(this.dateTimePickerDate);
            this.groupBox1.Controls.Add(this.numericUpDownNumeric);
            this.groupBox1.Controls.Add(this.textBoxString);
            this.groupBox1.Location = new System.Drawing.Point(15, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hledat";
            // 
            // comboBoxDate
            // 
            this.comboBoxDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDate.FormattingEnabled = true;
            this.comboBoxDate.Items.AddRange(new object[] {
            "=",
            ">",
            "<"});
            this.comboBoxDate.Location = new System.Drawing.Point(21, 72);
            this.comboBoxDate.Name = "comboBoxDate";
            this.comboBoxDate.Size = new System.Drawing.Size(63, 21);
            this.comboBoxDate.TabIndex = 3;
            // 
            // comboBoxNumeric
            // 
            this.comboBoxNumeric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNumeric.FormattingEnabled = true;
            this.comboBoxNumeric.Items.AddRange(new object[] {
            "=",
            ">",
            "<"});
            this.comboBoxNumeric.Location = new System.Drawing.Point(21, 45);
            this.comboBoxNumeric.Name = "comboBoxNumeric";
            this.comboBoxNumeric.Size = new System.Drawing.Size(63, 21);
            this.comboBoxNumeric.TabIndex = 1;
            this.comboBoxNumeric.SelectedIndexChanged += new System.EventHandler(this.comboBoxNumeric_SelectedIndexChanged);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(119, 71);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(147, 20);
            this.dateTimePickerDate.TabIndex = 4;
            // 
            // numericUpDownNumeric
            // 
            this.numericUpDownNumeric.Location = new System.Drawing.Point(119, 46);
            this.numericUpDownNumeric.Name = "numericUpDownNumeric";
            this.numericUpDownNumeric.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownNumeric.TabIndex = 2;
            // 
            // textBoxString
            // 
            this.textBoxString.Location = new System.Drawing.Point(21, 19);
            this.textBoxString.Name = "textBoxString";
            this.textBoxString.Size = new System.Drawing.Size(245, 20);
            this.textBoxString.TabIndex = 0;
            this.textBoxString.TextChanged += new System.EventHandler(this.textBoxString_TextChanged);
            // 
            // checkBoxUpcase
            // 
            this.checkBoxUpcase.AutoSize = true;
            this.checkBoxUpcase.Location = new System.Drawing.Point(35, 199);
            this.checkBoxUpcase.Name = "checkBoxUpcase";
            this.checkBoxUpcase.Size = new System.Drawing.Size(148, 17);
            this.checkBoxUpcase.TabIndex = 3;
            this.checkBoxUpcase.Text = "Ignorovat velikost písmen";
            this.checkBoxUpcase.UseVisualStyleBackColor = true;
            this.checkBoxUpcase.CheckedChanged += new System.EventHandler(this.checkBoxUpcase_CheckedChanged);
            // 
            // checkBoxFromStart
            // 
            this.checkBoxFromStart.AutoSize = true;
            this.checkBoxFromStart.Location = new System.Drawing.Point(35, 153);
            this.checkBoxFromStart.Name = "checkBoxFromStart";
            this.checkBoxFromStart.Size = new System.Drawing.Size(169, 17);
            this.checkBoxFromStart.TabIndex = 4;
            this.checkBoxFromStart.Text = "Prohledávat od prvního řádku";
            this.checkBoxFromStart.UseVisualStyleBackColor = true;
            this.checkBoxFromStart.CheckedChanged += new System.EventHandler(this.checkBoxFromStart_CheckedChanged);
            // 
            // checkBoxFromFirstChar
            // 
            this.checkBoxFromFirstChar.AutoSize = true;
            this.checkBoxFromFirstChar.Location = new System.Drawing.Point(36, 176);
            this.checkBoxFromFirstChar.Name = "checkBoxFromFirstChar";
            this.checkBoxFromFirstChar.Size = new System.Drawing.Size(168, 17);
            this.checkBoxFromFirstChar.TabIndex = 7;
            this.checkBoxFromFirstChar.Text = "Porovnávat od prvního znaku";
            this.checkBoxFromFirstChar.UseVisualStyleBackColor = true;
            this.checkBoxFromFirstChar.CheckedChanged += new System.EventHandler(this.checkBoxFromFirstChar_CheckedChanged);
            // 
            // checkBoxDiacritism
            // 
            this.checkBoxDiacritism.AutoSize = true;
            this.checkBoxDiacritism.Location = new System.Drawing.Point(36, 222);
            this.checkBoxDiacritism.Name = "checkBoxDiacritism";
            this.checkBoxDiacritism.Size = new System.Drawing.Size(114, 17);
            this.checkBoxDiacritism.TabIndex = 8;
            this.checkBoxDiacritism.Text = "Používat diakritiku";
            this.checkBoxDiacritism.UseVisualStyleBackColor = true;
            this.checkBoxDiacritism.CheckedChanged += new System.EventHandler(this.checkBoxDiacritism_CheckedChanged);
            // 
            // checkBoxWildCard
            // 
            this.checkBoxWildCard.AutoSize = true;
            this.checkBoxWildCard.Checked = true;
            this.checkBoxWildCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWildCard.Location = new System.Drawing.Point(36, 245);
            this.checkBoxWildCard.Name = "checkBoxWildCard";
            this.checkBoxWildCard.Size = new System.Drawing.Size(57, 17);
            this.checkBoxWildCard.TabIndex = 9;
            this.checkBoxWildCard.Text = "Použít";
            this.checkBoxWildCard.UseVisualStyleBackColor = true;
            this.checkBoxWildCard.CheckedChanged += new System.EventHandler(this.checkBoxWildCard_CheckedChanged);
            // 
            // comboBoxRegex
            // 
            this.comboBoxRegex.FormattingEnabled = true;
            this.comboBoxRegex.Items.AddRange(new object[] {
            "Divoké karty",
            "Regularni výrazy"});
            this.comboBoxRegex.Location = new System.Drawing.Point(134, 243);
            this.comboBoxRegex.Name = "comboBoxRegex";
            this.comboBoxRegex.Size = new System.Drawing.Size(147, 21);
            this.comboBoxRegex.TabIndex = 10;
            this.comboBoxRegex.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Prohledavani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 323);
            this.Controls.Add(this.comboBoxRegex);
            this.Controls.Add(this.checkBoxWildCard);
            this.Controls.Add(this.checkBoxDiacritism);
            this.Controls.Add(this.checkBoxFromFirstChar);
            this.Controls.Add(this.checkBoxFromStart);
            this.Controls.Add(this.checkBoxUpcase);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxColumns);
            this.MinimumSize = new System.Drawing.Size(337, 298);
            this.Name = "Prohledavani";
            this.Text = "Prohledávání tabulky";
            this.Shown += new System.EventHandler(this.Prohledavani_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxDate;
        private System.Windows.Forms.ComboBox comboBoxNumeric;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.NumericUpDown numericUpDownNumeric;
        private System.Windows.Forms.TextBox textBoxString;
        private System.Windows.Forms.CheckBox checkBoxUpcase;
        private System.Windows.Forms.CheckBox checkBoxFromStart;
        private System.Windows.Forms.CheckBox checkBoxFromFirstChar;
        private System.Windows.Forms.CheckBox checkBoxDiacritism;
        private System.Windows.Forms.CheckBox checkBoxWildCard;
        private System.Windows.Forms.ComboBox comboBoxRegex;
    }
}