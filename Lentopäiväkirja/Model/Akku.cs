using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using Windows.Storage;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Lentopäiväkirja.Model
{
    /// <summary>
    /// Tämä luokka säilöö akkujen tietoja
    /// </summary>
    public class Akku : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _akkunimi;
        public string akkunimi
        {
            get
            {
                return _akkunimi;
            }
            set
            {
                _akkunimi = value;
                RaisePropertyChanged("akkunimi");
            }
        }
        public string jannite { get; set; }
        public string vari { get; set; }
        public string kapasiteetti { get; set; }
        public string pvm { get; set; }
        public int syklit { get; set; }

        /// <summary>
        /// Tämä luokka säilöö akkujen view modell dataa
        /// </summary>
        public class AkkuViewModell
        {
            private ObservableCollection<Akku> akut = new ObservableCollection<Akku>();
            public ObservableCollection<Akku> Akut { get { return akut; } set { akut = value; } }

            // AKKU VIEWMODELL
            public AkkuViewModell()
            {

            }

            // LISÄÄ UUDEN AKUN
            public void LisaaAkku(Akku akku)
            {
                akut.Add(akku);
                TallennaAkut();
            }

            // POISTAA VALITUN AKUN
            public void PoistaAkku(Akku akku)
            {
                Akut.Remove(akku);
                TallennaAkut();
            }

            //PÄIVITTÄÄ VALITUN AKUN
            public void PaivitaAkku()
            {
                TallennaAkut();
            }

            // TALLENTAA AKUT TIEDOSTOON
            private async void TallennaAkut()
            {
                try
                {
                    // luo tiedoston
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile akutTiedosto = await storageFolder.CreateFileAsync("akut.dat", CreationCollisionOption.ReplaceExisting);

                    // tallenna tiedot tiedostoon
                    Stream stream = await akutTiedosto.OpenStreamForWriteAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Akku>));
                    serializer.WriteObject(stream, Akut);
                    await stream.FlushAsync();
                    stream.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Akkujen tallennus epäonnistui." + ex.ToString());
                }
            }

            // LUKEE AKUT TIEDOSTOSTA
            public async void LueAkut()
            {
                try
                {
                    // etsii tiedoston
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    Stream stream = await storageFolder.OpenStreamForReadAsync("akut.dat");

                    // lukee tiedot
                    DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Akku>));
                    Akut = (ObservableCollection<Akku>)serializer.ReadObject(stream);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Akkujen lukeminen epäonnistui." + ex.ToString());
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