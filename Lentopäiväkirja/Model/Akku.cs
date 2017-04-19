using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö akkujen tietoja.
    /// </summary>
    public class Akku
    {

        public string akkunimi { get; set; }
        public string jannite { get; set; }
        public string vari { get; set; }
        public string kapasiteetti { get; set; }
        public string pvm { get; set; }
        public int syklit { get; set; }
        public int ika { get; set; }


        /// <summary>
        /// Tämä luokka säilöö akku view modell dataa.
        /// </summary>
        public class AkkuViewModell
        {
            //private List<Akku> akut = new List<Akku>();
            //public List<Akku> Akut { get { return akut; } }
            private ObservableCollection<Akku> akut = new ObservableCollection<Akku>();
            public ObservableCollection<Akku> Akut { get { return akut; } }


            public AkkuViewModell() // Kysymys: Miten nämä saa tallennettua johonkin siten, että käyttäjä syöttää arvot eikä niitä aseteta tässä? Lisäksi miten se lisääminen/poistaminen ohjelman sisältä käsin tehdään?
            {
                // testi dataa
                akut.Add(new Akku { akkunimi = "akku1", jannite = "22,2", vari = "punainen", kapasiteetti = "2100", pvm = "28.5.2013", syklit = 0, ika = 150 });
                akut.Add(new Akku { akkunimi = "akku2", jannite = "14,8", vari = "turkoosi", kapasiteetti = "1800", pvm = "3.12.2011", syklit = 0, ika = 110 });
                akut.Add(new Akku { akkunimi = "akku3", jannite = "3,7", vari = "sininen", kapasiteetti = "4500", pvm = "30.10.2016", syklit = 45, ika = 148 });
                akut.Add(new Akku { akkunimi = "akku4", jannite = "7,4", vari = "keltainen", kapasiteetti = "5000", pvm = "15.2.2014", syklit = 67, ika = 120 });
                akut.Add(new Akku { akkunimi = "akku5", jannite = "22,2", vari = "violetti", kapasiteetti = "1200", pvm = "8.6.2015", syklit = 3, ika = 260 });
                akut.Add(new Akku { akkunimi = "akku6", jannite = "11,1", vari = "musta", kapasiteetti = "2400", pvm = "19.1.2012", syklit = 45, ika = 600 });
            }
        }
    }
}
