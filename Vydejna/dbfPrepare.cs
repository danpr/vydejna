using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vydejna
{
    class dbfPrepare
    {
        private const Int32 delkaHlavicky = 32;

        private Boolean dbOpened;
        private FileStream DBFStream = null;
        private Int64 DBFlength = 0;
        private Int32 pocetZaznamu = 0;

        public dbfPrepare()
        {
            this.dbOpened = false;
        }

        public void open(string fileName)
        {
            try
            {

                DBFStream = new FileStream(fileName, FileMode.Append);
                DBFlength = DBFStream.Length;
                dbOpened = true;
            }
            catch
            {
                DBFStream.Close();
                dbOpened = false;
            }

            getHeader();
        }

        private void getHeader()
        {
            if (dbOpened)
            {
                if (DBFlength > delkaHlavicky)
                {
                    BinaryReader br = new BinaryReader(DBFStream);
//                            while (br.BaseStream.Position < br.BaseStream.Length)
                  byte[] hlavicka = new byte[delkaHlavicky];
                  hlavicka = br.ReadBytes(delkaHlavicky);
                    // zaznamy 04 - 07
                  pocetZaznamu = hlavicka[4] * 256 * 16 + hlavicka[5] * 256 + hlavicka[6] * 16 + hlavicka[7];
                      //8-9 velikost hlavickty
                      //9-10 velikost zaznamu
                }
                else
                {
                    DBFStream.Close();
                    dbOpened = false;
                }




            }
        }





    }

}
