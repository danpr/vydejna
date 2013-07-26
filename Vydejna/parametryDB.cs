using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vydejna
{
    public class parametryDB : ICloneable

    {
        public int codeDB;
        public string nameDB;
        public string umistemiDB;
        public string adresaServerDB;
        public int portServerDB;
        public string userIdDB;
        public string userPasswdDB;
        public string adminIdDB;
        public string adminPasswdDB;


        public parametryDB()
        {
        this.codeDB = (int)kodDB.dbNone;
        this.nameDB = "";
        this.umistemiDB = "";
        this.adresaServerDB = "";
        this.portServerDB = 0;
        this.userIdDB = "";
        this.userPasswdDB = "";
        this.adminIdDB = "";
        this.adminPasswdDB = "";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
