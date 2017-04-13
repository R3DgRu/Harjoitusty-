using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö helikoptereiden tietoja
    /// </summary>
    public class Helikopteri
    {
        public string nimi { get; set; }
        public string sarjanumero { get; set; }
        public string vari { get; set; }
        public int lennot { get; set; }
        public int painelaakerit { get; set; }
        public int mHihna { get; set; }
        public int pHihna { get; set; }


        /// <summary>
        /// Tämä luokka säilöö helikopteri view model dataa.
        /// </summary>
        public class HelikopteriViewModel
        {
            //private List<Helikopteri> helikopterit = new List<Helikopteri>();
            //public List<Helikopteri> Helikopterit { get { return helikopterit; } }
            private ObservableCollection<Helikopteri> helikopterit = new ObservableCollection<Helikopteri>();
            public ObservableCollection<Helikopteri> Helikopterit { get { return helikopterit; } }


            public HelikopteriViewModel()
            {
                // testi dataa
                helikopterit.Add(new Helikopteri { nimi = "kopu1", sarjanumero = "p345r", vari = "punainen", lennot = 0, mHihna = 0, pHihna = 0, painelaakerit = 0 });
                helikopterit.Add(new Helikopteri { nimi = "kopu2", sarjanumero = "hu05r", vari = "turkoosi", lennot = 39, mHihna = 345, pHihna = 0, painelaakerit = 0 });
                helikopterit.Add(new Helikopteri { nimi = "kopu3", sarjanumero = "gz4u9", vari = "sininen", lennot = 4440, mHihna = 44, pHihna = 45, painelaakerit = 4 });
                helikopterit.Add(new Helikopteri { nimi = "kopu4", sarjanumero = "u745r", vari = "keltainen", lennot = 350, mHihna = 2, pHihna = 67, painelaakerit = 0 });
                helikopterit.Add(new Helikopteri { nimi = "kopu5", sarjanumero = "n2345r", vari = "violetti", lennot = 3, mHihna = 62, pHihna = 3, painelaakerit = 50 });
                helikopterit.Add(new Helikopteri { nimi = "kopu6", sarjanumero = "6lu45r", vari = "musta", lennot = 120, mHihna = 999, pHihna = 45, painelaakerit = 6 });
            }
        }

    }
}
