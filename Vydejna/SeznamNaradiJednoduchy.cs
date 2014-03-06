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
    public partial class SeznamNaradiJednoduchy : Form
    {

        private enum evenStateEnum {enable, disable};

        private vDatabase myDataBase;

        private evenStateEnum evenState = evenStateEnum.disable;

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



        public SeznamNaradiJednoduchy(vDatabase myDataBase, Font myFont)
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
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;



            Application.DoEvents();
            this.Font = myFont;

            Size size = ConfigReg.loadSettingWindowSize("LISTN");
            if (!(size.IsEmpty)) this.Size = size;

            Point location = ConfigReg.loadSettingWindowLocation("LISTN");
            if (!(location.IsEmpty)) this.SetDesktopLocation(location.X, location.Y);
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

                    dataGridView1.Columns["poradi"].HeaderText = "Pořadí";
                    dataGridView1.Columns["nazev"].HeaderText = "Název";
                    dataGridView1.Columns["jk"].HeaderText = "Označení JK";
                    dataGridView1.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridView1.Columns["fyzstav"].HeaderText = "KS/Fyzický stav";
                    dataGridView1.Columns["poznamka"].HeaderText = "Poznámka";
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

                prepravka = new messager( Convert.ToInt32( myRow.Cells["poradi"].Value), Convert.ToString( myRow.Cells["nazev"].Value),
                                          Convert.ToString( myRow.Cells["jk"].Value), Convert.ToString( myRow.Cells["rozmer"].Value),  
                                          Convert.ToInt32(myRow.Cells["fyzstav"].Value), Convert.ToDouble(myRow.Cells["cena"].Value));
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
            setColumnIndex();
            setColumnWidth();
            evenState = evenStateEnum.enable;
        }

        private void SeznamNaradiJednoduchy_LocationChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Location.IsEmpty)) ConfigReg.saveSettingWindowLocationSize("LISTN", this.Location.X, this.Location.Y, 0, 0);
            }
        }

        private void SeznamNaradiJednoduchy_SizeChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize("LISTN", 0, 0, this.Size.Width, this.Size.Height);
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnWidth("LISTN", "naradi", e.Column.Name, e.Column.Width);
            }
        }

        private void dataGridView1_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnIndex("LISTN", "naradi", e.Column.Name, e.Column.DisplayIndex);
            }

        }

        public virtual void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth("LISTN", "naradi");
            if (DBTableInfo != null)
            {
                foreach (DataGridViewColumn myColumn in dataGridView1.Columns)
                {
                    string myColumnName = myColumn.Name;
                    if (DBTableInfo.ContainsKey(myColumnName))
                    {
                        myColumn.Width = Convert.ToInt32(DBTableInfo[myColumnName]);
                    }

                }
            }
        }


        public virtual void setColumnIndex()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnIndex("LISTN", "naradi");
            if (DBTableInfo != null)
            {
                SortedDictionary<int, string> dict = new SortedDictionary<int, string>();

                // naplnime setrideny seznam  
                foreach (DictionaryEntry item in DBTableInfo)
                {
                    if (!(dict.ContainsKey(Convert.ToInt32(item.Value))))
                    {
                        dict.Add(Convert.ToInt32(item.Value), Convert.ToString(item.Key));
                    }
                }
                // upravime index podle setrideneho seznamu
                foreach (var sortItem in dict)
                {
                    try
                    {
                        dataGridView1.Columns[sortItem.Value.ToString()].DisplayIndex = Convert.ToInt32(sortItem.Key);
                    }
                    catch { }
                }
            }
        }


    }
}
