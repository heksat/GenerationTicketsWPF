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
using System.Security.Permissions;

namespace GenerationTicketsWPF.Pages
{

    /// <summary>
    /// Interaction logic for ChangeTables.xaml
    /// </summary>

    public partial class ChangeTables : Page
    {
        //public event UnhandledExceptionEventHandler? UnhandledException;

        public GenerationTicketsContext db = new GenerationTicketsContext(Config.Options);
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public ChangeTables()
        {
            try
            {
                InitializeComponent();
                MessageBox.Show("Все действия проделанные здесь после сохранения на прямую влияет на базу данных! При удалении какой-то записи также удаляются данные, связанные с удаленной записью в зависимых таблицах. При сохранении обязательно проверить правильности изменений. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
            try
            {
                db.SaveChanges();
           
            ChairGrid.Items.Refresh();
            DispGrid.Items.Refresh();
            LvlGrid.Items.Refresh();
            RoleGrid.Items.Refresh();
            SpecGrid.Items.Refresh();
            TaskGrid.Items.Refresh();
            TeachGrid.Items.Refresh();
            TypeGrid.Items.Refresh();
            TickGrid.Items.Refresh();
            WorkGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.ControlItems.Items.Refresh();
            //ControlItems.TabPages
            //foreach (var item in ControlItems.Items)
            //{
            //   item.tab
            //  if (item is TabItem tabItem)
            {
               //     if (tabItem)
             //       tabItem.UpdateLayout();
                //}
            //}
//                this.mainGrid.Children

            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) => MessageBox.Show("Так делать нельзя."));
            //UnhandledException += ((s, e) => MessageBox.Show("Так делать нельзя."));
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
            RoleViewSource.Source = db.Roles.Local.ToObservableCollection();
            specViewSource.Source = db.Specialties.Local.ToObservableCollection(); 
            taskViewSource.Source = db.Tasks.Local.ToObservableCollection(); 
            teachViewSource.Source = db.Teachings.Local.ToObservableCollection(); 
            ticketViewSource.Source = db.Tickets.Local.ToObservableCollection(); 
            typesViewSource.Source = db.TypesTasks.Local.ToObservableCollection(); 
            workerViewSource.Source = db.Workers.Local.ToObservableCollection();

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
