using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö helikoptereiden tietoja.
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

            // HELIKOPTERI VIEWMODEL
            public HelikopteriViewModel()
            {
                
            }

            // LISÄÄ UUDEN HELIKOPTERIN
            public void LisaaHelikopteri(Helikopteri helikopteri)
            {
                helikopterit.Add(helikopteri);
            }

            // POISTAA VALITUN HELIKOPTERIN
            public void RemoveHelikopteri(Helikopteri helikopteri)
            {
                Helikopterit.Remove(helikopteri);
            }

        }
    }
}