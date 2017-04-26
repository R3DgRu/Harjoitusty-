using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Windows.Storage;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö helikoptereiden tietoja
    /// </summary>
    public class Helikopteri : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _nimi;
        public string nimi
        {
            get
            {
                return _nimi;
            }
            set
            {
                _nimi = value;
                RaisePropertyChanged("nimi");
            }
        }
        public string sarjanumero { get; set; } 
        public string vari { get; set; }
        public int lennot { get; set; }
        public int painelaakerit { get; set; }
        public int mHihna { get; set; }
        public int pHihna { get; set; }

        /// <summary>
        /// Tämä luokka säilöö helikopterien view model dataa
        /// </summary>
        public class HelikopteriViewModel
        {
            private ObservableCollection<Helikopteri> helikopterit = new ObservableCollection<Helikopteri>();
            public ObservableCollection<Helikopteri> Helikopterit { get { return helikopterit; } set { helikopterit = value; } }

            // HELIKOPTERI VIEWMODEL
            public HelikopteriViewModel()
            {
                
            }

            // LISÄÄ UUDEN HELIKOPTERIN
            public void LisaaHelikopteri(Helikopteri helikopteri)
            {
                helikopterit.Add(helikopteri);
                TallennaHelikopterit();
            }

            // POISTAA VALITUN HELIKOPTERIN
            public void PoistaHelikopteri(Helikopteri helikopteri)
            {
                Helikopterit.Remove(helikopteri);
                TallennaHelikopterit();
            }

            //PÄIVITTÄÄ VALITUN HELIKOPTERIN
            public void PaivitaHelikopteri()
            {
                TallennaHelikopterit();
            }

            // TALLENTAA HELIKOPTERIT TIEDOSTOON
            private async void TallennaHelikopterit()
            {
                try
                {
                    // luo tiedoston
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile helikopteritTiedosto = await storageFolder.CreateFileAsync("helikopterit.dat", CreationCollisionOption.ReplaceExisting);

                    // tallenna tiedot tiedostoon
                    Stream stream = await helikopteritTiedosto.OpenStreamForWriteAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Helikopteri>));
                    serializer.WriteObject(stream, Helikopterit);
                    await stream.FlushAsync();
                    stream.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Helikopterien tallennus epäonnistui." + ex.ToString());
                }
            }

            // LUKEE HELIKOPTERIT TIEDOSTOSTA
            public async void LueHelikopterit()
            {
                try
                {
                    // etsii tiedoston
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    Stream stream = await storageFolder.OpenStreamForReadAsync("helikopterit.dat");

                    // lukee tiedot
                    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Helikopteri>));
                    Helikopterit = (ObservableCollection<Helikopteri>)serializer.ReadObject(stream);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Helikopterien lukeminen epäonnistui." + ex.ToString());
                }
            }
        }

        // ---- RAISEPROPERTYCHANGED ----
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}