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
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for ChangeTables.xaml
    /// </summary>
    public partial class ChangeTables : Page
    {
        public GenerationTicketsContext db = new GenerationTicketsContext(Config.Options);
        public ChangeTables()
        {
            InitializeComponent();
            //var db = new GenerationTicketsContext(Config.Options)
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
                // ChairGrid.ItemsSource = db.Chairmans.Select(x => x).ToList();
                //DispGrid.ItemsSource = db.Disciplines.Select(x => x).ToList();
                //LvlGrid.ItemsSource = db.Levels.Select(x => x).ToList();
                //RoleGrid.ItemsSource = db.Roles.Select(x => x).ToList();
                //SpecGrid.ItemsSource = db.Specialties.Select(x => x).ToList();
                //TaskGrid.ItemsSource = db.Tasks.Select(x => x).ToList();
                //TeachGrid.ItemsSource = db.Teachings.Select(x => x).ToList();
                //TickGrid.ItemsSource = db.Tickets.Select(x => x).ToList();
                //TypeGrid.ItemsSource = db.TypesTasks.Select(x => x).ToList();
                //WorkGrid.ItemsSource = db.Workers.Select(x => x).ToList();

           // }
        }

        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{

                db.SaveChanges();
            // Refresh the grids so the database generated values show up.
            this.ChairGrid.Items.Refresh();
            //this.ControlItems.Items.Refresh();
           // foreach (var item in ControlItems.Items)
           // {
              //  if (item is TabItem tabItem)
             //   {
             //       tabItem.UpdateLayout();
             //   }
            //}
//                this.mainGrid.Children

            //}
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
                
                System.Windows.Data.CollectionViewSource chairmanViewSource =
                   ((System.Windows.Data.CollectionViewSource)(this.FindResource("chairmanViewSource")));
            System.Windows.Data.CollectionViewSource discipViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("discipViewSource")));
            System.Windows.Data.CollectionViewSource LvlViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("LvlViewSource")));
            System.Windows.Data.CollectionViewSource RoleViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("RoleViewSource")));
            System.Windows.Data.CollectionViewSource specViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("specViewSource")));
            System.Windows.Data.CollectionViewSource taskViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("taskViewSource")));
            System.Windows.Data.CollectionViewSource ticketViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("ticketViewSource")));
            System.Windows.Data.CollectionViewSource teachViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("teachViewSource")));
            System.Windows.Data.CollectionViewSource typesViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("typesViewSource")));
            System.Windows.Data.CollectionViewSource workerViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("workerViewSource")));
            db.Chairmans.Load();
            db.Disciplines.Load();
            db.Levels.Load();
            db.Roles.Load();
            db.Specialties.Load();
            db.Tasks.Load();
            db.Teachings.Load();
            db.Tickets.Load();
            db.Workers.Load();
            db.TypesTasks.Load();
                chairmanViewSource.Source = db.Chairmans.Local.ToObservableCollection();
            discipViewSource.Source = db.Disciplines.Local.ToObservableCollection();
            LvlViewSource.Source = db.Levels.Local.ToObservableCollection();
            

            // ((ObservableCollection<Chairman>)chairmanViewSource.Source).CollectionChanged += Chairman_CollectionChanged;

            //}
        }
        //private static void Chairman_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Remove:
        //            using (var db = new GenerationTicketsContext(Config.Options))
        //            {
        //                db.Chairmans.Remove((Chairman)e.OldItems[0]);
        //            }
        //            break;
        //    }
        //}
    }
}
