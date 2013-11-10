using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class SeznamNaradiJednoduchy : Form
    {
        public SeznamNaradiJednoduchy(vDatabase myDataBase)
        {
            InitializeComponent();
            labelView.Text = "";

            // nastaveni gridView

            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

            if (myDataBase.DBIsOpened())
            {
                try
                {
                    labelView.Text = "Pracovníci provozu - Načítání";
                    Application.DoEvents();

                    dataGridView1.DataSource = myDataBase.loadDataTableNaradiJednoduchy();

                    dataGridView1.Columns[0].HeaderText = "Pořadí";
                    dataGridView1.Columns[1].HeaderText = "Název";
                    dataGridView1.Columns[2].HeaderText = "Označení JK";
                    dataGridView1.Columns[3].HeaderText = "KS/Fyzický stav";
                    dataGridView1.Columns[4].HeaderText = "Poznámka";
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    labelView.Text = "Pracovníci provozu - Zapůjčení nářadí";
                    Application.DoEvents();

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka naradi nelze otevřít.");
                }
                finally
                {
                    //   myDB.closeDB();
                }
            }


        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonOK.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();

        }
    }
}
