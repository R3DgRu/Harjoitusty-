using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö helikoptereiden tietoja
    /// </summary>
    class Helikopteri
    {
        public string nimi { get; set; }
        public string sarjanumero { get; set; }
        public string väri { get; set; }
        public int lennot { get; set; }
        public int painelaakerit { get; set; }
        public int mHihna { get; set; }
        public int pHihna { get; set; }


        /// <summary>
        /// This class holds employee view model data.
        /// </summary>
        public class HelikopteriTesti
        {
            public List<Helikopteri> helikopteri = new List<Helikopteri>();


            public HelikopteriTesti()
            {
                // testi dataa
                helikopteri.Add(new Helikopteri { nimi = "kopu1", sarjanumero = "hu345r", väri = "punainen", lennot = 0, mHihna = 0, pHihna = 0, painelaakerit = 0 });
                helikopteri.Add(new Helikopteri { nimi = "kopu2", sarjanumero = "hu05r", väri = "vihreä", lennot = 39, mHihna = 345, pHihna = 0, painelaakerit = 0 });
                helikopteri.Add(new Helikopteri { nimi = "kopu3", sarjanumero = "hu34u89", väri = "punaien", lennot = 4440, mHihna = 44, pHihna = 45, painelaakerit = 4 });
                helikopteri.Add(new Helikopteri { nimi = "kopu4", sarjanumero = "hu745r", väri = "punnen", lennot = 350, mHihna = 2, pHihna = 67, painelaakerit = 0 });
                helikopteri.Add(new Helikopteri { nimi = "kopu5", sarjanumero = "h2345r", väri = "painen", lennot = 3, mHihna = 62, pHihna = 3, painelaakerit = 50 });
                helikopteri.Add(new Helikopteri { nimi = "kopu6", sarjanumero = "hue45r", väri = "pun", lennot = 120, mHihna = 999, pHihna = 45, painelaakerit = 6 });
            }
        }

    }
}
