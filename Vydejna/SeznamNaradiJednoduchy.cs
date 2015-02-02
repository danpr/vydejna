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


        private Prohledavani searchWindow;
        private Int32 dataRowSearchSelectedIndex = -1;


        public SeznamNaradiJednoduchy(vDatabase myDataBase, Font myFont)
        {
            this.myDataBase = myDataBase;
            InitializeComponent();
            searchWindow = null;
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
                        try
                        {
                            myColumn.Width = Convert.ToInt32(DBTableInfo[myColumnName]);
                            myColumn.MinimumWidth = Convert.ToInt32(DBTableInfo[myColumnName]);
                        }
                        catch { }
                    }
                }
                foreach (DataGridViewColumn myColumn in dataGridView1.Columns)
                {
                    if (myColumn.Visible)
                    {
                        myColumn.MinimumWidth = 10;
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


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F3)
            {
                HledejDalsi(this.Location.Y + this.Size.Width, this.Top);

            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                NastaveniHledani(this.Location.X + this.Size.Width, this.Top);
            }
        
        }

        public virtual void NastaveniHledani(Int32 x, Int32 y)
        {
            if (searchWindow == null)
            {
                searchWindow = new Prohledavani(dataGridView1, "", "LISTN", "naradi");
                searchWindow.StartPosition = FormStartPosition.Manual;
            }

            if (x > (Screen.PrimaryScreen.Bounds.Width - searchWindow.Width))
            {
                x = Screen.PrimaryScreen.Bounds.Width - searchWindow.Width;
            }
            if (x < 0) { x = 0; }

            if (y > (Screen.PrimaryScreen.Bounds.Height - searchWindow.Height))
            {
                y = Screen.PrimaryScreen.Bounds.Height - searchWindow.Height;
            }
            if (y < 0) { y = 0; }
            searchWindow.SetDesktopLocation(x, y);
            searchWindow.ShowDialog();
        }

        public virtual void HledejDalsi(Int32 x, Int32 y)
        {
            if (searchWindow == null)
            {
                NastaveniHledani(x, y);
            }
            else
            {
                searchWindow.najdiRadku();
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataRowSearchSelectedIndex = -1;
            if (e.RowIndex == -1)
            {
                DataGridViewRow dataGridViewSelectedRow = dataGridView1.SelectedRows[0];
                if (dataGridViewSelectedRow != null)
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    dataRowSearchSelectedIndex = detail.findIndex(dt, dataGridViewSelectedRow);
                }
            }

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //trideni ukonceno
            if ((dataRowSearchSelectedIndex != -1) && e.ListChangedType == ListChangedType.Reset)
            {
                if (dataRowSearchSelectedIndex != -1)
                {   // zkusime smazat stary select 
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
                        if (dgvr != null)
                        {
                            Int32 indexDGV = dgvr.Index;
                            dataGridView1.Rows[indexDGV].Selected = false;
                        }
                    }

                    if (dataGridView1.Rows.Count > 0)
                    {
                        DataTable dt = (DataTable)dataGridView1.DataSource;
                        DataRowCollection drc = dt.Rows;
                        if (drc != null)
                        {
                            DataGridViewRow dgvrs = null;
                            for (Int32 i = 0; i < dataGridView1.Rows.Count - 1; i++)
                            {
                                dgvrs = dataGridView1.Rows[i];
                                DataRow row = ((DataRowView)dgvrs.DataBoundItem).Row;
                                if (dataRowSearchSelectedIndex == drc.IndexOf(row))
                                {
                                    // zavolame asynchrone presun na novy select
                                    dataGridView1.BeginInvoke((MethodInvoker)delegate()
                                    {
                                        dataGridView1.Rows[i].Selected = true;
                                        dataGridView1.CurrentCell = dataGridView1[1, i];
                                    });
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }



    }
}
