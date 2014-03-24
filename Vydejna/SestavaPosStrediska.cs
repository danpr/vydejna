﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Vydejna
{
    class SestavaPosStrediska : SestavaDefault
    {

        public SestavaPosStrediska(vDatabase myDataBase) : base(myDataBase,"Vyhodnocení poškozenek dle střediska", "") { }

        public override DataTable loadDataTable()
        {
            DateTime dateTimeFrom = getDateFrom();
            DateTime dateTimeTo = getDateTo();

            return myDataBase.loadDataTableSestavaPosStrediska("2/2/2010", "12/3/2014");
        }

    }
}
