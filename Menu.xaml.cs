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

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        //private Worker user;
        public Menu()
        {
            InitializeComponent();
            if (Config.User.RoleId == 1)
            {
                Button but = new Button()
                {
                    Content = "Doroy",
                    HorizontalAlignment = new System.Windows.HorizontalAlignment(),
                    Width = 100,
                    Height = 50,
                    Margin = new System.Windows.Thickness(573, 225, 0, 0),

                };
                but.Click += ((x, y) => this.NavigationService.Navigate(new Registration()));
                MainGrid.Children.Add(but);

            }

        }
   
        //public Menu(Worker user): this()
        //{
        //    this.user = user;
        //}
    }
}
