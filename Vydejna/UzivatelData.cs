using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Vydejna
{

    enum permCode
    {
        Nar = 1, NarAdd, NarEd, NarEdM, NarEdNaz, NarEdJK, NarEdCenaKs, NarEdUcCenaKs, NarEdUcCena, NarEdMin, NarEdFyStav, NarEdUcStav, NarDel, NarPrint, NarPrijem, NarPosk, NarOprO,
    ZNar, ZNarEd, ZNarDel, PNar, PNarEd, PNarDel, VNar, VNarEd, VNarDel, Prac, PracAdd, PracEd, PracDel, PracPrint, PracZapN, PracVracN, PassSet  };
    
       public class permStruct
       {
           public Int32 parent;
           public Int32 index;
           public string Description;

           public permStruct(Int32 parent, Int32 index, string Description)
           {
               this.parent = parent;
               this.index = index;
               this.Description = Description;
           }
       }


    
    public sealed class UzivatelData
    {

        public static ArrayList permList;

        private Boolean userLogin = false;
        private Boolean admin = false;
        private string userid = "";
        private string passwordHash = "";
        private string jmeno = "";
        private string prijmeni = "";
        private string permission = "";

        private vDatabase myDataBase = null;

        static UzivatelData()
        {
            permList = new ArrayList();
            permList.Add(new permStruct(0, (Int32)permCode.Nar, "Karty nářadí"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarAdd, "Přidání položky"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarEd, "Opravení položky"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdNaz, "Změna nazvu"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdJK, "Změna číslo položky"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdCenaKs, "Změna ceny za kus"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdUcCenaKs, "Změna ůčetní ceny za kus"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdUcCena, "Změna ůčetní ceny"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdMin, "Změna minimálního stavu"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdFyStav, "Změna fyzického stavu"));
            permList.Add(new permStruct((Int32)permCode.NarEd, (Int32)permCode.NarEdUcStav, "Změna učetního stavu"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarEdM, "Změna značky"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarPrijem, "Příjemka nářadí"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarPosk, "Poškozenka nářadí"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarDel, "Smazání položky"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarPrint, "Tisk položky"));
            permList.Add(new permStruct((Int32)permCode.Nar, (Int32)permCode.NarOprO, "Opravit ůčet. operaci"));
            permList.Add(new permStruct(0, (Int32)permCode.ZNar, "Zrušené karty"));
            permList.Add(new permStruct((Int32)permCode.ZNar, (Int32)permCode.ZNarEd, "Opravení položky"));
            permList.Add(new permStruct((Int32)permCode.ZNar, (Int32)permCode.ZNarDel, "Smazání položky"));
            permList.Add(new permStruct(0, (Int32)permCode.PNar, "Poškozené nářadí"));
            permList.Add(new permStruct((Int32)permCode.PNar, (Int32)permCode.PNarEd, "Opravení položky"));
            permList.Add(new permStruct((Int32)permCode.PNar, (Int32)permCode.PNarDel, "Smazání položky"));
            permList.Add(new permStruct(0, (Int32)permCode.VNar, "Vrácené nářadí"));
            permList.Add(new permStruct((Int32)permCode.VNar, (Int32)permCode.VNarEd, "Opravení položky"));
            permList.Add(new permStruct((Int32)permCode.VNar, (Int32)permCode.VNarDel, "Smazání položky"));
            permList.Add(new permStruct(0, (Int32)permCode.Prac, "Pracovníci"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracAdd, "Přidání položky"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracEd, "Opravení položky"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracDel, "Smazání položky"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracPrint, "Tisk položky"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracZapN, "Zapůjčení nářadí"));
            permList.Add(new permStruct((Int32)permCode.Prac, (Int32)permCode.PracVracN, "Vrácení nářadí"));
            permList.Add(new permStruct(0, (Int32)permCode.PassSet, "Změna hesla"));

        }

        public static string countHashPassd (string passwd)
        {
            using (SHA1 sha1FingerPrint = SHA1.Create())
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] passByte = encoder.GetBytes(passwd);
                sha1FingerPrint.ComputeHash(passByte);
                string passHash = Convert.ToBase64String(sha1FingerPrint.Hash);

                return passHash;
            }
        }


        public static string getPasswdHashFromDB (string userid, vDatabase myDataBase)
        {

            if (myDataBase == null)
            {
                return null;
            }

            if (!(myDataBase.DBIsOpened()))
            {
                return null;
            }

            Hashtable DBRow = myDataBase.getUzivateleLine(userid, null);
            if (DBRow.ContainsKey("password"))
            {
                return Convert.ToString( DBRow["password"]).Trim();
            }
            else return null;
        }

        private UzivatelData() // konstruktor
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

                    if (DBRow.ContainsKey("permission"))
                    {
                        this.permission = Convert.ToString(DBRow["permission"]);
                    }

                    if (DBRow.ContainsKey("admin"))
                    {
                        if (Convert.ToString(DBRow["admin"]) == "N")
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

        public Boolean userIsAdminWM()
        {
            Boolean returnCode = userIsAdmin();
            if (!(returnCode))
            {
                MessageBox.Show("Lituji, tuto akci muže vyvolat jen uživatel s administrátorským oprávněním.");
            }

            return returnCode;
        }

        public Boolean userIsAdmin ()
        {
            if (userLogin) { return admin; } else { return false; }
        }

        public Boolean userHasAccessRightsWM(Int32 numberOfPermcode)
        {
            Boolean returnCode = userIsAdmin();
            if (returnCode)
            {
                MessageBox.Show("Lituji, tuto akci nemůže vyvolat jen uživatel s administrátorským oprávněním.");
                return false;
            }
            else
            {
                returnCode = userHasAccessRights(numberOfPermcode);
                if (!(returnCode))
                {
                    MessageBox.Show("Lituji, ale nemáte právo pro vyvolání této akce.");
                }
                return returnCode;
            }
        }

        public Boolean userHasAccessRights(Int32 numberOfPermcode )
        {
            if (userLogin)
            {// permision

                if (admin)
                {
                    return false;
                }
                else
                {
                    if ((permission[numberOfPermcode] == 'Y') || (permission[numberOfPermcode] == 'A'))
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }




        public string Jmeno
        {
            get { if (userLogin) return jmeno; else return ""; }
        }

        public string Prijmeni
        {
            get { if (userLogin) return prijmeni; else return ""; }
        }

        public string userID
        {
            get { if (userLogin) return userid; else return ""; }
        }

    }
}
