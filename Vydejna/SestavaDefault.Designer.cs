namespace Vydejna
{
    partial class SestavaDefault
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
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.labelVyber = new System.Windows.Forms.Label();
            this.textBoxVyber = new System.Windows.Forms.TextBox();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewSestava = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCelkem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSestava)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Od :";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(45, 14);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Do :";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(225, 14);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(122, 20);
            this.dateTimePickerTo.TabIndex = 3;
            // 
            // labelVyber
            // 
            this.labelVyber.AutoSize = true;
            this.labelVyber.Location = new System.Drawing.Point(368, 18);
            this.labelVyber.Name = "labelVyber";
            this.labelVyber.Size = new System.Drawing.Size(55, 13);
            this.labelVyber.TabIndex = 4;
            this.labelVyber.Text = "TextVyber";
            // 
            // textBoxVyber
            // 
            this.textBoxVyber.Location = new System.Drawing.Point(429, 14);
            this.textBoxVyber.Name = "textBoxVyber";
            this.textBoxVyber.Size = new System.Drawing.Size(99, 20);
            this.textBoxVyber.TabIndex = 5;
            // 
            // buttonRetry
            // 
            this.buttonRetry.Location = new System.Drawing.Point(556, 11);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(71, 27);
            this.buttonRetry.TabIndex = 6;
            this.buttonRetry.Text = "Obnov";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(644, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "Tisk";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSestava
            // 
            this.dataGridViewSestava.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSestava.Location = new System.Drawing.Point(-1, 45);
            this.dataGridViewSestava.Name = "dataGridViewSestava";
            this.dataGridViewSestava.Size = new System.Drawing.Size(759, 358);
            this.dataGridViewSestava.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Celkem :";
            // 
            // labelCelkem
            // 
            this.labelCelkem.AutoSize = true;
            this.labelCelkem.Location = new System.Drawing.Point(97, 409);
            this.labelCelkem.Name = "labelCelkem";
            this.labelCelkem.Size = new System.Drawing.Size(28, 13);
            this.labelCelkem.TabIndex = 10;
            this.labelCelkem.Text = "0.00";
            // 
            // SestavaDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 431);
            this.Controls.Add(this.labelCelkem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewSestava);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.textBoxVyber);
            this.Controls.Add(this.labelVyber);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.label1);
            this.Name = "SestavaDefault";
            this.Text = "SestavaDefault";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSestava)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label labelVyber;
        private System.Windows.Forms.TextBox textBoxVyber;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewSestava;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCelkem;
    }
}