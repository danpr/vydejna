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
        private Int64 logPocetZaznamu = 0;
        private Int64 fyzPocetZaznamu = 0;

        public dbfPrepare()
        {
            this.dbOpened = false;
        }

        public void open(string fileName)
        {
            try
            {
                DBFStream = new FileStream(fileName, FileMode.Open);
                DBFlength = DBFStream.Length;
                logPocetZaznamu = 0;
                fyzPocetZaznamu = 0;
                dbOpened = true;
            }
            catch
            {
                if (DBFStream != null)
                {
                    DBFStream.Close();
                    DBFStream = null;
                }
                dbOpened = false;
                clearVariables();
            }

            getHeader();
        }

        public void close()
        {
            if (dbOpened)
            {

                if (DBFStream != null)
                {
                    DBFStream.Close();
                }
                dbOpened = false;
                clearVariables();
            }
        }

        private void getHeader()
        {
            if (dbOpened)
            {
                if (DBFlength > delkaHlavicky)
                {
                    BinaryReader br = new BinaryReader(DBFStream);
                    //                            while (br.BaseStream.Position < br.BaseStream.Length)
                    br.BaseStream.Position = 0;
                    byte[] hlavicka = new byte[delkaHlavicky]; // globalni udaje
                    hlavicka = br.ReadBytes(delkaHlavicky);
                    // zaznamy 04 - 07
                    logPocetZaznamu = hlavicka[7] * 65536 * 256 + hlavicka[6] * 65536 + hlavicka[5] * 256 + hlavicka[4];

                    Int32 velikostHlavicky = hlavicka[9] * 256 + hlavicka[8];
                    Int32 velikostZaznamu = hlavicka[11] * 256 + hlavicka[10];
                    Int32 pocetSloupcu = (velikostHlavicky / 32) - 1;
                    //8-9 velikost hlavickty
                    //9-10 velikost zaznamu
                    fyzPocetZaznamu = (DBFlength - velikostHlavicky) / velikostZaznamu;
                    br.Dispose();
                }
                else
                {
                    close();
//                    DBFStream.Close();
 //                   dbOpened = false;
//                    clearVariables();
                }

            }
        }


        public Boolean recordCountIsOK ()
        {
            if (fyzPocetZaznamu == logPocetZaznamu) 
              return true;
            else return false;
        }

        public void correctRecordCount()
        {
            if (dbOpened)
            {
                if (fyzPocetZaznamu != logPocetZaznamu)
                {
                    byte[] pocetZaznamu = new byte[4]; // globalni udaje
                    Int64 pocet = fyzPocetZaznamu;
                    pocetZaznamu[3] = (byte)(pocet / (65536 * 256));
                    pocet = pocet % (65536 * 256);
                    pocetZaznamu[2] = (byte)(pocet / (65536));
                    pocet = pocet % (65536);
                    pocetZaznamu[1] = (byte)(pocet / (256));
                    pocetZaznamu[0] = (byte)(pocet % (256));

//                    BinaryWriter bw = new BinaryWriter(DBFStream);
//                    bw.BaseStream.Position = 7;
//                    bw.Write(pocetZaznamu);
//                    getHeader();
                }
            }
        }

        private void clearVariables()
        {
        DBFStream = null;
        DBFlength = 0;
        logPocetZaznamu = 0;
        fyzPocetZaznamu = 0;
        }


    }

}
