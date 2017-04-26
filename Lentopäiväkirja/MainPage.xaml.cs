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
using Windows.UI;

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
            ViewModel.LueHelikopterit();
            ViewModell.LueAkut();
    }
        // -------------------------------------- HELIKOPTERI --------------------------------------

        // LISÄÄ HELIKOPTERIN TIEDOT OHJELMAAN, KUN SITÄ KLIKKAA LISTASTA
        private void listBox_ItemClick(object sender, ItemClickEventArgs e)
        {   
            Helikopteri helikopteri = (Helikopteri)e.ClickedItem;
            textBox.Text = helikopteri.nimi;
            textBox1.Text = helikopteri.sarjanumero;
            textBlock5.Text = helikopteri.lennot.ToString();
            textBlock8.Text = helikopteri.painelaakerit.ToString();
            textBlock10.Text = helikopteri.mHihna.ToString();
            textBlock12.Text = helikopteri.pHihna.ToString();
            // lisää värit ohjelmaan
            Color color = (Color)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Color), helikopteri.vari);
            Windows.UI.Xaml.Media.SolidColorBrush scb = new SolidColorBrush(color);
            textBox.BorderBrush = scb;
            textBox1.BorderBrush = scb;
            button2.BorderBrush = scb;
            button3.BorderBrush = scb;
            button4.BorderBrush = scb;
        }

        // VALITSEE HELIKOPTERIN LENTOJEN LISÄYSLISTASTA
        private void listBox2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Helikopteri helikopteri = (Helikopteri)e.ClickedItem;
            textBox.Text = helikopteri.nimi;
            textBox1.Text = helikopteri.sarjanumero;
            textBlock5.Text = helikopteri.lennot.ToString();
            textBlock8.Text = helikopteri.painelaakerit.ToString();
            textBlock10.Text = helikopteri.mHihna.ToString();
            textBlock12.Text = helikopteri.pHihna.ToString();
            // lisää värit ohjelmaan
            Color color = (Color)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Color), helikopteri.vari);
            Windows.UI.Xaml.Media.SolidColorBrush scb = new SolidColorBrush(color);
            textBox.BorderBrush = scb;
            textBox1.BorderBrush = scb;
            textBox6.BorderBrush = scb;
            button2.BorderBrush = scb;
            button3.BorderBrush = scb;
            button4.BorderBrush = scb;
            button7.BorderBrush = scb;
        }

        // LISÄÄ UUDEN HELIKOPTERIN TIETORAKENTEESEEN
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helikopteri uusihelikopteri = new Helikopteri();
                uusihelikopteri.nimi = uusinimi.Text;
                uusihelikopteri.sarjanumero = uusinumero.Text;
                // tallentaa värit
                SolidColorBrush vari = (SolidColorBrush)kopt_varivalitsin.Fill;
                uusihelikopteri.vari = vari.Color.ToString();
                
                ViewModel.LisaaHelikopteri(uusihelikopteri);
                Lisaa_helikopteri.Visibility = Visibility.Collapsed;
                Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;
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
                ViewModel.PoistaHelikopteri(helikopteri);
                Helikopterin_poisto.Visibility = Visibility.Collapsed;
                Helikopterin_poisto_border.Visibility = Visibility.Collapsed;
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
                ViewModel.PaivitaHelikopteri();
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
                int value = 0;
                value = Convert.ToInt32(textBox6.Text);
                int value2 = 0;
                value2 = Convert.ToInt32(textBlock5.Text);
                value = value2 + value;
                helikopteri.lennot = value;
                textBlock5.Text = helikopteri.lennot.ToString();

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

                ViewModel.PaivitaHelikopteri();
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

                bool check = checkBox.IsChecked ?? false;
                if (check)
                {
                    int value8 = 0;
                    value8 = Convert.ToInt32(textBlock8.Text);
                    value8 = 0;
                    helikopteri.painelaakerit = value8;
                    textBlock8.Text = helikopteri.painelaakerit.ToString();
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
                }

                else
                {
                    // älä tee mitään :)
                }

                ViewModel.PaivitaHelikopteri();
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
            // lisää värit ohjelmaan
            Color color = (Color)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Color), akku.vari);
            Windows.UI.Xaml.Media.SolidColorBrush scb = new SolidColorBrush(color);
            textBox2.BorderBrush = scb;
            textBox3.BorderBrush = scb;
            textBox4.BorderBrush = scb;
            textBox5.BorderBrush = scb;
            button5.BorderBrush = scb;
            button6.BorderBrush = scb;
        }

        // VALITSEE AKUN SYKLIEN LISÄYSLISTASTA
        private void listBox3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Akku akku = (Akku)e.ClickedItem;
            textBox2.Text = akku.akkunimi;
            textBox3.Text = akku.jannite;
            textBox4.Text = akku.kapasiteetti;
            textBox5.Text = akku.pvm;
            textBlock19.Text = akku.syklit.ToString();
            // lisää värit ohjelmaan
            Color color = (Color)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Color), akku.vari);
            Windows.UI.Xaml.Media.SolidColorBrush scb = new SolidColorBrush(color);
            textBox2.BorderBrush = scb;
            textBox3.BorderBrush = scb;
            textBox4.BorderBrush = scb;
            textBox5.BorderBrush = scb;
            textBox7.BorderBrush = scb;
            button5.BorderBrush = scb;
            button6.BorderBrush = scb;
            button8.BorderBrush = scb;
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
                // tallentaa värit
                SolidColorBrush vari = (SolidColorBrush)akun_varivalitsin.Fill;
                uusiakku.vari = vari.Color.ToString();

                ViewModell.LisaaAkku(uusiakku);
                Lisaa_akku.Visibility = Visibility.Collapsed;
                Lisaa_akku_border.Visibility = Visibility.Collapsed;
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
                ViewModell.PoistaAkku(akku);
                Akun_poisto.Visibility = Visibility.Collapsed;
                Akun_poisto_border.Visibility = Visibility.Collapsed;
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

                ViewModell.PaivitaAkku();
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
                int value10 = 0;
                value10 = Convert.ToInt32(textBox7.Text);
                int value11 = 0;
                value11 = Convert.ToInt32(textBlock19.Text);
                value10 = value11 + value10;
                akku.syklit = value10;
                textBlock19.Text = akku.syklit.ToString();

                ViewModell.PaivitaAkku();
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

        // --------------------------------- VÄRIT -----------------------------------

        // VÄRINVALINNAT HELIKOPTEREIHIN
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 240, 132, 132));
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 230, 120, 169));
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 198, 116, 219));
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 136, 122, 217));
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 92, 156, 224));
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 123, 200, 224));
        }

        private void button17_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 115, 230, 207));
        }

        private void button18_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 107, 224, 158));
        }

        private void button19_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 105, 191, 122));
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 161, 219, 94));
        }

        private void button21_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 209, 237, 133));
        }

        private void button22_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 235, 235, 99));
        }

        private void button23_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 237, 216, 83));
        }

        private void button24_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 235, 190, 54));
        }

        private void button25_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 247, 168, 10));
        }

        private void button26_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 245, 118, 8));
        }

        private void button27_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 242, 86, 39));
        }

        private void button28_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 224, 81, 81));
        }

        private void button29_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 184, 177, 184));
        }

        private void button30_Click(object sender, RoutedEventArgs e)
        {
            kopt_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 117, 117, 117));
        }

        // VÄRINVALINNAT AKKUIHIN
        private void button33_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 240, 132, 132));
        }

        private void button34_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 230, 120, 169));
        }

        private void button35_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 198, 116, 219));
        }

        private void button36_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 136, 122, 217));
        }

        private void button37_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 92, 156, 224));
        }

        private void button38_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 123, 200, 224));
        }

        private void button39_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 115, 230, 207));
        }

        private void button40_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 107, 224, 158));
        }

        private void button41_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 105, 191, 122));
        }

        private void button42_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 161, 219, 94));
        }

        private void button43_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 209, 237, 133));
        }

        private void button44_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 235, 235, 99));
        }

        private void button45_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 237, 216, 83));
        }

        private void button46_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 235, 190, 54));
        }

        private void button47_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 247, 168, 10));
        }

        private void button48_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 245, 118, 8));
        }

        private void button49_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 242, 86, 39));
        }

        private void button50_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 224, 81, 81));
        }

        private void button51_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 184, 177, 184));
        }

        private void button52_Click(object sender, RoutedEventArgs e)
        {
            akun_varivalitsin.Fill = new SolidColorBrush(Color.FromArgb(200, 117, 117, 117));
        }
    }
}