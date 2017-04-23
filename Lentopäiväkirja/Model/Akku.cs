using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

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
            private ObservableCollection<Akku> akut = new ObservableCollection<Akku>();
            public ObservableCollection<Akku> Akut { get { return akut; } }

            // AKKU VIEWMODELL
            public AkkuViewModell()
            {

            }

            // LISÄÄ UUDEN AKUN
            public void LisaaAkku(Akku akku)
            {
                akut.Add(akku);
            }

            // POISTAA VALITUN AKUN
            public void RemoveAkku(Akku akku)
            {
                Akut.Remove(akku);
            }
        }
    }
}