﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class OpravaKarta : Form
    {

        private Font parentFont;
        private string formName = "CRCARD";
        private vDatabase myDB;
        private Int32 poradi;


        public OpravaKarta(vDatabase myDataBase, Hashtable DBRow, Int32 poradi, Font myFont)
        {
            InitializeComponent();

            this.poradi = poradi;

            // jak menit meritko
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            parentFont = myFont;
//            setFont(myFont);

            myDB = myDataBase;

            dataGridViewZmeny.MultiSelect = false;
            dataGridViewZmeny.ReadOnly = true;
            dataGridViewZmeny.RowHeadersVisible = false;
            dataGridViewZmeny.AllowUserToAddRows = false;
            dataGridViewZmeny.AllowUserToDeleteRows = false;
            dataGridViewZmeny.AllowUserToResizeRows = false;
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            setData(DBRow);
            loadZmenyItems();
            this.CancelButton = this.buttonCancel;

            recountData();
        }

        private void loadZmenyItems()
        {
            Application.DoEvents();
            dataGridViewZmeny.Columns.Clear();
            dataGridViewZmeny.DataSource = null;
            Application.DoEvents();
            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridViewZmeny.DataSource = myDB.loadDataTableZmeny(poradi);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns["datum"].HeaderText = "Datum";
                    dataGridViewZmeny.Columns["stav"].HeaderText = "Operace";
                    dataGridViewZmeny.Columns["poznamka"].HeaderText = "Poznamka";
                    dataGridViewZmeny.Columns["prijem"].HeaderText = "Příjem";
                    dataGridViewZmeny.Columns["vydej"].HeaderText = "Výdej";
                    dataGridViewZmeny.Columns["zustatek"].HeaderText = "Stav";
                    dataGridViewZmeny.Columns["zapkarta"].HeaderText = "Zapůjčeno na kartu";

                    dataGridViewZmeny.Columns["poradi"].Visible = false;
                    dataGridViewZmeny.Columns["vevcislo"].Visible = false;

                    dataGridViewZmeny.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    for (int i = 0; i < dataGridViewZmeny.Columns.Count; i++)
                    {
                        dataGridViewZmeny.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    // pridame sloupec
                    dataGridViewZmeny.Columns.Add("novystav", "Nový stav");

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

        private void setData(Hashtable DBRow)
        {
            labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            labelJK.Text = Convert.ToString(DBRow["jk"]);

            numericUpDownUcetStav.Value = Convert.ToInt32(DBRow["ucetstav"]);
            labelUcetStav.Text = Convert.ToString(DBRow["ucetstav"]);

            numericUpDownFyzStav.Value = Convert.ToDecimal(DBRow["fyzstav"]);
            labelFyzStav.Text = Convert.ToString(DBRow["fyzstav"]);


        }

        private void recountData()
        {

            Int32 dtCount = (dataGridViewZmeny.DataSource as DataTable).Rows.Count;
            decimal novyZustatek = numericUpDownStartStav.Value;
            for (Int32 i = 0; i < dtCount; i++)
            {
                DataRow dr = (dataGridViewZmeny.DataSource as DataTable).Rows[i];
                Int32 prijem = Convert.ToInt32( dr["prijem"]);
                Int32 vydej = Convert.ToInt32(dr["vydej"]);
                Int32 zustatek = Convert.ToInt32(dr["zustatek"]);
                novyZustatek = novyZustatek + prijem - vydej;
                dr.SetField("novystav", novyZustatek);

            }
        }

    }
}
