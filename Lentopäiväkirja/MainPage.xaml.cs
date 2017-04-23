using Lentopäiväkirja.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lentopäiväkirja
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Helikopteri.HelikopteriViewModel ViewModel { get; set; }
        public Akku.AkkuViewModell ViewModell { get; set; }

        public MainPage()
        {
            // KÄYNNISTÄÄ SOVELLUKSEN HD-RESOLUUTIOLLA
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.InitializeComponent();
            this.ViewModel = new Helikopteri.HelikopteriViewModel();
            this.ViewModell = new Akku.AkkuViewModell();
    }
        // -------------------------------------- HELIKOPTERI --------------------------------------

        // LISÄÄ HELIKOPTERIN TIEDOT OHJELMAAN, KUN SITÄ KLIKKAA LISTASTA
        private void listBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Helikopteri helikopteri = (Helikopteri)e.ClickedItem;
                textBox.Text = helikopteri.nimi;
                textBox1.Text = helikopteri.sarjanumero;
                textBlock5.Text = helikopteri.lennot.ToString();
                textBlock8.Text = helikopteri.painelaakerit.ToString();
                textBlock10.Text = helikopteri.mHihna.ToString();
                textBlock12.Text = helikopteri.pHihna.ToString();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Helikopterin lisääminen listaan epäonnistui." + ex.ToString());
            }
        }

        // LISÄÄ UUDEN HELIKOPTERIN TIETORAKENTEESEEN
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri uusihelikopteri = new Helikopteri();
                uusihelikopteri.nimi = uusinimi.Text;
                uusihelikopteri.sarjanumero = uusinumero.Text;
                ViewModel.LisaaHelikopteri(uusihelikopteri);
                Lisaa_helikopteri.Visibility = Visibility.Collapsed;
                Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;

                SaveHelikopterit(); // tallentaa helikopterit tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Helikopterin lisääminen tietorakenteeseen epäonnistui." + ex.ToString());
            }
        }

        // POISTAA VALITUN HELIKOPTERIN
        private void button54_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri helikopteri = (Helikopteri)listBox.SelectedItem;
                ViewModel.RemoveHelikopteri(helikopteri);
                Helikopterin_poisto.Visibility = Visibility.Collapsed;
                Helikopterin_poisto_border.Visibility = Visibility.Collapsed;

                SaveHelikopterit(); // tallentaa helikopterit tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Helikopterin poistaminen epäonnistui." + ex.ToString());
            }
        }

        // PÄIVITTÄÄ VALITUN HELIKOPTERIN TIEDOT
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri helikopteri = (Helikopteri)listBox.SelectedItem;
                helikopteri.nimi = textBox.Text;
                helikopteri.sarjanumero = textBox1.Text;
                //listBox.ItemTemplate = HelikopteriListDataTemplate;
                //listBox.SelectedItem = textBox1.Text;

                SaveHelikopterit(); // tallentaa helikopterit tiedostoon
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Helikopterin tietojen päivittäminen epäonnistui." + ex.ToString());
            }
        }

        // LISÄÄ LENTOJA
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri helikopteri = (Helikopteri)listBox2.SelectedItem;
                // lentojen lisäys
                int value = 0; // määrittelee value muuttujan
                value = Convert.ToInt32(textBox6.Text); // ottaa textbox6:sta merkkijonon ja muuttaa sen numeroiksi ja tunkee value muuttujaan
                int value2 = 0; // määrittelee value2 muuttujan
                value2 = Convert.ToInt32(textBlock5.Text); // ottaa textblock5:stä merkkijonon ja muuttaa sen numeroiksi ja tunkee value2 muuttujaan
                value = value2 + value; // laskee yhteen molempien muuttujien arvot ja tunkee ne lopuksi value muuttujaan
                helikopteri.lennot = value; // laittaa helikopterin lentoihin valuen sisältämän arvon
                textBlock5.Text = helikopteri.lennot.ToString(); // muuttaa akun syklit merkkijonoksi ja sijoittaa sen textblock5:een

                // painelaakerien lisäys
                int value3 = 0;
                value3 = Convert.ToInt32(textBox6.Text);
                int value4 = 0;
                value4 = Convert.ToInt32(textBlock8.Text);
                value3 = value4 + value3;
                helikopteri.painelaakerit = value3;
                textBlock8.Text = helikopteri.painelaakerit.ToString();

                // moottorin hihnan lisäys
                int value5 = 0;
                value5 = Convert.ToInt32(textBox6.Text);
                int value6 = 0;
                value6 = Convert.ToInt32(textBlock10.Text);
                value5 = value6 + value5;
                helikopteri.mHihna = value5;
                textBlock10.Text = helikopteri.mHihna.ToString();

                // perän hihnan lisäys
                int value7 = 0;
                value7 = Convert.ToInt32(textBox6.Text);
                int value8 = 0;
                value8 = Convert.ToInt32(textBlock12.Text);
                value7 = value8 + value7;
                helikopteri.pHihna = value7;
                textBlock12.Text = helikopteri.pHihna.ToString();

                SaveHelikopterit(); // tallentaa helikopterit tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Helikopterin lentojen lisäys epäonnistui." + ex.ToString());
            }
        }

        // NOLLAA VALITUT KULUTUSOSAT
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri helikopteri = (Helikopteri)listBox.SelectedItem;
                // painelaakerien nollaus
                bool check = checkBox.IsChecked ?? false;
                if (check)
                {
                    int value8 = 0;
                    value8 = Convert.ToInt32(textBlock8.Text);
                    value8 = 0;
                    helikopteri.painelaakerit = value8;
                    textBlock8.Text = helikopteri.painelaakerit.ToString();

                    SaveHelikopterit(); // tallentaa helikopterit tiedostoon
                }

                // moottorin hihnan nollaus
                bool check2 = checkBox1.IsChecked ?? false;
                if (check2)
                {
                    int value9 = 0;
                    value9 = Convert.ToInt32(textBlock10.Text);
                    value9 = 0;
                    helikopteri.mHihna = value9;
                    textBlock10.Text = helikopteri.mHihna.ToString();

                    SaveHelikopterit(); // tallentaa helikopterit tiedostoon
                }

                // perän hihnan nollaus
                bool check3 = checkBox2.IsChecked ?? false;
                if (check3)
                {
                    int value9 = 0;
                    value9 = Convert.ToInt32(textBlock12.Text);
                    value9 = 0;
                    helikopteri.pHihna = value9;
                    textBlock12.Text = helikopteri.pHihna.ToString();

                    SaveHelikopterit(); // tallentaa helikopterit tiedostoon
                }

                else
                {
                    // älä tee mitään :)
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Helikopterin kulutusosien nollaus epäonnistui." + ex.ToString());
            }
        }

        // AVAA LISÄÄ HELIKOPTERI NÄKYMÄN
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Visible;
            Lisaa_helikopteri_border.Visibility = Visibility.Visible;
        }

        // SULKEE LISÄÄ HELIKOPTERI NÄKYMÄN
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Collapsed;
            Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;
        }

        // AVAA HELIKOPTERIN POISTOIKKUNAN
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Visible;
            Helikopterin_poisto_border.Visibility = Visibility.Visible; 
        }
        // SULKEE HELIKOPTERIN POISTOIKKUNAN
        private void button53_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Collapsed;
            Helikopterin_poisto_border.Visibility = Visibility.Collapsed;
        }

        // TALLENTAA HELIKOPTERIT TIEDOSTOON
        private async void SaveHelikopterit()
        {
            try
            {
                // avaa tai luo tiedosto
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile employeesFile = await storageFolder.CreateFileAsync("helikopterit.dat", CreationCollisionOption.OpenIfExists);

                // tallenna systeemit tiedostoon
                Stream stream = await employeesFile.OpenStreamForWriteAsync();
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Helikopteri>));
                serializer.WriteObject(stream, ViewModel.Helikopterit);
                await stream.FlushAsync();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Helikopterien tallennus epäonnistui." + ex.ToString());
            }
        }
        // -------------------------------------- AKKU --------------------------------------

        // LISÄÄ AKUN TIEDOT OHJELMAAN, KUN SITÄ KLIKKAA LISTASTA
        private void listBox1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Akku akku = (Akku)e.ClickedItem;
            textBox2.Text = akku.akkunimi;
            textBox3.Text = akku.jannite;
            textBox4.Text = akku.kapasiteetti;
            textBox5.Text = akku.pvm;
            textBlock19.Text = akku.syklit.ToString();
        }

        // LISÄÄ UUDEN AKUN TIETORAKENTEESEEN
        private void button32_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Akku uusiakku = new Akku();
                uusiakku.akkunimi = uusiakkunimi.Text;
                uusiakku.pvm = uusipvm.Text;
                uusiakku.jannite = uusijannite.Text;
                uusiakku.kapasiteetti = uusikapasiteetti.Text;
                ViewModell.LisaaAkku(uusiakku);

                Lisaa_akku.Visibility = Visibility.Collapsed;
                Lisaa_akku_border.Visibility = Visibility.Collapsed;

                SaveAkut(); // tallentaa akut tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Akun lisääminen tietorakenteeseen epäonnistui." + ex.ToString());
            }
        }

        // POISTAA VALITUN AKUN
        private void button56_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Akku akku = (Akku)listBox1.SelectedItem;
                ViewModell.RemoveAkku(akku);
            
                Akun_poisto.Visibility = Visibility.Collapsed;
                Akun_poisto_border.Visibility = Visibility.Collapsed;

                SaveAkut(); // tallentaa akut tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Akun poisto epäonnistui." + ex.ToString());
            }
        }

        // PÄIVITTÄÄ VALITUN AKUN TIEDOT
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Akku akku = (Akku)listBox1.SelectedItem;
                akku.akkunimi = textBox2.Text;
                akku.jannite = textBox3.Text;
                akku.kapasiteetti = textBox4.Text;
                akku.pvm = textBox5.Text;

                SaveAkut(); // tallentaa akut tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Akun tietojen päivittäminen epäonnistui." + ex.ToString());
            }
        }

        // LISÄÄ SYKLEJÄ
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Akku akku = (Akku)listBox3.SelectedItem;
                int value10 = 0; // määrittelee value10 muuttujan
                value10 = Convert.ToInt32(textBox7.Text); // ottaa textbox7:sta merkkijonon ja muuttaa sen numeroiksi ja tunkee value10 muuttujaan
                int value11 = 0; // määrittelee value11 muuttujan
                value11 = Convert.ToInt32(textBlock19.Text); // ottaa textblock19:sta merkkijonon ja muuttaa sen numeroiksi ja tunkee value11 muuttujaan
                value10 = value11 + value10; // laskee yhteen molempien muuttujien arvot ja tunkee ne lopuksi value10 muuttujaan
                akku.syklit = value10; // laittaa akun sykleihin value10:n sisältämän arvon
                textBlock19.Text = akku.syklit.ToString(); // muuttaa akun syklit merkkijonoksi ja sijoittaa ne textblock19:sta

                SaveAkut(); // tallentaa akut tiedostoon
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Akun syklien lisäys epäonnistui." + ex.ToString());
            }
        }

        // AVAA LISÄÄ AKKU NÄKYMÄN
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Visible;
            Lisaa_akku_border.Visibility = Visibility.Visible;
        }
        // SULKEE LISÄÄ AKKU NÄKYMÄN
        private void button31_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Collapsed;
            Lisaa_akku_border.Visibility = Visibility.Collapsed;
        }

        // AVAA AKUN POISTOIKKUNAN
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Visible;
            Akun_poisto_border.Visibility = Visibility.Visible;
        }
        // SULKEE AKUN POISTOIKKUNAN
        private void button55_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Collapsed;
            Akun_poisto_border.Visibility = Visibility.Collapsed;
        }

        // TALLENTAA AKUT TIEDOSTOON
        private async void SaveAkut()
        {
            try
            {
                // avaa tai luo tiedosto
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile employeesFile = await storageFolder.CreateFileAsync("akut.dat", CreationCollisionOption.OpenIfExists);

                // tallenna systeemit tiedostoon
                Stream stream = await employeesFile.OpenStreamForWriteAsync();
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Akku>));
                serializer.WriteObject(stream, ViewModell.Akut);
                await stream.FlushAsync();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Akkujen tallennus epäonnistui." + ex.ToString());
            }
        }


        // ---------------------------------------------------
        /*
        private async void ReadHelikopterit()
        {
            try
            {
                // find a file
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                Stream stream = await storageFolder.OpenStreamForReadAsync("helikopterit.dat");

                // is it empty
                // if (stream == null) Helikopteri = new ObservableCollection<Helikopteri>();

                // read data
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<Helikopteri>));
                ViewModel.Helikopterit = (ObservableCollection<Helikopteri>)serializer.ReadObject(stream);
                //ShowEmployees();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Following exception has happend (reading): " + ex.ToString());
            }
        }

        private void ShowHelikopterit()
        {
            listBox.ItemTemplate = "Employees:" + Environment.NewLine;
            foreach (Helikopteri helikopteri in helikopterit)
            {
                // EmployeesTextBlock.Text += employee.Firstname + " " + employee.Lastname + Environment.NewLine;
            }
        }
        */
    }
}