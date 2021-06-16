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
using System.Windows.Shapes;
using GenerationTicketsWPF.Models;
namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckStringOnSpaceAndStringEmpty(Pass.Password) && !CheckStringOnSpaceAndStringEmpty(TryPass.Password) && TryPass.Password.Length<50 && Pass.Password.Length<50)
            {
                if (Pass.Password.Equals(TryPass.Password))
                {
                    var DBhelper = new DbInteraction();
                    DBhelper.UpdatePassword(Pass.Password);
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод пароля!\n Подсказка: Нельзя использовать пробелы и пароль не должен превышать 50 символов! ");
            }
        }
        private bool CheckStringOnSpaceAndStringEmpty(string str)
        {
            bool check = false;
            if (!str.Contains(" "))
            {
                if (str == String.Empty)
                {
                    check = true;
                }
            }
            else
                check = true;
            return check;
        }
    }
}
