using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;


namespace Vydejna
{
    class ConfigReg
    {

        public class TableSearch
        {
            public string windowName;
            public string windowTableDesc;

            public string columnName;
            public Boolean searchFromFirstColumn;
            public Boolean noCaseSensitive;
            public Boolean diacritcs;
            public bool use;
            public int useType;
            public List<string> selectedColumns;

            public TableSearch(string windowName, string windowTableDesc, List<string> selectedColumns, string columnName, Boolean searchFromFirstColumn, Boolean noCaseSensitive, Boolean diacritcs, bool use, int useType)
            {
                this.windowName = windowName;
                this.windowTableDesc = windowTableDesc;
                this.selectedColumns = selectedColumns;
                this.columnName = columnName;
                this.searchFromFirstColumn = searchFromFirstColumn;
                this.noCaseSensitive = noCaseSensitive;
                this.diacritcs = diacritcs;
                this.use = use;
                this.useType = useType;
            }

        }




        public static void loadSettingDB(parametryDB myParametryDB)
        {
            // nastavime default hodnoty
            myParametryDB.nameDB = "";
            myParametryDB.codeDB = -1;
            myParametryDB.umistemiDB = "";
            myParametryDB.adresaServerDB = "";
            myParametryDB.nameDBServeru = "";
            myParametryDB.portServerDB = 0;
            myParametryDB.localizaceDBServeru = "";
            myParametryDB.driverDB = "";
            myParametryDB.userIdDB = "";
            myParametryDB.userPasswdDB = "";
            myParametryDB.adminIdDB = "";
            myParametryDB.adminPasswdDB = "";

            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            if (klic != null)
            {
                try
                {
                    myParametryDB.nameDB = klic.GetValue("Name").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.codeDB = Convert.ToInt32(klic.GetValue("CodeDB").ToString());
                }
                catch { }
                try
                {
                    myParametryDB.umistemiDB = klic.GetValue("Location").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.adresaServerDB = klic.GetValue("ServerAddr").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.nameDBServeru = klic.GetValue("ServerName").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.portServerDB = Convert.ToInt32(klic.GetValue("ServerPort").ToString());
                }
                catch { }
                try
                {
                    myParametryDB.localizaceDBServeru = klic.GetValue("ServerLocale").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.driverDB = klic.GetValue("ServerDriver").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.userIdDB = klic.GetValue("UserId").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.userPasswdDB = klic.GetValue("UserPassword").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.adminIdDB = klic.GetValue("AdminId").ToString();
                }
                catch { }
                try
                {
                    myParametryDB.adminPasswdDB = klic.GetValue("AdminPassword").ToString();
                }
                catch { }

            }
        }

        public static void saveSettingDB(parametryDB myParametryDB)
        {
            RegistryKey regHelpKlic;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            if (klic == null)
            {
                RegistryKey regKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                if (regKlic == null)
                {
                    regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                    regHelpKlic.CreateSubKey("CS");
                }
                regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                regHelpKlic.CreateSubKey("DB");
                klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            }
            // zapis polozky
            klic.SetValue("Name", myParametryDB.nameDB);
            klic.SetValue("CodeDB", myParametryDB.codeDB);
            klic.SetValue("Location", myParametryDB.umistemiDB);
            klic.SetValue("ServerAddr", myParametryDB.adresaServerDB);
            klic.SetValue("ServerName", myParametryDB.nameDBServeru);
            klic.SetValue("ServerPort", myParametryDB.portServerDB);
            klic.SetValue("ServerLocale", myParametryDB.localizaceDBServeru);
            klic.SetValue("ServerDriver", myParametryDB.driverDB);
            klic.SetValue("UserId", myParametryDB.userIdDB);
            klic.SetValue("UserPassword", myParametryDB.userPasswdDB);
            klic.SetValue("AdminId", myParametryDB.adminIdDB);
            klic.SetValue("AdminPassword", myParametryDB.adminPasswdDB);
        }



        public static Font loadSettingFontX( string name)
        {
            string stringKey = "SOFTWARE\\CS\\FONT";
            if (name.Trim() != "")
            {
                stringKey = "SOFTWARE\\CS\\FONT\\" + name;
            }
            

            RegistryKey rkey = null;

            rkey = Registry.CurrentUser.OpenSubKey(stringKey, true);

            Font myFont = null;
            string fontName;
            float fontSize;
            FontStyle fontStyle = FontStyle.Regular;

            if (rkey != null)
            {
                try
                {
                    fontName = rkey.GetValue("Name").ToString();
                    fontSize = (float)Convert.ToDouble(rkey.GetValue("Size"));
                }
                catch
                {
                    return null;
                }

                try
                {
                    fontStyle = (FontStyle)Convert.ToInt32(rkey.GetValue("Style"));
                }
                catch { }

                myFont = new Font(fontName, fontSize, fontStyle);
                return myFont;
            }
            return null;
        }




        public static void deleteSettingFontX(string name)
        {
            string stringKey = "SOFTWARE\\CS\\FONT";
            if (name.Trim() == "")
            {
                stringKey = "SOFTWARE\\CS";
                name = "FONT";
            }

            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(stringKey, true);

            if (rkey != null)
            {
                try
                {
                    rkey.DeleteSubKeyTree(name);
                }
                catch {}
            }
        }



        public static void saveSettingFontX(Font myFont, string name)
        {
            string stringKey = "SOFTWARE\\CS\\FONT\\" + name;
            RegistryKey rkey = null;

            if (name.Trim() != "")
            {
                rkey = Registry.CurrentUser.OpenSubKey(stringKey, true);
            }
            if (rkey == null)
            {
                rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\FONT", true);
                if (rkey == null)
                {
                    rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                    if (rkey == null)
                    {
                        rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                        rkey.CreateSubKey("CS");
                        rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                    }
                    rkey.CreateSubKey("FONT");
                    rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\FONT", true);
                    if (name.Trim() != "")
                    {
                        rkey = Registry.CurrentUser.OpenSubKey(stringKey, true);
                    }
                }
                if (name.Trim() != "")
                {
                    rkey.CreateSubKey(name);
                    rkey = Registry.CurrentUser.OpenSubKey(stringKey, true);
                }
            }
            // zapis polozky
            rkey.SetValue("Name", myFont.FontFamily.Name);
            rkey.SetValue("Size", myFont.Size);
            rkey.SetValue("Style", (Int32)myFont.Style);
        }


        public static List<string> loadColumnsSearch(string windowName, string WindowTableDesc)
        {
            string stringKey;
            if (windowName.Trim() == "")
            {
                return null;
            }
            else
            {
                if (WindowTableDesc.Trim() == "")
                {
                    stringKey = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\selectedColumns";
                }
                else
                {
                    stringKey = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\" + WindowTableDesc + "\\selectedColumns";
                }
            }
            List<string> selectedItems = null;
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(stringKey);
            if (rkey != null)
            {
                try
                {
                    foreach (string name in rkey.GetValueNames())
                    {
                        if (selectedItems == null)
                        {
                            selectedItems = new List<string>();
                        }
                        selectedItems.Add(rkey.GetValue(name).ToString());
                    }
                }
                catch { }
            }
            if (selectedItems.Count == 0) return null;
            else return selectedItems;
        }


        public static TableSearch loadSettingSearch(string windowName, string WindowTableDesc)
        {
            string stringKey = "";
            if (windowName.Trim() == "")
            {
                stringKey = "SOFTWARE\\CS\\SEARCH";
            }
            else
            {
                if (WindowTableDesc.Trim() == "")
                {
                    stringKey = "SOFTWARE\\CS\\SEARCH\\" + windowName;
                }
                else
                {
                    stringKey = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\" + WindowTableDesc;
                }

            }

            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(stringKey);

            TableSearch mySearch = null;
            string columnName;
            Boolean searchFromFirstColumn, noCaseSensitive, diacritcs, use;
            Int32 useType;

            if (rkey != null)
            {

                try
                {
                    columnName = rkey.GetValue("ColumnName").ToString();
                    searchFromFirstColumn = Convert.ToBoolean( rkey.GetValue("FromFirstColumn"));
                    noCaseSensitive = Convert.ToBoolean(rkey.GetValue("NoCaseSensitive"));
                    diacritcs = Convert.ToBoolean(rkey.GetValue("UseDiacritics"));
                    use = Convert.ToBoolean(rkey.GetValue("UseWildCart"));
                    useType = Convert.ToInt32(rkey.GetValue("WildCardType"));
                }
                catch
                {
                    return null;
                }

                List<string> selectedItems = null;
                rkey = Registry.CurrentUser.OpenSubKey(stringKey+"\\selectedColumns", true);
                if (rkey != null)
                {
                    try
                    {

                        foreach (string name in rkey.GetValueNames())
                        {
                            if (selectedItems == null)
                            {
                                selectedItems = new List<string>();
                            }
                            selectedItems.Add(rkey.GetValue(name).ToString());
                        }
                    }
                    catch { }
                }
                mySearch = new TableSearch(windowName, WindowTableDesc, selectedItems, columnName, searchFromFirstColumn, noCaseSensitive, diacritcs, use, useType);
                return mySearch;
            }
            return null;
        }

        public static void saveSettingSearch(TableSearch mySearch)
        {
            if ((mySearch != null) && (mySearch.windowName.Trim() != ""))
            {
                string windowName = mySearch.windowName;
                string typeWindowName = mySearch.windowTableDesc;

                List<string> selectedColumns = mySearch.selectedColumns;

                RegistryKey rkey = null;
                string klic1 = "";
                if (typeWindowName != "")
                {
                    klic1 = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\" + typeWindowName;
                }
                else
                {
                    klic1 = "SOFTWARE\\CS\\SEARCH\\" + windowName;
                }

                    rkey = Registry.CurrentUser.CreateSubKey(klic1);

                // zapis polozky
                if (rkey != null)
                {
                    rkey.SetValue("ColumnName", mySearch.columnName);
                    rkey.SetValue("FromFirstColumn", mySearch.searchFromFirstColumn);
                    rkey.SetValue("NoCaseSensitive", mySearch.noCaseSensitive);
                    rkey.SetValue("UseDiacritics", mySearch.diacritcs);
                    rkey.SetValue("UseWildCart", mySearch.use);
                    rkey.SetValue("WildCardType", mySearch.useType);

                    RegistryKey rkeyPar = rkey;
                    string klic2 = klic1 + "\\selectedColumns";
                    rkey = Registry.CurrentUser.CreateSubKey(klic2);
                    if (rkey != null)
                    {
                        string[] columns = rkey.GetValueNames();
                        foreach (string column in columns)
                        {
                            rkey.DeleteValue(column);// DeleteSubKey(column);
                        }
                        if (selectedColumns != null)
                        {
                            Int32 i = 0;
                            foreach (string column in selectedColumns)
                            {
                                rkey.SetValue(i.ToString(), column);
                                i++;
                            }
                        }
                    }
                }
            }
        }



        public static void saveColumnsSearch(string windowName, string typeWindowName, List<string> selectedColumns)
        {
            if (windowName.Trim() != "")
            {

                RegistryKey rkey = null;
                string klic1 = "";
                if (typeWindowName != "")
                {
                    klic1 = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\" + typeWindowName + "\\selectedColumns";
                }
                else
                {
                    klic1 = "SOFTWARE\\CS\\SEARCH\\" + windowName + "\\selectedColumns";
                }
                rkey = Registry.CurrentUser.CreateSubKey(klic1);

                // zapis polozky
                if (rkey != null)
                {
                    string[] columns = rkey.GetValueNames();
                    foreach (string column in columns)
                    {
                        rkey.DeleteValue(column);// DeleteSubKey(column);
                    }
                    if (selectedColumns != null)
                    {
                        Int32 i = 0;
                        foreach (string column in selectedColumns)
                        {
                            rkey.SetValue(i.ToString(), column);
                            i++;
                        }
                    }
                }
            }
        }



        public static string loadSettingLastUser()
        {
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);

            string lastUserName;

            if (klic != null)
            {
                try
                {
                    lastUserName = klic.GetValue("lastUserName").ToString();
                }
                catch
                {
                    return null;
                }
                return lastUserName;
            }
            return null;
        }



        public static void saveSettingLastUser(string user)
        {
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
            if (klic == null)
            {
                klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                klic.CreateSubKey("CS");
                klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
            }
            // zapis polozky
            klic.SetValue("LastUserName", user);
        }




        public static Point loadSettingWindowLocation(string name)
        {
            string stringKlic = "SOFTWARE\\CS\\WINDOWS\\" + name;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(stringKlic, true);

            Int32 x, y;

            if (klic != null)
            {
                try
                {
                    x = Convert.ToInt32(klic.GetValue("X"));
                    y = Convert.ToInt32(klic.GetValue("Y"));
                }
                catch
                {
                    return new Point(0, 0);
                }

                return new Point(x, y);
            }
            return new Point(0, 0);
        }


        public static Size loadSettingWindowSize(string name)
        {
            string stringKlic = "SOFTWARE\\CS\\WINDOWS\\" + name;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(stringKlic, true);

            Int32 w, h;

            if (klic != null)
            {
                try
                {
                    w = Convert.ToInt32(klic.GetValue("Width"));
                    h = Convert.ToInt32(klic.GetValue("Height"));
                }
                catch
                {
                    return new Size(0, 0);
                }
                return new Size(w, h);
            }
            return new Size(0, 0);
        }


        public static void saveSettingWindowLocationSize(string name, Int32 x, Int32 y, Int32 w, Int32 h)
        {
            string stringKlic = "SOFTWARE\\CS\\WINDOWS\\" + name;
            RegistryKey rKey;
            rKey = Registry.CurrentUser.OpenSubKey(stringKlic, true);
            if (rKey == null)
            {
                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                if (rKey == null)
                {
                    rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                    if (rKey == null)
                    {
                        rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                        rKey.CreateSubKey("CS");
                    }
                    rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                    rKey.CreateSubKey("WINDOWS");
                }
                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                rKey.CreateSubKey(name);
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic, true);
            }
            // zapis polozky
            if (rKey == null) return;
            if ((x != 0) && (y != 0))
            {
                rKey.SetValue("X", x);
                rKey.SetValue("Y", y);
            }
            if ((w > 0) && (h > 0))
            {
                rKey.SetValue("Width", w);
                rKey.SetValue("Height", h);
            }
        }


        private static Hashtable loadSettingWindowTableColumn(string nameWin, string nameTab, string stringKlic)
        {
            Hashtable DBTableItems = null;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(stringKlic, true);

            if (klic != null)
            {
                if (klic.ValueCount != 0)
                {
                    DBTableItems = new Hashtable();

                    foreach (string name in klic.GetValueNames())
                    {
                        DBTableItems.Add (name,klic.GetValue(name));
                    }
                }
            }
            return DBTableItems;
        }

        public static Hashtable loadSettingWindowTableColumnWidth(string nameWin, string nameTab)
        {
           string stringKlic = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS\\WIDTH";
           return loadSettingWindowTableColumn(nameWin, nameTab, stringKlic);    
        }


        public static Hashtable loadSettingWindowTableColumnIndex(string nameWin, string nameTab)
        {
            string stringKlic = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS\\INDEX";
            return loadSettingWindowTableColumn(nameWin, nameTab, stringKlic);
        }



        public static void saveSettingWindowTableColumnWidth(string nameWin, string nameTab, string nameCol, Int32 width)
        {
            string stringKlic1 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS\\WIDTH";
            string stringKlic2 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS";
            string stringKlic3 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab;
            string stringKlic4 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin;
            RegistryKey rKey;
            rKey = Registry.CurrentUser.OpenSubKey(stringKlic1, true);
            if (rKey == null)
            {
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic2, true);
                if (rKey == null)
                {
                    rKey = Registry.CurrentUser.OpenSubKey(stringKlic3, true);
                    if (rKey == null)
                    {
                        rKey = Registry.CurrentUser.OpenSubKey(stringKlic4, true);
                        if (rKey == null)
                        {
                            rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                            if (rKey == null)
                            {
                                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                                if (rKey == null)
                                {
                                    rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                                    rKey.CreateSubKey("CS");
                                }
                                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                                rKey.CreateSubKey("WINDOWS");
                            }
                            rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                            rKey.CreateSubKey(nameWin);
                        }
                        rKey = Registry.CurrentUser.OpenSubKey(stringKlic4, true);
                        rKey.CreateSubKey(nameTab);
                    }
                    rKey = Registry.CurrentUser.OpenSubKey(stringKlic3, true);
                    rKey.CreateSubKey("COLUMNS");
                }
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic2, true);
                rKey.CreateSubKey("WIDTH");
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic1, true);
            }
            // zapis polozky
            if (rKey == null) return;
                rKey.SetValue(nameCol, width);
        }

        public static void saveSettingWindowTableColumnIndex(string nameWin, string nameTab, string nameCol, Int32 index)
        {
            if (nameTab == "")
            {
                return;
            }
            string stringKlic1 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS\\INDEX";
            string stringKlic2 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab + "\\COLUMNS";
            string stringKlic3 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab;
            string stringKlic4 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin;
            RegistryKey rKey;
            rKey = Registry.CurrentUser.OpenSubKey(stringKlic1, true);
            if (rKey == null)
            {
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic2, true);
                if (rKey == null)
                {
                    rKey = Registry.CurrentUser.OpenSubKey(stringKlic3, true);
                    if (rKey == null)
                    {
                        rKey = Registry.CurrentUser.OpenSubKey(stringKlic4, true);
                        if (rKey == null)
                        {
                            rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                            if (rKey == null)
                            {
                                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                                if (rKey == null)
                                {
                                    rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                                    rKey.CreateSubKey("CS");
                                }
                                rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                                rKey.CreateSubKey("WINDOWS");
                            }
                            rKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\WINDOWS", true);
                            rKey.CreateSubKey(nameWin);
                        }
                        rKey = Registry.CurrentUser.OpenSubKey(stringKlic4, true);
                        rKey.CreateSubKey(nameTab);
                    }
                    rKey = Registry.CurrentUser.OpenSubKey(stringKlic3, true);
                    rKey.CreateSubKey("COLUMNS");
                }
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic2, true);
                rKey.CreateSubKey("INDEX");
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic1, true);
            }
            // zapis polozky
            if (rKey == null) return;
            rKey.SetValue(nameCol, index);
        }


    }
}
