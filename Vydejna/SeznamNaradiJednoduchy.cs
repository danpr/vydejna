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
        private vDatabase myDataBase;


        public class messager
        {
            public Int32 poradi;
            public string nazev;
            public string jk;
            public Int32 fyzStav; //cena
            public string rozmer;
            public double cena;


            public messager(Int32 poradi, string nazev, string jk, string rozmer, Int32 fyzStav, double cena)
            {
                this.poradi = poradi;
                this.nazev = nazev;
                this.jk = jk;
                this.fyzStav = fyzStav;
                this.rozmer = rozmer;
                this.cena = cena;
            }
        }



        public SeznamNaradiJednoduchy(vDatabase myDataBase)
        {
            this.myDataBase = myDataBase;
            InitializeComponent();
            labelView.Text = "";

            // nastaveni gridView

            AcceptButton = buttonOK;
            CancelButton = buttonCancel;

            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

        }

        private void loadData(vDatabase myDataBase)
        {
            if (myDataBase.DBIsOpened())
            {
                try
                {
                    labelView.Text = "Seznam nářadí - Načítání";
                    Application.DoEvents();

                    dataGridView1.DataSource = myDataBase.loadDataTableNaradiJednoduchy();

                    dataGridView1.Columns[0].HeaderText = "Pořadí";
                    dataGridView1.Columns[1].HeaderText = "Název";
                    dataGridView1.Columns[2].HeaderText = "Označení JK";
                    dataGridView1.Columns[3].HeaderText = "Rozměr";
                    dataGridView1.Columns[4].HeaderText = "KS/Fyzický stav";
                    dataGridView1.Columns[5].HeaderText = "Poznámka";
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme
                    dataGridView1.Columns["cena"].Visible = false;   // cenu nezobrazujeme
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


        public messager getMesseger()
        {
            messager prepravka;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow myRow = dataGridView1.SelectedRows[0];

                prepravka = new messager( Convert.ToInt32( myRow.Cells[0].Value), Convert.ToString( myRow.Cells[1].Value),Convert.ToString( myRow.Cells[2].Value), Convert.ToString( myRow.Cells[2].Value),  Convert.ToInt32(myRow.Cells[4].Value), Convert.ToDouble(myRow.Cells[6].Value));
            }
            else
            {
                prepravka = new messager(-1,"", "", "", 0, 0);
            }
            return prepravka;
        }

        private void SeznamNaradiJednoduchy_Shown(object sender, EventArgs e)
        {
            // pozobrazeni
            loadData(myDataBase);
        }


    }
}
