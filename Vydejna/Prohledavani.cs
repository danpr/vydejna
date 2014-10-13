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
        private ArrayList columnInfos;
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

            columnInfos = new ArrayList();

            this.myDataGridView = myDataGridView;

            // nastavi standartni hodnoty pro hledani

            loadColumnInfos();  // naplni tabulku columnInfos informacemi o sloupcich

            if (!(loadSettingSearch()))
            {
                setDefaultSearch();
                // zkontrolujeme zda nemame jen zaznan a zmenach sloupcu
                List<string> selectedColumns = ConfigReg.loadColumnsSearch(windowName, windowTableDesc);
                if (selectedColumns != null)
                {
                    loadComboBox(selectedColumns);
                    setColumnInComboBoxByDesc(ColumnInfosName2Desc(preferedColumn));
                }
            }

            buttonOK.Enabled = false;
            if (comboBoxNumeric.Items.Count > 0) comboBoxNumeric.SelectedIndex = 0;
            if (comboBoxDate.Items.Count > 0) comboBoxDate.SelectedIndex = 0;
            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
        }



        private bool loadSettingSearch()
        {
            ConfigReg.TableSearch myTableSearch = ConfigReg.loadSettingSearch(windowName, windowTableDesc);

            if (myTableSearch != null)
            {
                loadComboBox(myTableSearch.selectedColumns);
                string selectedColumnName = myTableSearch.columnName;

                checkBoxFromFirstChar.Checked = myTableSearch.searchFromFirstColumn;
                checkBoxUpcase.Checked = myTableSearch.noCaseSensitive;
                checkBoxDiacritism.Checked = myTableSearch.diacritcs;
                checkBoxWildCard.Checked = myTableSearch.use;
                comboBoxRegex.SelectedIndex = myTableSearch.useType;

                if (!(setColumnInComboBoxByDesc(ColumnInfosName2Desc(selectedColumnName))))
                {
                    setColumnInComboBoxByDesc(ColumnInfosName2Desc(preferedColumn));
                }
                return true;
            }
            else return false;
        }

        private void setDefaultSearch()
        {
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;

            checkBoxFromStart.Checked = true;
            checkBoxUpcase.Checked = true;
            checkBoxDiacritism.Checked = false;

            checkBoxWildCard.Checked = false;
            comboBoxRegex.Enabled = false;
            comboBoxRegex.SelectedIndex = -1;

            // nastavi hledani od prveho sloupce
            // v zavislosti od nastaveno zpusobu hledani
            enableFirstFromCharChecker();

            loadComboBox(null);
            setColumnInComboBoxByDesc(ColumnInfosName2Desc(preferedColumn));
        }



        private Int32 ColumnInfosIndexOfDescription(string description)
        {
            Int32 returnValue = -1;
            if (columnInfos.Count > 0)
            {
                for (Int32 i = 0; i < columnInfos.Count; i++)
                {

                    if (((ColumnInfo)columnInfos[i]).description == description)
                    {
                        returnValue = i;
                        break;
                    }
                }
            }
            return returnValue;
        }


        private string ColumnInfosName2Desc(string name)
        {
            string returnValue = "";
            if (columnInfos.Count > 0)
            {
                for (Int32 i = 0; i < columnInfos.Count; i++)
                {

                    if (((ColumnInfo)columnInfos[i]).name == name)
                    {
                        returnValue = ((ColumnInfo)columnInfos[i]).description;
                        break;
                    }
                }
            }
            return returnValue;
        }

        private string ColumnInfosDesc2Name(string desc)
        {
            string returnValue = "";
            if (columnInfos.Count > 0)
            {
                for (Int32 i = 0; i < columnInfos.Count; i++)
                {

                    if (((ColumnInfo)columnInfos[i]).description == desc)
                    {
                        returnValue = ((ColumnInfo)columnInfos[i]).name;
                        break;
                    }
                }
            }
            return returnValue;



        }

        private void loadColumnInfos()
        {
            DataTable mdt = (DataTable)myDataGridView.DataSource;

            for (int i = 0; i < myDataGridView.ColumnCount; i++)
            {
                if ((myDataGridView.Columns[i].Visible))
                {
                    string ns = mdt.Columns[i].DataType.ToString().Substring(mdt.Columns[i].DataType.ToString().IndexOf(".") + 1);
                    if ((ns == "Int64") || (ns == "Int32") || (ns == "Int16") || (ns == "Double"))
                    {
                        ns = "Numeric";
                    }
                    ColumnInfo myColumnInfo = new ColumnInfo(ns, myDataGridView.Columns[i].Name, myDataGridView.Columns[i].HeaderText.ToString());
                    columnInfos.Add(myColumnInfo);
                }
            }
        }


        private void loadComboBox(List<string> selectedItems)
        {
            // jestlize je  selected index null natahne se vse
            comboBoxColumns.Items.Clear();

            for (Int32 i = 0; i < columnInfos.Count; i++)
            {
                if ((selectedItems == null) || (selectedItems.Count == 0))
                {
                    comboBoxColumns.Items.Add(((ColumnInfo)columnInfos[i]).description);
                }
                else
                {
                    if (selectedItems.Contains(((ColumnInfo)columnInfos[i]).name))
                    {
                        string desc = ColumnInfosName2Desc(((ColumnInfo)columnInfos[i]).name);
                        if (desc != "")
                        {
                            comboBoxColumns.Items.Add(desc);
                        }
                    }
                }
            }
            if ((comboBoxColumns.Items.Count == 0) && (selectedItems.Count > 0))
            {
                for (Int32 i = 0; i < columnInfos.Count; i++)
                {
                    comboBoxColumns.Items.Add(((ColumnInfo)columnInfos[i]).description);
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

                string description = comboBoxColumns.Text;
                Int32 columnInfoIndex = ColumnInfosIndexOfDescription(description);

                ColumnInfo myColumnInfo = (ColumnInfo)columnInfos[columnInfoIndex];
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
                // je nastaveno zavolanim udalosti po zmene checkBoxWildCard
//                comboBoxRegex.SelectedIndex = -1;
//                comboBoxRegex.Enabled = false;


                groupBox1.Focus();
                switch (myType)
                {
                    case "String":
                        textBoxString.Enabled = true;
                        checkBoxUpcase.Enabled = true;
                        checkBoxFromFirstChar.Enabled = true;
                        checkBoxWildCard.Enabled = true;
//                        comboBoxRegex.Enabled = true;
                        checkBoxDiacritism.Enabled = true;

                        enableFirstFromCharChecker();
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
                ColumnInfo myColumnInfo = (ColumnInfo)columnInfos[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);

                Boolean lineIsFound = false;
                string columnName = ((ColumnInfo)columnInfos[comboBoxColumns.SelectedIndex]).name;
                
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
//                List<string> selectedColumns = new List<string>();

//                for (Int32 i = 0; i < comboBoxColumns.Items.Count; i++)
//                {
//                    selectedColumns.Add(ColumnInfosDesc2Name(comboBoxColumns.Items[i].ToString()));
//                }


                string desc = comboBoxColumns.Text;
                string name = ColumnInfosDesc2Name(desc);
                ConfigReg.saveSettingSearch
                    (new ConfigReg.TableSearch
                        (windowName, // okno
                        windowTableDesc,  // kod tabulky v okne
                        null, //  selectedColumns,
                        name, // jmeno polozky 
                        checkBoxFromFirstChar.Checked, checkBoxUpcase.Checked,
                        checkBoxDiacritism.Checked, checkBoxWildCard.Checked, 
                        comboBoxRegex.SelectedIndex));
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


        private Int32 getOrdNumInComboBox(string desc)
        {
            for (Int32 i = 0; i < comboBoxColumns.Items.Count; i++ )
            {
                if (comboBoxColumns.Items[i].ToString() == desc) return i;
            }

            return -1;
        }


        private Boolean setColumnInComboBoxByDesc(string desc)
        {
            if (comboBoxColumns.Items.Count > 0)
            {
                Int32 cisloSloupce = getOrdNumInComboBox(desc);
                if (cisloSloupce == -1)
                {
                    comboBoxColumns.SelectedIndex = -1;
                    return false;
                }
                else
                {
                    comboBoxColumns.SelectedIndex = cisloSloupce;
                    return true;
                }
            }
            else
            {
                comboBoxColumns.SelectedIndex = -1;
                return false;
            }
        }

        private void Prohledavani_Shown(object sender, EventArgs e)
        {

            checkBoxFromStart.Checked = true;

//            if ((preferedColumn != null) && (preferedColumn.Trim() != ""))
                if (comboBoxColumns.SelectedIndex != -1)
                {
                groupBox1.Focus();
                if (comboBoxColumns.SelectedIndex != -1)
                {
                    ColumnInfo myColumnInfo = (ColumnInfo)columnInfos[comboBoxColumns.SelectedIndex];
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
            enableFirstFromCharChecker();
        }

        private void checkBoxWildCard_CheckedChanged(object sender, EventArgs e)
        {
            settingChanged = true;
            checkBoxWildCarsChangeStatus();
            enableFirstFromCharChecker();
        }

        private void enableFirstFromCharChecker()
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



        private void textBoxString_DoubleClick(object sender, EventArgs e)
        {
//            nastavProhledavanePrvky();
        }

        private void nastavProhledavanePrvky()
        {
            List<String> mainStringList = new List<String>();
            List<String> selectStringList = new List<String>();

            selectStringList.Clear();
            for (Int32 i = 0; i < comboBoxColumns.Items.Count; i++)
            {
                //ColumnInfo myColumnInfo = (ColumnInfo)columnInfos[i];
                //string description = Convert.ToString(myColumnInfo.description);
                string description = Convert.ToString(comboBoxColumns.Items[i]);

                selectStringList.Add(description);
            }

            mainStringList.Clear();
            for (int i = 0; i < myDataGridView.ColumnCount; i++)
            {
                if ((myDataGridView.Columns[i].Visible))
                {
                    string name = myDataGridView.Columns[i].Name;
                    if (name != "")
                    {
                        string description = ColumnInfosName2Desc(name);
                        if (selectStringList.IndexOf(description) == -1)
                        {
                            mainStringList.Add(description);
                        }
                    }
                }
            }
            DoubleList prohledavanePrvky = new DoubleList(mainStringList, selectStringList);
            if (prohledavanePrvky.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedItem = comboBoxColumns.Text;
                List<String> selectedItemsDesc = prohledavanePrvky.getSelectedItems();
                List<String> selectedItemsName = new List<string>();

                foreach (string desc in selectedItemsDesc)
                {
                    string name = ColumnInfosDesc2Name(desc);
                    if (name.Trim() != "")
                    {
                        selectedItemsName.Add(name);
                    }
                }

                string oldSelectedName = ColumnInfosDesc2Name(comboBoxColumns.Text);
                loadComboBoxColumns(selectedItemsDesc);
                if (!(setColumnInComboBoxByDesc(ColumnInfosName2Desc(oldSelectedName))))
                {
                    setColumnInComboBoxByDesc(ColumnInfosName2Desc(preferedColumn));
                }
                ConfigReg.saveColumnsSearch(windowName, windowTableDesc, selectedItemsName);
            }
        }


        private void loadComboBoxColumns( List<String> selectedColumns)
        {
            comboBoxColumns.Items.Clear();
            foreach (ColumnInfo column in columnInfos)
            {
                if (selectedColumns.IndexOf(column.description) != -1)
                {
                    comboBoxColumns.Items.Add(column.description);
                }
            }
        }

        private void nastaveníProhledávanýchSloupcůToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nastavProhledavanePrvky();
        }

        private void checkBoxWildCard_EnabledChanged(object sender, EventArgs e)
        {
            checkBoxWildCarsChangeStatus();
        }


        private void checkBoxWildCarsChangeStatus()
        {
            if ((checkBoxWildCard.Enabled) && (checkBoxWildCard.Checked))
            {
                comboBoxRegex.Enabled = true;
                if ((comboBoxRegex.SelectedIndex == -1) && (comboBoxRegex.Items.Count > 0))
                {
                    comboBoxRegex.SelectedIndex = 0;
                }
            }
            else
            {
                comboBoxRegex.Enabled = false;
                if (comboBoxRegex.Items.Count == 0)
                {
                    comboBoxRegex.SelectedIndex = -1;
                }
            }

        }
    }
}
