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
    public partial class SestavaDefault : Form
    {

        private enum evenStateEnum { enable, disable };
        private evenStateEnum evenState = evenStateEnum.disable;

        protected vDatabase myDataBase;
        protected ISestava1 strategie;

        public SestavaDefault(vDatabase myDataBase, ISestava1 strategie, Font myFont)
        {
            evenState = evenStateEnum.disable;
            InitializeComponent();

            // jak menit meritko
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Font = myFont;

            this.strategie = strategie;
            this.myDataBase = myDataBase;


            dataGridViewSestava.MultiSelect = false;
            dataGridViewSestava.ReadOnly = true;
            dataGridViewSestava.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSestava.RowHeadersVisible = false;
            dataGridViewSestava.AllowUserToAddRows = false;
            dataGridViewSestava.AllowUserToDeleteRows = false;
            dataGridViewSestava.AllowUserToResizeRows = false;
            dataGridViewSestava.AllowUserToOrderColumns = false;
            dataGridViewSestava.Columns.Clear();
            dataGridViewSestava.DataSource = null;

            this.Text = strategie.getWindowHeader();
            if (strategie.existTextVyber())
            {
                labelVyber.Text = strategie.getTextVyberLabel();
            }
            else
            {
                hideTextVyber();
            }
        }


        public virtual void loadData()
        {
            dataGridViewSestava.DataSource = strategie.loadDataTable(myDataBase, dateTimePickerFrom.Value, dateTimePickerTo.Value,textBoxVyber.Text);
            Hashtable headerRow = strategie.getHeaderLabels();
            foreach (DictionaryEntry pair in headerRow)
            {
                dataGridViewSestava.Columns[Convert.ToString( pair.Key)].HeaderText = Convert.ToString( pair.Value);
            }



        }

        protected void hideTextVyber()
        {
            labelVyber.Hide();
            labelVyber.Enabled = false;
            textBoxVyber.Hide();
            textBoxVyber.Enabled = false;
        }

        protected DateTime getDateFrom()
        {
            return dateTimePickerFrom.Value;
        }


        protected DateTime getDateTo()
        {
            return dateTimePickerTo.Value;
        }


        private void buttonRetry_Click(object sender, EventArgs e)
        {
            loadData();
            makeSum();
            strategie.makeSumProcent(dataGridViewSestava.DataSource as DataTable);
            setColumnWidth();
            evenState = evenStateEnum.enable; // povolime ukladat zmeny sloupcu

        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }

        private void textBoxVyber_TextChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }


        protected virtual void makeSum()
        {
            labelCelkem.Text = Convert.ToString(strategie.makeSum(dataGridViewSestava.DataSource as DataTable));
        }

        protected void makeSum(string column)
        {
            if (column.Trim() != "")
            {
                if ((dataGridViewSestava.DataSource as DataTable).Columns.Contains(column))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < (dataGridViewSestava.DataSource as DataTable).Rows.Count; x++)
                    {
                    suma = suma + Convert.ToDecimal((dataGridViewSestava.DataSource as DataTable).Rows[x][column]);

                    }
                    labelCelkem.Text = Convert.ToString(suma);
                }
            }
        }


        private void setHeaderLabels()
        {
          Hashtable headerLabels = strategie.getHeaderLabels();
          for (Int32 i = 0; i < headerLabels.Count; i++)
          {
//              DataGridView.Columns["poradi"].HeaderText = "Pořadí";

          }



        }

        private void dataGridViewSestava_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // ulozeni velikosti
            if (evenState == evenStateEnum.enable)
            {
                
                if (strategie.getNameStrategy() != "")
                {
                    ConfigReg.saveSettingWindowTableColumnWidth("REPORT", strategie.getNameStrategy(), e.Column.Name, e.Column.Width);
                }
            }

        }

        private void SestavaDefault_Load(object sender, EventArgs e)
        {
//            evenState = evenStateEnum.enable;
        }

        public void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth("REPORT", strategie.getNameStrategy());
            if (DBTableInfo != null)
            {
                for (Int32 i = 0; i < dataGridViewSestava.Columns.Count; i++)
                {
                    string myColumnName = dataGridViewSestava.Columns[i].Name;
                    if (DBTableInfo.ContainsKey(myColumnName))
                    {
                        try
                        {
                            dataGridViewSestava.Columns[i].Width = Convert.ToInt32(DBTableInfo[myColumnName]);
                        }
                        catch { }
                    }
                }
            }
        }


    }
}
