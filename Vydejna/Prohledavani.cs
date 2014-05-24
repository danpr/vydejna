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
        private string preferedColumn;

        public Prohledavani(DataGridView myDataGridView, string preferedColumn)
        {
            InitializeComponent();
            this.preferedColumn = preferedColumn;
            this.TopMost = true;

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            comboBoxColumnInfo = new ArrayList();

            this.myDataGridView = myDataGridView;
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;
            loadComboBox(preferedColumn);

            buttonOK.Enabled = false;
            if (comboBoxNumeric.Items.Count > 0) comboBoxNumeric.SelectedIndex = 0;
            if (comboBoxDate.Items.Count > 0) comboBoxDate.SelectedIndex = 0;
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
        }

        private void loadComboBox(string preferedColumn)
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
            if (comboBoxColumns.Items.Count > 0)
            {
                Int32 cisloSloupce = najdiCisloSloupce(preferedColumn);
                if (cisloSloupce == -1)
                {
                comboBoxColumns.SelectedIndex = 0;
                }
                else
                {
                    comboBoxColumns.SelectedIndex = cisloSloupce;
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

                groupBox1.Focus();
                switch (myType)
                {
                    case "String":
                        textBoxString.Enabled = true;
                        checkBoxUpcase.Enabled = true;
                        if (textBoxString.Text.Trim() == "")
                        {
                            buttonOK.Enabled = false;
                        }
                        textBoxString.Focus();
                        break;
                    case "Numeric":
                        comboBoxNumeric.Enabled = true;
                        numericUpDownNumeric.Enabled = true;
                        comboBoxNumeric.Focus();
                        break;
                    case "DateTime":
                        comboBoxDate.Enabled = true;
                        dateTimePickerDate.Enabled = true;
                        comboBoxDate.Focus();
                        break;
                }

            }
            else
            {
                buttonOK.Enabled = false;
            }
        }

        public void najdiRadku()
        {

        if ((comboBoxColumns.SelectedIndex > -1) && (buttonOK.Enabled))
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
                            if (!(checkBoxDiacritism.Checked))
                            {
                                columnNameValue = RemoveDiacritics(columnNameValue);
                                substring = RemoveDiacritics(substring);
                            }

                            if (checkBoxUpcase.Checked)
                            {
                                columnNameValue = columnNameValue.ToUpper();
                                substring = substring.ToUpper();
                            }
                            // naprosta shoda
                            if (columnNameValue == substring)
                            {
                                lineIsFound = true;
                            }
                            else
                            {

                                // shoda od prvnuho znaku
                                if (checkBoxFromFirstChar.Checked)
                                {
                                    if (columnNameValue.IndexOf(substring) == 0)
                                    {
                                        lineIsFound = true;
                                    }
                                }
                                else // shoda kdekilov
                                {
                                    if (columnNameValue.IndexOf(substring) != -1)
                                    {
                                        lineIsFound = true;
                                    }
                                }
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
            checkBoxFromStart.Checked = false;

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

        private void Prohledavani_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
                Close();
            }
        }

        private int najdiCisloSloupce(string jmeno)
        {
            for (Int32 i = 0; i < comboBoxColumnInfo.Count; i++ )
            {
                ColumnInfo ci = (ColumnInfo)comboBoxColumnInfo[i];
                if (ci.name == jmeno)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Prohledavani_Shown(object sender, EventArgs e)
        {
            if ((preferedColumn != null) && (preferedColumn.Trim() != ""))
            {
                groupBox1.Focus();
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);
                switch (myType)
                {
                    case "String":
                        textBoxString.Focus();
                        break;
                    case "Numeric":
                        comboBoxNumeric.Focus();
                        break;
                    case "DateTime":
                        comboBoxDate.Focus();
                        break;
                }
            }

        }

        public static string RemoveDiacritics(String s)
        {
            // oddělení znaků od modifikátorů (háčků, čárek, atd.)
            s = s.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                // do řetězce přidá všechny znaky kromě modifikátorů
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(s[i]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[i]);
                }
            }
            // vrátí řetězec bez diakritiky
            return sb.ToString();
        }


    }
}
