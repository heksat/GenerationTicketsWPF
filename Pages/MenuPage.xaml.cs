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
using System.Reflection;
using GenerationTicketsWPF.Models;
using GenerationTicketsWPF.Pages;

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
                //PropertyInfo[] properties = db.GetType().GetProperties().Where(p => !p.GetMethod.IsVirtual).ToArray();
                AdmBut1.IsEnabled = true;
                AdmBut2.IsEnabled = true;
                //Menu adminPanel = new Menu() { Height = 30, VerticalAlignment = VerticalAlignment.Top};
                //var regBut = new MenuItem() { Header = "Регистрация пользователя"};
                //var changeTable = new MenuItem() { Header = "Изменить таблицы" };
                //adminPanel.Items.Add(regBut);
                //adminPanel.Items.Add(changeTable);
                //regBut.Click += ((x, y) => this.NavigationService.Navigate(new Registration()));
                //changeTable.Click += ((x, y) => NavigationService.Navigate(new ChangeTables()));
                //MainGrid.Children.Add(adminPanel);
               // Grid.SetColumn(adminPanel, 0);
               // Grid.SetRow(adminPanel, 0);
               // Grid.SetColumnSpan(adminPanel, 5); //создание и размещение админ панели
              //  using (var db = new GenerationTicketsContext(Config.Options))
             //   {
                    //var listlocal = new Role();
                    // db.Roles.Attach(listlocal);
                   // db.Roles.FromSqlRaw("select Role_ID,Role_descryption from Roles").ToList();
                    //check.ItemsSource = db("select name from Sys.objects where type = 'U'").ToList();
              //  }

            }

        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddTickets());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Generation());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalArea());
        }

        private void ViewTicketsList(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewTickets());
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void Change_DB(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeTables());
        }

        //public Menu(Worker user): this()
        //{
        //    this.user = user;
        //}
    }
}
