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
using System.Text.RegularExpressions;

namespace Vydejna
{
    public partial class Prohledavani : Form
    {

        public class Wildcard : Regex
        {
            public Wildcard(string pattern, Boolean useFirstColumn)
                : base(WildcardToRegex(pattern, useFirstColumn))
            {
            }



            public Wildcard(string pattern, RegexOptions options, Boolean useFirstColumn)
                : base(WildcardToRegex(pattern,useFirstColumn), options)
            {
            }



            public static string WildcardToRegex(string pattern, Boolean useFC = false)
            {
                if (useFC)
                {
                    return "^" + Regex.Escape(pattern).
                     Replace("\\*", ".*").
                     Replace("\\?", ".") + "$";
                }
                else
                {
                    return Regex.Escape(pattern).
                     Replace("\\*", ".*").
                     Replace("\\?", ".") + "$";
                }
            }
        }        
        
        
        public class ColumnInfo
        {
            public string varColumnType;
            public string name;
            public string description;

            public ColumnInfo (string varColumnType, string name, string description)
            {
            this.varColumnType = varColumnType;
            this.name = name;
            this.description = description;
            }
        }


        private DataGridView myDataGridView;
        private ArrayList comboBoxColumnInfo;
        private string preferedColumn;
        private string windowName;
        private string windowTableDesc;
        private Boolean settingChanged = false;

        public Prohledavani(DataGridView myDataGridView, string preferedColumn, string windowName = "", string windowTableDesc = "")
        {
            InitializeComponent();

// po prvnim spusteni je aktivovana prohledavani od prveho radku
            checkBoxFromStart.Checked = true;

            this.preferedColumn = preferedColumn;
            this.TopMost = true;
            this.windowName = windowName;
            this.windowTableDesc = windowTableDesc;


            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;

            comboBoxColumnInfo = new ArrayList();

            this.myDataGridView = myDataGridView;

            // nastavi standartni hodnoty pro hledani
            setDefaultSearch();

            ConfigReg.TableSearch myTableSearch = ConfigReg.loadSettingSearch(windowName, windowTableDesc);

            if (myTableSearch != null)
            {
                string fieldName = myTableSearch.columnName;
                for (Int32 i = 0; i < comboBoxColumnInfo.Count; i++)
                {
                    if (((ColumnInfo)comboBoxColumnInfo[i]).name == fieldName)
                    {
                        comboBoxColumns.SelectedIndex = i;
                        break;
                    }
                }


                checkBoxFromFirstChar.Checked = myTableSearch.searchFromFirstColumn;
                checkBoxUpcase.Checked = myTableSearch.noCaseSensitive;
                checkBoxDiacritism.Checked = myTableSearch.diacritcs;
                checkBoxWildCard.Checked = myTableSearch.use;
                comboBoxRegex.SelectedIndex = myTableSearch.useType;
            }

            buttonOK.Enabled = false;
            if (comboBoxNumeric.Items.Count > 0) comboBoxNumeric.SelectedIndex = 0;
            if (comboBoxDate.Items.Count > 0) comboBoxDate.SelectedIndex = 0;
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
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
                   ColumnInfo myColumnInfo = new ColumnInfo(ns, myDataGridView.Columns[i].Name, myDataGridView.Columns[i].HeaderText.ToString());
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
                settingChanged = true;
                buttonOK.Enabled = true;
                textBoxString.Enabled = false;
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);

                textBoxString.Enabled = false;
                comboBoxNumeric.Enabled = false;
                numericUpDownNumeric.Enabled = false;
                comboBoxDate.Enabled = false;
                dateTimePickerDate.Enabled = false;
                checkBoxDiacritism.Enabled = false;
                checkBoxUpcase.Enabled = false;
                checkBoxFromFirstChar.Enabled = false;
                checkBoxWildCard.Enabled = false;
                comboBoxRegex.Enabled = false;

                groupBox1.Focus();
                switch (myType)
                {
                    case "String":
                        textBoxString.Enabled = true;
                        checkBoxUpcase.Enabled = true;
                        checkBoxFromFirstChar.Enabled = true;
                        checkBoxWildCard.Enabled = true;
                        comboBoxRegex.Enabled = true;
                        checkBoxDiacritism.Enabled = true;

                        setFirstFromCharChecker();
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
                Regex regex = null;
                string substring="";


                if (myType == "String")
                {
                    substring = textBoxString.Text;

                    if (checkBoxWildCard.Checked)
                    {
                        if (!(checkBoxDiacritism.Checked))
                        {
                            substring = RemoveDiacritics(substring);
                        }
                        // pouzijeme divokou kartu

                        if (comboBoxRegex.SelectedIndex == 0)
                        {
                            if (checkBoxUpcase.Checked)
                            {
                                regex = new Wildcard(substring, RegexOptions.IgnoreCase, checkBoxFromFirstChar.Checked);
                            }
                            else
                            {
                                regex = new Wildcard(substring, checkBoxFromFirstChar.Checked);
                            }
                        }
                        else
                        {
                            if (checkBoxUpcase.Checked)
                            {
                                regex = new Regex(substring, RegexOptions.IgnoreCase);
                            }
                            else
                            {
                                regex = new Regex(substring);
                            }
                        }
                    }
                    else
                    {
                        if (!(checkBoxDiacritism.Checked))
                        {
                            substring = RemoveDiacritics(textBoxString.Text);
                        }

                        if (checkBoxUpcase.Checked)
                        {
                            substring = substring.ToUpper();
                        }

                    }
                }


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
                            if (!(checkBoxDiacritism.Checked))
                            {
                                columnNameValue = RemoveDiacritics(columnNameValue);
                            }

                            if (checkBoxUpcase.Checked)
                            {
                                columnNameValue = columnNameValue.ToUpper();
                            }

                            if (checkBoxWildCard.Checked)
                            {
                                // pouzijeme divokou kartu
                                if (regex.IsMatch(columnNameValue))
                                {
                                    lineIsFound = true;
                                }

                            }
                            else
                            {
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
            if (settingChanged)
            {
                ConfigReg.saveSettingSearch(new ConfigReg.TableSearch(windowName, windowTableDesc, ((ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex]).name, checkBoxFromFirstChar.Checked, checkBoxUpcase.Checked, checkBoxDiacritism.Checked, checkBoxWildCard.Checked, comboBoxRegex.SelectedIndex));
                settingChanged = false;
            }

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

//        private void Prohledavani_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == '1')
//            {
//                Close();
//            }
//        }

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


        private void setPreferedColumnInComboBox( string preferedColumn)
        {
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



        private void Prohledavani_Shown(object sender, EventArgs e)
        {

            checkBoxFromStart.Checked = true;
            
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingChanged = true;
            setFirstFromCharChecker();
        }

        private void checkBoxWildCard_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
            if (checkBoxWildCard.Checked )
            {
                comboBoxRegex.Enabled = true;
            }
            else
            {
                comboBoxRegex.Enabled = false;
            }
            setFirstFromCharChecker();

        }

        private void setFirstFromCharChecker()
        {
            if (checkBoxWildCard.Checked)
            {

                if (comboBoxRegex.SelectedIndex == 0)
                {
                    checkBoxFromFirstChar.Enabled = true;
                }
                else
                {
                    checkBoxFromFirstChar.Enabled = false;
                }
            }
            else
            {
                checkBoxFromFirstChar.Enabled = true;
            }
        }

        private void checkBoxFromStart_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
        }

        private void checkBoxFromFirstChar_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
        }

        private void checkBoxUpcase_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
        }

        private void checkBoxDiacritism_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            setDefaultSearch();
        }

        private void setDefaultSearch()
        {
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;

            loadComboBox();
            setPreferedColumnInComboBox(preferedColumn);

            checkBoxFromStart.Checked = true;
            checkBoxUpcase.Checked = true;
            checkBoxDiacritism.Checked = false;

            checkBoxWildCard.Checked = false;
            comboBoxRegex.Enabled = false;
            comboBoxRegex.SelectedIndex = 0;

            // nastavi hledani od prveho sloupce
            // v zavislosti od nastaveno zpusobu hledani
            setFirstFromCharChecker();
        }

        private void textBoxString_DoubleClick(object sender, EventArgs e)
        {
            nastavProhledavanePrvky();
        }

        private void nastavProhledavanePrvky()
        {

            List<String> mainStringList = new List<String>();
            List<String> selectStringList = new List<String>();


            selectStringList.Clear();
            for (Int32 i = 0; i < comboBoxColumns.Items.Count; i++)
            {
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[i];
                string name = Convert.ToString(myColumnInfo.name);

                selectStringList.Add(name);
            }

            mainStringList.Clear();
            for (int i = 0; i < myDataGridView.ColumnCount; i++)
            {
                if ((myDataGridView.Columns[i].Visible))
                {
                    string name = myDataGridView.Columns[i].Name;
                    if (selectStringList.IndexOf(name) == -1)
                    {
                        mainStringList.Add(name);
                    }
                }
            }



            DoubleList prohledavanePrvky = new DoubleList(mainStringList, selectStringList);
            if (prohledavanePrvky.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedItem = comboBoxColumns.Text;
                List<String> selectedItems = prohledavanePrvky.getSelectedItems();

                loadComboBoxColumns(selectedItems);
                setPreferedColumnInComboBox(selectedItem);
            }

        }


        private void loadComboBoxColumns( List<String> selectedColumns)
        {
            comboBoxColumns.Items.Clear();
            foreach (ColumnInfo column in comboBoxColumnInfo)
            {
                if (selectedColumns.IndexOf(column.name) != -1)
                {
                    comboBoxColumns.Items.Add(column.description);
                }
            }
        }

    }
}
