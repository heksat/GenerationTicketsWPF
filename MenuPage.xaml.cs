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

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        //private Worker user;
        public MenuPage()
        {
            InitializeComponent();
            if (Config.User.RoleId == 1)
            {
                Menu adminPanel = new Menu() { Height = 30, VerticalAlignment = VerticalAlignment.Top};
                var regBut = new MenuItem() { Header = "Регистрация пользователя"};
                var changeTable = new MenuItem() { Header = "Изменить таблицы" };
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    // Query for all blogs with names starting with B
                    var tableNames = from tn in db.TableNames select tn;
                    foreach (var item in tableNames)
                    {
                        changeTable.Items.Add(new MenuItem() { Header = item.Name });
                    }
                }
                adminPanel.Items.Add(regBut);
                adminPanel.Items.Add(changeTable);
                regBut.Click += ((x, y) => this.NavigationService.Navigate(new Registration()));
                MainGrid.Children.Add(adminPanel);
                Grid.SetColumn(adminPanel, 0);
                Grid.SetRow(adminPanel, 0);
                Grid.SetColumnSpan(adminPanel, 5); //создание и размещение админ панели
                
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    //var listlocal = new Role();
                    // db.Roles.Attach(listlocal);
                   // db.Roles.FromSqlRaw("select Role_ID,Role_descryption from Roles").ToList();
                    //check.ItemsSource = db("select name from Sys.objects where type = 'U'").ToList();
                }

            }

        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddTickets());
        }

        //public Menu(Worker user): this()
        //{
        //    this.user = user;
        //}
    }
}
