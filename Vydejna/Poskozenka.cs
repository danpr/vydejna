using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class Poskozenka : Form
    {


        public class messager
        {
            public string jk;
            public Int32 pocetKs;
            public DateTime datum;
            public string poznamka;
            public Int32 poradi;


            public messager(Int32 poradi, string jk, Int32 pocetKs, DateTime datum, string poznamka)
            {
                this.jk = jk;
                this.pocetKs = pocetKs;
                this.datum = datum;
                this.poznamka = poznamka;
                this.poradi = poradi;
            }
        }


        public Poskozenka(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
        }

        public messager getMesseger()
        {
            return null;
        }


    }
}
