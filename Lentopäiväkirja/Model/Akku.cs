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


            public AkkuViewModell() // Kysymys: Miten se lisääminen/poistaminen ohjelman sisältä käsin tehdään?
            {
                // testi dataa
                // akut.Add(new Akku { akkunimi = "akku1", jannite = "22,2", vari = "punainen", kapasiteetti = "2100", pvm = "28.5.2013", syklit = 0, ika = 150 });
              
            }

            // Lisää uuden akun
            public void LisaaAkku(Akku akku)
            {
                akut.Add(akku);
            }

            // Poistaa valitun akun
            public void RemoveAkku(Akku akku)
            {
                Akut.Remove(akku);
            }


        }
    }
}
