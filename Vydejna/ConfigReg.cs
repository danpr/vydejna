using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;


namespace Vydejna
{
    class ConfigReg
    {

        public static Font loadSettingFont()
        {
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\FONT", true);

            Font myFont = null;
            string fontName;
            float fontSize;
            FontStyle fontStyle = FontStyle.Regular;

            if (klic != null)
            {
                try
                {
                    fontName = klic.GetValue("Name").ToString();
                    fontSize = (float)Convert.ToDouble(klic.GetValue("Size"));
                }
                catch
                {
                    return null;
                }

                try
                {
                    fontStyle = (FontStyle)Convert.ToInt32(klic.GetValue("Style"));
                }
                catch { }

                myFont = new Font(fontName, fontSize, fontStyle);
                return myFont;
            }
            return null;
        }


        public static void saveSettingFont(Font myFont)
        {

            RegistryKey regHelpKlic;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\FONT", true);
            if (klic == null)
            {
                RegistryKey regKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                if (regKlic == null)
                {
                    regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                    regHelpKlic.CreateSubKey("CS");
                }
                regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                regHelpKlic.CreateSubKey("FONT");
                klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\FONT", true);
            }
            // zapis polozky
            klic.SetValue("Name", myFont.FontFamily.Name);
            klic.SetValue("Size", myFont.Size);
            klic.SetValue("Style", (Int32)myFont.Style);
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

        public static void saveSettingWindowTableColumnWidth(string nameWin, string nameTab, string nameCol, Int32 width)
        {
            string stringKlic1 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" +nameTab+"\\COLUMNS";
            string stringKlic2 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin + "\\" + nameTab;
            string stringKlic3 = "SOFTWARE\\CS\\WINDOWS\\" + nameWin;
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
                    rKey = Registry.CurrentUser.OpenSubKey(stringKlic3, true);
                    rKey.CreateSubKey(nameTab);
                }
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic2, true);
                rKey.CreateSubKey("COLUMNS");
                rKey = Registry.CurrentUser.OpenSubKey(stringKlic1, true);
            }
            // zapis polozky
            if (rKey == null) return;
                rKey.SetValue(nameCol, width);
        }


    }
}
