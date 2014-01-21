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
    public partial class SeznamUzivatelu : Form
    {
        private vDatabase myDataBase;

        public SeznamUzivatelu(vDatabase myDataBase)
        {
            InitializeComponent();

            labelView.Text = "";

            this.myDataBase = myDataBase;
            InitializeComponent();
            labelView.Text = "";

            // nastaveni gridView

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
                    labelView.Text = "Uživatelé - Načítání";
                    Application.DoEvents();

                    dataGridView1.DataSource = myDataBase.loadDataTableUzivatele();

                    dataGridView1.Columns["jmeno"].HeaderText = "Jméno";
                    dataGridView1.Columns["prijmeni"].HeaderText = "Název";
                    dataGridView1.Columns["user"].HeaderText = "Označení JK";
                    dataGridView1.Columns["admin"].HeaderText = "Rozměr";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    labelView.Text = "Uživatelé výdejny";
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

        private void seznamUzivatelu_Shown(object sender, EventArgs e)
        {
            loadData(myDataBase);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void přidatPoložkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // pridani polozky
        }




    }
}
