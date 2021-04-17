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
using GenerationTicketsWPF.Pages;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PersonalArea : Page
    {
        private bool pressed = false;
        public PersonalArea()
        {
            InitializeComponent();
            GridPA.DataContext = Config.User;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            pressed = !pressed;
            AllowOrBlockBox(pressed);
            if (pressed)
            {
                ((Button)sender).Content = "Применить";
            }
            else
            {
                    if (CheckStringOnSpaceAndStringEmpty(LnameBox.Text) || CheckStringOnSpaceAndStringEmpty(FnameBox.Text) || CheckStringOnSpaceAndStringEmpty(SnameBox.Text))
                    {
                        MessageBox.Show("Проверьте правильность измененных значений");
                    }
                    else
                    {
                        BindingExpression expression = LnameBox.GetBindingExpression(TextBox.TextProperty);
                        BindingExpression expression2 = FnameBox.GetBindingExpression(TextBox.TextProperty);
                        BindingExpression expression3 = SnameBox.GetBindingExpression(TextBox.TextProperty);
                        expression.UpdateSource();
                        expression2.UpdateSource();
                        expression3.UpdateSource();
                    }

                MessageBox.Show($"{Config.User.Lname},{Config.User.Fname},{Config.User.Sname}");
                ((Button)sender).Content = "Изменить";
            }

        }
        private void AllowOrBlockBox(bool value)
        {
            LnameBox.IsEnabled = value;
            FnameBox.IsEnabled = value;
            SnameBox.IsEnabled = value;
        }
        
        private bool CheckStringOnSpaceAndStringEmpty(string str)
        {
            bool check = false;
            if (!str.Contains(" ")) {
                if (str == String.Empty)
                {
                    check = true;
                }
            }
            else
                check = true;
            return check;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            var changepasswin = new ChangePassword() {Width = 300 };
            changepasswin.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
