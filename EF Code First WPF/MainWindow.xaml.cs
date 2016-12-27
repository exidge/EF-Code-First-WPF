using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EF_Code_First_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            Baza.initContext();
            dataGrid.ItemsSource = Baza.context.Uczestnicy.ToList();
        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window1 show = new Window1(true);
            show.ShowDialog();
            if (show.DialogResult.Value == true)
            {
                gridItemsRefresh();
                MessageBox.Show("Użytkownik został dodany.");
            }
        }

        private void buttonEdytuj_Click(object sender, RoutedEventArgs e)
        {
            Uczestnik selected = (Uczestnik)dataGrid.SelectedItem;
            try
            {
                int selectedID = selected.ID;
                Window1 show = new Window1(false, selectedID);
                show.ShowDialog();
                if (show.DialogResult.Value==true)
                {
                    gridItemsRefresh();
                    MessageBox.Show("Użytkownik został zmieniony.");
                }
            }
            catch(NullReferenceException)
            {

            }
        }

        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            Uczestnik edd = (Uczestnik)dataGrid.SelectedItem;
            Usun(edd.ID);
            System.Threading.Thread.Sleep(100);
            gridItemsRefresh();
        }

        private void gridItemsRefresh()
        {
            dataGrid.ItemsSource = Baza.context.Uczestnicy.ToList();
            dataGrid.Items.Refresh();
        }

        public static void Usun(int id)
        {
            var local = Baza.context.Set<Uczestnik>()
                    .Local
                    .FirstOrDefault(e => e.ID == id);
            if (local != null)
            {
                Baza.context.Entry(local).State = EntityState.Detached;
            }
            Uczestnik doUsuniecia = new Uczestnik() { ID = id };
            Baza.context.Uczestnicy.Attach(doUsuniecia);
            Baza.context.Uczestnicy.Remove(doUsuniecia);
            Baza.context.SaveChanges();
        }
    }
}
