using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
      //  private Role RoleId = null;
      //  private Discipline DisciplineId = null;
        public Registration()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                ListDiscipline.ItemsSource = db.Disciplines.Select(x => x.DisciplineName).ToList();
                ListRoles.ItemsSource = db.Roles.Select(x => x.RoleDecryption).ToList();
            }
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Role roleid = null;
            Discipline disciplineid = null;
            
            int countcheck = 0;
            if (!(LName.Text != "" && LName.Text.Length <= 50))
            {
                LName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            if (!(FName.Text != "" && FName.Text.Length <= 50))
            {
                FName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            if (!(SName.Text != "" && SName.Text.Length <= 50))
            {
                SName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            if (ListDiscipline.SelectedIndex == -1)
            {
                ListDiscipline.Background = new SolidColorBrush(Colors.Red); //не меняет цвет, условие рабочее. 
            }
            else {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    disciplineid = db.Disciplines.Where(x => x.DisciplineName == ListDiscipline.SelectedItem.ToString()).Select(x => x).FirstOrDefault();
                }
                if (disciplineid == null)
                    MessageBox.Show("Такой роли нет");
                else
                {
                    countcheck++;
                }
            }
            if (ListRoles.SelectedIndex == -1)
            {
                ListRoles.BorderBrush = new SolidColorBrush(Colors.Red); //не меняет цвет, условие рабочее. 
            }
            else
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    roleid = db.Roles.Where(x => x.RoleDecryption == ListRoles.SelectedItem.ToString()).Select(x => x).FirstOrDefault();
                }
                if (roleid == null)
                    MessageBox.Show("Такой роли нет");
                else
                    countcheck++;
            }
                if (Login.Text != "" && Login.Text.Length>5)
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    if ((db.Workers.Select(x => x.WorkerLogin).Contains(Login.Text)))
                    {
                        Login.BorderBrush = Brushes.Red;
                    }
                    else
                        countcheck++;
                }
            }
            else
            {
                Login.BorderBrush = Brushes.Red;
            }
            if (!(Password.Password != "" && Password.Password.Length <= 50))
            {
                Password.BorderBrush = TryPassword.BorderBrush = Brushes.Red;
                if (Password.Password == "")
                {
                    MessageBox.Show("Заполните поле пароля");
                }
                else
                {
                    MessageBox.Show("Пароль должен быть короче 50 символов");
                }
            }
            else
                if (Password.Password.Equals(TryPassword.Password))
            {
                countcheck += 2;
            }
            else
            {
                Password.BorderBrush = Brushes.Red;
                MessageBox.Show("Пароли не равны");
            }
            if (countcheck == 8)
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                   db.Workers.Add(new Worker() { Lname = LName.Text, Fname = FName.Text, Sname = SName.Text, DisciplineId = (int)disciplineid.DisciplineId, WorkerLogin = Login.Text, RoleId = (int)roleid.RoleId, WorkerPassword = Password.Password });
                   db.SaveChanges();
                    if (roleid.RoleDecryption == "Teacher")
                    {
                        var idnew = db.Workers.Where(x => x.WorkerLogin == Login.Text).Select(x => x.WorkerId).FirstOrDefault();
                        if (idnew != 0)
                        {
                            db.Teachings.Add(new Teaching() { WorkerId = (int)idnew, DisciplineId = disciplineid.DisciplineId });
                            db.SaveChanges();
                        }

                    }
                }
            }
            
        }

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            var el = (Control)sender;
            el.BorderBrush = Brushes.DarkGray;//Brushes.Gray;//new SolidColorBrush(Color.FromRgb(FFABADB3);
        }

        //private void ListRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    using (var db = new GenerationTicketsContext(Config.Options)) {
        //        RoleId = db.Roles.Where(x => x.RoleDecryption == ((ComboBox)sender).SelectedItem.ToString()).Select(x => x.RoleId).FirstOrDefault();
        //        MessageBox.Show($"{RoleId}");
        //    }
        //}
    }
}
