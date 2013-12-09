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
                    dataGridViewZmeny.DataSource = myDB.loadDataTableVypujcenoNaOsobuNext(labelOsCislo.Text);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns[0].HeaderText = "Poradi";
                    dataGridViewZmeny.Columns[1].HeaderText = "Datum";
                    dataGridViewZmeny.Columns[2].HeaderText = "Název";
                    dataGridViewZmeny.Columns[3].HeaderText = "Rozměr";
                    dataGridViewZmeny.Columns[4].HeaderText = "JK";
                    dataGridViewZmeny.Columns[5].HeaderText = "KS";
                    dataGridViewZmeny.Columns[6].HeaderText = "Cena";
                    dataGridViewZmeny.Columns[7].HeaderText = "Poznámka";
                    dataGridViewZmeny.Columns["poradi"].Visible = false;
                    dataGridViewZmeny.Columns["oscislo"].Visible = false;  
                    dataGridViewZmeny.Columns["pjmeno"].Visible = false;  
                    dataGridViewZmeny.Columns["pprijmeni"].Visible = false;
                    dataGridViewZmeny.Columns["pnazev"].Visible = false;
                    dataGridViewZmeny.Columns["pjk"].Visible = false;
                    dataGridViewZmeny.Columns["nporadi"].Visible = false;
                    dataGridViewZmeny.Columns["zporadi"].Visible = false;


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
            if (seznamNar != null)
            {
                seznamNar.Visible = false;   // formular se automaticky presune do show musime tedy ho vypnout
                if (seznamNar != null)
                {
                    try  // protoze konstruktor saznam naradi jednoduchy -  pracuje dlouho s natahovabim polozek - uzivatel jem muze prerusit a tim dojde k odstraneni objektu musime tedy testovat existenci objektu
                    {
                        if (seznamNar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            SeznamNaradiJednoduchy.messager myMesenger = seznamNar.getMesseger();
                            ZapujceniNaradi zapujcNaradi = new ZapujceniNaradi(DBRow, myMesenger.nazev, myMesenger.jk, myMesenger.fyzStav);

                                if (zapujcNaradi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    // pridame zapujcene naradi
                                    int pujcPradi;
//                                    if (myDB.addNewLineZmeny(myMesenger.poradi, myMesenger.jk, zapujcNaradi.getDatum(), 0, zapujcNaradi.getKs(), zapujcNaradi.getPoznamka(), "U", 0, zapujcNaradi.getKs(), osCislo) < 0)
                                    if ((pujcPradi = myDB.addNewLineZmenyAndPujceno(myMesenger.poradi, myMesenger.jk, zapujcNaradi.getDatum(), zapujcNaradi.getKs(), zapujcNaradi.getPoznamka(), 
                                                                       osCislo, labelJmeno.Text,labelPrijmeni.Text, myMesenger.nazev,myMesenger.cena)) < 0)
                                        {
                                            if (pujcPradi == -2) MessageBox.Show("Není možno vypůjčit více kusů než je stav na výdejně. Lituji.");
                                        else MessageBox.Show("Vypůjčeni nářadi se nezdařilo. Lituji.");
                                    }
                                    else
                                    {
                                        // prodame do  formulare // 
                                        Hashtable DBPujcRow = new Hashtable();
                                        Int32 zporadi = 0;
                                        if (myDB.getPujcenoLine(pujcPradi, DBPujcRow) != null)
                                        {
                                            if (DBPujcRow.Contains("zporadi"))
                                            {
                                                zporadi = Convert.ToInt32(DBPujcRow["zporadi"]);
                                            }
                                        }


                                        (dataGridViewZmeny.DataSource as DataTable).Rows.Add(pujcPradi, zapujcNaradi.getDatum(), myMesenger.nazev, myMesenger.rozmer, myMesenger.jk, zapujcNaradi.getKs(), myMesenger.cena,
                                                                                             zapujcNaradi.getPoznamka(), zapujcNaradi.getOsCiclo(), zapujcNaradi.getJmeno(), myMesenger.nazev, myMesenger.jk,myMesenger.poradi,zporadi);
                                        int counter = dataGridViewZmeny.Rows.Count - 1;

                                        dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[counter].Index;
                                        dataGridViewZmeny.Refresh();

                                        dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[counter].Cells[1];
                                        dataGridViewZmeny.Rows[counter].Selected = true;
                                    }
                                }
                            }
                    } catch {};
                }
            }            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


    }
}
