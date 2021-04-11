using GenerationTicketsWPF.Models;
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

namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for ChangeTables.xaml
    /// </summary>
    public partial class ChangeTables : Page
    {
        public ChangeTables()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                ChairGrid.ItemsSource = db.Chairmans.Select(x => x).ToList();
                DispGrid.ItemsSource = db.Disciplines.Select(x => x).ToList();
                LvlGrid.ItemsSource = db.Levels.Select(x => x).ToList();
                RoleGrid.ItemsSource = db.Roles.Select(x => x).ToList();
                SpecGrid.ItemsSource = db.Specialties.Select(x => x).ToList();
                TaskGrid.ItemsSource = db.Tasks.Select(x => x).ToList();
                TeachGrid.ItemsSource = db.Teachings.Select(x => x).ToList();
                TickGrid.ItemsSource = db.Tickets.Select(x => x).ToList();
                TypeGrid.ItemsSource = db.TypesTasks.Select(x => x).ToList();
                WorkGrid.ItemsSource = db.Workers.Select(x => x).ToList();

            }
        }

        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
