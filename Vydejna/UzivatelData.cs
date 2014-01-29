using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Vydejna
{
    public sealed class UzivatelData
    {
        private Boolean userLogin = false;
        private Boolean admin = false;
        private string userid = "";
        private string passwordHash = "";
        private string jmeno = "";
        private string prijmeni = "";

        private vDatabase myDataBase = null;
        
        
        private UzivatelData()
        {
        }

        static readonly UzivatelData _instance =  new UzivatelData();

        public static UzivatelData makeInstance()
        {
            return _instance;
        }


        public static Boolean userIDExist(string userid, vDatabase myDataBase)
        {
            if (myDataBase == null)
            {
                return false;
            }

            if (!(myDataBase.DBIsOpened()))
            {
                return false;
            }

            return myDataBase.tableUzivateleItemExist(userid);
        }


        public void setDB (vDatabase myDataBase)
        {
            this.myDataBase = myDataBase;
        }

        public Boolean loadUzivatele(string userid)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                Hashtable DBRow;
                DBRow = myDataBase.getUzivateleLine(userid, null);
                if (DBRow == null)
                {
                    return false;
                }
                else
                {
                    if (DBRow.ContainsKey("userid"))
                    {
                        this.userid = Convert.ToString( DBRow["userid"]);
                    }
                    if (DBRow.ContainsKey("password"))
                    {
                        this.passwordHash = Convert.ToString( DBRow["password"]);
                    }
                    if (DBRow.ContainsKey("jmeno"))
                    {
                        this.jmeno = Convert.ToString( DBRow["jmeno"]);
                    }
                    if (DBRow.ContainsKey("prijmeni"))
                    {
                        this.prijmeni = Convert.ToString( DBRow["prijmeni"]);
                    }
                    if (DBRow.ContainsKey("admin"))
                    {
                        if (Convert.ToString(DBRow["prijmeni"]) == "N")
                        {
                            this.admin = false;
                        }
                        else
                        {
                            this.admin = true;
                        }
                    }

                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public bool login()
        {
            if (myDataBase == null)
            {
                return false;
            }
            if (myDataBase.DBIsOpened())
            {
                PrihlaseniKarta prihlaseni = new PrihlaseniKarta(myDataBase);
                if (prihlaseni.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }
                else
                {
                    // nastavime promenne
                    userLogin = true;
                    loadUzivatele(prihlaseni.getUserId());
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


        public Boolean userIsLogin()
        {
            return userLogin;
        }

        public Boolean userIsAdmin ()
        {
            if (userLogin) { return admin; } else { return false; }
        }


        public string Jmeno
        {
            get { if (userLogin) return jmeno; else return ""; }
        }

        public string Prijmerni
        {
            get { if (userLogin) return prijmeni; else return ""; }
        }

    }
}
