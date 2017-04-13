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

        public MainPage()
        {
            this.InitializeComponent();

            // Käynnistää sovelluksen hd-resoluutiolla
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.ViewModel = new Helikopteri.HelikopteriViewModel();

            // ----------------------------------------------
        /*List<Model.Helikopteri> list = new List<Model.Helikopteri>();

            list.Add(new Model.Helikopteri { nimi = "Kopu1" });
            list.Add(new Model.Helikopteri { nimi = "Kopu2" });

            listBox.ItemsSource = list;
            */
    }

        // Lisää helikopterin nimen nimikenttään, kun sitä klikkaa listasta
        private void listBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            Helikopteri helikopteri = (Helikopteri)e.ClickedItem;
            textBox.Text = helikopteri.nimi;
        }


        // Avaa "Lisää helikopteri" -näkymän
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Visible;
            Lisaa_helikopteri_border.Visibility = Visibility.Visible;
        }

        // Sulkee (peruuttaa) "Lisää helikopteri" -näkymän
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_helikopteri.Visibility = Visibility.Collapsed;
            Lisaa_helikopteri_border.Visibility = Visibility.Collapsed;
        }

        // Avaa "Lisää akku" -näkymän
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Visible;
            Lisaa_akku_border.Visibility = Visibility.Visible;
        }

        // Sulkee (peruuttaa) "Lisää akku" -näkymän
        private void button31_Click(object sender, RoutedEventArgs e)
        {
            Lisaa_akku.Visibility = Visibility.Collapsed;
            Lisaa_akku_border.Visibility = Visibility.Collapsed;
        }

        // Avaa "Haluatko varmasti poistaa valitun helikopterin?" -ikkunan
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Visible;
            Helikopterin_poisto_border.Visibility = Visibility.Visible; 
        }

        // Sulkee helikopterin poisto popupin
        private void button53_Click(object sender, RoutedEventArgs e)
        {
            Helikopterin_poisto.Visibility = Visibility.Collapsed;
            Helikopterin_poisto_border.Visibility = Visibility.Collapsed;
        }

        // Avaa "Haluatko varmasti poistaa valitun akun?" -ikkunan
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Visible;
            Akun_poisto_border.Visibility = Visibility.Visible;
        }

        // Sulkee akun poisto popupin
        private void button55_Click(object sender, RoutedEventArgs e)
        {
            Akun_poisto.Visibility = Visibility.Collapsed;
            Akun_poisto_border.Visibility = Visibility.Collapsed;
        }

    }
}
