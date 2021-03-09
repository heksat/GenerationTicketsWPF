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
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
            using (GenerationTicketsContext db = new GenerationTicketsContext(Config.Options))
            {
                var users = db.Chairmans.ToList();
                foreach (Chairman e in users)
                {
                    //Test.Text = $" {e.Lname} and {e.Sname}\n"; //test connection
                }
            }
        }

        private void Log_In(object sender, RoutedEventArgs e)
        {
          // Worker user = null;
            using (GenerationTicketsContext db = new GenerationTicketsContext(Config.Options))
            {
                Config.User = db.Workers.Where(el => el.WorkerLogin.Equals(login.Text) && el.WorkerPassword.Equals(password.Password)).FirstOrDefault();
                if (Config.User != null)
                {
                    this.NavigationService.Navigate(new Menu());   
                }
                
            }
        }
    }
}
