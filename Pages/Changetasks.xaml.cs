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
using GenerationTicketsWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for Changetasks.xaml
    /// </summary>
    public partial class Changetasks : Page
    {
        public GenerationTicketsContext db = new GenerationTicketsContext(Config.Options);
        public Changetasks()
        {
            InitializeComponent();
        }
        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TaskGrid.Items.Refresh();
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) => MessageBox.Show("Так делать нельзя."));
            System.Windows.Data.CollectionViewSource taskViewSource =
                  ((System.Windows.Data.CollectionViewSource)(this.FindResource("taskViewSource")));
            db.Tasks.Load();
            taskViewSource.Source = db.Tasks.Local.ToObservableCollection();
        }


    }
}    
    

