﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{

    public enum uKartaState { show, add, edit };

    
    public partial class PracovniciKarta : Form
    {


        public class messager
        {
            public string prijmeni;
            public string jmeno;
            public string ulice;
            public string mesto;
            public string psc;
            public string telHome;
            public string oscislo;
            public string stredisko;

            public string cisZnamky;
            public string oddeleni;
            public string pracoviste;
            public string telZam;
            public string poznamka;

            public messager(string prijmeni, string jmeno, string ulice, string mesto, string psc, string telHome, string oscislo, string stredisko, string cisZnamky,
                            string oddeleni, string pracoviste, string telZam, string poznamka)
            {
                this.prijmeni = prijmeni;
                this.jmeno = jmeno;
                this.ulice = ulice;
                this.mesto = mesto;
                this.psc = psc;
                this.telHome = telHome;
                this.oscislo = oscislo;
                this.stredisko = stredisko;

                this.cisZnamky = cisZnamky;
                this.oddeleni = oddeleni;
                this.pracoviste = pracoviste;
                this.telZam = telZam;
                this.poznamka = poznamka;
            }
        }

        private vDatabase myDB;
        private uKartaState state;


        public PracovniciKarta(Hashtable DBRow, vDatabase myDataBase, uKartaState state = uKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            if (state == uKartaState.edit) setEditState();
            myDB = myDataBase;
            if (state == uKartaState.show)
            {
                buttonOK.Visible = false;
                buttonOK.Enabled = false;
                buttonCancel.Focus();
            }
            else
            {
                buttonOK.Visible = true;
                buttonOK.Enabled = true;
            }

            setData(DBRow);

            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
        }

        public PracovniciKarta(vDatabase myDataBase)
        {
            InitializeComponent();
            myDB = myDataBase;
            this.state = uKartaState.add;
            setAddState();
        }



        public void setData(Hashtable DBRow)
        {
            textBoxPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            textBoxJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            textBoxUlice.Text = Convert.ToString(DBRow["ulice"]);
            textBoxMesto.Text = Convert.ToString(DBRow["mesto"]);
            textBoxPSC.Text = Convert.ToString(DBRow["psc"]);
            textBoxTelDomu.Text = Convert.ToString(DBRow["telhome"]);
            textBoxOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            textBoxStredisko.Text = Convert.ToString(DBRow["stredisko"]);

            textBoxCisZnamky.Text = Convert.ToString(DBRow["cisznamky"]);

            textBoxOddeleni.Text = Convert.ToString(DBRow["odeleni"]);
            textBoxPracoviste.Text = Convert.ToString(DBRow["pracoviste"]);
            textBoxTelZamest.Text = Convert.ToString(DBRow["telzam"]);
            textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
        
        }
        private void setEditState()
        {
            setAddState();
            textBoxOsCislo.ReadOnly = true;
        }


        private void setAddState()
        {
            textBoxPrijmeni.ReadOnly = false;
            textBoxJmeno.ReadOnly = false;
            textBoxUlice.ReadOnly = false;
            textBoxMesto.ReadOnly = false;
            textBoxPSC.ReadOnly = false;
            textBoxTelDomu.ReadOnly = false;
            textBoxOsCislo.ReadOnly = false;
            textBoxStredisko.ReadOnly = false;
            textBoxCisZnamky.ReadOnly = false;
            textBoxOddeleni.ReadOnly = false;
            textBoxPracoviste.ReadOnly = false;
            textBoxTelZamest.ReadOnly = false;
            textBoxPoznamka.ReadOnly = false;
        }


        public messager getMesseger()
        {
            messager prepravka = new messager(textBoxPrijmeni.Text, textBoxJmeno.Text, textBoxUlice.Text, textBoxMesto.Text,
                                              textBoxPSC.Text,textBoxTelDomu.Text,textBoxOsCislo.Text,textBoxStredisko.Text,
                                              textBoxCisZnamky.Text, textBoxOddeleni.Text,textBoxPracoviste.Text,textBoxTelZamest.Text,
                                              textBoxPoznamka.Text);
            return prepravka;
        }

        private void PracovniciKarta_Activated(object sender, EventArgs e)
        {
            textBoxPrijmeni.Focus();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // stisknuti tlacitka OK
            if (textBoxOsCislo.Text.Trim() == "")
            {
                MessageBox.Show("Položka Osobní číslo není vyplněna.");
            }
            else
            {

                if (state == uKartaState.add)
                {
                    if (myDB.tableOsobyItemExist(textBoxOsCislo.Text.Trim()))
                    {
                        MessageBox.Show("Pracovník s tímto osobním číslem již existuje.");
                    }
                    else
                    {
                        buttonOK.DialogResult = DialogResult.OK;
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {

                    if (state == uKartaState.edit)
                    {
                        if (myDB.tableOsobyItemExist(textBoxOsCislo.Text.Trim()))
                        {
                            buttonOK.DialogResult = DialogResult.OK;
                            this.DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Pracovník s tímto osobním číslem již neexistuje.");
                        }


                    }


                }





            }



        }


    }
}
