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
    public partial class VyberRadku : Form
    {
        public VyberRadku(vDatabase myDataBase)
        {
            InitializeComponent();

            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;


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
    }
}
