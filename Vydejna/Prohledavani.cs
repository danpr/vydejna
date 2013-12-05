using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

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
            this.TopMost = true;

            comboBoxColumnInfo = new ArrayList();

            this.myDataGridView = myDataGridView;
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;
            loadComboBox();
            buttonOK.Enabled = false;
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
                buttonOK.Enabled = true;
                textBoxString.Enabled = false;
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);

                textBoxString.Enabled = false;
                comboBoxNumeric.Enabled = false;
                numericUpDownNumeric.Enabled = false;
                comboBoxDate.Enabled = false;
                dateTimePickerDate.Enabled = false;
                checkBoxUpcase.Enabled = false;

                switch (myType)
                {
                    case "String":
                        textBoxString.Enabled = true;
                        checkBoxUpcase.Enabled = true;
                        if (textBoxString.Text.Trim() == "")
                        {
                            buttonOK.Enabled = false;
                        }
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
            else
            {
                buttonOK.Enabled = false;
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

                if (checkBoxFromStart.Checked)
                {
                    testingRow = 0;
                }
                else 
                {
                    testingRow = myDataGridView.SelectedRows[0].Index+1;
                }

                while ((testingRow < myDataGridView.Rows.Count) && (!(lineIsFound)))
                {

                    string columnNameValue = (myDataGridView.Rows[testingRow].Cells[columnName]).Value.ToString();

                    switch (myType)
                    {
                        case "String" :
                            string substring = textBoxString.Text;

                            if (checkBoxUpcase.Checked)
                            {
                                columnNameValue = columnNameValue.ToUpper();
                                substring = substring.ToUpper();
                            }

                            if (columnNameValue.IndexOf(substring) != -1)
                                {
                                    lineIsFound = true;
                                }
                        break;
                        case "Numeric" :
                        
                        decimal columnNameValueDec = Convert.ToDecimal(columnNameValue);
                        switch (comboBoxNumeric.SelectedIndex)
                        {
                            case 0:
                                if (columnNameValueDec == numericUpDownNumeric.Value)
                                {
                                    lineIsFound = true;
                                }
                                break;
                            case 1:
                                if (columnNameValueDec > numericUpDownNumeric.Value)
                                {
                                    lineIsFound = true;
                                }
                                break;
                            case 2:
                                if (columnNameValueDec < numericUpDownNumeric.Value)
                                {
                                    lineIsFound = true;
                                }
                                break;
                        }
                        break;
                        case "DateTime":

                        DateTime columnNameValueDate = Convert.ToDateTime(columnNameValue);
                        switch ( comboBoxDate.SelectedIndex)
                        {
                            case 0:
                                if (columnNameValueDate == dateTimePickerDate.Value )
                                {
                                    lineIsFound = true;
                                }
                                break;
                            case 1:
                                if (columnNameValueDate > dateTimePickerDate.Value)
                                {
                                    lineIsFound = true;
                                }
                                break;
                            case 2:
                                if (columnNameValueDate < dateTimePickerDate.Value)
                                {
                                    lineIsFound = true;
                                }
                                break;
                        }


                        break;

                        
                    }

                    testingRow++;
                }
                if (lineIsFound)
                {
                    testingRow--;
                    // presuneme se na nalezenou radku
                    myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[testingRow].Index;
                    myDataGridView.Refresh();

                    myDataGridView.CurrentCell = myDataGridView.Rows[testingRow].Cells[1];
                    myDataGridView.Rows[testingRow].Selected = true;
                }
                else
                {
                    MessageBox.Show("Hledaná hodnota nabyla do konce tabulku nalezena.");
                }

            }


        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            // prohledavani
            najdiRadku();

        }

        private void comboBoxNumeric_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxString_TextChanged(object sender, EventArgs e)
        {
            if ((comboBoxColumns.SelectedIndex != -1) && (textBoxString.Text.Trim() != ""))
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }
        }


    }
}
