using System;
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
    public partial class SeznamUzivatelu : Form
    {
        private vDatabase myDataBase;

        public SeznamUzivatelu(vDatabase myDataBase)
        {
            this.myDataBase = myDataBase;
            InitializeComponent();
            labelView.Text = "";

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

//            loadData(myDataBase);
        }

        private void loadData(vDatabase myDataBase)
        {
            if (myDataBase.DBIsOpened())
            {
                try
                {
                    labelView.Text = "Seznam uživatelů - Načítání";
                    Application.DoEvents();

                    dataGridView1.DataSource = myDataBase.loadDataTableUzivatele();

                    dataGridView1.Columns["userid"].HeaderText = "Uživatelské jméno";
                    dataGridView1.Columns["jmeno"].HeaderText = "Jméno";
                    dataGridView1.Columns["prijmeni"].HeaderText = "Přijmení";
                    dataGridView1.Columns["admin"].HeaderText = "Administratorský ůčet";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    labelView.Text = "Seznam uživatelů";
                    Application.DoEvents();

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka uživatelů nelze otevřít.");
                }
                finally
                {
                    //   myDB.closeDB();
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SeznamUzivatelu_Shown(object sender, EventArgs e)
        {
            loadData(myDataBase);

        }

        private void přidatPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // pridani polozky

            UzivatelKarta uk = new UzivatelKarta(myDataBase, false);
            if (uk.ShowDialog() == DialogResult.OK)
            {
                string userID = uk.getUserID();
                Hashtable userList = myDataBase.getUzivateleLine(userID, null);
                if (userList != null)
                {
                    DataRow newRow = (dataGridView1.DataSource as DataTable).NewRow();
                    newRow["userid"] = userID;
                    newRow["jmeno"] = Convert.ToString(userList["jmeno"]);
                    newRow["prijmeni"] = Convert.ToString(userList["prijmeni"]);
                    newRow["admin"] = Convert.ToString(userList["admin"]);

                    (dataGridView1.DataSource as DataTable).Rows.Add(newRow);
                    //
                    int counter = dataGridView1.Rows.Count - 1;

                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[counter].Index;
                    dataGridView1.Refresh();

                    dataGridView1.CurrentCell = dataGridView1.Rows[counter].Cells[1];
                    dataGridView1.Rows[counter].Selected = true;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void smazatPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
              string userid = Convert.ToString( dataGridView1.SelectedRows[0].Cells["userid"].Value);
              Int32 errCode =  myDataBase.deleteLineUzivatele( userid);
              if (errCode == -2)
              {
                  MessageBox.Show("Lituji, uživatelské jméno neexistuje.");
              }
              if (errCode == -1)
              {
                  MessageBox.Show("Lituji, uživatele se nopadřilo odtranit.");
              }
              if (errCode == 0)
              {
                  Int32 dataIndex = detail.findIndex((dataGridView1.DataSource as DataTable), "userid", userid);
                  (dataGridView1.DataSource as DataTable).Rows.RemoveAt(dataIndex);
              }
            }
        }

        private void opravitPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opraveni polozky

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string userid =  Convert.ToString( dataGridView1.SelectedRows[0].Cells["userid"].Value);
                Hashtable DBRow =  myDataBase.getUzivateleLine(userid,null);

                if (DBRow != null)
                {
                    UzivatelKarta uk = new UzivatelKarta(myDataBase, DBRow);
                    if (uk.ShowDialog() == DialogResult.OK)
                    {
                        // zobrazime vysledek
                        Int32 index = detail.findIndex(dataGridView1.DataSource as DataTable, "userid", userid);
                        DBRow = myDataBase.getUzivateleLine(userid, DBRow);
                        (dataGridView1.DataSource as DataTable).Rows[index]["jmeno"] = Convert.ToString(DBRow["jmeno"]);
                        (dataGridView1.DataSource as DataTable).Rows[index]["prijmeni"] = Convert.ToString(DBRow["prijmeni"]);
                        (dataGridView1.DataSource as DataTable).Rows[index]["admin"] = Convert.ToString(DBRow["admin"]);
                    }
                }
                else
                {
                    MessageBox.Show("Lituji. Uživatel již neexistuje");
                }
            }            

    


        }


    }
}
