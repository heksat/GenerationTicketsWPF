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
        public Generation()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                DiscipList.ItemsSource = (from p in db.Disciplines
                                          join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                                          where c.WorkerId == Config.User.WorkerId
                                          select p.DisciplineName).ToList();
                
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DiscipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                //Chairmen.ItemsSource = (from p in db.Chairmans
                //                        join 

            }
          
        }
    }
}
