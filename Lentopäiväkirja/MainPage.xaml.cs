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
            this.InitializeComponent();

            // Käynnistää sovelluksen hd-resoluutiolla
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.ViewModel = new Helikopteri.HelikopteriViewModel();
            this.ViewModell = new Akku.AkkuViewModell();

    }

        // -------------------------------------- HELIKOPTERI --------------------------------------

        // Lisää helikopterin tiedot ohjelmaan, kun sitä klikkaa listasta
        private void listBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            Helikopteri helikopteri = (Helikopteri)e.ClickedItem;
            textBox.Text = helikopteri.nimi;
            textBox1.Text = helikopteri.sarjanumero;
            textBlock5.Text = helikopteri.lennot.ToString();
            textBlock8.Text = helikopteri.painelaakerit.ToString();
            textBlock10.Text = helikopteri.mHihna.ToString();
            textBlock12.Text = helikopteri.pHihna.ToString();
        }

        // Lisää uuden helikopterin tietorakenteeseen
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            Helikopteri uusihelikopteri = new Helikopteri();
            uusihelikopteri.nimi = uusinimi.Text;
            uusihelikopteri.sarjanumero = uusinumero.Text;
            ViewModel.LisaaHelikopteri(uusihelikopteri);
            Lisaa_helikopteri.Visibility = Visibility.Collapsed;
            Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;
        }

        // Poistaa valitun helikopterin
        private void button54_Click(object sender, RoutedEventArgs e)
        {
            Helikopteri helikopteri = (Helikopteri)listBox.SelectedItem;
            ViewModel.RemoveHelikopteri(helikopteri);
            Helikopterin_poisto.Visibility = Visibility.Collapsed;
            Helikopterin_poisto_border.Visibility = Visibility.Collapsed;
        }

        // Päivittää valitun helikopterin tiedot
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Helikopteri helikopteri = (Helikopteri)listBox.SelectedItem;
            helikopteri.nimi = textBox.Text;
            helikopteri.sarjanumero = textBox1.Text;
        }

        // Lisää lentoja
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            // kohde textBlock5
        }

        // Avaa "Lisää helikopteri" -näkymän
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Visible;
            Lisaa_helikopteri_border.Visibility = Visibility.Visible;
        }

        // Sulkee "Lisää helikopteri" -näkymän
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Collapsed;
            Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;
        }

        // Avaa helikopterin poistoikkunan
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Visible;
            Helikopterin_poisto_border.Visibility = Visibility.Visible; 
        }
        // Sulkee helikopterin poistoikkunan
        private void button53_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Collapsed;
            Helikopterin_poisto_border.Visibility = Visibility.Collapsed;
        }

        // -------------------------------------- AKKU --------------------------------------

        // Lisää akun tiedot ohjelmaan, kun sitä klikkaa listasta
        private void listBox1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Akku akku = (Akku)e.ClickedItem;
            textBox2.Text = akku.akkunimi;
            textBox3.Text = akku.jannite;
            textBox4.Text = akku.kapasiteetti;
            textBox5.Text = akku.pvm;
            textBlock19.Text = akku.syklit.ToString();
            textBlock21.Text = akku.ika.ToString();
        }

        // Lisää uuden akun tietorakenteeseen
        private void button32_Click(object sender, RoutedEventArgs e)
        {
            Akku uusiakku = new Akku();
            uusiakku.akkunimi = uusiakkunimi.Text;
            uusiakku.pvm = uusipvm.Text;
            uusiakku.jannite = uusijannite.Text;
            uusiakku.kapasiteetti = uusikapasiteetti.Text;
            ViewModell.LisaaAkku(uusiakku);

            Lisaa_akku.Visibility = Visibility.Collapsed;
            Lisaa_akku_border.Visibility = Visibility.Collapsed;
        }

        // Poistaa valitun akun
        private void button56_Click(object sender, RoutedEventArgs e)
        {
            Akku akku = (Akku)listBox1.SelectedItem;
            ViewModell.RemoveAkku(akku);

            Akun_poisto.Visibility = Visibility.Collapsed;
            Akun_poisto_border.Visibility = Visibility.Collapsed;
        }

        // Päivittää valitun akun tiedot
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Akku akku = (Akku)listBox1.SelectedItem;
            akku.akkunimi = textBox2.Text;
            akku.jannite = textBox3.Text;
            akku.kapasiteetti = textBox4.Text;
            akku.pvm = textBox5.Text;
        }

        // Lisää syklejä
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            // kohde textBlock19, lähde textBox7
            Akku akku = (Akku)listBox3.SelectedItem;


            textBlock19.Text = akku.syklit.ToString();
        }

        // Avaa "Lisää akku" -näkymän
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Visible;
            Lisaa_akku_border.Visibility = Visibility.Visible;
        }
        // Sulkee "Lisää akku" -näkymän
        private void button31_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Collapsed;
            Lisaa_akku_border.Visibility = Visibility.Collapsed;
        }

        // Avaa akun poistoikkunan
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Visible;
            Akun_poisto_border.Visibility = Visibility.Visible;
        }
        // Sulkee akun poistoikkunan
        private void button55_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Collapsed;
            Akun_poisto_border.Visibility = Visibility.Collapsed;
        }

    }
}
