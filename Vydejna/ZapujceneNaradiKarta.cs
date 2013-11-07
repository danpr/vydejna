using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class ZapujceneNaradiKarta : Form
    {

        private vDatabase myDB;

        public ZapujceneNaradiKarta(Hashtable DBRow, vDatabase myDataBase)
        {
            myDB = myDataBase;
            InitializeComponent();
            setData(DBRow);
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            loadVypujceneItems();
        }

        private void ZapujceneNaradiKarta_Load(object sender, EventArgs e)
        {

        }


        public void setData(Hashtable DBRow)
        {
            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelOddeleni.Text = Convert.ToString(DBRow["odeleni"]);
            labelPracoviste.Text = Convert.ToString(DBRow["pracoviste"]);
            labelCisZnamky.Text = Convert.ToString(DBRow["cisznamky"]);

        }

        private void loadVypujceneItems()
        {
            Application.DoEvents();
            dataGridViewZmeny.Columns.Clear();
            dataGridViewZmeny.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridViewZmeny.DataSource = myDB.loadDataTableVypujcenoNaOsobu(labelOsCislo.Text);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns[0].HeaderText = "Datum";
                    dataGridViewZmeny.Columns[1].HeaderText = "Název";
                    dataGridViewZmeny.Columns[2].HeaderText = "Rozměr";
                    dataGridViewZmeny.Columns[3].HeaderText = "JK";
                    dataGridViewZmeny.Columns[4].HeaderText = "KS";
                    dataGridViewZmeny.Columns[5].HeaderText = "Poznámka";

                    dataGridViewZmeny.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka změn stavu nářadí nelze otevřít.");
                }
                finally
                {
                    //                    myDB.closeDB();
                }
            }
            //            myDB.myDBConn.Dispose();
        }


    }
}
