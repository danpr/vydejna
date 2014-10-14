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
    public partial class OpravaKarta : Form
    {

        private enum evenStateEnum { enable, disable };
        private evenStateEnum evenState = evenStateEnum.disable;

        private Font parentFont;
        private string formName = "CRCARD";
        private vDatabase myDB;
        private Int32 poradi;
        private Font italicFont;


        public OpravaKarta(vDatabase myDataBase, Hashtable DBRow, Int32 poradi, Font myFont)
        {
            InitializeComponent();

            this.poradi = poradi;

            // jak menit meritko
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            parentFont = myFont;
//          setFont(myFont);
            italicFont = new Font(myFont, FontStyle.Italic);
//            this.Font = myFont;

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

            // zmeny barev v datagiridview je nutno udelat az casti load protoze konstruktor pri zobrazeni nastavi standartni hodnoty
            //recountData();
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

                    DataTable dt = (dataGridViewZmeny.DataSource as DataTable);
                    DataColumnCollection dcc = dt.Columns;
                    dcc.Add("novystav", System.Type.GetType("System.Decimal"));
                    dataGridViewZmeny.Columns["novystav"].HeaderText = "Nový stav";

                    dcc.Add("rozdil", System.Type.GetType("System.Decimal"));
                    dataGridViewZmeny.Columns["rozdil"].HeaderText = "Rozdíl";


                    // nastavime na posledni radku
                    int counter = dataGridViewZmeny.Rows.Count - 1;
                    //                    if (dataGridViewZmeny.Rows.Count > 0)
                    if (counter > 0)
                    {
                        dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[counter].Index;
                        dataGridViewZmeny.Refresh();
                        dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[counter].Cells[1];
                        dataGridViewZmeny.Rows[counter].Selected = true;
                    }


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
            decimal puvodniZustatek = numericUpDownStartStav.Value;
            decimal novyUcetStav = numericUpDownStartStav.Value;
            decimal novyFyzStav = numericUpDownStartStav.Value;
            for (Int32 i = 0; i < dtCount; i++)
            {
                DataRow dr = (dataGridViewZmeny.DataSource as DataTable).Rows[i];
                Int32 prijem = Convert.ToInt32( dr["prijem"]);
                Int32 vydej = Convert.ToInt32(dr["vydej"]);
                Int32 zustatek = Convert.ToInt32(dr["zustatek"]);
                string stavKod = Convert.ToString(dr["stavkod"]);
                novyZustatek = novyZustatek + prijem - vydej;
                novyFyzStav = novyFyzStav + prijem - vydej;
                // U - pujceno R - vraceno
                if ((stavKod != "U") && (stavKod != "R"))
                {
                    novyUcetStav = novyUcetStav + prijem - vydej;
                }

                dr.SetField("novystav", novyZustatek);
                dr.SetField("rozdil", puvodniZustatek + prijem - vydej - zustatek);

                puvodniZustatek = zustatek;
            }
            if (dtCount > 0) // existuji zaznamy o zmenach
            {
                if (novyFyzStav < 0)
                {
                    numericUpDownFyzStav.Minimum = novyFyzStav;
                    MessageBox.Show("Pozor FYZICKÝ stav je menši než nula je potřeba opravit hodnoty stavů.");
                }
                if (novyUcetStav < 0)
                {
                    numericUpDownUcetStav.Minimum = novyUcetStav;
                    MessageBox.Show("Pozor ÚČETNÍ stav je menši než nula je potřeba opravit hodnoty stavů.");
                }
                numericUpDownFyzStav.Value = novyFyzStav;
                numericUpDownUcetStav.Value = novyUcetStav;
                setZmenyColor();
            }
        }

        private void numericUpDownStartStav_ValueChanged(object sender, EventArgs e)
        {
            recountData();
            setStartStavColor();
        }

        private void setUcetStavColor()
        {
            Int32 labelUcetStavInt = 0;
            try
            {
                labelUcetStavInt = Convert.ToInt32(labelUcetStav.Text);

                if (numericUpDownUcetStav.Value != labelUcetStavInt)
                {
                    labelUcetStav.ForeColor = Color.Red;
                    numericUpDownUcetStav.ForeColor = Color.Red;
                }
                else
                {
                    labelUcetStav.ForeColor = SystemColors.ControlText;
                    numericUpDownUcetStav.ForeColor = SystemColors.ControlText;
                }

            }
            catch
            {
                labelUcetStav.ForeColor = SystemColors.ControlText;
                numericUpDownUcetStav.ForeColor = SystemColors.ControlText;
            }
        }


        private void setFyzStavColor()
        {
            Int32 labelFyzStavInt = 0;
            try
            {
                labelFyzStavInt = Convert.ToInt32(labelFyzStav.Text);

                if (numericUpDownFyzStav.Value != labelFyzStavInt)
                {
                    labelFyzStav.ForeColor = Color.Red;
                    numericUpDownFyzStav.ForeColor = Color.Red;

                }
                else
                {
                    labelFyzStav.ForeColor = SystemColors.ControlText;
                    numericUpDownFyzStav.ForeColor = SystemColors.ControlText;
                }

            }
            catch
            {
                labelFyzStav.ForeColor = SystemColors.ControlText;
                numericUpDownFyzStav.ForeColor = SystemColors.ControlText;
            }
        }


        private void setStartStavColor()
        {
            if (numericUpDownStartStav.Value != 0)
            {
                numericUpDownStartStav.ForeColor = Color.Red;
            }
            else
            {
                numericUpDownStartStav.ForeColor = SystemColors.ControlText;
             }
         }


        private void setZmenyColor()
        {
            foreach (DataGridViewRow row in dataGridViewZmeny.Rows)
            {
                if (row.Cells["zustatek"].Value != null && ( Convert.ToInt32( row.Cells["zustatek"].Value) !=  Convert.ToInt32( row.Cells["novystav"].Value )))
                {
                    if ((row.Cells["rozdil"].Value != null) && (Convert.ToInt32(row.Cells["rozdil"].Value) != 0))
                    {
                        row.DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                else
                {
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
                }

                if ((row.Cells["rozdil"].Value != null) && (Convert.ToInt32(row.Cells["rozdil"].Value) != 0))
                {
                    row.DefaultCellStyle.Font = italicFont;
                }
                else
                {
                    if ((row.DefaultCellStyle.Font != null) && (row.DefaultCellStyle.Font.Italic == true))
                    {
                        row.DefaultCellStyle.Font = parentFont;
                    }
                }



            }
        }

        private void numericUpDownUcetStav_ValueChanged(object sender, EventArgs e)
        {
            setUcetStavColor();
        }



        private void labelUcetStav_TextChanged(object sender, EventArgs e)
        {
            setUcetStavColor();
        }

        private void numericUpDownFyzStav_ValueChanged(object sender, EventArgs e)
        {
            setFyzStavColor();
        }

        private void labelFyzStav_TextChanged(object sender, EventArgs e)
        {
            setFyzStavColor();
        }


        private Int32 zmenyLineCount
        {
            get { return dataGridViewZmeny.Rows.Count; }
        }


        public Int32 fyzStav
        {
            get { return Convert.ToInt32(numericUpDownFyzStav.Value); }
        }


        public Int32 oldFyzStav
        {
            get { return Convert.ToInt32(labelFyzStav.Text); }
        }



        public Int32 ucetStav
        {
            get { return Convert.ToInt32(numericUpDownUcetStav.Value); }
        }


        public Int32 oldUcetStav
        {
            get { return Convert.ToInt32(labelUcetStav.Text); }
        }


        public zmenyCorrectLine[] getZmenyTab()
        {
            zmenyCorrectLine[] newZmeny;
            newZmeny = new zmenyCorrectLine[zmenyLineCount];
            for (Int32 i = 0; i < zmenyLineCount; i++)
            {
                //naplnime data
                DataGridViewRow newRow = dataGridViewZmeny.Rows[i];
                newZmeny[i].poradi = Convert.ToInt32(newRow.Cells["poradi"].Value);
                newZmeny[i].prijem = Convert.ToInt32(newRow.Cells["prijem"].Value);
                newZmeny[i].vydej = Convert.ToInt32(newRow.Cells["vydej"].Value);
                newZmeny[i].zustatek = Convert.ToInt32(newRow.Cells["zustatek"].Value);
                newZmeny[i].stavcod = Convert.ToString(newRow.Cells["stavkod"].Value);
                newZmeny[i].novyZustatek = Convert.ToInt32(newRow.Cells["novystav"].Value);
            }
            return newZmeny;
        }

        private void OpravaKarta_Load(object sender, EventArgs e)
        {
            setFont(parentFont);
            setGeometry();
            setColumnWidth();
            recountData();
        }

        private void setFont(Font myFont)
        {
            Font loadFont = ConfigReg.loadSettingFontX(formName);
            if (loadFont != null)
            {
                this.Font = loadFont;
            }
            else
            {
                this.Font = myFont;
            }
        }

        private void setAppFont()
        {
            this.Font = parentFont;
            ConfigReg.deleteSettingFontX(formName);
        }

        private void chooseFont()
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.Font = this.Font;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigReg.saveSettingFontX(fontDialog1.Font, formName);
                this.Font = fontDialog1.Font;
            }
        }

        private void setGeometry()
        {
            Size size = ConfigReg.loadSettingWindowSize(formName);
            if (!(size.IsEmpty)) this.Size = size;
            Point location = ConfigReg.loadSettingWindowLocation(formName);

            Int32 x = location.X;
            Int32 y = location.Y;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > (Screen.PrimaryScreen.Bounds.Width - 20)) x = Screen.PrimaryScreen.Bounds.Width - 20;
            if (y > (Screen.PrimaryScreen.Bounds.Height - 20)) y = Screen.PrimaryScreen.Bounds.Height - 20;

            if (!(location.IsEmpty))
            {
                StartPosition = FormStartPosition.Manual;
                this.SetDesktopLocation(x, y);
            }
            else
            {
                StartPosition = FormStartPosition.WindowsDefaultLocation;
            }

        }


        public virtual void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth(formName, "zmeny");
            if (DBTableInfo != null)
            {
                Int32 columnWidth = 0;
                for (Int32 i = 0; i < dataGridViewZmeny.Columns.Count; i++)
                {
                    string myColumnName = dataGridViewZmeny.Columns[i].Name;
                    if (DBTableInfo.ContainsKey(myColumnName))
                    {
                        columnWidth = Convert.ToInt32(DBTableInfo[myColumnName]);
                        try
                        {
                            dataGridViewZmeny.Columns[i].Width = columnWidth;
                        }
                        catch { }
                    }
                }
            }
        }


        private void OpravaKarta_SizeChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, 0, 0, this.Size.Width, this.Size.Height);
            }

        }

        private void OpravaKarta_LocationChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Location.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, this.Location.X, this.Location.Y, 0, 0);
            }

        }

        private void OpravaKarta_Shown(object sender, EventArgs e)
        {
            evenState = evenStateEnum.enable;

        }

        private void vybratPísmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chooseFont();
        }

        private void písmoAplikaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAppFont();
        }

        private void dataGridViewZmeny_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnIndex(formName, "zmeny", e.Column.Name, e.Column.DisplayIndex);
            }
        }

        private void dataGridViewZmeny_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnWidth(formName, "zmeny", e.Column.Name, e.Column.Width);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if ((numericUpDownUcetStav.Value < 0) || (numericUpDownFyzStav.Value < 0))
            {
                MessageBox.Show("Fyzický a účetní stav musí být nezáporné hodnoty.");
                buttonOK.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }




    }
}
