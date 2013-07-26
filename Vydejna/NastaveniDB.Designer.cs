namespace Vydejna
{
    partial class NastaveniDB
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPortDefault = new System.Windows.Forms.Button();
            this.buttonBrowseDialog = new System.Windows.Forms.Button();
            this.textBoxAdresa = new System.Windows.Forms.TextBox();
            this.textBoxUmisteni = new System.Windows.Forms.TextBox();
            this.textBoxNazev = new System.Windows.Forms.TextBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTypDB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxUserPasswd = new System.Windows.Forms.TextBox();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAdminPasswd = new System.Windows.Forms.TextBox();
            this.textBoxAdminId = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dbFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxUserPasswd = new System.Windows.Forms.CheckBox();
            this.checkBoxAdminPasswd = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPortDefault);
            this.groupBox1.Controls.Add(this.buttonBrowseDialog);
            this.groupBox1.Controls.Add(this.textBoxAdresa);
            this.groupBox1.Controls.Add(this.textBoxUmisteni);
            this.groupBox1.Controls.Add(this.textBoxNazev);
            this.groupBox1.Controls.Add(this.numericUpDownPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Databáze";
            // 
            // buttonPortDefault
            // 
            this.buttonPortDefault.Location = new System.Drawing.Point(297, 103);
            this.buttonPortDefault.Name = "buttonPortDefault";
            this.buttonPortDefault.Size = new System.Drawing.Size(122, 23);
            this.buttonPortDefault.TabIndex = 9;
            this.buttonPortDefault.Text = "Doporučené";
            this.buttonPortDefault.UseVisualStyleBackColor = true;
            this.buttonPortDefault.Click += new System.EventHandler(this.buttonPortDefault_Click);
            // 
            // buttonBrowseDialog
            // 
            this.buttonBrowseDialog.Location = new System.Drawing.Point(384, 49);
            this.buttonBrowseDialog.Name = "buttonBrowseDialog";
            this.buttonBrowseDialog.Size = new System.Drawing.Size(35, 23);
            this.buttonBrowseDialog.TabIndex = 8;
            this.buttonBrowseDialog.UseVisualStyleBackColor = true;
            this.buttonBrowseDialog.Click += new System.EventHandler(this.buttonBrowseDialog_Click);
            // 
            // textBoxAdresa
            // 
            this.textBoxAdresa.Location = new System.Drawing.Point(202, 77);
            this.textBoxAdresa.Name = "textBoxAdresa";
            this.textBoxAdresa.Size = new System.Drawing.Size(176, 20);
            this.textBoxAdresa.TabIndex = 7;
            // 
            // textBoxUmisteni
            // 
            this.textBoxUmisteni.Location = new System.Drawing.Point(202, 51);
            this.textBoxUmisteni.Name = "textBoxUmisteni";
            this.textBoxUmisteni.Size = new System.Drawing.Size(176, 20);
            this.textBoxUmisteni.TabIndex = 6;
            // 
            // textBoxNazev
            // 
            this.textBoxNazev.Location = new System.Drawing.Point(202, 25);
            this.textBoxNazev.Name = "textBoxNazev";
            this.textBoxNazev.Size = new System.Drawing.Size(176, 20);
            this.textBoxNazev.TabIndex = 5;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(202, 103);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65556,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownPort.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Port :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Adresa DB serveru :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Umístění  :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Název :";
            // 
            // comboBoxTypDB
            // 
            this.comboBoxTypDB.FormattingEnabled = true;
            this.comboBoxTypDB.Location = new System.Drawing.Point(214, 17);
            this.comboBoxTypDB.Name = "comboBoxTypDB";
            this.comboBoxTypDB.Size = new System.Drawing.Size(176, 21);
            this.comboBoxTypDB.TabIndex = 1;
            this.comboBoxTypDB.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypDB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Typ databáze :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxUserPasswd);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxUserPasswd);
            this.groupBox2.Controls.Add(this.textBoxUserId);
            this.groupBox2.Location = new System.Drawing.Point(12, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 80);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Práva uživatele";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Heslo :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Jméno :";
            // 
            // textBoxUserPasswd
            // 
            this.textBoxUserPasswd.Location = new System.Drawing.Point(202, 45);
            this.textBoxUserPasswd.Name = "textBoxUserPasswd";
            this.textBoxUserPasswd.PasswordChar = '*';
            this.textBoxUserPasswd.Size = new System.Drawing.Size(176, 20);
            this.textBoxUserPasswd.TabIndex = 1;
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(202, 19);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(176, 20);
            this.textBoxUserId.TabIndex = 0;
            this.textBoxUserId.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxAdminPasswd);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxAdminPasswd);
            this.groupBox3.Controls.Add(this.textBoxAdminId);
            this.groupBox3.Location = new System.Drawing.Point(12, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(432, 80);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Práva administratora";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Heslo :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Jméno :";
            // 
            // textBoxAdminPasswd
            // 
            this.textBoxAdminPasswd.Location = new System.Drawing.Point(202, 45);
            this.textBoxAdminPasswd.Name = "textBoxAdminPasswd";
            this.textBoxAdminPasswd.PasswordChar = '*';
            this.textBoxAdminPasswd.Size = new System.Drawing.Size(176, 20);
            this.textBoxAdminPasswd.TabIndex = 1;
            // 
            // textBoxAdminId
            // 
            this.textBoxAdminId.Location = new System.Drawing.Point(202, 19);
            this.textBoxAdminId.Name = "textBoxAdminId";
            this.textBoxAdminId.Size = new System.Drawing.Size(176, 20);
            this.textBoxAdminId.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(12, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zruš";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(369, 422);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Budiž";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBoxUserPasswd
            // 
            this.checkBoxUserPasswd.AutoSize = true;
            this.checkBoxUserPasswd.Location = new System.Drawing.Point(384, 51);
            this.checkBoxUserPasswd.Name = "checkBoxUserPasswd";
            this.checkBoxUserPasswd.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUserPasswd.TabIndex = 4;
            this.checkBoxUserPasswd.UseVisualStyleBackColor = true;
            this.checkBoxUserPasswd.CheckStateChanged += new System.EventHandler(this.checkBoxUserPasswd_CheckStateChanged);
            // 
            // checkBoxAdminPasswd
            // 
            this.checkBoxAdminPasswd.AutoSize = true;
            this.checkBoxAdminPasswd.Location = new System.Drawing.Point(384, 51);
            this.checkBoxAdminPasswd.Name = "checkBoxAdminPasswd";
            this.checkBoxAdminPasswd.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAdminPasswd.TabIndex = 5;
            this.checkBoxAdminPasswd.UseVisualStyleBackColor = true;
            this.checkBoxAdminPasswd.CheckedChanged += new System.EventHandler(this.checkBoxAdminPasswd_CheckedChanged);
            // 
            // NastaveniDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 457);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTypDB);
            this.Controls.Add(this.groupBox1);
            this.Name = "NastaveniDB";
            this.Text = "NastaveniDB";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxTypDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.TextBox textBoxAdresa;
        private System.Windows.Forms.TextBox textBoxUmisteni;
        private System.Windows.Forms.TextBox textBoxNazev;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxUserPasswd;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxAdminPasswd;
        private System.Windows.Forms.TextBox textBoxAdminId;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonBrowseDialog;
        private System.Windows.Forms.FolderBrowserDialog dbFolderBrowserDialog;
        private System.Windows.Forms.Button buttonPortDefault;
        private System.Windows.Forms.CheckBox checkBoxUserPasswd;
        private System.Windows.Forms.CheckBox checkBoxAdminPasswd;
    }
}