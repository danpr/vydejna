﻿using System;
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
                dbOpened = true;
            }
            catch
            {
                DBFStream.Close();
                dbOpened = false;
            }

            getHeader();
        }

        public void close()
        {
            DBFStream.Close();
        }

        private void getHeader()
        {
            if (dbOpened)
            {
                if (DBFlength > delkaHlavicky)
                {
                    BinaryReader br = new BinaryReader(DBFStream);
//                            while (br.BaseStream.Position < br.BaseStream.Length)
                  byte[] hlavicka = new byte[delkaHlavicky]; // globalni udaje
                  hlavicka = br.ReadBytes(delkaHlavicky);
                    // zaznamy 04 - 07
                  logPocetZaznamu = hlavicka[7] * 65536 * 256 + hlavicka[6] * 65536 + hlavicka[5] * 256 + hlavicka[4];

                  Int32 velikostHlavicky =  hlavicka[9] * 256 + hlavicka[8];
                  Int32 velikostZaznamu = hlavicka[11] * 256 + hlavicka[10];
                  Int32 pocetSloupcu = (velikostHlavicky / 32) - 1;
                    //8-9 velikost hlavickty
                      //9-10 velikost zaznamu
                  Int64 fyzPocetZaznamu = (DBFlength - velikostHlavicky) / velikostZaznamu;

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
