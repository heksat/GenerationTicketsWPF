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
using GenerationTicketsWPF.Models;
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
        }

        private void Log_In(object sender, RoutedEventArgs e)
        {
            var db = (new DbInteraction());
            Config.User = db.Auth(login.Text, password.Password);
            if (Config.User != null)
            {
                if (Config.User.RoleId == 1)
                {
                    db.checkactualdiscip();
                }
                this.NavigationService.Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("Не получилось!(");
            }
        }
    }
}
