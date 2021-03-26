using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Generation.xaml
    /// </summary>

    public partial class Generation : Page
    {
        private List<Task> teorqesta;
        private List<Task> practqesta;
        public Generation()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                DiscipList.ItemsSource = (from p in db.Disciplines
                                          join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                                          where c.WorkerId == Config.User.WorkerId
                                          select p.DisciplineName).ToList();
                Lvl.ItemsSource= db.Levels.Select(x => x.LeverDecryption).ToList();
            }

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
           // teorqesta = db.Tasks.Join()
        }

        private void DiscipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                Chairmen.ItemsSource = (from p in db.Chairmans
                                        join c in db.Specialties on p.ChairmanId equals c.ChairmanId
                                        join x in db.Disciplines on c.SpecialtyId equals x.SpecialtyId
                                        join t in db.Teachings on x.DisciplineId equals t.DisciplineId
                                        where (t.WorkerId == Config.User.WorkerId) && (((ComboBox)sender).SelectedItem.ToString()==x.DisciplineName)
                                        select p.Lname + ' ' + p.Fname + ' ' + p.Sname).Distinct().ToList();
            }
          
        }

        private void CountTickets_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
