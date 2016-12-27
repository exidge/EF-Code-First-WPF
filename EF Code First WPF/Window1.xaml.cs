using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EF_Code_First_WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool czyDodac;
        private int _id;
        public Window1()
        {
            //InitializeComponent();
        }
        public Window1(bool add)
        {
            InitializeComponent();
            czyDodac = add;
            if (add)
            {
                buttonExecute.Content = "Dodaj";
                this.Title = "Dodaj uczestnika";
            }
            else
            {
                buttonExecute.Content = "Edytuj";
                this.Title = "Edytuj uczestnika";
            }
            
        }
        public Window1(bool add, int id)
        {
            InitializeComponent();
            czyDodac = add;
            this._id = id;
            buttonExecute.Content = "Edytuj";
            this.Title = "Edytuj uczestnika";
            
        }

        private void WyswietlJednego(int id)
        {
            Uczestnik doWyswietlenia = Baza.context.Uczestnicy.First(e => e.ID == id);
            textBoxImie.Text = doWyswietlenia.Imie;
            textBoxNazwisko.Text = doWyswietlenia.Nazwisko;
            checkBoxObiad.IsChecked = doWyswietlenia.takObiad;
            checkBoxNocleg.IsChecked = doWyswietlenia.takNocleg;
            checkBoxOplata.IsChecked = doWyswietlenia.takOplata;
            DateTimePickerPrzyjazd.Value = doWyswietlenia.dataPrzyjazdu;
            DateTimePickerOdjazd.Value = doWyswietlenia.dataOdjazdu;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!czyDodac)
            {
                WyswietlJednego(_id);
            }
        }
        private void Edytuj(Uczestnik uczestnik)
        {
            var local = Baza.context.Set<Uczestnik>()
                    .Local
                    .FirstOrDefault(e => e.ID == uczestnik.ID);
            if (local != null)
            {
                Baza.context.Entry(local).State = EntityState.Detached;
            }
            var entry = Baza.context.Entry(uczestnik);
            entry.State = EntityState.Modified;
            Baza.context.SaveChanges();
        }
        private void Dodaj(Uczestnik osoba)
        {
            Baza.context.Uczestnicy.Add(osoba);
            Baza.context.SaveChanges();
        }
        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            Uczestnik doDodania = new Uczestnik();
            doDodania.ID = this._id;
            doDodania.Imie = textBoxImie.Text;
            doDodania.Nazwisko = textBoxNazwisko.Text;
            doDodania.takObiad = (bool)checkBoxObiad.IsChecked;
            doDodania.takNocleg = (bool)checkBoxNocleg.IsChecked;
            doDodania.takOplata = (bool)checkBoxOplata.IsChecked;
            doDodania.dataPrzyjazdu = (DateTime)DateTimePickerPrzyjazd.Value;
            doDodania.dataOdjazdu = (DateTime)DateTimePickerOdjazd.Value;
            if (!czyDodac)
            {
                Edytuj(doDodania);
            }
            else
            {
                Dodaj(doDodania);
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
