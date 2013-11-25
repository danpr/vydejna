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
    public partial class Prohledavani : Form
    {
        public class ColumnInfo
        {
            public string varColumnType;
            public string name;

            public ColumnInfo (string varColumnType, string name)
            {
            this.varColumnType = varColumnType;
            this.name = name;
            }
        }


        private DataGridView myDataGridView;
        private ArrayList comboBoxColumnInfo;

        public Prohledavani(DataGridView myDataGridView)
        {
            InitializeComponent();

            comboBoxColumnInfo = new ArrayList();

            this.myDataGridView = myDataGridView;
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;
            loadComboBox();
//            if (comboBoxColumns.Items.Count > 0) comboBoxColumns.SelectedIndex = 0;
            if (comboBoxNumeric.Items.Count > 0) comboBoxNumeric.SelectedIndex = 0;
            if (comboBoxDate.Items.Count > 0) comboBoxDate.SelectedIndex = 0;

        }

        private void loadComboBox()
        {
            DataTable mdt = (DataTable)myDataGridView.DataSource;

            comboBoxColumns.Items.Clear();
            for (int i = 0; i < myDataGridView.ColumnCount; i++ )
            {
               if ((myDataGridView.Columns[i].Visible))
               {
                   string ns = mdt.Columns[i].DataType.ToString().Substring(mdt.Columns[i].DataType.ToString().IndexOf(".") + 1);
                   if ((ns == "Int64") || (ns == "Int32") || (ns == "Int16") || (ns == "Double"))
                   {
                       ns = "Numeric";
                   }
                   ColumnInfo myColumnInfo = new ColumnInfo(ns, myDataGridView.Columns[i].Name);
                   comboBoxColumns.Items.Add(myDataGridView.Columns[i].HeaderText.ToString());
                   comboBoxColumnInfo.Add(myColumnInfo);
               }
            }
        }

        private void comboBoxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // vybrani
            if (comboBoxColumns.SelectedIndex != -1)
            {
                textBoxString.Enabled = false;
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);

                textBoxString.Enabled = false;
                comboBoxNumeric.Enabled = false;
                numericUpDownNumeric.Enabled = false;
                comboBoxDate.Enabled = false;
                dateTimePickerDate.Enabled = false;

                switch (myType)
                {
                    case "String" :
                    textBoxString.Enabled = true;
                    break;
                    case "Numeric":
                    comboBoxNumeric.Enabled = true;
                    numericUpDownNumeric.Enabled = true;
                    break;
                    case "DateTime":
                    comboBoxDate.Enabled = true;
                    dateTimePickerDate.Enabled = true;
                    break;
                }
                groupBox1.Focus();

             }
        }

        private void najdiRadku()
        {

        if (comboBoxColumns.SelectedIndex > -1)
            {
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);

                Boolean lineIsFound = false;

            
                string columnName = ((ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex]).name;

                Int32 testingRow = 0;

                while (testingRow < myDataGridView.Rows.Count)
                {
                    //DataGridViewRowCollection myDGVCol = myDataGridView.Rows[testingRow];
                    string columnNameValue = (myDataGridView.Rows[testingRow].Cells[columnName]).ToString();

                    switch (myType)
                    {
                        case "String" :

                            if (columnNameValue.IndexOf(comboBoxColumns.Text) != -1)
                            {
                                lineIsFound = true;

                            }

                        break;


                    }


                    testingRow++;
                }
                if (lineIsFound)
                {
                    // presuneme se na nalezenou radku
                }

            }


        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            // prohledavani
            najdiRadku();

        }
    }
}
