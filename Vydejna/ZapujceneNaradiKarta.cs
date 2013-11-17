﻿using System;
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
        Hashtable DBRow;
        private string osCislo;

        public ZapujceneNaradiKarta(Hashtable DBRow, vDatabase myDataBase)
        {
            myDB = myDataBase;
            this.DBRow = DBRow;
            InitializeComponent();
            setData(DBRow);

            dataGridViewZmeny.MultiSelect = false;
            dataGridViewZmeny.ReadOnly = true;
            dataGridViewZmeny.RowHeadersVisible = false;
            dataGridViewZmeny.AllowUserToAddRows = false;
            dataGridViewZmeny.AllowUserToResizeRows = false;

            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            loadVypujceneItems();
        }

        private void ZapujceneNaradiKarta_Load(object sender, EventArgs e)
        {

        }


        public void setData(Hashtable DBRow)
        {
            osCislo = Convert.ToString(DBRow["oscislo"]).Trim();
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
        }

        private void zapůjčeníNářadíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zobrazime seznam polozek naradi
            SeznamNaradiJednoduchy seznamNar = new SeznamNaradiJednoduchy(myDB);
            seznamNar.Visible = false;   // formular se automaticky presune do show musime tedy ho vypnout
            if (seznamNar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SeznamNaradiJednoduchy.messager myMesenger = seznamNar.getMesseger();
                ZapujceniNaradi zapujcNaradi = new ZapujceniNaradi(DBRow,myMesenger.nazev, myMesenger.jk, myMesenger.fyzStav);


                if (zapujcNaradi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // pridame zapujcene naradi
                    if (myDB.addNewLineZmeny(myMesenger.poradi, myMesenger.jk, zapujcNaradi.getDatum(), 0, zapujcNaradi.getKs(), zapujcNaradi.getPoznamka(), "U", 0, zapujcNaradi.getKs(), osCislo) < 0)
                    {
                        MessageBox.Show("Vypůjčeni nářadi se nezdařilo. Lituji.");
                    }
                    else 
                    {
                        // prodame do  formulare // 
                        (dataGridViewZmeny.DataSource as DataTable).Rows.Add(zapujcNaradi.getDatum(),myMesenger.nazev, myMesenger.rozmer, myMesenger.jk, zapujcNaradi.getKs(),zapujcNaradi.getPoznamka());
                        int counter = dataGridViewZmeny.Rows.Count - 1;

                        dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[counter].Index;
                        dataGridViewZmeny.Refresh();

                        dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[counter].Cells[1];
                        dataGridViewZmeny.Rows[counter].Selected = true;
                    }


                }



            }
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


    }
}
