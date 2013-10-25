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

    public partial class VyberRadku : Form
    {
        public vDatabase myDataBase;
        
        public VyberRadku(vDatabase myDataBase)
        {
            this.myDataBase = myDataBase;

            InitializeComponent();

            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;

            dataGridView2.ContextMenuStrip = contextMenuStrip1;

            Application.DoEvents();
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = null;
            Application.DoEvents();

            if (myDataBase.DBIsOpened())
            {
                try
                {
                    //myDB.loadDataTableOsoby(myDataTable);
                    dataGridView2.DataSource = myDataBase.loadDataTableOsoby();
                    dataGridView2.RowHeadersVisible = false;


                    dataGridView2.Columns[0].HeaderText = "Přijmení";
                    dataGridView2.Columns[1].HeaderText = "Jméno";
                    dataGridView2.Columns[2].HeaderText = "Osobní číslo";
                    dataGridView2.Columns[3].HeaderText = "Provoz";
                    dataGridView2.Columns[4].HeaderText = "Středisko";
                    dataGridView2.Columns[5].HeaderText = "Pracovište";
                    dataGridView2.Columns[6].HeaderText = "Číslo znamky";
                    dataGridView2.Columns[7].HeaderText = "Ulice";
                    dataGridView2.Columns[8].HeaderText = "PSČ";
                    dataGridView2.Columns[9].HeaderText = "Město";
                    dataGridView2.Columns[10].HeaderText = "Tel. domů";
                    dataGridView2.Columns[11].HeaderText = "Tel. zaměst.";
                    dataGridView2.Columns[12].HeaderText = "Poznamka";
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulku pracovníků provozu nelze otevřít.");
                }
                finally
                {
                    //    myDB.closeDB();
                }
            }


        }


        public Hashtable getDBRowFromSelectedRow(Hashtable newDBRow)
        {
            if (newDBRow == null)
            {
                newDBRow = new Hashtable();
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {

                DataGridViewRow myRow = dataGridView2.SelectedRows[0];

                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                {
                    if (newDBRow.ContainsKey(dataGridView2.Columns[i].Name))
                    {
                        newDBRow.Remove(dataGridView2.Columns[i].Name);
                    }
                    newDBRow.Add(dataGridView2.Columns[i].Name, myRow.Cells[i].Value);
                }
            }
            return newDBRow;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonOK.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonOK.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private void přidatPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(myDataBase);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {

                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                    Int32 stav = myDataBase.addNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto, mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko, mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (stav != -1)
                    {
                        (dataGridView2.DataSource as DataTable).Rows.Add(mesenger.prijmeni, mesenger.jmeno, mesenger.oscislo, mesenger.oddeleni, mesenger.stredisko, mesenger.pracoviste, mesenger.cisZnamky, mesenger.ulice, mesenger.psc, mesenger.mesto, mesenger.telHome, mesenger.telZam, mesenger.poznamka);
                        int counter = dataGridView2.Rows.Count - 1;

                        dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows[counter].Index;
                        dataGridView2.Refresh();
                        dataGridView2.CurrentCell = dataGridView2.Rows[counter].Cells[1];
                        dataGridView2.Rows[counter].Selected = true;
                    }

                }
            }



        }

        private void opravitPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Hashtable DBRow = getDBRowFromSelectedRow(null);

            PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDataBase, uKartaState.edit);
            if (pracKarta.ShowDialog() == DialogResult.OK)
            {
                PracovniciKarta.messager mesenger = pracKarta.getMesseger();


                Boolean updateIsOk = myDataBase.editNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto,
                                             mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko,
                                             mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                if (updateIsOk)
                {
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = -1;
                    for (int x = 0; x < (dataGridView2.DataSource as DataTable).Rows.Count - 1; x++)
                    {
                        if (Convert.ToString((dataGridView2.DataSource as DataTable).Rows[x][2]) == mesenger.oscislo)
                        {
                            dataRowIndex = x;
                            break;
                        }

                    }

                    if (dataRowIndex > -1)
                    {
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(0, mesenger.prijmeni);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(1, mesenger.jmeno);

                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.oscislo);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.oddeleni);

                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.stredisko);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.pracoviste);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.cisZnamky);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.ulice);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.psc);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(9, mesenger.mesto);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.telHome);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.telZam);
                        (dataGridView2.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.poznamka);

                        dataGridView2.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Nepodařilo se opravit záznam. Lituji.");
                    }
                }


            }

        }
    }
}
